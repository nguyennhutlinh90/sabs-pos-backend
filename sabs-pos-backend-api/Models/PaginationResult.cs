using System.Collections.Generic;

namespace sabs_pos_backend_api
{
    public class PaginationResult<TData>
    {
        public PaginationResult(List<TData> _datas, long _total, int _index, int _size)
        {
            datas = _datas;
            total = _total;
            index = _index;
            size = _size;
        }

        public List<TData> datas { get; set; }

        public long total { get; set; }

        public int index { get; set; }

        public int size { get; set; }
    }

    public class PaginationResult : PaginationResult<object>
    {
        public PaginationResult(List<object> _datas, long _total, int _index, int _size) : base(_datas, _total, _index, _size)
        {

        }
    }
}
