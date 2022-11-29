using System.Text.Json.Nodes;
using NMeter.App.Runner.Extensions;

namespace NMeter.App.Runner.Tests
{
    [TestClass]
    public class JsonNodeExtensionTest
    {
        private readonly string JSON_ARRAY = @"
        [
            {
                ""str"": ""str0"",
                ""int"": 0,
                ""arr"": [0, 1, 2]
            },
            {
                ""str"": ""str1"",
                ""int"": 1,
                ""arr"": []                
            }
        ]
        ";

        private readonly string JSON = @"
        {
            ""str"": ""str1"",
            ""int"": 1,
            ""arr"": []                
        }
        ";

        [TestMethod]
        public void TryGetJsonArray_Positive_Test()
        {
            var jsonNode = JsonNode.Parse(JSON_ARRAY);
            bool result = false;

            result = JsonNodeExtension.TryGetJsonArray(jsonNode, out JsonArray jsonArray);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TryGetJsonArray_Negative_Test()
        {
            var jsonNode = JsonNode.Parse(JSON);
            bool result = true;

            result = JsonNodeExtension.TryGetJsonArray(jsonNode, out JsonArray jsonArray);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TryGetJsonArray_Negative_Null_Test()
        {
            bool result = true;

            result = JsonNodeExtension.TryGetJsonArray(null, out JsonArray jsonArray);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetNodeByPath_Positive_Test()
        {
            var jsonNode = JsonNode.Parse(JSON_ARRAY);
            var path = @"$[0].str";

            var node = JsonNodeExtension.GetNodeByPath(jsonNode, path);

            Assert.IsNotNull(node);
        }

        [TestMethod]
        public void GetNodeByPath_Negative_Test()
        {
            var jsonNode = JsonNode.Parse(JSON_ARRAY);
            var path = @"$[2].str";

            var node = JsonNodeExtension.GetNodeByPath(jsonNode, path);

            Assert.IsNull(node);
        }

        [TestMethod]
        public void GetNodeByPath_Negative_Input_Node_Null_Test()
        {
            var path = @"$[0].str";

            var node = JsonNodeExtension.GetNodeByPath(null, path);

            Assert.IsNull(node);
        }

        [TestMethod]
        public void GetNodeByPath_Negative_Input_Path_Null_Test()
        {
            var jsonNode = JsonNode.Parse(JSON_ARRAY);

            var node = JsonNodeExtension.GetNodeByPath(jsonNode, null);

            Assert.IsNull(node);
        }
    }
}