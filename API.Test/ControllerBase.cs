using Newtonsoft.Json;
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
    }
}
