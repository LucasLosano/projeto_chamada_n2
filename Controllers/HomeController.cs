using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Volare.DAO;
using Volare.Helper;
using Volare.Models;

namespace Volare.Controllers
{
    public class HomeController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.Logged = false;
            if (ControllerHelper.IsUserOn(HttpContext.Session))
            {
                ViewBag.Logged = true;
                ViewBag.Username = ControllerHelper.GetUsername(HttpContext.Session);
            }

            base.OnActionExecuting(context);
        }
        UserDAO userDAO;
        public HomeController()
        {
            userDAO = new UserDAO();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login(UserViewModel userViewModel)
        {
            if (userDAO.IsUserValid(userViewModel))
            {
                HttpContext.Session.SetString("Logged", "true");
                HttpContext.Session.SetString("Username", userViewModel.Username);
                return RedirectToAction("index", "Home");
            }
            else
            {
                ModelState.AddModelError("Error","Usuário ou senha inválidos");
                return View("Index",userViewModel);
            }
        }
        public IActionResult Register(UserViewModel userViewModel)
        {
            Validate(userViewModel);
            if (!ModelState.IsValid)
            {
                return RedirectToAction("index", userViewModel);
            }

            HttpContext.Session.SetString("Logged", "true");
            HttpContext.Session.SetString("Username", userViewModel.Username);
            userDAO.Insert(userViewModel);
            return RedirectToAction("Index");
        }

        private void Validate(UserViewModel userViewModel)
        {
            ModelState.Clear();

            if(userDAO.IsUsernameInUse(userViewModel.Username))
            {
                ModelState.AddModelError("Error","Usuário em uso.");
            }
            if(userViewModel.ConfirmPassword != userViewModel.Password)
            {
                ModelState.AddModelError("Error","As senhas não batem.");
            }
        }

        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        
    }
}
