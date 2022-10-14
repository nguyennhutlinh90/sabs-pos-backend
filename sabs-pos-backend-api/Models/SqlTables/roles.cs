using CoreLib.Sql;

using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlTable("roles", "id")]
    public class roles
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, false)]
        public string uuid { get; set; }

        [SqlField("owner_id", SqlDbType.Int, true)]
        public int? owner_id { get; set; }

        [SqlField("name", SqlDbType.NVarChar, false)]
        public string name { get; set; }

        [SqlField("allow_pos", SqlDbType.Bit, false)]
        public bool allow_pos { get; set; }

        [SqlField("allow_back_office", SqlDbType.Bit, false)]
        public bool allow_back_office { get; set; }

        [SqlField("type", SqlDbType.VarChar, true)]
        public string type { get; set; }

        [SqlField("permissions", SqlDbType.NVarChar, true)]
        public string permissions { get; set; }
    }
}
