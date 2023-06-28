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

End Module
