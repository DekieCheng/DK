using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;

public partial class Impersonator : IDisposable
{
    private const int LOGON32_LOGON_INTERACTIVE = 2;
    private const int LOGON32_PROVIDER_DEFAULT = 0;

    private WindowsImpersonationContext impersonationContext = null;

    public Impersonator(string userName, string domainName, string password)
    {
        ImpersonateValidUser(userName, domainName, password);
    }

    private void ImpersonateValidUser(string userName, string domain, string password)
    {
        WindowsIdentity tempWindowsIdentity = null;
        IntPtr tokenDuplicate = IntPtr.Zero;

        try
        {
            if (RevertToSelf())
            {
                ResetPersistedTokenWhenIncomingUserNameIsDifferentOfthePreviousAuthenticatedUserName(userName);

                if (ImpersonatorPersistance.Token != IntPtr.Zero || LogonUser(userName, domain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref ImpersonatorPersistance.Token) != 0)
                {
                    ImpersonatorPersistance.CurrentUserName = userName;
                    if (DuplicateToken(ImpersonatorPersistance.Token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                    }
                    else
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                else
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }
        finally
        {
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);
        }
    }

    private void ResetPersistedTokenWhenIncomingUserNameIsDifferentOfthePreviousAuthenticatedUserName(string userName)
    {
        if (ImpersonatorPersistance.CurrentUserName != userName)
            ImpersonatorPersistance.Token = IntPtr.Zero;
    }

    private void UndoImpersonation()
    {
        if (impersonationContext != null)
            impersonationContext.Undo();
    }

    public void IDisposable_Dispose()
    {
        UndoImpersonation();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
