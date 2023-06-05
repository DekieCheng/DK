using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;

public sealed class ImpersonatorPersistance
{
    internal static IntPtr Token = IntPtr.Zero;
    internal static string CurrentUserName;
}