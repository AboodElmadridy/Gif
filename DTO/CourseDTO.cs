using System.ComponentModel.DataAnnotations;

namespace GIF_S.DTO
{
    public class CourseDTO
    {
     //   public int RoadMapId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool Free { get; set; }
        [Range(1,4)]
        public short Difficulty { get; set; }
        public string AuthorId { get; set; }
    }
}
