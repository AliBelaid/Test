using System;

namespace DatingApp.api.DTO
{
    public class CrateMessageDto
    {
       public int Senderid { get; set; }
         public int RecipientId { get; set; }
         public DateTime MessageSent { get; set; }
          public  string Context { get; set; }
          public CrateMessageDto()
          {

              MessageSent=DateTime.Now;
          }
        
    }
}