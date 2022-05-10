using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using OneCatalog.ElasticEngine.Sample.Models;

namespace OneCatalog.ElasticEngine.Sample.Controllers
{
    [Route("api/filling")]
    [ApiController]
    public class FillingController : ControllerBase
    {
        private readonly IElasticClient _elasticsearchClient;

        public FillingController(IElasticClient elasticsearchClient)
        {
            _elasticsearchClient = elasticsearchClient;
        }

        [HttpGet("products/{isNeeded:bool}")]
        public async Task PopulateDocumentData(bool isNeeded)
        {
            if (isNeeded is false) return;

            await _elasticsearchClient.Indices.CreateAsync("products", c => c
                .Map<Product>(m => m
                    .AutoMap<Category>()
                    )
                );

            var products = new List<Product>()
            {
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Пылесос",
                    Categories = new List<Category>()
                    {
                        new Category()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Техника",
                            Childs = new List<Category>()
                        }
                    },
                    Properties = new Dictionary<string, string>()
                    {
                        { "Размер", "30*40*180" },
                        { "Вес", "2kg" },
                        { "Цвет", "Белый" },
                        { "Мощность", "2500W" }
                    }
                }
            };

            var asyncBulkIndexResponse = await _elasticsearchClient.BulkAsync(b => b
                .Index("products")
                .IndexMany(products)
            );
        }
    }
}
