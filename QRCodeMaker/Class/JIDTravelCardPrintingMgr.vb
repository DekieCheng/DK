Imports FFSystem
Imports FlexFlow

Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections
Imports System.Data
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Public Class JIDTravelCardPrintingMgr
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="oCurrBatchLotUnit">it's the current batch lot unit</param>
    ''' <param name="UDPName">UDP name configured in task setting to load the travel card info</param>
    ''' <param name="IsReview">Decide if allow preview the report content before being printed out</param>
    ''' <param name="PrinterName">The specified printer name configured in task setting</param>
    ''' <returns></returns>
    Public Function ProcessToPrintTravelCard(ByVal oCurrBatchLotUnit As Unit, ByVal UDPName As String, ByVal IsReview As Boolean, ByVal PrinterName As String, ByVal oPO As ProductionOrder, ByVal oPart As Part) As Boolean
        Dim flag As Boolean = False
        Try
            Dim mJIDTravelCardInfo As clsJIDTravelCardInfo = New clsJIDTravelCardInfo()
            Dim m_objJIDPrintTravellerCard As New clsJIDPrintTravelCard

            m_objJIDPrintTravellerCard.SetJIDTravelCardInfo = mJIDTravelCardInfo.ProcessToGetTravelCardInfo(oCurrBatchLotUnit, UDPName, oPO, oPart)

            'Set Title
            m_objJIDPrintTravellerCard.SetTitleText = "产品跟踪单"
            m_objJIDPrintTravellerCard.SetTitleFont = New Font("宋体", 14, FontStyle.Bold)
            m_objJIDPrintTravellerCard.SetTitleHeight = 25

            'Set Head Description
            m_objJIDPrintTravellerCard.SetHeadFont = New Font("宋体", 12, FontStyle.Underline)
            m_objJIDPrintTravellerCard.SetHeadHeight = 20

            'Set Table Head Properties
            m_objJIDPrintTravellerCard.SetTableHeaderFont = New Font("宋体", 10, FontStyle.Bold)
            m_objJIDPrintTravellerCard.SetTableFont = New Font("宋体", 10)
            m_objJIDPrintTravellerCard.SetCellHeight = 20

            'Set Printer Name
            m_objJIDPrintTravellerCard.PrinterName = PrinterName

            m_objJIDPrintTravellerCard.Print(IsReview)

            flag = True
        Catch ex As FFException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
        Return flag
    End Function
End Class
