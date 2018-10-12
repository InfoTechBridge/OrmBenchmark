﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using OrmBenchmark.Core;
using System.Dynamic;

namespace OrmBenchmark.Ado
{
    public class PureAdoExecuter : IOrmExecuter
    {
        SqlConnection conn;

        public string Name
        {
            get
            {
                return "ADO (Pure)";
            }
        }

        public void Init(string connectionStrong)
        {
            conn = new SqlConnection(connectionStrong);
            conn.Open();
        }
        public IPost GetItemAsObject(int Id)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"select * from Posts where Id = @Id";
            var idParam = cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int);
            idParam.Value = Id;

            Post obj;
            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();
                obj = new Post
                {
                    Id = reader.GetInt32(0),
                    Text = reader.GetNullableString(1),
                    CreationDate = reader.GetDateTime(2),
                    LastChangeDate = reader.GetDateTime(3),
                    Counter1 = reader.GetNullableValue<int>(4),
                    Counter2 = reader.GetNullableValue<int>(5),
                    Counter3 = reader.GetNullableValue<int>(6),
                    Counter4 = reader.GetNullableValue<int>(7),
                    Counter5 = reader.GetNullableValue<int>(8),
                    Counter6 = reader.GetNullableValue<int>(9),
                    Counter7 = reader.GetNullableValue<int>(10),
                    Counter8 = reader.GetNullableValue<int>(11),
                    Counter9 = reader.GetNullableValue<int>(12),
                };
            }

            return obj;
        }

        public dynamic GetItemAsDynamic(int Id)
        {
            return null;

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"select * from Posts where Id = @Id";
            var idParam = cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int);
            idParam.Value = Id;

            dynamic obj;
            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();
                obj = new
                {
                    Id = reader.GetInt32(0),
                    Text = reader.GetNullableString(1),
                    CreationDate = reader.GetDateTime(2),
                    LastChangeDate = reader.GetDateTime(3),
                    Counter1 = reader.GetNullableValue<int>(4),
                    Counter2 = reader.GetNullableValue<int>(5),
                    Counter3 = reader.GetNullableValue<int>(6),
                    Counter4 = reader.GetNullableValue<int>(7),
                    Counter5 = reader.GetNullableValue<int>(8),
                    Counter6 = reader.GetNullableValue<int>(9),
                    Counter7 = reader.GetNullableValue<int>(10),
                    Counter8 = reader.GetNullableValue<int>(11),
                    Counter9 = reader.GetNullableValue<int>(12),
                };
            }

            return obj;
        }

        public IList<IPost> GetAllItemsAsObject()
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"select * from Posts";

            List<IPost> list = new List<IPost>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Post obj = new Post
                    {
                        Id = reader.GetInt32(0),
                        Text = reader.GetNullableString(1),
                        CreationDate = reader.GetDateTime(2),
                        LastChangeDate = reader.GetDateTime(3),
                        Counter1 = reader.GetNullableValue<int>(4),
                        Counter2 = reader.GetNullableValue<int>(5),
                        Counter3 = reader.GetNullableValue<int>(6),
                        Counter4 = reader.GetNullableValue<int>(7),
                        Counter5 = reader.GetNullableValue<int>(8),
                        Counter6 = reader.GetNullableValue<int>(9),
                        Counter7 = reader.GetNullableValue<int>(10),
                        Counter8 = reader.GetNullableValue<int>(11),
                        Counter9 = reader.GetNullableValue<int>(12),
                    };

                    list.Add(obj);
                }
            }

            return list;
        }

        public IList<dynamic> GetAllItemsAsDynamic()
        {
            return null;

#if NETFULL
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"select * from Posts";

            List<dynamic> list = new List<dynamic>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dynamic obj = new
                    {
                        Id = reader.GetInt32(0),
                        Text = reader.GetNullableString(1),
                        CreationDate = reader.GetDateTime(2),
                        LastChangeDate = reader.GetDateTime(3),
                        Counter1 = reader.GetNullableValue<int>(4),
                        Counter2 = reader.GetNullableValue<int>(5),
                        Counter3 = reader.GetNullableValue<int>(6),
                        Counter4 = reader.GetNullableValue<int>(7),
                        Counter5 = reader.GetNullableValue<int>(8),
                        Counter6 = reader.GetNullableValue<int>(9),
                        Counter7 = reader.GetNullableValue<int>(10),
                        Counter8 = reader.GetNullableValue<int>(11),
                        Counter9 = reader.GetNullableValue<int>(12),
                    };

                    list.Add(obj);
                }
            }

            return list;
#else
            return null;
#endif
        }

        public void Finish()
        {
            conn.Close();
        }

    }
}
