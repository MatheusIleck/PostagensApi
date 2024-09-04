using PostagensApi.Extensions;
using PostagensApi.Models;
using PostagensApi.Requests.User;
using PostagensApi.Response;
using PostagensApi.Services;

namespace PostagensApi.Endpoints.Users
{
    public class LikeAPostEndPoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/like", HandleAsync)
            .WithSummary("Like: Post")
            .WithName("Like a post")
            .WithDescription("Like a post")
            .Produces<Response<Like>>();

        private static async Task<IResult> HandleAsync(
            IAccountInterface Interface,
            HttpContext httpContext,
            LikeAPostRequest request)
        {
            var result = await Interface.LikeAPost(request);

            return result.IsSuccess
                ? TypedResults.Ok(request)
                : TypedResults.BadRequest(request);
        }



    }
}
