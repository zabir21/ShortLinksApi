using AutoFixture;
using DapperRelization.Context.Dto;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ShortLinksApi.Configuration;
using ShortLinksApi.Contracts.Request;
using ShortLinksApi.Contracts.Response.Base;
using ShortLinksApi.Enums;
using ShortLinkTest.Fixtures;
using ShortLinkTest.Helper;
using System.Net.Http.Json;
using Xunit;

namespace ShortLinkTest.Tests.ShortLinkControllerTest
{
    public class ShortLinkGetByIdTest : IClassFixture<DockerWebApplicationFactoryFixture>
    {
        private readonly DockerWebApplicationFactoryFixture _factory;
        private readonly HttpClient _client;
        private readonly Fixture _fixture;
        private readonly AuthorizationBasic _basicAuth;

        public ShortLinkGetByIdTest(DockerWebApplicationFactoryFixture factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            _fixture = new Fixture();
            _basicAuth = _factory.Services.GetRequiredService<IOptions<AuthorizationBasic>>().Value;
        }

        [Fact]
        public async Task GetById_Returns_ShortUrl()
        {
            _client.AddAuthorizationBasicHeader(_basicAuth.UserName, _basicAuth.Password);

            var request = new CreateShortLinkRequestModel()
            {
                FullUrl = "https://code-maze.com/dapper-migrations-fluentmigrator-aspnetcore/"
            };

            var createUrl = await _client.PostAsJsonAsync(HttpHelper.Urls.AddShortLink, request);

            createUrl.Should().NotBeNull();
            createUrl.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var shortId = await createUrl.Content.GetContent<ApiResult<ShortLinkDto>>();

            var response = await _client.GetAsync(HttpHelper.Urls.GetFullUrl+$"/{shortId.Data.Id}");

            response.RequestMessage.RequestUri.Should().Be(request.FullUrl);        
        }
    }
}
