using System.Text.Json.Nodes;

namespace NMeter.App.Runner.Extensions
{
    public static class JsonNodeExtension
    {
        public static bool TryGetJsonArray(this JsonNode jsonNode, out JsonArray jsonArray)
        {
            try
            {
                jsonArray = jsonNode.AsArray();
                return true;
            }
            catch (Exception)
            {
                jsonArray = new JsonArray();
                return false;
            }
        }

        public static JsonNode GetNodeByPath(this JsonNode jsonNode, string path)
        {
            if (jsonNode == null)
                return default;

            if (jsonNode.GetPath().Equals(path))
                return jsonNode;

            if (jsonNode.TryGetJsonArray(out JsonArray jsonArray))
            {
                foreach (var childNode in jsonArray)
                {
                    var result = GetNodeByPath(childNode, path);
                    if (result != null)
                        return result;
                }
            }
            else
            {
                var jsonObject = jsonNode as JsonObject;
                if (jsonObject != null)
                {
                    foreach (var obj in jsonObject)
                    {
                        var result = GetNodeByPath(obj.Value, path);
                        if (result != null)
                            return result;
                    }
                }
            }

            return default;
        }

        public static void AddUpdateJsonNode(this JsonNode jsonNode, KeyValuePair<string, object> valuePair)
        {
            jsonNode.AddUpdateJsonNode<string>(valuePair);
        }

        public static void AddUpdateJsonNode<T>(this JsonNode jsonNode, KeyValuePair<string, object> valuePair)
        {
            if (jsonNode is JsonArray)
            {
                foreach (var obj in jsonNode.AsArray())
                    AddUpdateJsonNode<T>(obj, valuePair);
            }
            else
            {
                if (valuePair.Value is T)
                {
                    var val = (T)valuePair.Value;
                    jsonNode[valuePair.Key] = JsonValue.Create(val);
                }
                else
                {
                    try
                    {
                        var val = (T)Convert.ChangeType(valuePair.Value, typeof(T));
                        jsonNode[valuePair.Key] = JsonValue.Create(val);
                    }
                    catch (Exception)
                    {
                        jsonNode[valuePair.Key] = JsonValue.Create(default(T));
                    }
                }
            }
        }
    }
}