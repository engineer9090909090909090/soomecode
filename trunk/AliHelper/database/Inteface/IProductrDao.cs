using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;

namespace Database
{
    public interface IProductDao
    {

        List<Categories> GetAllCategories();

        List<Categories> GetChildCategories(int ParentId);

        void InsertOrUpdateCategory(Categories item);

        int GetCategoryNewSortNo(int parentId, int level);

        void CategoryMoveSort(Categories cate1, Categories cate2);

        void DeleteCategory(int Id);

        QueryObject<PriceCate> GetPriceCates(QueryObject<PriceCate> query);

        PriceCate GetPriceCateById(int id);

        void InsertOrUpdatePriceCate(PriceCate item);

        void DeletePriceCate(int Id);

        int InsertOrUpdateProduct(Product item);

        Product GetProductById(int id);

        QueryObject<Product> GetProducts(QueryObject<Product> query);

        void UpdateStatus(int productId, string status);

        byte[] GetProductImage(int ProductImageId);

        void InsertProductImage(ProductImage item);

        void DeleteProductImage(int ProductImageId);

        List<ProductImage> GetImagesInfoByProductId(int ProductId);
    }
}
