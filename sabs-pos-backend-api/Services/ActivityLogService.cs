using CoreLib.Sql;

using sabs_pos_backend_api.Models;

using System.Threading.Tasks;

namespace sabs_pos_backend_api
{
    public class ActivityLogService : ServiceBase, IActivityLogService
    {
        readonly ISqlHandler _sqlHandler;
        public ActivityLogService(ISqlHandler sqlHandler)
        {
            _sqlHandler = sqlHandler;
        }

        public async Task Create(activity_logs data)
        {
            await ExecuteAsync(async () =>
            {
                var sqlQueryable = _sqlHandler.AsQueryable<activity_logs>();
                return await sqlQueryable.InsertAsync(data);
            });
        }
    }
}
