using System.Diagnostics;
using System.Text;
using NMeter.App.Runner.Data;
using NMeter.App.Runner.Interfaces;
using NMeter.App.Runner.Models;
using NMeter.App.Runner.Primitives;

namespace NMeter.App.Runner.Services
{
    public class HttpRequestStep : AbstractStep, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPlanVariablesManager _planVariablesManager;
        private readonly IResultRepository _resultRepository;
        private readonly PlanExecution _planExecution;
        private readonly ILogger<HttpRequestStep> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Uri _baseUri;
        private readonly ICollection<PlanGlobalVariable> _planVariables;
        private readonly Step _step;

        private HttpClient _httpClient;
        private HttpRequestMessage _requestMessage = new HttpRequestMessage();
        private HttpResponseMessage _responseMessage;
        private IServiceScope _scope;
        private Stopwatch _watch = new Stopwatch();

        public HttpRequestStep(IServiceProvider serviceProvider,
            PlanExecution planExecution,
            ICollection<PlanGlobalVariable> planVariables,
            Step step)
        {
            _serviceProvider = serviceProvider;
            _logger = LoggerFactory
                .Create(configure => configure.AddConsole())
                .CreateLogger<HttpRequestStep>();

            _httpClientFactory = _serviceProvider.GetRequiredService<IHttpClientFactory>();
            _planVariablesManager = _serviceProvider.GetRequiredService<IPlanVariablesManager>();

            _scope = _serviceProvider.CreateScope();
            _resultRepository = _scope.ServiceProvider.GetRequiredService<IResultRepository>();

            _planExecution = planExecution;
            _planVariables = planVariables;
            _step = step;
            _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(_planExecution.Plan.BaseUrl);
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposing");
            _scope.Dispose();
        }

        protected override async Task AfterExecution()
        {
            _logger.LogInformation($"{nameof(AfterExecution)}");

            await _planVariablesManager.RefreshPlanVariablesAsync(_responseMessage, _planVariables);

            var result = new Result();
            result.ResponseBody = await _responseMessage.Content.ReadAsStringAsync();
            result.ResponseHeaders = _responseMessage.Headers.ToString();
            result.ResponseCode = (int)_responseMessage.StatusCode;
            result.ResponseTime = _watch.ElapsedMilliseconds;
            result.StepId = _step.Id;
            result.ExecutionId = _planExecution.Execution.Id;

            await _resultRepository.SaveResultAsync(result);
        }

        protected override Task BeforeExecution()
        {
            _logger.LogInformation($"{nameof(BeforeExecution)}");

            _planVariablesManager.UpdateRequestData(_planVariables, _step);
            _requestMessage = new HttpRequestMessageBuilder().Build(_step);
            _watch.Reset();

            return Task.CompletedTask;
        }

        protected override async Task ExecuteStep()
        {
            _logger.LogInformation($"{nameof(ExecuteStep)}");

            try
            {
                _watch.Start();
                _responseMessage = await _httpClient.SendAsync(_requestMessage);
                _watch.Stop();
                _logger.LogDebug(_responseMessage.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}