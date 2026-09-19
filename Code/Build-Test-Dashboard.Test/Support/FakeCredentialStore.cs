using Build_Test_Dashboard.Interface;
using Build_Test_Dashboard.Models;

namespace Build_Test_Dashboard.Test.Support;

public class FakeCredentialStore : ICredentialStore
{
    public RepositoryCredential? StoredCredential { get; set; }

    public int StoreCallCount { get; private set; }

    public Task StoreAsync(
        RepositoryCredential credential,
        CancellationToken cancellationToken = default)
    {
        StoreCallCount++;
        StoredCredential = credential;

        return Task.CompletedTask;
    }

    public Task<RepositoryCredential?> GetAsync(
        int repositoryId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(
        int repositoryId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}