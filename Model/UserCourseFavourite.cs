using System.ComponentModel.DataAnnotations.Schema;

namespace GIF_S.Model
{
    public class UserCourseFavourite
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Course")]
        public int CrsId { get; set; }
        public virtual Course Course { get; set; }
        public bool Favourite { get; set; } = false;

    }
}
