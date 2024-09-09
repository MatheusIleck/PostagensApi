﻿using PostagensApi.Dto;
using PostagensApi.Extensions;
using PostagensApi.Models;
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
            .WithSummary("Get all Post")
            .WithDescription("Get all Post")
            .WithOrder(5)
            .Produces<Response<List<PostDto?>>>();


        private static async Task<IResult> HandleAsync(
            IPostInterface Interface,
            HttpContext httpContext
            )
        {
            var request = new GetAllPostRequest()
            {
                UserId = int.Parse(httpContext.User.FindFirst("UserId").Value)

            };
            var response = await Interface.GetAllPostsAsync(request);

            return response.IsSuccess
                ? TypedResults.Ok(response)
                : TypedResults.BadRequest(response);
        }



    }
}
