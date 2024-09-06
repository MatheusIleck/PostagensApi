using System.ComponentModel.DataAnnotations;

namespace PostagensApi.Requests.User
{
    public class UserEditRequest : Request
    {
        [Required(ErrorMessage = "A value is required")]
        public long Id { get; set; }

        [Required(ErrorMessage = "A value is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "A value is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A value is required")]
        public string Password { get; set; } = string.Empty;
    }
}
