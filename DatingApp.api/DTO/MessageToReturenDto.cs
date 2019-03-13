using System;

namespace DatingApp.api.DTO {
    public class MessageToReturenDto {
        public int Id { get; set; }
        public int Senderid { get; set; }
        public string SenderKnownAs { get; set; }
        public string SenderPhotoUrl { get; set; }

        public int RecipientId { get; set; }
        public string RecipientKnownAs { get; set; }
        public string RecipientPhotoUrl { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }
        public bool IsRead { get; set; }
        public string Context { get; set; }
    }
}