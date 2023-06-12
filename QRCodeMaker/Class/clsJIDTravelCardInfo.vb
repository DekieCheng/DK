Imports FFSystem
Imports FlexFlow

Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections
Imports System.Data
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Public Class clsJIDTravelCardInfo
    ' Fields
    Private m_ID As Integer
    Private m_BatchLotNo As String
    Private m_BatchLotQty As Integer
    Private m_WO As String
    Private m_WOQty As Integer
    Private m_ModelType As String
    Private m_WeekNo As String
    Private m_LineName As String
    Private m_Remark As String
    Private m_DateCode As String
    Private m_WHCode As String
    Private m_TravelCardProcessMasterInfo As DataTable
    Private m_TravelCardProcessDetailInfo As DataTable

    ' Methods
    Friend Sub New()

    End Sub
    Enum colName
        RowID = 0
        WO = 1
        WOQty = 2
        BatchLotNo = 3
        BatchLotQty = 4
        ModelType = 5
        WeekNo = 6
        LineName = 7
        Remarks = 8
        DateCode = 9
        WHCode = 10
    End Enum

    Public Function ProcessToGetTravelCardInfo(ByVal BatchLotUnit As Unit, ByVal UDPName As String, ByVal oPO As ProductionOrder, ByVal oPart As Part) As clsJIDTravelCardInfo
        Dim mJIDTravellerCard As New clsJIDTravelCardInfo

        Try
            Dim ds As DataSet = Me.ToGetTravelCardMasterInfo(BatchLotUnit, UDPName, oPO, oPart)

            Dim dt As DataTable = ds.Tables(0) '1st data table is the travel card 

            With mJIDTravellerCard
                .ID = Integer.Parse(IfIsNothing4(dt.Rows(0)(colName.RowID)))
                .WO = StripDBNull(dt.Rows(0)(colName.WO))
                .WOQty = Integer.Parse(IfIsNothing4(dt.Rows(0)(colName.WOQty)))
                .BatctLotNo = StripDBNull(dt.Rows(0)(colName.BatchLotNo))
                .BatchLotQty = Integer.Parse(IfIsNothing4(dt.Rows(0)(colName.BatchLotQty)))
                .ModelType = StripDBNull(dt.Rows(0)(colName.ModelType))
                .WeekNo = StripDBNull(dt.Rows(0)(colName.WeekNo))
                .LineName = StripDBNull(dt.Rows(0)(colName.LineName))
                .Remarks = StripDBNull(dt.Rows(0)(colName.Remarks))
                .DateCode = StripDBNull(dt.Rows(0)(colName.DateCode))
                .WHCode = StripDBNull(dt.Rows(0)(colName.WHCode))
                .TravelCardProcessMasterInfo = ds.Tables(1)
                .TravelCardProcessDetailInfo = ds.Tables(2)
            End With

        Catch ex As Exception
            Throw ex
        End Try

        Return mJIDTravellerCard

    End Function

    Private Function ToGetTravelCardMasterInfo(ByVal BatchLotUnit As Unit, ByVal UDPName As String, ByVal oPO As ProductionOrder, ByVal oPart As Part) As DataSet
        Dim ds As DataSet = Nothing
        Dim sp As StoredProcBuilder = New StoredProcBuilder

        Try
            Dim s As String = FFApplication.GetDBContext.DBName '.Server
            Dim p As String = FFApplication.GetDBContext.UserName
            Dim pwd As String = FFApplication.GetDBContext.Pwd

            If Not (BatchLotUnit Is Nothing) Then
                sp.AddParameter("@xmlUnitSN", BatchLotUnit)
            End If

            If Not (oPart Is Nothing) Then
                sp.AddParameter("@xmlPart", oPart)
            End If

            If Not (oPO Is Nothing) Then
                sp.AddParameter("@xmlProdOrder", oPO)
            End If

            sp.AddParameter("@xmlStation", StationMgr.CurrentStation)
            sp.AddParameter("@EmployeeID", EmployeeMgr.CurrentEmployee)
            sp.AddParameter("@xmlExtraData", "")

            sp.StoredProcName = UDPName
            sp.Execute()
            ds = sp.ReturnDataSet

        Catch ex As FFException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
        Return ds
    End Function

    Private Function StripNull(ByVal content As Object, Optional ByVal ReplaceNullWith As Object = "") As Object
        If (content Is DBNull.Value) Then
            Return ReplaceNullWith
        End If
        Return content
    End Function

#Region "Properties"
    Public Property ID() As Integer
        Get
            Return Me.m_ID
        End Get
        Set(ByVal Value As Integer)
            Me.m_ID = Value
        End Set
    End Property

    Public Property WO() As String
        Get
            Return Me.m_WO
        End Get
        Set(ByVal Value As String)
            Me.m_WO = Value
        End Set
    End Property

    Public Property WOQty() As Integer
        Get
            Return Me.m_WOQty

        End Get
        Set(ByVal Value As Integer)
            Me.m_WOQty = Value
        End Set
    End Property

    Public Property BatctLotNo() As String
        Get
            Return Me.m_BatchLotNo

        End Get
        Set(ByVal Value As String)
            Me.m_BatchLotNo = Value
        End Set
    End Property

    Public Property BatchLotQty() As Integer
        Get
            Return Me.m_BatchLotQty

        End Get
        Set(ByVal Value As Integer)
            Me.m_BatchLotQty = Value
        End Set
    End Property

    Public Property ModelType() As String
        Get
            Return Me.m_ModelType
        End Get
        Set(ByVal Value As String)
            Me.m_ModelType = Value
        End Set
    End Property

    Public Property WeekNo() As String
        Get
            Return Me.m_WeekNo
        End Get
        Set(ByVal Value As String)
            Me.m_WeekNo = Value
        End Set
    End Property
    Public Property LineName() As String
        Get
            Return Me.m_LineName
        End Get
        Set(ByVal Value As String)
            Me.m_LineName = Value
        End Set
    End Property
    Public Property Remarks() As String
        Get
            Return Me.m_Remark
        End Get
        Set(ByVal Value As String)
            Me.m_Remark = Value
        End Set
    End Property
    Public Property DateCode() As String
        Get
            Return Me.m_DateCode
        End Get
        Set(ByVal Value As String)
            Me.m_DateCode = Value
        End Set
    End Property

    Public Property WHCode() As String
        Get
            Return Me.m_WHCode
        End Get
        Set(ByVal Value As String)
            Me.m_WHCode = Value
        End Set
    End Property
    Public Property TravelCardProcessMasterInfo() As DataTable
        Get
            If m_TravelCardProcessMasterInfo.PrimaryKey IsNot Nothing Then
                Dim primaryKey(1) As DataColumn
                primaryKey(0) = m_TravelCardProcessMasterInfo.Columns(0)
                m_TravelCardProcessMasterInfo.PrimaryKey = primaryKey
            End If
            Return Me.m_TravelCardProcessMasterInfo
        End Get
        Set(ByVal Value As DataTable)
            Me.m_TravelCardProcessMasterInfo = Value
        End Set
    End Property
    Public Property TravelCardProcessDetailInfo() As DataTable
        Get
            Return Me.m_TravelCardProcessDetailInfo
        End Get
        Set(ByVal Value As DataTable)
            Me.m_TravelCardProcessDetailInfo = Value
        End Set
    End Property

#End Region

End Class