using Bug_Tracker.BackroundServices;
using Bug_Tracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bug_Tracker.Controllers
{
    public class CommentController : Controller
    { 
        private readonly ICommentRepository _commentRepository;

         public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public IActionResult Index()
        {
           var comments = _commentRepository.GetAll().ToList();
            return View(comments);
        } 

        //GET: /Comment/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: /Comment/Create
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create(Comment comment)
        {
            if(ModelState.IsValid)
            {
                _commentRepository.Add(comment); 
               return RedirectToAction(nameof(Index));
            } 
            return View(comment);
        }

        //GET: /Bug/Edit{id} 

        public IActionResult Edit(int id)
        {
            var comment = _commentRepository.GetById(id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        } 

        //POST: /Comment/Edit {id} 
        public IActionResult Edit(int id, Comment comment)
        {
             if( id != comment.CommentId)
            {
                return NotFound();
            } 

             if(ModelState .IsValid)
            {
                _commentRepository.Update(comment); 
                return RedirectToAction(nameof(Index)); 
            } 
             return View(comment);
        } 

        //GET: /Comment/Edit {id} 

        public IActionResult Delete(int id)
        {
            var comment = _commentRepository.GetById(id);
           if(comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        //POST: /Comment/Delete {id} 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var comment = _commentRepository.GetById(id);
             _commentRepository.Delete(comment);
            return RedirectToAction(nameof(Index));
       
        }  
    }
}
