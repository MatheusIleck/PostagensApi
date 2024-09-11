using PostagensApi.Extensions;
using PostagensApi.Models;
using PostagensApi.Requests.User;
using PostagensApi.Response;
using PostagensApi.Services;

namespace PostagensApi.Endpoints.Users
{
    public class UserRemoveEndPoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
      => app.MapDelete("/remove/{id}", HandleAsync)
            .WithName("User: Remove")
            .WithSummary("Remove a User")
            .WithDescription("Remove a User")
            .WithOrder(3)
            .Produces<Response<User>>()
            .RequireAuthorization(policy => policy.RequireRole("Admin"))
;


        private static async Task<IResult> HandleAsync(
            IAccountInterface Interface,
            long id
            )
        {
            var request = new UserRemoveRequest
            {
                id = id
            };
            var result = await Interface.UserRemove(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
