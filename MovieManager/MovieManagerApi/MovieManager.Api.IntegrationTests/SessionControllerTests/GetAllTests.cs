using Domain.Entities;
using FluentAssertions;
using System.Net;
using Newtonsoft.Json;
using AutoFixture;

namespace MovieManager.Api.IntegrationTests.SessionControllerTests
{
    public class GetAllTests : BaseTest
    {
        private readonly HttpClient _httpClient;

        public GetAllTests()
        {
            _httpClient = InitTestServer().GetClient();
        }

        [Fact]
        public async Task GetAsync_IfSessionNotExists_ShouldReturnsOkAndSessionListNull()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/v1/session");
            var response = await _httpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var sessions = JsonConvert.DeserializeObject<List<Session>>(responseContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Empty(sessions);
        }

        [Fact]
        public async Task GetAsync_IfBookExists_ShouldReturnsTitleOfBookNotBeNull()
        {
            var sessionInDb = await AddEntityToDb();

            var message = new HttpRequestMessage(HttpMethod.Get, "api/v1/session");
            var response = await _httpClient.SendAsync(message);

            var responseContent = await response.Content.ReadAsStringAsync();
            var sessions = JsonConvert.DeserializeObject<List<Session>>(responseContent);
            sessions.Should().NotBeNull();
            sessions?[0].RoomName.Should().Be(sessionInDb.RoomName);
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
