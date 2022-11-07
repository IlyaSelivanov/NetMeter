using System.Net.Http;
using Microsoft.Net.Http.Headers;
using NMeter.App.Runner.Interfaces;
using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Services
{
    public class BackgroundRunner : BackgroundService
    {
        private readonly ILogger<BackgroundRunner> _logger;
        private readonly IExecutionQueue _executionQueue;
        private readonly IHttpClientFactory _httpClientFactory;

        public BackgroundRunner(ILogger<BackgroundRunner> logger,
            IExecutionQueue executionQueue,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _executionQueue = executionQueue;
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("--> Starting Execution...");

            while (!stoppingToken.IsCancellationRequested)
            {
                var planExecution = await _executionQueue.DequeueBackgroundItemAsync();

                _logger.LogInformation($"--> Proceeding execution {planExecution.Execution.Id}.");
                _logger.LogInformation($"--> Proceeding plan {planExecution.Plan.Id}.");

                FireAndForget(planExecution);
            }
        }

        private void FireAndForget(PlanExecution planExecution)
        {
            _logger.LogInformation($"--> Fire execution: {planExecution.Execution.Id}");

            var plan = planExecution.Plan;
            for (int i = 0; i < plan.Profile.UsersNumber; i++)
            {
                _logger.LogInformation($"--> USer-{i} is starting...");
                Task.Run(async () =>
                {
                    var httpClient = _httpClientFactory.CreateClient();
                    httpClient.BaseAddress = new Uri(plan.BaseUrl);

                    foreach (var step in plan.Steps.OrderBy(s => s.Order))
                    {
                        var responseMessage = await httpClient.GetAsync(step.Path);
                        // _logger.LogInformation($"--> User-{i}: {step.Path} - {responseMessage.StatusCode}");
                    }
                    _logger.LogInformation($"--> USer-{i} is finishing...");
                });
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