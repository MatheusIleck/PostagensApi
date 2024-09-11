﻿using PostagensApi.Dto;
using PostagensApi.Extensions;
using PostagensApi.Models;
using PostagensApi.Requests.Post;
using PostagensApi.Response;
using PostagensApi.Services;


namespace PostagensApi.Endpoints.Posts
{
    public class GetAllYourPostEndPoint : IEndpoint
    {

        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Post: Get all your Posts")
            .WithSummary("Get all your Post")
            .WithDescription("Get all your Post")
            .WithOrder(5)
            .Produces<Response<List<PostDto?>>>();


        private static async Task<IResult> HandleAsync(
            IPostInterface Interface,
            HttpContext httpContext
            )
        {
            var request = new GetAllYourPostRequest()
            {
                UserId = int.Parse(httpContext.User.FindFirst("UserId").Value)

            };
            var response = await Interface.GetAllYourPostsAsync(request);

            return response.IsSuccess
                ? TypedResults.Ok(response)
                : TypedResults.BadRequest(response);
        }



    }
}
