using System.ComponentModel.DataAnnotations.Schema;

namespace GIF_S.Model
{
    public class UserCourseEnroll
    {
        public int Id { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        public bool Blocked { get; set; } = false;
        

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        
        [ForeignKey("Course")]
        public int CrsId { get; set; }
        public virtual Course Course { get; set; }
       
/*        [ForeignKey("Rate")]
        public int? RateId { get; set; }
        public virtual Rate? Rate { get; set; }*/

        [ForeignKey("Progress")]
        public int? ProgId { get; set; }
        public virtual Progress Progress { get; set; }
    }
}
