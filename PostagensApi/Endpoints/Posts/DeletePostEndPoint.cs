using PostagensApi.Data.Models;
using PostagensApi.Extensions;
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
            .WithSummary("Delete a post")
            .WithDescription("Delete a post")
            .WithOrder(2)
            .Produces<Response<Post?>>();

        private static async Task<IResult> HandleAsync(
            IPostInterface Interface,
            int id
            )
        {
            var request = new DeletePostRequest
            {
                Id = id,
                UserId = 1
            };

            var result = await Interface.DeletePostAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);



        }
    }
}
