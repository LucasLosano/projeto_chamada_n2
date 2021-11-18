using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Volare.DAO;
using Volare.Models;

namespace Volare.Controllers
{
    public class TurmaController : SuperController<TurmaViewModel>
    {
        public TurmaController()
        {
            DAO = new TurmaDAO();
        }

        public IActionResult Turma(int id)
        {
            ViewBag.TurmaId = id;
            return View(DAO.SelectAll(new TurmaViewModel(){Id = id})[0]);
        }

        protected override void ValidateModel(TurmaViewModel model)
        {
            ModelState.Clear();

            if(model.Semestre < 1 || 10 < model.Semestre)
            {
                ModelState.AddModelError("Semestre", "Semestre deve estar entre 1 e 10");
            }
            
            if(((TurmaDAO)DAO).cursoDAO.SelectById(model.CursoId) == null)
            {
                ModelState.AddModelError("CursoId", "Curso não existe");
            }

            if(ModelState.IsValid && DAO.SelectAll().Where(t => t.CursoId == model.CursoId && t.Semestre == model.Semestre ).Count() != 0)
            {
                ModelState.AddModelError("CursoId", "Já existe uma turma com esse curso e semestre");
            }
        }

        protected override void PrepareView()
        {
            ViewBag.Cursos = ((TurmaDAO)DAO).cursoDAO.SelectAll();
        }
    }
}