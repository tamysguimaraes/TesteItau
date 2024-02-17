using API.Test.Requests;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace API.Test.Testes
{
    public class POSTTests : ControllerBase
    {
        const string METODO = "/api/Products";
        public POSTTests(WebAppFactoryTest<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async void Validate_CreateProduct_Success()
        {
            var request = new ProductRequest
            {
                cBarCode = "711719547266",
                cName = "GOW Ragnarok",
                cCategory = "Jogo PS4",
                nValue = 299.90m
            };
            var response = await PostRequest(METODO, request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            responseData.Should().NotBeNull();
        }

        [Fact]
        public async void Validate_CreateProduct_Error_InvalidBarCode()
        {
            var request = new ProductRequest
            {
                cBarCode = "12345678",
                cName = "GOW Ragnarok",
                cCategory = "Jogo PS4",
                nValue = 299.90m
            };
            var response = await PostRequest(METODO, request);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseBody = await response.Content.ReadAsStringAsync();

            responseBody.Length.Should().BeGreaterThan(0);
            responseBody.Should().Contain("Código de barras inválido");
        }

        [Fact]
        public async void Validate_CreateProduct_Error_BarCodeExist()
        {
            var request = new ProductRequest
            {
                cBarCode = "711719547266",
                cName = "GOW Ragnarok",
                cCategory = "Jogo PS4",
                nValue = 299.90m
            };
            var response = await PostRequest(METODO, request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            request = new ProductRequest
            {
                cBarCode = "711719547266",
                cName = "GOW 3",
                cCategory = "Jogo PS4",
                nValue = 100.90m
            };

            response = await PostRequest(METODO, request);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseBody = await response.Content.ReadAsStringAsync();
            
            responseBody.Length.Should().BeGreaterThan(0);
            responseBody.Should().Contain("Código de barras já existe");

        }
    }
}
