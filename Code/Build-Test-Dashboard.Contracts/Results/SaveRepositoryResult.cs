using Build_Test_Dashboard.Contracts.Models;
using Build_Test_Dashboard.Contracts.Enums;

namespace Build_Test_Dashboard.Contracts.Results;

public class SaveRepositoryResult
{
    public RepositoryValidationResult ValidationResult { get; set; }
    public Repository? Repository { get; set; }
}