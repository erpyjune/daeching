Public Class frmChart

    Private Sub frmChart_Load(sender As Object, e As EventArgs) Handles MyBase.Load



    End Sub

    Private Function Array(p1 As String, p2 As String, p3 As String, p4 As String, p5 As String, p6 As String, p7 As String, p8 As String) As Object
        Throw New NotImplementedException
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.chartStock.Series.Clear()
        Me.chartRect.Series.Add("키움증권")

        'Me.chartStock.Series("키움증권").Points.AddXY("6/1", 33)
        'Me.chartStock.Series("키움증권").Points.AddXY("6/2", 29)
        'Me.chartStock.Series("키움증권").Points.AddXY("6/3", 45)
        'Me.chartStock.Series("키움증권").Points.AddXY("6/4", 33)
        'Me.chartStock.Series("키움증권").Points.AddXY("6/5", 21)
        'Me.chartStock.Series("키움증권").Points.AddXY("6/6", 10)

        'Me.chartStock.Series("삼성증권").Points.AddXY("6/1", 21)
        'Me.chartStock.Series("삼성증권").Points.AddXY("6/2", 11)
        'Me.chartStock.Series("삼성증권").Points.AddXY("6/3", 23)
        'Me.chartStock.Series("삼성증권").Points.AddXY("6/4", 343)
        'Me.chartStock.Series("삼성증권").Points.AddXY("6/5", 99)
        'Me.chartStock.Series("삼성증권").Points.AddXY("6/6", 65)

        'Me.chartStock.Series("유안타").Points.AddXY("6/1", 121)
        'Me.chartStock.Series("유안타").Points.AddXY("6/2", 113)
        'Me.chartStock.Series("유안타").Points.AddXY("6/3", 19)
        'Me.chartStock.Series("유안타").Points.AddXY("6/4", 45)
        'Me.chartStock.Series("유안타").Points.AddXY("6/5", 13)
        'Me.chartStock.Series("유안타").Points.AddXY("6/6", 22)

        'Me.chartStock.Series("stockValue").Points.AddY("삼성증권")
        'Me.chartStock.Series("stockValue").Points.AddY("키움증권")

    End Sub

    Private Sub chartRect_Click(sender As Object, e As EventArgs) Handles chartRect.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.chartRect.Series("Series1").XValueMember = "age"
        Me.chartRect.Series("Series1").XValueMember = "hight"


    End Sub
End Class