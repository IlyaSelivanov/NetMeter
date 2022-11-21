using System.Text;
using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Services
{
    public class HttpRequestMessageBuilder
    {
        private HttpRequestMessage _httpRequestMessage = new HttpRequestMessage();

        public HttpRequestMessageBuilder SetMethod(Method method)
        {
            switch (method)
            {
                case Method.GET:
                    _httpRequestMessage.Method = HttpMethod.Get;
                    break;
                case Method.POST:
                    _httpRequestMessage.Method = HttpMethod.Post;
                    break;
                case Method.PUT:
                    _httpRequestMessage.Method = HttpMethod.Put;
                    break;
                case Method.DELETE:
                    _httpRequestMessage.Method = HttpMethod.Delete;
                    break;
                default:
                    break;
            };

            return this;
        }

        public HttpRequestMessageBuilder SetContent(string content)
        {
            if (!string.IsNullOrEmpty(content))
                _httpRequestMessage.Content = new StringContent(content,
                    Encoding.UTF8,
                    "application/json");

            return this;
        }

        public HttpRequestMessageBuilder SetHeaders(ICollection<Header> headers)
        {
            if (headers != null && headers.Count > 0)
                headers.ToList()
                    .ForEach(item => _httpRequestMessage.Headers.Add(item.Key, item.Value));

            return this;
        }

        public HttpRequestMessageBuilder SetPath(string path)
        {
            if (!string.IsNullOrEmpty(path))
                _httpRequestMessage.RequestUri = new Uri(path, UriKind.Relative);

            return this;
        }

        public HttpRequestMessageBuilder SetPathWithQuery(string path,
            ICollection<UrlParameter> parameters)
        {
            if (string.IsNullOrEmpty(path))
                return this;

            bool isFirst = true;

            var sb = new StringBuilder();
            sb.Append(path);

            if (parameters == null || parameters.Count == 0)
                return this;

            foreach (var parameter in parameters)
            {
                if (parameter.Value == null)
                    continue;

                sb.Append(isFirst ? "?" : "&");
                sb.Append(parameter.Key);
                sb.Append("=");
                sb.Append(parameter.Value);

                isFirst = false;
            }

            _httpRequestMessage.RequestUri = new Uri(sb.ToString(), UriKind.Relative);

            return this;
        }

        public HttpRequestMessage Build(Step step)
        {
            _ = SetMethod(step.Method)
                .SetPathWithQuery(step.Path, step.Parameters)
                .SetHeaders(step.Headers)
                .SetContent(step.Body);

            return _httpRequestMessage;
        }

        public HttpRequestMessage Build()
        {
            return _httpRequestMessage;
        }
    }
}