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
        static public event NewEditItemEvent OnEditCategoryEvent;
        static public event NewEditItemEvent OnEditPriceCateEvent;

        public virtual void FireEditCategoryEvent(object o)
        {
            if (MyItemManager.OnEditCategoryEvent != null)
            {
                ItemEventArgs e = new ItemEventArgs(o);
                OnEditCategoryEvent(this, e);
            }
        }

        public virtual void FireEditPriceCateEvent(object o)
        {
            if (MyItemManager.OnEditPriceCateEvent != null)
            {
                ItemEventArgs e = new ItemEventArgs(o);
                OnEditPriceCateEvent(this, e);
            }
        }

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
                FireEditCategoryEvent(null);
                return;
            }
            throw new Exception("包含子类，不能被删除，请先删除子类。");
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
            FireEditCategoryEvent(item);
        }

        public int GetCategoryNewSortNo(int parentId, int level)
        {
          return productDao.GetCategoryNewSortNo(parentId, level);
        }

        public void CategoryMoveSort(Categories cate1, Categories cate2)
        {
            productDao.CategoryMoveSort(cate1, cate2);
            FireEditCategoryEvent(cate1);
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
            FireEditPriceCateEvent(item);
        }

        public void DeletePriceCate(int Id)
        {
            productDao.DeletePriceCate(Id);
            FireEditPriceCateEvent(Id);
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
