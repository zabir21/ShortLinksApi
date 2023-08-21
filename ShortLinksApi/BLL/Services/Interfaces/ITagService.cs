using DapperRelization.Context.Dto;
using DapperRelization.Context.Models;

namespace ShortLinksApi.BLL.Services.Interfaces
{
    public interface ITagService
    {
        Task<LinkTagDto> AddTagsToShortLink(Guid shortLinkId, Guid tagId);
        Task RemoveTagFromShortLink(Guid id, Guid tagId);
        Task<List<string>> GetShortLinksWithTag(Guid tagId);
    }
}
