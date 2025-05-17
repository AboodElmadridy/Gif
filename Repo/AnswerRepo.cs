using GIF_S.Model;

namespace GIF_S.Repo
{
    public class AnswerRepo : Repo<Answer> , IAnswer
    {
        private readonly GIFContext context;
       
        public AnswerRepo(GIFContext context):base(context)
        {
            this.context = context;
        }

        public void Update(Answer answer)
        {
            var old = context.Answers.FirstOrDefault(a => a.Id == answer.Id);
            if (old != null)
            {
                old.AnsBody = answer.AnsBody;
                old.Blocked = answer.Blocked;
                old.QuesId = answer.QuesId;
                old.IsTrue = answer.IsTrue;
            }
        }
    }
}
