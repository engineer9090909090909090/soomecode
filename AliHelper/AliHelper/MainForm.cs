using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace AliHelper
{
    public partial class MainForm : Form
    {
        //alibaba vip manage url
        public static string url = "http://hz.productposting.alibaba.com/product/manage_products.htm?tracelog=newschp_nav_mamng#tab=approved";
        
        
        //http://hz.productposting.alibaba.com/product/managementproducts/asyQueryProductList.do?status=approved&imageType=all&repositoryType=all&page=1&size=50&changePageSize=Y&_csrf_token_=18midyd775yoj
        public static string proudctListRequest = "http://hz.productposting.alibaba.com/product/managementproducts/asyQueryProductList.do?status=approved&imageType=all&repositoryType=all&page={0}&size=50&changePageSize=Y&_csrf_token_={1}";
        
        //string cookieString = "ali_apache_id=58.253.216.141.58471722733685.5; ali_apache_sid=58.253.216.141.58471722733685.5|1347724533; ali_apache_track=mt=3|ms=|mid=soomes; ali_apache_tracktmp=W_signed=Y; acs_usuc_t=acs_rt=db3a8e7c37dc417480129fccf4de3cab; xman_t=1jQzWzMCoFPgXF1KyNYdfuP0Fp+x4u8q7NYq24h6zlpVKL1AvhNnolKwj2Go0UuVOo/MRyPvrjb3tG5KXqC1SvgTi/6AzWeqY+M8MIsJSTxt1MotiNZkcG+fEwHrPcF6MnojfNcwHGWgW9yZiZ3nVZzB/qYOZn4eL2Qp1DlMvToe8ZLylRI6adAX1Buk7YCMbA7FTzaxzMKOTnRU0D+uwLXHLg3GfOTwzXr606B9hbqpErsyd0/oeOJj7Vns5PCBdxa1QhU6rVL0hErS0js5rNcoerndsLt/ysheO7ZltL7O91sbpr5ZUmv9DMMW4JEqs7uo6ZgmP7sNlG73lN4D6F2pDNtqbKWKvh0XmE0pdncJ+bd+a4wPgAz9KO+5QczpnL5wdw5kb8YWLOcrd+blftSlSALbWYwDNrjZ6/Rr24rKs9BibP/Ri9gaGGZiwOL4R4vI1BOf7PtMImUis6Pc9GxSVgWXpi7ZPDlDWQFIWYRHO0AuJWL7LadEmGykduVsuVO2xO/s/Zm/Ica9gR3tgrjz0/aYZamVC3Uh4EVjf63OsDH3x/Ac9Hd/yXZ4m8xUDThJqfiVb6vBklteoGg+JLSiGXbadx6do5LxzH+24vQejF0hCXRbYLRk0ZEHK8KCn8e69P+cvUGyWzoJ8ZzR6jzrwI9aa2YdRwsXqiJwzCI=; xman_f=i1GfVyJbbAEvK+cPMEdKZ5T5iNXeS37RxsMiHjl4x+viOzzjlLyg+quIgH5aSyPj1ZMzsQP6zP9qBHrggBAu6BuosUIYLGIAszVQm2TR3HdOBrR5bxL5WLk8QJEWTUGMUPwl2LCngtKHbpIAKKhdYE+Sh5O4JBB0/L35c3MfWa5XQ4uXKqixXU8STPwUKHUjLXtD0BczvK6ep75RkDN1PgmA56RCZgjN6SC3QYkNZiaRM+ll3/OTT0IfKOkMFA8590TSTaKrM9KHSWEhtmPnQFvczgUt4RS9cYPrAnhHFaZBCrZalPNIzg==; ali_beacon_id=58.253.216.141.1347722734366.6; xman_us_f=x_locale=zh_CN&no_popup_today=n&x_user=CN|Maria|Yang|cgs|200349974&last_popup_time=1347722744494; xman_us_t=x_lid=soomes&sign=y&x_user=XU1i2QY2ejPGc42vZGUhqchWGEg55ZgIkPj1Ky9jZ7w=&need_popup=y; intl_locale=zh_CN; intl_common_forever=Y9GEDJVRgOasHaVJmza1fkI4GOOcHhJA4879HXa6n11QRlqPGFSFWw==; acs_t=nbzT2IlQmZunRrVr5sJNecqHjsSiz4pcbolzPnfyh9YKcvR6WOMxRKqw2NXmpquJ; xman_mt2=true; xman_mt1=true; JSESSIONID=A7D7C043A4BBC34AC9565AFC0E2247E7; login_auto_atm=n";
        public MainForm()
        {
            InitializeComponent();
            //ShareCookie.Instance.LoginCookie = cookieString;
            string html = IEHandleUtils.WebRequestGetUrlHtml(url);
            string csrfToken = GetCsrfToken(html);
            if (!string.IsNullOrEmpty(csrfToken))
            {
                string reqUrl = string.Format(proudctListRequest, "1", csrfToken);
                string Json = IEHandleUtils.WebRequestGetUrlHtml(reqUrl);
            }
            IEHandleUtils.WebBrowerSetCookies_NavigateToUrl(this.webBrowser1, url);
        }


        public string GetCsrfToken(string html)
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
