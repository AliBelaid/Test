using System;

namespace DatingApp.api.DTO
{
    public class PhotoReturenDto
    {
          public int Id { get; set; }
        public string Url { get; set; }
        public DateTime DateAdd { get; set; }
        public string description { get; set; }
        public Boolean isMain { get; set; }
        public string PhotoId { get; set; }
    }
}