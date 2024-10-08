﻿using PostagensApi.Extensions;
using PostagensApi.Models;
using PostagensApi.Requests.User;
using PostagensApi.Response;
using PostagensApi.Services;

namespace PostagensApi.Endpoints.Users
{
    public class LikeAPostEndPoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/like", HandleAsync)
            .WithName("Like")
            .WithSummary("Like a Post")
            .WithDescription("Like a Post")
            .WithOrder(4)
            .Produces<Response<Like>>()
            .RequireAuthorization();

        private static async Task<IResult> HandleAsync(
            IAccountInterface Interface,
            HttpContext httpContext,
            LikeAPostRequest request)
        {
            var response = new LikeAPostRequest
            {
                postId = request.postId,
                UserId = long.Parse(httpContext.User.FindFirst("UserId").Value)
            };
            var result = await Interface.LikeAPost(request);

            return result.IsSuccess
                ? TypedResults.Ok(request)
                : TypedResults.BadRequest(request);
        }



    }
}
