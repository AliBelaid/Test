using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.api.Data;
using DatingApp.api.DTO;
using DatingApp.api.Helpers;
using DatingApp.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DatingApp.api.Controllers {
    [ServiceFilter (typeof (UserLogActivitey))]
    [Route ("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase {
        public IUserRepository _repo { get; set; }
        public IMapper _mapper { get; set; }
        public UserController (IUserRepository repo, IMapper mapper) {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers ([FromQuery] UsersParems userParem) {

            var userId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            userParem.Userid = userId;
            var userFromRepo = await _repo.GetUser (userId);
            if (string.IsNullOrEmpty (userParem.Gender)) {
                userParem.Gender = userFromRepo.Gender == "male" ? "female" : "male";
            }
            var users = await _repo.GetUsers (userParem);
            var UserReturn = _mapper.Map<IEnumerable<UserForListDto>> (users);
            Response.AddPagintainHeader (users.CurrentPage, users.PageSize, users.TotalPages, users.TotalCount);
            return Ok (UserReturn);
        }

        [HttpGet ("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser (int id) {
            var users = await _repo.GetUser (id);
            var UserReturn = _mapper.Map<UserDetailedDto> (users);
            return Ok (UserReturn);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> UpdateUser (int id, UpdateForUserDto userForUpdate) {
            if (id != int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value)) {
                return Unauthorized ();
            }
            var userFromRepo = await _repo.GetUser (id);
            _mapper.Map (userForUpdate, userFromRepo);

            if (await _repo.SaveAll ()) {
                return NoContent ();

            } else {
                throw new Exception ($"update user with {id} Filed!");
            }

        }

        [HttpPost ("{id}/like/{recipientId}")]
        public async Task<IActionResult> AddLike (int id, int recipientId) {
            if (id != int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value)) {
                return BadRequest ("the User aready likes");
            }
            var like = await _repo.GetLike (id, recipientId);
            if (like != null)
                return BadRequest ("the User aready likes");
            like = new Likes {
                LikerId = id,
                LikeeId = recipientId
            };

            _repo.Add<Likes> (like);

            if (await _repo.SaveAll ()) {
                return Ok ();
            }
            return BadRequest ("Sourry, Cant Like is failed");
        }

       
    }

}