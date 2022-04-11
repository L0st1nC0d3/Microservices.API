namespace Posts.Models
{
    public class Post : EditPost
    {
        public int Author { get; set; }
    }

    public class EditPost
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
