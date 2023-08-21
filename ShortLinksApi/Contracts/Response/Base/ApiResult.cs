using Microsoft.AspNetCore.Http;
using ShortLinksApi.Enums;

namespace ShortLinksApi.Contracts.Response.Base
{
    public class ApiResult<T> : ApiResult where T : class, new()
    {
        public T Data { get; set; }

        public ApiResult() { }

        public ApiResult(T data, ResultStatus status, string message, Dictionary<string, string[]> errors)
        {
            Data = data;
            Errors = errors;
            Message = message;
            Status = status;
        }

        public static ApiResult<T> Succces(T data)
        {
            return new ApiResult<T>
            {
                Data = data,
                Status = ResultStatus.Success
            };
        }

        public static ApiResult<T> BadRequest(T data, string message, Dictionary<string, string[]> errors)
        {
            return new ApiResult<T>
            {
                Data = data,
                Status = ResultStatus.Failed,
                Message = message,
                Errors = errors
            };
        }

        public static ApiResult<T> NotFound(string message, Dictionary<string, string[]> errors)
        {
            return new ApiResult<T>
            {
                Status = ResultStatus.NotFound,
                Message = message,
                Errors = errors
            };
        }

        public static ApiResult<T> NotFound(string message)
        {
            return new ApiResult<T>
            {
                Status = ResultStatus.NotFound,
                Message = message
            };
        }

        public static ApiResult<T> NotFound()
        {
            return new ApiResult<T>
            {
                Status = ResultStatus.NotFound
            };
        }

        public static ApiResult<T> NotFound(Dictionary<string, string[]> errors)
        {
            return new ApiResult<T>
            {
                Status = ResultStatus.Failed,
                Errors = errors
            };
        }
    }

    public class ApiResult
    {
        public string Message { get; set; }

        public Dictionary<string, string[]> Errors { get; set; }

        public ResultStatus Status { get; set; }

        public ErrorCode? ErrorCode { get; set; }

        public static ApiResult Succces()
        {
            return new ApiResult
            {
                Status = ResultStatus.Success
            };
        }

        public static ApiResult BadRequest(string message, ErrorCode errorCode, Dictionary<string, string[]> errors)
        {
            return new ApiResult
            {
                Status = ResultStatus.Failed,
                Message = message,
                Errors = errors,
                ErrorCode = errorCode
            };
        }

        public static ApiResult BadRequest(string message, Dictionary<string, string[]> errors)
        {
            return new ApiResult
            {
                Status = ResultStatus.Failed,
                Message = message,
                Errors = errors
            };
        }

        public static ApiResult BadRequest(string message, ErrorCode errorCode)
        {
            return new ApiResult
            {
                Status = ResultStatus.Failed,
                Message = message,
                ErrorCode = errorCode
            };
        }

        public static ApiResult BadRequest(string message)
        {
            return new ApiResult
            {
                Status = ResultStatus.Failed,
                Message = message
            };
        }

        public static ApiResult BadRequest(ErrorCode errorCode)
        {
            return new ApiResult
            {
                Status = ResultStatus.Failed,
                ErrorCode = errorCode
            };
        }

        public static ApiResult NotFound(string message, Dictionary<string, string[]> errors)
        {
            return new ApiResult
            {
                Status = ResultStatus.NotFound,
                Message = message,
                Errors = errors
            };
        }

        public static ApiResult NotFound(string message)
        {
            return new ApiResult
            {
                Status = ResultStatus.NotFound,
                Message = message
            };
        }

        public static ApiResult NotFound(string message, ErrorCode errorCode)
        {
            return new ApiResult
            {
                Status = ResultStatus.NotFound,
                Message = message,
                ErrorCode = errorCode
            };
        }

        public static ApiResult NotFound(ErrorCode errorCode)
        {
            return new ApiResult
            {
                Status = ResultStatus.NotFound,
                ErrorCode = errorCode
            };
        }


        public static ApiResult NotFound()
        {
            return new ApiResult
            {
                Status = ResultStatus.NotFound
            };
        }

        public static ApiResult NotFound(Dictionary<string, string[]> errors)
        {
            return new ApiResult
            {
                Status = ResultStatus.Failed,
                Errors = errors
            };
        }
    }
}
