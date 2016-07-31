Public Class frmSign
    Dim gHashStockTable As New Hashtable        '// 주식 종목 테이블

    Private Sub frmSign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        gHashStockTable = frmMain.getStockCodeTable
    End Sub

    Private Sub lstSign_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lstSign.MouseDoubleClick
        Dim sTxt As String

        sTxt = lstSign.SelectedItems(0).Text
        frmMain.txtSuggest.Text = sTxt
        frmMain.txtStockCode.Text = gHashStockTable(sTxt)

        Call frmMain.Button3_Click(sender, e)
        Call frmMain.btnCmd2_Click(sender, e)
        Call frmMain.btnCmd3_Click(sender, e)
    End Sub

    Private Sub lstSign_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstSign.SelectedIndexChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmMain.setStop(True)
    End Sub
End Class