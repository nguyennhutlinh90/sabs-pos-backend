using CoreLib.Sql;

using System;
using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlJoin(typeof(pos_devices), "pos_device_id", "id")]
    [SqlJoin(typeof(stores), "store_id", "id")]
    [SqlTable("shifts", "id")]
    public class shifts
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, true)]
        public string uuid { get; set; }

        [SqlField("owner_id", SqlDbType.Int, true)]
        public int? owner_id { get; set; }

        [SqlField("store_id", SqlDbType.Int, false)]
        public int? store_id { get; set; }

        [SqlField("pos_device_id", SqlDbType.Int, false)]
        public int? pos_device_id { get; set; }

        [SqlField("opened_at", SqlDbType.DateTime, true)]
        public DateTime? opened_at { get; set; }

        [SqlField("closed_at", SqlDbType.DateTime, true)]
        public DateTime? closed_at { get; set; }

        [SqlField("opened_by", SqlDbType.DateTime, true)]
        public DateTime? opened_by { get; set; }

        [SqlField("closed_by", SqlDbType.DateTime, true)]
        public DateTime? closed_by { get; set; }

        [SqlField("starting_cash", SqlDbType.Float, true)]
        public double? starting_cash { get; set; }

        [SqlField("cash_payment", SqlDbType.Float, true)]
        public double? cash_payment { get; set; }

        [SqlField("cash_refund", SqlDbType.Float, true)]
        public double? cash_refund { get; set; }

        [SqlField("paid_in", SqlDbType.Float, true)]
        public double? paid_in { get; set; }

        [SqlField("paid_out", SqlDbType.Float, true)]
        public double? paid_out { get; set; }

        [SqlField("expected_cash", SqlDbType.Float, true)]
        public double? expected_cash { get; set; }

        [SqlField("actual_cash", SqlDbType.Float, true)]
        public double? actual_cash { get; set; }

        [SqlField("gross_sales", SqlDbType.Float, true)]
        public double? gross_sales { get; set; }

        [SqlField("refunds", SqlDbType.Float, true)]
        public double? refunds { get; set; }

        [SqlField("discounts", SqlDbType.Float, true)]
        public double? discounts { get; set; }

        [SqlField("net_sales", SqlDbType.Float, true)]
        public double? net_sales { get; set; }

        [SqlField("tip", SqlDbType.Float, true)]
        public double? tip { get; set; }

        [SqlField("surcharge", SqlDbType.Float, true)]
        public double? surcharge { get; set; }

        [SqlField("taxes", SqlDbType.NVarChar, true)]
        public string taxes { get; set; }

        [SqlField("payments", SqlDbType.NVarChar, true)]
        public string payments { get; set; }

        [SqlField("cash_movements", SqlDbType.NVarChar, true)]
        public string cash_movements { get; set; }
    }
}
