using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Database
{
    public class SuplierDaoMysql
    {
        private MysqlDBHelper dbHelper;

        public SuplierDaoMysql(MysqlDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            CreateTable();
        }

        private void CreateTable()
        {
            /*供应商[ID,名称,联系人,联系方式,公司地址]*/
            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS Suplier("
            + "`Id` integer NOT NULL PRIMARY KEY AUTO_INCREMENT UNIQUE,"
            + "`Name` varchar(255) not null,"
            + "`Contact` varchar(500),"
            + "`Remark` varchar(500),"
            + "`Address` varchar(200),"
            + "`CreatedTime` datetime,"
            + "`ModifiedTime` datetime) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin ");

            /*供应商产品表[产品ID,供应商ID,供应产品名,图片,价格,价格说明,备注]*/
            dbHelper.ExecuteNonQuery(
             "CREATE TABLE IF NOT EXISTS Suplier_Item("
           + "`Id` integer NOT NULL PRIMARY KEY AUTO_INCREMENT UNIQUE,"
           + "`ProductId` integer not null,"
           + "`SuplierId` integer not null,"
           + "`Name` varchar(200) not null defult '',"
           + "`Image` BLOB,"
           + "`Price` double not null defult 0.0,"
           + "`PriceDesc` varchar(200),"
           + "`Remark` varchar(500),"
           + "`CreatedTime` datetime,"
           + "`ModifiedTime` datetime) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin ");

        }

        
    }
}
