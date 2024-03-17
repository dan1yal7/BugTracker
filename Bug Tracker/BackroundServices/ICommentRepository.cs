using Bug_Tracker.Models;
using Microsoft.EntityFrameworkCore;

namespace Bug_Tracker.BackroundServices
{
    public interface ICommentRepository
    { 
        Comment GetById(int id);
        IEnumerable<Comment> GetAll();  
        void Add(Comment comment); 
        void Update(Comment comment); 
        void Delete(Comment comment);

    }

    public class CommentRepository : ICommentRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly DbSet<Comment> _dbSet; 

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Comment>();
        }

        public void Add(Comment comment)
        {
            _dbSet.Add(comment);
        }

        public void Delete(Comment comment)
        {
            _dbSet.Remove(comment);
        }

        public IEnumerable<Comment> GetAll()
        {
            return _dbSet.ToList();
        }

        public Comment GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(Comment comment)
        {
           _context.Entry(comment).State = EntityState.Modified;
        }
    }
}
