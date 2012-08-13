using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace WebTools
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

        public void InsertProducts(List<ProductModel> modelList)
        {
            using (MySqlConnection connection = MySqlHelper.GetConnection())
            {
                connection.Open();
                MySqlTransaction trans = connection.BeginTransaction();
                try
                {
                    foreach (ProductModel model in modelList)
                    {
                        ProductModel model1 = AddProduct(model, trans);
                        AddProductDesc(model1, trans);
                        AddProductCategories(model1, trans);
                        AddProductImages(model1, trans);
                        AddProductTags(model1, trans);
                        AddProductStore(model1, trans);
                    }
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    System.Diagnostics.Trace.WriteLine(e.InnerException.Message);
                    throw e;
                }
            }
        
        }
        private ProductModel AddProduct(ProductModel model, MySqlTransaction trans)
        {
            string sql = "INSERT INTO `product` (`model`,`sku`,`upc`,`location`,`quantity`,`stock_status_id`,"
               +" `image`,`manufacturer_id`,`shipping`,`price`,`points`,`tax_class_id`,`date_available`,"
               +" `weight`,`weight_class_id`,`length`,`width`,`height`,`length_class_id`,`minimum`,`sort_order`,"
               +" `status`,`date_added`,`date_modified`)VALUES (@model,@sku,@upc,'',@quantity,"
               +" @stock_status_id,@image,@manufacturer_id,@shipping,@price,0, @tax_class_id,"
               +" @date_available,@weight,@weight_class_id,@length,@width,@height,@length_class_id,"
               +" @minimum,@sort_order,1,SYSDATE(),SYSDATE());";
            MySqlParameter[] parameter = new MySqlParameter[] 
            { 
                new MySqlParameter("@model",model.Model),
                new MySqlParameter("@sku",model.Sku),
                new MySqlParameter("@upc",model.Upc),
                new MySqlParameter("@quantity",model.Quantity),
                new MySqlParameter("@stock_status_id",model.StockStatusId),
                new MySqlParameter("@image",model.Image),
                new MySqlParameter("@manufacturer_id",model.ManufacturerId),
                new MySqlParameter("@shipping",model.shipping),
                new MySqlParameter("@price",model.Price),
                new MySqlParameter("@tax_class_id",model.TaxClassId),
                new MySqlParameter("@date_available",model.AvailableDate),
                new MySqlParameter("@weight",model.Weight),
                new MySqlParameter("@weight_class_id",model.WeightClassId),
                new MySqlParameter("@length",model.Length),
                new MySqlParameter("@width",model.Width),
                new MySqlParameter("@height",model.Height),
                new MySqlParameter("@length_class_id",model.LengthClassId),
                new MySqlParameter("@minimum",model.Minimum),
                new MySqlParameter("@sort_order",model.SortOrder)
            };
            MySqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, parameter);
            model.ProductId = Convert.ToInt32(MySqlHelper.ExecuteScalar(@"select max(Product_id) from product", null));
            return model;
        }

        private void AddProductDesc(ProductModel model, MySqlTransaction trans)
        {
            string sql = "INSERT INTO `product_description` (`product_id`, `language_id`, "
                +" `name`, `description`, `description2`, `meta_description`, `meta_keyword`)"
                +" VALUES(@product_id, @language_id, @name, "
                +" @description, @description2, @meta_description,@meta_keyword);";
            MySqlParameter[] parameter = new MySqlParameter[]
            { 
                new MySqlParameter("@product_id",model.ProductId),
                new MySqlParameter("@language_id",model.LanguageId),
                new MySqlParameter("@name",model.Name),
                new MySqlParameter("@description",model.Description),
                new MySqlParameter("@description2",model.Description2),
                new MySqlParameter("@meta_description",model.MetaDescription),
                new MySqlParameter("@meta_keyword",model.MetaKeyword)
            };
            MySqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, parameter);
        }


        private void AddProductCategories(ProductModel model, MySqlTransaction trans)
        {
            string sql = "INSERT INTO `product_to_category` (`product_id`, `category_id`)VALUES(@product_id, @category_id);";
            List<CategoryModel> CategoryList = model.Categories;
            if (CategoryList != null)
            {
                foreach (CategoryModel cmodel in CategoryList)
                {
                    MySqlParameter[] parameter = new MySqlParameter[]
                    { 
                        new MySqlParameter("@product_id",model.ProductId),
                        new MySqlParameter("@category_id",cmodel.CategoryId)
                    };
                    MySqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, parameter);
                }
            }
        }

         private void AddProductImages(ProductModel model, MySqlTransaction trans)
         {
             string sql = "INSERT INTO `product_image` (`product_id`, `image`)VALUES( @product_id, @image);";
             List<ImageModel> imageList = model.Images;
             if (imageList != null)
             {
                 foreach (ImageModel imodel in imageList)
                 {
                     MySqlParameter[] parameter = new MySqlParameter[]
                    { 
                        new MySqlParameter("@product_id",model.ProductId),
                        new MySqlParameter("@image",imodel.Image)
                    };
                    MySqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, parameter);
                 }
             }
         }

         private void AddProductTags(ProductModel model, MySqlTransaction trans)
         {
             string sql = "INSERT INTO `product_tag` (`product_id`, `language_id`, `tag`)VALUES(@product_id, @language_id, @tag);";
             List<TagModel> tagList = model.Tags;
             if (tagList != null)
             {
                 foreach (TagModel tmodel in tagList)
                 {
                     MySqlParameter[] parameter = new MySqlParameter[]
                    { 
                        new MySqlParameter("@product_id",model.ProductId),
                        new MySqlParameter("@language_id",model.LanguageId),
                        new MySqlParameter("@tag",tmodel.Tag)
                    };
                    MySqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, parameter);
                 }
             }
         }

         private void AddProductStore(ProductModel model, MySqlTransaction trans)
         {
                string sql = "insert into product_to_store(product_id, store_id)VALUES(@product_id, 0);";
                MySqlParameter[] parameter = new MySqlParameter[]
                { 
                    new MySqlParameter("@product_id",model.ProductId)
                };
                MySqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, parameter);
         }

         private void AddProductRelateds(ProductModel model, MySqlTransaction trans)
         {
             string sql = "INSERT INTO `product_related` (`product_id`, `related_id`)VALUES(@product_id, @related_id);";
             List<RelatedModel> rList = model.Relateds;
             if (rList != null)
             {
                 foreach (RelatedModel rmodel in rList)
                 {
                     MySqlParameter[] parameter = new MySqlParameter[]
                    { 
                        new MySqlParameter("@product_id",model.ProductId),
                        new MySqlParameter("@related_id",rmodel.RelatedId)
                    };
                     MySqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, parameter);
                 }
             }
         }

         public int getMaxSortOrder(List<CategoryModel> CategoryList)
         {
             int maxid = 0;
             foreach (CategoryModel categoryModel in CategoryList)
             {
                 if (categoryModel.CategoryId > maxid)
                 {
                     maxid = categoryModel.CategoryId;
                 }
             }
             string sql = "select max(p.sort_order) from product p join product_to_category c " +
                         " on p.product_id = c.product_id where c.category_id= " + maxid;
             object sortOrder = MySqlHelper.ExecuteScalar(sql, null);
             if (Convert.IsDBNull(sortOrder))
             {
                 return Convert.ToInt32(maxid.ToString() + "00001");
             }
             return Convert.ToInt32(sortOrder);
         }
    }

    
}
