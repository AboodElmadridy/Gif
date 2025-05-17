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
    public class RoadMapController : ControllerBase
    {
        private readonly UnitOfWork unit;
        private readonly UserManager<ApplicationUser> manager;
        public RoadMapController(UnitOfWork unit, UserManager<ApplicationUser> manager)
        {
            this.unit = unit;
            this.manager = manager;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddRoadMap")]
        //[Authorize]
        public ActionResult<GeneralResponse> AddRoadMap(RoadMapDTO mapDTO)
        {
            var exist = unit.RoadMapRepo.GetByFilter(rm=>rm.Title == mapDTO.Title);
            if (exist == null)
            {
                RoadMap roadMap = new RoadMap
                {
                    Title = mapDTO.Title,
                    Description =  mapDTO.Description,
                    Image = mapDTO.Image
                };
                if (ModelState.IsValid)
                {
                    unit.RoadMapRepo.Add(roadMap);

                    unit.Save();

                    return new GeneralResponse { Status = 200, Message = "Added successfully" , Data = roadMap.Id};
                }
                return new GeneralResponse { Status = 400, Message = "Invalid Data" };
            }
            else
            {
                return new GeneralResponse { Status = 400, Message = "RoadMap already exists" };
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateRoadMap")]
        public ActionResult<GeneralResponse> UpdateRoadMap(int RoadMapId, RoadMapDTO mapDTO)
        {
            var exist = unit.RoadMapRepo.GetByFilter(rm => rm.Id == RoadMapId);

            if (exist != null)
            {
                RoadMap roadMap = new RoadMap
                {
                    Id = RoadMapId,
                    Title = mapDTO.Title,
                    Description = mapDTO.Description,
                    Image = mapDTO.Image
                };
                if (ModelState.IsValid)
                {
                    unit.RoadMapRepo.Update(roadMap);

                    unit.Save();

                    return new GeneralResponse { Status = 200, Message = "Updated successfully", Data = roadMap.Id };
                }
                return new GeneralResponse { Status = 400, Message = "Invalid Data" };
            }
            else
            {
                return new GeneralResponse { Status = 400, Message = "File doesn't exist" };
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteRoadMap")]
        public ActionResult<GeneralResponse> DeleteRoadMap(int RoadMapId)
        {
            var ThisRoadMap = unit.RoadMapRepo.GetByFilter(rm => rm.Id == RoadMapId);
            ThisRoadMap.Blocked = true;
            unit.Save();
            return new GeneralResponse { Status = 200, Message = "Deleted successfully" };
        }

        [Authorize]
        [HttpGet("GetById")]
        public ActionResult<GeneralResponse> GetByID(int RoadMapId)
        {
            var ThisRoadMap = unit.RoadMapRepo.GetByFilter(rm => rm.Id == RoadMapId);

            return new GeneralResponse { Status = 200, Data = ThisRoadMap };
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
        public ActionResult<GeneralResponse> GetAll(int SecId) => new GeneralResponse { Data = unit.SFileRepo.GetAll().Where(f => f.SecId == SecId && f.Blocked == false).ToList() };

        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/GetAll")]
        public ActionResult<GeneralResponse> AdminGetAll(int SecId) => new GeneralResponse { Data = unit.SFileRepo.GetAll().Where(f => f.SecId == SecId).ToList() };
    
    }
}
