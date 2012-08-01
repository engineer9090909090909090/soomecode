using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SooMailer
{
    class MailModel
    {
        public int Id { set; get; }
        public string Email { set; get; }
        public string Company { set; get; }
        public string Country { set; get; }
        public string Username { set; get; }
        public string Subject { set; get; }
        public string ProductType { set; get; }
        public string Source { set; get; }
        public int Verify1 { set; get; }
        public int Verify2 { set; get; }
        public string SendDate { set; get; }

        public string GetVerifyString()
        {
            if (Verify1 == 1)
            {
                return "验证通过"; 
            }
            else if (Verify1 == 2)
            {
                return "验证不存在";
            }
            return "未验证";
        }
    }
}
