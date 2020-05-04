using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Art_ShopVF.Models;
using Microsoft.AspNetCore.Identity;

namespace Art_ShopVF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ArtShopDBContext dBContext;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private Task<AppUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);
        public HomeController(ILogger<HomeController> logger, ArtShopDBContext dBContext, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            this.dBContext = dBContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            var role = dBContext.UserRoles.SingleOrDefault(x => x.UserId == userId);
            if (role != null)
            { 
                var RoleName = dBContext.Roles.SingleOrDefault(x => x.Id == role.RoleId);
                ViewData["UserRole"] = RoleName.Name;
            }
            var listOfProducts = dBContext.Product.OrderByDescending(x => x.Rate).ToList();
            ViewData["HighProduct"] = listOfProducts.Take(3).ToList();
            
            return View(listOfProducts);
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
