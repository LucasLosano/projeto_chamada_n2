using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Volare.Models;

namespace Volare.DAO
{
    public class TurmaDAO : SuperDAO<TurmaViewModel>
    {
        public CursoDAO cursoDAO;

        public TurmaDAO()
        {
            cursoDAO = new CursoDAO();
        }
        protected override TurmaViewModel SetModel(DataRow row)
        {
            TurmaViewModel turmaViewModel = new TurmaViewModel();
            turmaViewModel.Id = Convert.ToInt32(row["turma_id"]);
            turmaViewModel.Semestre = Convert.ToInt32(row["turma_semestre"]);
            turmaViewModel.CursoId = Convert.ToInt32(row["curso_id"]);
            if (row.Table.Columns.Contains("turma_nome"))
            {
                turmaViewModel.Nome = row["turma_nome"].ToString();
            }
            if (row.Table.Columns.Contains("curso_nome"))
            {
                turmaViewModel.CursoNome = row["curso_nome"].ToString();
            }
            turmaViewModel.QuantidadeAlunos = Convert.ToInt32(row["quantidade_alunos"]);
            return turmaViewModel;
        }

        protected override SqlParameter[] SetParameters(TurmaViewModel model)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            if(model.Id != 0)
            {
                parameters.Add(new SqlParameter("id",model.Id));
            }
            if(model.Semestre != 0)
            {
                parameters.Add(new SqlParameter("semestre", model.Semestre));
                parameters.Add(new SqlParameter("curso_id", model.CursoId));
            }

            return parameters.ToArray();
        }

        protected override void SetTableName()
        {
            Table = "turma";
            SpSelectName = SpSelectName + "_" + Table;
        }
    }
}