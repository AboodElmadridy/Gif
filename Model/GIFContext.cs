using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GIF_S.Model
{
    public class GIFContext:IdentityDbContext<ApplicationUser>
    {
/*        public GIFContext()
        {
            
        }*/
        public GIFContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Course>().HasOne(c => c.Author).WithMany(u => u.CreatedCourses).HasForeignKey(c => c.AuthorId);
            base.OnModelCreating(builder);
        }
        

        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<SFile> SFiles { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<RoadMap> RoadMaps { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<UserCourseEnroll> UserCourseEnrolls  { get; set; }
        public DbSet<UserCourseFavourite> UserCourseFavourites { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Progress> Progresses { get; set; }
        public DbSet<InstructorForm> InstructorForms { get; set; }

    }
}
