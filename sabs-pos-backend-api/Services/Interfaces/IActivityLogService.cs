using sabs_pos_backend_api.Models;

using System.Threading.Tasks;

namespace sabs_pos_backend_api
{
    public interface IActivityLogService
    {
        Task Create(activity_logs data);
    }
}
