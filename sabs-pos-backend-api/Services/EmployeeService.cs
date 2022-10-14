using CoreLib.Sql;

using sabs_pos_backend_api.Models;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace sabs_pos_backend_api
{
    public class EmployeeService : ServiceBase, IEmployeeService
    {
        readonly ISqlHandler _sqlHandler;
        public EmployeeService(ISqlHandler sqlHandler)
        {
            _sqlHandler = sqlHandler;
        }

        public async Task Create(employees data)
        {
            await ExecuteAsync(async () =>
            {
                var sqlQueryable = _sqlHandler.AsQueryable<employees>();
                return await sqlQueryable.InsertAsync(data);
            });
        }

        public async Task Update(employees data)
        {
            await ExecuteAsync(async () =>
            {
                var sqlQueryable = _sqlHandler.AsQueryable<employees>();
                return await sqlQueryable.UpdateAsync(data, new SqlKey("id", data.id));
            });
        }

        public async Task Delete(int? id)
        {
            await ExecuteAsync(async () =>
            {
                var sqlQueryable = _sqlHandler.AsQueryable<employees>();
                return await sqlQueryable.UpdateAsync(new SqlField("deleted_at", DateTime.Now), new SqlKey("id", id));
            });
        }

        public async Task<T> Get<T>(string filter, string cursor = "", string include = "")
        {
            return await ExecuteAsync(async () =>
            {
                var sqlQueryable = _sqlHandler.AsQueryable<employees>();
                sqlQueryable = sqlQueryable.BuildFilter(filter);
                sqlQueryable = sqlQueryable.BuildCursor(cursor);
                sqlQueryable = sqlQueryable.BuildInclude(include);
                return await sqlQueryable.AsDataAsync<T>();
            });
        }

        public async Task<ReadResult<T>> Read<T>(string filter, string sort, string cursor = "", string include = "", int skips = 1, int limit = 0)
        {
            return await ExecuteReadAsync(async () =>
            {
                var sqlQueryable = _sqlHandler.AsQueryable<employees>();
                sqlQueryable = sqlQueryable.BuildFilter(filter);
                sqlQueryable = sqlQueryable.BuildCursor(cursor);
                sqlQueryable = sqlQueryable.BuildInclude(include);
                sqlQueryable = sqlQueryable.BuildSort(sort);
                return await sqlQueryable.AsDataPaginationAsync<T>(skips, limit);
            });
        }
    }
}
