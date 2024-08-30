using PostagensApi.Data.Models;
using PostagensApi.Extensions;
using PostagensApi.Requests.Post;
using PostagensApi.Response;
using PostagensApi.Services;

namespace PostagensApi.Endpoints.Posts
{
    public class GetAllPostEndPoint : IEndpoint
    {

        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Post: Get All")
            .WithSummary("Get all post")
            .WithDescription("Get all post")
            .WithOrder(5)
            .Produces<Response<List<Post?>>>();


        private static async Task<IResult> HandleAsync(
            IPostInterface Interface,
            int Id
            )
        {
            var request = new GetAllPostRequest()
            {
                UserId = Id,

            };
            var response = await Interface.GetAllPostsAsync(request);

            return response.IsSuccess
                ? TypedResults.Ok(response)
                : TypedResults.BadRequest(response);
        }



    }
}
