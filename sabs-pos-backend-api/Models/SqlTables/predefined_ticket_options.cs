using CoreLib.Sql;

using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlJoin(typeof(predefined_tickets), "predefined_id", "id")]
    [SqlTable("predefined_ticket_options", "id")]
    public class predefined_ticket_options
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("predefined_id", SqlDbType.Int, false)]
        public int? predefined_id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, false)]
        public string uuid { get; set; }

        [SqlField("name", SqlDbType.NVarChar, false)]
        public string name { get; set; }

        [SqlField("sequence", SqlDbType.Int, false)]
        public int? sequence { get; set; }
    }
}
