﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data.Common;
using System.Data;
using System.IO;
namespace AliRank
{
    public class SQLiteDBHelper
    {
        SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
        /// <summary> 
        /// 构造函数 
        /// </summary> 
        /// <param name="dbPath">SQLite数据库文件路径</param> 
        public SQLiteDBHelper(string dbPath, string password)
        {
            builder.DataSource = dbPath;
#if !DEBUG
            builder.Password = password;
#endif
        }

        public static void EncryptDatabase(string dbPath, string password)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=" + dbPath);
            connection.Open();
#if !DEBUG
            connection.ChangePassword(password);
#endif
            connection.Close();

        }

        public void ExecuteNonQuery(string sql)
        {
            using (SQLiteConnection connection = new SQLiteConnection(builder.ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
            }
        }


        /// <summary> 
        /// 对SQLite数据库执行增删改操作，返回受影响的行数。 
        /// </summary> 
        /// <param name="sql">要执行的增删改的SQL语句</param> 
        /// <param name="parameters">执行增删改语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public int ExecuteNonQuery(string sql, SQLiteParameter[] parameters)
        {
            int affectedRows = 0;
            using (SQLiteConnection connection = new SQLiteConnection(builder.ConnectionString))
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = sql;
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        affectedRows = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
            return affectedRows;
        }

        public void ExecuteNonQuery(string sql, List<SQLiteParameter[]> parametersList)
        {
            using (SQLiteConnection connection = new SQLiteConnection(builder.ConnectionString))
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = sql;
                        foreach (SQLiteParameter[] parameters in parametersList)
                        {
                            if (parameters != null)
                            {
                                command.Parameters.AddRange(parameters);
                                command.ExecuteNonQuery();
                            }
                        }                       
                    }
                    transaction.Commit();
                }
            }
        }

        /// <summary> 
        /// 执行一个查询语句，返回一个关联的SQLiteDataReader实例 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public SQLiteDataReader ExecuteReader(string sql, SQLiteParameter[] parameters)
        {
            SQLiteConnection connection = new SQLiteConnection(builder.ConnectionString);
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary> 
        /// 执行一个查询语句，返回一个包含查询结果的DataTable 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public DataTable ExecuteDataTable(string sql, SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(builder.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    return data;
                }
            }
        }

        /// <summary> 
        /// 执行一个查询语句，返回查询结果的第一行第一列 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public Object ExecuteScalar(string sql, SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(builder.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    if (data.Rows.Count == 0)
                    {
                        return DBNull.Value;
                    }
                    return data.Rows[0][0];
                }
            }

        }

        public bool IsExistColumn(string tableName, string columnName)
        {
            try
            {
                string sql = "select * from " + tableName + " where 1 = 0";
                DataTable table = ExecuteDataTable(sql, null);
                bool rev = table.Columns.Contains(columnName);
                table.Dispose();
                table = null;
                return rev;
            }
            catch
            {
                return false;
            }

        }

        /// <summary> 
        /// 查询数据库中的所有数据类型信息 
        /// </summary> 
        /// <returns></returns> 
        public DataTable GetSchema()
        {
            using (SQLiteConnection connection = new SQLiteConnection(builder.ConnectionString))
            {
                connection.Open();
                DataTable data = connection.GetSchema("TABLES");
                connection.Close();
                //foreach (DataColumn column in data.Columns) 
                //{ 
                //        Console.WriteLine(column.ColumnName); 
                //} 
                return data;
            }
        }

        //查询从50条起的20条记录 
        //string sql = "select * from test3 order by id desc limit 50 offset 20";

        /*
        string sql = "INSERT INTO Test3(Name,TypeName,addDate,UpdateTime,Time,Comments)values(@Name,@TypeName,@addDate,@UpdateTime,@Time,@Comments)";
        SQLiteParameter[] parameters = new SQLiteParameter[]
        {
            new SQLiteParameter("@Name",c+i.ToString()), 
            new SQLiteParameter("@TypeName",c.ToString()), 
            new SQLiteParameter("@addDate",DateTime.Now), 
            new SQLiteParameter("@UpdateTime",DateTime.Now.Date), 
            new SQLiteParameter("@Time",DateTime.Now.ToShortTimeString()), 
            new SQLiteParameter("@Comments","Just a Test"+i) 
        };
        db.ExecuteNonQuery(sql, parameters);
        */
    }
}