using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Data;

namespace WebTools
{
    public partial class MySqlHelper
    {

        #region Mysql Connector Net Methods


        enum CharClass : byte
        {
            None,
            Quote,
            Backslash
        }

        const string stringOfBackslashChars = "\u005c\u00a5\u0160\u20a9\u2216\ufe68\uff3c";
        const string stringOfQuoteChars =
            "\u0022\u0027\u0060\u00b4\u02b9\u02ba\u02bb\u02bc\u02c8\u02ca\u02cb\u02d9\u0300\u0301\u2018\u2019\u201a\u2032\u2035\u275b\u275c\uff07";

        static CharClass[] charClassArray = makeCharClassArray();

        private static CharClass[] makeCharClassArray()
        {

            CharClass[] a = new CharClass[65536];
            foreach (char c in stringOfBackslashChars)
            {
                a[c] = CharClass.Backslash;
            }
            foreach (char c in stringOfQuoteChars)
            {
                a[c] = CharClass.Quote;
            }
            return a;
        }

        private static bool needsQuoting(string s)
        {
            foreach (char c in s)
            {
                if (charClassArray[c] != CharClass.None)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Escapes the string.
        /// </summary>
        /// <param name="value">The string to escape</param>
        /// <returns>The string with all quotes escaped.</returns>
        public static string EscapeString(string value)
        {
            if (!needsQuoting(value))
                return value;

            StringBuilder sb = new StringBuilder();

            foreach (char c in value)
            {
                CharClass charClass = charClassArray[c];
                if (charClass != CharClass.None)
                {
                    sb.Append("\\");
                }
                sb.Append(c);
            }
            return sb.ToString();
        }

        public static string DoubleQuoteString(string value)
        {
            if (!needsQuoting(value))
                return value;

            StringBuilder sb = new StringBuilder();
            foreach (char c in value)
            {
                CharClass charClass = charClassArray[c];
                if (charClass == CharClass.Quote)
                    sb.Append(c);
                else if (charClass == CharClass.Backslash)
                    sb.Append("\\");
                sb.Append(c);
            }
            return sb.ToString();
        }


        #endregion

        public static readonly string default_connection_str = "server=localhost;uid=root;pwd=;database=shop;Allow Zero Datetime=true";

        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(default_connection_str, cmdType, cmdText, commandParameters);
        }

        public static int ExecuteNonQuery(string cmdText, params MySqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(default_connection_str, CommandType.Text, cmdText, commandParameters);
        }

        public static int ExecuteNonQueryProc(string StoredProcedureName, params MySqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(default_connection_str, CommandType.StoredProcedure, StoredProcedureName, commandParameters);
        }

        public static int ExecuteNonQuery(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static int ExecuteNonQuery(MySqlTransaction trans, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static MySqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                MySqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public static MySqlDataReader ExecuteReader(MySqlConnection conn, string cmdText, params MySqlParameter[] commandParameters)
        {
            return ExecuteReader(default_connection_str, CommandType.Text, cmdText, commandParameters);
        }

        public static MySqlDataReader ExecuteReader(string cmdText, params MySqlParameter[] commandParameters)
        {
            return ExecuteReader(default_connection_str, CommandType.Text, cmdText, commandParameters);
        }

        public static MySqlDataReader ExecuteReaderProc(string StoredProcedureName, params MySqlParameter[] commandParameters)
        {
            return ExecuteReader(default_connection_str, CommandType.StoredProcedure, StoredProcedureName, commandParameters);
        }

        public static MySqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            return ExecuteReader(default_connection_str, cmdType, cmdText, commandParameters);
        }

        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static object ExecuteScalar(string cmdText, params MySqlParameter[] commandParameters)
        {
            return ExecuteScalar(default_connection_str, CommandType.Text, cmdText, commandParameters);
        }

        public static object ExecuteScalarProc(string StoredProcedureName, params MySqlParameter[] commandParameters)
        {
            return ExecuteScalar(default_connection_str, CommandType.StoredProcedure, StoredProcedureName, commandParameters);
        }

        public static object ExecuteScalar(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            return ExecuteScalar(default_connection_str, cmdType, cmdText, commandParameters);
        }

        public static object ExecuteScalar(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        public static void CacheParameters(string cacheKey, params MySqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        public static MySqlParameter[] GetCachedParameters(string cacheKey)
        {
            MySqlParameter[] cachedParms = (MySqlParameter[])parmCache[cacheKey];
            if (cachedParms == null)
                return null;
            MySqlParameter[] clonedParms = new MySqlParameter[cachedParms.Length];
            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (MySqlParameter)((ICloneable)cachedParms[i]).Clone();
            return clonedParms;
        }

        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        public static DataTable ReadTable(MySqlTransaction transaction, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, transaction.Connection, transaction, cmdType, cmdText, commandParameters);
            DataTable dt = HelperBase.ReadTable(cmd);
            cmd.Parameters.Clear();
            return dt;
        }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(default_connection_str);
        }

        public static DataTable ReadTable(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                return ReadTable(connection, cmdType, cmdText, commandParameters);
            }
        }
        public static DataTable ReadTable(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            return ReadTable(default_connection_str, cmdType, cmdText, commandParameters);
        }

        public static DataTable ReadTable(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            DataTable dt = HelperBase.ReadTable(cmd);
            cmd.Parameters.Clear();
            return dt;
        }
        public static DataTable ReadTable(string cmdText, params MySqlParameter[] commandParameters)
        {
            return ReadTable(CommandType.Text, cmdText, commandParameters);
        }

        public static MySqlParameter CreateInputParameter(string paramName, MySqlDbType dbtype, object value)
        {
            return CreateParameter(ParameterDirection.Input, paramName, dbtype, 0, value);
        }
        public static MySqlParameter CreateInputParameter(string paramName, MySqlDbType dbtype, int size, object value)
        {
            return CreateParameter(ParameterDirection.Input, paramName, dbtype, size, value);
        }

        public static MySqlParameter CreateOutputParameter(string paramName, MySqlDbType dbtype)
        {
            return CreateParameter(ParameterDirection.Output, paramName, dbtype, 0, DBNull.Value);
        }

        public static MySqlParameter CreateOutputParameter(string paramName, MySqlDbType dbtype, int size)
        {
            return CreateParameter(ParameterDirection.Output, paramName, dbtype, size, DBNull.Value);
        }

        public static MySqlParameter CreateParameter(ParameterDirection direction, string paramName, MySqlDbType dbtype, int size, object value)
        {
            MySqlParameter param = new MySqlParameter(paramName, dbtype, size);
            param.Value = value;
            param.Direction = direction;
            return param;
        }



    }
}
