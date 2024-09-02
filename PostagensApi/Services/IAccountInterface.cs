using PostagensApi.Data.Models;
using PostagensApi.Requests.User;

namespace PostagensApi.Services
{
    public interface IAccountInterface
    {
        string Login(UserLoginRequest request);

    }
}
