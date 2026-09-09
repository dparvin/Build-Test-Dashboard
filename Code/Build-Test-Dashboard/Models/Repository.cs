namespace Build_Test_Dashboard.Models
{
    /// <summary>
    /// Represents a repository in the build and test dashboard.
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        public string Provider { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public string Owner { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the repository name.
        /// </summary>
        /// <value>
        /// The repository name.
        /// </value>
        public string RepositoryName { get; set; } = string.Empty;
    }
}