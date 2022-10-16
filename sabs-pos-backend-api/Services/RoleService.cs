using CoreLib.Sql;

using sabs_pos_backend_api.Models;

using System.Linq;
using System.Threading.Tasks;

namespace sabs_pos_backend_api
{
    public class RoleService : ServiceBase, IRoleService
    {
        readonly ISqlHandler _sqlHandler;
        public RoleService(ISqlHandler sqlHandler)
        {
            _sqlHandler = sqlHandler;
        }

        public async Task Create(roles data)
        {
            await ExecuteAsync(async () =>
            {
                var sqlQueryable = _sqlHandler.AsQueryable<roles>();
                return await sqlQueryable.InsertAsync(data);
            });
        }

        public async Task<T> Get<T>(string filter, string cursor = "", string include = "")
        {
            return await ExecuteAsync(async () =>
            {
                var sqlQueryable = _sqlHandler.AsQueryable<roles>();
                sqlQueryable = sqlQueryable.BuildFilter(filter);
                sqlQueryable = sqlQueryable.BuildCursor(cursor);
                sqlQueryable = sqlQueryable.BuildInclude(include);
                return await sqlQueryable.AsDataAsync<T>();
            });
        }
    }
}
