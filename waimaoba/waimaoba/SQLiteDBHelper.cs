using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data.Common;
using System.Data;
using System.IO;
namespace com.soomes
{
    public class SQLiteDBHelper
    {

        private string connectionString = string.Empty;
        private string dbPath = string.Empty;
        /// <summary> 

        /// 构造函数 

        /// </summary> 

        /// <param name="dbPath">SQLite数据库文件路径</param> 

        public SQLiteDBHelper(string dbPath)
        {
            this.dbPath = dbPath;            
            this.connectionString = "Data Source=" + dbPath;
        }

        public void ExecuteNonQuery(string sql)
        {
            using (SQLiteConnection connection = new SQLiteConnection(this.connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
            }
        }


        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        public void ExecuteNonQuery(DbTransaction trans, string sql)
        {
            SQLiteConnection connection = (SQLiteConnection)trans.Connection;
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        public int ExecuteNonQuery(DbTransaction trans, string sql, SQLiteParameter[] parameters)
        {
            int affectedRows = 0;
            SQLiteConnection connection = (SQLiteConnection)trans.Connection;
            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                affectedRows = command.ExecuteNonQuery();
            }
            return affectedRows;
        }

        public void ExecuteNonQuery(DbTransaction trans, string sql, List<SQLiteParameter[]> parametersList)
        {
            SQLiteConnection connection = (SQLiteConnection)trans.Connection;
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
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
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
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
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
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
        }

        /// <summary> 
        /// 执行一个查询语句，返回一个包含查询结果的DataTable 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public DataTable ExecuteDataTable(string sql, SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
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

        public int GetItemCount(string sql, SQLiteParameter[] parameters)
        {
            object count = ExecuteScalar("select count(1) from (" + sql + ") ItemCountTemp", parameters);
            if (Convert.IsDBNull(count))
            {
                return 0;
            }
            else {
                return Convert.ToInt32(count);
            }
        }

        public int GetLastInsertId(DbTransaction trans)
        {
            object id = ExecuteScalar(trans, "SELECT last_insert_rowid();", null);
            return Convert.IsDBNull(id) ? 0: Convert.ToInt32(id);
        }

        /// <summary> 
        /// 执行一个查询语句，返回查询结果的第一行第一列 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public Object ExecuteScalar(string sql, SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    object rev = command.ExecuteScalar();
                    command.Parameters.Clear();
                    return rev;
                }

            }

        }

        /// <summary> 
        /// 执行一个查询语句，返回查询结果的第一行第一列 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public object ExecuteScalar(DbTransaction trans, string sql, SQLiteParameter[] parameters)
        {
            SQLiteConnection connection = (SQLiteConnection)trans.Connection;
            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                object rev = command.ExecuteScalar();
                command.Parameters.Clear();
                return rev;
            }
        }

        public bool IsExistColumn(string tableName, string columnName)
        {
            string sql = "select * from " + tableName + " where 1 = 0";
            DataTable table = ExecuteDataTable(sql, null);
            bool rev = table.Columns.Contains(columnName);
            table.Dispose();
            table = null;
            return rev;
        }

        /// <summary> 
        /// 查询数据库中的所有数据类型信息 
        /// </summary> 
        /// <returns></returns> 
        public DataTable GetSchema()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                DataTable data = connection.GetSchema("TABLES");
                connection.Close();
                return data;
            }
        }


    }
}