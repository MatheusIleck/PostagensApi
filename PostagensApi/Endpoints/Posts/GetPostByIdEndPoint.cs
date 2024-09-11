using Microsoft.AspNetCore.Http;
using PostagensApi.Dto;
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
            .WithName("Post: Get By ID")
            .WithSummary("Get Post by ID")
            .WithDescription("Get Post by ID")
            .WithOrder(4)
            .Produces<Response<PostDto?>>();

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
            var response = await Interface.GetPostByIdAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                : TypedResults.BadRequest(response);
        }

   
    }
}
