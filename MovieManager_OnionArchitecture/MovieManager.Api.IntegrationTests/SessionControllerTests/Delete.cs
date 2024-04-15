using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;

namespace MovieManager.Api.IntegrationTests.SessionControllerTests
{
    public class Delete : BaseTest
    {
        private readonly HttpClient _httpClient;

        public Delete()
        {
            _httpClient = InitTestServer().GetClient();
        }

        [Fact]
        public async Task Delete_IfRecordExists_RemoveEntityFromDb()
        {
            var session = await AddEntityToDb();

            var message = new HttpRequestMessage(HttpMethod.Get, $"api/v1/session/{session.Id}");

            var response = await _httpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync();
            var movieResponse = JsonConvert.DeserializeObject<Session>(responseContent);

            movieResponse?.RoomName.Should().Be(session.RoomName);
        }

        [Fact]
        public async Task Delete_IfIdSessionNotFound_ThrowNotFound()
        {
            int sessionId = 1;

            var message = new HttpRequestMessage(HttpMethod.Get, $"api/v1/session/{sessionId}");
            var response = await _httpClient.SendAsync(message);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
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
