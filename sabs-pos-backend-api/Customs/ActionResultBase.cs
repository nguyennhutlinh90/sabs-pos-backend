using Microsoft.AspNetCore.Mvc;

using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace sabs_pos_backend_api
{
    public class ActionResultBase : ControllerBase
    {
        protected IActionResult Execute(Action executeFunc)
        {
            try
            {
                executeFunc();
                return ResponseResult.Success();
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure(ex);
            }
        }

        protected IActionResult Execute(Func<object> executeFunc)
        {
            try
            {
                var result = executeFunc();
                return ResponseResult.Success(result);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure(ex);
            }
        }

        protected IActionResult ExecuteRead(Func<ReadResult> executeFunc)
        {
            try
            {
                var result = executeFunc();
                return ResponseResult.Success(result.datas, result.total);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure(ex);
            }
        }

        protected IActionResult ExecuteRead<T>(Func<ReadResult<T>> executeFunc)
        {
            try
            {
                var result = executeFunc();
                return ResponseResult.Success(result.datas, result.total);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure(ex);
            }
        }

        protected IActionResult ExecutePagination(Func<PaginationResult> executeFunc)
        {
            try
            {
                var result = executeFunc();
                return ResponseResult.Success(result.datas, result.total, result.size, result.index);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure(ex);
            }
        }

        protected IActionResult ExecutePagination<T>(Func<PaginationResult<T>> executeFunc)
        {
            try
            {
                var result = executeFunc();
                return ResponseResult.Success(result.datas, result.total, result.size, result.index);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure(ex);
            }
        }

        protected async Task<IActionResult> ExecuteAsync(Func<Task> executeFuncAsync)
        {
            try
            {
                await executeFuncAsync();
                return ResponseResult.Success();
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure(ex);
            }
        }

        protected async Task<IActionResult> ExecuteAsync(Func<Task<object>> executeFuncAsync)
        {
            try
            {
                var result = await executeFuncAsync();
                return ResponseResult.Success(result);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure(ex);
            }
        }

        protected async Task<IActionResult> ExecuteReadAsync(Func<Task<ReadResult>> executeFuncAsync)
        {
            try
            {
                var result = await executeFuncAsync();
                return ResponseResult.Success(result.datas, result.total);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure(ex);
            }
        }

        protected async Task<IActionResult> ExecuteReadAsync<T>(Func<Task<ReadResult<T>>> executeFuncAsync)
        {
            try
            {
                var result = await executeFuncAsync();
                return ResponseResult.Success(result.datas, result.total);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure(ex);
            }
        }

        protected async Task<IActionResult> ExecutePaginationAsync(Func<Task<PaginationResult>> executeFuncAsync)
        {
            try
            {
                var result = await executeFuncAsync();
                return ResponseResult.Success(result.datas, result.total, result.size, result.index);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure(ex);
            }
        }

        protected async Task<IActionResult> ExecutePaginationAsync<T>(Func<Task<PaginationResult<T>>> executeFuncAsync)
        {
            try
            {
                var result = await executeFuncAsync();
                return ResponseResult.Success(result.datas, result.total, result.size, result.index);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure(ex);
            }
        }

        protected LoginInfo login
        {
            get
            {
                if (User == null)
                    throw new ResponseException(ResponseCode.UNAUTHORIZED);

                var owner_id = User.FindFirstValue(ClaimType.OWNER_ID).ConvertTo<int>();
                var login_id = User.FindFirstValue(ClaimType.LOGIN_ID).ConvertTo<int>();
                if (owner_id <= 0 || login_id <= 0)
                    throw new ResponseException(ResponseCode.UNAUTHORIZED);

                return new LoginInfo
                {
                    owner_id = owner_id,
                    owner_uuid = User.FindFirstValue(ClaimType.OWNER_UUID),
                    id = login_id,
                    uuid = User.FindFirstValue(ClaimType.LOGIN_UUID),
                    name = User.FindFirstValue(ClaimType.LOGIN_NAME)
                };
            }
        }
    }
}
