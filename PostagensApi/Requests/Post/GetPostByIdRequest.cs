using System.ComponentModel.DataAnnotations;

namespace PostagensApi.Requests.Post
{
    public class GetPostByIdRequest : Request
    {
        [Required]
        public int Id { get; set; }
    }
}
