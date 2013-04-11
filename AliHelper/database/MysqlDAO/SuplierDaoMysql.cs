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
    public class SuplierDaoMysql : ISupplierDao
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
              "CREATE TABLE IF NOT EXISTS Supplier("
            + "`Id` integer NOT NULL PRIMARY KEY AUTO_INCREMENT UNIQUE,"
            + "`Name` varchar(255) not null,"
            + "`Contact` varchar(500),"
            + "`Remark` varchar(500),"
            + "`Address` varchar(200),"
            + "`CreatedTime` datetime,"
            + "`ModifiedTime` datetime) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin ");

            /*供应商产品表[产品ID,供应商ID,供应产品名,图片,价格,价格说明,备注]*/
            dbHelper.ExecuteNonQuery(
             "CREATE TABLE IF NOT EXISTS Supplier_Item("
           + "`Id` integer NOT NULL PRIMARY KEY AUTO_INCREMENT UNIQUE,"
           + "`ProductId` integer not null,"
           + "`SupplierId` integer not null,"
           + "`Name` varchar(200) not null defult '',"
           + "`Image` BLOB,"
           + "`Price` double not null defult 0.0,"
           + "`PriceDesc` varchar(200),"
           + "`Remark` varchar(500),"
           + "`CreatedTime` datetime,"
           + "`ModifiedTime` datetime) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin ");
        }

        public QueryObject<Supplier> GetSuppliers(QueryObject<Supplier> query)
        {
            string sql = "select t.* FROM Supplier t where 1 = 1 ";
            List<MySqlParameter> QueryParameters = new List<MySqlParameter>();
            if (query.Condition != null)
            {
                if (!string.IsNullOrEmpty(query.Condition.Name))
                {
                    sql = sql + "and t.Name like '%" + query.Condition.Name.Trim() + "%' ";
                }
                if (!string.IsNullOrEmpty(query.Condition.Contact))
                {
                    sql = sql + "and t.Contact like '%" + query.Condition.Contact.Trim() + "%' ";
                }
                if (!string.IsNullOrEmpty(query.Condition.Remark))
                {
                    sql = sql + "and t.Remark like '%" + query.Condition.Remark.Trim() + "%' ";
                }
                if (!string.IsNullOrEmpty(query.Condition.Address))
                {
                    sql = sql + "and t.Address like '%" + query.Condition.Address.Trim() + "%' ";
                }
            }
            if (query.IsPager == true)
            {
                query.RecordCount = dbHelper.GetItemCount(sql, QueryParameters.ToArray());
                sql = sql + "order by ModifiedTime desc limit " + query.Start + ", " + query.PageSize;
            }
            else
            {
                sql = sql + " Order by ModifiedTime desc";
            }
            
            query.dt = dbHelper.ExecuteDataTable(sql, QueryParameters.ToArray());
            query.Result = DataTableToSupplierList(query.dt);
            return query;
        }

        public List<Supplier> DataTableToSupplierList(DataTable dt)
        {
            List<Supplier> list = new List<Supplier>();
            foreach (DataRow row in dt.Rows)
            {
                Supplier info = new Supplier();
                info.Id = Convert.ToInt32(row["Id"]);
                info.Name = (string)row["Name"];
                info.Contact = (string)row["Contact"];
                info.Remark = (string)row["Remark"];
                info.Address = (string)row["Address"];
                list.Add(info);
            }
            return list;
        }

        public List<SupplierItem> GetSupplierItems(int ProductId, int SupplierId)
        {
            string sql = "select t.* FROM Supplier_Item t where 1 = 1 ";
            if (ProductId > 0)
            {
                sql = sql + "and t.ProductId = " + ProductId;
            }
            if (SupplierId > 0)
            {
                sql = sql + "and t.SupplierId = " + SupplierId;
            }
            sql = sql + " Order by ModifiedTime desc";

            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            return DataTableToSupplierItems(dt);
        }

        public byte[] GetSupplierItemImage(int SupplierItemId)
        {
            return dbHelper.GetBlogField("select Image FROM Supplier_Item where Id = " + SupplierItemId);
        }

        public List<SupplierItem> DataTableToSupplierItems(DataTable dt)
        {
            List<SupplierItem> list = new List<SupplierItem>();
            foreach (DataRow row in dt.Rows)
            {
                SupplierItem info = new SupplierItem();
                info.Id = Convert.ToInt32(row["Id"]);
                info.SupplierId = Convert.ToInt32(row["SupplierId"]);
                info.ProductId = Convert.ToInt32(row["ProductId"]);
                info.Name = (string)row["Name"];
                info.Price = Convert.ToDouble(row["Price"]);
                info.PriceDesc = (string)row["PriceDesc"];
                info.Remark = (string)row["Remark"];
                list.Add(info);
            }
            return list;
        }

        public void InsertOrUpdateSupplier(Supplier item)
        {
            string InsSql = @"INSERT INTO Supplier(Name,Contact,Remark,Address,CreatedTime,ModifiedTime) "
                            + " VALUES(@Name,@Contact,@Remark,@Address,@CreatedTime,@ModifiedTime)";
            string UpdSql = @"UPDATE Supplier set Name=@Name,Contact=@Contact,Remark=@Remark,Address=@Address,ModifiedTime=@ModifiedTime "
                            + " WHERE Id = @Id";

            string ExistRecordSql = "SELECT count(1) FROM Supplier WHERE Id = " + item.Id;
            DateTime CurrentTime = DateTime.Now;
            MySqlParameter[] parameter = new MySqlParameter[]
            {
                new MySqlParameter("@Id",item.Id),
                new MySqlParameter("@Name",item.Name),
                new MySqlParameter("@Contact",item.Contact),
                new MySqlParameter("@Remark",item.Remark),
                new MySqlParameter("@Address",item.Address),
                new MySqlParameter("@CreatedTime", CurrentTime),
                new MySqlParameter("@ModifiedTime",CurrentTime)
            };
            int record = Convert.ToInt32(dbHelper.ExecuteScalar(ExistRecordSql, null));
            if (record == 0)
            {
                dbHelper.ExecuteNonQuery(InsSql, parameter);
            }
            else
            {
                dbHelper.ExecuteNonQuery(UpdSql, parameter);
            }
        }

        public void InsertOrUpdateSupplierItem(SupplierItem item)
        {
            string InsSql = @"INSERT INTO Supplier_Item(ProductId,SupplierId,Name,Image,Price,PriceDesc,Remark,CreatedTime,ModifiedTime) "
                            + " VALUES(@ProductId,@SupplierId,@Name,@Image,@Price,@PriceDesc,,@Remark,@CreatedTime,@ModifiedTime)";
            string UpdSql = @"UPDATE Supplier_Item set ProductId=@ProductId,SupplierId=@SupplierId,Name=@Name,Image=@Image,Price=@Price, "
                            + " PriceDesc=@PriceDesc,Remark=@Remark,ModifiedTime=@ModifiedTime WHERE Id = @Id";

            string ExistRecordSql = "SELECT count(1) FROM Supplier_Item WHERE Id = " + item.Id;
            DateTime CurrentTime = DateTime.Now;
            MySqlParameter[] parameter = new MySqlParameter[]
            {
                new MySqlParameter("@Id",item.Id),
                new MySqlParameter("@ProductId",item.ProductId),
                new MySqlParameter("@SupplierId",item.SupplierId),
                new MySqlParameter("@Name",item.Name),
                new MySqlParameter("@Image",item.Image),
                new MySqlParameter("@Price",item.Price),
                new MySqlParameter("@PriceDesc",item.PriceDesc),
                new MySqlParameter("@Remark",item.Remark),
                new MySqlParameter("@CreatedTime", CurrentTime),
                new MySqlParameter("@ModifiedTime",CurrentTime)
            };
            int record = Convert.ToInt32(dbHelper.ExecuteScalar(ExistRecordSql, null));
            if (record == 0)
            {
                dbHelper.ExecuteNonQuery(InsSql, parameter);
            }
            else
            {
                dbHelper.ExecuteNonQuery(UpdSql, parameter);
            }
        }

        public void DeleteSupplier(int Id)
        {
            using (MySqlConnection connection = dbHelper.GetConnection())
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    dbHelper.ExecuteNonQuery(transaction, "delete from Supplier_Item WHERE SupplierId = " + Id);
                    dbHelper.ExecuteNonQuery(transaction, "delete from Supplier WHERE Id = " + Id);
                    transaction.Commit();
                }
            }
        }

        public void DeleteSupplierItem(int Id)
        {
            dbHelper.ExecuteNonQuery("delete from Supplier_Item WHERE Id = " + Id);
        }
    }
}
