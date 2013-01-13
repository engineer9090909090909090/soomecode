using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;

namespace AliHelper
{
    public class HttpClient
    {
        public static string GroupListRequest = "http://hz.productposting.alibaba.com/product/group_ajax.htm?event=listGroup&parentGroupId={0}&_csrf_token_={1}&pageSize=";

        public static string ProudctListRequest = "http://hz.productposting.alibaba.com/product/managementproducts/asyQueryProductList.do?status=approved&imageType=all&repositoryType=all&page={0}&size=50&changePageSize=Y&_csrf_token_={1}&groupId={2}&groupLevel={3}";


        public static List<AliProduct> GetProducts(Hashtable groupDic, int groupId, int groupLevel, string csrfToken)
        {
            List<AliProduct> produtList = new List<AliProduct>();
            string prodcutsReqUrl = string.Format(ProudctListRequest, 1, csrfToken, groupId, groupLevel);
            string prodcutsJsonText = IEHandleUtils.GetHtml(prodcutsReqUrl);
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
                prodcutsJsonText = IEHandleUtils.GetHtml(prodcutsReqUrl);
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
            string groupJsonText = IEHandleUtils.GetHtml(groupReqUrl);
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

        public static List<CategroyNode> SearchCategories(string key)
        {
            string SearchUrl = @"http://hz.productposting.alibaba.com/product/recommend_post_category_ajax.htm?keyword={0}&language=en_us&_updateTime={1}";
            SearchUrl = string.Format(SearchUrl, key.Replace(" ", "+"), DateUtils.DateTimeToInt(DateTime.Now));
            string html = IEHandleUtils.GetHtml(SearchUrl);
            string json = RegexFetchJson.FetchJson("var categroyData =(.*?); return categroyData;", html);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            CategroyDataJson categroyData = JsonConvert.FromJson<CategroyDataJson>(json);
            if (categroyData == null || categroyData.Category == null)
            {
                return null;
            } 
            return categroyData.Category;
        }

        public static List<AttributeNode> GetSelectCategoryAttributes(string categoryId)
        {
            string SearchUrl = @"http://hz.productposting.alibaba.com/product/selectedCategoryAjax.htm";
            string postString = "action=selected_category_ajax_action";
            postString = postString + "&_fmp.se._0.c=" + categoryId;
            postString = postString + "&event_submit_do_selected_category=anything";
            postString = postString + "&time=0.04443414613303159";

            string html = IEHandleUtils.GetHtml(SearchUrl, postString);
            AttributeNodeJson attributeNodeJson = JsonConvert.FromJson<AttributeNodeJson>(html);
            if (attributeNodeJson == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(attributeNodeJson.BindAttrjson))
            {
                attributeNodeJson.BindAttributeNodes = JsonConvert.FromJson<List<AttributeNode>>(attributeNodeJson.BindAttrjson);
            }
            if (!string.IsNullOrEmpty(attributeNodeJson.SysAttrjson))
            {
                attributeNodeJson.SysAttributeNodes = JsonConvert.FromJson<List<AttributeNode>>(attributeNodeJson.SysAttrjson);
            }
            return attributeNodeJson.SysAttributeNodes;
        }

        public static List<ImageGroupNode> GetImageGroup(string branch)
        {
            string url = @"http://sh.vip.alibaba.com/photobank/ajaxImageGroup.htm?topLevel=true&event=listSubGroups&levelCode=,,";
            if (!string.IsNullOrEmpty(branch))
            {
                url = "http://sh.vip.alibaba.com/photobank/" + branch;
            }
            string html = IEHandleUtils.GetHtml(url);
            List<ImageGroupNode> imageGroupNodes = JsonConvert.FromJson<List<ImageGroupNode>>(html);
            return imageGroupNodes;
        }

        public static List<ImageInfo> GetAllImages()
        {
            List<ImageInfo> imageInfoList = new List<ImageInfo>();
            ImageInfoJson infoJson = GetAllGroupImages(1);
            imageInfoList.AddRange(infoJson.ImageInfos);
            for (int i = 2; i <= infoJson.Query.TotalPage; i++)
            {
                //ImageInfoJson itemJson = GetAllGroupImages(i);
                //imageInfoList.AddRange(itemJson.ImageInfos);
            }
            WebClient webClient = new WebClient();
            foreach (ImageInfo imageInfo in imageInfoList)
            {
                try
                {
                    imageInfo.LocationUrl = FileUtils.DownloadImage(webClient, imageInfo.Url, imageInfo.Id);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.WriteLine(e.Message);
                    imageInfo.LocationUrl = "";
                }
            }
            webClient.Dispose();
            return imageInfoList;
        }

        public static ImageInfoJson GetAllGroupImages(int page)
        {
            string url = "http://sh.vip.alibaba.com/photobank/ajaxPhotobank.htm";
            string postString = string.Format("event=searchImage&location=allGroup&page={0}", page);
            string html = IEHandleUtils.GetHtml(url, postString);
            ImageInfoJson imageInfoJson = JsonConvert.FromJson<ImageInfoJson>(html);
            Dictionary<int, string> dic = imageInfoJson.UrlMap;
            foreach (ImageInfo imageInfo in imageInfoJson.ImageInfos)
            {
                imageInfo.Url = dic[imageInfo.Id];
            }
            return imageInfoJson;
        }

        public static ImageInfoJson GetImages(int groupId, int page)
        {
            string url = "http://sh.vip.alibaba.com/photobank/ajaxPhotobank.htm";
            string postString = string.Format("event=searchImage&location=subGroup&groupId={0}&page={1}", groupId, page);
            string html = IEHandleUtils.GetHtml(url, postString);
            ImageInfoJson imageInfoJson = JsonConvert.FromJson<ImageInfoJson>(html);
            Dictionary<int,string> dic = imageInfoJson.UrlMap;
            WebClient webClient = new WebClient();
            foreach (ImageInfo imageInfo in imageInfoJson.ImageInfos)
            {
                imageInfo.Url = dic[imageInfo.Id];
                try
                {
                    imageInfo.LocationUrl = FileUtils.DownloadImage(webClient, imageInfo.Url, imageInfo.Id);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.WriteLine(e.Message);
                    imageInfo.LocationUrl = "";
                }
            }
            webClient.Dispose();
            return imageInfoJson;
        }

        public static string GetCsrfToken(string html)
        {
            Regex r = new Regex("var _csrf_ = {'_csrf_token_':'(.*?)'};");
            GroupCollection gc = r.Match(html).Groups;
            if (gc != null && gc.Count > 1)
            {
                return gc[1].Value.Trim();
            }
            return "";
        }
    }
}

