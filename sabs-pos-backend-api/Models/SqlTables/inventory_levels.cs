using CoreLib.Sql;

using System;
using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlJoin(typeof(item_variants), "variant_id", "id")]
    [SqlTable("inventory_levels", "id")]
    public class inventory_levels
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, true)]
        public string uuid { get; set; }

        [SqlField("owner_id", SqlDbType.Int, true)]
        public int? owner_id { get; set; }

        [SqlField("variant_id", SqlDbType.Int, false)]
        public int? variant_id { get; set; }

        [SqlField("store_id", SqlDbType.Int, false)]
        public int? store_id { get; set; }

        [SqlField("in_stock", SqlDbType.Float, false)]
        public double? in_stock { get; set; }

        [SqlField("updated_at", SqlDbType.DateTime, true)]
        public DateTime? updated_at { get; set; }

        [SqlField("created_at", SqlDbType.DateTime, true)]
        public DateTime? created_at { get; set; }
    }
}
