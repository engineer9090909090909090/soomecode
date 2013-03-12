using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
namespace Database
{
    public class MysqlDBHelper : IDisposable
    {

        private string connectionString = string.Empty;

        public MysqlDBHelper(string connectionString)
        {         
            this.connectionString = connectionString;
        }

        public void ExecuteNonQuery(string sql)
        {
            using (MySqlConnection connection = new MySqlConnection(this.connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
                connection.Close();
            }
        }


        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public void ExecuteNonQuery(DbTransaction trans, string sql)
        {
            MySqlConnection connection = (MySqlConnection)trans.Connection;
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        public int ExecuteNonQuery(DbTransaction trans, string sql, MySqlParameter[] parameters)
        {
            int affectedRows = 0;
            MySqlConnection connection = (MySqlConnection)trans.Connection;
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                affectedRows = command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.Dispose();
            }
            return affectedRows;
        }

        public void ExecuteNonQuery(DbTransaction trans, string sql, List<MySqlParameter[]> parametersList)
        {
            MySqlConnection connection = (MySqlConnection)trans.Connection;
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                foreach (MySqlParameter[] parameters in parametersList)
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                }
                command.Dispose();
            }
        }



        /// <summary> 
        /// 对SQLite数据库执行增删改操作，返回受影响的行数。 
        /// </summary> 
        /// <param name="sql">要执行的增删改的SQL语句</param> 
        /// <param name="parameters">执行增删改语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public int ExecuteNonQuery(string sql, MySqlParameter[] parameters)
        {
            int affectedRows = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        affectedRows = command.ExecuteNonQuery();
                        command.Parameters.Clear();
                        command.Dispose();
                    }
                    transaction.Commit();
                }
                connection.Close();
            }
            return affectedRows;
        }

        public void ExecuteNonQuery(string sql, List<MySqlParameter[]> parametersList)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        foreach (MySqlParameter[] parameters in parametersList)
                        {
                            if (parameters != null)
                            {
                                command.Parameters.AddRange(parameters);
                                command.ExecuteNonQuery();
                                command.Parameters.Clear();
                            }
                        }
                        command.Dispose();
                    }
                    transaction.Commit();
                }
                connection.Close();
            }
        }

       
        public DataTable ExecuteDataTable(string sql, MySqlParameter[] parameters)
        {
            DataTable dataTable = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    dataTable = ReadTable(command);
                    command.Parameters.Clear();
                }
                connection.Close();
            }
            return dataTable;
        }

        public int GetItemCount(string sql, MySqlParameter[] parameters)
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
            object id = ExecuteScalar(trans, "SELECT last_insert_id();", null);
            return Convert.IsDBNull(id) ? 0: Convert.ToInt32(id);
        }

        /// <summary> 
        /// 执行一个查询语句，返回查询结果的第一行第一列 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public Object ExecuteScalar(string sql, MySqlParameter[] parameters)
        {
            object rev = Convert.DBNull;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    rev = command.ExecuteScalar();
                    command.Parameters.Clear();
                    command.Dispose();
                }
                connection.Close();
            }
            return rev;

        }

        public Object ExecuteScalar(DbTransaction trans, string sql, MySqlParameter[] parameters)
        {
            MySqlConnection connection = (MySqlConnection)trans.Connection;
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                object rev = command.ExecuteScalar();
                command.Parameters.Clear();
                command.Dispose();
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
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                DataTable data = connection.GetSchema("TABLES", new string[0]);
                connection.Close();
                return data;
            }
        }

        public DataTable ReadTable(DbCommand cmd)
        {
            DataTable dt = new DataTable();
            DbDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
                int fieldc = reader.FieldCount;
                for (int i = 0; i < fieldc; i++)
                {
                    DataColumn dc = new DataColumn(reader.GetName(i), reader.GetFieldType(i));
                    dt.Columns.Add(dc);
                }
                while (reader.Read())
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < fieldc; i++)
                    {
                        dr[i] = reader[i];
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }

        private void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                cmd.Parameters.AddRange(cmdParms);
            }
        }

        public bool ConnectionTest()
        {
            bool Success = true;
            MySqlConnection connection = this.GetConnection();
            try
            {
                connection.Open();
            }
            catch
            {
                Success = false;
            }
            finally
            {
                connection.Close();
            }
            return Success;
        }

        public void Dispose()
        { 
            
        }

    }
}