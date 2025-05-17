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
    public class SectionController : ControllerBase
    {
        private readonly UnitOfWork unit;
        private readonly UserManager<ApplicationUser> manager;
        public SectionController(UnitOfWork unit, UserManager<ApplicationUser> manager)
        {
            this.unit = unit;
            this.manager = manager;
        }
        [Authorize(Roles = "Instructor")]
        [HttpPost("AddSection")]
        public ActionResult<GeneralResponse> AddSection(int CrsId ,SectionDTO SectionDTO)
        {
            var exist = unit.SectionRepo.GetByFilter(c => c.CrsId == CrsId && c.Title == SectionDTO.Title);
            if (exist == null)
            {
                Section Section = new Section
                {
                    Title = SectionDTO.Title,
                    Descreption = SectionDTO.Descreption,
                    CrsId = CrsId
                };
                if (ModelState.IsValid)
                {
                    unit.SectionRepo.Add(Section);

                    unit.Save();

                    return new GeneralResponse { Status = 200, Message = "Added successfully", Data = Section.Id };
                }
                return new GeneralResponse { Status = 400, Message = "Invalid Data" };
            }
            else
            {
                return new GeneralResponse { Status = 400, Message = "Section already exists" };
            }

        }

        [Authorize(Roles = "Instructor , Admin")]
        [HttpPut("UpdateSection")]
        public ActionResult<GeneralResponse> UpdateSection(int SecId ,SectionDTO SectionDTO)
        {
            var exist = unit.SectionRepo.GetByFilter(s => s.Id == SecId);
            var SameNamedSection = unit.SectionRepo.GetByFilter(s => s.Title == SectionDTO.Title && s.Id != SecId);
            if (exist != null && SameNamedSection == null)
            {
                Section Section = new Section
                {
                    Id = SecId,
                    Title = SectionDTO.Title,
                    Descreption = SectionDTO.Descreption,
                    CrsId = exist.CrsId
                };
                if (ModelState.IsValid)
                {
                    unit.SectionRepo.Update(Section);

                    unit.Save();

                    return new GeneralResponse { Status = 200, Message = "Updated successfully", Data = Section.Id };
                }
                return new GeneralResponse { Status = 400, Message = "Invalid Data" };
            }
            else
            {
                return new GeneralResponse { Status = 400, Message = "Section doesn't exist" };
            }
        }

        [Authorize(Roles = "Instructor , Admin")]
        [HttpDelete("DeleteSection")]
        public ActionResult<GeneralResponse> DeleteSection(int SecId)
        {
           var ThisSec = unit.SectionRepo.GetByFilter(s => s.Id == SecId);
            ThisSec.Blocked = true;
            unit.Save();
            return new GeneralResponse { Status = 200, Message = "Deleted successfully" };
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("AdmitSection")]
        public ActionResult<GeneralResponse> AdmitSection(int SecId)
        {
            var ThisSec = unit.SectionRepo.GetByFilter(s => s.Id == SecId);
            ThisSec.Blocked = false;
            unit.Save();
            return new GeneralResponse { Status = 200, Message = "Admitted successfully" };
        }

        [HttpGet("GetById")]
        public ActionResult<GeneralResponse> GetByID(int SecId)
        {
            var ThisSec = unit.SectionRepo.GetByFilter(s => s.Id == SecId);

            return new GeneralResponse { Status = 200, Data = ThisSec };
        }

        [HttpGet("GetByName")]
        public ActionResult<GeneralResponse> GetByName(string SecName)
        {
            var ThisSec = unit.SectionRepo.GetByFilter(s => s.Title == SecName);

            return new GeneralResponse { Status = 200, Data = ThisSec };
        }

        [HttpGet("GetAll")]
        public ActionResult<GeneralResponse> GetAll(int CrsId) => new GeneralResponse { Data = unit.SectionRepo.GetAll().Where(s=>s.CrsId == CrsId && s.Blocked == false).ToList() };

        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/GetAll")]
        public ActionResult<GeneralResponse> AdminGetAll(int CrsId) => new GeneralResponse { Data = unit.SectionRepo.GetAll().Where(s => s.CrsId == CrsId).ToList() };
    }
}
