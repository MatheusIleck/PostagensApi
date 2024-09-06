using Microsoft.AspNetCore.Authorization;
using PostagensApi.Endpoints.Posts;
using PostagensApi.Endpoints.Users;
using PostagensApi.Extensions;
using PostagensApi.Requests.Post;
using System.Security.Claims;

namespace PostagensApi.Endpoints
{
    [Authorize]
    public static class Endpoint
    {
        public static void MapEndPoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("");




            endpoints.MapGroup("/")
                .WithTags("Health Check")
                .MapGet("/", () => new { message = "OK" });

            endpoints.MapGroup("v1/Posts")
                     .RequireAuthorization()
                     .WithTags("Posts")
                     .MapEndpoint<CreatePostEndPoint>()
                     .MapEndpoint<GetPostByIdEndPoint>()
                     .MapEndpoint<GetAllPostEndPoint>()
                     .MapEndpoint<DeletePostEndPoint>()
                     .MapEndpoint<UpdatePostEndPoint>();

            endpoints.MapGroup("v1/User")
                .WithTags("Users")
                .MapEndpoint<UserLoginEndPoint>()
                .MapEndpoint<UserRegisterEndPoint>()
                .MapEndpoint<LikeAPostEndPoint>()
                .MapEndpoint<UserRemoveEndPoint>()
                .MapEndpoint<UserEditEndPoint>();





        }
        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
         where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app; ;
        }
    }
}
