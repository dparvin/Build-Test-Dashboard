namespace Build_Test_Dashboard.Enums;

/// <summary>
/// Represents the result of validating a repository in the build and test dashboard.
/// </summary>
public enum RepositoryValidationResult
{
    /// <summary>
    /// The valid
    /// </summary>
    Valid,
    /// <summary>
    /// The owner not found
    /// </summary>
    OwnerNotFound,
    /// <summary>
    /// The project not found
    /// </summary>
    ProjectNotFound,
    /// <summary>
    /// The repository not found
    /// </summary>
    RepositoryNotFound,
    /// <summary>
    /// The invalid credentials
    /// </summary>
    InvalidCredentials,
    /// <summary>
    /// The unknown error
    /// </summary>
    UnknownError
}