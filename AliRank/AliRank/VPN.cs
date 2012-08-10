using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using DotRas;
using System.Net;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;

namespace AliRank
{
    public class VPN
    {
        private static string WinDir = Environment.GetFolderPath(Environment.SpecialFolder.System);
        private static string fileName = @"rasdial.exe";
        private static string VPNPROCESS = WinDir + fileName;

        public VPN() { }

        public static bool Connect(string connName, VpnModel model)
        {
            string command = string.Format(@"{0} {1} {2} {3}", fileName, connName, model.Username, model.Password);
            string returnVal = CmdUtils.Cmd(command);
            string successString = "Successfully connected to "+model.Address+".";
            if (returnVal.IndexOf(successString) > 0)
            {
                return true;
            }
            return false;
        }

        public static bool DisConnect(string connName)
        {
            string command = string.Format(@"{0} {1} /d",fileName, connName);
            string returnString = CmdUtils.Cmd(command);
            if (returnString.IndexOf("Command completed successfully.") > 0)
            {
                return true;
            } 
            return false;
        }

        public static void Create(string connName, VpnModel model)
        {
            DotRas.RasDialer dialer = new DotRas.RasDialer();
            DotRas.RasPhoneBook phoneBook = new DotRas.RasPhoneBook();
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
            entry.Options = RasEntryOptions.Custom;
            entry.EncryptionType = RasEncryptionType.Optional;
            entry.Options = entry.Options |= RasEntryOptions.RequirePap;
            entry.Options = entry.Options |= RasEntryOptions.RequireChap;
            entry.Options = entry.Options |= RasEntryOptions.RequireMSChap;
            entry.Options = entry.Options |= RasEntryOptions.RequireMSChap2;
            entry.ExtendedOptions =  RasEntryExtendedOptions.UsePreSharedKey;
            phoneBook.Entries.Add(entry);
            dialer.EntryName = connName;
            dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.User);
            try
            {
                entry.UpdateCredentials(new NetworkCredential(model.Username, model.Password));
                entry.Update();

                if (model.VpnType.ToUpper().Equals("L2TP") && !string.IsNullOrEmpty(model.L2tpSec))
                {
                    entry.ExtendedOptions = RasEntryExtendedOptions.UsePreSharedKey;
                    entry.UpdateCredentials(RasPreSharedKey.Client, model.L2tpSec);
                    entry.Update();
                }
                dialer.DialAsync();
            }
            catch (Exception)
            {
                return;
            }
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
    }


}
