Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System.IO.File
Imports System.Security.AccessControl
Imports System.Runtime.InteropServices

Module modMain

#Region " User 32 API"

    <DllImport("user32.dll")>
    Public Function SetWindowPos(ByVal hWnd As Integer, ByVal hWndlnsertAfter As Integer, ByVal X__1 As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer,
     ByVal x__2 As Integer) As Boolean
    End Function

#End Region

    Public Function StripDBNull(ByVal Value As Object) As String
        If Value Is DBNull.Value Then
            Return String.Empty
        Else
            Return Value
        End If
    End Function

    Public Function IfIsNothingBool(ByVal blVal As String) As Boolean
        If blVal = "" Then blVal = "FALSE"
        Return Convert.ToBoolean(blVal)
    End Function

    Public Function IfIsNothingZero(ByVal blVal As String) As String
        If blVal = "" Then blVal = "0"
        Return blVal
    End Function

    Public Function IfIsNothingInt(ByVal blVal As String) As Integer
        If blVal = "" Then blVal = 1
        Return Integer.Parse(blVal)
    End Function

    Public Function IfIsNothingDouble(ByVal blVal As String) As Double
        If String.IsNullOrEmpty(blVal) Then blVal = "0.00"
        Return Double.Parse(blVal)
    End Function

    Public Function IfIsNothingStr(ByVal blVal As Object) As String
        If blVal = "" Then blVal = String.Empty
        Return blVal
    End Function

    Public Function SearchComboBox(ByVal oCombo As ComboBox, ByVal ValueToSearch As String) As Boolean
        Try
            For Count As Integer = 0 To oCombo.Items.Count - 1
                If oCombo.Items.Item(Count).ToString.ToUpper = ValueToSearch.ToUpper Then
                    Return True
                End If
            Next
        Catch ex As Exception
            Return False
        End Try

        Return False
    End Function

    Public Sub ShiftFocus(ByVal oControl As Control)
        If TypeOf oControl Is TextBox Then
            CType(oControl, TextBox).SelectAll()
        End If
        oControl.Focus()
    End Sub

    Public Sub AutoCompleteCombo_KeyUp(ByVal cbo As Object, ByVal e As KeyEventArgs)
        Try
            cbo = CType(cbo, ComboBox)

            Dim sTypedText As String
            Dim iFoundIndex As Integer
            Dim oFoundItem As Object
            Dim sFoundText As String
            Dim sAppendText As String
            'Allow select keys without Autocompleting
            Select Case e.KeyCode
                Case Keys.Back, Keys.Left, Keys.Right, Keys.Up, Keys.Delete, Keys.Down
                    Return
            End Select
            'Get the Typed Text and Find it in the list
            sTypedText = cbo.Text
            iFoundIndex = cbo.FindString(sTypedText)
            'If we found the Typed Text in the list then Autocomplete
            If iFoundIndex >= 0 Then
                'Get the Item from the list (Return Type depends if Datasource was bound 
                ' or List Created)
                oFoundItem = cbo.Items(iFoundIndex)
                'Use the ListControl.GetItemText to resolve the Name in case the Combo 
                ' was Data bound
                sFoundText = cbo.GetItemText(oFoundItem)
                'Append then found text to the typed text to preserve case
                sAppendText = sFoundText.Substring(sTypedText.Length)
                cbo.Text = sTypedText & sAppendText
                'Select the Appended Text
                cbo.SelectionStart = sTypedText.Length
                cbo.SelectionLength = sAppendText.Length
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub AutoCompleteCombo_Leave(ByVal cbo As Object, ByVal e As System.EventArgs)
        Try
            cbo = CType(cbo, ComboBox)

            Dim iFoundIndex As Integer
            iFoundIndex = cbo.FindStringExact(cbo.Text)
            cbo.SelectedIndex = iFoundIndex
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Module

