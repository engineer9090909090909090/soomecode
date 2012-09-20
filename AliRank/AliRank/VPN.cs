using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using DotRas;
using System.Net;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace AliRank
{
    public class VPN : IDisposable
    {
        private static string WinDir = Environment.GetFolderPath(Environment.SpecialFolder.System);
        private static string fileName = @"rasdial.exe";
        private static string VPNPROCESS = WinDir + fileName;

        ManualResetEvent eventX;
        private string connName;
        private VpnModel model;
        private RasDialer dialer;
        private RasHandle handle;
        private bool connectSatuts;

        public VPN(string connName, VpnModel model)
        {
            this.connName = connName;
            this.model = model;
        }

        public void Disconnect()
        {
            if (dialer.IsBusy)
            {
                dialer.DialAsyncCancel();
            }
            else
            {
                RasConnection connection = RasConnection.GetActiveConnectionByHandle(handle);
                if (connection != null)
                {
                    connection.HangUp();
                }
            }
        }
        public bool Connect()
        {
            dialer = new RasDialer();
            string AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                + Path.DirectorySeparatorChar + Application.ProductName + Path.DirectorySeparatorChar + "PhoneBook.pbk";
            using (RasPhoneBook phoneBook = new RasPhoneBook())
            {
                phoneBook.Open();
                RasEntry entry = null;
                if (phoneBook.Entries.Contains(connName))
                {
                    phoneBook.Entries.Remove(connName);
                }
                if (model.VpnType.ToUpper().Equals("L2TP"))
                {
                    entry = RasEntry.CreateVpnEntry(connName, model.Address, RasVpnStrategy.L2tpOnly, RasDevice.GetDeviceByName("(L2TP)", RasDeviceType.Vpn));
                }
                else
                {
                    entry = RasEntry.CreateVpnEntry(connName, model.Address, RasVpnStrategy.PptpOnly, RasDevice.GetDeviceByName("(PPTP)", RasDeviceType.Vpn));
                }
                phoneBook.Entries.Add(entry);
                entry.Options.PreviewDomain = false;
                entry.Options.ShowDialingProgress = false;
                entry.Options.PromoteAlternates = false;
                entry.Options.DoNotNegotiateMultilink = false;
                entry.DnsAddress = IPAddress.Parse("121.0.21.2");
                entry.DnsAddressAlt = IPAddress.Parse("110.75.217.1");
                if (model.VpnType.ToUpper().Equals("L2TP") && !string.IsNullOrEmpty(model.L2tpSec))
                {
                    entry.Options.UsePreSharedKey = true;
                    entry.UpdateCredentials(RasPreSharedKey.Client, model.L2tpSec);
                    entry.Update();
                }
                dialer.DialCompleted += new EventHandler<DialCompletedEventArgs>(Dialer_DialCompleted);
                dialer.EapOptions = new DotRas.RasEapOptions(false, false, false);
                dialer.HangUpPollingInterval = 0;
                dialer.Options = new DotRas.RasDialOptions(false, false, false, false, false, false, false, false, false, false);
                dialer.EntryName = connName;
                dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
                dialer.AllowUseStoredCredentials = true;
                dialer.AutoUpdateCredentials = RasUpdateCredential.AllUsers;
                dialer.Credentials = new NetworkCredential(model.Username, model.Password);
                eventX = new ManualResetEvent(false);
            }
            handle = dialer.DialAsync();
            eventX.WaitOne(Timeout.Infinite, true);
            return connectSatuts;
        }

        private void Dialer_DialCompleted(object sender, DialCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                connectSatuts = false;
            }
            else if (e.TimedOut)
            {
                connectSatuts = false;
            }
            else if (e.Error != null)
            {
                connectSatuts = false;
            }
            else if (e.Connected)
            {
                connectSatuts = true;
            }
            eventX.Set();
        }

        public static bool TestConnection(string IP)
        {
            bool RV = false;
            try
            {
                Ping ping = new Ping();
                if (ping.Send(IP).Status == IPStatus.Success)
                {
                    RV = true;
                }
                else
                {
                    RV = false;
                }
                ping = null;
            }
            catch (Exception Ex)
            {
                Debug.Assert(false, Ex.ToString());
                RV = false;
            }
            return RV;
        }

        public void Dispose()
        {
            dialer.DialCompleted -= new EventHandler<DialCompletedEventArgs>(Dialer_DialCompleted);
            connName = null;
            model = null;
            dialer = null;
            handle = null;
        }
    }


}
