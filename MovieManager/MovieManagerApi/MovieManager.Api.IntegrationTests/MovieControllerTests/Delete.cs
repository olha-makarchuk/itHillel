using AutoFixture;
using Domain.Entities;
using Newtonsoft.Json;
using System.Net;
using System;
using FluentAssertions;
using Persistence.Context;

namespace MovieManager.Api.IntegrationTests.MovieControllerTests
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
            var movie = await AddEntityToDb();

            var message = new HttpRequestMessage(HttpMethod.Get, $"api/v1/movie/{movie.Id}");

            var response = await _httpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync();
            var movieResponse = JsonConvert.DeserializeObject<Movie>(responseContent);

            movieResponse?.Title.Should().Be(movie.Title);
        }

        [Fact]
        public async Task Delete_IfIdMovieNotFound_ThrowNotFound()
        {
            int movieId = 1;

            var message = new HttpRequestMessage(HttpMethod.Get, $"api/v1/movie/{movieId}");
            var response = await _httpClient.SendAsync(message);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
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
