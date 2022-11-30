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

        [TestMethod]
        public void AddUpdateJsonNode_Update_String_Positive_Test()
        {
            var expectedJson = @"
            {
                ""str"": ""str_new"",
                ""int"": 1,
                ""arr"": []                
            }
            ";
            var expectedJsonNode = JsonNode.Parse(expectedJson);
            var jsonNode = JsonNode.Parse(JSON);

            JsonNodeExtension.AddUpdateJsonNode(jsonNode, new KeyValuePair<string, object>("str", "str_new"));

            Assert.AreEqual(expectedJsonNode?.ToJsonString(), jsonNode?.ToJsonString());
        }

        [TestMethod]
        public void AddUpdateJsonNode_Update_Int_Positive_Test()
        {
            var expectedJson = @"
            {
                ""str"": ""str1"",
                ""int"": 2,
                ""arr"": []                
            }
            ";
            var expectedJsonNode = JsonNode.Parse(expectedJson);
            var jsonNode = JsonNode.Parse(JSON);

            JsonNodeExtension.AddUpdateJsonNode<int>(jsonNode, new KeyValuePair<string, object>("int", "2"));

            Assert.AreEqual(expectedJsonNode?.ToJsonString(), jsonNode?.ToJsonString());
        }

        [TestMethod]
        public void AddUpdateJsonNode_Update_Arr_Positive_Test()
        {
            var expectedJson = @"
            {
                ""str"": ""str1"",
                ""int"": 1,
                ""arr"": [0, 1]                
            }
            ";
            var expectedJsonNode = JsonNode.Parse(expectedJson);
            var jsonNode = JsonNode.Parse(JSON);

            JsonNodeExtension.AddUpdateJsonNode<int[]>(jsonNode, new KeyValuePair<string, object>("arr", new int[] { 0, 1 }));

            Assert.AreEqual(expectedJsonNode?.ToJsonString(), jsonNode?.ToJsonString());
        }

        [TestMethod]
        public void AddUpdateJsonNode_Add_Str_Positive_Test()
        {
            var expectedJson = @"
            {
                ""str"": ""str1"",
                ""int"": 1,
                ""arr"": [],
                ""new"": ""new""             
            }
            ";
            var expectedJsonNode = JsonNode.Parse(expectedJson);
            var jsonNode = JsonNode.Parse(JSON);

            JsonNodeExtension.AddUpdateJsonNode<string>(jsonNode, new KeyValuePair<string, object>("new", "new"));

            Assert.AreEqual(expectedJsonNode?.ToJsonString(), jsonNode?.ToJsonString());
        }

        [TestMethod]
        public void AddUpdateJsonNode_Update_Multiple_Positive_Test()
        {
            var expectedJson = @"
            [
                {
                    ""str"": ""new_str"",
                    ""int"": 0,
                    ""arr"": [0, 1, 2]
                },
                {
                    ""str"": ""new_str"",
                    ""int"": 1,
                    ""arr"": []                
                }
            ]
            ";
            var expectedJsonNode = JsonNode.Parse(expectedJson);
            var jsonNode = JsonNode.Parse(JSON_ARRAY);

            JsonNodeExtension.AddUpdateJsonNode(jsonNode, new KeyValuePair<string, object>("str", "new_str"));

            Assert.AreEqual(expectedJsonNode?.ToJsonString(), jsonNode?.ToJsonString());
        }

        [TestMethod]
        public void AddUpdateJsonNode_Update_Wrong_Type_Negative_Test()
        {
            var expectedJson = @"
            {
                ""str"": ""str1"",
                ""int"": 1,
                ""arr"": []                
            }
            ";
            var expectedJsonNode = JsonNode.Parse(expectedJson);
            var jsonNode = JsonNode.Parse(JSON);

            JsonNodeExtension.AddUpdateJsonNode<int>(jsonNode, new KeyValuePair<string, object>("int", "qwe"));

            Assert.AreEqual(expectedJsonNode?.ToJsonString(), jsonNode?.ToJsonString());
        }
    }
}