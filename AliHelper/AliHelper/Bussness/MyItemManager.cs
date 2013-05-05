using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Database;
using Soomes;
using System.IO;

namespace AliHelper
{
    public class MyItemManager : BaseManager
    {
        public IProductDao productDao;
        static public event NewEditItemEvent OnEditCategoryEvent;
        static public event NewEditItemEvent OnEditPriceCateEvent;
        static public event NewEditItemEvent OnEditProductEvent;

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

        public virtual void FireEditProductEvent(object o)
        {
            if (MyItemManager.OnEditProductEvent != null)
            {
                ItemEventArgs e = new ItemEventArgs(o);
                OnEditProductEvent(this, e);
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

        public int InsertOrUpdateProduct(Product item, List<ProductImage> imageFiles)
        {
            int productId = productDao.InsertOrUpdateProduct(item);
            List<ProductImage> orgiImageList = this.GetImagesInfoByProductId(item.Id);
            if (imageFiles != null && imageFiles.Count > 0)
            {
                foreach(ProductImage image in imageFiles)
                {
                    if (image.Id == 0)
                    {
                        image.ProductId = productId;
                        productDao.InsertOrUpdateProductImage(image);
                    }
                    else 
                    {
                        foreach (ProductImage orgi in orgiImageList)
                        {
                            if (orgi.Id == image.Id)
                            {
                                if (orgi.Size != image.Size)
                                {
                                    productDao.InsertOrUpdateProductImage(image);
                                }
                                orgiImageList.Remove(orgi);
                                break;
                            }
                        }
                    }
                }
            }
            foreach (ProductImage orgi in orgiImageList)
            {
                productDao.DeleteProductImage(orgi.Id);
            }
            FireEditProductEvent(item);
            return productId;
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
            FireEditProductEvent(null);
        }

        public byte[] GetProductImage(int ProductImageId)
        {
            return productDao.GetProductImage(ProductImageId);
        }


        public string GetProductImageFile(int ProductId, ProductImage image)
        {
            string imageDir = FileUtils.GetUserDataFolder() + Path.DirectorySeparatorChar + Constants.MyItemImages;
            if (!Directory.Exists(imageDir))
            {
                Directory.CreateDirectory(imageDir);
            }
            string imageFile = imageDir + Path.DirectorySeparatorChar + ProductId + "_" + image.Id + ".jpg";
            if (File.Exists(imageFile))
            {
                Int64 size = new FileInfo(imageFile).Length;
                if (size != image.Size)
                {
                    byte[] imageBuffer = this.GetProductImage(image.Id);
                    FileUtils.ByteArrayToImageFile(imageBuffer, imageFile);
                    imageBuffer = null;
                }
            }
            else
            {
                byte[] imageBuffer = this.GetProductImage(image.Id);
                FileUtils.ByteArrayToImageFile(imageBuffer, imageFile);
                imageBuffer = null;
            }
            return imageFile;
        }

        public List<ProductImage> GetImagesInfoByProductId(int ProductId)
        {
            return productDao.GetImagesInfoByProductId(ProductId);
        }
    }
}
