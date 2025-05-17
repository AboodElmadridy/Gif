using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GIF_S.Model
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }//
        public string Descreption { get; set; }//
        public bool Free { get; set; } = true;
        public string Image { get; set; }//
        [Range(1,4)]
        public short Difficulty { get; set; }
        public DateTime Duration { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        public bool Blocked { get; set; } = true;
        public ApplicationUser Author { get; set; }
        [ForeignKey(nameof(Author))]
        public string AuthorId { get; set; }

        [ForeignKey(nameof(RoadMap))]
        public int? RoadMapId { get; set; }
        public RoadMap RoadMap { get; set; }

        public List<Section> Sections { get; set; } = new List<Section> { }; //
        public List<Category> Categories { get; set;} = new List<Category> { };
        //Requires nullable
        //Quizzes
        public List<UserCourseEnroll> UsersEnrolled { get; set;} = new List<UserCourseEnroll> { };
        public List<UserCourseFavourite> UserFavourite { get; set; } = new List<UserCourseFavourite> { };

    }
}
