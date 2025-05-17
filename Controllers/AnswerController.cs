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
    public class AnswerController : ControllerBase
    {
        private readonly UnitOfWork unit;
        private readonly UserManager<ApplicationUser> manager;
        public AnswerController(UnitOfWork unit, UserManager<ApplicationUser> manager)
        {
            this.unit = unit;
            this.manager = manager;
        }

        [Authorize(Roles = "Instructor")]
        [HttpPost("AddAnswer")]
        //[Authorize]
        public ActionResult<GeneralResponse> AddAnswer(int QuesId, AnswerDTO ansDTO)
        {
            var exist = unit.AnswerRepo.GetByFilter(a => a.AnsBody == ansDTO.AnsBody && a.QuesId == QuesId);
            if (exist == null)
            {
                Answer ans = new Answer
                {
                    AnsBody = ansDTO.AnsBody,
                    IsTrue = ansDTO.IsTrue,
                    QuesId = QuesId

                };
                if (ModelState.IsValid)
                {
                    unit.AnswerRepo.Add(ans);

                    unit.Save();

                    return new GeneralResponse { Status = 200, Message = "Added successfully" };
                }
                return new GeneralResponse { Status = 400, Message = "Invalid Data" };
            }
            else
            {
                return new GeneralResponse { Status = 400, Message = "Answer already exists" };
            }
        }

        [Authorize(Roles = "Instructor , Admin")]
        [HttpPut("UpdateAnswer")]
        public ActionResult<GeneralResponse> UpdateQuestion(int AnsId, AnswerDTO ansDTO)
        {
            var exist = unit.AnswerRepo.GetByFilter(a => a.Id == AnsId);

            if (exist != null)
            {
                Answer ans = new Answer
                {
                    Id = AnsId,
                    AnsBody = ansDTO.AnsBody,
                    IsTrue = ansDTO.IsTrue,
                    QuesId = exist.QuesId
                    
                };
                if (ModelState.IsValid)
                {
                    unit.AnswerRepo.Update(ans);

                    unit.Save();

                    return new GeneralResponse { Status = 200, Message = "Updated successfully", Data = ans.Id };
                }
                return new GeneralResponse { Status = 400, Message = "Invalid Data" };
            }
            else
            {
                return new GeneralResponse { Status = 400, Message = "Answer doesn't exist" };
            }
        }

        [Authorize(Roles = "Instructor , Admin")]
        [HttpDelete("DeleteAnswer")]
        public ActionResult<GeneralResponse> DeleteQuestion(int AnsId)
        {
            var ThisAns = unit.AnswerRepo.GetByFilter(a => a.Id == AnsId);
            ThisAns.Blocked = true;
            unit.Save();
            return new GeneralResponse { Status = 200, Message = "Deleted successfully" };
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("AdminAnswer")]
        public ActionResult<GeneralResponse> AdminQuestion(int AnsId)
        {
            var ThisAns = unit.AnswerRepo.GetByFilter(a => a.Id == AnsId);
            ThisAns.Blocked = false;
            unit.Save();
            return new GeneralResponse { Status = 200, Message = "Admitted successfully" };
        }

        [Authorize]
        [HttpGet("GetById")]
        public ActionResult<GeneralResponse> GetByID(int AnsId)
        {
            var ThisAns = unit.AnswerRepo.GetByFilter(a => a.Id == AnsId);

            return new GeneralResponse { Status = 200, Data = ThisAns };
        }

        [Authorize]
        [HttpGet("GetAll")]
        public ActionResult<GeneralResponse> GetAll(int QuesId) => new GeneralResponse { Data = unit.AnswerRepo.GetAll().Where(a => a.QuesId == QuesId && a.Blocked == false) };

        [Authorize("Admin")]
        [HttpGet("Admin/GetAll")]
        public ActionResult<GeneralResponse> AdminGetAll(int QuesId) => new GeneralResponse { Data = unit.AnswerRepo.GetAll().Where(a => a.QuesId == QuesId) };
    }
}
