using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace GIF_S.DTO
{
    public class RegisterDTO
    {

        public string Name { get; set; }
        [Email]
        public string Email {  get; set; }

        public string Password { get; set; }
     
    }
}