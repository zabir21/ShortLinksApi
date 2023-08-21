using Dapper;
using DapperRelization.Context;
using DapperRelization.Context.Dto;
using DapperRelization.Context.Models;
using DapperRelization.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using static Dapper.SqlMapper;


namespace DapperRelization.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        //private readonly IDbConnection connection;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public UrlRepository(IDbConnectionFactory dbConnectionFactory)
        {
            //_db = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task CreateShortLink(ShortLinkModel fullLink)
        {
            var sql = "INSERT INTO short_link (id,full_url,short_url, created_date)" +
                " VALUES (@Id, @full_url,@short_url,@created_date)";

            var connection = _dbConnectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, fullLink); 
        }

        public async Task<string> GetFullUrl(Guid id)
        {
            var query = " SELECT full_url FROM short_link WHERE id = @Id";
            
            using var connection = _dbConnectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<string>(query, new
            {
                Id = id
            });
        }

        public async Task<IEnumerable<ShortLinkModel>> GetAllShortLinks()
        {
            var query = " SELECT * FROM short_link";
            using var connection = _dbConnectionFactory.CreateConnection();

            return await connection.QueryAsync<ShortLinkModel>(query);
        }

        public async Task DeleteShortLink(Guid id)
        {
            var query = "DELETE FROM short_link WHERE id = @Id";
            using var connection = _dbConnectionFactory.CreateConnection();

            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task AddTagsToShortLink(Guid shortLinkId, Guid tagId)
        {
            var sql = "INSERT INTO link_tag (short_link_id, tag_id) " +
                                 "VALUES (@ShortLinkId, @TagId)";
            using var connection = _dbConnectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                ShortLinkId = shortLinkId,
                TagId = tagId
            });
        }

        public async Task<string> GetShortUrl(Guid shortLinkId)
        {
            var shortUrl = "SELECT short_url FROM short_link WHERE id = @Id";
            using var connection = _dbConnectionFactory.CreateConnection();

            var url = await connection.QuerySingleOrDefaultAsync<string>(shortUrl, new
            {
                Id = shortLinkId
            });

            return url;
        }

        public async Task<Guid> GetTagId(Guid tagId)
        {
            var query = "SELECT id FROM tag WHERE id = @id";
            var connection = _dbConnectionFactory.CreateConnection();

            var tag = await connection.QuerySingleOrDefaultAsync<Guid>(query, new
            {
                Id = tagId
            });

            return tag;
        }

        public async Task<Guid> GetTagIdByShortUrlId(Guid shortLinkId, Guid tagId)
        {
            var query = "select tag_id from link_tag where short_link_id = @Id and tag_id = @TagId";
            var connection = _dbConnectionFactory.CreateConnection();

            var tag = await connection.QueryFirstOrDefaultAsync<Guid>(query, new
            {
                Id = shortLinkId,
                TagId = tagId
            });
            return tag;
        }

        public async Task RemoveTagFromShortLink(Guid shortLinkId, Guid tagId)
        {
            var query = "DELETE FROM link_tag WHERE short_link_id = @ShortLinkId AND tag_id = @TagId ";
            var connection = _dbConnectionFactory.CreateConnection();

            await connection.ExecuteAsync(query, new { ShortLinkId = shortLinkId, TagId = tagId });

        }
        public async Task<IEnumerable<string>> GetShortLinksByTag(Guid tagId)
        {
            var query = "SELECT short_url FROM short_link " +
                " INNER JOIN link_tag ON id = short_link_id " +
                " WHERE tag_id = @TagId";
            var connection = _dbConnectionFactory.CreateConnection();

            return await connection.QueryAsync<string>(query, new
            {
                TagId = tagId
            });
        }
        public async Task<ShortLinkModel> GetFullLink(string fullLink)
        {
            var fullUrl = "SELECT full_url FROM short_link WHERE full_url = @FullUrl";
            var connection = _dbConnectionFactory.CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<ShortLinkModel>(fullUrl, new
            {
                FullUrl = fullLink
            });
        }
        
        public async Task<ShortLinkModel> GetShortUrl(string fullLink)
        {
            var fullUrl = "SELECT short_url FROM short_link WHERE full_url = @FullUrl";
            var connection = _dbConnectionFactory.CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<ShortLinkModel>(fullUrl, new
            {
                FullUrl = fullLink
            });
        }
    }
}
