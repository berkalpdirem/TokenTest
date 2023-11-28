using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Security.Claims;
using TokenTest.Context;
using TokenTest.Data.DTOs;
using TokenTest.Data.Entity;
using TokenTest.Token;

namespace TokenTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly AppDbContext _Context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AppUserController( AppDbContext context, UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _Context = context;
            _userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            TokenGen tokgen = new TokenGen();
            var result = "";
            var RelatedUser = await _userManager.FindByEmailAsync(dto.Email);
            if (RelatedUser != null) 
            {
                var login = await signInManager.PasswordSignInAsync(RelatedUser, dto.Password, false, false);
                if (login.Succeeded)
                {
                    var roleList = await _userManager.GetRolesAsync(RelatedUser);
                    var roleClaims = roleList.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

                    var claimList = await _userManager.GetClaimsAsync(RelatedUser);
                    var listUserInfo = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name,RelatedUser.Name),
                        new Claim(ClaimTypes.Surname,RelatedUser.SurName.ToString()),
                        new Claim(ClaimTypes.DateOfBirth,RelatedUser.BirthDate.ToString())
                    };

                    roleClaims.AddRange(claimList);
                    roleClaims.AddRange(listUserInfo);

                    result = tokgen.GenerateToken(roleClaims);

                }
                else
                {
                    result = "Kullanıcı Adı veya Şife Hatalı";
                }

            }
            else
            {
                result = "Kullanıcı Adı veya Şife Hatalı";
            }
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            AppUser RelatedUser = new AppUser()
            {
                Email= dto.Email,
                PasswordHash = dto.Password,
                UserName = dto.UserName,
                Name = dto.Name,
                SurName = dto.SurName,
                BirthDate = dto.BirthDate,
               
            };

            var userRegisterAction = await _userManager.CreateAsync(RelatedUser);
            if (userRegisterAction.Succeeded)
            {
                return Ok("Başarılı Kayıt");
            }
            return BadRequest("Başarısız Kayıt");
        }
    }
}
