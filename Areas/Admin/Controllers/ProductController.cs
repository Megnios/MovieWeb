using Microsoft.AspNetCore.Mvc;
using MovieWeb.Areas.Admin.Models;
using MovieWeb.Data;

namespace MovieWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _db.Products.ToList();
            return View(objProductList);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (obj.Name == obj.Director.ToString())
            {
                ModelState.AddModelError("name", "Display Order cannot exactly match the Name");

            }

            if (ModelState.IsValid)
            {
                _db.Products.Add(obj);
                _db.SaveChanges();
                TempData["Succes"] = "Product created succesfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Product? productFromDb = _db.Products.FirstOrDefault(x => x.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            
            return View(productFromDb);
        }

        [HttpPost]

        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Update(product);
                _db.SaveChanges();
                TempData["Success"] = "Product updated succesfully";
                return RedirectToAction("Index");
            }
            return Index();
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Product? productFromDb = _db.Products.FirstOrDefault(x => x.Id == id);

            if(productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
            
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product product = _db.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Remove(product);
                _db.SaveChanges();
                TempData["Succes"] = "Product Deleted succesfully";
                return RedirectToAction("Index");
            }

            return Index();
        }
    }
}
