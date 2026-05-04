using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StatistikkApp.Services
{
    public class SsbService
    {
        private readonly HttpClient _httpClient;

        public SsbService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetPopulationData()
        {
            var url = "https://data.ssb.no/api/v0/no/table/11342/";

            var query = @"
            {
              ""query"": [
                {
                  ""code"": ""Region"",
                  ""selection"": {
                    ""filter"": ""item"",
                    ""values"": [""0301""]
                  }
                },
                {
                  ""code"": ""ContentsCode"",
                  ""selection"": {
                    ""filter"": ""item"",
                    ""values"": [""Folkemengde""]
                  }
                }
              ],
              ""response"": {
                ""format"": ""json""
              }
            }";

            var content = new StringContent(query, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            return await response.Content.ReadAsStringAsync();
        }
    }
}