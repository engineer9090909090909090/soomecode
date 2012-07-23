using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using DotRas;
using System.Net;
using log4net;

namespace com.soomes.ali
{

    public class VPN
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(VPN));
        private static string WinDir = Environment.GetFolderPath(Environment.SpecialFolder.System);
        private static string fileName = @"\rasdial.exe";
        private static string VPNPROCESS = WinDir + fileName;

        public VPN(){}

        public static bool TestConnection(string serverIP)
        {
            bool RV = false;
            try
            {
                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();

                if (ping.Send(serverIP).Status == System.Net.NetworkInformation.IPStatus.Success)
                {
                    RV = true;
                }
                else
                {
                    RV = false;
                }
                ping = null;
            }
            catch (Exception e)
            {
                Debug.Assert(false, e.ToString());
                Log.Error(serverIP + "TestConnection fail.\n" + e.Message);
                RV = false;
            }
            return RV;
        }

        public static bool ConnectToVPN(string connName, string userName, string password)
        {

            bool RV = false;
            try
            {
                string args = string.Format(@"{0} {1} {2}", connName, userName, password);
                ProcessStartInfo myProcess = new ProcessStartInfo(VPNPROCESS, args);
                myProcess.CreateNoWindow = true;
                myProcess.UseShellExecute = false;
                Process.Start(myProcess);
                RV = true;

            }
            catch (Exception e)
            {
                Debug.Assert(false, e.ToString());
                Log.Error(connName + "disConnection fail.\n" + e.Message);
                RV = false;
            }
            return RV;
        }

        public static bool DisconnectFromVPN(string connName)
        {
            bool RV = false;
            try
            {
                string args = string.Format(@"{0} /d", connName);
                ProcessStartInfo myProcess = new ProcessStartInfo(VPNPROCESS, args);
                myProcess.CreateNoWindow = true;
                myProcess.UseShellExecute = false;
                System.Diagnostics.Process.Start(myProcess);
                RV = true;
            }
            catch (Exception e)
            {
                Debug.Assert(false, e.ToString());
                Log.Error(connName + "disConnection fail.\n" + e.Message );
                RV = false;
            }
            return RV;
        }

        public static void CreateVPN(string connName, string serverIP, string userName, string password)
        {
            if (connName.ToUpper().EndsWith("US"))
            {
                CreateVPN_US(connName, serverIP, userName, password);
                return;
            }
            DotRas.RasDialer dialer = new DotRas.RasDialer();
            DotRas.RasPhoneBook allUsersPhoneBook = new DotRas.RasPhoneBook();
            allUsersPhoneBook.Open();

            RasEntry entry = null;
            if (!allUsersPhoneBook.Entries.Contains(connName))
            {
                entry = RasEntry.CreateVpnEntry(connName, serverIP, RasVpnStrategy.PptpOnly, RasDevice.GetDeviceByName("(PPTP)", RasDeviceType.Vpn));
                entry.Options = RasEntryOptions.Custom;
                entry.EncryptionType = RasEncryptionType.Optional;
                entry.Options = entry.Options |= RasEntryOptions.RequirePap;
                entry.Options = entry.Options |= RasEntryOptions.RequireChap;
                entry.Options = entry.Options |= RasEntryOptions.RequireMSChap;
                entry.Options = entry.Options |= RasEntryOptions.RequireMSChap2;
                allUsersPhoneBook.Entries.Add(entry);
            }
            else 
            {
                entry = allUsersPhoneBook.Entries[connName];
                entry.PhoneNumber = serverIP;
                IPAddress _ip;
                IPAddress.TryParse(serverIP, out _ip);
                entry.IPAddress = _ip;
            }
            dialer.EntryName = connName;
            dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);

            try
            {
                NetworkCredential nc = new NetworkCredential(userName, password);
                entry.UpdateCredentials(nc);
                entry.Update();
                dialer.DialAsync();
            }
            catch (Exception e)
            {
                Log.Error(serverIP + "Create VPN Connection fail.\n +" + e.Message);
                return;
            }
        }


        public static void CreateVPN_US(string connName, string serverIP, string userName, string password)
        {
            DotRas.RasDialer dialer = new DotRas.RasDialer();
            DotRas.RasPhoneBook allUsersPhoneBook = new DotRas.RasPhoneBook();
            allUsersPhoneBook.Open();
            RasEntry entry = null;
            if (!allUsersPhoneBook.Entries.Contains(connName))
            {
                entry = RasEntry.CreateVpnEntry(connName, serverIP, RasVpnStrategy.PptpOnly, RasDevice.GetDeviceByName("(PPTP)", RasDeviceType.Vpn));
                entry.Options = RasEntryOptions.Custom;
                entry.EncryptionType = RasEncryptionType.Optional;
                entry.Options = entry.Options |= RasEntryOptions.RequireMSChap;
                entry.Options = entry.Options |= RasEntryOptions.RequireWin95MSChap;
                entry.Options = entry.Options |= RasEntryOptions.RequireMSChap2;
                allUsersPhoneBook.Entries.Add(entry);
            }
            else
            {
                entry = allUsersPhoneBook.Entries[connName];
                entry.PhoneNumber = serverIP;
                IPAddress _ip;
                IPAddress.TryParse(serverIP, out _ip);
                entry.IPAddress = _ip;
            }

            dialer.EntryName = connName;
            dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);

            try
            {
                NetworkCredential nc = new NetworkCredential(userName, password);
                entry.UpdateCredentials(nc);
                entry.Update();
                dialer.DialAsync();
            }
            catch (Exception e)
            {
                Log.Error(serverIP + "Create VPN Connection fail.\n +" + e.Message);
                return;
            }
        }

    }
}