using Microsoft.AspNetCore.Mvc;
using Nest;
using OneCatalog.ElasticEngine.Sample.Models;

namespace OneCatalog.ElasticEngine.Sample.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IElasticClient _elasticsearchClient;

        public SearchController(IElasticClient elasticsearchClient)
        {
            _elasticsearchClient = elasticsearchClient;
        }

        [HttpGet("products")]
        public async Task<IEnumerable<Product>> SearchProducts()
        {
            var searchResponse = await _elasticsearchClient.SearchAsync<Product>(s => s.MatchAll());

            if (!searchResponse.IsValid)
            {
                // Handle errors
                var debugInfo = searchResponse.DebugInformation;
                var error = searchResponse.ServerError?.Error;

                Console.WriteLine(debugInfo);
                Console.WriteLine(error);
            }

            var products = searchResponse.Documents;

            return products;
        }
    }
}
