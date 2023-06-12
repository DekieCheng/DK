Imports System.Drawing.Printing
Imports System.Drawing.Color
Imports System.Windows.Forms
Imports System.Drawing.Font
Imports System.Drawing.PointF
Imports System.Windows.Forms.DataGrid
Imports System.Drawing.Pen
Imports System.Drawing

Public Class clsJIDPrintTravelCard

#Region "Class variables"

    Private m_CON As String() = New String() {"���ڣ�"�� "������"�� "���ޣ�"�� "ǩ����"�� "��ע��"}

    Private m_JIDTravellerCard As New clsJIDTravelCardInfo
    Private m_PrinterName As String = ""

    'User defined properties
    Private TableHeaderFont As New Font("Verdana", 9)           'font and font size for the table
    Private TableFont As New Font("Verdana", 9)           'font and font size for the table

    Private TitleFont As New Font("Verdana", 10, FontStyle.Underline) 'fonr of the header
    Private HeadFont As New Font("Verdana", 10, FontStyle.Underline) 'fonr of the header
    Private SubHeadFont As New Font("Verdana", 10, FontStyle.Regular) 'font for sub header

    Private TitleText As String   'text for header
    Private SubHeadLeftText As String  'text for subheader at left
    Private SubHeadRightText As String  'text for subheader at right

    Private TitleHeight As Integer = 40 'Height of header
    Private HeadHeight As Integer = 40 'Height of header
    Private SubHeadHeight As Integer = 30 'Height of subheader

    Private FootFont As New Font("Verdana", 12, FontStyle.Underline) 'font size at foot
    Private SubFootFont As New Font("Verdana", 10, FontStyle.Regular) 'font size at subfoot
    Private FootText As String  'text of at foot

    Private SubFootLeftText As String  'text of at subfoot
    Private SubfootRightText As String
    Private FootHeight As Integer = 40 'height of at foot
    Private SubFootHeight As Integer = 30 'height of at subfoot

    Dim X_unit As Integer                               'basic unit for the table
    Dim Y_unit As Integer = TableFont.Height * 2.5

    'Used in internal module
    Private ev As Printing.PrintPageEventArgs
    Private PrintDocument1 As Printing.PrintDocument

    Private m_objJIDTravelCardInfo As clsJIDTravelCardInfo
    Private m_ProcessTable As DataTable
    Private m_ProcessDetailTable As DataTable
    Private ColsCount As Integer                        'Total column
    Private PrintingLineNumber As Integer = 0           'the row number will be printed
    Private PageRecordNumber As Integer                 'total record row number for the current printing
    Private PrintingPageNumber As Integer = 0           'Page number is printing
    Private PageNumber As Integer                       'Total pages
    Private PrintRecordLeave As Integer                 'Total records unprinted at the current
    Private PrintRecordComplete As Integer = 0          'Total records number has been printed out

    Private Pleft As Integer
    Private Ptop As Integer
    Private Pright As Integer
    Private Pbottom As Integer
    Private Pwidth As Integer
    Private Pheigh As Integer

    Private Totalpage As Integer 'Total pages will be printed
#End Region

#Region "Constructure"

    Sub New()
        MyBase.New()
    End Sub

    Sub New(ByVal TableSource As DataTable)
        m_ProcessTable = TableSource
    End Sub
#End Region

