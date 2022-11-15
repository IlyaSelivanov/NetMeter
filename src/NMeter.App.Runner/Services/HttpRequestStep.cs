using System.Text;
using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Services
{
    public class HttpRequestStep : AbstractStep
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<HttpRequestStep> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Uri _baseUri;
        private readonly Step _step;

        private HttpClient _httpClient;
        private HttpRequestMessage _requestMessage = new HttpRequestMessage();
        private HttpResponseMessage _responseMessage;

        public HttpRequestStep(IServiceProvider serviceProvider,
            Uri baseUri,
            Step step)
        {
            _serviceProvider = serviceProvider;
            _logger = LoggerFactory
                .Create(configure => configure.AddConsole())
                .CreateLogger<HttpRequestStep>();

            using (var scope = _serviceProvider.CreateScope())
                _httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();

            _baseUri = baseUri;
            _step = step;
            _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = baseUri;
        }

        protected override Task AfterExecution()
        {
            _logger.LogInformation($"{nameof(AfterExecution)}");

            return Task.CompletedTask;
        }

        protected override Task BeforeExecution()
        {
            _logger.LogInformation($"{nameof(BeforeExecution)}");

            _requestMessage.Content = new ByteArrayContent(Encoding.UTF8.GetBytes(_step.Body));
            switch (_step.Method)
            {
                case Method.GET:
                    _requestMessage.Method = HttpMethod.Get;
                    break;
                case Method.POST:
                    _requestMessage.Method = HttpMethod.Post;
                    break;
                case Method.PUT:
                    _requestMessage.Method = HttpMethod.Put;
                    break;
                case Method.DELETE:
                    _requestMessage.Method = HttpMethod.Delete;
                    break;
                default:
                    break;
            };
            _requestMessage.RequestUri = _step.Path == string.Empty ? null : new Uri(_step.Path, UriKind.Relative);
            _step.Headers
                .ToList()
                .ForEach(item => _requestMessage.Headers.Add(item.Key, item.Value));

            return Task.CompletedTask;
        }

        protected override async Task ExecuteStep()
        {
            _logger.LogInformation($"{nameof(ExecuteStep)}");

            try
            {
                _responseMessage = await _httpClient.SendAsync(_requestMessage);
                _logger.LogDebug(_responseMessage.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}