using GIF_S.Model;
using Microsoft.EntityFrameworkCore;

namespace GIF_S.Repo
{
    public class CourseRepo : Repo<Course>, ICourse
    {
        private readonly GIFContext context;

        public CourseRepo(GIFContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Course Course)
        {
            var old = context.Courses.FirstOrDefault(c => c.Id == Course.Id);
            if (old != null)
            {
                old.Free = Course.Free;
                old.Descreption = Course.Descreption;
                old.Image = Course.Image;
                old.Title = Course.Title;
                old.LastUpdate = Course.LastUpdate;
                old.Blocked = Course.Blocked;
            }
        }
        public List<Course> CoursesWithCategories() => context.Courses.Include(c => c.Categories).ToList();
/*        public Category GetCategory(string CategoryName)
        {
            return context.Categories.FirstOrDefault(c => c.Name == CategoryName);
        }*/
    }
}
