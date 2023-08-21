using AutoFixture;
using DapperRelization.Context.Dto;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Ocsp;
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

namespace ShortLinkTest.Tests.TagControllerTest
{
    public class GetAllShortUrlByTagIdTest : IClassFixture<DockerWebApplicationFactoryFixture>
    {
        private readonly DockerWebApplicationFactoryFixture _factory;
        private readonly HttpClient _client;
        private readonly Fixture _fixture;
        private readonly AuthorizationBasic _basicAuth;

        public GetAllShortUrlByTagIdTest(DockerWebApplicationFactoryFixture factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            _fixture = new Fixture();
            _basicAuth = _factory.Services.GetRequiredService<IOptions<AuthorizationBasic>>().Value;

        }
    }
}
