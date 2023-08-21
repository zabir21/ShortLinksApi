using DapperRelization.Context.Dto;
using DapperRelization.Context.Models;
using DapperRelization.DAL.Entity;

namespace DapperRelization.Repositories.Interfaces
{
    public interface IUrlRepository 
    {
        Task CreateShortLink(ShortLinkModel fullLink);
        Task<string> GetFullUrl(Guid id);
        Task<IEnumerable<ShortLinkModel>> GetAllShortLinks();
        Task DeleteShortLink(Guid id);
        Task AddTagsToShortLink(Guid shortLinkId,Guid tagId);
        Task RemoveTagFromShortLink(Guid shortLinkId, Guid tagId);
        Task<IEnumerable<string>> GetShortLinksByTag(Guid tagId);

        Task<string> GetShortUrl(Guid shortLinkId);

        Task<Guid> GetTagId(Guid tagId);

        Task<Guid> GetTagIdByShortUrlId(Guid shortIrl, Guid tagId);
        Task<ShortLinkModel> GetFullLink(string fullLink);
        Task<ShortLinkModel> GetShortUrl(string fullLink);

    }
}
