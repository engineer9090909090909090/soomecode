using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using AliHelper.DAO;

namespace AliHelper
{
    public class ProductsManager : BaseManager
    {
        public event NewEditItemEvent OnAddOrUpdateItemEvent;
        public AliGroupDao groupDao;
        public AliProductDao productDao;
        public AliImageDao aliImageDao;
        public AliProductDetailDao detailDao;
        public ProductsManager()
        {
            groupDao = DAOFactory.Instance.GetAliGroupDao();
            productDao = DAOFactory.Instance.GetAliProductDao();
            aliImageDao = DAOFactory.Instance.GetAliImageDao();
            detailDao = DAOFactory.Instance.GetAliProductDetailDao();
        }

        public virtual void FireNewUpdateEvent(object o)
        {
            if (OnAddOrUpdateItemEvent != null)
            {
                ItemEventArgs e = new ItemEventArgs(o);
                OnAddOrUpdateItemEvent(this, e);
            }
        }

        public void UpdateGroupProdcuts(int GroupId, List<AliProduct> products)
        {
            //productDao.DeleteProduct4GroupId(GroupId);
            productDao.InsertOrUpdate(products);
        }

        public void InsertOrUpdateProdcutDetail(ProductDetail detail)
        {
            detailDao.InsertOrUpdate(detail);
            FireNewUpdateEvent(detail.pid);
        }

        public void UpdateGroups(List<AliGroup> groups)
        {
            List<int> existGroupIds = groupDao.GetAliGroupIdList();
            List<AliGroup> InsertList = new List<AliGroup>();
            List<AliGroup> UpdateList = new List<AliGroup>();
            foreach (AliGroup item in groups)
            {
                if (existGroupIds.Contains(item.Id))
                {
                    UpdateList.Add(item);
                    existGroupIds.Remove(item.Id);
                }
                else
                {
                    InsertList.Add(item);
                }
            }
            foreach (int item in existGroupIds)
            {
                productDao.DeleteProduct4GroupId(item);
            }
            groupDao.DeleteGroups(existGroupIds);
            groupDao.Insert(InsertList);
            groupDao.Update(UpdateList);
        }


        public List<AliGroup> GetGroupList()
        {
            return groupDao.GetAliGroupList();
        }

        public List<AliProduct> GetProductList(int GroupId)
        {
            return productDao.GetAliProductList(GroupId);
        }

        public void UpdateImageInfos(List<ImageInfo> images)
        {
            aliImageDao.DeleteAllImages();
            aliImageDao.Insert(images);
        }

        public QueryObject<ImageInfo> GetImageInfoList(QueryObject<ImageInfo> query)
        {
            return aliImageDao.GetAliImageList(query);
        }

        public void InitGroupOptions()
        {
            List<FormElement> elList = new List<FormElement>();
            List<AliGroup> groups = this.GetGroupList();
            foreach (AliGroup a in groups)
            {
                if (a.Level == 1)
                {
                    FormElement el = new FormElement();
                    el.Name = a.Name;
                    el.Value = a.Id.ToString() + "_-1_-1";
                    elList.Add(el);
                    if (a.HasChildren)
                    {
                        foreach (AliGroup b in groups)
                        {
                            if (b.ParentId == a.Id && b.Level == a.Level + 1)
                            {
                                FormElement e2 = new FormElement();
                                e2.Name = a.Name + ">" + b.Name;
                                e2.Value = a.Id.ToString() + "_" + b.Id.ToString() + "_-1";
                                elList.Add(e2);
                                if (b.HasChildren)
                                {
                                    foreach (AliGroup c in groups)
                                    {
                                        if (b.ParentId == c.Id && b.Level == c.Level + 1)
                                        {
                                            FormElement e3 = new FormElement();
                                            e3.Name = a.Name + ">" + b.Name + ">" + c.Name;
                                            e3.Value = a.Id.ToString() + "_" + b.Id.ToString() + "_" + c.Id.ToString();
                                            elList.Add(e3);
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            DataCache.Instance.GroupListOptions = elList;
        }

        public ProductDetail GetProductDetail(int id)
        {
            return detailDao.GetProductDetail(id);
        }

        public AliProduct GetAliProduct(int Id)
        {
            return productDao.GetAliProduct(Id);
        }

        public bool IsNeedUpdateDetail(int id)
        {
            return productDao.IsNeedUpdateDetail(id);
        }
    }
}
