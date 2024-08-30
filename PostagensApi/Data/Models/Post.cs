namespace PostagensApi.Data.Models
{
    public class Post
    {
        public long Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty ;

        public int AuthorId { get; set; }



    }
}
