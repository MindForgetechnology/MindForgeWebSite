namespace MindForgeWeb.Models
{
    public class CommentBlogsDto
    {
        public int CommentId { get; set; }
        public int BlogId { get; set; }
        public bool IsDeleted { get; set; }
        public bool Checked { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Action { get; set; }
    }
}
