using GIF_S.Model;

namespace GIF_S.Repo
{
    public class UserCourseFavouriteRepo : Repo<UserCourseFavourite>
    {
        private readonly GIFContext context;

        public UserCourseFavouriteRepo(GIFContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(UserCourseFavourite UserCourse)
        {
            var old = context.UserCourseFavourites.FirstOrDefault(uc => uc.Id == UserCourse.Id);
            if (old != null)
            {
                old.UserId = UserCourse.UserId;
                old.CrsId = UserCourse.CrsId;
                old.Favourite = UserCourse.Favourite;
                
            }
        }


    }
}