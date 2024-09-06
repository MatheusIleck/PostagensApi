using System.ComponentModel.DataAnnotations;

namespace PostagensApi.Requests.User
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "A value is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "A value is required")]
        public string Password { get; set; } = string.Empty;
            
    }
}
