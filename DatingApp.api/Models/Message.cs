using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.api.Models
{
    public class Message
    {
 

      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Senderid { get; set; }
        public User Sender { get; set; }
        public int RecipientId { get; set; }
        public User Recipient { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }
         public bool IsRead { get; set; }
         public  string Context { get; set; }
         public bool SenderDelate { get; set; }
        public bool RecipientDelate { get; set; }
    }
}