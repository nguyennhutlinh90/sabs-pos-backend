using CoreLib.Sql;

using System;
using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlJoin(typeof(roles), "role_id", "id")]
    [SqlJoin(typeof(owners), "owner_id", "id")]
    [SqlTable("accounts", "id")]
    public class accounts
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, false)]
        public string uuid { get; set; }

        [SqlField("role_id", SqlDbType.Int, false)]
        public int? role_id { get; set; }

        [SqlField("name", SqlDbType.NVarChar, false)]
        public string name { get; set; }

        [SqlField("type", SqlDbType.VarChar, false)]
        public string type { get; set; }

        [SqlField("owner_id", SqlDbType.Int, true)]
        public int? owner_id { get; set; }

        [SqlField("email", SqlDbType.VarChar, false)]
        public string email { get; set; }

        [SqlField("phone_number", SqlDbType.VarChar, true)]
        public string phone_number { get; set; }

        [SqlField("country", SqlDbType.VarChar, true)]
        public string country { get; set; }

        [SqlField("timezone", SqlDbType.VarChar, true)]
        public string timezone { get; set; }

        [SqlField("salt_key", SqlDbType.VarChar, false)]
        public string salt_key { get; set; }

        [SqlField("password_hash", SqlDbType.VarChar, false)]
        public string password_hash { get; set; }

        [SqlField("currency", SqlDbType.NChar, true)]
        public string currency { get; set; }

        [SqlField("created_at", SqlDbType.DateTime, true)]
        public DateTime? created_at { get; set; }

        [SqlField("updated_at", SqlDbType.DateTime, true)]
        public DateTime? updated_at { get; set; }

        [SqlField("deleted_at", SqlDbType.DateTime, true)]
        public DateTime? deleted_at { get; set; }

        [SqlField("status", SqlDbType.VarChar, false)]
        public string status { get; set; }
    }
}
