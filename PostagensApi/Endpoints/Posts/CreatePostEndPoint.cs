using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PostagensApi.Extensions;
using PostagensApi.Models;
using PostagensApi.Requests.Post;
using PostagensApi.Response;
using PostagensApi.Services;
using System.Net.Http;
using System.Security.Claims;

namespace PostagensApi.Endpoints.Posts
{
    public class CreatePostEndPoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        
        => app.MapPost("/", HandleAsync)
            .WithName("Post: Create")
            .WithSummary("Create a new Post")
            .WithDescription("Create a new Post")
            .WithOrder(1)
            .Produces<Response<Post?>>();

        private static async Task<IResult> HandleAsync(
            IPostInterface Interface,
        CreatePostRequest request,
            HttpContext httpContext
            )
        {
            request.UserId = long.Parse(httpContext.User.FindFirst("UserId").Value);
            var response = await Interface.CreatePostAsync(request);
            return response.IsSuccess
                ? TypedResults.Created($"v1/Post/{response.Data?.Id}",response)
                : TypedResults.BadRequest(response);

        }

       
    }
}
