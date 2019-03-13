using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.api.Models
{
    public class Photo
    {
 

      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime DateAdd { get; set; }
        public string description { get; set; }
        public Boolean isMain { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string PhotoId { get; set; }

    }
}