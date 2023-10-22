using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc.Server;
using VolleyData.Server.Data;
using VolleyData.Server.GrpcServices;

namespace VolleyData.Server
{
    public class Startup
    {
        private readonly string CorsPolicy = "CorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy,
                    builder =>
                    {
                        builder
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .WithOrigins("https://localhost:5002");
                    });
            });
            services.AddGrpc();
            services.AddCodeFirstGrpc(config => { config.ResponseCompressionLevel = CompressionLevel.Optimal; });
            services.AddDbContext<ToDoDbContext>(options => options.UseInMemoryDatabase("ToDoDatabase"));
            //services.AddSqlite<ToDoDbContext>("Data Source=app.db");
            //services.AddDbContext<ToDoDbContext>(options => options.UseSqlite());
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors(CorsPolicy);

            app.UseRouting();

            app.UseGrpcWeb();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<ToDoService>().EnableGrpcWeb();
                endpoints.MapGrpcService<TimeService>().EnableGrpcWeb();
            });
        }
    }
}
