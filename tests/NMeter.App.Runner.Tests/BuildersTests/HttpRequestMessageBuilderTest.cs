using NMeter.App.Runner.Models;
using NMeter.App.Runner.Services;

namespace NMeter.App.Runner.Tests
{
    [TestClass]
    public class HttpRequestMessageBuilderTest
    {
        private Step _step;
        private string _content;
        private ICollection<Header> _headers;
        private ICollection<UrlParameter> _parameters;
        private const string PATH_WITH_PARAMETERS = @"/relative_path?param1=value1&param2=value2";

        public HttpRequestMessageBuilderTest()
        {
            _step = new Step
            {
                Id = 1,
                Path = @"/relative_path",
                Order = 0,
                Method = Method.POST,
                PlanId = 1
            };
            _content = "{ field: value }";
            _headers = new List<Header>
            {
                new Header
                {
                    Key = "header1",
                    Value = "value1"
                },
                new Header
                {
                    Key = "header2",
                    Value = "value2"
                }
            };
            _parameters = new List<UrlParameter>
            {
                new UrlParameter
                {
                    Key = "param1",
                    Value = "value1"
                },
                new UrlParameter
                {
                    Key = "param2",
                    Value = "value2"
                }
            };

            _step.Body = _content;
            _step.Headers = _headers;
            _step.Parameters = _parameters;
        }

        [TestMethod]
        public void Empty_Step_Negative_Test()
        {
            var step = new Step();

            var httpRequestMessage = new HttpRequestMessageBuilder().Build(step);
            var result = httpRequestMessage.Content == null
                && httpRequestMessage.Method == HttpMethod.Get
                && httpRequestMessage.Headers.Count() == 0
                && httpRequestMessage.RequestUri == null;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Set_Method_Test()
        {
            var httpRequestMessage = new HttpRequestMessageBuilder()
                .SetMethod(_step.Method)
                .Build();

            Assert.AreEqual(HttpMethod.Post, httpRequestMessage.Method);
        }

        [TestMethod]
        public void Set_Content_Test()
        {
            var httpRequestMessage = new HttpRequestMessageBuilder()
                .SetContent(_step.Body)
                .Build();

            var httpContent = httpRequestMessage.Content?.ReadAsStringAsync().Result;

            Assert.AreEqual(_content, httpContent);
        }

        [TestMethod]
        public void Set_Headers_Test()
        {
            var httpRequestMessage = new HttpRequestMessageBuilder()
                .SetHeaders(_step.Headers)
                .Build();

            Assert.AreEqual(_headers.Count, httpRequestMessage.Headers.Count());
        }

        [TestMethod]
        public void Set_Path_Test()
        {
            var httpRequestMessage = new HttpRequestMessageBuilder()
                .SetPath(_step.Path)
                .Build();

            Assert.AreEqual(_step.Path, httpRequestMessage.RequestUri?.OriginalString);
        }

        [TestMethod]
        public void Set_Path_With_Parameters_Test()
        {
            var httpRequestMessage = new HttpRequestMessageBuilder()
                .SetPathWithQuery(_step.Path, _step.Parameters)
                .Build();

            Assert.AreEqual(PATH_WITH_PARAMETERS, httpRequestMessage.RequestUri?.OriginalString);
        }
    }
}