Imports System.Drawing.Drawing2D
Imports System.Windows
Imports System.Runtime.InteropServices

Public Class frmMain

#Region "Commnon Variables"

    Public mMenu As ContextMenu = Nothing

#End Region

#Region "Auto snap to board of screen"

    <StructLayout(LayoutKind.Sequential)>
    Public Structure WINDOWPOS
        Public hwnd As IntPtr
        Public hwndInsertAfter As IntPtr
        Public x As Integer
        Public y As Integer
        Public cx As Integer
        Public cy As Integer
        Public flags As Integer
    End Structure

    Protected Overrides Sub WndProc(ByRef m As Message)
        ' Listen for operating system messages
        Select Case m.Msg
            Case WM_WINDOWPOSCHANGING
                SnapToDesktopBorder(Me, m.LParam, 0)
        End Select

        MyBase.WndProc(m)
    End Sub

    Public Shared Sub SnapToDesktopBorder(ByVal clientForm _
           As Form, ByVal LParam As IntPtr, ByVal widthAdjustment As Integer)
        If clientForm Is Nothing Then
            ' Satisfies rule: Validate parameters
            Throw New ArgumentNullException("clientForm")
        End If

        ' Snap client to the top, left, bottom or right desktop border
        ' as the form is moved near that border.

        Try
            ' Marshal the LPARAM value which is a WINDOWPOS struct
            Dim NewPosition As New WINDOWPOS
            NewPosition = CType(Runtime.InteropServices.Marshal.PtrToStructure(
                LParam, GetType(WINDOWPOS)), WINDOWPOS)

            If NewPosition.y = 0 OrElse NewPosition.x = 0 Then
                Return ' Nothing to do!
            End If

            ' Adjust the client size for borders and caption bar
            Dim ClientRect As Rectangle =
                clientForm.RectangleToScreen(clientForm.ClientRectangle)
            ClientRect.Width +=
                SystemInformation.FrameBorderSize.Width - widthAdjustment
            ClientRect.Height += (SystemInformation.FrameBorderSize.Height +
                                  SystemInformation.CaptionHeight)

            ' Now get the screen working area (without taskbar)
            Dim WorkingRect As Rectangle =
                Screen.GetWorkingArea(clientForm.ClientRectangle)

            ' Left border
            If NewPosition.x >= WorkingRect.X - mSnapOffset AndAlso
                NewPosition.x <= WorkingRect.X + mSnapOffset Then
                NewPosition.x = WorkingRect.X
            End If

            ' Get screen bounds and taskbar height
            ' (when taskbar is horizontal)
            Dim ScreenRect As Rectangle =
                Screen.GetBounds(Screen.PrimaryScreen.Bounds)
            Dim TaskbarHeight As Integer =
                ScreenRect.Height - WorkingRect.Height

            ' Top border (check if taskbar is on top
            ' or bottom via WorkingRect.Y)
            If NewPosition.y >= -mSnapOffset AndAlso
                 (WorkingRect.Y > 0 AndAlso NewPosition.y <=
                 (TaskbarHeight + mSnapOffset)) OrElse
                 (WorkingRect.Y <= 0 AndAlso NewPosition.y <=
                 (mSnapOffset)) Then
                If TaskbarHeight > 0 Then
                    NewPosition.y = WorkingRect.Y ' Horizontal Taskbar
                Else
                    NewPosition.y = 0 ' Vertical Taskbar
                End If
            End If

            ' Right border
            If NewPosition.x + ClientRect.Width <=
                 WorkingRect.Right + mSnapOffset AndAlso
                 NewPosition.x + ClientRect.Width >=
                 WorkingRect.Right - mSnapOffset Then
                NewPosition.x = WorkingRect.Right - (ClientRect.Width +
                                SystemInformation.FrameBorderSize.Width)
            End If

            ' Bottom border
            If NewPosition.y + ClientRect.Height <=
                   WorkingRect.Bottom + mSnapOffset AndAlso
                   NewPosition.y + ClientRect.Height >=
                   WorkingRect.Bottom - mSnapOffset Then
                NewPosition.y = WorkingRect.Bottom - (ClientRect.Height +
                                SystemInformation.FrameBorderSize.Height)
            End If

            ' Marshal it back
            Runtime.InteropServices.Marshal.StructureToPtr(NewPosition,
                                                           LParam, True)
        Catch ex As ArgumentException
        End Try
    End Sub

