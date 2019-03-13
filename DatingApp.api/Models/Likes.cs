namespace DatingApp.api.Models {
    public class Likes {
        public int LikerId { get; set; }
        public int LikeeId { get; set; }
        public User Likers { get; set; }
        public User Likees { get; set; }

    }
}