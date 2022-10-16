using sabs_pos_backend_api.Models;

using System.Threading.Tasks;

namespace sabs_pos_backend_api
{
    public interface IEmployeeService
    {
        Task Create(employees data);

        Task Update(employees data);

        Task Delete(int? id);

        Task<T> Get<T>(string filter, string cursor = "", string include = "");

        Task<ReadResult<T>> Read<T>(string filter, string sort, string cursor, string include, int skips = 1, int limit = 0);
    }
}
