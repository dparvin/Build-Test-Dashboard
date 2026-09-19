using Build_Test_Dashboard.Enums;
using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Results;

public class SaveRepositoryResult
{
    public RepositoryValidationResult ValidationResult { get; set; }
    public Repository? Repository { get; set; }
}