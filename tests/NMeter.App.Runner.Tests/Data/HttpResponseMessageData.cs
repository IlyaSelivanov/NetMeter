using System.Net.Http.Json;

namespace NMeter.App.Runner.Tests.Data
{
    public record RefreshResponseContentModel(string str, int @int, int[] arr);
    public record UpdatehResponseContentModel(string body, string header, string parameter);

    public static class HttpResponseMessageData
    {
        public static HttpResponseMessage GetRefreshData()
        {
            var data = new HttpResponseMessage();

            data.Content = JsonContent.Create<List<RefreshResponseContentModel>>(
                new List<RefreshResponseContentModel>
                {
                    new RefreshResponseContentModel("str0", 0, new int[] {0, 0}),
                    new RefreshResponseContentModel("str1", 1, new int[] {1, 1})
                }
            );

            return data;
        }

        public static HttpResponseMessage GetUpdateData()
        {
            var data = new HttpResponseMessage();

            data.Content = JsonContent.Create<List<UpdatehResponseContentModel>>(
                new List<UpdatehResponseContentModel>
                {
                    new UpdatehResponseContentModel("body", "header", "parameter"),
                }
            );

            return data;
        }
    }
}