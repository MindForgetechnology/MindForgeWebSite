namespace MindForgeWeb.Models
{
    public class TeamDetailsDto
    {

        public int TeamsId { get; set; }
  
        public string Designation { get; set; }
        public string Name { get; set; }
        public string Filename { get; set; }
        public IFormFile FilePath { get; set; }
        public string Action { get; set; }


    }
}
