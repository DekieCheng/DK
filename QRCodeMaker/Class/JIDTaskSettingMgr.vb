Imports FFSystem
Imports FlexFlow
Imports System.Xml
Imports System.IO
Imports System.Collections.Generic
Imports System
Imports System.Configuration
Imports System.Reflection

Public Class JIDTaskSettingMgr

#Region "Const"

#End Region

#Region "Variables"

    Private m_CurrTask As Task = Nothing

#End Region

#Region "Constructor"

    Public Sub New(ByVal CurrTask As Task)
        Try
            m_CurrTask = CurrTask
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetTaskSettings() As Boolean
        Dim flag As Boolean = False
        Try
            flag = ReadTaskSettings(Me.m_CurrTask)
        Catch ex As Exception
            Throw ex
        End Try
        Return flag
    End Function

    Private Function ReadTaskSettings(ByVal CurrTask As Task) As Boolean
        Dim flag As Boolean = False

        Try
            m_IsReprintStation = IfIsNothing(CurrTask.GetSetting("RePrintStation"))
            m_AllowReview = IfIsNothing(CurrTask.GetSetting("AllowPreview"))
            m_UDPToLoadTravelCardInfo = StripDBNull(CurrTask.GetSetting("UDPToLoadTravelCardInfo"))
            m_PrinterName = StripDBNull(CurrTask.GetSetting("PrinterName"))

            flag = True

        Catch ex As Exception
            Throw ex
        End Try
        Return flag
    End Function

#End Region

#Region "Properties"

    Private m_IsReprintStation As Boolean
    Public ReadOnly Property IsReprintStation As Boolean
        Get
            Return m_IsReprintStation
        End Get
    End Property

    Private m_AllowReview As Boolean
    Public ReadOnly Property AllowReview As Integer
        Get
            Return m_AllowReview
        End Get
    End Property

    Private m_UDPToLoadTravelCardInfo As String = ""
    Public ReadOnly Property UDPToLoadTravelCardInfo As String
        Get
            Return m_UDPToLoadTravelCardInfo
        End Get
    End Property

    Private m_PrinterName As String = ""
    Public ReadOnly Property PrinterName As String
        Get
            Return m_PrinterName
        End Get
    End Property

#End Region

#Region "Methods"

#End Region

End Class
