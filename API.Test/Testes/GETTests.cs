using API.Test.Requests;
using FluentAssertions;
using Products.Domain.Models;
using System.Net;
using System.Text.Json;

namespace API.Test.Testes
{
    public class GETTests : ControllerBase
    {
        public GETTests(WebAppFactoryTest<Program> factory) : base(factory)
        {
        }
        const string METODO = "/api/Products";
        [Fact]
        public async void Validate_GetAllProducts_Success()
        {
            PopulateProducts();

            var responseGet = await GetRequest($"{METODO}/GetProducts");

            responseGet.StatusCode.Should().Be(HttpStatusCode.OK);

            await using var responseBody = await responseGet.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            responseData.Should().NotBeNull();

            var list = JsonSerializer.Deserialize<List<Product>>(responseData);

        }

        [Fact]
        public async void Validate_GetAllProducts_Error_NoProducts()
        {
            var response = await GetRequest($"{METODO}/GetProducts");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseBody = await response.Content.ReadAsStringAsync();

            responseBody.Length.Should().BeGreaterThan(0);
            responseBody.Should().Contain("Não há produtos cadastrados!");
        }

        [Fact]
        public async void Validate_GetProductByID_Success()
        {
            PopulateProducts();

            var responseGet = await GetRequest($"{METODO}/GetProductByID/1");
            responseGet.StatusCode.Should().Be(HttpStatusCode.OK);

            await using var responseBody = await responseGet.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            responseData.Should().NotBeNull();
        }

        [Fact]
        public async void Validate_GetProductByName_Success()
        {
            PopulateProducts();

            var responseGet = await GetRequest($"{METODO}/GetProductByName/Ragnarok");
            responseGet.StatusCode.Should().Be(HttpStatusCode.OK);

            await using var responseBody = await responseGet.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            responseData.Should().NotBeNull();
        }

        [Fact]
        public async void Validate_GetProductByID_Error()
        {
            PopulateProducts();

            var responseGet = await GetRequest($"{METODO}/GetProductByID/10");
            responseGet.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseBody = await responseGet.Content.ReadAsStringAsync();

            responseBody.Length.Should().BeGreaterThan(0);
            responseBody.Should().Contain("Produto não encontrado!");
        }

        [Fact]
        public async void Validate_GetProductByName_Error()
        {
            PopulateProducts();

            var responseGet = await GetRequest($"{METODO}/GetProductByName/Chocolate");
            responseGet.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseBody = await responseGet.Content.ReadAsStringAsync();

            responseBody.Length.Should().BeGreaterThan(0);
            responseBody.Should().Contain("Produto não encontrado!");
        }
    }
}
