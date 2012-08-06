using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace SooWebSiteTools
{
    class ProducctDAO
    {
        public ProducctDAO() { 
        
        }

        public DataTable GetProducts(string LangId)
        {
            string sql = "SELECT d.name, P.*  FROM product p JOIN product_description d ON p.product_id= d.product_id "
                       + "WHERE d.language_id = " + LangId;
            DataTable dt = MySqlHelper.ReadTable(sql, null);
            return dt;
        }

        public DataTable GetLanguages()
        {
            string sql = "SELECT language_id id, NAME FROM LANGUAGE WHERE STATUS = 1";
            return MySqlHelper.ReadTable(sql, null);
        }

        public DataTable GetManufacturers()
        {
            string sql = "SELECT manufacturer_id id,NAME FROM manufacturer ORDER BY sort_order";
            return MySqlHelper.ReadTable(sql, null);
        }

        public DataTable GetStockStatus(string LangId)
        {
            string sql = "SELECT stock_status_id id, NAME FROM stock_status WHERE language_id =" + LangId;
            return MySqlHelper.ReadTable(sql, null);
        }

        public DataTable GetWeightClass(string LangId)
        {
            string sql = "SELECT weight_class_id id, title Name FROM weight_class_description WHERE language_id  =" + LangId;
            return MySqlHelper.ReadTable(sql, null);
        }

        public DataTable GetLengthClass(string LangId)
        {
            string sql = "SELECT length_class_id id, title Name FROM length_class_description WHERE language_id  =" + LangId;
            return MySqlHelper.ReadTable(sql, null);
        }

        public DataTable GetCategories(string LangId)
        {
            int parentId = 0;
            string sql = "SELECT c.parent_id pid, d.category_id id, d.name "
            + " FROM category_description d JOIN category c ON d.category_id= c.category_id AND c.status = 1"
            + " WHERE c.parent_id = @parent_id AND d.language_id = @language_id ORDER BY id";
            MySqlParameter[] parameter = new MySqlParameter[] 
            { 
                new MySqlParameter("@parent_id",parentId),
                new MySqlParameter("@language_id",LangId)
            };
            DataTable dt = MySqlHelper.ReadTable(sql, parameter);
            DataTable newtable = dt.Copy();
            newtable.Rows.Clear();
            for (int i =0; i< dt.Rows.Count; i ++ )
            {
                string pname = Convert.ToString(dt.Rows[i]["name"]);
                newtable.ImportRow(dt.Rows[i]);
                parentId = Convert.ToInt32(dt.Rows[i]["id"]);
                MySqlParameter[] subParameter = new MySqlParameter[] 
                { 
                    new MySqlParameter("@parent_id",parentId),
                    new MySqlParameter("@language_id",Convert.ToInt32(LangId))
                };
                DataTable ddt = MySqlHelper.ReadTable(sql, subParameter);
                for (int j = 0; j < ddt.Rows.Count; j++)
                {
                    ddt.Rows[j]["name"] = pname + " >> " + ddt.Rows[j]["name"];
                    newtable.ImportRow(ddt.Rows[j]);
                }
            }
            return newtable;
        }
    }

    
}
