using System;

static class Variables
{

    // Public g_ErrorForm As ErrorMessage_Form.frmErrorMessage
    public static string g_AppServerName = Environment.MachineName;
    public enum StatusFlag
    {
        Status_Pending = 0,
        Status_Success = 1,
        Status_Error = 2
    }
    public enum MsgCategoryFlag
    {
        MsgError = 0,
        MsgReminding = 1,
        MsgNormal = 2,
        MsgIncoming = 3,
        MsgOutput = 4
    }

}
