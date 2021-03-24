using GraphQLHotChocolate.DAL;
using GraphQLHotChocolate.DAL.Context;
using GraphQLHotChocolate.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLHotChocolate
{
    public class Startup
    {
        private readonly string AllowedOrigin = "allowedOrigin";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GraphQLHotChocolate", Version = "v1" });
            });


            services.AddDbContext<SchoolDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("SchoolDbContext")));

            services.AddInMemorySubscriptions();

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>();

            services.AddScoped<StudentRepository, StudentRepository>();
            services.AddScoped<SchoolRepository, SchoolRepository>();

            services.AddCors(option => {
                option.AddPolicy("allowedOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                    );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GraphQLHotChocolate v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(AllowedOrigin);
            app.UseWebSockets();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGraphQL();
            });
        }
    }
}
