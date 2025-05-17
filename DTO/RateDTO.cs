using System.ComponentModel.DataAnnotations;

namespace GIF_S.DTO
{
    public class RateDTO
    {
        public string UserId { get; set; }
        public int CrsId { get; set; }
        public string? Review { get; set; }
        public int No_Of_Stars { get; set; }
    }
}
