using PostagensApi.Extensions;
using PostagensApi.Models;
using PostagensApi.Requests.User;
using PostagensApi.Response;
using PostagensApi.Services;

namespace PostagensApi.Endpoints.Users
{
    public class RegisterEndPoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("User: Create")
            .WithSummary("Create a new User")
            .WithDescription("Create a new User")
            .WithOrder(1)
            .Produces<Response<User>>();


        private static async Task<IResult> HandleAsync(
            IAccountInterface Interface,
            UserRegisterRequest request
            )
        {
            var result = await Interface.Register(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);

        }
    }
}
