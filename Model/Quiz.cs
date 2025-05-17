using System.ComponentModel.DataAnnotations.Schema;

namespace GIF_S.Model
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Blocked { get; set; } = false;

        [ForeignKey("Section")]
        public int SecId { get; set; }
        public virtual Section Section { get; set; }
        public List<Question> questions { get; set; } = new List<Question>();
    }
}
