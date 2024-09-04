using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PostagensApi.Data;
using PostagensApi.Models;
using PostagensApi.Requests.Post;
using PostagensApi.Response;

namespace PostagensApi.Services
{
    public class PostService(AppDbContext _db) : IPostInterface
    {

        public async Task<Response<Post?>> CreatePostAsync(CreatePostRequest request)
        {
            var post = new Post()
            {
                Title = request.Title,
                Description = request.description,
                AuthorId = request.UserId,
            };
            try
            {
                await _db.Post.AddAsync(post);
                await _db.SaveChangesAsync();

                return new Response<Post?>(post, 201, "Post criado com sucesso!");
            }
            catch
            {
                return new Response<Post?>(null, 500, "Não foi possivel criar o post");
            }
        }

        public async Task<Response<Post?>> DeletePostAsync(DeletePostRequest request)
        {
            try
            {

                var post = await _db.Post.FirstOrDefaultAsync(x => x.Id == request.Id && x.AuthorId == request.UserId);
                if (post == null)
                    return new Response<Post?>(null, 204, "Post não encontrado");


                _db.Post.Remove(post);
                await _db.SaveChangesAsync();

                return new Response<Post?>(post, 200, "Post removido");


            }
            catch
            {
                return new Response<Post?>(null, 500, "Solicitação Inválida");
            }
        }

        public async Task<Response<List<Post>>> GetAllPostsAsync(GetAllPostRequest request)
        {
            try
            {
                var posts = await _db.Post
                    .AsNoTracking()
                    .Where(x => x.AuthorId == request.UserId)
                    .OrderBy(x => x.Title).ToListAsync();

     

                if (posts == null)
                    return new Response<List<Post>>(null, 204, "Nenhum post encontrado");

                return new Response<List<Post>>(posts, 200, "Posts listados");



            }
            catch
            {
                return new Response<List<Post>>(null, 400, "Solicitação Inválida");
            }
        }

        public async Task<Response<Post?>> GetPostById(GetPostByIdRequest request)
        {
            try
            {
                var post = await _db.Post.FirstAsync(x => x.Id == request.Id);

                if (post == null)
                    return new Response<Post?>(null, 204, "Post não encontrado");

                return new Response<Post?>(post, 302, "Post localizado");
            }
            catch
            {
                return new Response<Post?>(null, 400, "Solicitação inválida");
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
                    return new Response<Post?>(null, 204, "Não foi possivel atualizar o post");

                _db.Post.Update(post);
                await _db.SaveChangesAsync();


                return new Response<Post?>(post, 200, "Post atualizado com sucesso!");


            }
            catch
            {
                return new Response<Post?>(null, 500, "Solicitação Inválida");
            }
        }
    }
}
