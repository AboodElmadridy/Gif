using GIF_S.Model;

namespace GIF_S.Repo
{
    public class ProgressRepo : Repo<Progress>, IProgress
    {
        private readonly GIFContext context;

        public ProgressRepo(GIFContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Progress Progress)
        {
            var old = context.Progresses.FirstOrDefault(p => p.Id == Progress.Id);
            if (old != null)
            {
                old.UserCourse.Id = Progress.UserCourse.Id;
                old.Ratio= Progress.Ratio;
                old.Blocked = Progress.Blocked;
                
            }
        }
    }
}
