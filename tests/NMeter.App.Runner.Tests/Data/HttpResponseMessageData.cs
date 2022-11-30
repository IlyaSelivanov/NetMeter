using System.Net.Http.Json;

namespace NMeter.App.Runner.Tests.Data
{
    public record ResponseContentModel(string str, int @int, int[] arr);

    public static class HttpResponseMessageData
    {
        public static HttpResponseMessage GetData()
        {
            var data = new HttpResponseMessage();

            data.Content = JsonContent.Create<List<ResponseContentModel>>(
                new List<ResponseContentModel>
                {
                    new ResponseContentModel("str0", 0, new int[] {0, 0}),
                    new ResponseContentModel("str1", 1, new int[] {1, 1})
                }
            );

            return data;
        }
    }
}