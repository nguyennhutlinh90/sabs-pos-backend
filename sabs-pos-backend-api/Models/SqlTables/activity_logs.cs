using CoreLib.Sql;

using System;
using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlTable("activity_logs", "id")]
    public class activity_logs
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("action_type", SqlDbType.VarChar, false)]
        public string action_type { get; set; }

        [SqlField("detail", SqlDbType.NVarChar, true)]
        public string detail { get; set; }

        [SqlField("created_by", SqlDbType.Int, false)]
        public int? created_by { get; set; }

        [SqlField("created_at", SqlDbType.DateTime, true)]
        public DateTime? created_at { get; set; }
    }
}
