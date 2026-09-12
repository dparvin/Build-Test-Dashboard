using Build_Test_Dashboard.Interface;

namespace Build_Test_Dashboard.Stores;

/// <summary>
/// Represents a credential manager for Windows, responsible for securely storing and retrieving credentials.
/// </summary>
public class WindowsCredentialManager : IWindowsCredentialManager
{
    /// <summary>
    /// Deletes a credential.
    /// </summary>
    /// <param name="target">The credential target name.</param>
    /// <returns>
    ///   <see langword="true" /> if the credential was deleted; otherwise, <see langword="false" />.
    /// </returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public bool Delete(string target) => throw new NotImplementedException();
    /// <summary>
    /// Retrieves a credential.
    /// </summary>
    /// <param name="target">The credential target name.</param>
    /// <returns>
    /// The credential, or <see langword="null" /> if it does not exist.
    /// </returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public (string? Username, string? Secret)? Read(string target) => throw new NotImplementedException();
    /// <summary>
    /// Stores a credential.
    /// </summary>
    /// <param name="target">The credential target name.</param>
    /// <param name="username">The username.</param>
    /// <param name="secret">The secret.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void Write(string target, string? username, string? secret) => throw new NotImplementedException();
}