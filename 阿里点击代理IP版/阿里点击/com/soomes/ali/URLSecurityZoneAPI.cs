﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;

namespace com.soomes.ali
{

    /// <summary> 
    /// Enables or disables a specified Internet Explorer feature control 
    /// Minimum availability: Internet Explorer 6.0 
    /// Minimum operating systems: Windows XP SP2 
    /// </summary> 
    class URLSecurityZoneAPI
    {

        /// <summary> 
        /// Specifies where to set the feature control value 
        /// </summary> 
        public enum SetFeatureOn : int
        {
            THREAD = 0x00000001,
            PROCESS = 0x00000002,
            REGISTRY = 0x00000004,
            THREAD_LOCALMACHINE = 0x00000008,
            THREAD_INTRANET = 0x00000010,
            THREAD_TRUSTED = 0x00000020,
            THREAD_INTERNET = 0x00000040,
            THREAD_RESTRICTED = 0x00000080
        }

        /// <summary> 
        /// InternetFeaturelist 
        /// </summary> 
        public enum InternetFeaturelist : int
        {
            OBJECT_CACHING = 0,
            ZONE_ELEVATION = 1,
            MIME_HANDLING = 2,
            MIME_SNIFFING = 3,
            WINDOW_RESTRICTIONS = 4,
            WEBOC_POPUPMANAGEMENT = 5,
            BEHAVIORS = 6,
            DISABLE_MK_PROTOCOL = 7,
            LOCALMACHINE_LOCKDOWN = 8,
            SECURITYBAND = 9,
            RESTRICT_ACTIVEXINSTALL = 10,
            VALIDATE_NAVIGATE_URL = 11,
            RESTRICT_FILEDOWNLOAD = 12,
            ADDON_MANAGEMENT = 13,
            PROTOCOL_LOCKDOWN = 14,
            HTTP_USERNAME_PASSWORD_DISABLE = 15,
            SAFE_BINDTOOBJECT = 16,
            UNC_SAVEDFILECHECK = 17,
            GET_URL_DOM_FILEPATH_UNENCODED = 18,
            TABBED_BROWSING = 19,
            SSLUX = 20,
            DISABLE_NAVIGATION_SOUNDS = 21,
            DISABLE_LEGACY_COMPRESSION = 22,
            FORCE_ADDR_AND_STATUS = 23,
            XMLHTTP = 24,
            DISABLE_TELNET_PROTOCOL = 25,
            FEEDS = 26,
            BLOCK_INPUT_PROMPTS = 27,
            MAX = 28
        }

        /// <summary> 
        /// Enables or disables a specified feature control.  
        /// </summary>             
        [DllImport("urlmon.dll", ExactSpelling = true), PreserveSig, SecurityCritical, SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Error)]
        static extern int CoInternetSetFeatureEnabled(int featureEntry, [MarshalAs(UnmanagedType.U4)] int dwFlags, bool fEnable);

        /// <summary> 
        /// Determines whether the specified feature control is enabled.  
        /// </summary> 
        [DllImport("urlmon.dll", ExactSpelling = true), PreserveSig, SecurityCritical, SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Error)]
        static extern int CoInternetIsFeatureEnabled(int featureEntry, int dwFlags);

        /// <summary> 
        /// Set the internet feature enabled/disabled 
        /// </summary> 
        /// <param name="feature">The feature from <c>InternetFeaturelist</c></param> 
        /// <param name="target">The target from <c>SetFeatureOn</c></param> 
        /// <param name="enabled">enabled the feature?</param> 
        /// <returns><c>true</c> if [is internet set feature enabled] [the specified feature]; otherwise, <c>false</c>.</returns> 
        public static bool InternetSetFeatureEnabled(InternetFeaturelist feature, SetFeatureOn target, bool enabled)
        {
            return (CoInternetSetFeatureEnabled((int)feature, (int)target, enabled) == 0);
        }

        /// <summary> 
        /// Determines whether the internet feature is enabled. 
        /// </summary> 
        /// <param name="feature">The feature from <c>InternetFeaturelist</c></param> 
        /// <param name="target">The target from <c>SetFeatureOn</c></param> 
        /// <returns><c>true</c> if the internet feature is enabled; otherwise, <c>false</c>. 
        /// </returns> 
        public static bool IsInternetSetFeatureEnabled(InternetFeaturelist feature, SetFeatureOn target)
        {
            return (CoInternetIsFeatureEnabled((int)feature, (int)target) == 0);
        }

    } 

}
