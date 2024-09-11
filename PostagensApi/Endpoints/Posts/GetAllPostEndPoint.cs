using Microsoft.AspNetCore.Mvc;
using PostagensApi.Dto;
using PostagensApi.Extensions;
using PostagensApi.Requests.Post;
using PostagensApi.Response;
using PostagensApi.Services;

namespace PostagensApi.Endpoints.Posts
{
    public class GetAllPostEndPoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/Feed", HandleAsync)
            .WithName("Post: All Post")
            .WithSummary("Get All Post")
            .WithDescription("Get All Post")
            .Produces<Response<List<PostDto>>>();

        private static async Task<IResult> HandleAsync(
            [FromBody]
            GetAllPostRequest request, 
            IPostInterface Interface
            )
        {
            var response = new GetAllPostRequest
            {
                pageIndex = request.pageIndex,
                pageSize = request.pageSize,
            };

            var result = await Interface.GetAllPostAsync(response);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
