using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
<<<<<<< HEAD
using Neo4jClient;
=======
using Neo4j.Driver;
using StackExchange.Redis;

>>>>>>> 3cf367ded8d3613485912aa0cc49935c77e61a3e
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
<<<<<<< HEAD
            services.AddStackExchangeRedisCache(options => options.Configuration = this.Configuration.GetConnectionString("redisServerUrl"));
            var neo4jclient = new GraphClient(new System.Uri("bolt://localhost:7687"), "neo4j", "adminadmin");
            neo4jclient.ConnectAsync();
            services.AddSingleton<IGraphClient>(neo4jclient);
            //services.AddSingleton(GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "adminadmin")));
=======
            //services.AddStackExchangeRedisCache(options => options.Configuration = this.Configuration.GetConnectionString("redisServerUrl"));
            services.AddSingleton<IRedisService, RedisService>();
            services.AddSingleton(GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "adminadmin")));

            
>>>>>>> 3cf367ded8d3613485912aa0cc49935c77e61a3e
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
