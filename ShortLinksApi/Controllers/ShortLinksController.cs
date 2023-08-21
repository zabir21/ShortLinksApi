using DapperRelization.Context.Dto;
using DapperRelization.Context.Models;
using Microsoft.AspNetCore.Mvc;
using ShortLinksApi.BLL.Exceptions;
using ShortLinksApi.BLL.Services.Interfaces;
using ShortLinksApi.Contracts.Request;
using ShortLinksApi.Contracts.Response.Base;

namespace ShortLinksApi.Controllers
{
    [Route("api/shortlinks")]
    [ApiController]
    public class ShortLinksController : BaseApiController
    {
        private readonly IShortLinkService _shortLinkService;

        public ShortLinksController(IShortLinkService shortLinkService)
        {
            _shortLinkService = shortLinkService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<ShortLinkDto>>> AddShortLink(CreateShortLinkRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ValidateUrlWithWellFormedUriString(request.FullUrl) == false)
            {
                return BadRequest();
            }

            var result = await _shortLinkService.CreateShortLink(new CreateShortLinkModel()
            {
                FullUrl = request.FullUrl,
            });

            return Ok(result);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetFullUrl(string id)
        {
            try
            {
                Guid guidId;

                if (id.Contains("-"))
                {
                    guidId = Guid.Parse(id);
                }
                else
                {
                    guidId = Guid.ParseExact(id, "N");
                }

                var fullUrl = await _shortLinkService.GetFullUrl(guidId);

                return Redirect(fullUrl);
            }
            catch (FullUrlNotFoundException e)
            {
                return BadRequest(Enums.ErrorCode.FullUrlNotFound, e.Message);
            }      
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<ShortLinkDto>>>> GetAllFullUrl()
        {
            var shortLinks = await _shortLinkService.GetAllShortLinks();

            return Ok(shortLinks.ToList());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteShortLink(Guid id)
        {
            try
            {
                await _shortLinkService.DeleteShortLink(id);
                return Ok();
            }

            catch (ShortUrlNotFoundException ex) 
            {
                return BadRequest(Enums.ErrorCode.ShortUrlNotFound, ex.Message);
            }
        }

        static bool ValidateUrlWithWellFormedUriString(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }
    }
}
