namespace NMeter.App.Runner.Services
{
    public class HttpRequestItem
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public HttpRequestItem(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(@"http://microservices.com");
        }

        public void ExecuteItem()
        {
            Task.Run(async () => await ExecuteTask());
        }

        private async Task ExecuteTask()
        {
            await Task.Delay(100);
        }
    }
}