#End Region

#Region "Generate List View and populate data"

    Private Function ToGenerateColumnHeader(ByVal hdText As String, ByVal hdWidth As Integer) As ColumnHeader
        Dim colHeader As System.Windows.Forms.ColumnHeader = New ColumnHeader With {.Text = hdText, .Width = hdWidth}
        Return colHeader
    End Function

    Private Sub InitListViewLayOut()
        Me.lvFFClientList.BackColor = Color.SteelBlue
        Me.lvFFClientList.ForeColor = Color.White
        Me.lvFFClientList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {
                                           ToGenerateColumnHeader("No.", 100),
                                           ToGenerateColumnHeader("FFClient", 100),
                                           ToGenerateColumnHeader("Window Title", 100)
                                           })
        Me.lvFFClientList.Font = New System.Drawing.Font("Verdana", 10.75!, FontStyle.Regular)
        Me.lvFFClientList.FullRowSelect = True
        Me.lvFFClientList.HideSelection = False
        Me.lvFFClientList.Location = New System.Drawing.Point(2, 2)
        Me.lvFFClientList.MultiSelect = False
        Me.lvFFClientList.HoverSelection = False
        Me.lvFFClientList.Name = "lvFFClientList"
        Me.lvFFClientList.ShowItemToolTips = True
        Me.lvFFClientList.TabIndex = 2
        Me.lvFFClientList.UseCompatibleStateImageBehavior = True
        Me.lvFFClientList.View = System.Windows.Forms.View.Tile
        Me.lvFFClientList.StateImageList = Me.ImageList1
        Me.lvFFClientList.SmallImageList = Me.ImageList1
        Me.lvFFClientList.LargeImageList = Me.ImageList1
        Me.lvFFClientList.Sorting = SortOrder.Ascending
    End Sub

    Private Sub LoadFlexFlowClientList()
        Try
            Me.lvFFClientList.BeginUpdate()
            Me.lvFFClientList.Items.Clear()
            Dim allProcess As System.Diagnostics.Process() = Process.GetProcessesByName(FFAPP)
            Dim iCounter As Integer = 0
            For Each p As Process In allProcess
                iCounter += 1
                Me.lvFFClientList.Items.Add(ToPopuldateClientIntance(iCounter, p))
            Next
            lvFFClientList.EndUpdate()

            Me.ToRefreshMenuItems()
        Catch ex As Exception

        End Try
    End Sub

    Private Function ToPopuldateClientIntance(ByVal Seq As Integer, ByVal currProcess As Process) As ListViewItem
        Static iCounter As Integer = 1
        Try
            Dim prjName As String = ParserToGetProjectName(currProcess.MainModule.FileName)
            Dim spliterChar() As String = New String() {" • "}

            Dim sTitles As String() = currProcess.MainWindowTitle.Split(spliterChar, StringSplitOptions.RemoveEmptyEntries)
            Dim stName As String = ""
            If (sTitles(0).Contains("Flexflow") Or sTitles(0).Contains("FlexFlow")) Then
                stName = sTitles(0).Substring(0, 12) & "..."
            Else
                stName = sTitles(0)
            End If

            Dim it As New ListViewItem(prjName, 0)
            it.Tag = currProcess
            Dim itSub1 As New ListViewItem.ListViewSubItem()
            itSub1.Text = stName

            Dim itSub2 As New ListViewItem.ListViewSubItem
            itSub2.Text = currProcess.MainModule.FileVersionInfo.InternalName

            it.SubItems.Add(itSub1)
            it.SubItems.Add(itSub2)

            Return it
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function ParserToGetProjectName(ByVal fileName As String)
        fileName = fileName.Substring(0, fileName.LastIndexOf("\"))
        Dim fElement As String() = fileName.Split("\")
        Return fElement(fElement.Length - 1)
    End Function

#End Region

#Region "Generate Context Menus"

    Private Sub ToGenMenuInstance()
        mMenu = New ContextMenu()
        mMenu.MenuItems.Add(ToGenMenuItemInstance(MU_REFRESH))
        mMenu.MenuItems.Add(ToGenMenuItemInstance(""))
        mMenu.MenuItems.Add(ToGenMenuItemInstance("-"))
        'mMenu.MenuItems.Add(ToGenMenuItemInstance(MU_OPENNEWINSTANCE))
        mMenu.MenuItems.Add(ToGenMenuItemInstance(MU_ENDSELECTEDTASK))
        mMenu.MenuItems.Add(ToGenMenuItemInstance(MU_ENDALLTASK))
        'mMenu.MenuItems.Add(ToGenMenuItemInstance("-"))
        'mMenu.MenuItems.Add(ToGenMenuItemInstance(MU_V_Title))
        'mMenu.MenuItems.Add(ToGenMenuItemInstance(MU_V_List))
        'mMenu.MenuItems.Add(ToGenMenuItemInstance(MU_V_Detail))
        'mMenu.MenuItems.Add(ToGenMenuItemInstance(MU_V_SmallIcon))
        'mMenu.MenuItems.Add(ToGenMenuItemInstance(MU_V_LargeIcon))
        mMenu.MenuItems.Add(ToGenMenuItemInstance("-"))
        mMenu.MenuItems.Add(ToGenMenuItemInstance(""))
        mMenu.MenuItems.Add(ToGenMenuItemInstance(MU_CLOSE))
        Me.ContextMenu = mMenu
        ToRefreshMenuItems()
    End Sub

    Private Sub ToRefreshMenuItems()
        If (Me.ContextMenu Is Nothing) Then Return

        If (Me.lvFFClientList.Items.Count = 0 Or Me.lvFFClientList.SelectedItems.Count = 0) Then
            ' Me.ContextMenu.MenuItems.Item(MU_OPENNEWINSTANCE).Enabled = False
            Me.ContextMenu.MenuItems.Item(MU_ENDSELECTEDTASK).Enabled = False
        Else
            '  Me.ContextMenu.MenuItems.Item(MU_OPENNEWINSTANCE).Enabled = True
            Me.ContextMenu.MenuItems.Item(MU_ENDSELECTEDTASK).Enabled = True
        End If

        If (Me.lvFFClientList.Items.Count = 0) Then
            Me.ContextMenu.MenuItems.Item(MU_ENDALLTASK).Enabled = False
        Else
            Me.ContextMenu.MenuItems.Item(MU_ENDALLTASK).Enabled = True
        End If

    End Sub

    Private Function ToGenMenuItemInstance(ByVal menuName As String) As MenuItem
        Dim mObject As New MenuItem(menuName)
        mObject.Name = menuName
        If String.IsNullOrEmpty(menuName) Then
            mObject.Enabled = False
            Return mObject
        End If

        AddHandler mObject.Click, AddressOf MenuItemClicked
        Return mObject
    End Function

    Private Sub MenuItemClicked(ByVal sender As Object, ByVal e As EventArgs)
        Dim m As MenuItem = DirectCast(sender, MenuItem)
        Select Case m.Name.ToUpper()
            Case MU_REFRESH.ToUpper
                LoadFlexFlowClientList()
            Case MU_OPENNEWINSTANCE.ToUpper
                Me.OpenANewProcess()
            Case MU_ENDSELECTEDTASK.ToUpper
                Me.KillCurrProcess()
            Case MU_ENDALLTASK.ToUpper
                Me.KillAllProcess()
            Case MU_CLOSE.ToUpper
                Me.Close()
            Case Else
                ToAssignListViewState(m.Name)
        End Select
    End Sub

#End Region

#Region "Form Events List"

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.BackColor = Color.SteelBlue
            Me.lvFFClientList.BackColor = Color.SteelBlue
            Me.AllowTransparency = True
            Me.TopMost = True
            Me.FormBorderStyle = FormBorderStyle.None
            modMain.SetWindowPos(Me.Handle, 0, 240, 0, 1000, 8, 0)
            InitListViewLayOut()
            LoadFlexFlowClientList()
            AddMouseLeaveHandlers()
            ToGenMenuInstance()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Name & "::frmMain_Load()")
        End Try
    End Sub

    Private Sub frmMain_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If (e.Button = MouseButtons.Left) Then
            ReleaseCapture()
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0)
        End If
    End Sub

    Private Sub frmMain_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        LoadFlexFlowClientList()
        Me.Height = 200
    End Sub

    Private Sub lvFFClientList_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvFFClientList.MouseDoubleClick
        ActiveCurrProcess()
    End Sub

    Private Sub EventToTriggerRefreshMenuItems(sender As Object, e As EventArgs) Handles lvFFClientList.SelectedIndexChanged, lvFFClientList.ItemActivate
        ToRefreshMenuItems()
    End Sub

#End Region

#Region "common functions of auto hide/display form"

    Sub AddMouseLeaveHandlers()
        For Each c As Control In Me.Controls
            HookItUp(c)
        Next
        AddHandler Me.MouseLeave, AddressOf CheckMouseLeave
    End Sub

    Sub HookItUp(ByVal c As Control)
        AddHandler c.MouseLeave, AddressOf CheckMouseLeave
        If c.HasChildren Then
            For Each f As Control In c.Controls
                HookItUp(f)
            Next
        End If
    End Sub

    Private Sub CheckMouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim pt As Point = PointToClient(Cursor.Position)
        If ClientRectangle.Contains(pt) = False Then
            Me.Height = 8
        End If
    End Sub

#End Region

#Region "Context Menu mothods"

    Private Sub OpenANewProcess()
        If (Me.lvFFClientList.Items.Count = 0) Then Return
        If (Me.lvFFClientList.SelectedItems.Count = 0) Then Return

        Dim it As ListViewItem = Me.lvFFClientList.SelectedItems(0)
        If (it Is Nothing) Then Return

        Dim currP As Process = it.Tag
        System.Diagnostics.Process.Start(currP.MainModule.FileName).WaitForInputIdle()
        LoadFlexFlowClientList()
    End Sub

    Private Sub KillCurrProcess()
        Dim it As ListViewItem = Me.lvFFClientList.SelectedItems(0)
        If (it Is Nothing) Then Return

        Dim currP As Process = it.Tag
        currP.Kill()
        Me.lvFFClientList.Items.Remove(it)
        ToRefreshMenuItems()
    End Sub

    Private Sub KillAllProcess()
        For Each it As ListViewItem In Me.lvFFClientList.Items
            Dim currP As Process = it.Tag
            currP.Kill()
            Me.lvFFClientList.Items.Remove(it)
        Next
        ToRefreshMenuItems()
    End Sub

    Private Sub ActiveCurrProcess()
        Dim it As ListViewItem = Me.lvFFClientList.SelectedItems(0)
        If (it Is Nothing) Then Return

        Dim currP As Process = it.Tag
        SetForegroundWindow(currP.MainWindowHandle)
        ShowWindow(currP.MainWindowHandle, SW_SHOWMAXIMIZED)
    End Sub

    Private Sub ToAssignListViewState(ByVal listViewMode As String)
        Select Case listViewMode.ToUpper()
            Case "TITLE"
                Me.lvFFClientList.View = System.Windows.Forms.View.Tile
            Case "DETAILS"
                Me.lvFFClientList.View = System.Windows.Forms.View.Details
            Case "LIST"
                Me.lvFFClientList.View = System.Windows.Forms.View.List
            Case "SMALLICON"
                Me.lvFFClientList.View = System.Windows.Forms.View.SmallIcon
            Case "LARGEICON"
                Me.lvFFClientList.View = System.Windows.Forms.View.LargeIcon

        End Select
    End Sub
#End Region

End Class
