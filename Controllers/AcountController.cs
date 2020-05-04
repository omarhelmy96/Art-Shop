using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Art_ShopVF.Models;
using Art_ShopVF.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting.Internal;

namespace Art_ShopVF.Controllers
{
    public class AcountController : Controller
    {
        private ArtShopDBContext DBContext;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        
        public AcountController(ArtShopDBContext DBContext, IHostingEnvironment hostingEnvironment
            , UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            this.DBContext = DBContext;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roles = DBContext.Roles.ToList();
            ViewBag.Roles = new SelectList(roles, "Name", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photos != null && model.Photos.Count > 0)
                {
                   
                    foreach (IFormFile photo in model.Photos)
                    {
                      
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                        
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                }
                User NewDetalies = new User
                {
                    Adress = model.Adress,
                    Country = model.Country,
                    City = model.City,
                    Phone = model.Phone,
                    Image = uniqueFileName,
                    Gender = model.Gender
                };
                DBContext.User.Add(NewDetalies);
                DBContext.SaveChanges();
                int IdOFInfoUser = DBContext.User.Max(x => x.Id);
                AppUser newUser = new AppUser
                {
                    UserName=model.UserName,
                    Email=model.Email,
                    USerId= IdOFInfoUser
                };

                var result = await userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(newUser, isPersistent: false);
                    var Role = DBContext.Roles.SingleOrDefault(x=>x.Name==model.Role);
                    var UserRole = new IdentityUserRole<string>()
                    {
                        UserId= newUser.Id,
                        RoleId= Role.Id
                    };
                    DBContext.UserRoles.Add(UserRole);
                    DBContext.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                //DBContext.Users.Add(newUser);
                return RedirectToAction("Index","Home");
            }

            return RedirectToAction();
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }
    }
}