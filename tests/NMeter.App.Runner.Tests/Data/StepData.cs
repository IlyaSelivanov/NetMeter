using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Tests.Data
{
    public static class StepData
    {
        public static Step GetData()
        {
            return new Step
            {
                Body = @"
                {
                    ""body"": ""body""
                }
                ",
                Headers = new List<Header>
                {
                    new Header
                    {
                        Key = "header",
                        Value = "header"
                    }
                },
                Parameters = new List<UrlParameter>
                {
                    new UrlParameter
                    {
                        Key = "parameter",
                        Value = "parameter"
                    }
                }
            };
        }
    }
}