using System.Text.Json.Serialization;

namespace GIF_S.Model
{
   
    public class RoadMap
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image {  get; set; }
        public string Description { get; set; }
        public bool Blocked { get; set; } = false;
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}
