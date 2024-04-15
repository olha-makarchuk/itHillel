using Domain.Entities;
using FluentAssertions;
using System.Net;
using Newtonsoft.Json;
using AutoFixture;

namespace MovieManager.Api.IntegrationTests.MovieControllerTests
{
    public class GetAllTests : BaseTest
    {
        private readonly HttpClient _httpClient;

        public GetAllTests()
        {
            _httpClient = InitTestServer().GetClient();
        }

        [Fact]
        public async Task GetAsync_IfMovieNotExists_ShouldReturnsOkAndMovieListNull()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/v1/movie");
            var response = await _httpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<List<Movie>>(responseContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Empty(movies);
        }

        [Fact]
        public async Task GetAsync_IfBookExists_ShouldReturnsTitleOfBookNotBeNull()
        {
            var movie = await AddEntityToDb();

            var message = new HttpRequestMessage(HttpMethod.Get, "api/v1/movie");
            var response = await _httpClient.SendAsync(message);

            var responseContent = await response.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<List<Movie>>(responseContent);
            movies.Should().NotBeNull();
            movies?[0].Title.Should().Be(movie.Title);
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
