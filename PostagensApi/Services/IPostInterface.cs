using PostagensApi.Dto;
using PostagensApi.Models;
using PostagensApi.Requests.Post;
using PostagensApi.Response;

namespace PostagensApi.Services
{

    public interface IPostInterface
    {
        Task<Response<Post?>> CreatePostAsync(CreatePostRequest request);

        Task<Response<PostDto?>> GetPostByIdAsync(GetPostByIdRequest request);

        Task<Response<List<PostDto>>> GetAllYourPostsAsync(GetAllYourPostRequest request);

        Task<Response<Post?>> UpdatePostAsync(UpdatePostRequest request);

        Task<Response<Post?>>DeletePostAsync(DeletePostRequest request);
        Task<Response<List<PostDto>>> GetAllPostAsync(GetAllPostRequest request);
    }
}
