using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volare.DAO;
using Volare.Helper;
using Volare.Models;

namespace Volare.Controllers
{
    public class ChamadaController : Controller
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
        
        ChamadaDAO DAO = new ChamadaDAO();
        public IActionResult Index(int aulaId)
        {
            DAO.InsertFromMongo();
            ViewBag.AulaId = aulaId;
            return View();
        }

        public IActionResult Table(ChamadaViewModel Model)
        {
            return PartialView(DAO.SelectAll(Model));
        }
    }
}