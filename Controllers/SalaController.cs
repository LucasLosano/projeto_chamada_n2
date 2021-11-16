using Microsoft.AspNetCore.Mvc;
using Volare.DAO;
using Volare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Volare.Controllers
{
    public class SalaController : SuperController<SalaViewModel>
    {
        public SalaController()
        {
            DAO = new SalaDAO();
        }
        protected override void ValidateModel(SalaViewModel model)
        {
            ModelState.Clear();

            if (model.Capacidade < 5)
            {
                ModelState.AddModelError("Capacidade", "Capacidade não pode ser menor que 5.");
            }
        }
    }
}
