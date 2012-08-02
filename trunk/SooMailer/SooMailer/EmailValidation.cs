using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Sockets;

namespace SooMailer
{
    class EmailValidation
    {
        TcpClient tcpc;
        NetworkStream s;
        byte[] bb;
        int len;
        string read;
        string stringTosend;
        byte[] arrayToSend;
        bool flag;

        public string getMailServer(string strEmail, bool IsCheck)
        {
            string strDomain = strEmail.Split('@')[1];
            if (IsCheck == true)
            {
                //this.textBoxShow.Text += "分离出邮箱域名：" + strDomain + "\r\n";
            }
            ProcessStartInfo info = new ProcessStartInfo();   //指定启动进程时使用的一组值。  
            info.UseShellExecute = false;
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.FileName = "nslookup";
            info.CreateNoWindow = true;
            info.Arguments = "-type=mx " + strDomain;
            Process ns = Process.Start(info);        //提供对本地和远程进程的访问并使您能够启动和停止本地系统进程。  
            StreamReader sout = ns.StandardOutput;

            Regex reg = new Regex(@"mail exchanger = (?<mailServer>[^\s]+)");
            string strResponse = "";
            while ((strResponse = sout.ReadLine()) != null)
            {

                Match amatch = reg.Match(strResponse);   // Match  表示单个正则表达式匹配的结果。  

                if (reg.Match(strResponse).Success)
                {
                    return amatch.Groups["mailServer"].Value;   //获取由正则表达式匹配的组的集合  

                }
            }
            return null;
        }

        private void Connect(string mailServer)
        {
            try
            {
                tcpc.Connect(mailServer, 25);
                s = tcpc.GetStream();
                len = s.Read(bb, 0, bb.Length);
                read = Encoding.UTF8.GetString(bb);
                if (read.StartsWith("220") == true)
                {
                    //this.textBoxShow.Text += "连接服务器成功！" + "\r\n";
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.Message);
                throw e;
            }
        }

        private bool SendCommand(string command)
        {
            try
            {
                arrayToSend = Encoding.UTF8.GetBytes(command.ToCharArray());
                s.Write(arrayToSend, 0, arrayToSend.Length);
                len = s.Read(bb, 0, bb.Length);
                read = Encoding.UTF8.GetString(bb);
            }
            catch (IOException)
            {
                return false;
            }

            if (read.StartsWith("250"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckEmail(string mailAddress)
        {
            Regex reg = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (!reg.IsMatch(mailAddress))
            {
                return false;
            }
            string mailServer = getMailServer(mailAddress, true);
            if (mailServer == null)
            {
                return false;//邮件服务器探测错误  
            }

            tcpc = new TcpClient();      //为 TCP 网络服务提供客户端连接。  
            tcpc.NoDelay = true;
            tcpc.ReceiveTimeout = 3000;
            tcpc.SendTimeout = 3000;
            bb = new byte[512];

            try
            {
                Connect(mailServer);//创建连接
                stringTosend = "helo " + mailServer + "\r\n"; ////写入HELO命令  
                flag = SendCommand(stringTosend);
                if (flag == false)
                { 
                    return flag;
                }
                stringTosend = "mail from:<" + mailAddress + ">" + "\r\n"; 
                //写入Mail From命令  
                flag = SendCommand(stringTosend);
                if (flag == false)
                {
                    return flag;
                }
                stringTosend = "rcpt to:<" + mailAddress + ">" + "\r\n";
                //写入RCPT命令，这是关键的一步，后面的参数便是查询的Email的地址  
                flag = SendCommand(stringTosend);
                if (flag == true)
                {
                    return flag;//邮箱存在 
                }
                else
                {
                    return flag;//邮箱不存在
                }
            }
            catch (Exception)
            { 
                return false;//发生错误或邮件服务器不可达
            }
        }
    }
}
