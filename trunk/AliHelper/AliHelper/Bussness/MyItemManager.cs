using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Database;
using Soomes;

namespace AliHelper
{
    public class MyItemManager : BaseManager
    {
        public IProductDao productDao;

        public MyItemManager()
        {
            productDao = DAOFactory.GetInstance().GetProductDao();
        }

        public void DelteCategory(int id)
        {
            List<Categories> list = productDao.GetChildCategories(id);
            if (list.Count == 0)
            {
                productDao.DeleteCategory(id);
                return;
            }
            throw new Exception("包含子类，不能删除。");
        }


        public List<Categories> GetAllCategories()
        {
            return productDao.GetAllCategories();
        }

        public List<Categories> GetChildCategories(int ParentId)
        {
            return productDao.GetChildCategories(ParentId);
        }

        public void InsertOrUpdateCategory(Categories item) 
        {
            productDao.InsertOrUpdateCategory(item);
        }

        public int GetCategoryNewSortNo(int parentId, int level)
        {
          return productDao.GetCategoryNewSortNo(parentId, level);
        }

        public void CategoryMoveSort(Categories cate1, Categories cate2)
        {
            productDao.CategoryMoveSort(cate1, cate2);
        }

        public void DeleteCategory(int Id)
        {
            productDao.DeleteCategory(Id);
        }

        public QueryObject<PriceCate> GetPriceCates(QueryObject<PriceCate> query)
        {
            return productDao.GetPriceCates(query);
        }

        public PriceCate GetPriceCateById(int id)
        {
            return productDao.GetPriceCateById(id);
        }

        public void InsertOrUpdatePriceCate(PriceCate item)
        {
            productDao.InsertOrUpdatePriceCate(item);
        }

        public void DeletePriceCate(int Id)
        {
            productDao.DeletePriceCate(Id);
        }

        public void InsertOrUpdateProduct(Product item)
        {
            productDao.InsertOrUpdateProduct(item);
        }

        public Product GetProductById(int id)
        {
            return productDao.GetProductById(id);
        }

        public QueryObject<Product> GetProducts(QueryObject<Product> query)
        {
            return productDao.GetProducts(query);
        }

        public void UpdateStatus(int productId, string status)
        {
            productDao.UpdateStatus(productId, status);
        }

        public byte[] GetProductImage(int ProductImageId)
        {
            return productDao.GetProductImage(ProductImageId);
        }

        public void InsertProductImage(ProductImage item)
        {
            productDao.InsertProductImage(item);
        }

        public void DeleteProductImage(int ProductImageId)
        {
            productDao.DeleteProductImage(ProductImageId);
        }

        public List<ProductImage> GetImagesInfoByProductId(int ProductId)
        {
            return productDao.GetImagesInfoByProductId(ProductId);
        }
    }
}
