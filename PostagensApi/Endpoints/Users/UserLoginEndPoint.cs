using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PostagensApi.Extensions;
using PostagensApi.Requests.User;
using PostagensApi.Services;

namespace PostagensApi.Endpoints.Users
{
    public class UserLoginEndPoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/login", Handle)
               .WithName("User: Login")
               .WithSummary("Login a user")
               .WithDescription("Login a user")
               .WithOrder(2)
               .Produces<string>();
        }

        private static IResult Handle(
            UserLoginRequest request,
            IAccountInterface userInterface
        )
        {

            var result = userInterface.UserAuth(request);

            if (result != null)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest();
        }
    }


}
