using System;
using System.Threading.Tasks;
using DatingApp.api.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
           _context = context;

        } 
        public async Task<User> Login(string username, string password)
        {
            var user= await _context.Users.Include(p =>p.Photos).FirstOrDefaultAsync(x=>x.UserName==username);
            if(user==null)
            return null;
              if(!virfypasswoordhash(user.passwordSalt,user.passwordHash,password)){
               return null;}
            return user;
        }

        private bool virfypasswoordhash(byte[] passwordSalt, byte[] passwordHash, string password)
        {
          using (var x = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
               var  computeHash = x.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
               for (int i = 0; i < computeHash.Length; i++)
               {
                    if(passwordHash[i]!=computeHash[i]){
                    return false;
                }
               }

                    return true;

            }
        }

        public async Task<bool> UserExists(string username)
        {
         if( await _context .Users.AnyAsync(x=>x.UserName==username)){
             return true;
         };
  return false;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] Passwordhash;
            byte[] Passwordsalt;

            CreatPasseordHash(password, out Passwordhash, out Passwordsalt);
             user.passwordSalt = Passwordsalt;
            user.passwordHash = Passwordhash;
 
           await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreatPasseordHash(string password, out byte[] passwordhash, out byte[] passwordsalt)
        {
            using (var x = new System.Security.Cryptography.HMACSHA512())
            {
                passwordsalt = x.Key;
                passwordhash = x.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }
    }
}