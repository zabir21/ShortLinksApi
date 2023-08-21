using DapperRelization.Context.Dto;
using DapperRelization.Context.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShortLinksApi.BLL.Exceptions;
using ShortLinksApi.BLL.Services.Interfaces;
using ShortLinksApi.Contracts.Request;
using ShortLinksApi.Contracts.Response.Base;

namespace ShortLinksApi.Controllers
{
    [Route("api/shortlinks")]
    [ApiController]
    public class TagsController : BaseApiController
    {
        private readonly ITagService _tag;

        public TagsController(ITagService tag) 
        {
            _tag = tag;
        }

        [HttpPost("{id}/tags")]
        public async Task<IActionResult> CreateTagForShortLink(Guid id, Guid tagId )
        {
            try 
            {
                await _tag.AddTagsToShortLink(id, tagId);

                return Ok();

            }
            catch (ShortUrlNotFoundException ex) 
            {
                return BadRequest(Enums.ErrorCode.ShortUrlNotFound, ex.Message);
            }
            catch (TagNotFoundException ex) 
            {
                return BadRequest(Enums.ErrorCode.TagNotFound, ex.Message);
            }
        }

        [HttpDelete("{id}/tags/{tagId}")]
        public async Task<IActionResult> DeleteTagFromShortLink(Guid id, Guid tagId)
        {
            try
            {
                await _tag.RemoveTagFromShortLink(id, tagId);
                return Ok();
            }
            catch (ShortUrlNotFoundException ex)
            {
                return BadRequest(Enums.ErrorCode.ShortUrlNotFound, ex.Message);
            }
            catch (TagNotFoundException ex) 
            {
                return BadRequest(Enums.ErrorCode.TagNotFound, ex.Message);
            }

        }

        [HttpGet("tags/{tagId}")]
        public async Task<ActionResult<ApiResult<ShortLinkDto>>> GetShortLinksWithTag(Guid tagId)
        {
            try
            {
                var shortLinks = await _tag.GetShortLinksWithTag(tagId);

                return Ok(shortLinks);
            }
            catch (TagNotFoundException ex)
            {
                return BadRequest(Enums.ErrorCode.TagNotFound, ex.Message);
            }
        }
    }
}
