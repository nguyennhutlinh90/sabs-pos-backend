using CoreLib.Sql;

using System;
using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlJoin(typeof(categories), "category_id", "id")]
    [SqlTable("items", "id")]
    public class items
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, false)]
        public string uuid { get; set; }

        [SqlField("owner_id", SqlDbType.Int, true)]
        public int? owner_id { get; set; }

        [SqlField("name", SqlDbType.NVarChar, false)]
        public string name { get; set; }

        [SqlField("description", SqlDbType.NVarChar, true)]
        public string description { get; set; }

        [SqlField("reference_id", SqlDbType.VarChar, true)]
        public string reference_id { get; set; }

        [SqlField("category_id", SqlDbType.Int, false)]
        public int? category_id { get; set; }

        [SqlField("track_stock", SqlDbType.Bit, false)]
        public bool track_stock { get; set; }

        [SqlField("sold_by_weight", SqlDbType.Bit, false)]
        public bool sold_by_weight { get; set; }

        [SqlField("is_composite", SqlDbType.Bit, false)]
        public bool is_composite { get; set; }

        [SqlField("use_production", SqlDbType.Bit, false)]
        public bool use_production { get; set; }

        [SqlField("components", SqlDbType.NVarChar, true)]
        public string components { get; set; }

        [SqlField("supplier_id", SqlDbType.Int, true)]
        public int? supplier_id { get; set; }

        [SqlField("tax_ids", SqlDbType.VarChar, true)]
        public string tax_ids { get; set; }

        [SqlField("modifers_ids", SqlDbType.VarChar, true)]
        public string modifers_ids { get; set; }

        [SqlField("form", SqlDbType.NChar, true)]
        public string form { get; set; }

        [SqlField("color", SqlDbType.NChar, true)]
        public string color { get; set; }

        [SqlField("image_url", SqlDbType.NVarChar, true)]
        public string image_url { get; set; }

        [SqlField("variant_options", SqlDbType.VarChar, true)]
        public string variant_options { get; set; }

        [SqlField("created_at", SqlDbType.DateTime, true)]
        public DateTime? created_at { get; set; }

        [SqlField("updated_at", SqlDbType.DateTime, true)]
        public DateTime? updated_at { get; set; }

        [SqlField("deleted_at", SqlDbType.DateTime, true)]
        public DateTime? deleted_at { get; set; }
    }
}
