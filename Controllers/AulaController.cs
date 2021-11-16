using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volare.DAO;
using Volare.Models;

namespace Volare.Controllers
{
    public class AulaController : SuperController<AulaViewModel>
    {
        public AulaController()
        {
            DAO = new AulaDAO();
        }

        public IActionResult Table(AulaViewModel model)
        {
            return PartialView("Table",DAO.SelectAll(model));
        }

        protected override void ValidateModel(AulaViewModel model)
        {
            ModelState.Clear();

            if(((AulaDAO)DAO).materiaDAO.SelectById(model.MateriaId) == null)
            {
                ModelState.AddModelError("MateriaId", "Nenhuma matéria existe");
            }

            if(((AulaDAO)DAO).turmaDAO.SelectById(model.TurmaId) == null)
            {
                ModelState.AddModelError("TurmaId", "Nenhuma turma existe");
            }

            if(((AulaDAO)DAO).salaDAO.SelectById(model.SalaId) == null)
            {
                ModelState.AddModelError("SalaId", "Nenhuma sala existe");
            }
        }

        protected override void PrepareView()
        {
            var materias = (new SelectList(((AulaDAO)DAO).materiaDAO.SelectAll(), "Id", "Nome")).ToList();
            materias.Insert(0,new SelectListItem("Selecione uma materia","0"));
            ViewBag.Materias = materias;

            var salas = (new SelectList(((AulaDAO)DAO).materiaDAO.SelectAll(), "Id", "Id")).ToList();
            salas.Insert(0,new SelectListItem("Selecione uma sala","0"));
            ViewBag.Salas = salas;
            
            var turmas = (new SelectList(((AulaDAO)DAO).turmaDAO.SelectAll(), "Id", "Id")).ToList();
            turmas.Insert(0,new SelectListItem("Selecione uma turma","0"));
            ViewBag.Turmas = turmas;

            var professores = (new SelectList(((AulaDAO)DAO).materiaDAO.professorDAO.SelectAll(), "Id", "Nome")).ToList();
            professores.Insert(0,new SelectListItem("Selecione um professor","0"));
            ViewBag.Professores = professores;
        }
    }
}