using CoreLib.Sql;

using System;
using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlJoin(typeof(timecards), "timecard_id", "id")]
    [SqlTable("timecard_history", "id")]
    public class timecard_history
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, true)]
        public string uuid { get; set; }

        [SqlField("timecard_id", SqlDbType.Int, true)]
        public int? timecard_id { get; set; }

        [SqlField("type", SqlDbType.VarChar, false)]
        public string type { get; set; }

        [SqlField("clock_in", SqlDbType.DateTime, true)]
        public DateTime? clock_in { get; set; }

        [SqlField("clock_out", SqlDbType.DateTime, true)]
        public DateTime? clock_out { get; set; }

        [SqlField("edited_at", SqlDbType.DateTime, true)]
        public DateTime? edited_at { get; set; }
    }
}
