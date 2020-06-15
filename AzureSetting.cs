using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureSettingsConverter
{
    public class AzureSetting
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
        [JsonProperty(PropertyName = "slotSetting")]
        public bool SlotSetting { get; set; } = false;
    }
}
