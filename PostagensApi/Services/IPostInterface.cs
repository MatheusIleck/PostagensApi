using PostagensApi.Models;
using PostagensApi.Requests.Post;
using PostagensApi.Response;

namespace PostagensApi.Services
{

    public interface IPostInterface
    {
        Task<Response<Post?>> CreatePostAsync(CreatePostRequest request);

        Task<Response<Post?>> GetPostById(GetPostByIdRequest request);

        Task<Response<List<Post>>> GetAllPostsAsync(GetAllPostRequest request);

        Task<Response<Post?>> UpdatePostAsync(UpdatePostRequest request);

        Task<Response<Post?>>DeletePostAsync(DeletePostRequest request);

    }
}
