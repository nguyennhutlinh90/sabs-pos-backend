using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;

namespace sabs_pos_backend_api
{
    public class ResponseResult : OkObjectResult
    {
        public ResponseResult(object value) : base(value)
        {

        }

        public static ResponseResult Failure(int status, string code, string message)
        {
            var value = new { success = false, error_code = code, error_message = message };
            return new ResponseResult(value);
        }

        public static ResponseResult Failure(Exception ex)
        {
            var code = ResponseCode.INTERNAL_SERVER_ERROR.ToString();
            if (ex is ResponseException responseException)
                code = responseException.Code;

            var value = new { success = false, error_code = code, error_message = ex.Message };
            return new ResponseResult(value);
        }

        public static ResponseResult Success()
        {
            var value = new { success = true };
            return new ResponseResult(value);
        }

        public static ResponseResult Success<T>(T data)
        {
            var value = new { data = data, success = true };
            return new ResponseResult(value);
        }

        public static ResponseResult Success<T>(IEnumerable<T> datas)
        {
            var value = new { data = datas, success = true };
            return new ResponseResult(value);
        }

        public static ResponseResult Success<T>(IEnumerable<T> datas, long total)
        {
            var value = new { data = datas, total, success = true };
            return new ResponseResult(value);
        }

        public static ResponseResult Success<T>(IEnumerable<T> datas, long total, int page_size, int page_index)
        {
            var pagination = new { page_size = page_size, page_index = page_index, total = total };
            var value = new { data = datas, pagination = pagination, success = true };
            return new ResponseResult(value);
        }
    }
}
