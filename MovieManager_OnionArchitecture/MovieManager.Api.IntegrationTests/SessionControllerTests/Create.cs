using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MovieManager.Api.IntegrationTests.SessionControllerTests
{
    public class Create : BaseTest
    {
        private readonly HttpClient _httpClient;
        public Create()
        {
            _httpClient = InitTestServer().GetClient();
        }

        private static Fixture fixture = new Fixture();
        private Session session = fixture.Build<Session>().With(x => x.Id, 1).Create();

        [Fact]
        public async Task Post_IfSessionIsCorrect_ReturnsOk()
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "api/v1/session");
            message.Content = new StringContent(JsonConvert.SerializeObject(session), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(message);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
