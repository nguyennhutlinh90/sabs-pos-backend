using sabs_pos_backend_api.Models;

using System.Threading.Tasks;

namespace sabs_pos_backend_api
{
    public interface IPOSDeviceService
    {
        Task Create(pos_devices data);

        Task Update(pos_devices data);

        Task Delete(int? id);

        Task<T> Get<T>(string filter, string cursor = "", string include = "");

        Task<ReadResult<T>> Read<T>(string filter, string sort, string cursor = "", string include = "", int skips = 1, int limit = 0);
    }
}
