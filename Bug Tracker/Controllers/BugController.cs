using Bug_Tracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bug_Tracker.Controllers
{
    public class BugController : Controller
    {
        private readonly ApplicationDbContext _context; 

        public BugController(ApplicationDbContext context)
        {
            _context = context;
        }

      
    }
}
 