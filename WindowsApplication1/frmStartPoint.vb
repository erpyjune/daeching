Public Class frmStartPoint

    Dim gHashScreenStock As New Hashtable
    Dim gHashCompanyOnlyBuy As New Hashtable '// 종목별 1포, 2포, 3포 순매수 저장
    Dim gHashCompanyBizInfo As New Hashtable '// 상장주식수, 영업이익, 순이익, 매출
    Dim gHashStockTable As New Hashtable
    Dim gListStartPointHigh As New List(Of StartPointInfo)
    Dim gListStartPointLow As New List(Of StartPointInfo)
    Dim gListStartPointNear As New List(Of StartPointInfo)
    Dim gSendCount As Integer
    Dim gRecvCount As Integer

    Sub requestTROnlyBuy(ByVal stockCode As String, ByVal screenNo As Integer)
        KHOpenAPI.SetInputValue("종목코드", Trim(stockCode))
        KHOpenAPI.SetInputValue("조회구분", "0")
        KHOpenAPI.SetInputValue("시작일자", "20160101")
        KHOpenAPI.SetInputValue("종료일자", "20160722")
        KHOpenAPI.CommRqData("시작점종목별증권사순위", "OPT10038", CInt("0"), CStr(screenNo))
    End Sub
    Sub requestTRStockInfo(ByVal stockCode As String, ByVal screenNo As Integer)
        KHOpenAPI.SetInputValue("종목코드", Trim(stockCode))
        KHOpenAPI.CommRqData("주식기본정보", "OPT10001", CInt("0"), CStr(screenNo))
    End Sub
    Private Sub frmStartPoint_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KHOpenAPI = frmMain.getAPI
        Call printStartPoint()
    End Sub
    Sub printStartPoint()
        Dim nStartPer As Integer, nEndPer As Integer
        Dim hashTodayStartEndValue As New Hashtable
        Dim hashPrintedStock As New Hashtable
        Dim stockVInfo As StockValueInfo
        Dim stockValueInfo As New StockValueInfo
        Dim maxStockValueInfo As New StockValueInfo
        Dim stockTodayValue As New StockValueInfo
        Dim stockStartValue As New StockValueInfo
        Dim startPointInfo As StartPointInfo
        Dim listStockValueInfo As New List(Of StockValueInfo)()
        Dim listSijackStockValueInfo As New List(Of StockValueInfo)()
        Dim gHashSijaMainKeys As ICollection
        Dim nMaxTV As Integer


        '/////////////////////////////////////////////////////////////////////////////////////////////
        '// 시작점 찾기
        '/////////////////////////////////////////////////////////////////////////////////////////////

        gListStartPointHigh.Clear() '// 상향 돌파
        gListStartPointLow.Clear() '// 하향 돌파
        gListStartPointNear.Clear() '// 근처

        '// progress bar를 위해서 미리 처리할 개수를 count 한다.
        Dim nProgressCount As Integer = 0
        gHashSijaMainKeys = frmMain.getHashSijaMain().Keys
        For Each key In gHashSijaMainKeys
            listStockValueInfo = frmMain.getHashSijaMainData(key.ToString)
            For Each stockValueInfo In listStockValueInfo
                nProgressCount += 1
            Next
        Next

        '// progress bar 초기화
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = nProgressCount
        Dim nProgressValue As Integer = 0

        '// 시작점 찾기
        gHashSijaMainKeys = frmMain.getHashSijaMain().Keys
        For Each key In gHashSijaMainKeys
            listStockValueInfo = frmMain.getHashSijaMainData(key.ToString)
            nMaxTV = 0
            For Each stockValueInfo In listStockValueInfo
                If nMaxTV < stockValueInfo.getTradeV Then
                    maxStockValueInfo.setStockValue(stockValueInfo.getCurDate, stockValueInfo.getStartV, stockValueInfo.getEndV, stockValueInfo.getTradeV, stockValueInfo.getName, "")
                    nMaxTV = stockValueInfo.getTradeV
                End If

                '// 오늘 주가 정보 저장 - 오늘 시작점 위치인것 찾을때 사용
                If "20160725" = Trim(stockValueInfo.getCurDate) Then
                    stockVInfo = New StockValueInfo
                    stockVInfo.setStockValue(stockValueInfo.getCurDate, stockValueInfo.getStartV, stockValueInfo.getEndV, stockValueInfo.getTradeV, stockValueInfo.getName, "")
                    hashTodayStartEndValue.Add(stockValueInfo.getName, stockVInfo)
                End If

                '// progress bar add
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                Application.DoEvents()
                'Console.WriteLine("{0},{1},{2},{3},{4}", key.ToString, stockValueInfo.getCurDate, stockValueInfo.getTradeV, stockValueInfo.getStartV, stockValueInfo.getEndV)
            Next
            '// 찾은 시작점 list에 추가.
            stockVInfo = New StockValueInfo
            stockVInfo.setStockValue(maxStockValueInfo.getCurDate, maxStockValueInfo.getStartV, maxStockValueInfo.getEndV, maxStockValueInfo.getTradeV, maxStockValueInfo.getName, "")
            listSijackStockValueInfo.Add(stockVInfo)
        Next

        Console.WriteLine("===================== 종목별 시작점 날짜 =======================")
        Console.WriteLine("================================================================")
        Console.WriteLine("================================================================")


        '// 동일 종목을 계산하지 않음.
        hashPrintedStock.Clear()

        '////////////////////////////////////////////////////////////////////
        '// 시작점 근처 종목 프린트
        '////////////////////////////////////////////////////////////////////
        If listSijackStockValueInfo.Count = 0 Then
            MsgBox("시작점 데이터를 찾지 못했습니다.")
            Return
        End If

        '// progress bar 초기화
        nProgressValue = 0
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = listSijackStockValueInfo.Count

        Dim nPrintCount As Integer = 0
        Dim sName As String
        Do While nPrintCount < 3
            For Each stockStartValue In listSijackStockValueInfo
                sName = stockStartValue.getName
                stockTodayValue = hashTodayStartEndValue(sName)

                nStartPer = 200
                nEndPer = 200

                If stockStartValue.getStartV >= stockTodayValue.getEndV Then
                    nStartPer = stockStartValue.getStartV / stockTodayValue.getEndV * 100
                ElseIf stockStartValue.getStartV < stockTodayValue.getEndV Then
                    nStartPer = stockTodayValue.getEndV / stockStartValue.getStartV * 100
                End If

                If stockStartValue.getEndV >= stockTodayValue.getEndV Then
                    nEndPer = stockStartValue.getEndV / stockTodayValue.getEndV * 100
                ElseIf stockStartValue.getEndV < stockTodayValue.getEndV Then
                    nEndPer = stockTodayValue.getEndV / stockStartValue.getEndV * 100
                End If

                If nStartPer <= 101 And nPrintCount = 1 Then
                    If hashPrintedStock.Contains(stockStartValue.getName) = False Then
                        Console.WriteLine("근처 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)

                        hashPrintedStock.Add(stockStartValue.getName, 1)

                        startPointInfo = New StartPointInfo
                        startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                               stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
                        gListStartPointNear.Add(startPointInfo)

                    End If

                End If

                If nEndPer <= 101 And nPrintCount = 1 Then
                    If hashPrintedStock.Contains(stockStartValue.getName) = False Then
                        Console.WriteLine("근처 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)

                        hashPrintedStock.Add(stockStartValue.getName, 1)

                        startPointInfo = New StartPointInfo
                        startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                               stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
                        gListStartPointNear.Add(startPointInfo)
                    End If
                End If

                '/////////////////////////////////////////////////////////
                '// 오늘 시작, 종가가 시작가를 상향돌파
                If stockTodayValue.getStartV < stockTodayValue.getEndV And nPrintCount = 0 Then
                    If stockStartValue.getStartV >= stockTodayValue.getStartV And stockStartValue.getStartV <= stockTodayValue.getEndV Then
                        If hashPrintedStock.Contains(stockStartValue.getName) = False Then
                            Console.WriteLine("시작점 돌파 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)

                            hashPrintedStock.Add(stockStartValue.getName, 1)

                            startPointInfo = New StartPointInfo
                            startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                                   stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
                            gListStartPointHigh.Add(startPointInfo)
                        End If
                    End If

                    If stockStartValue.getEndV >= stockTodayValue.getStartV And stockStartValue.getEndV <= stockTodayValue.getEndV Then
                        If hashPrintedStock.Contains(stockStartValue.getName) = False Then
                            Console.WriteLine("시작점 돌파 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)

                            hashPrintedStock.Add(stockStartValue.getName, 1)

                            startPointInfo = New StartPointInfo
                            startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                                   stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
                            gListStartPointHigh.Add(startPointInfo)
                        End If
                    End If

                End If

                '/////////////////////////////////////////////////////////
                '// 오늘 시작, 종가가 시작가를 하향돌파
                If stockTodayValue.getEndV < stockTodayValue.getStartV And nPrintCount = 2 Then
                    If stockStartValue.getStartV <= stockTodayValue.getStartV And stockStartValue.getStartV >= stockTodayValue.getEndV Then
                        If hashPrintedStock.Contains(stockStartValue.getName) = False Then
                            Console.WriteLine("시작점 하향돌파 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                            hashPrintedStock.Add(stockStartValue.getName, 1)
                            startPointInfo = New StartPointInfo
                            startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                                   stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
                            gListStartPointLow.Add(startPointInfo)
                        End If
                    End If

                    If stockStartValue.getEndV <= stockTodayValue.getStartV And stockStartValue.getEndV >= stockTodayValue.getEndV Then
                        If hashPrintedStock.Contains(stockStartValue.getName) = False Then
                            Console.WriteLine("시작점 하향돌파 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                            hashPrintedStock.Add(stockStartValue.getName, 1)
                            startPointInfo = New StartPointInfo
                            startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                                   stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
                            gListStartPointLow.Add(startPointInfo)
                        End If
                    End If

                End If

            Next

            nPrintCount += 1
            nProgressValue += 1
            ProgressBar1.Value = nProgressValue

        Loop


        '/////////////////////////////////////////////////////////////////////////////////////////////
        '// 종목별회원사 순매수 가져오기
        '/////////////////////////////////////////////////////////////////////////////////////////////
        gSendCount = 0
        gRecvCount = 0
        gHashCompanyOnlyBuy.Clear()
        gHashScreenStock.Clear()

        '// 시작점 상향 돌파 
        Dim nScreenNo As Integer = 7000
        Dim st As New StartPointInfo
        If gListStartPointHigh.Count > 0 Then
            For Each st In gListStartPointHigh
                Console.WriteLine("상향돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(300)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
            Next
        End If

        '// 시작점 근처
        If gListStartPointNear.Count > 0 Then
            For Each st In gListStartPointNear
                Console.WriteLine("근처 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(300)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
            Next
        End If

        '// 시작점 하향 돌파
        If gListStartPointLow.Count > 0 Then
            For Each st In gListStartPointLow
                Console.WriteLine("하향돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(300)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
            Next
        End If

        '// 주포1,2,3 데이터 다 받을때까지 대기.
        Dim totalRetryCount As Integer = 0
        Do While True
            If gSendCount <= gRecvCount Then
                Exit Do
            End If
            Threading.Thread.Sleep(300)
            Console.WriteLine("Wait OnRecvTRdata...S[" + CStr(gSendCount) + "], R[" + CStr(gRecvCount) + "]")
            Application.DoEvents()
            totalRetryCount += 1
            If totalRetryCount >= 100 Then
                MsgBox("데이터를 서버로 부터 모두 받지 못했습니다. 다시한번 실행해 주세요!")
                Return
            End If
        Loop


        '/////////////////////////////////////////////////////////////////////////////////////////////
        '// 영업이익, 순이익 가져오기
        '/////////////////////////////////////////////////////////////////////////////////////////////
        gSendCount = 0
        gRecvCount = 0
        nScreenNo = 7000
        gHashScreenStock.Clear()
        gHashCompanyBizInfo.Clear()

        '// 시작점 상향 돌파
        If gListStartPointHigh.Count > 0 Then
            For Each st In gListStartPointHigh
                Console.WriteLine("상향돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(300)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
            Next
        End If

        '// 시작점 근처
        If gListStartPointNear.Count > 0 Then
            For Each st In gListStartPointNear
                Console.WriteLine("근처 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(300)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
            Next
        End If

        '// 시작점 하향 돌파
        If gListStartPointLow.Count > 0 Then
            For Each st In gListStartPointLow
                Console.WriteLine("하향돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(300)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
            Next
        End If

        '// 종목별 영업이익, 순이익 가져올때까지 대기 
        totalRetryCount = 0
        Do While True
            If gSendCount <= gRecvCount Then
                Exit Do
            End If
            Threading.Thread.Sleep(300)
            Console.WriteLine("Wait OnRecvTRdata...S[" + CStr(gSendCount) + "], R[" + CStr(gRecvCount) + "]")
            Application.DoEvents()
            totalRetryCount += 1
            If totalRetryCount >= 100 Then
                MsgBox("데이터를 서버로 부터 모두 받지 못했습니다. 다시한번 실행해 주세요!")
                Return
            End If
        Loop


        '// 리스트뷰에 채운다.
        Call printListView()

    End Sub
    Sub printListView()

        '// setting listView
        Dim item As ListViewItem
        lstViewStartPoint.Items.Clear()
        lstViewStartPoint.Columns(0).TextAlign = HorizontalAlignment.Center
        lstViewStartPoint.Columns(1).TextAlign = HorizontalAlignment.Center
        lstViewStartPoint.Columns(2).TextAlign = HorizontalAlignment.Center
        lstViewStartPoint.Columns(3).TextAlign = HorizontalAlignment.Left   '// 주식 상태
        lstViewStartPoint.Columns(4).TextAlign = HorizontalAlignment.Right  '// 순매수 주포1
        lstViewStartPoint.Columns(5).TextAlign = HorizontalAlignment.Right  '// 순매수 주포2
        lstViewStartPoint.Columns(6).TextAlign = HorizontalAlignment.Right  '// 순매수 주포3
        lstViewStartPoint.Columns(7).TextAlign = HorizontalAlignment.Right     '// 상장주식수
        lstViewStartPoint.Columns(8).TextAlign = HorizontalAlignment.Right     '// 시가총액
        lstViewStartPoint.Columns(9).TextAlign = HorizontalAlignment.Right     '// 영업이익
        lstViewStartPoint.Columns(10).TextAlign = HorizontalAlignment.Right    '// 당기순이익

        '// 시작점 상향 돌파 
        Dim nScreenNo As Integer = 0
        Dim st As New StartPointInfo
        Dim stOnlyBuy As New StartPointInfo
        Dim stStockBiz As New StockBizInfo

        If gListStartPointHigh.Count > 0 Then
            For Each st In gListStartPointHigh
                Console.WriteLine("상향돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                item = New ListViewItem(CStr(st.getName))
                item.SubItems.Add(CStr(st.getDate))
                item.SubItems.Add("상향돌파")
                item.UseItemStyleForSubItems = False
                item.SubItems(2).ForeColor = Color.Blue
                item.SubItems.Add(frmMain.getStockStatus(Trim(st.getName)))
                '// 주포1, 2, 3 순매수
                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                item.SubItems.Add(CStr(stOnlyBuy.getCompany1))
                item.SubItems.Add(CStr(stOnlyBuy.getCompany2))
                item.SubItems.Add(CStr(stOnlyBuy.getCompany3))
                '// 상장주식수, 시가총액, 영업이익, 당기순이익
                stStockBiz = gHashCompanyBizInfo(st.getName)
                item.SubItems.Add(CStr(stStockBiz.getStockTotalCount))
                item.SubItems.Add(CStr(stStockBiz.getCompayTotalValue))
                item.SubItems.Add(CStr(stStockBiz.getSaleProfitValue))
                If stStockBiz.getSaleProfitValue > 0 Then
                    item.UseItemStyleForSubItems = False
                    item.SubItems(9).ForeColor = Color.Blue
                Else
                    item.UseItemStyleForSubItems = False
                    item.SubItems(9).ForeColor = Color.Red
                End If

                item.SubItems.Add(CStr(stStockBiz.getSaleNetProfitValue))
                If stStockBiz.getSaleNetProfitValue > 0 Then
                    item.UseItemStyleForSubItems = False
                    item.SubItems(10).ForeColor = Color.Blue
                Else
                    item.UseItemStyleForSubItems = False
                    item.SubItems(10).ForeColor = Color.Red
                End If

                lstViewStartPoint.Items.Add(item)
            Next
        End If

        '// 시작점 근처
        If gListStartPointNear.Count > 0 Then
            For Each st In gListStartPointNear
                Console.WriteLine("근처 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                item = New ListViewItem(CStr(st.getName))
                item.SubItems.Add(CStr(st.getDate))
                item.SubItems.Add("근접")
                item.SubItems.Add(frmMain.getStockStatus(Trim(st.getName)))
                '// 주포1, 2, 3 순매수
                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                item.SubItems.Add(CStr(stOnlyBuy.getCompany1))
                item.SubItems.Add(CStr(stOnlyBuy.getCompany2))
                item.SubItems.Add(CStr(stOnlyBuy.getCompany3))
                '// 상장주식수, 시가총액, 영업이익, 당기순이익
                stStockBiz = gHashCompanyBizInfo(st.getName)
                item.SubItems.Add(CStr(stStockBiz.getStockTotalCount))
                item.SubItems.Add(CStr(stStockBiz.getCompayTotalValue))
                item.SubItems.Add(CStr(stStockBiz.getSaleProfitValue))
                If stStockBiz.getSaleProfitValue > 0 Then
                    item.UseItemStyleForSubItems = False
                    item.SubItems(9).ForeColor = Color.Blue
                Else
                    item.UseItemStyleForSubItems = False
                    item.SubItems(9).ForeColor = Color.Red
                End If

                item.SubItems.Add(CStr(stStockBiz.getSaleNetProfitValue))
                If stStockBiz.getSaleNetProfitValue > 0 Then
                    item.UseItemStyleForSubItems = False
                    item.SubItems(10).ForeColor = Color.Blue
                Else
                    item.UseItemStyleForSubItems = False
                    item.SubItems(10).ForeColor = Color.Red
                End If

                lstViewStartPoint.Items.Add(item)
            Next
        End If

        '// 시작점 하향 돌파
        If gListStartPointLow.Count > 0 Then
            For Each st In gListStartPointLow
                Console.WriteLine("하향돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                item = New ListViewItem(CStr(st.getName))
                item.SubItems.Add(CStr(st.getDate))
                item.SubItems.Add("하향돌파")
                item.UseItemStyleForSubItems = False
                item.SubItems(2).ForeColor = Color.Red
                item.SubItems.Add(frmMain.getStockStatus(Trim(st.getName)))
                '// 주포1, 2, 3 순매수
                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                item.SubItems.Add(CStr(stOnlyBuy.getCompany1))
                item.SubItems.Add(CStr(stOnlyBuy.getCompany2))
                item.SubItems.Add(CStr(stOnlyBuy.getCompany3))
                '// 상장주식수, 시가총액, 영업이익, 당기순이익
                stStockBiz = gHashCompanyBizInfo(st.getName)
                item.SubItems.Add(CStr(stStockBiz.getStockTotalCount))
                item.SubItems.Add(CStr(stStockBiz.getCompayTotalValue))
                item.SubItems.Add(CStr(stStockBiz.getSaleProfitValue))
                If stStockBiz.getSaleProfitValue > 0 Then
                    item.UseItemStyleForSubItems = False
                    item.SubItems(9).ForeColor = Color.Blue
                Else
                    item.UseItemStyleForSubItems = False
                    item.SubItems(9).ForeColor = Color.Red
                End If

                item.SubItems.Add(CStr(stStockBiz.getSaleNetProfitValue))
                If stStockBiz.getSaleNetProfitValue > 0 Then
                    item.UseItemStyleForSubItems = False
                    item.SubItems(10).ForeColor = Color.Blue
                Else
                    item.UseItemStyleForSubItems = False
                    item.SubItems(10).ForeColor = Color.Red
                End If

                lstViewStartPoint.Items.Add(item)
            Next
        End If

    End Sub
    Sub trProcStockInfo(sender As Object, e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim nCnt As Short, i As Short
        Dim strItemValue As String, sStockName As String
        Dim stockBiz As StockBizInfo
        Dim nStockTotalCount As Integer = 0, nCompanyTotalValue As Integer = 0
        Dim nSaleProfitValue As Integer = 0, nSaleNetProfitValue As Integer = 0

        sStockName = gHashScreenStock(CStr(e.sScrNo))
        gHashScreenStock.Remove(e.sScrNo)

        nCnt = KHOpenAPI.GetRepeatCnt(e.sTrCode, e.sRQName)
        For i = 0 To (nCnt - 1)
            'strItemValue = Trim(KHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "종목코드"))
            'Console.WriteLine("종목코드 : " + strItemValue)

            'strItemValue = Trim(KHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "종목명"))
            'Console.WriteLine("종목명 : " + strItemValue)

            strItemValue = Trim(KHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "상장주식"))
            Console.WriteLine("상장주식 : " + strItemValue)
            If strItemValue.Length = 0 Then
                strItemValue = "0"
            End If
            nStockTotalCount = CInt(strItemValue)

            strItemValue = Trim(KHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "시가총액"))
            Console.WriteLine("시가총액 : " + strItemValue)
            If strItemValue.Length = 0 Then
                strItemValue = "0"
            End If
            nCompanyTotalValue = CInt(strItemValue)

            strItemValue = Trim(KHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "영업이익"))
            Console.WriteLine("영업이익 : " + strItemValue)
            If strItemValue.Length = 0 Then
                strItemValue = "0"
            End If
            nSaleProfitValue = CInt(strItemValue)

            strItemValue = Trim(KHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "당기순이익"))
            Console.WriteLine("당기순이익 : " + strItemValue)
            If strItemValue.Length = 0 Then
                strItemValue = "0"
            End If
            nSaleNetProfitValue = CInt(strItemValue)
        Next

        stockBiz = New StockBizInfo
        stockBiz.setData(nStockTotalCount, nCompanyTotalValue, nSaleProfitValue, nSaleNetProfitValue)
        Console.WriteLine("{0}. {1}, {2}, {3}, {4}", _
                          sStockName, nStockTotalCount, nCompanyTotalValue, nSaleProfitValue, nSaleNetProfitValue)
        gHashCompanyBizInfo.Add(Trim(sStockName), stockBiz)
        gRecvCount += 1

    End Sub


    Sub trProcCompanyOnlyBuy(sender As Object, e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim nCnt As Integer, nIndex As Integer = 0
        Dim nCmp1 As Integer, nCmp2 As Integer, nCmp3 As Integer
        Dim strItemValue As String
        Dim sStockName As String
        Dim startPointInfo As StartPointInfo

        sStockName = gHashScreenStock(CStr(e.sScrNo))
        gHashScreenStock.Remove(e.sScrNo)

        nCnt = KHOpenAPI.GetRepeatCnt(e.sTrCode, e.sRQName)
        For i = 0 To (nCnt - 1)
            'strItemValue = KHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "회원사명")
            strItemValue = KHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "누적순매수수량")

            If nIndex = 0 Then
                nCmp1 = CInt(Trim(strItemValue))
            End If

            If nIndex = 1 Then
                nCmp2 = CInt(Trim(strItemValue))
            End If

            If nIndex = 2 Then
                nCmp3 = CInt(Trim(strItemValue))
            End If

            nIndex += 1
            If nIndex >= 3 Then
                Exit For
            End If
        Next

        startPointInfo = New StartPointInfo
        Console.WriteLine("{0}. {1}, {2}, {3}", sStockName, nCmp1, nCmp2, nCmp3)
        startPointInfo.setData(sStockName, "", "", "", nCmp1, nCmp2, nCmp3)
        gHashCompanyOnlyBuy.Add(Trim(sStockName), startPointInfo)
        gRecvCount += 1

    End Sub
    Private Sub KHOpenAPI_OnReceiveTrData(sender As Object, e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent) Handles KHOpenAPI.OnReceiveTrData
        If e.sRQName = "시작점종목별증권사순위" Then
            Call trProcCompanyOnlyBuy(sender, e)
            KHOpenAPI.DisconnectRealData(e.sScrNo)
            Console.WriteLine("DisconnectRealData SrcNumber : " + e.sScrNo)
        ElseIf e.sRQName = "주식기본정보" Then
            Call trProcStockInfo(sender, e)
            KHOpenAPI.DisconnectRealData(e.sScrNo)
            Console.WriteLine("DisconnectRealData SrcNumber : " + e.sScrNo)
        End If
    End Sub

    Private Sub lstViewStartPoint_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstViewStartPoint.SelectedIndexChanged

    End Sub
End Class