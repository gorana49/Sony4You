using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Neo4j.Driver;
using Microsoft.Extensions.Caching.StackExchangeRedis;

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
            // services.AddMvc().AddNewtonsoftJson(o => 
            // {
            //     o.JsonSerializerSetting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            // });   

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "back", Version = "v1" });
            });

            services.AddStackExchangeRedisCache(options => options.Configuration = this.Configuration.GetConnectionString("redisServerUrl"));
            services.AddSingleton(GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "adminadmin")) );
            // services.AddHttpsRedirection(options =>
            // {
            //     options.HttpsPort = 443;
            // });
            //services.AddSingleton(options=> options.Configuration = this.Configuration.Driver(this.Configuration.GetConnectionString("NeO4jConnectionSettings")));//, AuthTokens.Basic("neo4j", "adminadmin")));
        }
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
