Imports FlexFlow
Imports FFSystem

Module Variables

    'Public g_ErrorForm As ErrorMessage_Form.frmErrorMessage
    Public g_AppServerName As String = Environment.MachineName
    Enum StatusFlag
        Status_Pending = 0
        Status_Success = 1
        Status_Error = 2
    End Enum
    Enum MsgCategoryFlag
        MsgError = 0
        MsgReminding = 1
        MsgNormal = 2
        MsgIncoming = 3
        MsgOutput = 4
    End Enum

    Public Const STEP_FFPREPARETRAY As String = "FFPREPARETRAY"
    Public Const STEP_FFPREPAREUNIT As String = "FFPREPAREUNIT"
    Public Const STEP_ASKFFSTATUS As String = "ASKFFSTATUS"
    Public Const STEP_FFREPORTSTATUS As String = "FFREPORTSTATUS"
    Public Const STEP_LASERISREADY As String = "LASERISREADY"
    Public Const STEP_FFSENDDATA As String = "FFSENDDATA"
    Public Const STEP_DATACHECK As String = "DATACHECK"
    Public Const STEP_DATACHECK_NAK As String = "DATACHECK_NAK"
    Public Const STEP_FFSTARTLASER As String = "FFSTARTLASER"
    Public Const STEP_LASEREND As String = "LASEREND"
    Public Const STEP_LASEREND_ERROR As String = "LASEREND_ERROR"
    Public Const STEP_FFENDLASER As String = "FFENDLASER"

    Public Const STEP_LASERERROR As String = "LASERERROR"

    Public Const STEP_UNKNOWN As String = "UNKNOWN"
    'Public g_NextStep As String = STEP_FFPREPARETRAY
    'Public g_NextOPStep As clsLaserOperation

    Public Const OBJ_OutgoingClient As String = "OutgoingClient"

End Module
