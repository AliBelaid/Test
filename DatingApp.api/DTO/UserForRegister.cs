using System;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.api.DTO {
    public class UserForRegister {
        [Required]
        public string UserName { get; set; }

        [StringLength (8, MinimumLength = 4, ErrorMessage = "You moust specify password between 4 to 8 characters")]
        public string Password { get; set; }

        [Required]
        public string KnownAs { get; set; }

        [Required]
        public string Gender { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        public UserForRegister () {
            Created = DateTime.Now;

            LastActive = DateTime.Now;

        }
    }
}