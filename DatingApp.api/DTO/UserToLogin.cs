namespace DatingApp.api.DTO


{using System.ComponentModel.DataAnnotations;
    public class UserToLogin
   {
  [Required]
   public string UserName { get; set; }
 [Required]
   public string Password { get; set; }   
    }
}