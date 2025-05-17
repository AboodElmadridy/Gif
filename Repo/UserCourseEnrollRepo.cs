using GIF_S.Model;

namespace GIF_S.Repo
{
    public class UserCourseEnrollRepo : Repo<UserCourseEnroll>, IUserCourse
    {
        private readonly GIFContext context;

        public UserCourseEnrollRepo(GIFContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(UserCourseEnroll UserCourse)
        {
            var old = context.UserCourseEnrolls.FirstOrDefault(uc => uc.Id == UserCourse.Id);
            if (old != null)
            {
                old.EnrollmentDate = UserCourse.EnrollmentDate;
                old.UserId = UserCourse.UserId;
                old.CrsId = UserCourse.CrsId;
                old.Blocked = UserCourse.Blocked;
            }
        }
    }
}