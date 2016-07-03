Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Windows.Forms.DataVisualization.Charting.Chart


Public Class frmMain

    Dim gStrDate As String
    Dim gStore As StoreClass
    Dim lRefreshCount As Long = 0
    Dim bLoginStatus As Boolean
    Dim bOnRecvTRdata As Boolean
    Dim StockTable As New Hashtable()
    Dim gStockCompData As New Hashtable()
    Dim gHashScreenAndDate As New Hashtable()
    Dim gHashStockCompany As New Hashtable()
    Dim gSendCommandCount As Integer
    Dim gRecvCommandCount As Integer
    Dim gStockSellBuyInfoSub, gBefStockSellBuyInfoSub As StockSellBuyInfoSub
    Dim gListStockSellBuyInfoSub As New List(Of StockSellBuyInfoSub)()
    Dim gListStockSellBuyInfoMain As New List(Of StockSellBuyInfoMain)()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        gStore = New StoreClass
        gBefStockSellBuyInfoSub = New StockSellBuyInfoSub

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

    End Sub
    Private Sub trProcStockStandard(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim item As ListViewItem
        Dim sDate As String
        Dim stockSellBuyInfoMain As StockSellBuyInfoMain

        Dim nCnt As Short, i As Short
        Dim strItemValue As String
        Dim sCompany As String
        Dim tCompany As String
        Dim lOnlyBuy, lBuy, lSell As Long

        '// class 하나 셋팅
        stockSellBuyInfoMain = New StockSellBuyInfoMain

        nCnt = KHOpenAPI.GetRepeatCnt(eventArgs.sTrCode, eventArgs.sRQName)

        For i = 0 To (nCnt - 1)

            If eventArgs.sRQName = "종목별증권사순위요청기간1" Then
                item = New ListViewItem(Trim(txtStartDate1.Text))
            ElseIf eventArgs.sRQName = "종목별증권사순위요청기간2" Then
                item = New ListViewItem(Trim(txtStartDate2.Text))
            ElseIf eventArgs.sRQName = "종목별증권사순위요청기간3" Then
                item = New ListViewItem(Trim(txtStartDate3.Text))
            End If

            'strItemValue = KHOpenAPI1.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "순위")
            'ListResult.Items.Add("순위:" + strItemValue)

            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "회원사명")
            sCompany = Trim(strItemValue).Replace(" ", "")
            tCompany = ""
            tCompany = gHashStockCompany(sCompany)

            If tCompany = Nothing Then
                gHashStockCompany.Add(sCompany, sCompany)
            End If

            '// 증권사
            item.SubItems.Add(sCompany)

            '// 누적순매수
            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "누적순매수수량")
            lOnlyBuy = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))
            item.SubItems.Add(Trim(strItemValue).Replace("+-", "-"))
            If lOnlyBuy > 0 Then
                item.UseItemStyleForSubItems = False
                item.SubItems(2).ForeColor = Color.Blue
                'item.ForeColor = Color.Blue
            Else
                item.UseItemStyleForSubItems = False
                item.SubItems(2).ForeColor = Color.Red
                'item.ForeColor = Color.Red
            End If

            '// 매수수량
            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "매수수량")
            lBuy = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))
            item.SubItems.Add(Trim(strItemValue).Replace("+-", "-"))
            If lBuy > 0 Then
                item.UseItemStyleForSubItems = False
                item.SubItems(3).ForeColor = Color.Blue
                'item.ForeColor = Color.Blue
            Else
                item.UseItemStyleForSubItems = False
                item.SubItems(3).ForeColor = Color.Red
                'item.ForeColor = Color.Red
            End If

            '// 매도수량
            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "매도수량")
            lSell = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))
            item.SubItems.Add(Trim(strItemValue).Replace("+-", "-"))
            If lSell > 0 Then
                item.UseItemStyleForSubItems = False
                item.SubItems(4).ForeColor = Color.Blue
                'item.ForeColor = Color.Blue
            Else
                item.UseItemStyleForSubItems = False
                item.SubItems(4).ForeColor = Color.Red
                'item.ForeColor = Color.Red
            End If

            If eventArgs.sRQName = "종목별증권사순위요청기간1" Then
                lstView1.Items.Add(item)
            ElseIf eventArgs.sRQName = "종목별증권사순위요청기간2" Then
                lstView2.Items.Add(item)
            ElseIf eventArgs.sRQName = "종목별증권사순위요청기간3" Then
                lstView3.Items.Add(item)
            End If

            '// class에 값 셋팅
            gStockSellBuyInfoSub = New StockSellBuyInfoSub
            gStockSellBuyInfoSub.setData(sCompany, lOnlyBuy, lSell, lBuy)
            stockSellBuyInfoMain.addStockSellBuyInfo(gStockSellBuyInfoSub)
        Next

        '// listView 셋팅
        lstView1.Columns(0).TextAlign = HorizontalAlignment.Center
        lstView1.Columns(1).TextAlign = HorizontalAlignment.Center
        lstView1.Columns(2).TextAlign = HorizontalAlignment.Center

        sDate = gHashScreenAndDate(eventArgs.sScrNo)
        stockSellBuyInfoMain.setCurrDate(sDate)
        gListStockSellBuyInfoMain.Add(stockSellBuyInfoMain)
        gRecvCommandCount = gRecvCommandCount + 1
        '// 처리후에 화면번호와 매핑한 날짜 데이터는 삭제한다.
        '// 혹시 동일한 화면번호 다른날짜와 겹칠까봐 등록후에 제거한다.
        gHashScreenAndDate.Remove(eventArgs.sScrNo)

        System.Console.WriteLine("RecvCommandCount : " + CStr(gRecvCommandCount) + " | sScrNo : " + eventArgs.sScrNo + " | 결과개수 : " + CStr(nCnt))

        '// 그래프를 그린다
        Call drowChartStockSellBuy()

    End Sub
    Private Sub trProcStock(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim nCnt As Integer
        Dim item As ListViewItem
        Dim lOnlyBuy As Long, lSell As Long, lBuy As Long
        Dim strItemValue As String, sCompany As String
        Dim tStockSellBuyInfoSub As StockSellBuyInfoSub
        '// Dim gListStockSellBuyInfoSub As New List(Of StockSellBuyInfoSub)()

        '// List 초기화
        gListStockSellBuyInfoSub.Clear()

        '// ListView 초기화
        lstView1.Items.Clear()

        '// listView 셋팅
        lstView1.Columns(0).TextAlign = HorizontalAlignment.Center
        lstView1.Columns(1).TextAlign = HorizontalAlignment.Center
        lstView1.Columns(2).TextAlign = HorizontalAlignment.Center

        nCnt = KHOpenAPI.GetRepeatCnt(eventArgs.sTrCode, eventArgs.sRQName)
        For i = 0 To (nCnt - 1)
            'strItemValue = KHOpenAPI1.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "순위")
            'ListResult.Items.Add("순위:" + strItemValue)

            '// List view item setting.
            item = New ListViewItem(Trim(txtAnalStartDate.Text))

            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "회원사명")
            item.SubItems.Add(Trim(strItemValue))
            sCompany = Trim(strItemValue).Replace(" ", "")
            System.Console.WriteLine("회원사 : " + sCompany)

            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "누적순매수수량")
            item.SubItems.Add(Trim(strItemValue).Replace("+-", "-"))
            lOnlyBuy = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))
            If lOnlyBuy > 0 Then
                item.UseItemStyleForSubItems = False
                item.SubItems(2).ForeColor = Color.Blue
                'item.ForeColor = Color.Blue
            Else
                item.UseItemStyleForSubItems = False
                item.SubItems(2).ForeColor = Color.Red
                'item.ForeColor = Color.Red
            End If

            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "매수수량")
            item.SubItems.Add(Trim(strItemValue).Replace("+-", "-"))
            lBuy = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))
            If lBuy > 0 Then
                item.UseItemStyleForSubItems = False
                item.SubItems(3).ForeColor = Color.Blue
                'item.ForeColor = Color.Blue
            Else
                item.UseItemStyleForSubItems = False
                item.SubItems(3).ForeColor = Color.Red
                'item.ForeColor = Color.Red
            End If

            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "매도수량")
            item.SubItems.Add(Trim(strItemValue).Replace("+-", "-"))
            lSell = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))
            If lSell > 0 Then
                item.UseItemStyleForSubItems = False
                item.SubItems(4).ForeColor = Color.Blue
                'item.ForeColor = Color.Blue
            Else
                item.UseItemStyleForSubItems = False
                item.SubItems(4).ForeColor = Color.Red
                'item.ForeColor = Color.Red
            End If

            tStockSellBuyInfoSub = New StockSellBuyInfoSub
            tStockSellBuyInfoSub.setData(sCompany, lOnlyBuy, lSell, lBuy)
            gListStockSellBuyInfoSub.Add(tStockSellBuyInfoSub)

            '// List view add item.
            lstView1.Items.Add(item)

        Next

        Call drowChartStockSellBuy()

    End Sub
    Private Sub drowChartStockSellBuy()

        '/// Series를 초기화 한다.
        Me.chartStock.Series.Clear()
        Me.chartStock.ResetAutoValues()
        Me.chartStock.ResetText()

        '/// Series를 등록. chart type 설정.
        For Each stockSellBuySub In gListStockSellBuyInfoSub
            Me.chartStock.Series.Add(stockSellBuySub.getName)
            Me.chartStock.Series(stockSellBuySub.getName).ChartType = DataVisualization.Charting.SeriesChartType.Bar
            Me.chartStock.Series(stockSellBuySub.getName).IsValueShownAsLabel = True
            Me.chartStock.Series(stockSellBuySub.getName)("PointWidth") = "1.8"
            Me.chartStock.Series(stockSellBuySub.getName).ToolTip = " #VALX | #VALY"
            'Me.chartStock.Series(stockSellBuySub.getName).ToolTip = " #AXISLABEL | #VALY"

            '// "#VALX : #VALY
            '// Chart1.Series("Series2")("PointWidth") = "0.1"
            'Me.chartStock.Series(stockSellBuySub.getName).BorderWidth = 10
            'chart1.Series["Series1"]["PixelPointWidth"] = "1";
            'Me.chartStock.Series(stockSellBuySub.getName).font = New Font("Times", 8)
        Next

        '//// 차트를 그린다.
        For Each stockSellBuySub In gListStockSellBuyInfoSub
            Me.chartStock.Series(stockSellBuySub.getName).Points.AddXY(stockSellBuySub.getName, stockSellBuySub.getOnlyBuyCount)
        Next


        ''// 증권사 삭제
        'For Each stockSellBuySub In gListStockSellBuyInfoSub
        '    Me.chartStock.Series.(stockSellBuySub.getName)
        'Next

        ''// 증권사 추가
        'For Each stockSellBuySub In gListStockSellBuyInfoSub
        '    Me.chartStock.Series.Add(stockSellBuySub.getName)
        'Next

        ''// 매수/매도량 추가
        'For Each stockSellBuySub In gListStockSellBuyInfoSub
        '    Me.chartStock.Series(stockSellBuySub.getName).chartType = DataVisualization.Charting.SeriesChartType.Bar
        '    Me.chartStock.Series(stockSellBuySub.getName).Points.AddXY(stockSellBuySub.getName, stockSellBuySub.getOnlyBuyCount)
        'Next

    End Sub
    Private Sub KHOpenAPI_OnReceiveTrData(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent) Handles KHOpenAPI.OnReceiveTrData

        If eventArgs.sRQName = "종목별증권사순위요청기간1" Or eventArgs.sRQName = "종목별증권사순위요청기간2" Or eventArgs.sRQName = "종목별증권사순위요청기간3" Then
            Call trProcStockStandard(sender, eventArgs)
        ElseIf eventArgs.sRQName = "종목별증권사순위요청기간단순" Then
            Call trProcStock(sender, eventArgs)
        End If

        bOnRecvTRdata = True

    End Sub

    Private Sub btnCmd1_Click(sender As Object, e As EventArgs) Handles btnCmd1.Click
        KHOpenAPI.SetInputValue("종목코드", Trim(txtStockCode.Text))
        KHOpenAPI.SetInputValue("수정주가구분", "1")
        '/// 조회구분 = 0: 입력한 시작,종료 날짜로 조회
        '///            1: 전일, 조회지정일 입력(5일 ~ 120일)
        KHOpenAPI.SetInputValue("조회구분", "0")
        KHOpenAPI.SetInputValue("시작일자", Trim(txtStartDate1.Text))
        KHOpenAPI.SetInputValue("종료일자", Trim(txtEndDate1.Text))
        'KHOpenAPI.SetInputValue("기간", "1")

        lstView1.Items.Clear()

        KHOpenAPI.CommRqData("종목별증권사순위요청기간1", "OPT10038", CInt("0"), "4000")
    End Sub

    Private Sub btnCmd2_Click(sender As Object, e As EventArgs) Handles btnCmd2.Click
        If bLoginStatus = False Then
            MsgBox("로그인이 필요합니다!!")
            Return
        End If

        KHOpenAPI.SetInputValue("종목코드", Trim(txtStockCode.Text))
        KHOpenAPI.SetInputValue("수정주가구분", "1")
        '/// 조회구분 = 0: 입력한 시작,종료 날짜로 조회
        '///            1: 전일, 조회지정일 입력(5일 ~ 120일)
        KHOpenAPI.SetInputValue("조회구분", "0")
        KHOpenAPI.SetInputValue("시작일자", Trim(txtStartDate2.Text))
        KHOpenAPI.SetInputValue("종료일자", Trim(txtEndDate2.Text))
        'KHOpenAPI1.SetInputValue("기간", "")

        lstView2.Items.Clear()

        KHOpenAPI.CommRqData("종목별증권사순위요청기간2", "OPT10038", CInt("0"), "4001")
    End Sub

    Private Sub btnCmd3_Click(sender As Object, e As EventArgs) Handles btnCmd3.Click
        If bLoginStatus = False Then
            MsgBox("로그인이 필요합니다!!")
            Return
        End If

        KHOpenAPI.SetInputValue("종목코드", Trim(txtStockCode.Text))
        KHOpenAPI.SetInputValue("수정주가구분", "1")
        '/// 조회구분 = 0: 입력한 시작,종료 날짜로 조회
        '///            1: 전일, 조회지정일 입력(5일 ~ 120일)
        KHOpenAPI.SetInputValue("조회구분", "0")
        KHOpenAPI.SetInputValue("시작일자", Trim(txtStartDate3.Text))
        KHOpenAPI.SetInputValue("종료일자", Trim(txtEndDate3.Text))
        'KHOpenAPI1.SetInputValue("기간", "")

        lstView3.Items.Clear()

        KHOpenAPI.CommRqData("종목별증권사순위요청기간3", "OPT10038", CInt("0"), "4002")
    End Sub

    Private Sub chartStock_Click(sender As Object, e As EventArgs) Handles chartStock.Click

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        frmChart.Show()
    End Sub

    Private Sub btnAnalBetween_Click(sender As Object, e As EventArgs) Handles btnAnalBetween.Click

        Dim chartSeries As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim nScreenNumber As Integer
        Dim strStartDate As String
        Dim strStartY, strStartM, strStartD As String

        strStartY = txtAnalStartDate.Text.Substring(0, 4)
        strStartM = txtAnalStartDate.Text.Substring(4, 2)
        strStartD = txtAnalStartDate.Text.Substring(6, 2)

        Dim startDate As DateTime = New DateTime(CInt(strStartY), CInt(strStartM), CInt(strStartD))

        gSendCommandCount = 0
        nScreenNumber = 1000
        gStore.setCleaerList()

        Do While True
            strStartDate = startDate.Year.ToString + checkMonthDay(CInt(startDate.Month.ToString)) + checkMonthDay(CInt(startDate.Day.ToString))

            KHOpenAPI.SetInputValue("종목코드", Trim(txtStockCode.Text))
            KHOpenAPI.SetInputValue("수정주가구분", "1")
            KHOpenAPI.SetInputValue("조회구분", "0")
            KHOpenAPI.SetInputValue("시작일자", strStartDate)
            KHOpenAPI.SetInputValue("종료일자", strStartDate)
            lstView1.Items.Clear()
            KHOpenAPI.CommRqData("종목별증권사순위요청기간", "OPT10038", CInt("0"), CStr(nScreenNumber))
            gHashScreenAndDate.Add(CStr(nScreenNumber), strStartDate) '// RecvTRdata에서 날짜를 가져오기 위해 셋팅
            gSendCommandCount = gSendCommandCount + 1

            If nScreenNumber = 1099 Then
                nScreenNumber = 1000
            Else
                nScreenNumber = nScreenNumber + 1
            End If

            System.Console.WriteLine(strStartDate + " | SendCommandCount : " + CStr(gSendCommandCount) + " | screen : " + CStr(nScreenNumber))


            bOnRecvTRdata = False

            'Do While True
            '    If bOnRecvTRdata = True Then
            '        Exit Do
            '    End If
            '    Threading.Thread.Sleep(1000)
            '    System.Console.WriteLine("Retry recvTR data...")
            'Loop

            If strStartDate = Trim(txtAnalEndDate.Text) Then
                System.Console.WriteLine("end program !!")
                Exit Do
            End If
            startDate = startDate.AddDays(+1)
            Threading.Thread.Sleep(300)
        Loop


        Dim stockInfo As StockSellBuyInfoSub
        Dim listStockSellBuyInfoSub As New List(Of StockSellBuyInfoSub)()

        For Each tStockSellBuyInfoMain In gListStockSellBuyInfoMain
            Console.WriteLine("Curr date : " + tStockSellBuyInfoMain.getCurrDate())
            listStockSellBuyInfoSub = tStockSellBuyInfoMain.getStockSellBuyInfo
            For Each s In listStockSellBuyInfoSub
                stockInfo = s
                Console.WriteLine("증권사:" + CStr(s.getName))
                Console.WriteLine("순매수:" + CStr(s.getOnlyBuyCount))
            Next
            Console.WriteLine("============================================")
        Next

        '////////////////////////////////////////////////////////////////////
        Dim keysCompanyKeys As ICollection
        keysCompanyKeys = gHashStockCompany.Keys()
        For Each Key In keysCompanyKeys
            Console.WriteLine("증권사:" + CStr(Key.ToString))
        Next

        '////////////////////////////////////////////////////////////////////
        'Me.chartStock = New System.Windows.Forms.DataVisualization.Charting.Chart()
        'Me.chartStock.Series.Clear()
        'chartSeries.ChartType = DataVisualization.Charting.SeriesChartType.Line
        'Me.chartStock.Series.Add(chartSeries)

        For Each Key In keysCompanyKeys
            Me.chartStock.Series.Add(Key.ToString)
        Next

        For Each tStockSellBuyInfoMain In gListStockSellBuyInfoMain
            listStockSellBuyInfoSub = tStockSellBuyInfoMain.getStockSellBuyInfo
            For Each s In listStockSellBuyInfoSub
                stockInfo = s
                Me.chartStock.Series(s.getName).Points.AddXY(tStockSellBuyInfoMain.getCurrDate(), s.getOnlyBuyCount)
                Me.chartStock.Series(s.getName).chartType = DataVisualization.Charting.SeriesChartType.Line
            Next
            Console.WriteLine("============================================")
        Next


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        'Dim value As DateTime = New DateTime(2014, 2, 1)
        'Dim yesterday As DateTime = DateTime.Today.AddDays(-1)
        'Dim y As DateTime = value.AddDays(-2)
        'MsgBox(y.Year.ToString + y.Month.ToString + y.Day.ToString + " | " + value)

        'Dim strStartY, strStartM, strStartD As String
        'Dim strEndY, strEndM, strEndD As String

        'strStartY = txtAnalStartDate.Text.Substring(0, 4)
        'strStartM = txtAnalStartDate.Text.Substring(4, 2)
        'strStartD = txtAnalStartDate.Text.Substring(6, 2)

        'strEndY = txtAnalEndDate.Text.Substring(0, 4)
        'strEndM = txtAnalEndDate.Text.Substring(4, 2)
        'strEndD = txtAnalEndDate.Text.Substring(6, 2)

        'Dim startDate As DateTime = New DateTime(CInt(strStartY), CInt(strStartM), CInt(strStartD))
        'Dim strStartDate As String

        'Do While True
        '    strStartDate = startDate.Year.ToString + checkMonthDay(CInt(startDate.Month.ToString)) + checkMonthDay(CInt(startDate.Day.ToString))
        '    System.Console.WriteLine(strStartDate)

        '    If strStartDate = Trim(txtAnalEndDate.Text) Then
        '        System.Console.WriteLine("exit do!!")
        '        Exit Do
        '    End If
        '    startDate = startDate.AddDays(+1)
        'Loop

        Dim sTest As String
        sTest = "+-849384"
        If sTest.StartsWith("+-") = True Then

        End If


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If bLoginStatus = False Then
            MsgBox("로그인이 필요합니다!!")
            Return
        End If

        KHOpenAPI.SetInputValue("종목코드", Trim(txtStockCode.Text))
        KHOpenAPI.SetInputValue("수정주가구분", "1")
        '/// 조회구분 = 0: 입력한 시작,종료 날짜로 조회
        '///            1: 전일, 조회지정일 입력(5일 ~ 120일)
        KHOpenAPI.SetInputValue("조회구분", "0")
        KHOpenAPI.SetInputValue("시작일자", Trim(txtAnalStartDate.Text))
        KHOpenAPI.SetInputValue("종료일자", Trim(txtAnalEndDate.Text))
        'KHOpenAPI1.SetInputValue("기간", "")

        'KHOpenAPI.CommRqData("종목별증권사순위요청기간", "OPT10038", CInt("0"), "1001")
        KHOpenAPI.CommRqData("종목별증권사순위요청기간단순", "OPT10038", CInt("0"), "1001")

        If chkRefresh.Checked = True Then
            Timer1.Interval = 10000
            Timer1.Start()
        End If
    End Sub

    Private Sub btnTodayCompany_Click(sender As Object, e As EventArgs) Handles btnTodayCompany.Click
        KHOpenAPI.SetInputValue("종목코드", Trim(txtStockCode.Text))
        KHOpenAPI.CommRqData("당일주요거래원", "opt10040", CInt("0"), "1000")
    End Sub

    Private Sub btnTimer_Click(sender As Object, e As EventArgs) Handles btnTimer.Click
        Timer1.Interval = 10000
        Timer1.Start()
        MsgBox("타이머시작")
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If Me.chkRefresh.Checked = True Then
            Call Button3_Click(sender, e)
            lstMsg.Items.Add(("리프레쉬 [" + CStr(lRefreshCount) + "]"))
            lstMsg.TopIndex = lstMsg.Items.Count - 1
            lRefreshCount = lRefreshCount + 1
        End If

    End Sub
End Class
