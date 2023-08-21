using AutoFixture;
using ShortLinkTest.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortLinkTest.Tests.TagControllerTest
{
    public class TagsDeleteWithShortUrlTest : IClassFixture<DockerWebApplicationFactoryFixture>
    {
        private readonly DockerWebApplicationFactoryFixture _factory;
        private readonly HttpClient _client;
        private readonly Fixture _fixture;

        public TagsDeleteWithShortUrlTest(DockerWebApplicationFactoryFixture factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            _fixture = new Fixture();
        }
    }
}
