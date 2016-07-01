Public Class frmMain

    Dim strHowCommand As String
    Dim bLoginStatus As Boolean
    Dim StockTable As New Hashtable()
    Dim gStockCompData As New Hashtable





    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim stockValueMain As StockCompMain
        Dim stockValueSub As StockCompSub

        stockValueSub.vBuyCount = 100
        stockValueSub.vDate = "20161010"
        stockValueSub.vOnlyBuyCount = 101
        stockValueSub.vSaleCount = 99

        stockValueMain.stockByCompValue.add("aaaa")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        ' 현재 로그인 상태일 경우
        If bLoginStatus = True Then
            KHOpenAPI.CommTerminate()
            lstMsg.Items.Add(("==============================="))
            lstMsg.Items.Add(("로그아웃!!!"))
            bLoginStatus = False
            ' 현재 로그아웃 상태일 경우
        Else
            lstMsg.Items.Add(("==============================="))
            lstMsg.Items.Add(("로그인창 열기"))
            KHOpenAPI.CommConnect()
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        KHOpenAPI.SetInputValue("종목코드", Trim(txtStockCode.Text))
        KHOpenAPI.SetInputValue("수정주가구분", "1")
        '/// 조회구분 = 0: 입력한 시작,종료 날짜로 조회
        '///            1: 전일, 조회지정일 입력(5일 ~ 120일)
        KHOpenAPI.SetInputValue("조회구분", "0")
        KHOpenAPI.SetInputValue("시작일자", "20160601")
        KHOpenAPI.SetInputValue("종료일자", "20160624")
        'KHOpenAPI1.SetInputValue("기간", "")


        '//////////// 리스트뷰 삭제
        lstMsg.Items.Clear()

        KHOpenAPI.CommRqData("종목별증권사순위요청기간", "OPT10038", CInt("0"), "3002")

        strHowCommand = "어제"
    End Sub

    Private Sub KHOpenAPI_OnReceiveTrData(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent) Handles KHOpenAPI.OnReceiveTrData

        Dim item As ListViewItem

        If eventArgs.sRQName = "종목별증권사순위요청기간" Then
            Dim nCnt As Short, i As Short
            Dim strItemValue As String, strTemp As String

            nCnt = KHOpenAPI.GetRepeatCnt(eventArgs.sTrCode, eventArgs.sRQName)
            For i = 0 To (nCnt - 1)

                If strHowCommand = "종목별증권사순위요청기간1" Then
                    item = New ListViewItem(Trim(txtStartDate1.Text))
                ElseIf strHowCommand = "종목별증권사순위요청기간2" Then
                    item = New ListViewItem(Trim(txtStartDate2.Text))
                ElseIf strHowCommand = "종목별증권사순위요청기간3" Then
                    item = New ListViewItem(Trim(txtStartDate3.Text))
                Else
                    item = New ListViewItem(" ")
                End If


                'strItemValue = KHOpenAPI1.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "순위")
                'ListResult.Items.Add("순위:" + strItemValue)

                strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "회원사명")
                strTemp = Trim(strItemValue).Replace(" ", "")
                item.SubItems.Add(strTemp)

                strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "누적순매수수량")
                item.SubItems.Add(strItemValue)

                strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "매수수량")
                item.SubItems.Add(strItemValue)

                strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "매도수량")
                item.SubItems.Add(strItemValue)

                'strItemValue = KHOpenAPI1.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "순위1")
                'ListResult.Items.Add("순위1:" + strItemValue)

                'strItemValue = KHOpenAPI1.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "순위2")
                'ListResult.Items.Add("순위2:" + strItemValue)

                'strItemValue = KHOpenAPI1.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "순위3")
                'ListResult.Items.Add("순위3:" + strItemValue)

                'strItemValue = KHOpenAPI1.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "기간중거래량")
                'ListResult.Items.Add("기간중거래량:" + strItemValue)

                If strHowCommand = "종목별증권사순위요청기간1" Then
                    lstView1.Items.Add(item)
                ElseIf strHowCommand = "종목별증권사순위요청기간2" Then
                    lstView2.Items.Add(item)
                ElseIf strHowCommand = "종목별증권사순위요청기간3" Then
                    lstView3.Items.Add(item)
                Else
                    lstView3.Items.Add(item)
                End If

            Next
        ElseIf eventArgs.sRQName = "종목별증권사순위요청기간1" Then

        End If
    End Sub

    Private Sub btnCmd1_Click(sender As Object, e As EventArgs) Handles btnCmd1.Click
        KHOpenAPI.SetInputValue("종목코드", Trim(txtStockCode.Text))
        KHOpenAPI.SetInputValue("수정주가구분", "1")
        '/// 조회구분 = 0: 입력한 시작,종료 날짜로 조회
        '///            1: 전일, 조회지정일 입력(5일 ~ 120일)
        KHOpenAPI.SetInputValue("조회구분", "0")
        KHOpenAPI.SetInputValue("시작일자", Trim(txtStartDate1.Text))
        KHOpenAPI.SetInputValue("종료일자", Trim(txtEndDate1.Text))
        'KHOpenAPI1.SetInputValue("기간", "")

        lstView1.Items.Clear()

        KHOpenAPI.CommRqData("종목별증권사순위요청기간", "OPT10038", CInt("0"), "3002")

        strHowCommand = "종목별증권사순위요청기간1"
    End Sub

    Private Sub btnCmd2_Click(sender As Object, e As EventArgs) Handles btnCmd2.Click
        KHOpenAPI.SetInputValue("종목코드", Trim(txtStockCode.Text))
        KHOpenAPI.SetInputValue("수정주가구분", "1")
        '/// 조회구분 = 0: 입력한 시작,종료 날짜로 조회
        '///            1: 전일, 조회지정일 입력(5일 ~ 120일)
        KHOpenAPI.SetInputValue("조회구분", "0")
        KHOpenAPI.SetInputValue("시작일자", Trim(txtStartDate2.Text))
        KHOpenAPI.SetInputValue("종료일자", Trim(txtEndDate2.Text))
        'KHOpenAPI1.SetInputValue("기간", "")

        lstView2.Items.Clear()

        KHOpenAPI.CommRqData("종목별증권사순위요청기간", "OPT10038", CInt("0"), "3002")

        strHowCommand = "종목별증권사순위요청기간2"
    End Sub

    Private Sub btnCmd3_Click(sender As Object, e As EventArgs) Handles btnCmd3.Click
        KHOpenAPI.SetInputValue("종목코드", Trim(txtStockCode.Text))
        KHOpenAPI.SetInputValue("수정주가구분", "1")
        '/// 조회구분 = 0: 입력한 시작,종료 날짜로 조회
        '///            1: 전일, 조회지정일 입력(5일 ~ 120일)
        KHOpenAPI.SetInputValue("조회구분", "0")
        KHOpenAPI.SetInputValue("시작일자", Trim(txtStartDate3.Text))
        KHOpenAPI.SetInputValue("종료일자", Trim(txtEndDate3.Text))
        'KHOpenAPI1.SetInputValue("기간", "")

        lstView3.Items.Clear()

        KHOpenAPI.CommRqData("종목별증권사순위요청기간", "OPT10038", CInt("0"), "3002")

        strHowCommand = "종목별증권사순위요청기간3"
    End Sub

    Private Sub chartStock_Click(sender As Object, e As EventArgs) Handles chartStock.Click

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        frmChart.Show()

    End Sub
End Class
