using Microsoft.AspNetCore.Http;
using PostagensApi.Extensions;
using PostagensApi.Models;
using PostagensApi.Requests.Post;
using PostagensApi.Response;
using PostagensApi.Services;

namespace PostagensApi.Endpoints.Posts
{
    public class DeletePostEndPoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Post: Delete")
            .WithSummary("Delete a Post")
            .WithDescription("Delete a Post")
            .WithOrder(2)
            .Produces<Response<Post?>>();

        private static async Task<IResult> HandleAsync(
            IPostInterface Interface,
            HttpContext httpContext,
            int id
            )
        {
            var request = new DeletePostRequest
            {
                Id = id,
                UserId = int.Parse(httpContext.User.FindFirst("UserId").Value)
            };

            var result = await Interface.DeletePostAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);



        }
    }
}
