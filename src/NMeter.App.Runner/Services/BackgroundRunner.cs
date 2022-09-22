using System.Net.Http;
using Microsoft.Net.Http.Headers;

namespace NMeter.App.Runner.Services
{
    public class BackgroundRunner : BackgroundService
    {
        private readonly ILogger<BackgroundRunner> _logger;
        private readonly IBackgroundQueue _backgroundQueue;
        private readonly IHttpClientFactory _httpClientFactory;

        public BackgroundRunner(ILogger<BackgroundRunner> logger,
            IBackgroundQueue backgroundQueue,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _backgroundQueue = backgroundQueue;
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("--> Starting Execution...");

            while(!stoppingToken.IsCancellationRequested)
            {
                var planExecution =  await _backgroundQueue.DequeueBackgroundItemAsync();
                _logger.LogInformation($"--> Proceeding execution {planExecution.Execution.Id}.");
                _logger.LogInformation($"--> Proceeding plan {planExecution.Plan.Id}.");
            }
        }

        public async Task OnGet()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                "/api/v1/platforms")
            {
                Headers =
            {
                { HeaderNames.Accept, "application/vnd.github.v3+json" },
                { HeaderNames.UserAgent, "HttpRequestsSample" }
            }
            };

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(@"http://microservices.com");
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var responseMessage = await httpResponseMessage.Content.ReadAsStringAsync();
                _logger.LogInformation(responseMessage);
            }
        }
    }
}