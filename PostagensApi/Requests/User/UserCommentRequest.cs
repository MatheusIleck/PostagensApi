namespace PostagensApi.Requests.User
{
    public class UserCommentRequest : Request
    {

        public long idPost { get; set; }

        public string Comment { get; set; } = string.Empty;
    }
}
