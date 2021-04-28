using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrickingLibrary.Data;

namespace TrickingLibrary.API
{
    public class Startup
    {
        private const string Policy = "All";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("Dev"));
            services.AddCors(options => options.AddPolicy(Policy, builder =>
                builder.AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //cors = cross origin resource sharing , enables web-client to call api
            app.UseCors(Policy);

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                //.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            });
        }
    }
}