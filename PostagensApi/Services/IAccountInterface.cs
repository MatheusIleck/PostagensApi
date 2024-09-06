using PostagensApi.Models;
using PostagensApi.Requests.User;
using PostagensApi.Response;

namespace PostagensApi.Services
{
    public interface IAccountInterface
    {
        string UserAuth(UserLoginRequest request);

        Task<Response<User>> UserRegister(UserRegisterRequest request);

        Task<Response<Like>> LikeAPost(LikeAPostRequest request);

        Task<Response<User>> UserRemove(UserRemoveRequest request);

        Task<Response<User>> UserEdit(UserEditRequest request);

    }
}
