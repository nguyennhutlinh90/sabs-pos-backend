using CoreLib.Sql;

using System;
using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlTable("taxes", "id")]
    public class taxes
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, false)]
        public string uuid { get; set; }

        [SqlField("owner_id", SqlDbType.Int, true)]
        public int? owner_id { get; set; }

        [SqlField("name", SqlDbType.NVarChar, false)]
        public string name { get; set; }

        [SqlField("type", SqlDbType.NChar, false)]
        public string type { get; set; }

        [SqlField("rate", SqlDbType.Float, true)]
        public double? rate { get; set; }

        [SqlField("stores", SqlDbType.NVarChar, true)]
        public string stores { get; set; }

        [SqlField("created_at", SqlDbType.DateTime, true)]
        public DateTime? created_at { get; set; }

        [SqlField("updated_at", SqlDbType.DateTime, true)]
        public DateTime? updated_at { get; set; }

        [SqlField("deleted_at", SqlDbType.DateTime, true)]
        public DateTime? deleted_at { get; set; }
    }
}
