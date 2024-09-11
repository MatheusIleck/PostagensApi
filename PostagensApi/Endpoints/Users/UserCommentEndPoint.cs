using PostagensApi.Extensions;
using PostagensApi.Models;
using PostagensApi.Requests.User;
using PostagensApi.Response;
using PostagensApi.Services;

namespace PostagensApi.Endpoints.Users
{
    public class UserCommentEndPoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/comment", HandleAsync)
            .WithName("User: Comment")
            .WithSummary("Commenting on a post")
            .RequireAuthorization()
            .Produces<Response<Comment>>();

        private static async Task<IResult> HandleAsync(
            HttpContext httpContext,
            UserCommentRequest request,
            IAccountInterface Interface
            )
        {

            var response = new UserCommentRequest
            {
                UserId = long.Parse(httpContext.User.FindFirst("UserId").Value),
                Comment = request.Comment,
                idPost = request.idPost
            };

            var result = await Interface.UserComment(response);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
