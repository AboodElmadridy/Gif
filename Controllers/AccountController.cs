using GIF_S.DTO;
using GIF_S.Model;
using GIF_S.Repo;
using GIF_S.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GIF_S.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration config;
        private readonly UnitOfWork unit;

        public AccountController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole>roleManager, IConfiguration config ,UnitOfWork unit)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.config = config;
            this.unit = unit;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<GeneralResponse>> Register(RegisterDTO registerUser)
        {
            var ApplicationUser = new ApplicationUser();
            ApplicationUser.UserName = registerUser.Name;
            ApplicationUser.Email = registerUser.Email;
            var res = await userManager.CreateAsync(ApplicationUser, registerUser.Password);

            if (ModelState.IsValid)
            {
                if (res.Succeeded)
                {
                    return new GeneralResponse { Message = "Created Successfully", Status = 201 };
                }


                foreach (var erorr in res.Errors)
                    ModelState.AddModelError("", erorr.Description);
            }
            return new GeneralResponse {Message = "Can't Register" , Status = 400 , Data = ModelState};
        }
        [HttpPost("Login")]
        public async Task<ActionResult<GeneralResponse>> Login(LoginDTO loginUser)
        {
            if (ModelState.IsValid)
            {
                var UserFromDB = await userManager.FindByEmailAsync(loginUser.Email);
                if (UserFromDB != null)
                {
                    bool IsMatch = await userManager.CheckPasswordAsync(UserFromDB, loginUser.Password);
                    if (IsMatch)
                    {

                        List<Claim> Claims = new List<Claim>();
                        Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); //token ID
                        Claims.Add(new Claim(ClaimTypes.NameIdentifier, UserFromDB.Id));
                        Claims.Add(new Claim(ClaimTypes.Name, UserFromDB.UserName));
                        var Roles = await userManager.GetRolesAsync(UserFromDB);
                        foreach (var role in Roles)
                            Claims.Add(new Claim(ClaimTypes.Role, role));

                        var SecKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecurityKey"]));
                        var SigninCred = new SigningCredentials(SecKey, SecurityAlgorithms.HmacSha256);

                        //Token design
                        JwtSecurityToken token = new JwtSecurityToken(
                             issuer: config["JWT:IssuerIP"],
                             audience: config["JWT:AudienceIP"],
                             expires: DateTime.Now.AddDays(30),
                             claims: Claims,
                             signingCredentials: SigninCred
                             );

                        //Token generate
                        return new GeneralResponse

                        {
                            Message = "دخلته يكسمك",
                            Status = 200,
                            Data = new
                            {
                                token = new JwtSecurityTokenHandler().WriteToken(token),
                                expiration = token.ValidTo
                            }

                        };
                        
                    }
                }
                ModelState.AddModelError("UserName", "UserName or Password is INvalid");

            }
            return new GeneralResponse {Status = 400 , Message = "منتاش فاهمني" , Data = ModelState };
        }
        [Authorize]
        [HttpPost("BecomeInstructor")]
        public ActionResult<GeneralResponse> BecomeInstructor(InstructorForm form)
        {
            unit.InstructorFormRepo.Add(form);
            unit.Save();
            return new GeneralResponse { Status = 201, Message = "Submitted successfully" };
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddRole")]
        public async Task<ActionResult<GeneralResponse>> AddRole(string RoleName)
        {
            var res = await roleManager.CreateAsync(new IdentityRole { Name = RoleName });
            if (res.Succeeded) return new GeneralResponse { Message = "طب تمام" };
            foreach (var erorr in res.Errors)
                ModelState.AddModelError("", erorr.Description);
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AssignRole")]
        public async Task<ActionResult<GeneralResponse>> AssignRole(string UserId , string RoleId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            var role = await roleManager.FindByIdAsync(RoleId);
            await userManager.AddToRoleAsync(user, role.Name);
            return new GeneralResponse { Status = 201, Message = "اد رول عادي وخلاص" };
        }
        //suport
    }
}
