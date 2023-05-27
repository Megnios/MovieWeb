using Microsoft.AspNetCore.Mvc;
using MovieWeb.Data;
using MovieWeb.Models;

namespace MovieWeb.Controllers
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
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Display Order cannot exactly match the Name");
                
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["Succes"] = "Category created succesfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            
            Category? CategoryFromDb = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (CategoryFromDb == null)
            {
                return NotFound();
            }

            return View(CategoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _db.Update(obj);
                _db.SaveChanges();
                TempData["Succes"] = "Category updated succesfully";
                return RedirectToAction("Index");
            }
            
            return View();
           
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Category? CategoryFromDb = _db.Categories.FirstOrDefault(c => c.Id == id);

            if (CategoryFromDb == null)
            {
                return NotFound();
            }

            return View(CategoryFromDb);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? CategoryFromDb = _db.Categories.Find(id);

            if (CategoryFromDb == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(CategoryFromDb);
            _db.SaveChanges();
            TempData["Succes"] = "Category deleted succesfully";
            return RedirectToAction("Index");

        }

    }
}
