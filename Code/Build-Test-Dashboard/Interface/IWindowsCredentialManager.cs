namespace Build_Test_Dashboard.Interface;

/// <summary>
/// Represents an interface for interacting with Windows Credential Manager.
/// </summary>
public interface IWindowsCredentialManager
{
    /// <summary>
    /// Stores a credential.
    /// </summary>
    /// <param name="target">The credential target name.</param>
    /// <param name="username">The username.</param>
    /// <param name="secret">The secret.</param>
    void Write(string target, string? username, string? secret);

    /// <summary>
    /// Retrieves a credential.
    /// </summary>
    /// <param name="target">The credential target name.</param>
    /// <returns>The credential, or <see langword="null"/> if it does not exist.</returns>
    (string? Username, string? Secret)? Read(string target);

    /// <summary>
    /// Deletes a credential.
    /// </summary>
    /// <param name="target">The credential target name.</param>
    /// <returns><see langword="true"/> if the credential was deleted; otherwise, <see langword="false"/>.</returns>
    bool Delete(string target);
}