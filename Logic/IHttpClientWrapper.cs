
namespace Logic
{
    internal interface IHttpClientWrapper
    {
        Task<string> GetAsync(string endpointsUrl);
    }
}
