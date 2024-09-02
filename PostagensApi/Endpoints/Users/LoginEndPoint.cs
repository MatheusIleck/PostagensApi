using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PostagensApi.Extensions;
using PostagensApi.Requests.User;
using PostagensApi.Services;

namespace PostagensApi.Endpoints.Users
{
    public class LoginEndPoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/login", Handle)
               .WithSummary("Users: Login")
               .WithName("Login")
               .Produces<string>();
        }

        private static IResult Handle(
            UserLoginRequest request,
            IAccountInterface userInterface
        )
        {
            
            var result = userInterface.Login(request);

            if (result != null)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest();
        }
    }


}
