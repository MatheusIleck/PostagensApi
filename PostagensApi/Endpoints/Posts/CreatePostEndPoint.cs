using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PostagensApi.Dto;
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
        
        => app.MapPost("/create", HandleAsync)
            .WithName("Post: Create")
            .WithSummary("Create a new Post")
            .WithDescription("Create a new Post")
            .WithOrder(1)
            .Produces<Response<PostDto?>>();

        private static async Task<IResult> HandleAsync(
            IPostInterface Interface,
            CreatePostRequest request,
            HttpContext httpContext
            )
        {
            request.UserId = long.Parse(httpContext.User.FindFirst("UserId").Value);

            var response = new CreatePostRequest { 
                UserId = request.UserId,
                description = request.description,
                Title = request.Title
            };

            var result = await Interface.CreatePostAsync(response);
            return result.IsSuccess
                ? TypedResults.Created($"v1/Post/{result.Data?.Id}",response)
                : TypedResults.BadRequest(response);

        }

       
    }
}
