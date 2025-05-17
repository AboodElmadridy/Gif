using System.ComponentModel.DataAnnotations.Schema;

namespace GIF_S.Model
{
    public class Answer
    {
        public int Id { get; set; }
        public string AnsBody { get; set; }
        public bool IsTrue { get; set; } = false;
        public bool Blocked { get; set; } = false;

        [ForeignKey("Question")]
        public int QuesId { get; set; }
        public virtual Question Question { get; set; }
    }
}
