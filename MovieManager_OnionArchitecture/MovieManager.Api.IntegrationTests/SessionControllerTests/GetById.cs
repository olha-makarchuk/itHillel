using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;

namespace MovieManager.Api.IntegrationTests.SessionControllerTests
{
    public class GetById : BaseTest
    {
        private readonly HttpClient _httpClient;
        public GetById()
        {
            _httpClient = InitTestServer().GetClient();
        }

        [Fact]
        public async Task GetById_IfRecordExists_ReturnsOk()
        {
            var session = await AddEntityToDb();

            var message = new HttpRequestMessage(HttpMethod.Get, $"api/v1/session/{session.Id}");
            var response = await _httpClient.SendAsync(message);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_IfIdSessionNotFound_ThrowNotFound()
        {
            int sessionId = 2;

            var message = new HttpRequestMessage(HttpMethod.Get, $"api/v1/session/{sessionId}");
            var response = await _httpClient.SendAsync(message);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetById_IfMovieExist_ReturnsTitleOfMovieNotBeNull()
        {
            var SessionInDb = await AddEntityToDb();

            var message = new HttpRequestMessage(HttpMethod.Get, $"api/v1/session/{SessionInDb.Id}");
            var response = await _httpClient.SendAsync(message);
            var responseContent = await response.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<Session>(responseContent);

            movies.Should().NotBeNull();
            movies?.RoomName.Should().Be(SessionInDb.RoomName);
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
