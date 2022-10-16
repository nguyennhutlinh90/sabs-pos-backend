using sabs_pos_backend_api.Models;

using System.Threading.Tasks;

namespace sabs_pos_backend_api
{
    public interface IRoleService
    {
        Task Create(roles data);

        Task<T> Get<T>(string filter, string cursor = "", string include = "");
    }
}
