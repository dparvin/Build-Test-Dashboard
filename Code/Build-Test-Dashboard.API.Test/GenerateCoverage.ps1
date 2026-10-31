param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Debug"
)

$ErrorActionPreference = "Stop"

$targetFramework = "net10.0"
$scriptDirectory = $PSScriptRoot
$workerTestProject = Join-Path `
    (Split-Path -Parent $scriptDirectory) `
    "Build-Test-Dashboard.Worker.Test"

$testProjects = @(
    @{
        Name = "Build-Test-Dashboard.API.Test"
        Path = $scriptDirectory
        Assembly = "Build-Test-Dashboard.API.Test.dll"
    },
    @{
        Name = "Build-Test-Dashboard.Worker.Test"
        Path = $workerTestProject
        Assembly = "Build-Test-Dashboard.Worker.Test.dll"
    }
)
Write-Host "Running coverage script with configuration: $Configuration"

# Validate project directories.
foreach ($project in $testProjects) {
    if (-not (Test-Path $project.Path)) {
        throw "Test project directory not found: $($project.Path)"
    }
}

# Clean previous coverage output.
foreach ($project in $testProjects) {
    Remove-Item `
        -Recurse `
        -Force `
        (Join-Path $project.Path "bin\$Configuration\$targetFramework\TestResults") `
        -ErrorAction Ignore
}

Remove-Item -Recurse -Force "CoverageReport" -ErrorAction Ignore
Remove-Item -Force "report.log" -ErrorAction Ignore

# Run tests and collect each Cobertura report.
$coverageFiles = @()

foreach ($project in $testProjects) {
    Write-Host "`nRunning coverage for $($project.Name)..."

    dotnet run --project $project.Path `
        --configuration $Configuration `
        --no-build `
        -- `
        --coverlet `
        --coverlet-output-format cobertura `
        --coverlet-exclude-by-file "**/Migrations/**" `
        --coverlet-exclude-by-file "**/Data/**" `
        --coverlet-exclude-by-attribute GeneratedCode

    if ($LASTEXITCODE -ne 0) {
        throw "Coverage run failed for $($project.Name). Exit code: $LASTEXITCODE"
    }

    $coverageDirectory = Join-Path `
        $project.Path `
        "bin\$Configuration\$targetFramework\TestResults"

    $coverageFile = Get-ChildItem `
        -Path $coverageDirectory `
        -Filter "coverage.cobertura.*.xml" `
        -File `
        -ErrorAction SilentlyContinue |
        Sort-Object LastWriteTime -Descending |
        Select-Object -First 1

    if (-not $coverageFile) {
        throw "Coverage file not found for $($project.Name) in '$coverageDirectory'."
    }

    $coverageFiles += $coverageFile.FullName
}

# Full path to ReportGenerator.exe
$rgVersion = "5.5.11"
$reportGeneratorExe = Join-Path `
    $env:USERPROFILE `
    ".nuget\packages\reportgenerator\$rgVersion\tools\net10.0\ReportGenerator.exe"

if (-not (Test-Path $reportGeneratorExe)) {
    throw "ReportGenerator.exe not found at '$reportGeneratorExe'."
}

# Combine both reports into one HTML report.
$reportDirectory = Join-Path $scriptDirectory "CoverageReport"
$reportIndex = Join-Path $reportDirectory "index.htm"
$reportInput = $coverageFiles -join ";"

Write-Host "`nGenerating combined coverage report..."

& $reportGeneratorExe `
    "-reports:$reportInput" `
    "-targetdir:$reportDirectory" `
    "-reporttypes:Html" `
    "-log:report.log" `
    "-verbosity:Verbose"

if ($LASTEXITCODE -ne 0) {
    throw "ReportGenerator failed with exit code $LASTEXITCODE."
}

if (Test-Path $reportIndex) {
    Start-Process $reportIndex
}
else {
    throw "Report generation completed, but '$reportIndex' was not found."
}

return 0