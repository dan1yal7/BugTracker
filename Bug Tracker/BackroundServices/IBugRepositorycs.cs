using Bug_Tracker.Models;
using Microsoft.EntityFrameworkCore;

namespace Bug_Tracker.BackroundServices
{


    //Создал отдельно repository service используя Repository pattern чтобы он служил промежуточным слоем слоем между контроллером и моеим источником данных - БД
    // Интерфейс определяет контракты методов которые мой репозиторий должен реализовать. Таким образом, репозиторий предоставляет уровень абстракции над доступом к данным и позволяет легко изменять источник данных без изменения кода контроллеров. 
    public interface IBugRepositorycs
    { 
        Bug GetById(int id);    
        IEnumerable<Bug> GetAllBugs();
        void Add(Bug bug);  
        void Update(Bug bug);   
        void Delete(Bug bug);
    }
    //BugRepository предоставляет реализацию этих методов. Также как видно ниже я внедрил ApplicatonDbContext для доступа к базе данных
    // Также я отдельно опишу функцитнал каждого метода, так как это служит хорошей практикой для более глубокого понимания
    public class BugRepository : IBugRepositorycs
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Bug> _dbSet;

        public BugRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Bug>();

        } 
        /// <summary>
        /// Получение бага в списке-таблице по его id. _dbset - набор сущеностей моей модели,используется для выполнения операций CRUD  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Bug GetById(int id)
        {
            return _dbSet.Find(id); 
        }

        /// <summary>
        /// Вытаксиквает все баги в список из БД
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Bug> GetAllBugs()
        {
            return _dbSet.ToList(); 
        } 


        /// <summary>
        /// Добавдение бага
        /// </summary>
        /// <param name="bug"></param>
        public void Add(Bug bug) 
        { 
            _dbSet.Add(bug);
        
        }

        /// <summary>
        /// Update который используется для метода Edit в самом контроллере то есть используя паттерн репозиттори 
        /// мы еще используем один из принципов SOLID, один из принципов, принццип открытости-закрытости который гласит что
        /// программные сущности (классы, модули, функции и т. д.) должны быть открыты для расширения, но закрыты для модификации
        /// </summary>
        /// <param name="bug"></param>
        public void Update(Bug bug)
        {
            _context.Entry(bug).State = EntityState.Modified;
        }
        /// <summary>
        /// В методе Update(Bug bug) вы используете _context.Entry(bug).State = EntityState.Modified; для обозначения того,
        /// что сущность была изменена и нужно применить соответствующие изменения в базе данных.
        /// </summary>
        /// <param name="bug"></param>
        public void Delete(Bug bug)  
        {
              _dbSet.Remove(bug);
        
        }

    }
    
}
