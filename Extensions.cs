using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureSettingsConverter
{
    public static class Extensions
    {
        public static string ToJSON(this object obj)
        {
            return JToken.FromObject(obj).ToString();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        public static IEnumerable<AzureSetting> ToAzureSettings(this IConfiguration config, string baseName = null, bool isEnumerable = false)
        {
            var section = config.GetChildren();
            var result = new List<AzureSetting>();

            foreach(var item in section)
            {
                var hasChilds = (!item.GetChildren().IsNullOrEmpty());
                var hasValue = (item.Value != null);

                if (!hasChilds && hasValue)
                {
                    result.Add(new AzureSetting()
                    {
                        Name = $"{baseName}{(baseName.Contains("__") ? string.Empty : "__")}{item.Key.ToUpperInvariant()}",
                        Value = $"{item.Value}"
                    });
                }
                else
                {
                    result.AddRange(item.ToAzureSettings($"{baseName}{item.Key.ToUpperInvariant()}__"));
                }
            }

            return result;
        }
    }
}
