using CoreLib.Sql;

using System;
using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlJoin(typeof(roles), "role_id", "id")]
    [SqlTable("employees", "id")]
    public class employees
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, false)]
        public string uuid { get; set; }

        [SqlField("name", SqlDbType.NVarChar, false)]
        public string name { get; set; }

        [SqlField("email", SqlDbType.NVarChar, true)]
        public string email { get; set; }

        [SqlField("phone_number", SqlDbType.NChar, true)]
        public string phone_number { get; set; }

        [SqlField("stores", SqlDbType.VarChar, true)]
        public string stores { get; set; }

        [SqlField("is_owner", SqlDbType.Bit, true)]
        public bool is_owner { get; set; }

        [SqlField("owner_id", SqlDbType.Int, true)]
        public int? owner_id { get; set; }

        [SqlField("role_id", SqlDbType.Int, false)]
        public int? role_id { get; set; }

        [SqlField("status", SqlDbType.NChar, true)]
        public string status { get; set; }

        [SqlField("pin_code", SqlDbType.NChar, true)]
        public string pin_code { get; set; }

        [SqlField("created_at", SqlDbType.DateTime, true)]
        public DateTime? created_at { get; set; }

        [SqlField("updated_at", SqlDbType.DateTime, true)]
        public DateTime? updated_at { get; set; }

        [SqlField("deleted_at", SqlDbType.DateTime, true)]
        public DateTime? deleted_at { get; set; }

        [SqlField("created_by", SqlDbType.VarChar, true)]
        public string created_by { get; set; }

        [SqlField("updated_by", SqlDbType.VarChar, true)]
        public string updated_by { get; set; }

        [SqlField("deleted_by", SqlDbType.VarChar, true)]
        public string deleted_by { get; set; }
    }
}
