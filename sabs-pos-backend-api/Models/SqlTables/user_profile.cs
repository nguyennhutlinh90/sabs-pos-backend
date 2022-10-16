using CoreLib.Sql;

using System;
using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlTable("user_profile", "id")]
    public class user_profile
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, false)]
        public string uuid { get; set; }

        [SqlField("owner_id", SqlDbType.Int, false)]
        public int? owner_id { get; set; }

        [SqlField("account_id", SqlDbType.Int, false)]
        public int? account_id { get; set; }

        [SqlField("settings", SqlDbType.NVarChar, true)]
        public string settings { get; set; }

        [SqlField("billing_info", SqlDbType.NVarChar, true)]
        public string billing_info { get; set; }

        [SqlField("plan_details", SqlDbType.NVarChar, true)]
        public string plan_details { get; set; }

        [SqlField("created_at", SqlDbType.DateTime, true)]
        public DateTime? created_at { get; set; }

        [SqlField("updated_at", SqlDbType.DateTime, true)]
        public DateTime? updated_at { get; set; }
    }
}
