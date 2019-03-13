using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DatingApp.api.Data;
using DatingApp.api.Models;
using Newtonsoft.Json;

namespace DatingApp.api.Controllers
{
    public class SeedDataJson
    {
       

        public SeedDataJson(DataContext context)
        {
            _context = context;
        }


        public DataContext _context { get; set; }

        public void SeedData()
        {   
            if(! _context.Users.Any()) {
                 var userFile = File.ReadAllText("Data/DataSeed.json");
            var Users = JsonConvert.DeserializeObject<List<User>>(userFile);
            foreach (var user in Users)
            {
                byte[] passwordSelt, passwordHash;
                CearteHashPaasword("password", out passwordSelt, out passwordHash);
                user.passwordHash =passwordHash;
                user.passwordSalt=passwordSelt;
                user.UserName=user.UserName.ToLower();
                _context.Users.Add(user);
            }

                _context.SaveChanges();
            }
           


        }

        private void CearteHashPaasword(string v, out byte[] passwordSelt, out byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSelt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(v));

            }
        
        }
    }
}