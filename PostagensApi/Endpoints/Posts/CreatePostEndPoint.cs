using Microsoft.AspNetCore.Authorization;
using PostagensApi.Data.Models;
using PostagensApi.Extensions;
using PostagensApi.Requests.Post;
using PostagensApi.Response;
using PostagensApi.Services;

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
            CreatePostRequest request
            )
        {
            request.UserId = 1;
            var response = await Interface.CreatePostAsync( request );
            return response.IsSuccess
                ? TypedResults.Created($"v1/Post/{response.Data?.Id}",response)
                : TypedResults.BadRequest(response);

        }

       
    }
}
