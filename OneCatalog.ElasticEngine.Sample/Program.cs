using Elasticsearch.Net;
using Nest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var pool = new SingleNodeConnectionPool(new Uri("https://80.78.248.251:9200"));

var settings = new ConnectionSettings(pool)
    .DefaultIndex("products")
    .CertificateFingerprint("A6:EE:85:FB:44:AC:0E:B0:88:79:E5:98:7F:A0:96:4D:34:60:B1:DE:69:22:30:AE:82:66:C7:54:7F:7E:65:F2")
    .BasicAuthentication("elastic", "p@ssw0rd")
    .EnableApiVersioningHeader();

var client = new ElasticClient(settings);
builder.Services.AddSingleton<IElasticClient>(client);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
