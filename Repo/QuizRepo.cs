using GIF_S.Model;
using Microsoft.EntityFrameworkCore;

namespace GIF_S.Repo
{
    public class QuizRepo : Repo<Quiz>, IQuiz
    {
        private readonly GIFContext context;

        public QuizRepo(GIFContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Quiz Quiz)
        {
            var old = context.Quizzes.FirstOrDefault(q => q.Id == Quiz.Id);
            if (old != null)
            {
                old.SecId = Quiz.SecId;
                old.Blocked = Quiz.Blocked;            }
        }

    }
}