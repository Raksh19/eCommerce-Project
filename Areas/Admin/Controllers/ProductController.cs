using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace eCommerce.Areas.Admin
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {

        private ApplicationDbContext _db;
        private IHostingEnvironment _he;
        public ProductController(ApplicationDbContext db,IHostingEnvironment he)
        {
            _db = db;
            _he = he;


        }
        public IActionResult Index()
        {
            return View(_db.Products.ToList());
        }
        //Post Index Action
        [HttpPost]
        public IActionResult Index(string pname)
        {
            var products = _db.Products.Where(c => c.Name.Contains(pname)).ToList();
            if(products==null)
            {
                return NotFound();
            }
            return View(products);
        }
        //Get Create Method
        public IActionResult Create()
        {
  
            return View();
        }
        //Post Create Method
        [HttpPost]
        public async Task<IActionResult>Create(Products Product,IFormFile image)
        {
            if(ModelState.IsValid)
            {
                if(Product.ImageFile!=null)
                {
                    string folder = "pimages/";
                    folder += Product.ImageFile.FileName;
                    string serverfolder = Path.Combine(_he.WebRootPath, folder);
                    Product.Image ="/" +folder;
                    await Product.ImageFile.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
                   
                }
                else if(Product.ImageFile == null)
                {
                    Product.Image = "/pimages/Noimage.jpg";
                }
                _db.Products.Add(Product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(Product);
        }
        //Get Edit Method
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var product = _db.Products.FirstOrDefault(c => c.Id == Id);
            return View(product); 
        }
        //Post Edit Method
        [HttpPost]
        public async Task<IActionResult> Edit(Products Product, IFormFile image)
        {
            if(ModelState.IsValid)
            {
                if (Product.ImageFile != null)
                {
                    string folder = "pimages/";
                    folder += Product.ImageFile.FileName;
                    string serverfolder = Path.Combine(_he.WebRootPath, folder);
                    Product.Image = "/" + folder;
                    await Product.ImageFile.CopyToAsync(new FileStream(serverfolder, FileMode.Create));

                }
                else if (Product.ImageFile == null)
                {
                    Product.Image = "/pimages/Noimage.jpg";
                }
                _db.Products.Update(Product);
                await _db.SaveChangesAsync();
                

            }
            return RedirectToAction(nameof(Index));

        }
       


        //Get Display Method
        public ActionResult Details(int? Id)
        {
            if(Id==null)
            {
                return NotFound();
            }
            var product = _db.Products.FirstOrDefault(c => c.Id == Id);
            if(product==null)
            {
                return NotFound();
            }
            return View(product);
            
        }
        //Get Delete Action
        public ActionResult Delete(int? Id)
        {
            if(Id == null)
            {
                return NotFound();
            }
            var product = _db.Products.FirstOrDefault(c => c.Id == Id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        //Post Delete Action
        [HttpPost]
        public async Task<IActionResult>Delete(Products Product, IFormFile image)
        {
           
            _db.Products.Remove(Product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));



        }

    }
}