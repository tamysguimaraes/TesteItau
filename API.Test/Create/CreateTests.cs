using API.Test.Requests;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace API.Test.Create
{
    public class CreateTests : ControllerBase
    {
       
        public CreateTests(WebAppFactoryTest<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async void Validate_CreateProduct_Success()
        {
            var request = new ProductRequest
            {
                cBarCode = "78937680",
                cName = "Chesterfield",
                cCategory = "Cigarro",
                nValue = 5.75m
            };
            var response = await PostRequest("/api/Products", request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            await using var resposeBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(resposeBody);
            responseData.Should().NotBeNull();
        }

        [Fact]
        public async void Validate_CreateProduct_Fail_InvalidBarCode()
        {
            var request = new ProductRequest
            {
                cBarCode = "12345678",
                cName = "Chesterfield",
                cCategory = "Cigarro",
                nValue = 5.75m
            };
            var response = await PostRequest("/api/Products", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
