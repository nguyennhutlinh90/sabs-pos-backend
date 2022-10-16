using CoreLib.Sql;

using System.Data;

namespace sabs_pos_backend_api.Models
{
    [SqlJoin(typeof(stores), "store_id", "id")]
    [SqlTable("receipt_attributes", "id")]
    public class receipt_attributes
    {
        [SqlField("id", SqlDbType.Int, true)]
        public int? id { get; set; }

        [SqlField("uuid", SqlDbType.VarChar, true)]
        public string uuid { get; set; }

        [SqlField("owner_id", SqlDbType.Int, false)]
        public int? owner_id { get; set; }

        [SqlField("store_id", SqlDbType.Int, false)]
        public int? store_id { get; set; }

        [SqlField("email_logo_url", SqlDbType.NVarChar, true)]
        public string email_logo_url { get; set; }

        [SqlField("print_logo_url", SqlDbType.NVarChar, true)]
        public string print_logo_url { get; set; }

        [SqlField("show_customer", SqlDbType.Bit, true)]
        public bool show_customer { get; set; }

        [SqlField("show_note", SqlDbType.Bit, true)]
        public bool show_note { get; set; }

        [SqlField("receipt_header", SqlDbType.NVarChar, true)]
        public string receipt_header { get; set; }

        [SqlField("receipt_footer", SqlDbType.NVarChar, true)]
        public string receipt_footer { get; set; }
    }
}
