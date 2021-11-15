using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ProjetoN2.Models;

namespace ProjetoN2.DAO
{
    public class AulaDAO : SuperDAO<AulaViewModel>
    {
        public MateriaDAO materiaDAO;
        public TurmaDAO turmaDAO;
        public SalaDAO salaDAO;

        public AulaDAO()
        {
            materiaDAO = new MateriaDAO();
            turmaDAO = new TurmaDAO();
            salaDAO = new SalaDAO();
        }
        protected override AulaViewModel SetModel(DataRow row)
        {
            AulaViewModel aulaViewModel = new AulaViewModel();
            aulaViewModel.Id = Convert.ToInt32(row["aula_id"]);
            aulaViewModel.Data = Convert.ToDateTime(row["aula_data"]);
            aulaViewModel.SalaId = Convert.ToInt32(row["sala_id"]);
            if(row.Table.Columns.Contains("materia_nome"))
            {
                aulaViewModel.MateriaNome = row["materia_nome"].ToString();
                aulaViewModel.TurmaNome = row["turma_nome"].ToString();
            }
            else
            {
                aulaViewModel.MateriaId = Convert.ToInt32(row["materia_id"]);
                aulaViewModel.TurmaId = Convert.ToInt32(row["turma_id"]);
            }

            return aulaViewModel;
        }
        protected override SqlParameter[] SetParameters(AulaViewModel model)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            if(model.Id != 0)
            {
                parameters.Add(new SqlParameter("id",model.Id));
            }
            if(model.ProfessorId != 0)
            {
                parameters.Add(new SqlParameter("professor_id",model.ProfessorId));
            }
            parameters.Add(new SqlParameter("data", model.Data.Year != 1 ? model.Data : DateTime.Now.AddYears(-100)));
            parameters.Add(new SqlParameter("turma_id", model.TurmaId));
            parameters.Add(new SqlParameter("materia_id", model.MateriaId));
            parameters.Add(new SqlParameter("sala_id", model.SalaId));

            return parameters.ToArray();
        }

        protected override void SetTableName()
        {
            Table = "aula";
            SpSelectName = SpSelectName + "_" + Table;
        }
    }
}