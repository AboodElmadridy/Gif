using System.ComponentModel.DataAnnotations.Schema;

namespace GIF_S.Model
{
    public class Question
    {
        public int Id { get; set; }
        public string QuesBody { get; set; }
        public bool Blocked { get; set; } = false;
        public Quiz Quiz { get; set; }
        [ForeignKey(nameof(Quiz))]
        public int QuizId { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
}
