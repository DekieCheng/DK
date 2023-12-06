Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Public Class IEEE754Helper
    Public Shared Function FloatToHex(ByVal paraFloat As Single) As String
        Dim sb As StringBuilder = New StringBuilder()
        Dim bytes As Byte() = BitConverter.GetBytes(paraFloat)

        For Each item As Byte In bytes
            sb.Insert(0, item.ToString("X2"))
        Next

        Return sb.Insert(0, "0X").ToString()
    End Function

    Public Shared Function HexToFloat(ByVal hexStr As String) As Single
        hexStr = hexStr.Replace(" ", "")
        hexStr = hexStr.Trim().ToUpper()

        If hexStr.StartsWith("0X") OrElse hexStr.StartsWith("0x") Then
            hexStr = hexStr.Substring(2)
        End If

        If hexStr.Length <> 8 Then
            Return Single.MinValue
        End If

        Dim bytes As Byte() = New Byte(3) {}

        For i As Integer = 0 To 4 - 1
            bytes(i) = Convert.ToByte(hexStr.Substring((3 - i) * 2, 2), 16)
        Next

        Return BitConverter.ToSingle(bytes, 0)
    End Function

    Public Shared Function FloatToBtyes(ByVal paraFloat As Single) As Byte()
        Return BitConverter.GetBytes(paraFloat)
    End Function

    Public Shared Function ByteToFloat(ByVal bytes As Byte()) As Single
        If bytes.Length <> 4 Then
            Return Single.MinValue
        End If

        Return BitConverter.ToSingle(bytes, 0)
    End Function
End Class