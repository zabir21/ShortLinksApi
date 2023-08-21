using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShortLinksApi.Contracts.Response.Base;
using ShortLinksApi.Enums;

namespace ShortLinksApi.Controllers
{   
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        [NonAction]
        protected OkObjectResult Ok<T>(T result) where T : class, new()
        {
            return base.Ok(ApiResult<T>.Succces(result));
        }

        [NonAction]
        public new OkObjectResult Ok()
        {
            return base.Ok(ApiResult.Succces());
        }

        [NonAction]
        protected NotFoundObjectResult NotFound(string message)
        {
            return base.NotFound(ApiResult.NotFound(message));
        }

        [NonAction]
        public new NotFoundObjectResult NotFound()
        {
            return base.NotFound(ApiResult.NotFound());
        }

        [NonAction]
        protected NotFoundObjectResult NotFound(string message, Dictionary<string, string[]> errors)
        {
            return base.NotFound(ApiResult.NotFound(message, errors));
        }

        [NonAction]
        protected NotFoundObjectResult NotFound(Dictionary<string, string[]> errors)
        {
            return base.NotFound(ApiResult.NotFound(errors));
        }

        [NonAction]
        protected BadRequestObjectResult BadRequest(string message, Dictionary<string, string[]> errors)
        {
            return base.BadRequest(ApiResult.BadRequest(message, errors));
        }

        [NonAction]
        protected BadRequestObjectResult BadRequest(ErrorCode errorCode, string message)
        {
            return base.BadRequest(ApiResult.BadRequest(message, errorCode));
        }

        [NonAction]
        protected BadRequestObjectResult BadRequest(ErrorCode errorCode)
        {
            return base.BadRequest(ApiResult.BadRequest(errorCode));
        }

        [NonAction]
        protected BadRequestObjectResult BadRequest(string message)
        {
            return base.BadRequest(ApiResult.BadRequest(message));
        }
    }
}
