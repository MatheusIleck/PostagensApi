using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PostagensApi.Dto;
using PostagensApi.Models;
using PostagensApi.Requests.Post;
using PostagensApi.Response;
using System.Xml.Linq;

namespace PostagensApi.Services
{
    public class PostService(db_SocialContext _db) : IPostInterface
    {

        public async Task<Response<Post?>> CreatePostAsync(CreatePostRequest request)
        {

            var user = _db.Users.FirstOrDefault(x => x.Id == request.UserId);
            var post = new Post()
            {
                Title = request.Title,
                Description = request.description,
                UserId = request.UserId,
            };
            try
            {
                await _db.Posts.AddAsync(post);
                await _db.SaveChangesAsync();

                return new Response<Post?>(post, 201, "Post published successfully!");
            }
            catch (Exception ex)
            {
                return new Response<Post?>(null, 500, ex.Message);
            }
        }

        public async Task<Response<Post?>> DeletePostAsync(DeletePostRequest request)
        {
            try
            {

                var post = await _db.Posts.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
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

        public async Task<Response<List<PostDto>>> GetAllPostAsync(GetAllPostRequest request)
        {
            try
            {
                var posts = await _db.Posts.Include(p => p.Comments)
                    .ThenInclude(u => u.User)
                    .Include(u => u.User)
                    .Include(p => p.Likes)
                    .ToListAsync();

                var paginatedPosts = posts.Select(x => new PostDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    userName = x.User.Name,
                    Likes = x.Likes.Count(),
                    Comments = x.Comments.Select(c=> new CommentDto
                    {
                        Name = c.User.Name,
                        Comment = c.Comment1
                    }).ToList()
                })
                    .Skip(request.pageIndex * request.pageSize)
                  .Take(request.pageSize)
                  .ToList();

                return new Response<List<PostDto>>(paginatedPosts, 200, "Sucesso");
            }
            catch(Exception ex) 
            {
                return new Response<List<PostDto>>(null, 500, ex.Message);
            }
        }

        public async Task<Response<List<PostDto>>> GetAllYourPostsAsync(GetAllYourPostRequest request)
        {
            try
            {
                var posts = await _db.Posts
                    .Where(x => x.UserId == request.UserId)
                    .Include(p => p.Comments)
                    .ThenInclude(uc => uc.User)
                    .Include(p => p.Likes)
                    .Include(u=> u.User)
                    .ToListAsync();


                var postDto = posts.Select(x => new PostDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    userName = x.User.Name,
                    Likes = x.Likes.Count(),
                    Comments = x.Comments.Select(c => new CommentDto
                    {
                        Name = c.User.Name,
                        Comment = c.Comment1
                    }).ToList()
                }).ToList();

                if (postDto == null || !posts.Any())
                    return new Response<List<PostDto>>(null, 204, "No posts found");

                return new Response<List<PostDto>>(postDto, 200, "Posts listed");
            }
            catch (Exception ex)
            {
                return new Response<List<PostDto>>(null, 400, ex.Message);
            }
        }

        public async Task<Response<PostDto?>> GetPostByIdAsync(GetPostByIdRequest request)
        {
            try
            {

                var posts = await _db.Posts
                 .Include(p => p.Likes)
                 .Include(p => p.Comments)
                 .ThenInclude(c => c.User)
                 .Where(p => p.Id == request.Id)
                 .ToListAsync();

                var postDto = posts.Select(p => new PostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    userName = p.User.Name,
                    Likes = p.Likes.Count(),
                    Comments = p.Comments.Select(c => new CommentDto
                    {
                        Name = c.User.Name,
                        Comment = c.Comment1
                    }).ToList()
                }).FirstOrDefault();

                if (postDto == null)
                {
                    return new Response<PostDto?>(null, 204, "Post not found");
                }

                return new Response<PostDto?>(postDto, 200, "The post was found");
            }
            catch (Exception ex)
            {
                return new Response<PostDto?>(null, 400, ex.Message);
            }
        }

        public async Task<Response<PostDto?>> UpdatePostAsync(UpdatePostRequest request)
        {
            try
            {
                var findPost = _db.Posts
                    .Include(u => u.User)
                    .FirstOrDefault(x => x.Id == request.Id && x.UserId == request.UserId);

                if (findPost.UserId == request.UserId)
                {
                    findPost.Title = request.Title;
                    findPost.Description = request.Description;

                    _db.Posts.Update(findPost);
                    await _db.SaveChangesAsync();


                    PostDto postDto = new PostDto
                    {
                        Title = findPost.Title,
                        Description = findPost.Description,
                        Id = findPost.Id,
                        userName = findPost.User.Name,
                        Likes = findPost.Likes.Count(),
                    };

                    return new Response<PostDto?>(postDto, 200, "Post updated successfully!");
                }

                else
                    return new Response<PostDto?>(null, 204, "The post could not be updated");



            }
            catch
            {
                return new Response<PostDto?>(null, 500, "Inválid request");
            }
        }

    }
}
