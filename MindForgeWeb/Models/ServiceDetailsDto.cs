namespace MindForgeWeb.Models
{
    public class ServiceDetailsDto
    {
        public int ServiceId { get; set; }
        public int ServicedId { get; set; }
        public string ServiceTittle { get; set; }
        public string ServiceDescription { get; set; }
        public string Filename { get; set; }
        public IFormFile FilePath { get; set; } 
        public string Action { get; set; }
    }
}
