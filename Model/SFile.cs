using System.ComponentModel.DataAnnotations.Schema;

namespace GIF_S.Model
{
    public class SFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Type { get; set; }
        public DateTime Duration { get; set; }
        public bool IsCompleted { get; set; } = false;
        public bool Blocked { get; set; } = false;

        [ForeignKey("Section")]
        public int SecId { get; set; }
        public virtual Section Section { get; set; }
    }
}
