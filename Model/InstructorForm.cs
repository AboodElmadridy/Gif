using System.ComponentModel.DataAnnotations.Schema;

namespace GIF_S.Model
{
    public class InstructorForm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string ExperienceQuestion { get; set; }
        public string TopicQuestion { get; set; }
        public string CompanyQuestion { get; set; }
        public string LinkedIn { get; set; }
        public string CV {  get; set; }
        public ApplicationUser Instructor { get; set; }
        [ForeignKey(nameof(Instructor))]
        public string InstructorId { get; set; }
    }
}
