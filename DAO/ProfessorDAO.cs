using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Volare.Models;

namespace Volare.DAO
{
    public class ProfessorDAO : SuperDAO<ProfessorViewModel>
    {
        protected override ProfessorViewModel SetModel(DataRow row)
        {
            ProfessorViewModel professorViewModel = new ProfessorViewModel();
            professorViewModel.Id = Convert.ToInt32(row["professor_id"]);
            professorViewModel.Nome = row["professor_nome"].ToString();

            return professorViewModel;
        }

        protected override SqlParameter[] SetParameters(ProfessorViewModel model)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            if(model.Id != 0)
            {
                parameters.Add(new SqlParameter("id",model.Id));
            }
            parameters.Add(new SqlParameter("nome", model.Nome));

            return parameters.ToArray();
        }

        protected override void SetTableName()
        {
            Table = "professor";
        }
    }
}