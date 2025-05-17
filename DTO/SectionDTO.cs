using System.ComponentModel.DataAnnotations.Schema;

namespace GIF_S.DTO
{
    public class SectionDTO
    {
//        public int CrsId { get; set; }
        public string Title { get; set; }
        public string Descreption { get; set; }
      //  public List<AddFilesDTO>? Files { get; set; } = new List<AddFilesDTO>();
        //public List<AddQuizDTO>? Quizzes { get; set; } = new List<AddQuizDTO>();
    }
}
