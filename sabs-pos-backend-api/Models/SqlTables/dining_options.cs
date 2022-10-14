using CoreLib.Sql;

using System;
using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlTable("dining_options", "id")]
    public class dining_options
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("owner_id", SqlDbType.Int, false)]
        public int? owner_id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, false)]
        public string uuid { get; set; }

        [SqlField("name", SqlDbType.NVarChar, false)]
        public string name { get; set; }

        [SqlField("type", SqlDbType.NChar, false)]
        public string type { get; set; }

        [SqlField("stores", SqlDbType.NVarChar, true)]
        public string stores { get; set; }

        [SqlField("created_at", SqlDbType.DateTime, true)]
        public DateTime? created_at { get; set; }

        [SqlField("edited_at", SqlDbType.DateTime, true)]
        public DateTime? edited_at { get; set; }

        [SqlField("deleted_at", SqlDbType.DateTime, true)]
        public DateTime? deleted_at { get; set; }
    }
}
