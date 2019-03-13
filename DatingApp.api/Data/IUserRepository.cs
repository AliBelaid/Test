using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.api.Helpers;
using DatingApp.api.Models;

namespace DatingApp.api.Data
{
    public interface IUserRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<PageList<User>> GetUsers(UsersParems userParem);
        Task<User> GetUser(int Id);
        Task<bool> SaveAll();
        Task <Photo> GetPhoto(int Id);
        Task<Photo> GetMainPhoto(int UserId);
        Task<Likes> GetLike(int userId,int recipientId);
        Task<PageList<Message>> GetMessages(MessageParems messageParem);
        Task <Message> GetMessage(int id);
        Task <IEnumerable<Message>> GetMessageThread(int SenderId,int RecipientId);
    }
}