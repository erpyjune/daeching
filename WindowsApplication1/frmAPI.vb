﻿Public Class frmAPI
    Private Sub frmAPI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KHOpenAPI = frmMain.getAPI
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim msg As String
        msg = KHOpenAPI.GetMasterStockState(Trim(txtCode.Text))
        MsgBox(msg)
    End Sub

    Private Sub KHOpenAPI_OnEventConnect(sender As Object, e As AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent) Handles KHOpenAPI.OnEventConnect
        If e.nErrCode = 0 Then
            MsgBox("로그인 성공!!")
        Else
            MsgBox("로그인 실패")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim s As String
        s = "스팩대한민국제2"
        MsgBox(CStr(InStr(s, "스팩ㅇㅇㅇ")))
    End Sub
End Class