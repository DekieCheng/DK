using System;
using System.Runtime.InteropServices;

public partial class Impersonator
{
    [DllImport("advapi32.dll", SetLastError = true)]
    extern static int LogonUser(string lpszUserName, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    extern static int DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);


    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    extern static bool RevertToSelf();

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    extern static bool CloseHandle(IntPtr handle);
}
