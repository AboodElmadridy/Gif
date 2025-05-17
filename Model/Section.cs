using System.ComponentModel.DataAnnotations.Schema;

namespace GIF_S.Model
{
    public class Section
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descreption { get; set; }
        public DateTime Duration { get; set; }
        public bool Blocked { get; set; } = false;

        [ForeignKey("Course")]
        public int CrsId { get; set; }
        public virtual Course Course { get; set; }
        public List<SFile> sfiles { get; set; } = new List<SFile>();
        public List<Quiz>? Quizzes { get; set; } = new List<Quiz>();
    }
}
