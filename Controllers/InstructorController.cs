using GIF_S.Model;
using GIF_S.Repo;
using GIF_S.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GIF_S.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly UnitOfWork unit;
        private readonly UserManager<ApplicationUser> manager;
        public InstructorController(UnitOfWork unit, UserManager<ApplicationUser> manager)
        {
            this.unit = unit;
            this.manager = manager;
        }
    }
}
