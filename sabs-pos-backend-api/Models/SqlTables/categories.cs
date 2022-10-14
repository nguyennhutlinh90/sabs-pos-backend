using CoreLib.Sql;

using System;
using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlTable("categories", "id")]
    public class categories
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, false)]
        public string uuid { get; set; }

        [SqlField("name", SqlDbType.NVarChar, false)]
        public string name { get; set; }

        [SqlField("owner_id", SqlDbType.Int, false)]
        public int? owner_id { get; set; }

        [SqlField("color", SqlDbType.VarChar, false)]
        public string color { get; set; }

        [SqlField("created_at", SqlDbType.DateTime, true)]
        public DateTime? created_at { get; set; }

        [SqlField("created_by", SqlDbType.VarChar, true)]
        public string created_by { get; set; }

        [SqlField("deleted_at", SqlDbType.DateTime, true)]
        public DateTime? deleted_at { get; set; }

        [SqlField("deleted_by", SqlDbType.VarChar, true)]
        public string deleted_by { get; set; }
    }
}
