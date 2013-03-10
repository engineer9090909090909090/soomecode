using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace Database
{
    public partial class MySqlHelper:HelperBase
    {
        public MySqlHelper()
        {
            ConnectionString = default_connection_str;
            Connection = new MySqlConnection();
            Command = Connection.CreateCommand();
        }

        public MySqlHelper(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
            Connection = new MySqlConnection();
            Command = Connection.CreateCommand();
        }

        public override void Open()
        {
            base.Open();
        }

        public MySqlParameter AddParameter(string ParameterName, MySqlDbType type, object value)
        {
            return AddParameter(ParameterName, type, value, ParameterDirection.Input);
        }

        public MySqlParameter AddParameter(string ParameterName, MySqlDbType type, object value, ParameterDirection direction)
        {
            MySqlParameter param = new MySqlParameter(ParameterName, type);
            param.Value = value;
            param.Direction = direction;
            Command.Parameters.Add(param);
            return param;
        }

        public MySqlParameter AddParameter(string ParameterName, MySqlDbType type, int size, object value)
        {
            return AddParameter(ParameterName, type, size, value, ParameterDirection.Input);
        }

        public MySqlParameter AddParameter(string ParameterName, MySqlDbType type, int size, object value, ParameterDirection direction)
        {
            MySqlParameter param = new MySqlParameter(ParameterName, type, size);
            param.Direction = direction;
            param.Value = value;
            Command.Parameters.Add(param);
            return param;
        }

        public void AddRangeParameters(MySqlParameter[] parameters)
        {
            Command.Parameters.AddRange(parameters);
        }


    }
}
