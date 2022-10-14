using CoreLib.Sql;

using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlJoin(typeof(receipts), "receipt_number", "id")]
    [SqlTable("receipt_details", "id")]
    public class receipt_details
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, false)]
        public string uuid { get; set; }

        [SqlField("receipt_number", SqlDbType.Int, false)]
        public int? receipt_number { get; set; }

        [SqlField("item_id", SqlDbType.Int, true)]
        public int? item_id { get; set; }

        [SqlField("variant_id", SqlDbType.Int, true)]
        public int? variant_id { get; set; }

        [SqlField("quantity", SqlDbType.Float, true)]
        public double? quantity { get; set; }

        [SqlField("line_note", SqlDbType.NVarChar, true)]
        public string line_note { get; set; }

        [SqlField("total_money", SqlDbType.Float, true)]
        public double? total_money { get; set; }

        [SqlField("total_discount", SqlDbType.Float, true)]
        public double? total_discount { get; set; }

        [SqlField("line_taxes", SqlDbType.NVarChar, true)]
        public string line_taxes { get; set; }

        [SqlField("line_discounts", SqlDbType.NVarChar, true)]
        public string line_discounts { get; set; }

        [SqlField("line_modifers", SqlDbType.NVarChar, true)]
        public string line_modifers { get; set; }
    }
}
