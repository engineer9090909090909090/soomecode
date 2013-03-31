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
    public class ProductDaoMysql
    {
        private MysqlDBHelper dbHelper;

        public ProductDaoMysql(MysqlDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            CreateTable();
        }

        private void CreateTable()
        {

            /*产品种类表[ID,名称,父ID,级别,儿子数]*/
            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS Category("
            + "Id integer NOT NULL PRIMARY KEY AUTO_INCREMENT UNIQUE,"
            + "Name varchar(300) NOT NULL,"
            + "ChildrenCount integer default 0,"
            + "ParentId integer NOT NULL,"
            + "Level integer default 0,"
            + "Index Index_ParentId (`ParentId`)) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin ");
			
			dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS PriceCate("
            + "Id integer NOT NULL PRIMARY KEY AUTO_INCREMENT UNIQUE,"
			+ "`UsePrice1` Boolean default false,"
            + "`Price1Name` varchar(100) NOT NULL,"
			+ "`Price1Val` double default 0.0,"
			+ "`UsePrice2` Boolean default false,"
            + "`Price2Name` varchar(100) NOT NULL,"
			+ "`Price2Val` double default 0.0,"
			+ "`UsePrice3` Boolean default false,"
            + "`Price3Name` varchar(100) NOT NULL,"
			+ "`Price3Val` double default 0.0,"
			+ "`UsePrice4` Boolean default false,"
            + "`Price4Name` varchar(100) NOT NULL,"
			+ "`Price4Val` double default 0.0,"
			+ "`UsePrice5` Boolean default false,"
            + "`Price5Name` varchar(100) NOT NULL,"
			+ "`Price5Val` double default 0.0) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin ");

            /*ID,种类,名称,型号,图片,价格,尺寸,重量,最小订量,排序*/
            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS Product("
            + "`Id` integer NOT NULL PRIMARY KEY AUTO_INCREMENT UNIQUE,"
            + "`CategoryId` integer not null,"
            + "`Name` varchar(100) not null,"
            + "`Model` varchar(50) not null,"
            + "`Image` BLOB not null,"
            + "`Price` double default 0.0,"
            + "`PriceCate` integer not null default 0,"
            + "`Minimum` integer,"
            + "`Size` varchar(50),"
            + "`Weight` varchar(50),"
            + "`Packing` varchar(500),"
            + "`Sort` integer,"
            + "`Status` varchar(50),"
            + "`CreatedTime` datetime,"
            + "`ModifiedTime` datetime,"
            + "Index Index_CateId (`CateId`)) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin ");

            dbHelper.ExecuteNonQuery(
             "CREATE TABLE IF NOT EXISTS Product_Image("
           + "`Id` integer NOT NULL PRIMARY KEY AUTO_INCREMENT UNIQUE,"
           + "`ProductId` integer not null,"
           + "`Image` BLOB not null,"
           + "Index Index_ProductId (`ProductId`)) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin ");
            
        }

    }
}
