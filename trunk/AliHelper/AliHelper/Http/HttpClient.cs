using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Collections;

namespace AliHelper.Http
{
    public class HttpClient
    {
        public static string GroupListRequest = "http://hz.productposting.alibaba.com/product/group_ajax.htm?event=listGroup&parentGroupId={0}&_csrf_token_={1}&pageSize=";

        public static string ProudctListRequest = "http://hz.productposting.alibaba.com/product/managementproducts/asyQueryProductList.do?status=approved&imageType=all&repositoryType=all&page={0}&size=50&changePageSize=Y&_csrf_token_={1}&groupId={2}&groupLevel={3}";


        public static List<AliProduct> GetProducts(Hashtable groupDic, int groupId, int groupLevel, string csrfToken)
        {
            List<AliProduct> produtList = new List<AliProduct>();
            string prodcutsReqUrl = string.Format(ProudctListRequest, 1, csrfToken, groupId, groupLevel);
            string prodcutsJsonText = IEHandleUtils.WebRequestGetUrlHtml(prodcutsReqUrl);
            AliProductInfo productsInfo = JsonConvert.FromJson<AliProductInfo>(prodcutsJsonText);
            foreach (AliProduct p in productsInfo.Products)
            {
                string groupIds = string.Empty;
                if (groupDic.ContainsKey(p.GroupName3))
                {
                    groupIds = Convert.ToString(groupDic[p.GroupName3]) + "-" + groupIds;
                }
                if (groupDic.ContainsKey(p.GroupName2))
                {
                    groupIds = Convert.ToString(groupDic[p.GroupName2]) + "-" + groupIds;
                }
                p.GroupId = Convert.ToString(groupDic[p.GroupName1]) + "-" + groupIds;
                produtList.Add(p);
            }
            int pageNumber = productsInfo.Count / 50 + ((productsInfo.Count % 50 > 0) ? 1 : 0);
            for (int i = 2; i <= pageNumber; i++)
            {
                prodcutsReqUrl = string.Format(ProudctListRequest, i, csrfToken, groupId, groupLevel);
                prodcutsJsonText = IEHandleUtils.WebRequestGetUrlHtml(prodcutsReqUrl);
                AliProductInfo obj = JsonConvert.FromJson<AliProductInfo>(prodcutsJsonText);
                foreach (AliProduct p in obj.Products)
                {
                    string groupIds = string.Empty;
                    if (groupDic.ContainsKey(p.GroupName3))
                    {
                        groupIds = Convert.ToString(groupDic[p.GroupName3]) + "-" + groupIds;
                    }
                    if (groupDic.ContainsKey(p.GroupName2))
                    {
                        groupIds = Convert.ToString(groupDic[p.GroupName2]) + "-" + groupIds;
                    }
                    p.GroupId = Convert.ToString(groupDic[p.GroupName1]) + "-" + groupIds;
                    produtList.Add(p);
                }
            }
            return produtList;
        }


        public static List<AliGroup> GetGroups(int parentId, int parentLevel, string csrfToken)
        {
            List<AliGroup> groups = new List<AliGroup>();
            string groupReqUrl = string.Format(GroupListRequest, parentId, csrfToken);
            string groupJsonText = IEHandleUtils.WebRequestGetUrlHtml(groupReqUrl);
            AliGroupInfo groupInfo = JsonConvert.FromJson<AliGroupInfo>(groupJsonText);
            foreach (AliGroup g in groupInfo.Data)
            {
                g.ParentId = parentId;
                g.Level = parentLevel + 1;
                groups.Add(g);
            }
            foreach (AliGroup g in groupInfo.Data)
            {
                if (g.HasChildren)
                {
                    groups.AddRange(GetGroups(g.Id, g.Level, csrfToken));
                }
            }
            return groups;
        }

    }
}
