using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StockExChange.EF;
using StockExChange.EF.Dtos.AccountsDto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StockExChange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly IConfiguration _config;

        public AccountsController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole>roleManger, IConfiguration config)
        {
            _usermanager = userManager;
            _rolemanager = roleManger;
            _config = config;

        }

        [HttpPost("createRole")]

        public async Task<IActionResult> CreateRole(RoleDto roledto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }
            var newrole = new IdentityRole
            {
                Id=Guid.NewGuid().ToString(),
                Name=roledto.RoleName,
            };
            var saverole=await _rolemanager.CreateAsync(newrole);

            if(saverole.Succeeded)
            {
                return Ok(new { message = "Role Created Successfully", roledto });
            }
            return BadRequest(saverole.Errors.FirstOrDefault());
        }



        [HttpPost("Register")]

        public async Task<IActionResult> CreateUser(UserDto userdto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newuser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Name = userdto.Name,
                UserName = userdto.UserName,
                Email = userdto.Email,

            };


            var SaveUser=await _usermanager.CreateAsync(newuser,userdto.Password);
            if(SaveUser.Succeeded)
            {
                var AddUserRole = await _usermanager.AddToRoleAsync(newuser,"Admin");
                if (AddUserRole.Succeeded)
                {
                    return Ok(new { message = "user Created successfully", userdto });
                }

                return BadRequest(AddUserRole.Errors.FirstOrDefault());
            }
                return BadRequest(new {message= SaveUser.Errors.FirstOrDefault() });

        }


        [HttpPost("Login")]

        public async Task<IActionResult> Login(LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }

            ApplicationUser User = await _usermanager.FindByNameAsync(login.UserName);
            if (User != null)
            {
                var checkPassowrd = await _usermanager.CheckPasswordAsync(User, login.Password);
                if (checkPassowrd)
                {
                    var Claims = new List<Claim>();
                    Claims.Add(new Claim(ClaimTypes.Name, User.UserName));
                    Claims.Add(new Claim(ClaimTypes.NameIdentifier, User.Id));
                    Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    var roles = await _usermanager.GetRolesAsync(User);

                    foreach (var role in roles)
                    {
                        Claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
                    SigningCredentials SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


                    JwtSecurityToken MyToken = new JwtSecurityToken(
                         issuer: _config["JWT:ValidIssuar"],
                        audience: _config["JWT:ValidAudience"],
                        claims: Claims,
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: SigningCredentials


                        );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(MyToken),
                        Expiration = MyToken.ValidTo,
                        UserName = login.UserName,
                        Role = roles[0]

                    });
                }
                return Unauthorized();
            }
            return Ok();
        }
    }
}
