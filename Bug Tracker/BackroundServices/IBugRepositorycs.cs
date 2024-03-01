using Bug_Tracker.Models;
using Microsoft.EntityFrameworkCore;

namespace Bug_Tracker.BackroundServices
{
    public interface IBugRepositorycs
    { 
        Bug GetById(int id);    
        IEnumerable<Bug> GetAllBugs();
        void Add(Bug bug);  
        void Update(Bug bug);   
        void Delete(Bug bug);
    }

    public class BugRepository : IBugRepositorycs
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Bug> _dbSet;

        public BugRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Bug>();

        } 

        public Bug GetById(int id)
        {
            return _dbSet.Find(id); 
        }

        public IEnumerable<Bug> GetAllBugs()
        {
            return _dbSet.ToList(); 
        } 

        public void Add(Bug bug) 
        { 
            _dbSet.Add(bug);
        
        } 
        public void Update(Bug bug)
        {
            _context.Entry(bug).State = EntityState.Modified;
        }

        public void Delete(Bug bug)  
        {
              _dbSet.Remove(bug);
        
        }

    }
    
}
