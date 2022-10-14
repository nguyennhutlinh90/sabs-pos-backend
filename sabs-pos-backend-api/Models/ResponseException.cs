using System;

namespace sabs_pos_backend_api
{
    public class ResponseException : Exception
    {
        public ResponseException(ResponseCode errorCode, string message) : base(message)
        {
            HResult = (int)errorCode;
            Code = errorCode.ToString();
        }
        public ResponseException(ResponseCode errorCode, params object[] errorArgs) : base(errorCode.GetDecription(errorArgs))
        {
            HResult = (int)errorCode;
            Code = errorCode.ToString();
        }

        public string Code { get; set; }
    }
}
