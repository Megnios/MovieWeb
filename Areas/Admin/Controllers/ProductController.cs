using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieWeb.Areas.Admin.Models;
using MovieWeb.Areas.Admin.Models.ViewModels;
using MovieWeb.Data;
using System.Collections.Generic;

namespace MovieWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _db.Products.ToList();
            return View(objProductList);

        }

        public IActionResult Upsert(int? id)
        {
            
            ProductVM productVm = new()
            {
                CategoryList = _db.Categories.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                Product = new Product()
            };

            if (id == null || id == 0)
            {
                return View(productVm);
            }
            else
            {
                productVm.Product = _db.Products.FirstOrDefault(p => p.Id == id);
                return View(productVm);
            }


        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVm, IFormFile? file)
        { 

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVm.Product.imageUrl = @"\images\product" + fileName;
                }

                _db.Products.Add(productVm.Product);
                _db.SaveChanges();
                TempData["Succes"] = "Product created succesfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVm.CategoryList = _db.Categories.ToList().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                });
                return View(productVm); 
            }
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
