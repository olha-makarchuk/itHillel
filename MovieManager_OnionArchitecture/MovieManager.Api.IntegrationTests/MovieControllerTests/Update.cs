using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MovieManager.Api.IntegrationTests.MovieControllerTests
{
    public class Update : BaseTest
    {
        private readonly HttpClient _httpClient;
        private static Fixture fixture = new Fixture();

        public Update()
        {
            _httpClient = InitTestServer().GetClient();
        }

        private Movie movie = fixture.Build<Movie>().With(x => x.Id, 1).Create();

        [Fact]
        public async Task UpdateAsync_IfMovieExists_ReturnsOk()
        {
            var movieInDb = await AddEntityToDb();

            var message = new HttpRequestMessage(HttpMethod.Put, $"api/v1/movie/{movieInDb.Id}");
            message.Content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(message);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async Task UpdateAsync_IfMovieIsNotExists_ThrowNotFound()
        {
            int movieId = 1;

            var message = new HttpRequestMessage(HttpMethod.Put, $"api/v1/movie/{movieId}");
            message.Content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(message);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateAsync_IfMovieUpdates_ReturnsOk()
        {
            var movieInDb = await AddEntityToDb();

            var message = new HttpRequestMessage(HttpMethod.Put, $"api/v1/movie/{movieInDb.Id}");
            message.Content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(message);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBe(movieInDb);
        }

        public async Task<Movie> AddEntityToDb()
        {
            var movie = fixture.Build<Movie>().With(x => x.Id, 1).Create();

            await AppDbContext.Movies.AddAsync(movie);
            AppDbContext.SaveChanges();

            return movie;
        }
    }
}
