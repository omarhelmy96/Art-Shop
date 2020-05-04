using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Art_ShopVF.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Art_ShopVF.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Art_ShopVF.Controllers
{
    
    public class ProductsController : Controller
    {
        private readonly ArtShopDBContext _context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private Task<AppUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);
        public ProductsController(ArtShopDBContext context, IHostingEnvironment hostingEnvironment
            , UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // GET: Products
        [Authorize("Seller")]
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            var artShopDBContext = _context.Product.Where(p => p.SellerId==userId);
            return View(await artShopDBContext.ToListAsync());
    
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize("Seller")]
        public IActionResult Create()
        {
            ViewData["SellerId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Seller")]
        public async Task<IActionResult> Create([Bind("Type,Description,Brand,Color,Size,Price,Photos")] ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                 var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                string uniqueFileName = null;
                if (product.Photos != null && product.Photos.Count > 0)
                {

                    foreach (IFormFile photo in product.Photos)
                    {

                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");

                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                }
                Product NewProduct = new Product
                {
                    Type = product.Type,
                    Image = uniqueFileName,
                    Description = product.Description,
                    Price = product.Price,
                    Color=product.Color,
                    Size=product.Size,
                    Brand=product.Brand,
                    SellerId=userId
                };

                
                _context.Add(NewProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize("Seller")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ProductViewModel ThisProduct = new ProductViewModel 
            {
                Type=product.Type,
                Brand=product.Brand,
                Price=product.Price,
                Color=product.Color,
                Description=product.Description,
                Size=product.Size
            };
            ViewData["ProductID"] = id;
            ViewData["Image"] = product.Image;
            TempData["productid"] = id;
            TempData.Keep();
            return View(ThisProduct);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Seller")]
        public async Task<IActionResult> Edit(int id, [Bind("Type,Description,Brand,Color,Size,Price,Photos")] ProductViewModel product)
        {
            int ProductID =(int)TempData["productid"];
            if (id != ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await GetCurrentUserAsync();
                    var userId = user?.Id;
                    string uniqueFileName = null;
                    if (product.Photos != null && product.Photos.Count > 0)
                    {

                        foreach (IFormFile photo in product.Photos)
                        {

                            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");

                            uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                            photo.CopyTo(new FileStream(filePath, FileMode.Create));
                        }
                    }
                    Product product1 = _context.Product.SingleOrDefault(x => x.Id == ProductID);

                    product1.Type = product.Type;
                    product1.Description = product.Description;
                    product1.Brand = product.Brand;
                    product1.Color = product.Color;
                    product1.Size = product.Size;
                    product1.Price = product.Price;
                    product1.Image = uniqueFileName;
                    product1.SellerId = userId;
                    
                    _context.Update(product1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize("Seller")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize("Seller")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
