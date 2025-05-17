using GIF_S.Model;

namespace GIF_S.Repo
{
    public class QuestionRepo : Repo<Question>, IQuestion
    {
        private readonly GIFContext context;

        public QuestionRepo(GIFContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Question Question)
        {
            var old = context.Questions.FirstOrDefault(q => q.Id == Question.Id);
            if (old != null)
            {
                old.QuesBody = Question.QuesBody;
                old.Blocked = Question.Blocked;
            }
        }
    }
}
