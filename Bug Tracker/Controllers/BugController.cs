using Bug_Tracker.BackroundServices;
using Bug_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
                _bugrepository.SaveChanges();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditConfirmed(int id, Bug bug)
        {
          
            if (ModelState.IsValid)
            {
               
                var bg = _bugrepository.GetById(id);
                _bugrepository.Update(bug);
                _bugrepository.SaveChanges();
                return RedirectToAction(nameof(Index)); 

              
            } 

            foreach(var item in ModelState)
            {
                if(item.Value.ValidationState == ModelValidationState.Invalid)
                {
                    string errorMessages = " "; 

                    errorMessages = $"{errorMessages}\nОшибки для свойства {item.Key}:\n";
                   
                    foreach (var error in item.Value.Errors)
                    {
                        errorMessages = $"{errorMessages}{error.ErrorMessage}\n";
                    }

                    return View(errorMessages);
                }
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
            _bugrepository.SaveChanges();

            return RedirectToAction(nameof(Index));
        } 
        
    }
}
  
  //Исправить баг с сохранением - closed
  //подправить поле описание в методе добавления бага на самом сайте, сделать шире, то есть,чтобы можно было отпустить вниз строку
  // Разобраться, что происходит с датами создания и обновления.