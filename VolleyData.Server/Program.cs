using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc.Server;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using VolleyData.Server.Data;
using VolleyData.Server.GrpcServices;


var _corsPolicy = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(_corsPolicy,
        builder =>
        {
            builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("https://localhost:7085");
        });
});
builder.Services.AddGrpc();
builder.Services.AddCodeFirstGrpc(config => { config.ResponseCompressionLevel = CompressionLevel.Optimal; });
builder.Services.AddSqlite<ToDoDbContext>("Data Source=app.db");
builder.Services.AddDbContext<ToDoDbContext>(options => options.UseSqlite());
builder.Services.AddControllers();

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseCors(_corsPolicy);
app.UseRouting();
app.UseGrpcWeb();
app.UseAuthorization();
app.MapControllers();
app.MapGrpcService<ToDoService>().EnableGrpcWeb();
app.MapGrpcService<TimeService>().EnableGrpcWeb();
app.MapFallbackToFile("index.html");


app.Run("https://localhost:7298");
