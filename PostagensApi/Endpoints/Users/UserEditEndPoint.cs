using PostagensApi.Extensions;
using PostagensApi.Models;
using PostagensApi.Requests.User;
using PostagensApi.Response;
using PostagensApi.Services;

namespace PostagensApi.Endpoints.Users
{
    public class UserEditEndPoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/user", HandleAsync)
            .WithName("User: Edit")
            .WithSummary("Edit a user")
            .WithDescription("Edit a user")
            .Produces<Response<Post>>()
            .RequireAuthorization();


        private static async Task<IResult> HandleAsync(
            IAccountInterface Interface,
            UserEditRequest request
            )
        {
            var result = await Interface.UserEdit(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
