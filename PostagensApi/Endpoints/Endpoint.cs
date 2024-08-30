using PostagensApi.Endpoints.Posts;
using PostagensApi.Extensions;
using PostagensApi.Requests.Post;

namespace PostagensApi.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndPoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("");

   

            endpoints.MapGroup("/")
                .WithTags("Health Check")
                .MapGet("/", () => new { message = "OK" });

            endpoints.MapGroup("v1/Posts")
                     .WithTags("Posts")
                     .MapEndpoint<CreatePostEndPoint>()
                     .MapEndpoint<GetPostByIdEndPoint>()
                     .MapEndpoint<GetAllPostEndPoint>()
                     .MapEndpoint<DeletePostEndPoint>()
                     .MapEndpoint<UpdatePostEndPoint>();







        }
        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
         where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app; ;
        }
    }
}
