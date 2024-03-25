using Bug_Tracker.BackroundServices;
using Bug_Tracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bug_Tracker.Controllers
{ 
    public class BugController : Controller
    {
        private readonly IBugRepositorycs _bugrepository; 

        public BugController(IBugRepositorycs bugrepository)
        {
            _bugrepository = bugrepository;
        }  


        public IActionResult Index ()
        {
            var bugs = _bugrepository.GetAllBugs();
            return View(bugs);
        }
         
        //GET: /BUG/Create
        public IActionResult Create()
        {
            return View();
        } 

        //POST: /BUG/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create(Bug bug)
        { 
            if(ModelState .IsValid)
            {
                _bugrepository.Add(bug);
                   return RedirectToAction(nameof(Index)); 
            } 
            return View(bug);
           
        }

        //GET: /Bug/Edit{id} 

        public IActionResult Edit(int id)
        {
            var bug = _bugrepository.GetById(id);
            if(bug == null)
            {
                return NotFound();
            }
            return View(bug);   
        }

        //POST: /Bug/Edit{id} 

        public IActionResult Change(int id, Bug bug)
        {
            if( id != bug.BugId)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                _bugrepository.Update(bug); 
                return RedirectToAction(nameof(Index)); 
               
            }
            return View(bug);
        } 

        
        //GET: /Bug/Delete{id}
        
        public IActionResult Delete(int id)
        {
            var bug = _bugrepository.GetById(id); 

            if(bug == null)
            {
                return NotFound();
            } 
            return View(bug);
        }

        //POST: /Bug/Delete/{id} 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] 

        public IActionResult DeleteConfirmed(int id)
        {
            var bug = _bugrepository.GetById(id);
            _bugrepository.Delete(bug);

            return RedirectToAction(nameof(Index));
        } 

    }
}
  
// Исправить ошибку с сохраненим при редактировании бага
// Создать представления для Create и разобраться с удалением 