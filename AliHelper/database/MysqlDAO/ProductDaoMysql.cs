﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Database
{
    public class ProductDaoMysql : IProductDao
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
            + "Sort integer default 0,"
            + "Index Index_ParentId (`ParentId`)) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin ");

            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS PriceCate("
            + "Id integer NOT NULL PRIMARY KEY AUTO_INCREMENT UNIQUE,"
            + "`CateName` varchar(100) NOT NULL,"
            + "`UsePrice1` Boolean default false,"
            + "`Price1Name` varchar(100),"
            + "`Price1Val` double default 0.0,"
            + "`UsePrice2` Boolean default false,"
            + "`Price2Name` varchar(100),"
            + "`Price2Val` double default 0.0,"
            + "`UsePrice3` Boolean default false,"
            + "`Price3Name` varchar(100),"
            + "`Price3Val` double default 0.0,"
            + "`UsePrice4` Boolean default false,"
            + "`Price4Name` varchar(100),"
            + "`Price4Val` double default 0.0,"
            + "`UsePrice5` Boolean default false,"
            + "`Price5Name` varchar(100),"
            + "`Price5Val` double default 0.0,"
            + "`Status` varchar(10) NOT NULL default 'A') ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin ");

            /*ID,种类,名称,型号,图片,价格,尺寸,重量,最小订量,排序*/
            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS Product("
            + "`Id` integer NOT NULL PRIMARY KEY AUTO_INCREMENT UNIQUE,"
            + "`CategoryId` integer not null,"
            + "`Name` varchar(100) not null,"
            + "`Model` varchar(50) not null,"
            + "`Price` double default 0.0,"
            + "`PriceCate` integer not null default 0,"
            + "`Minimum` integer,"
            + "`Size` varchar(50),"
            + "`Weight` varchar(50),"
            + "`Packing` varchar(500),"
            + "`Description` varchar(8000),"
            + "`Sort` integer,"
            + "`Status` varchar(50),"
            + "`CreatedTime` datetime,"
            + "`ModifiedTime` datetime,"
            + "Index Index_CateId (`CategoryId`)) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin ");

            dbHelper.ExecuteNonQuery(
             "CREATE TABLE IF NOT EXISTS Product_Image("
           + "`Id` integer NOT NULL PRIMARY KEY AUTO_INCREMENT UNIQUE,"
           + "`ProductId` integer not null,"
           + "`Image` LongBLOB not null,"
           + "`Size` long not null,"
           + "`IsMain` Boolean not null default false,"
           + "`CreatedTime` datetime not null,"
           + "`ModifiedTime` datetime not null,"
           + "Index Index_ProductId (`ProductId`)) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin ");

        }

        public List<Categories> GetAllCategories()
        {
            string sql = "SELECT Id, Name, Sort, ChildrenCount, Level, ParentId FROM Category order by ParentId asc, Sort asc";
            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            return DataTableToCategoris(dt);
        }

        private List<Categories> DataTableToCategoris(DataTable dt)
        {
            List<Categories> list = new List<Categories>();
            foreach (DataRow row in dt.Rows)
            {
                Categories kw = new Categories();
                kw.Id = Convert.ToInt32(row["Id"]);
                kw.Name = (string)row["Name"];
                kw.Sort = Convert.ToInt32(row["Sort"]);
                kw.ChildrenCount = Convert.ToInt32(row["ChildrenCount"]);
                kw.Level = Convert.ToInt32(row["Level"]);
                kw.ParentId = Convert.ToInt32(row["ParentId"]);
                list.Add(kw);
            }
            return list;
        }

        public List<Categories> GetChildCategories(int ParentId)
        {
            string sql = "SELECT Id, Name, Sort, ChildrenCount, Level, ParentId FROM Category ";
            sql = sql + " where ParentId = " + ParentId + " order by ParentId asc, Sort asc ";
            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            return DataTableToCategoris(dt);
        }

        public void InsertOrUpdateCategory(Categories item)
        {
            string InsSql = @"INSERT INTO Category(Name, Sort, ChildrenCount, Level,ParentId)"
                            + "values(@Name, @Sort, @ChildrenCount, @Level,@ParentId)";
            string UpdSql = @"Update Category SET Name = @Name, Sort = @Sort, ChildrenCount = @ChildrenCount, "
                            + "Level = @Level, ParentId = @ParentId WHERE Id = @Id";
            MySqlParameter[] parameter = new MySqlParameter[]
            {
                new MySqlParameter("@Id",item.Id),
                new MySqlParameter("@Name",item.Name), 
                new MySqlParameter("@Sort",item.Sort),
                new MySqlParameter("@ChildrenCount",item.ChildrenCount),
                new MySqlParameter("@Level",item.Level),
                new MySqlParameter("@ParentId",item.ParentId)
            };
            if (item.Id == 0)
            {
                dbHelper.ExecuteNonQuery(InsSql, parameter);
            }
            else
            {
                dbHelper.ExecuteNonQuery(UpdSql, parameter);
            }
        }

        public int GetCategoryNewSortNo(int parentId, int level)
        {
            string sql = "select ifnull(max(Sort),0) + 1 from Category where ParentId = @ParentId and Level = @Level";
            MySqlParameter[] parameter = new MySqlParameter[]
            {
                new MySqlParameter("@Level",level),
                new MySqlParameter("@ParentId",parentId)
            };
            return Convert.ToInt32(dbHelper.ExecuteScalar(sql, parameter));
        }

        public void CategoryMoveSort(Categories cate1, Categories cate2)
        {
            string UpdSql = @"Update Category SET Sort = @Sort WHERE Id = @Id";
            using (MySqlConnection conn = dbHelper.GetConnection())
            {
                conn.Open();
                using (DbTransaction transaction = conn.BeginTransaction())
                {
                    dbHelper.ExecuteNonQuery(transaction, UpdSql, 
                        new MySqlParameter[]{
                            new MySqlParameter("@Id",cate1.Id),
                            new MySqlParameter("@Sort",cate2.Sort)
                         }
                    );
                    dbHelper.ExecuteNonQuery(transaction, UpdSql,
                        new MySqlParameter[]{
                            new MySqlParameter("@Id",cate2.Id),
                            new MySqlParameter("@Sort",cate1.Sort)
                         }
                    );
                    transaction.Commit();
                }
            }
        }

        public void DeleteCategory(int Id)
        {
            dbHelper.ExecuteNonQuery("delete from Category WHERE Id = " + Id);
        }

        public QueryObject<PriceCate> GetPriceCates(QueryObject<PriceCate> query)
        {
            string sql = "SELECT Id,CateName, UsePrice1, Price1Name, Price1Val, UsePrice2, Price2Name, Price2Val, UsePrice3, Price3Name, Price3Val, ";
            sql = sql + " UsePrice4, Price4Name, Price4Val, UsePrice5, Price5Name, Price5Val, Status FROM PriceCate";
            if (!query.IsExport)
            {
                query.RecordCount = dbHelper.GetItemCount(sql, null);
                sql = sql + " order by id asc limit " + query.Start + ", " + query.PageSize;
            }
            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            query.Result = DataTableToPriceCateList(dt);
            return query;
        }

        private List<PriceCate> DataTableToPriceCateList(DataTable dt)
        {
            List<PriceCate> list = new List<PriceCate>();
            foreach (DataRow row in dt.Rows)
            {
                PriceCate kw = new PriceCate();
                kw.Id = Convert.ToInt32(row["Id"]);
                kw.CateName = (string)row["CateName"];
                kw.UsePrice1 = Convert.ToBoolean(row["UsePrice1"]);
                kw.Price1Name = (string)row["Price1Name"];
                kw.Price1Val = Convert.ToDouble(row["Price1Val"]);

                kw.UsePrice2 = Convert.ToBoolean(row["UsePrice2"]);
                kw.Price2Name = (string)row["Price2Name"];
                kw.Price2Val = Convert.ToDouble(row["Price2Val"]);

                kw.UsePrice3 = Convert.ToBoolean(row["UsePrice3"]);
                kw.Price3Name = (string)row["Price3Name"];
                kw.Price3Val = Convert.ToDouble(row["Price3Val"]);

                kw.UsePrice4 = Convert.ToBoolean(row["UsePrice4"]);
                kw.Price4Name = (string)row["Price4Name"];
                kw.Price4Val = Convert.ToDouble(row["Price4Val"]);

                kw.UsePrice5 = Convert.ToBoolean(row["UsePrice5"]);
                kw.Price5Name = (string)row["Price5Name"];
                kw.Price5Val = Convert.ToDouble(row["Price5Val"]);
                kw.Status = (string)row["Status"];
                list.Add(kw);
            }
            return list;
        }

        public PriceCate GetPriceCateById(int id)
        {
            string sql = "select t.* FROM PriceCate t where Id = " + id;
            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            List<PriceCate> list = DataTableToPriceCateList(dt);
            if (list.Count > 0)
                return list[0];
            else
                return null;
        }

        public void InsertOrUpdatePriceCate(PriceCate item)
        {
            string InsSql = @"INSERT INTO PriceCate( CateName,UsePrice1, Price1Name, Price1Val, UsePrice2, Price2Name, Price2Val, UsePrice3, Price3Name, Price3Val,"
                          + "UsePrice4, Price4Name, Price4Val, UsePrice5, Price5Name, Price5Val, Status)"
                          + "values(@CateName, @UsePrice1, @Price1Name,@Price1Val, @UsePrice2, @Price2Name, @Price2Val, @UsePrice3, @Price3Name, @Price3Val,"
                          + "@UsePrice4, @Price4Name, @Price4Val, @UsePrice5, @Price5Name, @Price5Val, @Status)";
            string UpdSql = @"Update PriceCate SET CateName=@CateName,UsePrice1=@UsePrice1, Price1Name=@Price1Name, Price1Val=@Price1Val, "
                            + "UsePrice2=@UsePrice2, Price2Name=@Price2Name, Price2Val=@Price2Val, UsePrice3=@UsePrice3, Price3Name=@Price3Name,"
                            + "Price3Val=@Price3Val, UsePrice4=@UsePrice4, Price4Name=@Price4Name, Price4Val=@Price4Val, UsePrice5=@UsePrice5,  "
                            + "Price5Name=@Price5Name,Price5Val=@Price5Val, Status=@Status WHERE Id = @Id";
            MySqlParameter[] parameter = new MySqlParameter[]
            {
                new MySqlParameter("@Id",item.Id),
                new MySqlParameter("@CateName",item.CateName), 
                new MySqlParameter("@UsePrice1",item.UsePrice1), 
                new MySqlParameter("@Price1Name",item.Price1Name),
                new MySqlParameter("@Price1Val",item.Price1Val),
                new MySqlParameter("@UsePrice2",item.UsePrice2), 
                new MySqlParameter("@Price2Name",item.Price2Name),
                new MySqlParameter("@Price2Val",item.Price2Val),
                new MySqlParameter("@UsePrice3",item.UsePrice3), 
                new MySqlParameter("@Price3Name",item.Price3Name),
                new MySqlParameter("@Price3Val",item.Price3Val),
                new MySqlParameter("@UsePrice4",item.UsePrice4), 
                new MySqlParameter("@Price4Name",item.Price4Name),
                new MySqlParameter("@Price4Val",item.Price4Val),
                new MySqlParameter("@UsePrice5",item.UsePrice5), 
                new MySqlParameter("@Price5Name",item.Price5Name),
                new MySqlParameter("@Price5Val",item.Price5Val),
                new MySqlParameter("@Status",item.Status)
            };
            if (item.Id == 0)
            {
                dbHelper.ExecuteNonQuery(InsSql, parameter);
            }
            else
            {
                dbHelper.ExecuteNonQuery(UpdSql, parameter);
            }
        }

        public void DeletePriceCate(int Id)
        {
            dbHelper.ExecuteNonQuery("delete from PriceCate WHERE Id = " + Id);
        }

        public int InsertOrUpdateProduct(Product item)
        {
            string InsSql = @"INSERT INTO Product(CategoryId, Name, Model, Price,PriceCate, Minimum,Size, Weight, Packing,Description, Sort,Status,CreatedTime,ModifiedTime)"
                            + "values(@CategoryId, @Name, @Model, @Price,@PriceCate, @Minimum,@Size, @Weight, @Packing,@Description, @Sort,@Status,@CreatedTime,@ModifiedTime)";
            string UpdSql = @"Update Product SET CategoryId=@CategoryId, Name=@Name, Model=@Model, Price=@Price,PriceCate=@PriceCate,"
                    + " Minimum=@Minimum,Size=@Size, Weight=@Weight, Packing=@Packing,Description=@Description, Sort=@Sort,Status=@Status,ModifiedTime=@ModifiedTime WHERE Id = @Id";
            DateTime CurrentTime = DateTime.Now;
            MySqlParameter[] parameter = new MySqlParameter[]
            {
                new MySqlParameter("@Id",item.Id),
                new MySqlParameter("@CategoryId",item.CategoryId), 
                new MySqlParameter("@Name",item.Name),
                new MySqlParameter("@Model",item.Model),
                new MySqlParameter("@Price",item.Price),
                new MySqlParameter("@PriceCate",item.PriceCate),
                new MySqlParameter("@Minimum",item.Minimum), 
                new MySqlParameter("@Size",item.Size),
                new MySqlParameter("@Weight",item.Weight),
                new MySqlParameter("@Packing",item.Packing),
                new MySqlParameter("@Description",item.Description),
                new MySqlParameter("@Sort",item.Sort),
                new MySqlParameter("@Status",item.Status),
                new MySqlParameter("@CreatedTime",CurrentTime),
                new MySqlParameter("@ModifiedTime",CurrentTime),
            };
            if (item.Id == 0)
            {
                using (MySqlConnection connection = dbHelper.GetConnection())
                {
                    connection.Open();
                    using (DbTransaction transaction = connection.BeginTransaction())
                    {
                        dbHelper.ExecuteNonQuery(transaction, InsSql, parameter);
                        item.Id = dbHelper.GetLastInsertId(transaction);
                        transaction.Commit();
                    }
                }
            }
            else
            {
                dbHelper.ExecuteNonQuery(UpdSql, parameter);
                return item.Id;
            }
            return item.Id;
        }

        public Product GetProductById(int id)
        {
            string sql = "select t.* FROM Product t where id = " + id;
            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            List<Product> list = DataTableToList(dt);
            if (list.Count > 0)
                return list[0];
            else
                return null;
        }

        private List<Product> DataTableToList(DataTable dt)
        {
            List<Product> list = new List<Product>();
            foreach (DataRow row in dt.Rows)
            {
                Product info = new Product();
                info.Id = Convert.ToInt32(row["Id"]);
                info.CategoryId = Convert.ToInt32(row["CategoryId"]);
                info.Name = (string)row["Name"];
                info.Model = (string)row["Model"];
                info.Price = Convert.ToDouble(row["Price"]);
                info.PriceCate = Convert.ToInt32(row["PriceCate"]);
                info.Minimum = Convert.ToInt32(row["Minimum"]);
                info.Size = (string)row["Size"];
                info.Weight = (string)row["Weight"];
                info.Packing = (string)row["Packing"];
                info.Sort = Convert.ToInt32(row["Sort"]);
                info.Status = (string)row["Status"];
                info.Description = (string)row["Description"];
                info.Image = GetMainImageByProductId(info.Id);
                list.Add(info);
            }
            return list;
        }

        public QueryObject<Product> GetProducts(QueryObject<Product> query)
        {
            string sql = "select Id, CategoryId, Name, Model, Price,PriceCate, Minimum,Size, Weight,Description, Packing,Sort,Status FROM Product where 1 = 1 ";
            List<MySqlParameter> QueryParameters = new List<MySqlParameter>();
            if (query.Condition != null)
            {
                if (!string.IsNullOrEmpty(query.Condition.Name))
                {
                    sql = sql + "and Name like '%" + query.Condition.Name.Trim() + "%' ";
                }
                if (!string.IsNullOrEmpty(query.Condition.Model))
                {
                    sql = sql + "and Model like '%" + query.Condition.Model.Trim() + "%' ";
                }
                if (query.Condition.CategoryId != 0)
                {
                    sql = sql + "and CategoryId = @CategoryId ";
                    QueryParameters.Add(new MySqlParameter("@CategoryId", query.Condition.CategoryId));
                }
                if (!string.IsNullOrEmpty(query.Condition.Status))
                {
                    sql = sql + "and Status = @Status ";
                    QueryParameters.Add(new MySqlParameter("@Status", query.Condition.Status));
                }
            }
            if (query.IsExport == false)
            {
                query.RecordCount = dbHelper.GetItemCount(sql, QueryParameters.ToArray());
                sql = sql + " order by ModifiedTime desc limit " + query.Start + ", " + query.PageSize;
            }
            else
            {
                sql = sql + " order by id asc";
            }

            query.dt = dbHelper.ExecuteDataTable(sql, QueryParameters.ToArray());
            query.Result = DataTableToList(query.dt);
            return query;
        }

        public void UpdateStatus(int productId, string status)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@Id", productId));
            parameters.Add(new MySqlParameter("@Status", status));
            dbHelper.ExecuteNonQuery("update Product set Status = @Status  WHERE Id = @Id ", parameters.ToArray());
        }

        public byte[] GetProductImage(int ProductImageId)
        {
            return dbHelper.GetBlogField("select Image FROM Product_Image where Id = " + ProductImageId);
        }

        public void InsertOrUpdateProductImage(ProductImage item)
        {
            string InsSql = @"INSERT INTO Product_Image(ProductId, Image,Size, IsMain,CreatedTime,ModifiedTime)"
                            + "values(@ProductId, @Image,@Size, @IsMain,@CreatedTime,@ModifiedTime)";
            string UpdSql = @"Update Product_Image SET Size=@Size, Image=@Image, ModifiedTime=@ModifiedTime WHERE Id = @Id";
            
            DateTime CurrentTime = DateTime.Now;
            MySqlParameter[] parameter = new MySqlParameter[]
            {
                new MySqlParameter("@Id",item.Id),
                new MySqlParameter("@ProductId",item.ProductId),
                new MySqlParameter("@Image", item.Image), 
                new MySqlParameter("@Size",item.Size), 
                new MySqlParameter("@IsMain",item.IsMain),
                new MySqlParameter("@CreatedTime",CurrentTime),
                new MySqlParameter("@ModifiedTime",CurrentTime)
            };

            if (item.Id == 0)
            {
                dbHelper.ExecuteNonQuery(InsSql, parameter);
            }
            else 
            {
                dbHelper.ExecuteNonQuery(UpdSql, parameter);
            }
        }

        public void DeleteProductImage(int ProductImageId)
        {
            dbHelper.ExecuteNonQuery("delete from Product_Image WHERE Id = " + ProductImageId);
        }

        private ProductImage GetMainImageByProductId(int ProductId)
        {
            string sql = "SELECT Id, ProductId, Image,Size, IsMain,CreatedTime,ModifiedTime "
                       + " FROM Product_Image where ProductId = "
                       + ProductId + " and IsMain = 1 limit 0,1";
            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            foreach (DataRow row in dt.Rows)
            {
                ProductImage kw = new ProductImage();
                kw.Id = Convert.ToInt32(row["Id"]);
                kw.ProductId = Convert.ToInt32(row["ProductId"]);
                kw.Size = Convert.ToInt64(row["Size"]);
                kw.IsMain = Convert.ToBoolean(row["IsMain"]);
                kw.CreatedTime = Convert.ToDateTime(row["CreatedTime"]);
                kw.ModifiedTime = Convert.ToDateTime(row["ModifiedTime"]);
                return kw;
            }
            return null;
        }

        public List<ProductImage> GetImagesInfoByProductId(int ProductId)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@ProductId", ProductId));
            string sql = "SELECT Id, ProductId, Image, Size, IsMain, CreatedTime, ModifiedTime "
                + " FROM Product_Image where ProductId = @ProductId order by IsMain desc, Id asc";
            DataTable dt = dbHelper.ExecuteDataTable(sql, parameters.ToArray());
            List<ProductImage> list = new List<ProductImage>();
            foreach (DataRow row in dt.Rows)
            {
                ProductImage kw = new ProductImage();
                kw.Id = Convert.ToInt32(row["Id"]);
                kw.ProductId = Convert.ToInt32(row["ProductId"]);
                kw.Size = Convert.ToInt64(row["Size"]);
                kw.IsMain = Convert.ToBoolean(row["IsMain"]);
                kw.CreatedTime = Convert.ToDateTime(row["CreatedTime"]);
                kw.ModifiedTime = Convert.ToDateTime(row["ModifiedTime"]);
                list.Add(kw);
            }
            return list;
        }
    
    }

}
