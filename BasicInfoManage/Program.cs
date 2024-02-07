using BasicInfoManage.Application;
using BasicInfoManage.Application.Interfaces;
using BasicInfoManage.Application.Services;
using BasicInfoManage.Data;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;

namespace BasicInfoManage
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddMemoryCache();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApiVersioning(setupAction =>
            {
                setupAction.AssumeDefaultVersionWhenUnspecified = true;
                setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                setupAction.ReportApiVersions = true;
            });

            builder.Services.AddDbContext<CountryInfoContext>(c =>
                c.UseInMemoryDatabase("CountryInfo"));

            builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
            builder.Services.AddTransient<ICountryInfoService, CountryInfoService>();
            
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            /**/

            var app = builder.Build();
            app.Logger.LogInformation("PublicApi App created...");
            app.Logger.LogInformation("Seeding Database...");
            
            using (var scope = app.Services.CreateScope())
            {
                var scopedProvider = scope.ServiceProvider;
                try
                {
                    var countryinfoContext = scopedProvider.GetRequiredService<CountryInfoContext>();
                    CountryInfoContextSeed.SeedAsync(countryinfoContext, app.Logger);
                }
                catch (Exception ex)
                {
                    app.Logger.LogError(ex, "An error occurred seeding the Database");
                }
            }
            /**/
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
           
            /**/
            app.MapControllers();

            app.Run();
        }
    }
}