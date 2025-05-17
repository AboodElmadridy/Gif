using Microsoft.AspNetCore.Identity;

namespace GIF_S.Model
{
    public class ApplicationUser:IdentityUser
    {
        public string? Photo {  get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Bio {  get; set; } = string.Empty ;

        public bool Blocked { get; set; } = false;
        public DateTime RegisterationDate { get; set; } = DateTime.Now;
        public List<Course>? CreatedCourses { get; set; } = new List<Course>();
        public List<UserCourseEnroll> Courses { get; set; } = new List<UserCourseEnroll> { };
        public List<UserCourseFavourite> CoureFavourite { get; set; } = new List<UserCourseFavourite> { };
    }
}
