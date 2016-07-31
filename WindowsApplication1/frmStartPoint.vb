Public Class frmStartPoint
    Dim gHashStockTable As New Hashtable

    Private Sub frmStartPoint_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KHOpenAPI = frmMain.getAPI
        gHashStockTable = frmMain.getStockCodeTable
    End Sub
    Private Sub lstViewStartPoint_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lstViewStartPoint.MouseDoubleClick
        Dim sTxt As String

        sTxt = lstViewStartPoint.SelectedItems(0).Text
        frmMain.txtSuggest.Text = sTxt
        frmMain.txtStockCode.Text = gHashStockTable(sTxt)

        Call frmMain.Button3_Click(sender, e)
        Call frmMain.btnCmd2_Click(sender, e)
        Call frmMain.btnCmd3_Click(sender, e)

    End Sub

    Private Sub lstViewStartPoint_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstViewStartPoint.SelectedIndexChanged

    End Sub
End Class