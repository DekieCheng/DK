Module modVariables
    'Author         : Tai T. Nguyen
    'CreationDate   : 4/1/2004 4:05:01 PM
    'Description    : check to see what the user enter is valid 
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

    'Author         : Tai T. Nguyen
    'CreationDate   : 3/31/2004 11:18:56 AM
    'Description    : Change all controls in a form to predefined color
    Public Sub SetBackColor(ByVal oControl As Control)
        Dim oCtrol As Control

        For Each oCtrol In oControl.Controls
            Try
                If oCtrol.BackColor.ToString <> "Color [Window]" Then
                    oCtrol.BackColor = System.Drawing.Color.FromArgb(CType(236, Byte), CType(233, Byte), CType(216, Byte))
                    'recursively go through all controls
                    SetBackColor(oCtrol)
                End If
                'change font
                Dim FontWidth As Single = oCtrol.Font.SizeInPoints
                Dim FontStyle As Integer = oCtrol.Font.Style

                oCtrol.Font = New System.Drawing.Font("Verdana", FontWidth, FontStyle, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Catch ex As Exception
            End Try
        Next
    End Sub

    'Author         : Tai T. Nguyen
    'CreationDate   : 3/31/2004 12:17:49 PM
    'Description    : set focus and select all text if applicable
    Public Sub ShiftFocus(ByVal oControl As Control)
        If TypeOf oControl Is TextBox Then
            CType(oControl, TextBox).SelectAll()
            'CType(oControl, TextBox).Text = ""
        End If
        oControl.Focus()
    End Sub

    'Author         : Kate Chong Mei Ying
    'CreationDate   : 4/7/2004 4:36:16 PM
    'Description    : Auto Select text when user tyep something in the combo box
    'ModificationHistory    : Tai T. Nguyen: modified cbo as ComboBox to Object so we can use AddHandler event 
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

    'Author         : Kate Chong Mei Ying
    'CreationDate   : 4/7/2004 4:36:21 PM
    'Description    : Auto Select text when user tyep something in the combo box
    'ModificationHistory    : Tai T. Nguyen: modified cbo as ComboBox to cbo as Object and add second parameter so we can use AddHandler event
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
    Public Function StripDBNull(ByVal Value As Object) As String
        If Value Is DBNull.Value Then
            Return String.Empty
        Else
            Return Value
        End If
    End Function
    Public Function IfIsNothing(ByVal blVal As String) As Boolean
        If blVal = "" Then blVal = "FALSE"
        Return CBool(blVal)
    End Function
    Public Function IfIsNothing2(ByVal blVal As String) As String
        If blVal = "" Then blVal = "0"
        Return blVal
    End Function
    Public Function IfIsNothing3(ByVal blVal As Object) As String
        If blVal = "" Then blVal = String.Empty
        Return blVal
    End Function
    Public Function IfIsNothing4(ByVal blVal As String) As Integer
        If blVal = "" Then blVal = Integer.MinValue.ToString
        Return Integer.Parse(blVal)
    End Function

End Module
