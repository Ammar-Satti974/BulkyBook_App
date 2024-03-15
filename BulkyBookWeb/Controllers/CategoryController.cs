using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objcategorylist = _db.Categories;
            return View(objcategorylist);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Disaplay order cannot match with existing name. ");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category created Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            // return on base of primary key
            var CategoryFromDB = _db.Categories.Find(id);
            // return first element from db if there are more element with same name
           // var CategoryFromDBFirst = _db.Categories.FirstOrDefault(u =>u.id = id);
            // show exception  if there are more element with same name
           // var CategoryFromDBSingle = _db.Categories.SingleOrDefault(u=>u.Id= id);
           if(CategoryFromDB == null)
            {
                return NotFound();
            }   
            return View(CategoryFromDB);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Disaplay order cannot match with existing name. ");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category updated Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // return on base of primary key
            var CategoryFromDB = _db.Categories.Find(id);
            // return first element from db if there are more element with same name
            // var CategoryFromDBFirst = _db.Categories.FirstOrDefault(u =>u.id = id);
            // show exception  if there are more element with same name
            // var CategoryFromDBSingle = _db.Categories.SingleOrDefault(u=>u.Id= id);
            if (CategoryFromDB == null)
            {
                return NotFound();
            }
            return View(CategoryFromDB);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
           var obj = _db.Categories.Find(id);
            if (obj== null)
            {
                return NotFound();
            }
            
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category deleted Successfully!";
            return RedirectToAction("Index");
            
            return View(obj);
        }
    }
}
