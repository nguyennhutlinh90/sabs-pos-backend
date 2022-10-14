using CoreLib.Sql;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sabs_pos_backend_api
{
    public class ServiceBase
    {
        protected void Execute(Func<ISqlResult> executeFunc)
        {
            try
            {
                var sqlResult = executeFunc();
                if (!sqlResult.Success)
                    throw new ResponseException(ResponseCode.INTERNAL_SERVER_ERROR, sqlResult.Exception.Message);
            }
            catch (Exception ex)
            {
                throw responseException(ex);
            }
        }

        protected object Execute(Func<ISqlDataResult> executeFunc)
        {
            try
            {
                var sqlResult = executeFunc();
                if (!sqlResult.Success)
                    throw new ResponseException(ResponseCode.INTERNAL_SERVER_ERROR, sqlResult.Exception.Message);
                return sqlResult.Data;
            }
            catch (Exception ex)
            {
                throw responseException(ex);
            }
        }

        protected TData Execute<TData>(Func<ISqlDataResult<TData>> executeFunc)
        {
            try
            {
                var sqlResult = executeFunc();
                if (!sqlResult.Success)
                    throw new ResponseException(ResponseCode.INTERNAL_SERVER_ERROR, sqlResult.Exception.Message);
                return sqlResult.Data;
            }
            catch (Exception ex)
            {
                throw responseException(ex);
            }
        }

        protected List<object> Execute(Func<ISqlDataListResult> executeFunc)
        {
            try
            {
                var sqlResult = executeFunc();
                if (!sqlResult.Success)
                    throw new ResponseException(ResponseCode.INTERNAL_SERVER_ERROR, sqlResult.Exception.Message);
                return sqlResult.Datas;
            }
            catch (Exception ex)
            {
                throw responseException(ex);
            }
        }

        protected List<TData> Execute<TData>(Func<ISqlDataListResult<TData>> executeFunc)
        {
            try
            {
                var sqlResult = executeFunc();
                if (!sqlResult.Success)
                    throw new ResponseException(ResponseCode.INTERNAL_SERVER_ERROR, sqlResult.Exception.Message);
                return sqlResult.Datas;
            }
            catch (Exception ex)
            {
                throw responseException(ex);
            }
        }

        protected ReadResult ExecuteRead(Func<ISqlDataListResult> executeFunc)
        {
            try
            {
                var sqlResult = executeFunc();
                if (!sqlResult.Success)
                    throw new ResponseException(ResponseCode.INTERNAL_SERVER_ERROR, sqlResult.Exception.Message);
                return new ReadResult(sqlResult.Datas, sqlResult.Total);
            }
            catch (Exception ex)
            {
                throw responseException(ex);
            }
        }

        protected ReadResult<TData> ExecuteRead<TData>(Func<ISqlDataListResult<TData>> executeFunc)
        {
            try
            {
                var sqlResult = executeFunc();
                if (!sqlResult.Success)
                    throw new ResponseException(ResponseCode.INTERNAL_SERVER_ERROR, sqlResult.Exception.Message);
                return new ReadResult<TData>(sqlResult.Datas, sqlResult.Total);
            }
            catch (Exception ex)
            {
                throw responseException(ex);
            }
        }

        protected async Task ExecuteAsync(Func<Task<ISqlResult>> executeFuncAsync)
        {
            try
            {
                var sqlResult = await executeFuncAsync();
                if (!sqlResult.Success)
                    throw new ResponseException(ResponseCode.INTERNAL_SERVER_ERROR, sqlResult.Exception.Message);
            }
            catch (Exception ex)
            {
                throw responseException(ex);
            }
        }

        protected async Task<object> ExecuteAsync(Func<Task<ISqlDataResult>> executeFuncAsync)
        {
            try
            {
                var sqlResult = await executeFuncAsync();
                if (!sqlResult.Success)
                    throw new ResponseException(ResponseCode.INTERNAL_SERVER_ERROR, sqlResult.Exception.Message);
                return sqlResult.Data;
            }
            catch (Exception ex)
            {
                throw responseException(ex);
            }
        }

        protected async Task<TData> ExecuteAsync<TData>(Func<Task<ISqlDataResult<TData>>> executeFuncAsync)
        {
            try
            {
                var sqlResult = await executeFuncAsync();
                if (!sqlResult.Success)
                    throw new ResponseException(ResponseCode.INTERNAL_SERVER_ERROR, sqlResult.Exception.Message);
                return sqlResult.Data;
            }
            catch (Exception ex)
            {
                throw responseException(ex);
            }
        }

        protected async Task<List<object>> ExecuteAsync(Func<Task<ISqlDataListResult>> executeFuncAsync)
        {
            try
            {
                var sqlResult = await executeFuncAsync();
                if (!sqlResult.Success)
                    throw new ResponseException(ResponseCode.INTERNAL_SERVER_ERROR, sqlResult.Exception.Message);
                return sqlResult.Datas;
            }
            catch (Exception ex)
            {
                throw responseException(ex);
            }
        }

        protected async Task<List<TData>> ExecuteAsync<TData>(Func<Task<ISqlDataListResult<TData>>> executeFuncAsync)
        {
            try
            {
                var sqlResult = await executeFuncAsync();
                if (!sqlResult.Success)
                    throw new ResponseException(ResponseCode.INTERNAL_SERVER_ERROR, sqlResult.Exception.Message);
                return sqlResult.Datas;
            }
            catch (Exception ex)
            {
                throw responseException(ex);
            }
        }

        protected async Task<ReadResult> ExecuteReadAsync(Func<Task<ISqlDataListResult>> executeFuncAsync)
        {
            try
            {
                var sqlResult = await executeFuncAsync();
                if (!sqlResult.Success)
                    throw new ResponseException(ResponseCode.INTERNAL_SERVER_ERROR, sqlResult.Exception.Message);
                return new ReadResult(sqlResult.Datas, sqlResult.Total);
            }
            catch (Exception ex)
            {
                throw responseException(ex);
            }
        }

        protected async Task<ReadResult<TData>> ExecuteReadAsync<TData>(Func<Task<ISqlDataListResult<TData>>> executeFuncAsync)
        {
            try
            {
                var sqlResult = await executeFuncAsync();
                if (!sqlResult.Success)
                    throw new ResponseException(ResponseCode.INTERNAL_SERVER_ERROR, sqlResult.Exception.Message);
                return new ReadResult<TData>(sqlResult.Datas, sqlResult.Total);
            }
            catch (Exception ex)
            {
                throw responseException(ex);
            }
        }

        ResponseException responseException(Exception ex)
        {
            if (ex is ResponseException rex)
                return rex;
            return new ResponseException(ResponseCode.INTERNAL_SERVER_ERROR, ex.Message);
        }
    }
}
