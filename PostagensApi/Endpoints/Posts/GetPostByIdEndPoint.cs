using Microsoft.AspNetCore.Http;
using PostagensApi.Extensions;
using PostagensApi.Models;
using PostagensApi.Requests.Post;
using PostagensApi.Response;
using PostagensApi.Services;

namespace PostagensApi.Endpoints.Posts
{
    public class GetPostByIdEndPoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Post: Get By Id")
            .WithSummary("Get post by id")
            .WithDescription("Get post by id")
            .WithOrder(4)
            .Produces<Response<Post?>>();

        private static async Task<IResult> HandleAsync(
            IPostInterface Interface,
            HttpContext httpContext,
            int id
            )
        {
            var request = new GetPostByIdRequest
            {
                Id = id,
                UserId = int.Parse(httpContext.User.FindFirst("UserId").Value),

            };
            var response = await Interface.GetPostById(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                : TypedResults.BadRequest(response);
        }

   
    }
}
