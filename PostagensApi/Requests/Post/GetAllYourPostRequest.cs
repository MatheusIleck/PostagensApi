using Microsoft.EntityFrameworkCore.Infrastructure;
using PostagensApi.Models;

namespace PostagensApi.Requests.Post
{
    public class GetAllYourPostRequest : Request
    {
        public List<Models.Post> Posts { get; set; } = new List<Models.Post>();
    }
}
