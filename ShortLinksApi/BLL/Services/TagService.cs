using DapperRelization.Context.Dto;
using DapperRelization.DAL.Entity;
using DapperRelization.Repositories.Interfaces;
using ShortLinksApi.BLL.Exceptions;
using ShortLinksApi.BLL.Services.Interfaces;

namespace ShortLinksApi.BLL.Services
{
    public class TagService : ITagService
    {
        private readonly IUrlRepository _repository;

        public TagService(IUrlRepository repository)
        {
            _repository = repository;
        }

        public async Task<LinkTagDto> AddTagsToShortLink(Guid shortLinkId, Guid tagId)
        {
            var shortLink = await _repository.GetShortUrl(shortLinkId);

            if (shortLink == null)
            {
                throw new ShortUrlNotFoundException();
            }

            var tagIdModel = await _repository.GetTagId(tagId);

            if (tagIdModel == Guid.Empty)
            {
                throw new TagNotFoundException();
            }

            var tagExists = await _repository.GetTagIdByShortUrlId(shortLinkId, tagId);

            if (tagExists != Guid.Empty)
            {
                return new LinkTagDto
                {
                    ShortLinkId = shortLinkId,
                    TagId = tagId
                };
            }
            else
            {
                await _repository.AddTagsToShortLink(shortLinkId, tagId);

                return new LinkTagDto
                {
                    ShortLinkId = shortLinkId,
                    TagId = tagId
                };
            }
        }

        public async Task RemoveTagFromShortLink(Guid id, Guid tagId)
        {
            var shortLink = await _repository.GetShortUrl(id);

            var tagIdModel = await _repository.GetTagId(tagId);

            if (shortLink == null)
            {
                throw new ShortUrlNotFoundException();
            }

            else if (tagIdModel == Guid.Empty)
            {
                throw new TagNotFoundException();
            }

            await _repository.RemoveTagFromShortLink(id, tagId);                       
        }

        public async Task<List<string>> GetShortLinksWithTag(Guid tagId)
        {
            var tag = await _repository.GetTagId(tagId);

            if (tag != Guid.Empty)
            {               
                var shortLinks = await _repository.GetShortLinksByTag(tagId);

                return shortLinks.ToList();
            }
            else
            { 
                throw new TagNotFoundException();
            }
        }
    }
}
