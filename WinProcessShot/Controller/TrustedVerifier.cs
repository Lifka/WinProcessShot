using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WinProcessShot.Model;

namespace WinProcessShot.Controller
{
    internal static class TrustedVerifier
    {
        #region MEMBERS

        private static readonly string TRUSTED_MD5_RELATIVE_FILE_PATH = "Resources/TrustedMD5";
        private static HashSet<string> trustedMD5 = null;

        #endregion

        #region METHODS

        private static HashSet<string> GetTrustedMD5()
        {
            if (trustedMD5 == null)
            {
                try
                {
                    LoadTrustedMD5FromDisk();
                }
                catch
                {
                    trustedMD5 = new HashSet<string>();
                }
            }

            return trustedMD5;
        }

        private static void LoadTrustedMD5FromDisk()
        {
            string trustedMD5FilePath = Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), TRUSTED_MD5_RELATIVE_FILE_PATH);
            if (!File.Exists(trustedMD5FilePath))
            {
                trustedMD5 = new HashSet<string>();
            }

            trustedMD5 = new HashSet<string>(File.ReadLines(trustedMD5FilePath));
        }

        private static bool HasAWellKnownMD5(ProcessInfoObj processInfoObj)
        {
            bool result = false;

            if (processInfoObj != null
                && !string.IsNullOrEmpty(processInfoObj.MD5)
                && GetTrustedMD5().Contains(processInfoObj.MD5))
            {
                result = true;
            }

            return result;
        }

        public static bool IsTrusted(ProcessInfoObj processInfoObj)
        {
            if (HasAWellKnownMD5(processInfoObj))
            {
                return true;
            }

            if (HasAValidSignature(processInfoObj))
            {
                return true;
            }

            return false;
        }

        public static bool HasAValidSignature(ProcessInfoObj processInfoObj)
        {
            if (processInfoObj == null
                || string.IsNullOrEmpty(processInfoObj.ExecutablePath)
                || !File.Exists(processInfoObj.ExecutablePath))
            {
                return false;
            }

            return IsSigned(processInfoObj.ExecutablePath);
        }

        private static bool IsSigned(string filePath)
        {
            var file = new WINTRUST_FILE_INFO();
            file.cbStruct = Marshal.SizeOf(typeof(WINTRUST_FILE_INFO));
            file.pcwszFilePath = filePath;

            var data = new WINTRUST_DATA();
            data.cbStruct = Marshal.SizeOf(typeof(WINTRUST_DATA));
            data.dwUIChoice = WTD_UI_NONE;
            data.dwUnionChoice = WTD_CHOICE_FILE;
            data.fdwRevocationChecks = WTD_REVOKE_NONE;
            data.pFile = Marshal.AllocHGlobal(file.cbStruct);
            Marshal.StructureToPtr(file, data.pFile, false);

            int hr;
            try
            {
                hr = WinVerifyTrust(INVALID_HANDLE_VALUE, WINTRUST_ACTION_GENERIC_VERIFY_V2, ref data);
            }
            finally
            {
                Marshal.FreeHGlobal(data.pFile);
            }
            return hr == 0;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct WINTRUST_FILE_INFO
        {
            public int cbStruct;
            public string pcwszFilePath;
            public IntPtr hFile;
            public IntPtr pgKnownSubject;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        private struct WINTRUST_DATA
        {
            public int cbStruct;
            public IntPtr pPolicyCallbackData;
            public IntPtr pSIPClientData;
            public int dwUIChoice;
            public int fdwRevocationChecks;
            public int dwUnionChoice;
            public IntPtr pFile;
            public int dwStateAction;
            public IntPtr hWVTStateData;
            public IntPtr pwszURLReference;
            public int dwProvFlags;
            public int dwUIContext;
            public IntPtr pSignatureSettings;
        }

        private const int WTD_UI_NONE = 2;
        private const int WTD_REVOKE_NONE = 0;
        private const int WTD_CHOICE_FILE = 1;
        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        private static readonly Guid WINTRUST_ACTION_GENERIC_VERIFY_V2 = new Guid("{00AAC56B-CD44-11d0-8CC2-00C04FC295EE}");

        [DllImport("wintrust.dll")]
        private static extern int WinVerifyTrust(IntPtr hwnd, [MarshalAs(UnmanagedType.LPStruct)] Guid pgActionID, ref WINTRUST_DATA pWVTData);

        #endregion
    }
}
