using CoreLib.Sql;

using System;
using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlTable("suppliers", "id")]
    public class suppliers
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("uuid", SqlDbType.Int, true)]
        public int? uuid { get; set; }

        [SqlField("name", SqlDbType.NVarChar, false)]
        public string name { get; set; }

        [SqlField("owner_id", SqlDbType.Int, true)]
        public int? owner_id { get; set; }

        [SqlField("phone_number", SqlDbType.NChar, true)]
        public string phone_number { get; set; }

        [SqlField("contact_name", SqlDbType.NVarChar, true)]
        public string contact_name { get; set; }

        [SqlField("website", SqlDbType.NVarChar, true)]
        public string website { get; set; }

        [SqlField("address_1", SqlDbType.NVarChar, true)]
        public string address_1 { get; set; }

        [SqlField("address_2", SqlDbType.NVarChar, true)]
        public string address_2 { get; set; }

        [SqlField("city", SqlDbType.NVarChar, true)]
        public string city { get; set; }

        [SqlField("note", SqlDbType.NVarChar, true)]
        public string note { get; set; }

        [SqlField("created_at", SqlDbType.DateTime, true)]
        public DateTime? created_at { get; set; }

        [SqlField("updated_at", SqlDbType.DateTime, true)]
        public DateTime? updated_at { get; set; }

        [SqlField("deleted_at", SqlDbType.DateTime, true)]
        public DateTime? deleted_at { get; set; }
    }
}
