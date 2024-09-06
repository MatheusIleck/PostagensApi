using System.ComponentModel.DataAnnotations;

namespace PostagensApi.Requests.User
{
    public class LikeAPostRequest : Request
    {
        [Required(ErrorMessage = "A value is required")]
        public int postId { get; set; }
    }
}
