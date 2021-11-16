using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Volare.DAO;
using Volare.Helper;

namespace Volare.Controllers
{
    public class DashboardController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!ControllerHelper.IsUserOn(HttpContext.Session))
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
        DashboardDAO DAO = new DashboardDAO();
        public IActionResult Turma(int id)
        {
            return Json(DAO.SelectAll(id,"sp_turma_dashboard"));
        }
        public IActionResult Aluno(int id)
        {
            return Json(DAO.SelectAll(id,"sp_aluno_dashboard"));
        }
    }
}