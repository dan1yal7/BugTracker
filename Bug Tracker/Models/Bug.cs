using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bug_Tracker.Models
{
    public class Bug
    {
        [Key] 
        public int BugId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; } 
        public BugPriority Priority { get; set; } 
        public BugStatus BugStatus { get; set; } 
        public DateTime Date { get; set; } 
        public DateTime UpdateAt { get; set; }
        public int CreatedByUserId { get; set; }  
        public int AssignedToUserId { get; set; }

    } 
    public enum BugPriority
    {
        Low,
        Medium,
        High,
        Critical    
    } 

    public enum BugStatus
    {

        Open,
        InProgress,
        Resolved,
        Closed
    }
}

/// Написать полное представление для BugController и его методов edit details delete и тд 
/// После приступить к UserController и его методам   
/// Разобраться с Bootstrap CSS HTML 
