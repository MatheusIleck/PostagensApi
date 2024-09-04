using PostagensApi.Models;
using PostagensApi.Requests.User;
using PostagensApi.Response;

namespace PostagensApi.Services
{
    public interface IAccountInterface
    {
        string Login(UserLoginRequest request);

        Task<Response<User>> Register(UserRegisterRequest request);

        Task<Response<Like>> LikeAPost(LikeAPostRequest request);

    }
}
