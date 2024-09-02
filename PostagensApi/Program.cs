
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PostagensApi.Endpoints;
using PostagensApi.Extensions;
using System;
using System.Security.Claims;
using System.Text;

namespace PostagensApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
     

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.AddArchitectures();
            builder.AddDataContexts();
            builder.AddDocumentation();
            builder.AddServices();
            builder.AddAuthentication();
       
  
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
                app.ConfigureDevEnvironment();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapEndPoints();

           
            app.Run();
        }
    }
}
