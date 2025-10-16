namespace TaskListApp.Service.ServiceAuthenticator
{
    public interface IHttpClientFactory
    {
        Task<HttpClient> HttpClientWithToken();
    }
}
