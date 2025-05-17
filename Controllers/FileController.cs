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
    public class FileController : ControllerBase
    {
        private readonly UnitOfWork unit;
        private readonly UserManager<ApplicationUser> manager;
        public FileController(UnitOfWork unit, UserManager<ApplicationUser> manager)
        {
            this.unit = unit;
            this.manager = manager;
        }
        [Authorize(Roles = "Instructor")]
        [HttpPost("AddFile")]
        //[Authorize]
        public ActionResult<GeneralResponse> AddFile(int SectionId, FileDTO fileDTO)
        {
            var exist = unit.SFileRepo.GetByFilter(f => f.URL == fileDTO.URL && f.SecId == SectionId);
            if (exist == null)
            {
                SFile file = new SFile
                {
                    Name = fileDTO.Name,
                    URL = fileDTO.URL,
                    Type = fileDTO.Type,
                    SecId = SectionId

                };
                if (ModelState.IsValid)
                {
                    unit.SFileRepo.Add(file);

                    unit.Save();

                    return new GeneralResponse { Status = 200, Message = "Added successfully" };
                }
                return new GeneralResponse { Status = 400, Message = "Invalid Data" };
            }
            else
            {
                return new GeneralResponse { Status = 400, Message = "File already exists" };
            }
        }

        [Authorize(Roles = "Instructor , Admin")]
        [HttpPut("UpdateFile")]
        public ActionResult<GeneralResponse> UpdateFile(int FileId, FileDTO fileDTO)
        {
            var exist = unit.SFileRepo.GetByFilter(f => f.Id == FileId);
           
            if (exist != null)
            {
               SFile file = new SFile
                {
                    Id = FileId,
                    Name = fileDTO.Name,
                    URL = fileDTO.URL,
                    Type = fileDTO.Type,
                    SecId = exist.SecId,
               };
                if (ModelState.IsValid)
                {
                    unit.SFileRepo.Update(file);

                    unit.Save();

                    return new GeneralResponse { Status = 200, Message = "Updated successfully", Data = file.Id };
                }
                return new GeneralResponse { Status = 400, Message = "Invalid Data" };
            }
            else
            {
                return new GeneralResponse { Status = 400, Message = "File doesn't exist" };
            }
        }

        [Authorize(Roles = "Instructor , Admin")]
        [HttpDelete("DeleteFIle")]
        public ActionResult<GeneralResponse> DeleteFile(int FileId)
        {
            var ThisFile = unit.SFileRepo.GetByFilter(f => f.Id == FileId);
            ThisFile.Blocked = true;
            unit.Save();
            return new GeneralResponse { Status = 200, Message = "Deleted successfully" };
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("AdmitFIle")]
        public ActionResult<GeneralResponse> AdmitFile(int FileId)
        {
            var ThisFile = unit.SFileRepo.GetByFilter(f => f.Id == FileId);
            ThisFile.Blocked = false;
            unit.Save();
            return new GeneralResponse { Status = 200, Message = "Admitted successfully" };
        }

        [Authorize]
        [HttpGet("GetById")]
        public ActionResult<GeneralResponse> GetByID(int FileId)
        {
            var ThisFile = unit.SFileRepo.GetByFilter(f => f.Id == FileId);

            return new GeneralResponse { Status = 200, Data = ThisFile };
        }

        [Authorize]
        [HttpGet("GetByName")]
        public ActionResult<GeneralResponse> GetByName(string FileName)
        {
            var ThisFile = unit.SFileRepo.GetByFilter(f => f.Name == FileName);

            return new GeneralResponse { Status = 200, Data = ThisFile };
        }

        [Authorize]
        [HttpGet("GetAll")]
        public ActionResult<GeneralResponse> GetAll(int SecId) => new GeneralResponse { Data = unit.SFileRepo.GetAll().Where(f=>f.SecId == SecId && f.Blocked == false).ToList() };

        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/GetAll")]
        public ActionResult<GeneralResponse> AdminGetAll(int SecId) => new GeneralResponse { Data = unit.SFileRepo.GetAll().Where(f => f.SecId == SecId).ToList() };
    }
}
