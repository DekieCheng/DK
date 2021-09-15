Imports System.Runtime.InteropServices

Module modMain

#Region "Commnon Variables"

    Public Const FFAPP = "Flexflow_Client"
    Public Const SW_SHOWNOMAL = 1
    Public Const SW_SHOWMAXIMIZED = 3
    Public Const SW_MAXIMIZE = 3

    Public IsFormBeingDragged As Boolean = False
    Public MouseDownX As Integer
    Public MouseDownY As Integer

    Public Const mSnapOffset As Integer = 35
    Public Const WM_WINDOWPOSCHANGING As Integer = &H46

    Public Const MU_REFRESH = "Refresh"
    Public Const MU_CLOSE = "Close"
    Public Const MU_OPENNEWINSTANCE = "Open New Instance"
    Public Const MU_ENDSELECTEDTASK = "End Selected Task"
    Public Const MU_ENDALLTASK = "End All Task"

#End Region

#Region "Common API "

    Declare Function SetForegroundWindow Lib "USER32.DLL" Alias "SetForegroundWindow" (ByVal hWnd As IntPtr) As Boolean
    Declare Function SetWindowPos Lib "user32" Alias "SetWindowPos" (ByVal hWnd As Integer, ByVal hWndlnsertAfter As Integer, ByVal X__1 As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal x__2 As Integer) As Boolean
    Declare Function ShowWindow Lib "USER32.DLL" Alias "ShowWindow" (ByVal hwnd As Long, ByVal nCmdShow As Long) As Long

    Public Const HWND_TOP = 0
    Public Const WM_NCLBUTTONDOWN As Integer = 161
    Public Const HT_CAPTION As Integer = 2

    <DllImport("USER32")>
    Public Function SendMessage(hWnd As IntPtr, Msg As Integer, wParam As Integer, lParam As Integer) As Integer
    End Function

    <DllImport("USER32")>
    Public Function ReleaseCapture() As Boolean
    End Function

#End Region

End Module
