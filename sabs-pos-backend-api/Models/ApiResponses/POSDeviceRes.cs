using Newtonsoft.Json;

using System;

namespace sabs_pos_backend_api.Models
{
    public class POSDeviceRes
    {
        [JsonProperty("id")]
        public string uuid { get; set; }

        public string name { get; set; }

        [JsonProperty("store_id")]
        public string store_uuid { get; set; }

        public bool activated { get; set; }

        public DateTime? deleted_at { get; set; }
    }
}
