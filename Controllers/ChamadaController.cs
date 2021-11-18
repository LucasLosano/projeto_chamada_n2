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
            ViewBag.AulaId = aulaId;
            PrepareView();
            return View();
        }

        public IActionResult Table(ChamadaViewModel Model)
        {
            return PartialView(DAO.SelectAll(Model));
        }

        public IActionResult Teste()
        {
            ApiHelper.InitializeClient();
            string url = "https://pokeapi.co/api/v2/pokemon/bulbasaur";
            using(HttpResponseMessage response = ApiHelper.ApiClient.GetAsync(url).Result)
            {
                if(response.IsSuccessStatusCode)
                {
                    string resposta = response.Content.ReadAsStringAsync().Result;
                    return Content(resposta);
                }
            }
            return Ok();
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