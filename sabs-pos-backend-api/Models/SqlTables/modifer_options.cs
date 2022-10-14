using CoreLib.Sql;

using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlJoin(typeof(modifers), "modifer_id", "id")]
    [SqlTable("modifer_options", "id")]
    public class modifer_options
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, false)]
        public string uuid { get; set; }

        [SqlField("modifer_id", SqlDbType.Int, true)]
        public int? modifer_id { get; set; }

        [SqlField("name", SqlDbType.NVarChar, false)]
        public string name { get; set; }

        [SqlField("price", SqlDbType.Float, false)]
        public double? price { get; set; }

        [SqlField("sequence", SqlDbType.Int, false)]
        public int? sequence { get; set; }
    }
}
