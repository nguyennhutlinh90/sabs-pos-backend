using CoreLib.Sql;

using System;
using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlTable("customers", "id")]
    public class customers
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, false)]
        public string uuid { get; set; }

        [SqlField("owner_id", SqlDbType.Int, false)]
        public int? owner_id { get; set; }

        [SqlField("name", SqlDbType.NVarChar, false)]
        public string name { get; set; }

        [SqlField("email", SqlDbType.NVarChar, true)]
        public string email { get; set; }

        [SqlField("phone_number", SqlDbType.NChar, true)]
        public string phone_number { get; set; }

        [SqlField("address", SqlDbType.NVarChar, true)]
        public string address { get; set; }

        [SqlField("city", SqlDbType.NVarChar, true)]
        public string city { get; set; }

        [SqlField("postal_code", SqlDbType.VarChar, true)]
        public string postal_code { get; set; }

        [SqlField("country_code", SqlDbType.VarChar, true)]
        public string country_code { get; set; }

        [SqlField("customer_code", SqlDbType.VarChar, true)]
        public string customer_code { get; set; }

        [SqlField("note", SqlDbType.NVarChar, true)]
        public string note { get; set; }

        [SqlField("first_visit", SqlDbType.DateTime, true)]
        public DateTime? first_visit { get; set; }

        [SqlField("last_visit", SqlDbType.DateTime, true)]
        public DateTime? last_visit { get; set; }

        [SqlField("total_visits", SqlDbType.Int, true)]
        public int? total_visits { get; set; }

        [SqlField("total_spent", SqlDbType.Float, true)]
        public double? total_spent { get; set; }

        [SqlField("total_points", SqlDbType.Int, true)]
        public int? total_points { get; set; }

        [SqlField("created_at", SqlDbType.DateTime, true)]
        public DateTime? created_at { get; set; }

        [SqlField("updated_at", SqlDbType.DateTime, true)]
        public DateTime? updated_at { get; set; }

        [SqlField("deleted_at", SqlDbType.DateTime, true)]
        public DateTime? deleted_at { get; set; }

        [SqlField("permanent_deletion_at", SqlDbType.DateTime, true)]
        public DateTime? permanent_deletion_at { get; set; }
    }
}
