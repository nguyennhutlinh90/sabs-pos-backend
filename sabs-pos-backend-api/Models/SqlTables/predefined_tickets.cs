using CoreLib.Sql;

using System;
using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlJoin(typeof(stores), "store_id", "id")]
    [SqlTable("predefined_tickets", "id")]
    public class predefined_tickets
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, false)]
        public string uuid { get; set; }

        [SqlField("owner_id", SqlDbType.Int, false)]
        public int? owner_id { get; set; }

        [SqlField("store_id", SqlDbType.Int, false)]
        public int? store_id { get; set; }

        [SqlField("name", SqlDbType.NVarChar, true)]
        public string name { get; set; }

        [SqlField("use_predefined", SqlDbType.Bit, false)]
        public bool use_predefined { get; set; }

        [SqlField("created_at", SqlDbType.DateTime, true)]
        public DateTime? created_at { get; set; }

        [SqlField("edited_at", SqlDbType.DateTime, true)]
        public DateTime? edited_at { get; set; }

        [SqlField("deleted_at", SqlDbType.DateTime, true)]
        public DateTime? deleted_at { get; set; }
    }
}
