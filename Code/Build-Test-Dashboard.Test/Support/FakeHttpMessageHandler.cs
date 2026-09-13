using System.Net;

namespace Build_Test_Dashboard.Test.Support;

/// <summary>
/// Represents a fake HTTP message handler for testing purposes.
/// </summary>
/// <seealso cref="System.Net.Http.HttpMessageHandler" />
public class FakeHttpMessageHandler : HttpMessageHandler
{
    /// <summary>
    /// Gets the request.
    /// </summary>
    /// <value>
    /// The request.
    /// </value>
    public HttpRequestMessage? Request { get; private set; }

    /// <summary>
    /// Gets or sets the response.
    /// </summary>
    /// <value>
    /// The response.
    /// </value>
    public HttpResponseMessage Response { get; set; } =
        new HttpResponseMessage(HttpStatusCode.OK);

    /// <summary>
    /// Gets the call count.
    /// </summary>
    /// <value>
    /// The call count.
    /// </value>
    public int CallCount { get; private set; }

    /// <summary>
    /// Gets or sets the send function.
    /// </summary>
    /// <value>
    /// The send function.
    /// </value>
    public Func<HttpRequestMessage, HttpResponseMessage>? SendFunc { get; set; }

    /// <summary>
    /// Send an HTTP request as an asynchronous operation.
    /// </summary>
    /// <param name="request">The HTTP request message to send.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <returns>
    /// The task object representing the asynchronous operation.
    /// </returns>
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        CallCount++;
        Request = request;

        if (SendFunc != null)
            return Task.FromResult(SendFunc(request));

        return Task.FromResult(Response);
    }
}
