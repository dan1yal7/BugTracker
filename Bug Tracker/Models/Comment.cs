namespace Bug_Tracker.Models
{
    public class Comment
    { 
        public int CommentId { get; set; }
        public string CommentText { get; set; } 
        public DateTime Date { get; set; }
        public int UserId { get; set; } 
        public int BugId { get; set; }
    }
}
    