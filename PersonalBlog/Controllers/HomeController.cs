using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Models;

namespace PersonalBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        #region access

        [HttpGet]
        public IActionResult Login() => View("~/Views/Home/Access/Login.cshtml");
        [HttpGet]
        public IActionResult Register() => View("~/Views/Home/Access/Register.cshtml");

        // post api for Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                // authentication cookie for admin log
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, username),
                    new(ClaimTypes.Role, "Admin")
                };

                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                HttpContext.SignInAsync("CookieAuth", claimsPrincipal).Wait();

                return RedirectToAction("AdminDashboard", "Home");
            }

            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
                // authentication cookie for standard users 
                var userClaims = new List<Claim>
            {
                new(ClaimTypes.Name , user.UserName),
                new(ClaimTypes.Role, "User")
            };

                var userClaimsIdentity = new ClaimsIdentity(userClaims, "CookieAuth");
                var userClaimsPrincipal = new ClaimsPrincipal(userClaimsIdentity);

                HttpContext.SignInAsync("CookieAuth", userClaimsPrincipal).Wait();

                return RedirectToAction("UserDashboard", "Home");
            }
            catch
            {
                ViewBag.ErrorMessage = "Invalid username or password";
                return View("~/Views/Home/Access/Login.cshtml");
            }
        }

        // post api for register
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.Role = Role.User; // default
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // logout method
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("CookieAuth").Wait();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminDashboard() => View("~/Views/Home/Admin/AdminDashboard.cshtml");

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult UserDashboard()
        {
            var articles = _context.Articles.ToList();
            return View("~/Views/Home/User/UserDashboard.cshtml", articles);
        }

        #endregion

        #region Article

        // publish article
        [HttpPost]
        public IActionResult AddArticle(Article article)
        {
            if (ModelState.IsValid)
            {
                article.PublishedAt = DateTime.Now;
                _context.Articles.Add(article);
                _context.SaveChanges();
                return RedirectToAction("AdminDashboard");
            }
            return View(article);   
        }

        // method to see the add article page
        [HttpGet]
        public IActionResult AddArticle() => View("~/Views/Home/Admin/AddArticle.cshtml");

        // show all articles for admin
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult ShowArticlesAdmin()
        {
            List<Article> articles = _context.Articles.ToList();
            if(articles.Count > 0)
                return View("~/Views/Home/Admin/ShowArticles.cshtml", articles);
            return View("~/Views/Home/Admin/ShowArticles.cshtml");
        }

        // show all articles for admin
        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult ShowArticlesUser()
        {
            List<Article> articles = _context.Articles.ToList();
            if (articles.Count > 0)
                return View("~/Views/Home/User/UserDashboard.cshtml", articles);
            return View("~/Views/Home/User/UserDashboard.cshtml");
        }



        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult SeeSingleArticle(int id)
        {
            try
            {
                Article? article = _context.Articles.FirstOrDefault(a => a.Id == id);
                return View("~/Views/Home/User/SeeSingleArticle.cshtml", article);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult SeeSingleArticleAdmin(int id)
        {
            try
            {
                Article? article = _context.Articles.FirstOrDefault(a => a.Id == id);
                return View("~/Views/Home/Admin/SeeSingleArticleAdmin.cshtml", article);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditArticle(Article updatedArticle)
        {
            try
            {
                var article = _context.Articles.FirstOrDefault(a => a.Id == updatedArticle.Id);
                if (article == null)
                {
                    return NotFound();
                }

                article.Title = updatedArticle.Title;
                article.Content = updatedArticle.Content;

                _context.SaveChanges(); 

                return View("~/Views/Home/Admin/AdminDashboard.cshtml"); 
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditArticle(int id) 
        {
            try
            {
                Article? article = _context.Articles.FirstOrDefault(a => a.Id == id);
                return View("~/Views/Home/Admin/EditArticle.cshtml", article);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteArticle(int id)
        {
            try
            {
                _context.Articles.Remove(_context.Articles.FirstOrDefault(a => a.Id == id));
                _context.SaveChanges();
                return View("~/Views/Home/Admin/AdminDashboard.cshtml");
            }
            catch
            {
                return NotFound();
            }
        }

        #endregion

        [HttpGet]
        public IActionResult Index() => View("~/Views/Home/Index.cshtml");

        [HttpGet]
        public IActionResult Privacy() => View("~/Views/Home/Privacy.cshtml");

        [HttpGet]
        public IActionResult BlogInfo() => View("~/Views/Home/BlogInfo.cshtml");

        #region error-handling

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    
        #endregion
    }
}
