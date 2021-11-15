using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetoN2.DAO;
using ProjetoN2.Helper;
using ProjetoN2.Models;

namespace Volare.Controllers
{
    public class ChamadaController : Controller
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
        
        ChamadaDAO DAO = new ChamadaDAO();
        public IActionResult Index(int aulaId)
        {
            ViewBag.AulaId = aulaId;
            PrepareView();
            return View();
        }

        public IActionResult Table(ChamadaViewModel Model)
        {
            return PartialView(DAO.SelectAll(Model));
        }

        public void PrepareView()
        {
            var materias = (new SelectList(DAO.materiaDAO.SelectAll(), "Id", "Nome")).ToList();
            materias.Insert(0,new SelectListItem("Selecione uma materia","0"));
            ViewBag.Materias = materias;
            
            var turmas = (new SelectList(DAO.turmaDAO.SelectAll(), "Id", "Id")).ToList();
            turmas.Insert(0,new SelectListItem("Selecione uma turma","0"));
            ViewBag.Turmas = turmas;
        }
    }
}