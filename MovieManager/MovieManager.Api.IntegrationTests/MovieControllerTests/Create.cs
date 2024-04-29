using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MovieManager.Api.IntegrationTests.MovieControllerTests
{
    public class Create : BaseTest
    {
        private readonly HttpClient _httpClient;
        public Create()
        {
            _httpClient = InitTestServer().GetClient();
        }

        private static Fixture fixture = new Fixture();
        private Movie movie = fixture.Build<Movie>().With(x => x.Id, 1).Create();

        [Fact]
        public async Task Post_IfMovieIsCorrect_ReturnsOk()
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "api/v1/movie");
            message.Content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(message);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
