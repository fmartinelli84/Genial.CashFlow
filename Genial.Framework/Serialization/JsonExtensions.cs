using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genial.Framework.Serialization
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this T value, bool enumAsString = false)
        {
            return JsonConvert.SerializeObject(value, GetJsonSerializerSettings(enumAsString));
        }


        public static T? FromJson<T>(this string json, bool enumAsString = false)
        {
            return JsonConvert.DeserializeObject<T>(json, GetJsonSerializerSettings(enumAsString));
        }
        public static object? FromJson(this string json, Type type, bool enumAsString = false)
        {
            return JsonConvert.DeserializeObject(json, type, GetJsonSerializerSettings(enumAsString));
        }

        public static object? FromJson<T>(this JToken json, Type type, bool enumAsString = false)
        {
            return json.ToObject<T>(JsonSerializer.Create(GetJsonSerializerSettings(enumAsString)));
        }
        public static object? FromJson(this JToken json, Type type, bool enumAsString = false)
        {
            return json.ToObject(type, JsonSerializer.Create(GetJsonSerializerSettings(enumAsString)));
        }


        public static JsonSerializerSettings GetJsonSerializerSettings(bool enumAsString = false)
        {
            var contractResolver = new CamelCasePropertyNamesContractResolver();
            contractResolver.NamingStrategy!.OverrideSpecifiedNames = false;

            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = contractResolver
            };

            if (enumAsString)
            {
                settings.Converters.Add(new StringEnumConverter(new DefaultNamingStrategy()));
            }

            return settings;
        }
    }
}
