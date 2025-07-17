namespace MindForgeWeb.Models
{
    public class PftDetailsDto
    {
        public int PftId { get; set; }
        public int PftDId { get; set; } 
        public string Filename { get; set; }
        public IFormFile FilePath { get; set; }
        public string Action { get; set; }
    }
}
