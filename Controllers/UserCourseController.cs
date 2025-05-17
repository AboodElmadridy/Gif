using GIF_S.DTO;
using GIF_S.Model;
using GIF_S.Repo;
using GIF_S.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GIF_S.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCourseController : ControllerBase
    {
        private readonly UnitOfWork unit;
        private readonly UserManager<ApplicationUser> manager;
        public UserCourseController(UnitOfWork unit, UserManager<ApplicationUser> manager)
        {
            this.unit = unit;
            this.manager = manager;
        }

        [Authorize]
        [HttpPost("Enroll")]
        public ActionResult<GeneralResponse> Enroll(UserCourseDTO enrollDTO)
        {
            UserCourseEnroll userCourse = new UserCourseEnroll { CrsId = enrollDTO.CrsId , UserId = enrollDTO.UserId};
            unit.UserCourseEnrollRepo.Add(userCourse);
            unit.Save();
            return new GeneralResponse {Status = 200 ,Message = "Enrolled Successfully" };
        }

        [Authorize]
        [HttpGet("IsEnrolled")]
        public ActionResult<GeneralResponse> IsEnrolled([FromQuery] UserCourseDTO enrolledDTO)
        {
           var exist = unit.UserCourseEnrollRepo.GetAll().FirstOrDefault(uc => uc.CrsId == enrolledDTO.CrsId && uc.UserId == enrolledDTO.UserId);
            if (exist == null)
            {
                return new GeneralResponse { Status = 404, Message = "This user doesn't enroll in that course", Data = false };
            }
            return new GeneralResponse { Status = 200, Message = "This user already enrolled in that course", Data = true };
        }

        [Authorize]
        [HttpPost("Favourite")]
        public ActionResult<GeneralResponse> Favourite(UserCourseDTO favouriteDTO)
        {
            UserCourseFavourite userCourse = new UserCourseFavourite { CrsId = favouriteDTO.CrsId, UserId = favouriteDTO.UserId };
            unit.UserCourseFavouriteRepo.Add(userCourse);
            unit.Save();
            return new GeneralResponse { Status = 200, Message = "Added to favourite Successfully" };
        }

        [Authorize]
        [HttpGet("IsFavourite")]
        public ActionResult<GeneralResponse> IsFavourite([FromQuery] UserCourseDTO favouriteDTO)
        {
            var exist = unit.UserCourseFavouriteRepo.GetAll().FirstOrDefault(uc => uc.CrsId == favouriteDTO.CrsId && uc.UserId == favouriteDTO.UserId);
            if (exist == null)
            {
                return new GeneralResponse { Status = 404, Message = "This user doesn't select that course as a favourite one", Data = false };
            }
            return new GeneralResponse { Status = 200, Message = "This user already selected that course as a favourite one", Data = true };
        }

        [Authorize]
        [HttpPost("Rate")]
        public ActionResult<GeneralResponse> Rate(RateDTO rateDTO)
        {
            var id = unit.UserCourseEnrollRepo.GetAll().FirstOrDefault(u=>u.UserId == rateDTO.UserId && u.CrsId == rateDTO.CrsId).Id;
            Rate rate = new Rate { No_Of_Stars =  rateDTO.No_Of_Stars, Review =  rateDTO.Review , UserCourseId = id };
            unit.RateRepo.Add(rate);
            unit.Save();
            return new GeneralResponse { Status = 200, Message = "Rated Successfully" };
        }

        [Authorize]
        [HttpGet("IsRated")]
        public ActionResult<GeneralResponse> IsRated([FromQuery]UserCourseDTO userCourse )
        {
            var id = unit.UserCourseEnrollRepo.GetAll().FirstOrDefault(u => u.UserId == userCourse.UserId && u.CrsId == userCourse.CrsId).Id;
            var exist = unit.RateRepo.GetAll().FirstOrDefault(r => r.UserCourseId == id);
            if (exist == null)
            {
                return new GeneralResponse { Status = 404, Message = "This user doesn't rate that course", Data = 0 };
            }
            return new GeneralResponse { Status = 200, Message = "This user already rate that course", Data = exist.No_Of_Stars };
        }

    }
}
