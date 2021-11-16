using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Volare.Helper;
using Volare.Models;

namespace Volare.DAO
{
    public class UserDAO
    {
        protected string Table { get; set; } = "usuario";

        private SqlParameter[] SetParameters(UserViewModel model)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
           
            parameters.Add(new SqlParameter("username", model.Username));
            parameters.Add(new SqlParameter("password", HashHelper.HashedString(model.Password)));

            return parameters.ToArray();
        }

        public void Insert(UserViewModel model)
        {
            HelperDAO.ExecuteProcedure("sp_insert_" + Table, SetParameters(model));
        }
        public bool IsUsernameInUse(string username)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("username", username)
            };
            var dataSet = HelperDAO.ExecuteProcedureSelect("sp_username_in_use", parameters);
            return dataSet.Rows.Count != 0;
        }
        public bool IsUserValid(UserViewModel model)
        {
            var dataSet = HelperDAO.ExecuteProcedureSelect("sp_is_user_valid", SetParameters(model));
            return dataSet.Rows.Count != 0;
        }

        
    }
}