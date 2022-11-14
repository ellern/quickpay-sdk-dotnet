using System.IO;
using System.Text.Json;

namespace QuickPay.SDK
{
    public static class JSON
    {
        public static JsonSerializerOptions JsonSerializerOptions;

        static JSON()
        {
            JsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            };
        }

        public static string Serialize(object value) => JsonSerializer.Serialize(value, JsonSerializerOptions);

        public static void Serialize(Stream utf8Json, object value) => JsonSerializer.Serialize(utf8Json, value, JsonSerializerOptions);

        public static TValue Deserialize<TValue>(string json) => JsonSerializer.Deserialize<TValue>(json, JsonSerializerOptions);

        public static TValue Deserialize<TValue>(Stream utf8Json) => JsonSerializer.Deserialize<TValue>(utf8Json, JsonSerializerOptions);
    }

}
