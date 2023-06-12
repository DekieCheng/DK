Public Class frmQRMaker


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

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
        If String.IsNullOrEmpty(Me.txtQRCodeString.Text) Then Return
        Try
            'Dim modelImge As Image = ToGenImage(Me.txtQRCodeString.Text)

            Me.PictureBox1.Image = ToGenImage(Me.txtQRCodeString.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class