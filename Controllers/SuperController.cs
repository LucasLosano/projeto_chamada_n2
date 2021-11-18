using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Volare.DAO;
using Volare.Helper;
using Volare.Models;

namespace Volare.Controllers
{
    public class SuperController<T> : Controller where T : SuperViewModel
    {
        protected SuperDAO<T> DAO { get; set; }
        protected string IndexViewName { get; set; } = "Index";
        protected string FormViewName { get; set; } = "Form";
        protected bool ExigeAutenticacao { get; set; } = true;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (ExigeAutenticacao && !ControllerHelper.IsUserOn(HttpContext.Session))
            {
                ViewBag.Logged = false;
                context.Result = RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Logged = true;
                ViewBag.Username = ControllerHelper.GetUsername(HttpContext.Session);
                base.OnActionExecuting(context);
            }
        }

        public virtual IActionResult Index()
        {
            try
            {
                var modelList = DAO.SelectAll();
                PrepareView();
                return View(IndexViewName, modelList);
            }
            catch (System.Exception e)
            {
                return View("Error", new ErrorViewModel(e.ToString()));
            }
        }
        protected virtual void PrepareView()
        {
            return;
        }

        public virtual IActionResult Create()
        {
            try
            {
                PrepareView();
                return View(FormViewName);
            }
            catch (System.Exception e)
            {
                return View("Error", new ErrorViewModel(e.ToString()));
            }
        }

        public virtual IActionResult Update(int id)
        {
            try
            {
                var model = DAO.SelectById(id);
                if(model == null)
                {
                    return RedirectToAction(IndexViewName);
                }
                PrepareView();
                return View(FormViewName, model);
            }
            catch (System.Exception e)
            {
                return View("Error", new ErrorViewModel(e.ToString()));
            }
        }
        public virtual IActionResult Delete(int id)
        {
            try
            {
                using(var transaction = new System.Transactions.TransactionScope())
                {
                    DAO.Delete(id);
                    transaction.Complete();
                }

                return RedirectToAction(IndexViewName);
                    
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                ViewBag.SQLError = "Ocorreu um erro inesperado. Por favor entre em contato com a equipe VOLARE.";
                var modelList = DAO.SelectAll();
                PrepareView();
                return View("Index", modelList);
            }
            catch (System.Exception e)
            {
                return View("Error", new ErrorViewModel(e.ToString()));
            }
        }

        public virtual IActionResult Save(T model)
        {
            try
            {
                using(var transaction = new System.Transactions.TransactionScope())
                {
                    ValidateModel(model);
                    if(!ModelState.IsValid)
                    {
                        PrepareView();
                        return View(FormViewName, model);
                    }

                    if(model.Id == 0)
                    {
                        DAO.Insert(model);
                    }
                    else
                    {
                        DAO.Update(model);
                    }

                    transaction.Complete();
                }
                return RedirectToAction(IndexViewName);
            }
            catch (System.Exception e)
            {
                return View("Error", new ErrorViewModel(e.ToString()));
            }
        }

        protected virtual void ValidateModel(T model)
        {
            return;
        }
    }
}
