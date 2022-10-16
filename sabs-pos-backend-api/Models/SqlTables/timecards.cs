using CoreLib.Sql;

using System;
using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlJoin(typeof(stores), "store_id", "id")]
    [SqlTable("timecards", "id")]
    public class timecards
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, true)]
        public string uuid { get; set; }

        [SqlField("merchant_id", SqlDbType.Int, false)]
        public int? merchant_id { get; set; }

        [SqlField("store_id", SqlDbType.Int, false)]
        public int? store_id { get; set; }

        [SqlField("owner_id", SqlDbType.Int, false)]
        public int? owner_id { get; set; }

        [SqlField("clock_in", SqlDbType.DateTime, true)]
        public DateTime? clock_in { get; set; }

        [SqlField("clock_out", SqlDbType.DateTime, true)]
        public DateTime? clock_out { get; set; }
    }
}
