using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Volare.Models;

namespace Volare.DAO
{
    public class MateriaDAO : SuperDAO<MateriaViewModel>
    {
        public ProfessorDAO professorDAO;
        public CursoDAO cursoDAO;

        public MateriaDAO()
        {
            professorDAO = new ProfessorDAO();
            cursoDAO = new CursoDAO();
        }
        protected override MateriaViewModel SetModel(DataRow row)
        {
            MateriaViewModel materiaViewModel = new MateriaViewModel();
            materiaViewModel.Id = Convert.ToInt32(row["materia_id"]);
            materiaViewModel.Nome = row["materia_nome"].ToString();

            if(row.Table.Columns.Contains("professor_nome"))
            {
                materiaViewModel.ProfessorNome = row["professor_nome"].ToString();
                materiaViewModel.CursoNome = row["curso_nome"].ToString();
            }
            else
            {
                materiaViewModel.ProfessorId = Convert.ToInt32(row["professor_id"]);
                materiaViewModel.CursoId = Convert.ToInt32(row["curso_id"]);
            }

            

            return materiaViewModel;
        }

        protected override SqlParameter[] SetParameters(MateriaViewModel model)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            if(model.Id != 0)
            {
                parameters.Add(new SqlParameter("id",model.Id));
            }
            parameters.Add(new SqlParameter("nome", model.Nome));
            parameters.Add(new SqlParameter("professor_id", model.ProfessorId));
            parameters.Add(new SqlParameter("curso_id", model.CursoId));

            return parameters.ToArray();
        }

        protected override void SetTableName()
        {
            Table = "materia";
            SpSelectName = SpSelectName + "_" + Table;
        }
    }
}