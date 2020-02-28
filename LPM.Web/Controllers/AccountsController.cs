using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LPM.Web.Context;
using LPM.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace LPM.Web.Controllers
{
    public class AccountsController : Controller
    {
        LPMContext db = null;
        public AccountsController()
        {
            db = new LPMContext();
        }
        public IActionResult Login()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View(new LoginViewModel() { UserName = "gokul.mahajan@zensar.com", Password = "Admin@123" });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                var user = db.User.Where(x => x.EmailAddress == loginViewModel.UserName && x.Password == loginViewModel.Password).FirstOrDefault();
                if (user != null)
                {
                    var identity = new ClaimsIdentity(new[] {
                                                            new Claim(ClaimTypes.Name, user.Name),
                                                            new Claim(ClaimTypes.Email, user.EmailAddress),
                                                              new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                                                            new Claim(ClaimTypes.Role, user.Role),
                                                        }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);


                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    db.UserLogin.Add(new UserLogin() { LoginDate = DateTime.Now, UserId = user.Id });
                    await db.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("UserName", "Username and Password not valid");
                    return RedirectToAction("Login");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorNotLoggedIn");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Accounts");
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            try
            {
                db.User.Add(user);
                await db.SaveChangesAsync();
                return await Login(new LoginViewModel() { UserName = user.EmailAddress, Password = user.Password });
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorNotLoggedIn");
            }
        }

        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult ErrorAccessDenied()
        {
            return View();
        }

    
        public IActionResult ErrorNotLoggedIn()
        {
            return View();
        }
    }
}