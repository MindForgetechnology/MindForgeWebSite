namespace MindForgeWeb.Models
{
    public class BlogDetailsDto
    {
        public int BlogId { get; set; }
        public string BlogTittle { get; set; }
        public string BlogDescription { get; set; }
        public string PostDate { get; set; }
        public string Filename { get; set; }
        public IFormFile FilePath { get; set; }
        public string Action { get; set; }
    }
}
