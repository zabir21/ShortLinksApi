using DapperRelization.Context.Dto;
using DapperRelization.Context.Models;
using DapperRelization.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using ShortLinksApi.BLL.Exceptions;
using ShortLinksApi.BLL.Services.Interfaces;

namespace ShortLinksApi.BLL.Services
{
    public class ShortLinkService : IShortLinkService
    {
        private readonly IUrlRepository _repository;
        private readonly IHttpContextAccessor _ctx;

        public ShortLinkService(IUrlRepository repository, IHttpContextAccessor ctx)
        {
            _repository = repository;
            _ctx = ctx;
        }

        public async Task<ShortLinkDto> CreateShortLink(CreateShortLinkModel model)
        {

            var fullUrl = await _repository.GetFullLink(model.FullUrl);

            var host = _ctx.HttpContext!.Request.Host.Value;
            var protocol = _ctx.HttpContext.Request.IsHttps ? "https" : "http";

            if (fullUrl == null)
            {
                var id = Guid.NewGuid();

                var now = DateTime.UtcNow;

                var link = new ShortLinkModel
                {
                    Id = id,
                    full_url = model.FullUrl,
                    short_url = $"/api/shortlinks/{id:N}",
                    created_date = now
                };

                await _repository.CreateShortLink(link);

                return new ShortLinkDto
                {
                    Id = link.Id,
                    full_url = link.full_url,
                    short_url = $"{protocol}://{host}{link.short_url}",
                    created_date = link.created_date
                };
            }
            else
            {
                var shortUrl = await _repository.GetShortUrl(model.FullUrl);
                return new ShortLinkDto
                {
                    Id = shortUrl.Id,
                    full_url = shortUrl.full_url,
                    short_url = $"{protocol}://{host}{shortUrl.short_url}",
                    created_date = shortUrl.created_date
                };
            }

        }       

        public async Task<string> GetFullUrl(Guid id)
        {
            var fullUrl = await _repository.GetFullUrl(id);

            if (fullUrl == null)
            {
                throw new FullUrlNotFoundException("Failed to get full URL.");
            }

            return fullUrl;
        }

        public async Task<IEnumerable<ShortLinkDto>> GetAllShortLinks()
        {
            var result = await _repository.GetAllShortLinks();

            return result.Select(x => new ShortLinkDto
            {
                Id = x.Id,
                short_url = x.short_url,
                created_date = x.created_date,
                full_url = x.full_url,
            }).ToList();
        }

        public async Task DeleteShortLink(Guid id)
        {
            var shortLink = await _repository.GetShortUrl(id);

            if (shortLink != null)
            {
                await _repository.DeleteShortLink(id);
            }
            else
            {
                throw new ShortUrlNotFoundException();
            }
        }
    }
}