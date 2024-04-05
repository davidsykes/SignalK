namespace Logic.Wrappers
{
    internal interface IHttpClientWrapper
    {
        Task<string> GetAsync(string endpointsUrl);
    }
}
