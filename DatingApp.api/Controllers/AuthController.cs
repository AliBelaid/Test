using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.api.Data;
using DatingApp.api.DTO;
using DatingApp.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.api.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        public readonly IAuthRepository _repo;

        public IConfiguration _Config { get; set; }
        public IMapper _mapper { get; set; }

        public AuthController (IAuthRepository repo, IConfiguration config, IMapper mapper) {
            _mapper = mapper;
            _repo = repo;
            _Config = config;
        }

        [HttpPost ("register")]
        public async Task<IActionResult> Register ([FromBody] UserForRegister userForRegister) {
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            userForRegister.UserName = userForRegister.UserName.ToLower ();
            if (await _repo.UserExists (userForRegister.UserName)) {
                return BadRequest ("userِ aready Exists");
            }
            // var userToCreate = new User {
            //     UserName = userForRegister.UserName
            // };
            var userToCreate =_mapper.Map<User>(userForRegister);
            var CreateUser = await _repo.Register (userToCreate, userForRegister.Password);
            var userDetailes =_mapper.Map<UserDetailedDto>(CreateUser);
            return CreatedAtRoute("GetUser" ,new {Controller="User",id=userDetailes.Id},userDetailes) ;
        }

        [HttpPost ("Login")]
        public async Task<IActionResult> Login ([FromBody] UserToLogin userToLogin) {
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            var user = await _repo.Login(userToLogin.UserName.ToLower(), userToLogin.Password);
            if (user == null) {
                return Unauthorized ();
            }
            var claims = new [] {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString ()),
                new Claim (ClaimTypes.Name, user.UserName),

            };
            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_Config.GetSection ("AppSetting:Token").Value));
            var creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha512Signature);

            var tokendescriper = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.Now.AddSeconds (60 * 60),
                SigningCredentials = creds
            };

            var TokenHandler = new JwtSecurityTokenHandler ();
            var token = TokenHandler.CreateToken (tokendescriper);
            var userToReturen = _mapper.Map<UserForListDto>(user);

            return Ok (new {
                token = TokenHandler.WriteToken (token)  ,userToReturen
            });

        }

    }
}