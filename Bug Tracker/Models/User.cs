using System.ComponentModel.DataAnnotations.Schema;

namespace Bug_Tracker.Models
{
    public class User
    {  
        public int UserId { get; set; }
        public string UserName { get; set; } 

        public string Email  {  get; set; }

        public string HashedPassword { get; set; }   

        public UserRole Role { get; set; }

    }

    public enum UserRole
    {
        Administrator,
        RegularUser
        
    }
}
