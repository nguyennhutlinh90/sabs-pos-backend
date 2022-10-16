using CoreLib.Sql;

using System;
using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlJoin(typeof(items), "item_id", "id")]
    [SqlTable("item_variants", "id")]
    public class item_variants
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, false)]
        public string uuid { get; set; }

        [SqlField("item_id", SqlDbType.Int, false)]
        public int? item_id { get; set; }

        [SqlField("sku", SqlDbType.NVarChar, true)]
        public string sku { get; set; }

        [SqlField("reference_variant_id", SqlDbType.Int, true)]
        public int? reference_variant_id { get; set; }

        [SqlField("option_values", SqlDbType.NVarChar, true)]
        public string option_values { get; set; }

        [SqlField("barcode", SqlDbType.VarChar, true)]
        public string barcode { get; set; }

        [SqlField("cost", SqlDbType.Float, true)]
        public double? cost { get; set; }

        [SqlField("purchase_cost", SqlDbType.Float, true)]
        public double? purchase_cost { get; set; }

        [SqlField("default_pricing_type", SqlDbType.NChar, true)]
        public string default_pricing_type { get; set; }

        [SqlField("default_price", SqlDbType.Float, true)]
        public double? default_price { get; set; }

        [SqlField("stores", SqlDbType.VarChar, true)]
        public string stores { get; set; }

        [SqlField("created_at", SqlDbType.DateTime, true)]
        public DateTime? created_at { get; set; }

        [SqlField("updated_at", SqlDbType.DateTime, true)]
        public DateTime? updated_at { get; set; }

        [SqlField("deleted_at", SqlDbType.DateTime, true)]
        public DateTime? deleted_at { get; set; }
    }
}
