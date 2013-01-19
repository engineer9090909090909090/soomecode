using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;
using System.Threading;
using HtmlAgilityPack;

namespace AliHelper
{
    public class HttpClient
    {
        public static string GroupListRequest = "http://hz.productposting.alibaba.com/product/group_ajax.htm?event=listGroup&parentGroupId={0}&_csrf_token_={1}&pageSize=";

        public static string ProudctListRequest = "http://hz.productposting.alibaba.com/product/managementproducts/asyQueryProductList.do?status=approved&imageType=all&repositoryType=all&page={0}&size=50&changePageSize=Y&_csrf_token_={1}&groupId={2}&groupLevel={3}";

        public static string RemoteRequest(string url, string postString)
        {
            string html = IEHandleUtils.GetHtml(url, postString);
            if (html.IndexOf("isNeedImagePassword") > 0)
            {
                string CheckCode = string.Empty;
                CheckCodeForm checkCodeForm = new CheckCodeForm();
                if (checkCodeForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    CheckCode = checkCodeForm.CheckCode;
                    checkCodeForm.Close();
                }
                url = url + "&imagePassword=" + CheckCode;
                return RemoteRequest(url, postString);
            }
            return html;

        }

        public static List<AliProduct> GetProducts(Hashtable groupDic, int groupId, int groupLevel, string csrfToken)
        {
            List<AliProduct> produtList = new List<AliProduct>();
            string prodcutsReqUrl = string.Format(ProudctListRequest, 1, csrfToken, groupId, groupLevel);
            string prodcutsJsonText = RemoteRequest(prodcutsReqUrl, null);
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
                prodcutsJsonText = RemoteRequest(prodcutsReqUrl, null);
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
            string groupJsonText = RemoteRequest(groupReqUrl, null);
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
            string html = RemoteRequest(SearchUrl, null);
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

            string html = RemoteRequest(SearchUrl, postString);
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
            string html = RemoteRequest(url, null);
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
                ImageInfoJson itemJson = GetAllGroupImages(i);
                imageInfoList.AddRange(itemJson.ImageInfos);
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
            string html = RemoteRequest(url, postString);
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
            string html = RemoteRequest(url, postString);
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


        public static List<CategroyNode> GetMyCategories()
        {
            List<CategroyNode> cateNodeList = new List<CategroyNode>();
            string url = "http://hz.productposting.alibaba.com/product/commonPostCategoryIframe.htm?iframe_delete=true&set_domain=true&catLang=en_us&_time="
                + DateUtils.DateTimeToInt(DateTime.Now);
            string html = RemoteRequest(url, null);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            HtmlNode tableNode = document.GetElementbyId("categoryCommonTable");
            HtmlNodeCollection nodes = tableNode.SelectNodes("//a");
            foreach (HtmlNode node in nodes)
            {
                CategroyNode categroyNode = new CategroyNode();
                string idstring = node.GetAttributeValue("id", "");
                categroyNode.Id = Convert.ToInt32(idstring.Replace("item-", ""));
                categroyNode.warnMessage = node.GetAttributeValue("warnMessage", "");
                categroyNode.hasPrivilege = node.GetAttributeValue("hasPrivilege", "");
                categroyNode.Name = HttpUtility.HtmlDecode(node.InnerHtml);
                cateNodeList.Add(categroyNode);
            }
            document = null;
            return cateNodeList;
        }

        public static List<Soomes.Attribute> GetCountries()
        {
            string url = "http://hz.productposting.alibaba.com/product/post_cat_country_attr_ajax.htm";
            string html = RemoteRequest(url, null);
            Regex r = new Regex("=(.*?);");
            string jsonstring = string.Empty;
            List<Soomes.Attribute> countries = new List<Soomes.Attribute>();
            GroupCollection gc = r.Match(html).Groups;
            if (gc != null && gc.Count > 1)
            {
                jsonstring = gc[1].Value.Trim();
            }
            if (string.IsNullOrEmpty(jsonstring))
            {
                return countries;
            }
            List<AttributeNode> attributeNodes = JsonConvert.FromJson<List<AttributeNode>>(jsonstring);
            foreach (AttributeNode node in attributeNodes)
            {
                countries.Add(node.Data);
            }
            return countries;
        }
        

        public static string GetCheckCodeUrl(string html)
        {
            string checkCodeUrl = string.Empty;
            HtmlDocument document = new HtmlDocument();
            try
            {
                document.LoadHtml(html);
                HtmlNode imagePasswordImg = document.GetElementbyId("imagePasswordImg");
                checkCodeUrl = imagePasswordImg.GetAttributeValue("src", null);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.Message);
            }
            finally 
            {
                document = null;
            }
            return checkCodeUrl;
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

