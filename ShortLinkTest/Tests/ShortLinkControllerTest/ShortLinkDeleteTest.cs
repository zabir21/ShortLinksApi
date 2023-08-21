using AutoFixture;
using DapperRelization.Context.Dto;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ShortLinksApi.Configuration;
using ShortLinksApi.Contracts.Request;
using ShortLinksApi.Contracts.Response.Base;
using ShortLinkTest.Fixtures;
using ShortLinkTest.Helper;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace ShortLinkTest.Tests.ShortLinkControllerTest
{
    public class ShortLinkDeleteTest : IClassFixture<DockerWebApplicationFactoryFixture>
    {
        private readonly DockerWebApplicationFactoryFixture _factory;
        private readonly HttpClient _client;
        private readonly Fixture _fixture;
        private readonly AuthorizationBasic _basicAuth;

        public ShortLinkDeleteTest(DockerWebApplicationFactoryFixture factory)
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
                FullUrl = "https://www.codingame.com/playgrounds/35462/creating-web-api-in-asp-net-core-2-0/part-3---integration-tests"
            };

            var createUrl = await _client.PostAsJsonAsync(HttpHelper.Urls.AddShortLink, request);

            createUrl.Should().NotBeNull();

            var shortId = await createUrl.Content.GetContent<ApiResult<ShortLinkDto>>();

            var response = await _client.DeleteAsync(HttpHelper.Urls.DeleteShortLink + shortId.Data.Id);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);      
        }
    }
}
