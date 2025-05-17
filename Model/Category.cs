using System.ComponentModel.DataAnnotations.Schema;

namespace GIF_S.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey(nameof(Id))]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
