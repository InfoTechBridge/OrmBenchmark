using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBenchmark.Core
{
    public class PureAdoExecuterGetValues : IOrmExecuter
    {
        SqlConnection conn;

        public string Name
        {
            get
            {
                return "Pure ADO (DataTable)";
            }
        }

        public void Init(string connectionStrong)
        {
            conn = new SqlConnection(connectionStrong);
            conn.Open();
        }

        public object GetItemAsObject(int Id)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"select Id, [Text], [CreationDate], LastChangeDate, 
                Counter1,Counter2,Counter3,Counter4,Counter5,Counter6,Counter7,Counter8,Counter9 from Posts where Id = @Id";
            var idParam = cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int);
            idParam.Value = Id;

            DataTable table = new DataTable
            {
                Columns =
                    {
                        {"Id", typeof (int)},
                        {"Text", typeof (string)},
                        {"CreationDate", typeof (DateTime)},
                        {"LastChangeDate", typeof (DateTime)},
                        {"Counter1", typeof (int)},
                        {"Counter2", typeof (int)},
                        {"Counter3", typeof (int)},
                        {"Counter4", typeof (int)},
                        {"Counter5", typeof (int)},
                        {"Counter6", typeof (int)},
                        {"Counter7", typeof (int)},
                        {"Counter8", typeof (int)},
                        {"Counter9", typeof (int)},
                    }
            };

            object[] values = new object[13];
            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();
                reader.GetValues(values);
                table.Rows.Add(values);

                //obj = new {
                //    Id = reader.GetInt32(0),
                //    Text = reader.GetValue(1),
                //    CreationDate = reader.GetDateTime(2),
                //    LastChangeDate = reader.GetDateTime(3),
                //    Counter1 = reader.GetValue(4) != DBNull.Value ? reader.GetValue(4) : null,
                //    Counter2 = reader.GetValue(5) != DBNull.Value ? reader.GetValue(4) : null,
                //    Counter3 = reader.GetValue(6) != DBNull.Value ? reader.GetValue(4) : null,
                //    Counter4 = reader.GetValue(7) != DBNull.Value ? reader.GetValue(4) : null,
                //    Counter5 = reader.GetValue(8) != DBNull.Value ? reader.GetValue(4) : null,
                //    Counter6 = reader.GetValue(9) != DBNull.Value ? reader.GetValue(4) : null,
                //    Counter7 = reader.GetValue(10) != DBNull.Value ? reader.GetValue(4) : null,
                //    Counter8 = reader.GetValue(11) != DBNull.Value ? reader.GetValue(4) : null,
                //    Counter9 = reader.GetValue(12) != DBNull.Value ? reader.GetValue(4) : null,
                //};
            }

            return null;
        }

        public dynamic GetItemAsDynamic(int Id)
        {
            throw new NotImplementedException();
        }

        public IList<object> GetAllItemsAsObject()
        {
            throw new NotImplementedException();
        }

        public IList<dynamic> GetAllItemsAsDynamic()
        {
            throw new NotImplementedException();
        }

        public void Finish()
        {
            conn.Close();
        }

    }
}
