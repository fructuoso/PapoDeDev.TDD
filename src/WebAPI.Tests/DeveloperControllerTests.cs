using Microsoft.AspNetCore.Mvc;
using PapoDeDev.TDD.WebAPI.Developer;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAPI.Tests.Setup;
using Xunit;

namespace WebAPI.Tests
{
    public class DeveloperControllerTests : TestBase
    {
        public DeveloperControllerTests(CustomWebApplicationFactory factory) : base(factory) { }

        [Fact(DisplayName = "DADO um Developer válido QUANDO solicitamos sua inclusão ENTÃO retornar StatusCode = Created")]
        public async Task PostWhenIsValid()
        {
            //Arrange
            HttpClient httpClient = CreateHttpClient();
            DeveloperModel model = new DeveloperModel() { FirstName = "Victor", LastName = "Fructuoso" };

            //Act
            var response = await httpClient.PostAsJsonAsync("/api/Developer", model);

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact(DisplayName = "DADO um Developer inválido QUANDO solicitamos sua inclusão ENTÃO retornar StatusCode = BadRequest")]
        public async Task PostWhenIsNotValid()
        {
            //Arrange
            HttpClient httpClient = CreateHttpClient();
            DeveloperModel model = new DeveloperModel() { FirstName = "Victor" };

            //Act
            var response = await httpClient.PostAsJsonAsync("/api/Developer", model);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "DADO um Developer válido QUANDO solicitamos sua modificação ENTÃO retornar StatusCode = NoContent")]
        public async Task PutWhenIsValid()
        {
            //Arrange
            HttpClient httpClient = CreateHttpClient();

            //Act
            var response = await httpClient.PutAsJsonAsync("/api/Developer/0d46be7a-3417-4a06-adff-3e1090bf4ea9", new DeveloperModel() { FirstName = "Victor", LastName = "Fructuoso" });

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact(DisplayName = "DADO um Developer inexistente QUANDO solicitamos sua modificação ENTÃO retornar StatusCode = NotFound")]
        public async Task PutWhenNotExist()
        {
            //Arrange
            HttpClient httpClient = CreateHttpClient();

            //Act
            var response = await httpClient.PutAsJsonAsync($"/api/Developer/{Guid.NewGuid()}", new DeveloperModel() { FirstName = "Victor", LastName = "Fructuoso" });

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact(DisplayName = "DADO que temos ao menos um Developer existente QUANDO solicitamos uma listagem ENTÃO retornar StatusCode = OK E uma lista de DeveloperModel")]
        public async Task GetWhenExists()
        {
            //Arrange
            HttpClient httpClient = CreateHttpClient();

            //Act
            var response = await httpClient.GetAsync("/api/Developer");
            var developers = await response.Content.ReadFromJsonAsync<IEnumerable<DeveloperModel>>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(developers);
        }

        [Fact(DisplayName = "DADO um Developer existente QUANDO consultamos por Id ENTÃO retornar StatusCode = OK")]
        public async Task GetByIdWhenExists()
        {
            //Arrange
            HttpClient httpClient = CreateHttpClient();

            //Act
            var response = await httpClient.GetAsync("/api/Developer/0d46be7a-3417-4a06-adff-3e1090bf4ea9");
            var developer = await response.Content.ReadFromJsonAsync<DeveloperModel>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(developer);
            Assert.Equal(Guid.Parse("0d46be7a-3417-4a06-adff-3e1090bf4ea9"), developer.Id);
        }

        [Fact(DisplayName = "DADO um Developer inexistente QUANDO consultamos por Id ENTÃO retornar StatusCode = NotFound")]
        public async Task GetByIdWhenNotExist()
        {
            //Arrange
            HttpClient httpClient = CreateHttpClient();

            //Act
            var response = await httpClient.GetAsync($"/api/Developer/{Guid.NewGuid()}");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact(DisplayName = "DADO um Developer válido QUANDO solicitamos sua exclusão ENTÃO retornar StatusCode = NoContent")]
        public async Task DeleteWhenIsValid()
        {
            //Arrange
            HttpClient httpClient = CreateHttpClient();

            //Act
            var response = await httpClient.DeleteAsync("/api/Developer/d13a07df-085d-4830-a26d-976fa06c1074");

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact(DisplayName = "DADO um Developer inexistente QUANDO solicitamos sua exclusão ENTÃO retornar StatusCode = NotFound")]
        public async Task DeleteWhenNotExist()
        {
            //Arrange
            HttpClient httpClient = CreateHttpClient();

            //Act
            var response = await httpClient.DeleteAsync($"/api/Developer/{Guid.NewGuid()}");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
