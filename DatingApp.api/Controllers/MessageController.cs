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
    [Route ("api/User/{userId}/[controller]")]
    [Authorize]
    [ApiController]
    public class MessageController : ControllerBase {
        public IMapper _map { get; set; }
        public IUserRepository _repo { get; set; }

        public MessageController (IMapper map, IUserRepository repo) {
            _repo = repo;
            _map = map;

        }

        [HttpGet ("{id}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage (int UserId, int id) {
            if (UserId != int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value)) {
                return BadRequest ("the User aready likes");
            }

            var messageRepo = await _repo.GetMessage (id);
            if (messageRepo == null) {
                return NoContent ();
            }
            return Ok (messageRepo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage (int UserId, CrateMessageDto messageDto) {
                var sender = await _repo.GetUser (UserId);
            if (sender.Id != int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value)) {
                return Unauthorized ();
            }

            messageDto.Senderid = UserId;

            var UserRecipient = await _repo.GetUser (messageDto.RecipientId);
            if (UserRecipient == null) {
                return BadRequest ("Could Not Send");
            }
            var message = _map.Map<Message> (messageDto);
            _repo.Add (message);
         
            if (await _repo.SaveAll ()) {
                   var messageReturen = _map.Map<MessageToReturenDto> (message);
                return CreatedAtRoute ("GetMessage", new { id = message.Id }, messageReturen);
            }
            throw new Exception ("Could not Send");
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages (int UserId, [FromQuery] MessageParems messageParem) {
             if (UserId != int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value)) {
                return Unauthorized ();
            }
            messageParem.Userid = UserId;
            var messagesFromRepo = await _repo.GetMessages (messageParem);
            var messageToReturen = _map.Map<IEnumerable<MessageToReturenDto>> (messagesFromRepo);
            Response.AddPagintainHeader (messagesFromRepo.CurrentPage, messagesFromRepo.PageSize, messagesFromRepo.TotalPages, messagesFromRepo.TotalCount);
            return Ok (messageToReturen);
        }

        [HttpGet ("Thread/{recipientId}")]
        public async Task<IActionResult> GetMessageThread (int UserId, int recipientId) {
            if (UserId != int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value)) {
                return Unauthorized ();
            }

            var messageFromRepo = await _repo.GetMessageThread (UserId, recipientId);

            var messageToReturen = _map.Map<IEnumerable<MessageToReturenDto>> (messageFromRepo);
            return Ok (messageToReturen);
        }

         [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteMessage (int userId, int id) {
            if (userId != int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value)) {
                return Unauthorized ();
            }
            var message = await _repo.GetMessage (id);

            if (message.RecipientId == userId && message.RecipientDelate == false) {
                message.RecipientDelate = true;
            }
            if (message.Senderid == userId && message.SenderDelate == false) {
                message.SenderDelate = true;
            }
            if (message.SenderDelate == true && message.RecipientDelate == true) {
                _repo.Delete (message);
            }
            if (await _repo.SaveAll ()) {
                return NoContent ();
            }
            throw new Exception (" Can not Delete Message");
        }

        [HttpPost ("{id}/Read")]
        public async Task<IActionResult> MarkMessageAsRead (int userId, int id) {
            if (userId != int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value)) {
                return Unauthorized ();
            }
            var message = await _repo.GetMessage (id);
            if (message.RecipientId != userId) {
                Unauthorized ();
            }
            message.IsRead = true;
            message.DateRead = DateTime.Now;

           await _repo.SaveAll();
           return NoContent();
        }
    }
}