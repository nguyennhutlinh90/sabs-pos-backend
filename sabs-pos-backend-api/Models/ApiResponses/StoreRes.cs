using Newtonsoft.Json;

using System;

namespace sabs_pos_backend_api.Models
{
    public class StoreRes
    {
        [JsonProperty("id")]
        public string uuid { get; set; }

        [JsonProperty("owner_id")]
        public string owner_uuid { get; set; }

        public string name { get; set; }

        public string address { get; set; }

        public string phone_number { get; set; }

        public string description { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public DateTime? deleted_at { get; set; }
    }
}
