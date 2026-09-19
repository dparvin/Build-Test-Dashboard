using Build_Test_Dashboard.Enums;

namespace Build_Test_Dashboard.Test.Support;

public class FakeRepositoryServiceState
{
    /// <summary>
    /// Gets or sets a value indicating whether [use real service].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [use real service]; otherwise, <c>false</c>.
    /// </value>
    public bool UseRealService { get; set; } = true;

    /// <summary>
    /// Gets or sets the validation result.
    /// </summary>
    /// <value>
    /// The validation result.
    /// </value>
    public RepositoryValidationResult ValidationResult { get; set; } = RepositoryValidationResult.Valid;
}