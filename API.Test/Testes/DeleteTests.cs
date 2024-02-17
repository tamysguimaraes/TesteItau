using API.Test.Requests;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace API.Test.Testes
{
    public class DeleteTests : ControllerBase
    {
        public DeleteTests(WebAppFactoryTest<Program> factory) : base(factory)
        {
        }

        const string METODO = "/api/Products";

        [Fact]
        public async void Validate_DeleteProduct_Success()
        {
            PopulateProducts();
            var responseDelete = await DeleteRequest($"{METODO}/1");
            responseDelete.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void Validate_DeleteProduct_Error()
        {
            PopulateProducts();
            var responseDelete = await DeleteRequest($"{METODO}/10");
            responseDelete.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseBody = await responseDelete.Content.ReadAsStringAsync();

            responseBody.Length.Should().BeGreaterThan(0);
            responseBody.Should().Contain("Produto não encontrado!");
        }
    }
}
