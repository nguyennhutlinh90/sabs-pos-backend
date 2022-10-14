using CoreLib.Sql;

using Microsoft.Data.SqlClient;

using sabs_pos_backend_api.Models;

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace sabs_pos_backend_api
{
    public class AccountService : ServiceBase, IAccountService
    {
        readonly ISqlHandler _sqlHandler;
        public AccountService(ISqlHandler sqlHandler)
        {
            _sqlHandler = sqlHandler;
        }

        public async Task Create(accounts data)
        {
            await ExecuteAsync(async () =>
            {
                var sqlQueryable = _sqlHandler.AsQueryable<accounts>();
                return await sqlQueryable.InsertAsync(data);
            });
        }

        public async Task CreateOwner(accounts data)
        {
            await ExecuteAsync(async () =>
            {
                var sqlParameters = new List<SqlParameter> {
                    "uuid".CreateParameterIn(data.uuid, SqlDbType.VarChar),
                    "role_id".CreateParameterIn(data.role_id, SqlDbType.Int),
                    "name".CreateParameterIn(data.name, SqlDbType.VarChar),
                    "type".CreateParameterIn(data.type, SqlDbType.VarChar),
                    "email".CreateParameterIn(data.email, SqlDbType.VarChar),
                    "phone_number".CreateParameterIn(data.phone_number, SqlDbType.VarChar),
                    "country".CreateParameterIn(data.country, SqlDbType.VarChar),
                    "timezone".CreateParameterIn(data.timezone, SqlDbType.VarChar),
                    "salt_key".CreateParameterIn(data.salt_key, SqlDbType.VarChar),
                    "password_hash".CreateParameterIn(data.password_hash, SqlDbType.VarChar),
                    "currency".CreateParameterIn(data.currency, SqlDbType.VarChar),
                    "created_at".CreateParameterIn(data.created_at, SqlDbType.DateTime),
                    "status".CreateParameterIn(data.status, SqlDbType.VarChar),
                    "error_message".CreateParameterOut(int.MaxValue, SqlDbType.VarChar)
                };
                return await _sqlHandler.ExecuteStoreAsync("sp_create_owner", sqlParameters);
            });
        }

        public async Task<T> Get<T>(string filter, string cursor = "", string include = "")
        {
            return await ExecuteAsync(async () =>
            {
                var sqlQueryable = _sqlHandler.AsQueryable<accounts>();
                sqlQueryable = sqlQueryable.BuildFilter(filter);
                sqlQueryable = sqlQueryable.BuildCursor(cursor);
                sqlQueryable = sqlQueryable.BuildInclude(include);
                return await sqlQueryable.AsDataAsync<T>();
            });
        }
    }
}
