using AutoFixture;
using DapperRelization.Context.Dto;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ShortLinksApi.Configuration;
using ShortLinksApi.Contracts.Request;
using ShortLinksApi.Contracts.Response.Base;
using ShortLinkTest.Fixtures;
using ShortLinkTest.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ShortLinkTest.Tests.ShortLinkControllerTest
{
    public class ShortLinkPostTest : IClassFixture<DockerWebApplicationFactoryFixture>
    {
        private readonly DockerWebApplicationFactoryFixture _factory;
        private readonly HttpClient _client;
        private readonly Fixture _fixture;
        private readonly AuthorizationBasic _basicAuth;

        public ShortLinkPostTest(DockerWebApplicationFactoryFixture factory)
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
        }
    }
}
