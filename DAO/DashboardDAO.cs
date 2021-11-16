using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Volare.Models;

namespace Volare.DAO
{
    public class DashboardDAO
    {
        private object SetModel(DataRow row, string sp_procedure)
        {
            if(sp_procedure == "sp_turma_dashboard")
            {
                TurmaDashboardViewModel turmaDashboardViewModel = new TurmaDashboardViewModel();
                turmaDashboardViewModel.AlunoNome = row["aluno_nome"].ToString();
                turmaDashboardViewModel.Presencas = Convert.ToInt32(row["presencas"]);
                turmaDashboardViewModel.totalAulas = Convert.ToInt32(row["total_aulas"]);
                return turmaDashboardViewModel;
            }

            AlunoDashboardViewModel alunoDashboardViewModel = new AlunoDashboardViewModel();
            alunoDashboardViewModel.MateriaNome = row["materia_nome"].ToString();
            alunoDashboardViewModel.Presencas = Convert.ToInt32(row["presencas"]);
            alunoDashboardViewModel.totalAulas = Convert.ToInt32(row["total_aulas"]);

            return alunoDashboardViewModel;
        }

        public object SelectAll(int id, string sp_procedure)
        {
            SqlParameter[] parameters = new SqlParameter[]{ new SqlParameter("id", id)};
            var dataSet = HelperDAO.ExecuteProcedureSelect(sp_procedure, parameters);
            return dataSet.AsEnumerable().Select(row => SetModel(row, sp_procedure)).ToList();
        }

        private SqlParameter[] SetParameters(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("id",id));

            return parameters.ToArray();
        }
    }
}