using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.api.Helpers;
using DatingApp.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.api.Data {
   public class UserRepository : IUserRepository {
      public UserRepository (DataContext _Repo) {
         this._Repo = _Repo;

      }
      public DataContext _Repo { get; set; }

      public void Add<T> (T entity) where T : class {
         _Repo.Add (entity);
      }
      public void Delete<T> (T entity) where T : class {
         _Repo.Remove (entity);
      }
      public async Task<User> GetUser (int id) {
         var user = await _Repo.Users.Include (m => m.Photos).FirstOrDefaultAsync (i => i.Id == id);
         return user;
      }
      public async Task<PageList<User>> GetUsers (UsersParems usersParems) {
         var users = _Repo.Users.Include (m => m.Photos).OrderByDescending (p => p.LastActive).AsQueryable ();
         if (usersParems.MinAge != 18 || usersParems.MaxAge != 99) {
            var minDto = DateTime.Today.AddYears (-usersParems.MaxAge - 1);
            var maxDto = DateTime.Today.AddYears (-usersParems.MinAge);
            users = users.Where (u => u.DateOfBirth >= minDto && u.DateOfBirth <= maxDto);
         }

         if (!string.IsNullOrEmpty (usersParems.OrderBy)) {
            switch (usersParems.OrderBy) {
               case "Created":
                  users = users.OrderByDescending (p => p.Created);
                  break;
               default:
                  users = users.OrderByDescending (p => p.LastActive);
                  break;
            }
         }
         users = users.Where (u => u.Id != usersParems.Userid);
         users = users.Where (u => u.Gender == usersParems.Gender);

         if (usersParems.Likees) {
            var listUserLikees = await GetUserLike (usersParems.Userid, usersParems.Likees);
            users = users.Where (i => listUserLikees.Contains (i.Id));
         }
         if (usersParems.Likers) {
            var listUserLikers = await GetUserLike (usersParems.Userid, usersParems.Likees);
            users = users.Where (i => listUserLikers.Contains (i.Id));
         }
         return await PageList<User>.CreateAsync (users, usersParems.PageSize, usersParems.PageNumber);
      }

      private async Task<IEnumerable<int>> GetUserLike (int userid, bool likees) {
         var user = await _Repo.Users.Include (m => m.Likees).Include (m => m.Likers).
         FirstOrDefaultAsync (i => i.Id == userid);

         if (likees) {
            return user.Likees.Where (m => m.LikerId == userid).Select (u => u.LikeeId);
         } else {
            return user.Likers.Where (m => m.LikeeId == userid).Select (u => u.LikerId);
         }
      }
      public async Task<bool> SaveAll () {
         return await _Repo.SaveChangesAsync () > 0;
      }
      public async Task<Photo> GetPhoto (int id) {
         var photo = await _Repo.Photos.FirstOrDefaultAsync (p => p.Id == id);
         return photo;
      }
      public async Task<Photo> GetMainPhoto (int userid) {
         return await _Repo.Photos.Where (u => u.UserId == userid).FirstOrDefaultAsync (p => p.isMain);
      }

      public async Task<Likes> GetLike (int userId, int recipientId) {
         return await _Repo.Likes.FirstOrDefaultAsync (u =>
            u.LikerId == userId &&
            u.LikeeId == recipientId);
      }

      public async Task<Message> GetMessage (int id) {
         return await _Repo.Messages.FirstOrDefaultAsync (i => i.Id == id);
      }

      public async Task<IEnumerable<Message>> GetMessageThread (int UserId, int RecipientId) {
         var message = await _Repo.Messages.Include (i => i.Recipient).ThenInclude (p => p.Photos)
            .Include (i => i.Sender).ThenInclude (p => p.Photos).
         Where (u => u.Senderid == UserId && u.RecipientId == RecipientId && u.SenderDelate !=true|| u.Senderid == RecipientId && u.RecipientId == UserId  && u.RecipientDelate!=true)
            .OrderByDescending (p => p.MessageSent).ToListAsync ();
         return message;
      }
      public async Task<PageList<Message>> GetMessages (MessageParems messageParem) {
         var message = _Repo.Messages.Include (i => i.Recipient).ThenInclude (p => p.Photos)
            .Include (i => i.Sender).ThenInclude (p => p.Photos).AsQueryable ();
         switch (messageParem.MessageContainer) {
            case "Inbox":
               message = message.Where (p => p.RecipientId == messageParem.Userid && p.RecipientDelate!=true);
               break;
            case "Outbox":
               message = message.Where (p => p.Senderid == messageParem.Userid && p.SenderDelate != true);
               break;
            default:
               message = message.Where (p => p.RecipientId == messageParem.Userid && p.IsRead == false && p.RecipientDelate!=true);
               break;
         }
         message = message.OrderByDescending (p => p.MessageSent);
         return await PageList<Message>.CreateAsync (message, messageParem.PageSize, messageParem.PageNumber);
      }
   }
}