#Region "Properties"

    'User defined Fonts and Size
    Public WriteOnly Property SetJIDTravelCardInfo() As clsJIDTravelCardInfo
        Set(ByVal Value As clsJIDTravelCardInfo)
            m_objJIDTravelCardInfo = Value
            m_ProcessTable = Value.TravelCardProcessMasterInfo
            m_ProcessDetailTable = Value.TravelCardProcessDetailInfo
        End Set
    End Property

    'User defined Fonts and Size
    Public WriteOnly Property SetTableHeaderFont() As System.Drawing.Font
        Set(ByVal Value As System.Drawing.Font)
            TableHeaderFont = Value
        End Set
    End Property

    Public WriteOnly Property SetTableFont() As System.Drawing.Font
        Set(ByVal Value As System.Drawing.Font)
            TableFont = Value
        End Set
    End Property

    Public WriteOnly Property SetTitleFont() As System.Drawing.Font
        Set(ByVal Value As System.Drawing.Font)
            TitleFont = Value
        End Set
    End Property

    Public WriteOnly Property SetHeadFont() As System.Drawing.Font
        Set(ByVal Value As System.Drawing.Font)
            HeadFont = Value
        End Set
    End Property

    Public WriteOnly Property SetSubHeadFont() As System.Drawing.Font
        Set(ByVal Value As System.Drawing.Font)
            SubHeadFont = Value
        End Set
    End Property

    Public WriteOnly Property SetFootFont() As System.Drawing.Font
        Set(ByVal Value As System.Drawing.Font)
            FootFont = Value
        End Set
    End Property

    Public WriteOnly Property SetSubFootFont() As System.Drawing.Font
        Set(ByVal Value As System.Drawing.Font)
            SubFootFont = Value
        End Set
    End Property

    Public WriteOnly Property SetTitleText() As String
        Set(ByVal Value As String)
            TitleText = Value
        End Set
    End Property

    Public WriteOnly Property SetSubHeadLeftText() As String
        Set(ByVal Value As String)
            SubHeadLeftText = Value
        End Set
    End Property

    Public WriteOnly Property SetSubHeadRightText() As String
        Set(ByVal Value As String)
            SubHeadRightText = Value
        End Set
    End Property

    Public WriteOnly Property SetFootText() As String
        Set(ByVal Value As String)
            FootText = Value
        End Set
    End Property

    Public WriteOnly Property SetSubFootLeftText() As String
        Set(ByVal Value As String)
            SubFootLeftText = Value
        End Set
    End Property

    Public WriteOnly Property SetSubFootRightText() As String
        Set(ByVal Value As String)
            SubfootRightText = Value
        End Set
    End Property

    Public WriteOnly Property SetHeadHeight() As Integer
        Set(ByVal Value As Integer)
            HeadHeight = Value
        End Set
    End Property

    Public WriteOnly Property SetTitleHeight() As Integer
        Set(ByVal Value As Integer)
            TitleHeight = Value
        End Set
    End Property

    Public WriteOnly Property SetSubHeadHeight() As Integer
        Set(ByVal Value As Integer)
            SubHeadHeight = Value
        End Set
    End Property

    Public WriteOnly Property SetFootHeight() As Integer
        Set(ByVal Value As Integer)
            FootHeight = Value
        End Set
    End Property

    Public WriteOnly Property SetSubFootHeight() As Integer
        Set(ByVal Value As Integer)
            SubFootHeight = Value
        End Set
    End Property

    Public WriteOnly Property SetCellHeight() As Integer
        Set(ByVal Value As Integer)
            Y_unit = Value
        End Set
    End Property

    Public WriteOnly Property SetProcessDataSource() As DataTable
        Set(ByVal Value As DataTable)
            Me.m_ProcessTable = Value
        End Set
    End Property

    Public WriteOnly Property SetProcessDetailSource() As DataTable
        Set(ByVal Value As DataTable)
            Me.m_ProcessDetailTable = Value
        End Set
    End Property

    Public WriteOnly Property PrinterName() As String
        Set(ByVal value As String)
            m_PrinterName = value
        End Set
    End Property
#End Region

