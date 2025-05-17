using System.ComponentModel.DataAnnotations.Schema;

namespace GIF_S.Model
{
    public class WishList
    {
        public int Id { get; set; }
        public bool Blocked { get; set; } = false;

        [ForeignKey("User")]
        public string UserId { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
        public virtual ApplicationUser User { get; set; }
    }
}
