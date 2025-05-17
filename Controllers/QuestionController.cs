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
    public class QuestionController : ControllerBase
    {
        private readonly UnitOfWork unit;
        private readonly UserManager<ApplicationUser> manager;
        public QuestionController(UnitOfWork unit, UserManager<ApplicationUser> manager)
        {
            this.unit = unit;
            this.manager = manager;
        }

        [Authorize(Roles = "Instructor")]
        [HttpPost("AddQuestion")]
        //[Authorize]
        public ActionResult<GeneralResponse> AddQuestion(int QuizId, QuestionDTO quesDTO)
        {
            var exist = unit.QuestionRepo.GetByFilter(q => q.QuesBody == quesDTO.QuesBody && q.QuesBody == quesDTO.QuesBody);
            if (exist == null)
            {
                Question question = new Question
                {
                    QuesBody = quesDTO.QuesBody,

                    QuizId = QuizId

                };
                if (ModelState.IsValid)
                {
                    unit.QuestionRepo.Add(question);

                    unit.Save();

                    return new GeneralResponse { Status = 200, Message = "Added successfully" };
                }
                return new GeneralResponse { Status = 400, Message = "Invalid Data" };
            }
            else
            {
                return new GeneralResponse { Status = 400, Message = "Question already exists" };
            }
        }

        [Authorize(Roles = "Instructor , Admin")]
        [HttpPut("UpdateQuestion")]
        public ActionResult<GeneralResponse> UpdateQuestion(int QuesId, QuestionDTO quesDTO)
        {
            var exist = unit.QuestionRepo.GetByFilter(q => q.Id == QuesId);

            if (exist != null)
            {
                Question ques = new Question
                {
                    Id = QuesId,
                    QuesBody = quesDTO.QuesBody,

                    QuizId = exist.QuizId,
                };
                if (ModelState.IsValid)
                {
                    unit.QuestionRepo.Update(ques);

                    unit.Save();

                    return new GeneralResponse { Status = 200, Message = "Updated successfully", Data = ques.Id };
                }
                return new GeneralResponse { Status = 400, Message = "Invalid Data" };
            }
            else
            {
                return new GeneralResponse { Status = 400, Message = "Question doesn't exist" };
            }
        }

        [Authorize(Roles = "Instructor , Admin")]
        [HttpDelete("DeleteQuestion")]
        public ActionResult<GeneralResponse> DeleteQuestion(int QuesId)
        {
            var ThisQues = unit.QuestionRepo.GetByFilter(q => q.Id == QuesId);
            ThisQues.Blocked = true;
            unit.Save();
            return new GeneralResponse { Status = 200, Message = "Deleted successfully" };
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("AdmitQuestion")]
        public ActionResult<GeneralResponse> AdmitQuestion(int QuesId)
        {
            var ThisQues = unit.QuestionRepo.GetByFilter(q => q.Id == QuesId);
            ThisQues.Blocked = false;
            unit.Save();
            return new GeneralResponse { Status = 200, Message = "Admitted successfully" };
        }

        [Authorize]
        [HttpGet("GetById")]
        public ActionResult<GeneralResponse> GetByID(int QuesId)
        {
            var ThisQues = unit.QuestionRepo.GetByFilter(q => q.Id == QuesId);

            return new GeneralResponse { Status = 200, Data = ThisQues };
        }

        [Authorize]
        [HttpGet("GetAll")]
        public ActionResult<GeneralResponse> GetAll(int QuizId) => new GeneralResponse { Data = unit.QuestionRepo.GetAll().Where(q=>q.QuizId == QuizId && q.Blocked == false) };

        [Authorize("Admin")]
        [HttpGet("Admin/GetAll")]
        public ActionResult<GeneralResponse> AdminGetAll(int QuizId) => new GeneralResponse { Data = unit.QuestionRepo.GetAll().Where(q => q.QuizId == QuizId) };
    }
}
