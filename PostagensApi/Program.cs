
using PostagensApi.Endpoints;
using PostagensApi.Extensions;

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

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
                app.ConfigureDevEnvironment();

            app.MapEndPoints();

            app.Run();
        }
    }
}
