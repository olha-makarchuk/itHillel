using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;

namespace MovieManager.Api.IntegrationTests.MovieControllerTests
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
            var movie = await AddEntityToDb();

            var message = new HttpRequestMessage(HttpMethod.Get, $"api/v1/movie/{movie.Id}");
            var response = await _httpClient.SendAsync(message);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_IfIdMovieNotFound_ThrowNotFound()
        {
            int movieId = 2;

            var message = new HttpRequestMessage(HttpMethod.Get, $"api/v1/movie/{movieId}");
            var response = await _httpClient.SendAsync(message);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetById_IfMovieExist_ReturnsTitleOfMovieNotBeNull()
        {
            var movie = await AddEntityToDb();

            var message = new HttpRequestMessage(HttpMethod.Get, $"api/v1/movie/{movie.Id}");
            var response = await _httpClient.SendAsync(message);
            var responseContent = await response.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<Movie>(responseContent);

            movies.Should().NotBeNull();
            movies?.Title.Should().Be(movie.Title);
        }

        public async Task<Movie> AddEntityToDb()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<Movie>().With(x => x.Id, 1).Create();

            await AppDbContext.Movies.AddAsync(movie);
            AppDbContext.SaveChanges();

            return movie;
        }
    }
}
