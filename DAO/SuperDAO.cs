using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Volare.Models;

namespace Volare.DAO
{
    public abstract class SuperDAO<T> where T : SuperViewModel
    {
        public SuperDAO()
        {
            SetTableName();
        }
        protected string Table { get; set; }
        protected string SpSelectName { get; set; } = "sp_select";

        protected abstract SqlParameter[] SetParameters(T model);
        protected abstract T SetModel(DataRow row);
        protected abstract void SetTableName();

        public virtual void Insert(T model)
        {
            HelperDAO.ExecuteProcedure("sp_insert_" + Table, SetParameters(model));
        }
        public virtual void Update(T model)
        {
            HelperDAO.ExecuteProcedure("sp_update_" + Table, SetParameters(model));
        }
        public virtual void Delete(int id)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter("tabela", Table)
            };
            HelperDAO.ExecuteProcedure("sp_delete", parameters);
        }
        public virtual List<T> SelectAll(T model = null)
        {
            SqlParameter[] parameters = null;
            if(SpSelectName == "sp_select")
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("tabela", Table),
                    new SqlParameter("ordem", "1")
                };
            }
            else if(model != null)
            {
                parameters = SetParameters(model);
            }
            
            var dataSet = HelperDAO.ExecuteProcedureSelect(SpSelectName, parameters);
            return dataSet.AsEnumerable().Select(row => SetModel(row)).ToList();
        }
        public virtual T SelectById(int id)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter("tabela", Table)
            };
            var dataSet = HelperDAO.ExecuteProcedureSelect("sp_select_by_id", parameters);
            return dataSet.Rows.Count != 0 ? SetModel(dataSet.Rows[0]) : null;
        }

        
    }
}