using GIF_S.DTO;
using GIF_S.Model;
using GIF_S.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GIF_S.Response;
using static System.Collections.Specialized.BitVector32;
namespace GIF_S.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly UnitOfWork unit;
        private readonly UserManager<ApplicationUser> manager;
        public QuizController(UnitOfWork unit, UserManager<ApplicationUser> manager)
        {
            this.unit = unit;
            this.manager = manager;
        }

        [Authorize(Roles = "Instructor")]
        [HttpPost("AddQuiz")]
        //[Authorize]
        public ActionResult<GeneralResponse> AddQuiz(int SecId, QuizDTO quizDTO)
        {
            var exist = unit.QuizRepo.GetByFilter(q => q.Name == quizDTO.Name && q.SecId == SecId);
            if (exist == null)
            {
                Quiz quiz = new Quiz
                {
                    Name = quizDTO.Name,

                    SecId = SecId

                };
                if (ModelState.IsValid)
                {
                    unit.QuizRepo.Add(quiz);

                    unit.Save();

                    return new GeneralResponse { Status = 200, Message = "Added successfully" };
                }
                return new GeneralResponse { Status = 400, Message = "Invalid Data" };
            }
            else
            {
                return new GeneralResponse { Status = 400, Message = "Quiz already exists" };
            }
        }

        [Authorize(Roles = "Instructor , Admit")]
        [HttpPut("UpdateQuiz")]
        public ActionResult<GeneralResponse> UpdateQuiz(int QuizId, QuizDTO quizDTO)
        {
            var exist = unit.QuizRepo.GetByFilter(q => q.Id == QuizId);

            if (exist != null)
            {
               Quiz quiz = new Quiz
                {
                    Id = QuizId,
                    Name = quizDTO.Name,

                    SecId = exist.SecId,
                };
                if (ModelState.IsValid)
                {
                    unit.QuizRepo.Update(quiz);

                    unit.Save();

                    return new GeneralResponse { Status = 200, Message = "Updated successfully", Data = quiz.Id };
                }
                return new GeneralResponse { Status = 400, Message = "Invalid Data" };
            }
            else
            {
                return new GeneralResponse { Status = 400, Message = "Quiz doesn't exist" };
            }
        }

        [Authorize(Roles = "Instructor , Admin")]
        [HttpDelete("DeleteQuiz")]
        public ActionResult<GeneralResponse> DeleteQuiz(int QuizId)
        {
            var ThisQuiz = unit.QuizRepo.GetByFilter(q => q.Id == QuizId);
            ThisQuiz.Blocked = true;
            unit.Save();
            return new GeneralResponse { Status = 200, Message = "Deleted successfully" };
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("AdmitQuiz")]
        public ActionResult<GeneralResponse> AdmitQuiz(int QuizId)
        {
            var ThisQuiz = unit.QuizRepo.GetByFilter(q => q.Id == QuizId);
            ThisQuiz.Blocked = false;
            unit.Save();
            return new GeneralResponse { Status = 200, Message = "Admitted successfully" };
        }

        [Authorize]
        [HttpGet("GetById")]
        public ActionResult<GeneralResponse> GetByID(int QuizId)
        {
            var ThisQuiz = unit.QuizRepo.GetByFilter(q => q.Id == QuizId);

            return new GeneralResponse { Status = 200, Data = ThisQuiz };
        }

        [Authorize]
        [HttpGet("GetByName")]
        public ActionResult<GeneralResponse> GetByName(string QuizName)
        {
            var ThisQuiz = unit.QuizRepo.GetByFilter(q => q.Name == QuizName);

            return new GeneralResponse { Status = 200, Data = ThisQuiz };
        }

        [Authorize]
        [HttpGet("GetAll")]
        public ActionResult<GeneralResponse> GetAll(int SecId ,int CrsId,string UserId)
        {
            var exist = unit.UserCourseEnrollRepo.GetAll().FirstOrDefault(uc => uc.CrsId == CrsId && uc.UserId == UserId);
            if (exist == null)
                return new GeneralResponse { Status = 401, Message = "!!انرول عادي وخلاص اصل هتحل كويزات ازاي يعني" };
            return new GeneralResponse { Data = unit.QuizRepo.GetAll().Where(q => q.SecId == SecId && q.Blocked == false) };
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/GetAll")]
        public ActionResult<GeneralResponse> GetAll(int SecId) => new GeneralResponse { Data = unit.QuizRepo.GetAll().Where(q => q.SecId == SecId) };
        
    }
}