#Region "Public Methods"
    Public Sub Print(ByVal IsPreview As Boolean)
        Try
            PrintDocument1 = New Printing.PrintDocument
            AddHandler PrintDocument1.PrintPage, AddressOf Me.PrintDocument1_PrintPage

            Dim PageSetup As PageSetupDialog
            PageSetup = New PageSetupDialog
            PageSetup.Document = PrintDocument1

            PageSetup.PageSettings.Landscape = False
            If m_PrinterName <> "" Or m_PrinterName.ToUpper <> "NONE" Then
                PageSetup.PageSettings.PrinterSettings.PrinterName = m_PrinterName
            End If

            PrintDocument1.DefaultPageSettings = PageSetup.PageSettings
            If IsPreview Then
                ' PageSetup.PageSettings.Landscape = False
                If PageSetup.ShowDialog() = DialogResult.Cancel Then
                    Exit Sub
                End If
            End If

            Pleft = 20
            Ptop = 30
            Pright = 30
            Pbottom = 30
            Pwidth = PrintDocument1.DefaultPageSettings.Bounds.Width
            Pheigh = PrintDocument1.DefaultPageSettings.Bounds.Height

            PrintDocument1.DocumentName = Totalpage.ToString
            If IsPreview Then
                Dim PrintPriview As PrintPreviewDialog
                PrintPriview = New PrintPreviewDialog
                PrintPriview.Document = PrintDocument1
                PrintPriview.WindowState = FormWindowState.Maximized
                PrintPriview.ShowDialog()
            Else
                PrintDocument1.DocumentName = "��Ʒ���ٵ�"
                PrintDocument1.Print()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbOK)
        End Try
    End Sub

    Private m_CurrPage As Integer = 0
    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal ev As System.Drawing.Printing.PrintPageEventArgs)
        Try
            m_CurrPage += 1

            Dim fmt As New StringFormat
            fmt.LineAlignment = StringAlignment.Center '���¶���
            fmt.FormatFlags = StringFormatFlags.LineLimit '�Զ�����

            Dim Rect As New Rectangle 'Range for print
            Dim Pen As New Pen(Brushes.Black, 1) 'Line style for print
            Dim Font_10 As Font = New Font("����", 10, FontStyle.Regular)
            Dim BoldFont_18 As Font = New Font("����", 18, FontStyle.Bold)
            Dim BoldFont_10 As Font = New Font("����", 10, FontStyle.Bold)

            If m_CurrPage = 1 Then
                'Only the 1st page will need to print the header information
                fmt.Alignment = StringAlignment.Near
                Rect.X = Pleft
                Rect.Y = Ptop - 15
                Rect.Height = 40
                Rect.Width = 500 'Pwidth - Pleft - Pright

                ev.Graphics.DrawString("��Ʒ���ٵ�", BoldFont_18, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)

                Rect.X = Pleft + 180
                Rect.Y = Ptop - 30
                ev.Graphics.DrawString("������/����:" & m_objJIDTravelCardInfo.WO & "/" & m_objJIDTravelCardInfo.WOQty.ToString, Font_10, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)

                'Draw barcode
                Rect.Y = Ptop - 10
                ev.Graphics.DrawString("*" & m_objJIDTravelCardInfo.WO & "*", New Font("Free 3 of 9 Extended", 25, FontStyle.Regular), Brushes.Black, RectangleF.op_Implicit(Rect), fmt)

                Rect.X = Pleft + 400
                Rect.Y = Ptop - 30
                ev.Graphics.DrawString("����/����:" & m_objJIDTravelCardInfo.BatctLotNo & "/" & m_objJIDTravelCardInfo.BatchLotQty.ToString, Font_10, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)

                'Draw barcode
                Rect.Y = Ptop - 10
                ev.Graphics.DrawString("*" & m_objJIDTravelCardInfo.BatctLotNo & "*", New Font("Free 3 of 9 Extended", 25, FontStyle.Regular), Brushes.Black, RectangleF.op_Implicit(Rect), fmt)

                'Draw the rectangle of the header
                Rect.X = Pleft
                Rect.Y = Ptop + 25
                Rect.Height = 80
                Rect.Width = 770
                Dim myBrush As New SolidBrush(Drawing.Color.WhiteSmoke)
                ev.Graphics.FillRectangle(myBrush, Rect)
                ev.Graphics.DrawRectangle(Pen, Rect)

                Rect.X = Pleft + 5
                Rect.Y = Ptop + 30
                Rect.Height = TitleHeight
                Rect.Width = 100
                ev.Graphics.DrawString("�����ţ�", BoldFont_10, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)
                Rect.Y = Rect.Y + 25
                ev.Graphics.DrawString("�ܴΣ�", BoldFont_10, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)
                Rect.Y = Rect.Y + 25
                ev.Graphics.DrawString("��ע��", BoldFont_10, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)

                Rect.X = Rect.Width - 5
                Rect.Y = Ptop + 30
                ev.Graphics.DrawString(m_objJIDTravelCardInfo.WO, BoldFont_10, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)

                Rect.Y = Rect.Y + 25
                ev.Graphics.DrawString(m_objJIDTravelCardInfo.WeekNo, BoldFont_10, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)
                Rect.X = Rect.Width + 60
                If Not m_objJIDTravelCardInfo.WeekNo = String.Empty Then
                    ev.Graphics.DrawString("*" & m_objJIDTravelCardInfo.WeekNo & "*", New Font("Free 3 of 9 Extended", 16, FontStyle.Regular), Brushes.Black, RectangleF.op_Implicit(Rect), fmt)
                End If

                Rect.Y = Rect.Y + 25
                Rect.X = Rect.X - 95
                Rect.Width = 200
                ev.Graphics.DrawString(m_objJIDTravelCardInfo.Remarks, BoldFont_10, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)

                Rect.X = Rect.X + 190
                Rect.Y = Ptop + 30
                Rect.Height = TitleHeight
                Rect.Width = 100
                ev.Graphics.DrawString("ģ�����ͣ�", BoldFont_10, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)
                Rect.Y = Rect.Y + 25
                ev.Graphics.DrawString("�ߣ�", BoldFont_10, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)
                Rect.Y = Rect.Y + 25
                ev.Graphics.DrawString("���ڴ��룺", BoldFont_10, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)

                Rect.X = Rect.X + 75
                Dim orgX As Integer = Rect.X
                Rect.Y = Ptop + 30
                Rect.Width = 280
                ev.Graphics.DrawString(m_objJIDTravelCardInfo.ModelType, BoldFont_10, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)
                Rect.Width = 200 'restore back the width
                Rect.X = orgX + 255
                'ev.Graphics.DrawString("*" & m_objJIDTravelCardInfo.ModelType & "*", New Font("Free 3 of 9 Extended", 16, FontStyle.Regular), Brushes.Black, RectangleF.op_Implicit(Rect), fmt)

                Dim modelImge As Image = ToGenImage(m_objJIDTravelCardInfo.ModelType)
                If modelImge IsNot Nothing Then
                    ev.Graphics.DrawImage(modelImge, Rect.X + 50, Rect.Y - 2, 55, 55)
                End If

                Rect.X = orgX
                Rect.Y = Rect.Y + 25
                ev.Graphics.DrawString(m_objJIDTravelCardInfo.LineName, BoldFont_10, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)
                Rect.Y = Rect.Y + 25
                ev.Graphics.DrawString(m_objJIDTravelCardInfo.DateCode, BoldFont_10, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)
                Rect.X = Rect.Width + 220
                If Not m_objJIDTravelCardInfo.DateCode = String.Empty Then
                    ev.Graphics.DrawString("*" & m_objJIDTravelCardInfo.DateCode & "*", New Font("Free 3 of 9 Extended", 16, FontStyle.Regular), Brushes.Black, RectangleF.op_Implicit(Rect), fmt)
                End If

                Rect.X = Rect.X + 120
                Rect.Y = Ptop + 80
                Rect.Height = TitleHeight
                Rect.Width = 100
                ev.Graphics.DrawString("�ֿ���룺", BoldFont_10, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)
                Rect.X = Rect.X + 80
                ev.Graphics.DrawString(m_objJIDTravelCardInfo.WHCode, BoldFont_10, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)

                If Not m_objJIDTravelCardInfo.WHCode = String.Empty Then
                    Rect.X = Rect.X + 80
                    ev.Graphics.DrawString("*" & m_objJIDTravelCardInfo.WHCode & "*", New Font("Free 3 of 9 Extended", 16, FontStyle.Regular), Brushes.Black, RectangleF.op_Implicit(Rect), fmt)
                End If

                Rect.Y = Ptop + 80 'Start from here to draw the records
            Else
                Rect.Y = Ptop - 25 'Start from here to draw the records
            End If

            Dim tmpProcessDT As DataTable = Me.m_ProcessTable.Copy ' Copy the record to temp datatable

            '1st while ... loop for Process Name
            For Each dr As DataRow In tmpProcessDT.Rows
                Dim ProceeName As String = dr(0).ToString & "." & dr(1).ToString

                Rect.X = Pleft
                Rect.Y = Rect.Y + 25
                Rect.Width = 600
                Rect.Height = 30
                ev.Graphics.DrawString(ProceeName, New Font("����", 14, FontStyle.Bold), Brushes.Black, RectangleF.op_Implicit(Rect), fmt)

                'here will draw the detail
                Dim dtDR As DataRow() = m_ProcessDetailTable.Select("Seq='" & dr(0).ToString & "'")
                Dim idxOfRow As Integer = 0
                Dim iCol As Integer = 0 ' the Column counter
                Rect.Y = Rect.Y + 25
                For Each mydr As DataRow In dtDR
                    If idxOfRow > 0 And (idxOfRow Mod 3) = 0 Then
                        Rect.X = Pleft
                        Rect.Y = Rect.Y + 13
                        iCol = 0
                    End If
                    ev.Graphics.DrawString(mydr(2).ToString, New Font("����", 7, FontStyle.Regular), Brushes.Black, Rect.X + iCol * 250, Rect.Y)
                    idxOfRow += 1
                    iCol += 1
                Next

                'draw the signature area
                If dtDR.Length > 0 Then
                    Rect.Y = Rect.Y + 10
                Else
                    Rect.Y = Rect.Y + 0
                End If
                For i As Integer = 0 To 4
                    ev.Graphics.DrawRectangle(Pen, Rect.X + 153 * i, Rect.Y, 153, 25)
                    ev.Graphics.DrawString(m_CON(i), New Font("����", 10, FontStyle.Bold), Brushes.Black, Rect.X + 153 * i + 2, Rect.Y + 6)
                Next

                Try
                    m_ProcessTable.Rows.Remove(m_ProcessTable.Rows.Find(dr(0).ToString))
                Catch ex As MissingPrimaryKeyException
                    Throw ex
                Catch ex As Exception
                    Throw ex
                End Try

                If Rect.Y + 80 > Pheigh - Pbottom Then
                    PrintingPageNumber += 1
                    Exit For
                End If
            Next

            If m_ProcessTable.Rows.Count <= 0 Then
                ev.HasMorePages = False
                PrintingPageNumber += 1
            Else
                ev.HasMorePages = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function ToGenImage(ByVal Value As String) As Image
        Try
            Dim BarCode As ThoughtWorks.QRCode.Codec.QRCodeEncoder = New ThoughtWorks.QRCode.Codec.QRCodeEncoder()
            BarCode.QRCodeEncodeMode = BarCode.ENCODE_MODE.BYTE
            BarCode.QRCodeScale = Convert.ToInt16(2)
            BarCode.QRCodeVersion = Convert.ToInt16(7)
            BarCode.QRCodeErrorCorrect = BarCode.ERROR_CORRECTION.L

            Dim image As Image
            image = BarCode.Encode(Value)

            Return image
        Catch ex As Exception
            Throw ex
            Return Nothing
        End Try
    End Function

#End Region

End Class


