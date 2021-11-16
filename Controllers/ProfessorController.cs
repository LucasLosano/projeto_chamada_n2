using Microsoft.AspNetCore.Mvc;
using Volare.DAO;
using Volare.Models;

namespace Volare.Controllers
{
    public class ProfessorController : SuperController<ProfessorViewModel>
    {
        public ProfessorController()
        {
            DAO = new ProfessorDAO();
        }
        protected override void ValidateModel(ProfessorViewModel model)
        {
            ModelState.Clear();

            if(string.IsNullOrEmpty(model.Nome))
            {
                ModelState.AddModelError("Nome", "Nome não pode ser vazio.");
            }
        }
    }
}