using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PostagensApi.Dto;
using PostagensApi.Models;
using PostagensApi.Requests.Post;
using PostagensApi.Response;

namespace PostagensApi.Services
{
    public class PostService(db_SocialContext _db) : IPostInterface
    {

        public async Task<Response<Post?>> CreatePostAsync(CreatePostRequest request)
        {

            var user = _db.Users.FirstOrDefault(x=> x.Id == request.UserId);
            var post = new Post()
            {
                Title = request.Title,
                Description = request.description,
                AuthorId = request.UserId,
                User = user,
            };
            try
            {
                await _db.Posts.AddAsync(post);
                await _db.SaveChangesAsync();

                return new Response<Post?>(post, 201, "Post published successfully!");
            }
            catch
            {
                return new Response<Post?>(null, 500, "Post creation failed");
            }
        }

        public async Task<Response<Post?>> DeletePostAsync(DeletePostRequest request)
        {
            try
            {

                var post = await _db.Posts.FirstOrDefaultAsync(x => x.Id == request.Id && x.AuthorId == request.UserId);
                if (post == null)
                    return new Response<Post?>(null, 204, "Post not found");


                _db.Posts.Remove(post);
                await _db.SaveChangesAsync();

                return new Response<Post?>(post, 200, "Post removed");


            }
            catch
            {
                return new Response<Post?>(null, 500, "Invalid request");
            }
        }

        public async Task<Response<List<PostDto>>> GetAllPostsAsync(GetAllPostRequest request)
        {
            try
            {
                var posts = await _db.Posts
                    .Where(x => x.AuthorId == request.UserId)
                    .Include(p => p.Likes).Select(p => new PostDto
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        AuthorId = p.AuthorId,
                        Likes = p.Likes.Count()
                    })
                    .ToListAsync(); 


                if (posts == null || !posts.Any())
                    return new Response<List<PostDto>>(null, 204, "No posts found");

                return new Response<List<PostDto>>(posts, 200, "Posts listed");
            }
            catch (Exception ex)
            {
                return new Response<List<PostDto>>(null, 400, "Something went wrong");
            }
        }

        public async Task<Response<PostDto?>> GetPostById(GetPostByIdRequest request)
        {
            try
            {

                var postss = await _db.Posts
                     .Include(p => p.Likes)
                     .Select(p => new PostDto
                     {
                         Id = p.Id,
                         Title = p.Title,
                         Description = p.Description,
                         AuthorId = p.AuthorId,
                         Likes = p.Likes.Count()
                     })
                     .FirstOrDefaultAsync(p => p.Id == request.Id);

                if (postss == null)
                {
                    return new Response<PostDto?>(null, 204, "Post not found");
                }

                return new Response<PostDto?>(postss, 200, "The post was found"); // Use 200 for success
            }
            catch (Exception ex)
            {
                return new Response<PostDto?>(null, 400, ex.Message);
            }
        }

        public async Task<Response<Post?>> UpdatePostAsync(UpdatePostRequest request)
        {
            try
            {
                var post = new Post
                {
                    AuthorId = request.UserId,
                    Id = request.Id,
                    Title = request.Title,
                    Description = request.Description

                };
                if (post == null)
                    return new Response<Post?>(null, 204, "The post could not be updated");

                _db.Posts.Update(post);
                await _db.SaveChangesAsync();


                return new Response<Post?>(post, 200, "Post updated successfully!");


            }
            catch
            {
                return new Response<Post?>(null, 500, "Inválid request");
            }
        }
    }
}
