using Newtonsoft.Json;

using System;

namespace sabs_pos_backend_api.Models
{
    public class AccountRes
    {
        [JsonIgnore]
        public int? id { get; set; }

        [JsonProperty("id")]
        public string uuid { get; set; }

        [JsonProperty("role_id")]
        public string role_uuid { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        [JsonIgnore]
        public int? owner_id { get; set; }

        [JsonProperty("owner_id")]
        public string owner_uuid { get; set; }

        public string email { get; set; }

        public string phone_number { get; set; }

        public string country { get; set; }

        public string timezone { get; set; }

        [JsonIgnore]
        public string salt_key { get; set; }

        [JsonIgnore]
        public string password_hash { get; set; }

        public string currency { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public DateTime? deleted_at { get; set; }

        public string status { get; set; }
    }
}
