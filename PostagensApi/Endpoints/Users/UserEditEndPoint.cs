using Microsoft.AspNetCore.Http;
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
        => app.MapPut("/{id}", HandleAsync)
            .WithName("User: Edit")
            .WithSummary("Edit a User")
            .WithDescription("Edit a User")
            .Produces<Response<Post>>()
            .RequireAuthorization();


        private static async Task<IResult> HandleAsync(
            IAccountInterface Interface,
            HttpContext httpContext,
            UserEditRequest request
            )
        {
            request.Id = long.Parse(httpContext.User.FindFirst("UserId").Value);

            var result = await Interface.UserEdit(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
