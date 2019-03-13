using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace DatingApp.api.Helpers {
    public class PhotoCreationeDto {

        public string Url { get; set; }
    
        public IFormFile File { get; set; }
        public DateTime DateAdd { get; set; }
        public string description { get; set; }
        public string PhotoId { get; set; }
        public PhotoCreationeDto()
        {
            DateAdd= DateTime.Now;
        }

    }
}