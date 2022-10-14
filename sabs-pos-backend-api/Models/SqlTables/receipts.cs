using CoreLib.Sql;

using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlJoin(typeof(pos_devices), "pos_device_id", "id")]
    [SqlJoin(typeof(stores), "store_id", "id")]
    [SqlTable("receipts", "id")]
    public class receipts
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, false)]
        public string uuid { get; set; }

        [SqlField("owner_id", SqlDbType.Int, false)]
        public int? owner_id { get; set; }

        [SqlField("store_id", SqlDbType.Int, true)]
        public int? store_id { get; set; }

        [SqlField("pos_device_id", SqlDbType.Int, true)]
        public int? pos_device_id { get; set; }

        [SqlField("employee_id", SqlDbType.Int, true)]
        public int? employee_id { get; set; }

        [SqlField("customer_id", SqlDbType.Int, true)]
        public int? customer_id { get; set; }

        [SqlField("note", SqlDbType.NVarChar, true)]
        public string note { get; set; }

        [SqlField("type", SqlDbType.NChar, false)]
        public string type { get; set; }

        [SqlField("fefund_for", SqlDbType.Int, false)]
        public int? fefund_for { get; set; }

        [SqlField("order_no", SqlDbType.NVarChar, true)]
        public string order_no { get; set; }

        [SqlField("source", SqlDbType.NVarChar, true)]
        public string source { get; set; }

        [SqlField("total_money", SqlDbType.Float, true)]
        public double? total_money { get; set; }

        [SqlField("total_tax", SqlDbType.Float, true)]
        public double? total_tax { get; set; }

        [SqlField("points_earned", SqlDbType.Float, true)]
        public double? points_earned { get; set; }

        [SqlField("points_deducted", SqlDbType.Float, true)]
        public double? points_deducted { get; set; }

        [SqlField("points_balance", SqlDbType.Float, true)]
        public double? points_balance { get; set; }

        [SqlField("total_discount", SqlDbType.Float, true)]
        public double? total_discount { get; set; }

        [SqlField("total_discounts", SqlDbType.NVarChar, true)]
        public string total_discounts { get; set; }

        [SqlField("total_taxes", SqlDbType.NVarChar, true)]
        public string total_taxes { get; set; }

        [SqlField("tip", SqlDbType.Float, true)]
        public double? tip { get; set; }

        [SqlField("surchage", SqlDbType.Float, true)]
        public double? surchage { get; set; }

        [SqlField("payments", SqlDbType.NVarChar, true)]
        public string payments { get; set; }
    }
}
