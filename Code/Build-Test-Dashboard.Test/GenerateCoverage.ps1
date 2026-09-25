param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Debug"
)

$ErrorActionPreference = "Stop"

Write-Host "Running coverage script with configuration: $Configuration"
# Only run if target framework folder is net9.0*
$projectRoot = Split-Path -Parent $MyInvocation.MyCommand.Definition
$targetFramework = "net10.0"
$testDll = Join-Path $projectRoot "bin\$Configuration\$targetFramework\Build-Test-Dashboard.Test.dll"

if (-Not (Test-Path $testDll)) {
    Write-Host "Skipping code coverage for non-net10.0 builds."
    exit 0
}

Remove-Item -Recurse -Force coverage, CoverageReport -ErrorAction Ignore

# Proceed with coverage
dotnet run --project $PSScriptRoot `
  --no-build `
  -- `
  --coverlet `
  --coverlet-output-format cobertura `
  --coverlet-exclude-by-file "**/Migrations/**" `
  --coverlet-exclude-by-file "**/Data/**" `
  --coverlet-exclude-by-attribute GeneratedCode

if ($LASTEXITCODE -ne 0) {
    Write-Error "Code coverage test run failed with exit code $LASTEXITCODE."
    exit $LASTEXITCODE
}

# Full path to ReportGenerator.exe
$rgVersion = "5.5.11"
$reportGeneratorExe = "$env:USERPROFILE\.nuget\packages\reportgenerator\$rgVersion\tools\net10.0\ReportGenerator.exe"

# Paths
$coverageDir = Join-Path $projectRoot "bin\$Configuration\$targetFramework\TestResults"

if (Test-Path $coverageDir) {
    $coverageFile = Get-ChildItem `
        -Path $coverageDir `
        -Filter "coverage.cobertura.*.xml" |
        Sort-Object LastWriteTime -Descending |
        Select-Object -First 1

    if (-not $coverageFile) {
        Write-Warning "Coverage file not found in '$coverageDir'. Skipping report generation."
        return 0
    }
}
else {
    Write-Warning "Coverage folder not found at '$coverageDir'. Skipping report generation."
    return 0
}

$reportDir = "CoverageReport"
$reportIndex = Join-Path $reportDir "index.htm"

# Run ReportGenerator
if (Test-Path $reportGeneratorExe) {
    & $reportGeneratorExe "-reports:$($coverageFile.FullName)" -targetdir:$reportDir -reporttypes:Html -log:report.log -verbosity:Verbose
    if (Test-Path $reportIndex) {
        Start-Process $reportIndex
    } else {
        Write-Host "Report generated, but index.htm not found."
    }
} else {
    Write-Error "ReportGenerator.exe not found at $reportGeneratorExe"
}

return 0