using back.IRepository;
using back.IService;
using back.Models;
using back.Repository;
using back.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Neo4jClient;
using System.Text.Json;

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
            services.AddScoped<IRedisConnectionBuilder, RedisConnectionBuilder>();
            services.AddScoped<IRedisService, RedisService>();
            services.AddScoped<IRedisRepository, RedisRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "back", Version = "v1" });
            });

            var graphClient = new BoltGraphClient("bolt://localhost:7687", "neo4j", "adminadmin");
            graphClient.ConnectAsync();
            services.AddSingleton<IBoltGraphClient>(graphClient);
            services.AddScoped(typeof(IRentererRepository), typeof(RentererRepository));
            services.AddScoped(typeof(IRentererService), typeof(RentererService));
            services.AddScoped(typeof(IRenteeService), typeof(RenteeService));
            services.AddScoped(typeof(IRenteeRepository), typeof(RenteeRepository));
            services.AddScoped(typeof(IContestRepository), typeof(ContestRepository));
            services.AddScoped(typeof(ISonyRepository), typeof(SonyRepository));
            services.AddScoped(typeof(IGameRepository), typeof(GameRepository));
            services.AddScoped(typeof(ICommentRepository), typeof(CommentRepository));
            services.AddScoped(typeof(IFriendRequestRepository), typeof(FriendRequestRepository));
            services.AddScoped(typeof(IMessageRepository), typeof(MessageRepository));
            services.AddScoped(typeof(IForumRepository), typeof(ForumRepository));
            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            }).AddMvcOptions(options =>
            {
                options.EnableEndpointRouting = false;
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CORS", builder =>
                {
                    builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((host) => true)
                   .AllowCredentials();
                });
            });
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });
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

            app.UseCors("CORS");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("messenger");
            });
        }
    }
}
