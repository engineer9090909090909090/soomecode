using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AliEmail
{
    public class DAO
    {
        public static DataSet FetchMailContext(string fileName)
        {
            MdbAcccess ma = new MdbAcccess(fileName);
            string sql = "select 主题,正文,'' as 发送时间 from Email";
            string sql2 = "select subject, '' as sendto mail_body from Email";
            DataSet ds =  ma.ReturnDataSet(sql);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else 
            {
                return ma.ReturnDataSet(sql2);
            }
            
        }
    }
}
