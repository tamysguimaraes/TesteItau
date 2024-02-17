using API.Test.Requests;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using Xunit;

namespace API.Test
{
    public class ControllerBase : IClassFixture<WebAppFactoryTest<Program>>
    {
        private readonly HttpClient _client;
        public ControllerBase(WebAppFactoryTest<Program> factory)
        {
            _client = factory.CreateClient();
            
        }

        protected async Task<HttpResponseMessage> PostRequest(string metodo,object body)
        {
            var jsonString = JsonConvert.SerializeObject(body);
            return await _client.PostAsync(metodo, new StringContent(jsonString,Encoding.UTF8,"application/json"));
        }

        protected async Task<HttpResponseMessage> PutRequest(string metodo, object body)
        {
            var jsonString = JsonConvert.SerializeObject(body);

            return await _client.PutAsync(metodo, new StringContent(jsonString, Encoding.UTF8, "application/json"));
        }

        protected async Task<HttpResponseMessage> GetRequest(string metodo)
        {
            return await _client.GetAsync(metodo);
        }

        protected async Task<HttpResponseMessage> DeleteRequest(string metodo)
        {
            return await _client.DeleteAsync(metodo);
        }

        protected async void PopulateProducts()
        {
            List<ProductRequest> listProducts = new List<ProductRequest>();
            listProducts.Add(new ProductRequest
            {
                cBarCode = "711719547266",
                cName = "GOW Ragnarok",
                cCategory = "Jogo PS4",
                nValue = 299.90m
            });
            listProducts.Add(new ProductRequest
            {
                cBarCode = "711719506058",
                cName = "Death Stranding",
                cCategory = "Jogo PS4",
                nValue = 299.90m
            });
            listProducts.Add(new ProductRequest
            {
                cBarCode = "711719526377",
                cName = "Last Of Us",
                cCategory = "Jogo PS4",
                nValue = 299.90m
            });
            foreach (var request in listProducts)
            {
                var responsePost = await PostRequest("/api/Products", request);
                responsePost.StatusCode.Should().Be(HttpStatusCode.OK);
            }
            
        }
    }
}
