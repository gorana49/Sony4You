using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Neo4jClient;
using StackExchange.Redis;

namespace back
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IConnectionMultiplexer>(x =>
            ConnectionMultiplexer.Connect(Configuration.GetValue<string>("ConnectionStrings:redisServerUrl")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "back", Version = "v1" });
            });
            services.AddStackExchangeRedisCache(options => options.Configuration = this.Configuration.GetConnectionString("redisServerUrl"));
            var neo4jclient = new GraphClient(new System.Uri("bolt://localhost:7687"), "neo4j", "adminadmin");
            neo4jclient.ConnectAsync();
            services.AddSingleton<IGraphClient>(neo4jclient);
            services.AddSingleton<IRedisService, RedisService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "back v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
