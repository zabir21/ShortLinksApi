using DapperRelization.Context.Dto;
using DapperRelization.Context.Models;
using DapperRelization.DAL.Entity;

namespace ShortLinksApi.BLL.Services.Interfaces
{
    public interface IShortLinkService 
    {
        Task<ShortLinkDto> CreateShortLink(CreateShortLinkModel model);
        Task<string> GetFullUrl(Guid id);
        Task<IEnumerable<ShortLinkDto>> GetAllShortLinks();
        Task DeleteShortLink(Guid id);
        
    }
}
