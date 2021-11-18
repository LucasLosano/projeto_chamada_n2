using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Volare.Helper;
using Volare.Models;

namespace Volare.DAO
{
    public class ChamadaDAO
    {
        protected string Table { get; set; } = "chamada";
        
        AlunoDAO alunoDAO;
        AulaDAO aulaDAO;
        SalaDAO salaDAO;
        public ChamadaDAO()
        {
            alunoDAO = new AlunoDAO();
            aulaDAO = new AulaDAO();
            salaDAO = new SalaDAO();
        }

        private ChamadaViewModel SetModel(DataRow row)
        {
            ChamadaViewModel chamadaViewModel = new ChamadaViewModel();
            chamadaViewModel.AulaData = Convert.ToDateTime(row["aula_data"]);
            chamadaViewModel.AlunoNome = row["aluno_nome"].ToString();
            chamadaViewModel.MateriaNome = row["materia_nome"].ToString();
            chamadaViewModel.TurmaNome = row["turma_nome"].ToString();
            chamadaViewModel.Status = row["status"].ToString();

            return chamadaViewModel;
        }

        public List<ChamadaViewModel> SelectAll(ChamadaViewModel model = null)
        {
            SqlParameter[] parameters = null;
            if(model != null)
            {
                parameters = SetParameters(model);
            }
            var dataSet = HelperDAO.ExecuteProcedureSelect("sp_select_chamada", parameters);
            return dataSet.AsEnumerable().Select(row => SetModel(row)).ToList();
        }

        private SqlParameter[] SetParameters(ChamadaViewModel model)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("aula_id",model.AulaId));
            if(model.AlunoId != 0)
            {
                parameters.Add(new SqlParameter("aluno_id",model.AlunoId));
            }
            else
            {
                parameters.Add(new SqlParameter("aluno_nome", model.AlunoNome));
                parameters.Add(new SqlParameter("status", model.Status));
            }

            return parameters.ToArray();
        }

        public void Insert(ChamadaViewModel model)
        {
            HelperDAO.ExecuteProcedure("sp_insert_" + Table, SetParameters(model));
        }

        public void InsertFromMongo()
        {
            var chamadasDTO  = MongoDAO.GetChamadaData().Where(c => 
                alunoDAO.SelectById(c.AlunoId) != null && salaDAO.SelectById(c.SalaId) != null
            ).ToList();

            chamadasDTO.ForEach(c => {
                c.TurmaId = alunoDAO.SelectById(c.AlunoId).TurmaId;
            });
            var chamadasViewModels = chamadasDTO.Select(c =>{
                var aulaList = aulaDAO.SelectAll(new AulaViewModel(){TurmaId = c.TurmaId, SalaId = c.SalaId,  Data = c.Chegada});
                ChamadaViewModel chamadaViewModel = null;
                if(aulaList.Count == 0)
                {
                    return chamadaViewModel;
                }
                aulaList = aulaList.OrderBy(a => Math.Abs(a.Data.Ticks-c.Chegada.Ticks)).ToList();
                chamadaViewModel = new ChamadaViewModel(){AlunoId = c.AlunoId, AulaId = aulaList[0].Id};
                return chamadaViewModel;
            }).ToList();

            chamadasViewModels.ForEach(c => {
                if(c != null)
                {
                    Insert(c);
                }
            });
        }
    }
}