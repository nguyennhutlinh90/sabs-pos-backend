using System.Collections.Generic;

namespace sabs_pos_backend_api
{
    public class ReadResult<TData>
    {
        public ReadResult(List<TData> _datas, long _total)
        {
            datas = _datas;
            total = _total;
        }

        public List<TData> datas { get; set; }

        public long total { get; set; }
    }

    public class ReadResult : ReadResult<object>
    {
        public ReadResult(List<object> _datas, long _total) : base(_datas, _total)
        {

        }
    }
}
