using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GIF_S.Model
{
    public class Rate
    {
        public int Id {  get; set; }
        public string? Review { get; set; }
        public bool Blocked { get; set; } = false;

        [Range(0, 5)]
        public int No_Of_Stars { get; set; }

        [ForeignKey(nameof(UserCourse))]
        public int UserCourseId { get; set; }
        public UserCourseEnroll UserCourse { get; set; }
    }
}
