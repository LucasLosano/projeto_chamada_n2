using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Volare.Enums;
using Volare.Models;

namespace Volare.DAO
{
    public class CursoDAO : SuperDAO<CursoViewModel>
    {
        protected override CursoViewModel SetModel(DataRow row)
        {
            CursoViewModel cursoViewModel = new CursoViewModel();
            cursoViewModel.Id = Convert.ToInt32(row["curso_id"]);
            cursoViewModel.Nome = row["curso_nome"].ToString();
            cursoViewModel.Periodo = (EPeriodo)Convert.ToInt32(row["curso_periodo"]);
            
            return cursoViewModel;
        }

        protected override SqlParameter[] SetParameters(CursoViewModel model)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            if(model.Id != 0)
            {
                parameters.Add(new SqlParameter("id",model.Id));
            }
            parameters.Add(new SqlParameter("nome", model.Nome));
            parameters.Add(new SqlParameter("periodo", (int)model.Periodo));

            return parameters.ToArray();
        }

        protected override void SetTableName()
        {
            Table = "curso";
        }
    }
}