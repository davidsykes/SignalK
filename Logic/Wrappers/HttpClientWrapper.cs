namespace Logic.Wrappers
{
    internal class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient = new();

        public async Task<string> GetAsync(string endpointsUrl)
        {
            var response = await _httpClient.GetAsync(endpointsUrl);
            var responseContent = response.Content;
            using var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
            var responseText = await reader.ReadToEndAsync();
            return responseText;
        }
    }
}
