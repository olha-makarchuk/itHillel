using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MovieManager.Api.IntegrationTests.SessionControllerTests
{
    public class Update : BaseTest
    {
        private readonly HttpClient _httpClient;
        private static Fixture fixture = new Fixture();

        public Update()
        {
            _httpClient = InitTestServer().GetClient();
        }

        private Session session = fixture.Build<Session>().With(x => x.Id, 1).Create();

        [Fact]
        public async Task UpdateAsync_IfSessionExists_ReturnsOk()
        {
            var movieInDb = await AddEntityToDb();

            var message = new HttpRequestMessage(HttpMethod.Put, $"api/v1/session/{movieInDb.Id}");
            message.Content = new StringContent(JsonConvert.SerializeObject(session), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(message);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async Task UpdateAsync_IfSessionIsNotExists_ThrowNotFound()
        {
            int sessionId = 1;

            var message = new HttpRequestMessage(HttpMethod.Put, $"api/v1/session/{sessionId}");
            message.Content = new StringContent(JsonConvert.SerializeObject(session), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(message);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateAsync_IfSessionUpdates_ReturnsOk()
        {
            var sessionInDb = await AddEntityToDb();

            var message = new HttpRequestMessage(HttpMethod.Put, $"api/v1/session/{sessionInDb.Id}");
            message.Content = new StringContent(JsonConvert.SerializeObject(session), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(message);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBe(sessionInDb);
        }
        public async Task<Session> AddEntityToDb()
        {
            var fixture = new Fixture();
            var session = fixture.Build<Session>().With(x => x.Id, 1).Create();

            await AppDbContext.Sessions.AddAsync(session);
            AppDbContext.SaveChanges();

            return session;
        }
    }
}
