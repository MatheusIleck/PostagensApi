using Microsoft.AspNetCore.Http;
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
            .WithSummary("Update a post")
            .WithDescription("Update a post")
            .WithOrder(3)
            .Produces<Response<Post?>>();

        public static async Task<IResult> HandleAsync(
            IPostInterface Interface,
            UpdatePostRequest request,
            HttpContext httpContext,
        int id
     )
        {
            request.UserId = int.Parse(httpContext.User.FindFirst("UserId").Value);
            ;

            request.Id = id;

            var result = await Interface.UpdatePostAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
