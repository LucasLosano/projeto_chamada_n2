using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ProjetoN2.Models;

namespace ProjetoN2.DAO
{
    public class AlunoDAO : SuperDAO<AlunoViewModel>
    {
        public TurmaDAO turmaDAO;
        public AlunoDAO()
        {
            turmaDAO = new TurmaDAO();
        }
        protected override AlunoViewModel SetModel(DataRow row)
        {
            AlunoViewModel alunoViewModel = new AlunoViewModel();
            alunoViewModel.Id = Convert.ToInt32(row["aluno_id"]);
            alunoViewModel.Nome = row["aluno_nome"].ToString();
            if(row.Table.Columns.Contains("turma_nome"))
            {
                alunoViewModel.TurmaNome = row["turma_nome"].ToString();
            }
            else
            {
                alunoViewModel.TurmaId = Convert.ToInt32(row["turma_id"]);
            }

            return alunoViewModel;
        }

        protected override SqlParameter[] SetParameters(AlunoViewModel model)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            if(model.Id != 0)
            {
                parameters.Add(new SqlParameter("id", model.Id));
            }
            parameters.Add(new SqlParameter("nome", model.Nome == null ? "" : model.Nome));
            parameters.Add(new SqlParameter("turma_id", model.TurmaId));

            return parameters.ToArray();
        }

        protected override void SetTableName()
        {
            Table = "aluno";
            SpSelectName = SpSelectName + "_" + Table;
        }
    }
}