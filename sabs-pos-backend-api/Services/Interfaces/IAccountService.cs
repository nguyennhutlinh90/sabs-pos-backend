using sabs_pos_backend_api.Models;

using System.Threading.Tasks;

namespace sabs_pos_backend_api
{
    public interface IAccountService
    {
        Task Create(accounts data);

        Task CreateOwner(accounts data);

        Task<T> Get<T>(string filter, string cursor = "", string include = "");
    }
}
