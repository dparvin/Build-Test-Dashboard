using Build_Test_Dashboard.Interface;

namespace Build_Test_Dashboard.Test.Support;

/// <summary>
/// Represents a fake implementation of the <see cref="IWindowsCredentialManager"/> interface for testing purposes.
/// </summary>
/// <seealso cref="Build_Test_Dashboard.Interface.IWindowsCredentialManager" />
public class FakeWindowsCredentialManager : IWindowsCredentialManager
{
    /// <summary>
    /// Gets the target.
    /// </summary>
    /// <value>
    /// The target.
    /// </value>
    public string? WriteTarget { get; private set; }
    /// <summary>
    /// Gets the read target.
    /// </summary>
    /// <value>
    /// The read target.
    /// </value>
    public string? ReadTarget { get; private set; }
    /// <summary>
    /// Gets the delete target.
    /// </summary>
    /// <value>
    /// The delete target.
    /// </value>
    public string? DeleteTarget { get; private set; }
    /// <summary>
    /// Gets the username.
    /// </summary>
    /// <value>
    /// The username.
    /// </value>
    public string? Username { get; private set; }
    /// <summary>
    /// Gets the secret.
    /// </summary>
    /// <value>
    /// The secret.
    /// </value>
    public string? Secret { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating what the delete call returns.
    /// </summary>
    /// <value>
    ///   can be set to <c>true</c> or <c>false</c>.
    /// </value>
    public bool DeleteResult { get; set; } = true;
    /// <summary>
    /// Gets or sets the read result.
    /// </summary>
    /// <value>
    /// The read result.
    /// </value>
    public (string? Username, string? Secret)? ReadResult { get; set; }

    /// <summary>
    /// Gets the write call count.
    /// </summary>
    /// <value>
    /// The write call count.
    /// </value>
    public int WriteCallCount { get; private set; }
    /// <summary>
    /// Gets the read call count.
    /// </summary>
    /// <value>
    /// The read call count.
    /// </value>
    public int ReadCallCount { get; private set; }
    /// <summary>
    /// Gets the delete call count.
    /// </summary>
    /// <value>
    /// The delete call count.
    /// </value>
    public int DeleteCallCount { get; private set; }

    /// <summary>
    /// Gets or sets the write action.
    /// </summary>
    /// <value>
    /// The write action.
    /// </value>
    public Action<string, string?, string?>? WriteAction { get; set; }
    /// <summary>
    /// Gets or sets the read function.
    /// </summary>
    /// <value>
    /// The read function.
    /// </value>
    public Func<string, (string? Username, string? Secret)?>? ReadFunc { get; set; }
    /// <summary>
    /// Gets or sets the delete function.
    /// </summary>
    /// <value>
    /// The delete function.
    /// </value>
    public Func<string, bool>? DeleteFunc { get; set; }

    /// <summary>
    /// Deletes a credential.
    /// </summary>
    /// <param name="target">The credential target name.</param>
    /// <returns>
    ///   <see langword="true" /> if the credential was deleted; otherwise, <see langword="false" />.
    /// </returns>
    public bool Delete(string target)
    {
        DeleteCallCount++;
        DeleteTarget = target;
        if (DeleteFunc != null)
            return DeleteFunc(target);
        return DeleteResult;
    }

    /// <summary>
    /// Retrieves a credential.
    /// </summary>
    /// <param name="target">The credential target name.</param>
    /// <returns>
    /// The credential, or <see langword="null" /> if it does not exist.
    /// </returns>
    public (string? Username, string? Secret)? Read(string target)
    {
        ReadCallCount++;
        ReadTarget = target;
        if (ReadFunc != null)
            return ReadFunc(target);
        return ReadResult;
    }

    /// <summary>
    /// Stores a credential.
    /// </summary>
    /// <param name="target">The credential target name.</param>
    /// <param name="username">The username.</param>
    /// <param name="secret">The secret.</param>
    public void Write(string target, string? username, string? secret)
    {
        WriteCallCount++;
        WriteTarget = target;
        Username = username;
        Secret = secret;

        WriteAction?.Invoke(target, username, secret);
    }
}
