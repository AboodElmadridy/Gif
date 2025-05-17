using GIF_S.DTO;
using GIF_S.Model;
using GIF_S.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GIF_S.Response;
namespace GIF_S.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly UnitOfWork unit;
        private readonly UserManager<ApplicationUser> manager;
        public CourseController(UnitOfWork unit ,UserManager<ApplicationUser> manager)
        {
            this.unit = unit;
            this.manager = manager;
        }

        [Authorize(Roles = "Instructor")]
        [HttpPost("AddCourse")]
        //[Authorize]
        public ActionResult<GeneralResponse> AddCourse(int? RoadMapId,CourseDTO courseDTO)
        {
            
            var exist = unit.CourseRepo.GetByFilter(c => c.AuthorId == courseDTO.AuthorId && c.Title == courseDTO.Title && c.RoadMapId == RoadMapId);
            if (exist == null)
            {
                Course course = new Course
                {
                    Title = courseDTO.Title,
                    Descreption = courseDTO.Description,
                    Image = courseDTO.Image,
                    Free = courseDTO.Free,
                    Difficulty = courseDTO.Difficulty,
                    AuthorId = courseDTO.AuthorId,
                    TimeCreate = DateTime.Now,
                    RoadMapId = RoadMapId

                };
                if (ModelState.IsValid)
                {
                    unit.CourseRepo.Add(course);

                    unit.Save();

                    return new GeneralResponse { Status = 200, Message = "Added successfully" };
                }
                return new GeneralResponse { Status = 400, Message = "Invalid Data" };
            }
            else
            {
                return new GeneralResponse { Status = 400, Message = "Course already exists" };
            }
        }

         #region Complex Update
        /*  [HttpPost("Update/{Id:int}")]
          public ActionResult<GeneralResponse> UpdateCourse(int Id , UpdateCourseDTO updateCourseFromReq)
          {
              var courseFromDB = unit.CourseRepo.GetByFilter(c => c.Id == Id);
              if(courseFromDB != null)
              {
                  courseFromDB.Title = updateCourseFromReq.Title;
                  courseFromDB.Descreption = updateCourseFromReq.Description;
                  courseFromDB.Image = updateCourseFromReq.Image;
                  courseFromDB.Difficulty = updateCourseFromReq.Difficulty;

                  foreach(var section in updateCourseFromReq.Sections)
                  {
                      var sectionDTO = new Section {Title = section.Title , Descreption = section.Descreption ,CrsId = Id };         
                      unit.SectionRepo.Add(sectionDTO);
                  }
                  unit.Save();
                  var SectionsFromDB = unit.SectionRepo.GetAll().Where(s => s.CrsId == Id);
                  foreach (var section in updateCourseFromReq.Sections)
                  {
                      int ID = 0;
                      foreach(var sec in SectionsFromDB)
                      {
                          if(sec.Title == section.Title && sec.Descreption == section.Descreption && sec.CrsId == Id)
                          {
                              ID = sec.Id;
                              break;
                          }
                      }
                      foreach(var file in section.Files)
                      {

                          var fileDTO = new SFile { Name = file.Name, URL = file.URL, Type = file.Type, SecId = ID };
                          unit.SFileRepo.Add(fileDTO);
                      }
                  }
                  unit.Save();
                  return new GeneralResponse { Status = 200, Message = "Added successfully" };    
              }
              return new GeneralResponse { Status = 400, Message = "Undefined course" };

          }*/
          #endregion

        [Authorize(Roles = "Admin , Instructor")]
        [HttpPut("UpdateCourse")]
        public ActionResult<GeneralResponse> UpdateCourse(int CrsId,int? RoadMapId, CourseDTO CourseDTO)
        {
            var exist = unit.CourseRepo.GetByFilter(c => c.Id == CrsId);
            var SameNamedCourse = unit.CourseRepo.GetByFilter(s => s.Title == CourseDTO.Title && s.Id != CrsId);
            if (exist != null && SameNamedCourse == null)
            {
                Course Course = new Course
                {
                    Id = CrsId,
                    Title = CourseDTO.Title,
                    Descreption = CourseDTO.Description,
                    Image = CourseDTO.Image,
                    
                    RoadMapId = RoadMapId
                };
                if (ModelState.IsValid)
                {
                    unit.CourseRepo.Update(Course);

                    unit.Save();

                    return new GeneralResponse { Status = 200, Message = "Updated successfully", Data = Course.Id };
                }
                return new GeneralResponse { Status = 400, Message = "Invalid Data" };
            }
            else
            {
                return new GeneralResponse { Status = 400, Message = "Course doesn't exist" };
            }
        }

        [Authorize(Roles = "Instructor , Admin")] 
        [HttpDelete("DeleteCourse")]
        public ActionResult<GeneralResponse> DeleteCourse(int CrsId)
        {
            var ThisCrs = unit.CourseRepo.GetByFilter(s => s.Id == CrsId);
            ThisCrs.Blocked = true;
            unit.Save();
            return new GeneralResponse { Status = 200, Message = "Deleted successfully" };
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AdmitCourse")]
        public ActionResult<GeneralResponse> AdmitCourse(int CrsId)
        {
            var ThisCrs = unit.CourseRepo.GetByFilter(s => s.Id == CrsId);
            ThisCrs.Blocked = false;
            unit.Save();
            return new GeneralResponse { Status = 200, Message = "Admitted successfully" };
        }

        [HttpGet("GetById")]
        public ActionResult<GeneralResponse> GetByID(int CrsId)
        {
            var ThisCrs = unit.CourseRepo.GetByFilter(s => s.Id == CrsId);

            return new GeneralResponse { Status = 200, Data = ThisCrs };
        }

        [HttpGet("GetByName")]
        public ActionResult<GeneralResponse> GetByName(string CrsName)
        {
            var ThisCrs = unit.CourseRepo.GetByFilter(s => s.Title == CrsName);

            return new GeneralResponse { Status = 200, Data = ThisCrs };
        }

        [HttpGet("GetByCategory")]
        public ActionResult<GeneralResponse> GetByCategory(string category)
        {

           var MatchedCourses = unit.CourseRepo.CoursesWithCategories().Where(c => c.Categories.FirstOrDefault(cat=>cat.Name == category) != null).Select(c => new {Id = c.Id , Name = c.Title ,Free= c.Free}).ToList();
            return new GeneralResponse { Status = 200, Data = MatchedCourses };
        }

        [HttpGet("GetFreeCourses")]
        public ActionResult<GeneralResponse> GetFreeCourses()
        {
            var FreeCourses = unit.CourseRepo.GetAll().Where(c => c.Free == false);
            return new GeneralResponse { Status = 200, Data = FreeCourses };
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/GetAll")]
        public ActionResult<GeneralResponse> AdminGetAll() => new GeneralResponse { Data = unit.CourseRepo.GetAll() };

        [HttpGet("GetAll")]
        public ActionResult<GeneralResponse> GetAll() => new GeneralResponse { Data = unit.CourseRepo.GetAll().Where(c=>c.Blocked == false) };

        [HttpGet("Admin/GetInRoadMap")]
        public ActionResult<GeneralResponse> AdminGetInRoadMap(int RMapId) => new GeneralResponse { Data = unit.CourseRepo.GetAll().Where(c=>c.RoadMapId == RMapId).ToList() };
        
        [HttpGet("GetInRoadMap")]
        public ActionResult<GeneralResponse> GetInRoadMap(int RMapId) => new GeneralResponse { Data = unit.CourseRepo.GetAll().Where(c => c.RoadMapId == RMapId).Where(c=>c.Blocked == false).ToList() };

        [Authorize]
        [HttpGet("GetAllEnrolled")]
        public ActionResult<GeneralResponse> GetAllEnrolled(string UserId) => new GeneralResponse { Data = unit.UserCourseEnrollRepo.GetAll().Where(uc=>uc.UserId == UserId).ToList()};

        [Authorize]
        [HttpGet("GetAllFavourite")]
        public ActionResult<GeneralResponse> GetAllFavourite(string UserId) => new GeneralResponse { Data = unit.UserCourseFavouriteRepo.GetAll().Where(uc => uc.UserId == UserId).ToList() };

        [HttpGet("GetAllPopular")]
        public ActionResult<GeneralResponse> GetAllPopular() => new GeneralResponse { Data = unit.UserCourseEnrollRepo.GetAll().GroupBy(uc=>uc.CrsId).Select(uc=> new {Id = uc.Key , cnt = uc.Count()}).OrderByDescending(uc=>uc.cnt).Take(10) };
        [HttpGet("GetAllNewlly")]
        public ActionResult<GeneralResponse> GetAllNewlly() => new GeneralResponse { Data = unit.CourseRepo.GetAll().OrderByDescending(c=>c.TimeCreate).Take(10)};

        
        [HttpGet("GetCreatedCourses")]
        public ActionResult<GeneralResponse> CreatedCourses(string AuthorId) => new GeneralResponse { Data = unit.CourseRepo.GetAll().Where(c => c.AuthorId == AuthorId) };
    }
    
}
