using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Volare.Models;

namespace Volare.DAO
{
    public class SalaDAO : SuperDAO<SalaViewModel>
    {
        protected override SalaViewModel SetModel(DataRow row)
        {
            SalaViewModel salaViewModel = new SalaViewModel();
            salaViewModel.Id = Convert.ToInt32(row["sala_id"]);
            salaViewModel.Capacidade = Convert.ToInt32(row["sala_capacidade"]);

            return salaViewModel;
        }

        protected override SqlParameter[] SetParameters(SalaViewModel model)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (model.Id != 0)
            {
                parameters.Add(new SqlParameter("id", model.Id));
            }
            parameters.Add(new SqlParameter("capacidade", model.Capacidade));

            return parameters.ToArray();
        }

        protected override void SetTableName()
        {
            Table = "sala";
        }
    }
}