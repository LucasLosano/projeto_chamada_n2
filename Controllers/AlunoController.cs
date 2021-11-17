using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volare.DAO;
using Volare.Models;

namespace Volare.Controllers
{
    public class AlunoController : SuperController<AlunoViewModel>
    {
        public AlunoController()
        {
            DAO = new AlunoDAO();
        }

        public IActionResult Table(AlunoViewModel model)
        {
            return PartialView("Table",DAO.SelectAll(model));
        }
        public IActionResult Aluno(int id)
        {
            ViewBag.AlunoId = id;
            return View(DAO.SelectById(id));
        }

        protected override void ValidateModel(AlunoViewModel model)
        {
            ModelState.Clear();

            if(string.IsNullOrEmpty(model.Nome))
            {
                ModelState.AddModelError("Nome", "Nome não pode ser vazio.");
            }

            if(((AlunoDAO)DAO).turmaDAO.SelectAll(new TurmaViewModel(){ Id = model.TurmaId}) == null)
            {
                ModelState.AddModelError("TurmaId", "Nenhuma turma existe.");
            }
        }

        protected override void PrepareView()
        {
            var turmas = (new SelectList(((AlunoDAO)DAO).turmaDAO.SelectAll(), "Id", "Nome")).ToList();
            turmas.Insert(0,new SelectListItem("Selecione uma turma","0"));
            ViewBag.Turmas = turmas;
        }
    }
}