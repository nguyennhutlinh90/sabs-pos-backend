using CoreLib.Sql;

using sabs_pos_backend_api.Models;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace sabs_pos_backend_api
{
    public class StoreService : ServiceBase, IStoreService
    {
        readonly ISqlHandler _sqlHandler;
        public StoreService(ISqlHandler sqlHandler)
        {
            _sqlHandler = sqlHandler;
        }

        public async Task Create(stores data)
        {
            await ExecuteAsync(async () =>
            {
                var sqlQueryable = _sqlHandler.AsQueryable<stores>();
                return await sqlQueryable.InsertAsync(data);
            });
        }

        public async Task Update(stores data)
        {
            await ExecuteAsync(async () =>
            {
                var sqlQueryable = _sqlHandler.AsQueryable<stores>();
                return await sqlQueryable.UpdateAsync(data, new SqlKey("id", data.id));
            });
        }

        public async Task Delete(int? id)
        {
            await ExecuteAsync(async () =>
            {
                var sqlQueryable = _sqlHandler.AsQueryable<stores>();
                return await sqlQueryable.UpdateAsync(new SqlField("deleted_at", DateTime.Now), new SqlKey("id", id));
            });
        }

        public async Task<T> Get<T>(string filter, string cursor = "", string include = "")
        {
            return await ExecuteAsync(async () =>
            {
                var sqlQueryable = _sqlHandler.AsQueryable<stores>();
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
                var sqlQueryable = _sqlHandler.AsQueryable<stores>();
                sqlQueryable = sqlQueryable.BuildFilter(filter);
                sqlQueryable = sqlQueryable.BuildCursor(cursor);
                sqlQueryable = sqlQueryable.BuildInclude(include);
                sqlQueryable = sqlQueryable.BuildSort(sort);
                return await sqlQueryable.AsDataPaginationAsync<T>(skips, limit);
            });
        }
    }
}
