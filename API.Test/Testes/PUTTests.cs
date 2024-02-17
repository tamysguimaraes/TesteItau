using API.Test.Requests;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace API.Test.Testes
{
    public class PUTTests : ControllerBase
    {
        public PUTTests(WebAppFactoryTest<Program> factory) : base(factory)
        {
        }

        const string METODO = "/api/Products";

        [Fact]
        public async void Validate_UpdateProduct_Success()
        {
            var request = new ProductRequest
            {
                cBarCode = "78937680",
                cName = "Chesterfield",
                cCategory = "Cigarro",
                nValue = 5.75m
            };
            var responsePost = await PostRequest(METODO, request);
            responsePost.StatusCode.Should().Be(HttpStatusCode.OK);

            request = new ProductRequestUpdate
            {
                Id = 1,
                cBarCode = "78937680",
                cName = "Chester",
                cCategory = "Frios",
                nValue = 5.75m
            };
            var responsePut = await PutRequest(METODO, request);

            responsePut.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void Validate_UpdateProduct_Error_BarCode()
        {
            var request = new ProductRequest
            {
                cBarCode = "78937680",
                cName = "Chesterfield",
                cCategory = "Cigarro",
                nValue = 5.75m
            };
            var responsePost = await PostRequest(METODO, request);
            responsePost.StatusCode.Should().Be(HttpStatusCode.OK);

            request = new ProductRequestUpdate
            {
                Id =1,
                cBarCode = "123456",
                cName = "Chester",
                cCategory = "Frios",
                nValue = 5.75m
            };
            var responsePut = await PutRequest(METODO, request);

            responsePut.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseBody = await responsePut.Content.ReadAsStringAsync();

            responseBody.Length.Should().BeGreaterThan(0);
            responseBody.Should().Contain("Código de barras inválido");
        }

        [Fact]
        public async void Validate_UpdateProduct_Error_BarCodeExist()
        {
            var request = new ProductRequest
            {
                cBarCode = "78937680",
                cName = "Chesterfield",
                cCategory = "Cigarro",
                nValue = 5.75m
            };
            var responsePost = await PostRequest(METODO, request);
            responsePost.StatusCode.Should().Be(HttpStatusCode.OK);

            request = new ProductRequest
            {
                cBarCode = "7891515256517",
                cName = "Chester",
                cCategory = "Frios",
                nValue = 5.75m
            };

            responsePost = await PostRequest(METODO, request);
            responsePost.StatusCode.Should().Be(HttpStatusCode.OK);

            request = new ProductRequest
            {
                cBarCode = "11201482633014",
                cName = "Peito de Chester",
                cCategory = "Frios",
                nValue = 5.75m
            };

            responsePost = await PostRequest(METODO, request);
            responsePost.StatusCode.Should().Be(HttpStatusCode.OK);

            request = new ProductRequestUpdate
            {
                Id = 2,
                cBarCode = "11201482633014",
                cName = "Chester",
                cCategory = "Frios",
                nValue = 5.75m
            };
            var responsePut = await PutRequest(METODO, request);

            responsePut.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseBody = await responsePut.Content.ReadAsStringAsync();

            responseBody.Length.Should().BeGreaterThan(0);
            responseBody.Should().Contain("Código de barras já existe");
        }
    }
}
