using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.api.Models
{
    public class User
    {

      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string UserName { get; set; }
        public string KnownAs { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Interests { get; set; }
        public string LookingFor { get; set; }
        public string Introduction { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public string city { get; set; }
        public string Country { get; set; }
        public ICollection<Likes> Likers { get; set; }
       public ICollection<Likes> Likees { get; set; }
       public ICollection<Message> MessageSent {get;set;}
       public ICollection<Message> MessageReceived {get;set;}

    }
}