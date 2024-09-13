using Microsoft.AspNetCore.Http;
using PostagensApi.Dto;
using PostagensApi.Extensions;
using PostagensApi.Models;
using PostagensApi.Requests.Post;
using PostagensApi.Response;
using PostagensApi.Services;

namespace PostagensApi.Endpoints.Posts
{
    public class UpdatePostEndPoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Post: Update")
            .WithSummary("Update a Post")
            .WithDescription("Update a Post")
            .WithOrder(3)
            .Produces<Response<PostDto?>>();

        public static async Task<IResult> HandleAsync(
            IPostInterface Interface,
            UpdatePostRequest request,
            HttpContext httpContext,
        int id
     )
        {

            var response = new UpdatePostRequest
            {
                UserId = long.Parse(httpContext.User.FindFirst("UserId").Value),
                Id = id,
                Title = request.Title,
                Description = request.Description,
            };
 

            var result = await Interface.UpdatePostAsync(response);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
