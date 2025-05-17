using GIF_S.Model;

namespace GIF_S.Repo
{
    public class UnitOfWork
    {

        private GIFContext context { get; set;}
        public UnitOfWork(GIFContext context)
        {
            this.context = context;
            RateRepo = new RateRepo(context);
            AnswerRepo = new AnswerRepo(context);
            ProgressRepo = new ProgressRepo(context);
            QuestionRepo = new QuestionRepo(context);
            QuizRepo = new QuizRepo(context);
            RoadMapRepo = new RoadMapRepo(context);
            SectionRepo = new SectionRepo(context);
            SFileRepo = new SFileRepo(context);
            WishListRepo = new WishListRepo(context);
            UserCourseEnrollRepo = new UserCourseEnrollRepo(context);
            UserCourseFavouriteRepo = new UserCourseFavouriteRepo(context);
            CourseRepo = new CourseRepo(context);
            InstructorFormRepo = new InstructorFormRepo(context);
        }

        public RateRepo RateRepo { get; set; }
        public AnswerRepo AnswerRepo { get; set; }
        public ProgressRepo ProgressRepo { get; set; }
        public QuestionRepo QuestionRepo { get; set; }
        public QuizRepo QuizRepo { get; set; }
        public RoadMapRepo RoadMapRepo { get; set; }
        public SectionRepo SectionRepo { get; set; }
        public SFileRepo SFileRepo { get; set; }
        public WishListRepo WishListRepo { get; set; }
        public UserCourseEnrollRepo UserCourseEnrollRepo { get; set; }
        public UserCourseFavouriteRepo UserCourseFavouriteRepo { get; set; } 
        public CourseRepo CourseRepo { get; set; }
        public InstructorFormRepo InstructorFormRepo { get; set; }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
