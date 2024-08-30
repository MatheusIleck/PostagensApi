using Microsoft.EntityFrameworkCore.Infrastructure;
using PostagensApi.Data.Models;

namespace PostagensApi.Requests.Post
{
    public class GetAllPostRequest : Request
    {
        public List<Data.Models.Post> Posts { get; set; } = new List<Data.Models.Post>();
    }
}
