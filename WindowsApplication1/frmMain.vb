Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Windows.Forms.DataVisualization.Charting.Chart
'Imports System.String


Public Class frmMain

    Dim gStrDate As String
    Dim gStore As StoreClass
    Dim lRefreshCount As Long = 0
    Dim bLoginStatus As Boolean
    Dim gStockCodeTable As New Hashtable
    Dim gStockCompanyCodeTable As New Hashtable
    Dim gListStockCompanyCode As New List(Of String)()
    Dim gHashScreenAndDate As New Hashtable
    Dim gHashStockCompany As New Hashtable
    Dim gCompanySellBuyCount As New Hashtable
    Dim gPrintChartCompanySeries As New Hashtable
    Dim gSendCommandCount As Integer
    Dim gRecvCommandCount As Integer
    Dim gStockSellBuyInfoSub, gBefStockSellBuyInfoSub As StockSellBuyInfoSub
    Dim gListStockSellBuyInfoSub As New List(Of StockSellBuyInfoSub)()
    Dim gListStockSellBuyInfoMain As New List(Of StockSellBuyInfoMain)()
    Dim gSortedListStockSellBuyInfo As SortedList = New SortedList

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        MsgBox(Application.ExecutablePath)

        '// main 초기화
        Call mainInitialize()
        '// 종목 코드 로딩.
        Call loadingStockCodeData()
        '// 회원사 코드 로딩
        Call loadingStockCompanyCodeData()

    End Sub
    Private Sub mainInitialize()
        gStore = New StoreClass
        gBefStockSellBuyInfoSub = New StockSellBuyInfoSub

        txtEndDate1.Text = Format(Now, "yyyyMMdd")
        txtAnalEndDate.Text = Format(Now, "yyyyMMdd")

    End Sub

    Private Sub loadingStockCompanyCodeData()
        Dim strCodeFilePath As String = "C:\Temp\_sotck_company_code.txt"
        Dim TArr() As String
        Dim strCode As String, strName As String
        Dim fileReader As System.IO.StreamReader
        Dim stringReader As String

        Try
            fileReader = My.Computer.FileSystem.OpenTextFileReader(strCodeFilePath)
            While Not fileReader.EndOfStream
                stringReader = ""
                stringReader = fileReader.ReadLine()
                If Trim(stringReader) = "" Then
                    Exit While
                End If

                TArr = Split(Trim(stringReader), "|")
                For i = LBound(TArr) To UBound(TArr)
                    If i = 0 Then
                        strCode = Trim(TArr(i))
                    ElseIf i = 1 Then
                        strName = Trim(TArr(i))
                        gStockCompanyCodeTable.Add(strName, strCode)
                        gListStockCompanyCode.Add(strName)
                        System.Console.WriteLine(strName)
                    End If
                Next
            End While
        Catch ex As System.IO.IOException
            MsgBox(strCodeFilePath + " 회원사별 종목코드 파일을 찾을 수 없습니다.")
            Return
        End Try

    End Sub
    Private Sub loadingStockCodeData()
        Dim strCodeFilePath As String
        Dim TArr() As String
        Dim strCode As String, strName As String
        Dim HSource As New AutoCompleteStringCollection()

        strCodeFilePath = "C:\Temp\_stock_code.txt"
        Dim fileReader As System.IO.StreamReader
        Dim stringReader As String

        Try
            fileReader = My.Computer.FileSystem.OpenTextFileReader(strCodeFilePath)
            While Not fileReader.EndOfStream
                stringReader = ""
                stringReader = fileReader.ReadLine()
                If Trim(stringReader) = "" Then
                    Exit While
                End If

                TArr = Split(Trim(stringReader), "|")
                For i = LBound(TArr) To UBound(TArr)
                    If i = 0 Then
                        strCode = Trim(TArr(i))
                    ElseIf i = 1 Then
                        strName = Trim(TArr(i))
                        gStockCodeTable.Add(strName, strCode)
                        'System.Console.WriteLine(strName)
                        'cmbStock.Items.Add(strName)
                        HSource.Add(strName)
                        '// ComboBox1.Items.Add("Tokyo")
                    End If
                Next
            End While
        Catch ex As System.IO.IOException
            MsgBox(strCodeFilePath + " 주식 종목코드 파일이 없습니다.")
            Return
        End Try


        With txtSuggest
            .AutoCompleteCustomSource = HSource
            .AutoCompleteMode = AutoCompleteMode.SuggestAppend
            .AutoCompleteSource = AutoCompleteSource.CustomSource
        End With

        With cmbStock
            .AutoCompleteCustomSource = HSource
            .AutoCompleteMode = AutoCompleteMode.SuggestAppend
            .AutoCompleteSource = AutoCompleteSource.CustomSource
        End With

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

    Private Sub trStock900BongInfo(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim nCnt As Integer
        Dim i As Integer
        Dim strItemValue As String


        nCnt = KHOpenAPI.GetRepeatCnt(eventArgs.sTrCode, eventArgs.sRQName)

        For i = 0 To (nCnt - 1)
            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "일자")
            Console.WriteLine("일자:" + strItemValue)

            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "거래량")
            Console.WriteLine("거래량:" + strItemValue)

            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "시가")
            Console.WriteLine("시가:" + strItemValue)

            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "현재가")
            Console.WriteLine("종가(현재가):" + strItemValue)

            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "저가")
            Console.WriteLine("저가:" + strItemValue)

            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "고가")
            Console.WriteLine("고가:" + strItemValue)

            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "거래대금")
            Console.WriteLine("거래대금:" + strItemValue)
        Next

    End Sub

    Private Sub trProcTest(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim sDate As String
        Dim stockSellBuyInfoMain As StockSellBuyInfoMain

        Dim nCnt As Short, i As Short
        Dim strItemValue As String
        Dim sCompany As String
        Dim tCompany As String
        Dim lOnlyBuy, lBuy, lSell As Long
        Dim lSellBuy As Long = 0, lSellBuyTotal As Long = 0

        '// class 하나 셋팅
        stockSellBuyInfoMain = New StockSellBuyInfoMain

        nCnt = KHOpenAPI.GetRepeatCnt(eventArgs.sTrCode, eventArgs.sRQName)

        For i = 0 To (nCnt - 1)

            'strItemValue = KHOpenAPI1.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "순위")

            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "회원사명")
            sCompany = Trim(strItemValue).Replace(" ", "")
            tCompany = ""
            tCompany = gHashStockCompany(sCompany)
            If tCompany = Nothing Then
                gHashStockCompany.Add(sCompany, sCompany)
            End If

            '// 누적순매수
            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "누적순매수수량")
            lOnlyBuy = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))

            '// 매수수량
            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "매수수량")
            lBuy = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))

            '// 매도수량
            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "매도수량")
            lSell = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))

            '// 순매수 합산
            If gCompanySellBuyCount.Contains(sCompany) = False Then
                lSellBuyTotal = lOnlyBuy
                gCompanySellBuyCount.Add(sCompany, lSellBuyTotal)
            Else
                lSellBuy = gCompanySellBuyCount(sCompany)
                lSellBuyTotal = lSellBuy + lOnlyBuy
                gCompanySellBuyCount.Remove(sCompany)
                gCompanySellBuyCount.Add(sCompany, lSellBuyTotal)
            End If

            '// class에 값 셋팅
            gStockSellBuyInfoSub = New StockSellBuyInfoSub
            gStockSellBuyInfoSub.setData(sCompany, lSellBuyTotal, lSell, lBuy)
            stockSellBuyInfoMain.addStockSellBuyInfo(gStockSellBuyInfoSub)
        Next

        sDate = gHashScreenAndDate(eventArgs.sScrNo)
        If sDate = Nothing Then
            MsgBox("날짜가 없습니다!!" + " | SrcNO : " + eventArgs.sScrNo)
        End If

        stockSellBuyInfoMain.setCurrDate(sDate)
        gListStockSellBuyInfoMain.Add(stockSellBuyInfoMain)
        gRecvCommandCount = gRecvCommandCount + 1
        '// 처리후에 화면번호와 매핑한 날짜 데이터는 삭제한다.
        '// 혹시 동일한 화면번호 다른날짜와 겹칠까봐 등록후에 제거한다.
        gHashScreenAndDate.Remove(eventArgs.sScrNo)

        System.Console.WriteLine(sDate + "| RecvCommandCount : " + CStr(gRecvCommandCount) + " | sScrNo : " + eventArgs.sScrNo + " | 결과개수 : " + CStr(nCnt))

    End Sub
    Private Sub trProcSellBuyAnalDataTest(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim sDate As String, tDate As String
        Dim stockSellBuyInfoMain As StockSellBuyInfoMain

        Dim nCnt As Short, i As Short
        Dim strItemValue As String
        Dim sCompany As String
        Dim lOnlyBuy, lBuy, lSell As Long
        Dim lSellBuy As Long = 0, lSellBuyTotal As Long = 0
        Dim hashTodayStockCompany As New Hashtable

        '// class 하나 셋팅
        stockSellBuyInfoMain = New StockSellBuyInfoMain

        tDate = gHashScreenAndDate(eventArgs.sScrNo)
        If Trim(tDate) = "" Then
            Console.WriteLine("날짜가 없습니다!!" + " | SrcNO : " + eventArgs.sScrNo)
        End If

        nCnt = KHOpenAPI.GetRepeatCnt(eventArgs.sTrCode, eventArgs.sRQName)
        For i = 0 To (nCnt - 1)

            'strItemValue = KHOpenAPI1.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "순위")

            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "회원사명")
            sCompany = Trim(strItemValue).Replace(" ", "").Replace(".", "")
            If gHashStockCompany.Contains(sCompany) = False Then
                gHashStockCompany.Add(sCompany, sCompany)
            End If

            '// 누적순매수
            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "누적순매수수량")
            lOnlyBuy = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))

            '// 매수수량
            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "매수수량")
            lBuy = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))

            '// 매도수량
            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "매도수량")
            lSell = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))

            gStockSellBuyInfoSub = New StockSellBuyInfoSub
            gStockSellBuyInfoSub.setData(sCompany, lOnlyBuy, lSell, lBuy)
            stockSellBuyInfoMain.addStockSellBuyInfo(gStockSellBuyInfoSub)

            'Console.WriteLine(tDate + ". 증권사:" + sCompany + ", 순매수:" + CStr(lOnlyBuy) + ", 매도:" + CStr(lSell) + ", 매수:" + CStr(lBuy))

            '// 오늘 15등 안에 출현한 증권사 hash table
            If hashTodayStockCompany.Contains(sCompany) = False Then
                hashTodayStockCompany.Add(sCompany, "")
            End If
        Next

        '// 오늘 날짜에서 빠진 증권사에 대해서 이전 순매두/매도 값을 넣어 준다.
        '// 데이터가 15등까지만 나와서 어제까지 나왔던 증권사가 오늘 순위안에 없어면 차트그릴때 데이터가 비게 된다.
        Dim key As Object
        Dim gCompanySellBuyCountKeys As ICollection
        gCompanySellBuyCountKeys = gCompanySellBuyCount.Keys
        '// key ==> 증권사명
        For Each key In gCompanySellBuyCountKeys
            If hashTodayStockCompany.Contains(key.ToString) = False Then
                '// 오늘 장보가 없는 증권사
                gStockSellBuyInfoSub = New StockSellBuyInfoSub
                '// 오늘 15등 안에 없었기 때문에 모두 0으로 셋팅한다.
                '// google chart에서는 매수/매도 없어도 이름이 필요함.
                gStockSellBuyInfoSub.setData(key.ToString, 0, 0, 0)
                stockSellBuyInfoMain.addStockSellBuyInfo(gStockSellBuyInfoSub)
            End If
        Next

        '// 서버에서 받은 날짜 셋팅
        stockSellBuyInfoMain.setCurrDate(tDate)
        '// 증권사 매수/매도 정보 저장
        gListStockSellBuyInfoMain.Add(stockSellBuyInfoMain)
        gRecvCommandCount = gRecvCommandCount + 1
        '// 처리후에 화면번호와 매핑한 날짜 데이터는 삭제한다.
        '// 혹시 동일한 화면번호 다른날짜와 겹칠까봐 등록후에 제거한다.
        gHashScreenAndDate.Remove(eventArgs.sScrNo)

        System.Console.WriteLine(tDate + "| RecvCommandCount : " + CStr(gRecvCommandCount) + " | ScrNumer : " + eventArgs.sScrNo)

    End Sub

    Private Sub trProcSellBuyAnalData(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim sDate As String, tDate As String
        Dim stockSellBuyInfoMain As StockSellBuyInfoMain

        Dim nCnt As Short, i As Short
        Dim strItemValue As String
        Dim sCompany As String
        Dim lOnlyBuy, lBuy, lSell As Long
        Dim lSellBuy As Long = 0, lSellBuyTotal As Long = 0
        Dim hashTodayStockCompany As New Hashtable

        '// class 하나 셋팅
        stockSellBuyInfoMain = New StockSellBuyInfoMain

        tDate = gHashScreenAndDate(eventArgs.sScrNo)

        nCnt = KHOpenAPI.GetRepeatCnt(eventArgs.sTrCode, eventArgs.sRQName)
        For i = 0 To (nCnt - 1)

            'strItemValue = KHOpenAPI1.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "순위")

            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "회원사명")
            sCompany = Trim(strItemValue).Replace(" ", "").Replace(".", "")
            If gHashStockCompany.Contains(sCompany) = False Then
                gHashStockCompany.Add(sCompany, sCompany)
            End If

            '// 누적순매수
            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "누적순매수수량")
            lOnlyBuy = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))

            '// 매수수량
            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "매수수량")
            lBuy = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))

            '// 매도수량
            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "매도수량")
            lSell = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))

            '// 순매수, 순매도 합을 더하고 뺀다.
            If gCompanySellBuyCount.Contains(sCompany) = False Then
                gCompanySellBuyCount.Add(sCompany, lOnlyBuy)

                gStockSellBuyInfoSub = New StockSellBuyInfoSub
                gStockSellBuyInfoSub.setData(sCompany, lOnlyBuy, lSell, lBuy)
                stockSellBuyInfoMain.addStockSellBuyInfo(gStockSellBuyInfoSub)
            Else
                lSellBuy = gCompanySellBuyCount(sCompany)
                lSellBuyTotal = lSellBuy + lOnlyBuy
                gCompanySellBuyCount.Remove(sCompany)
                gCompanySellBuyCount.Add(sCompany, lSellBuyTotal)

                gStockSellBuyInfoSub = New StockSellBuyInfoSub
                gStockSellBuyInfoSub.setData(sCompany, lSellBuyTotal, lSell, lBuy)
                stockSellBuyInfoMain.addStockSellBuyInfo(gStockSellBuyInfoSub)
                Console.WriteLine(tDate + ". 증권사:" + sCompany + ", 순매수:" + CStr(lOnlyBuy) + ", 매도:" + CStr(lSell) + ", 매수:" + CStr(lBuy))
            End If

            '// 오늘 15등 안에 출현한 증권사 hash table
            If hashTodayStockCompany.Contains(sCompany) = False Then
                hashTodayStockCompany.Add(sCompany, "")
            End If

            '// 마지막 날짜가 되면 total 값을 sorted list에 넣고 이를 정렬하여
            '// 순매수가 큰 데이터 상위 몇개를 그래프로 보여준다.
            If tDate = Trim(txtEndDate1.Text) Then
                If gSortedListStockSellBuyInfo.Contains(lSellBuyTotal) = False Then
                    gSortedListStockSellBuyInfo.Add(lSellBuyTotal, sCompany)
                End If
            End If

        Next

        '// 오늘 날짜에서 빠진 증권사에 대해서 이전 순매두/매도 값을 넣어 준다.
        '// 데이터가 15등까지만 나와서 어제까지 나왔던 증권사가 오늘 순위안에 없어면 차트그릴때 데이터가 비게 된다.
        Dim key As Object
        Dim gCompanySellBuyCountKeys As ICollection

        gCompanySellBuyCountKeys = gCompanySellBuyCount.Keys
        '// key ==> 증권사명
        For Each key In gCompanySellBuyCountKeys
            If hashTodayStockCompany.Contains(key.ToString) = False Then
                '// 오늘 장보가 없는 증권사
                gStockSellBuyInfoSub = New StockSellBuyInfoSub
                gStockSellBuyInfoSub.setData(key.ToString, gCompanySellBuyCount(key.ToString), 0, 0)
                stockSellBuyInfoMain.addStockSellBuyInfo(gStockSellBuyInfoSub)
            End If
        Next

        'Console.WriteLine("tDate : " + tDate + " | txtEndDate1 : " + Trim(txtEndDate1.Text))
        sDate = ""
        sDate = gHashScreenAndDate(eventArgs.sScrNo)
        If Trim(sDate) = "" Then
            Console.WriteLine("날짜가 없습니다!!" + " | SrcNO : " + eventArgs.sScrNo)
        End If

        '// 서버에서 받은 날짜 셋팅
        stockSellBuyInfoMain.setCurrDate(sDate)
        '// 증권사 매수/매도 정보 저장
        gListStockSellBuyInfoMain.Add(stockSellBuyInfoMain)
        gRecvCommandCount = gRecvCommandCount + 1
        '// 처리후에 화면번호와 매핑한 날짜 데이터는 삭제한다.
        '// 혹시 동일한 화면번호 다른날짜와 겹칠까봐 등록후에 제거한다.
        gHashScreenAndDate.Remove(eventArgs.sScrNo)

        System.Console.WriteLine(sDate + "| RecvCommandCount : " + CStr(gRecvCommandCount) + " | ScrNumer : " + eventArgs.sScrNo)

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
        Call drawChartStockSellBuyBarChart()

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

        Call drawChartStockSellBuyBarChart()

    End Sub
    Private Sub drawChartStockSellBuyLineChart()
        '/// Series를 초기화 한다.
        Me.chartStock.Series.Clear()
        Me.chartStock.ResetAutoValues()
        Me.chartStock.ResetText()

        '// 차트 그리기 위한 종목 선정
        Dim SLEnum As IDictionaryEnumerator
        Dim listCount As Integer, chartPrintCountBreaek As Integer = 0
        Dim total As Integer = 0

        listCount = gSortedListStockSellBuyInfo.Count
        chartPrintCountBreaek = listCount - 2
        SLEnum = gSortedListStockSellBuyInfo.GetEnumerator()
        gPrintChartCompanySeries.Clear()

        While SLEnum.MoveNext
            total += 1
            'Console.WriteLine(" 정렬값 {0} : " + CStr(SLEnum.Key.ToString) + " | " + SLEnum.Value.ToString)
            If total >= chartPrintCountBreaek Then
                gPrintChartCompanySeries.Add(SLEnum.Value.ToString, SLEnum.Value.ToString)
                'Console.WriteLine(" 차트종목 : " + CStr(SLEnum.Key.ToString) + " | " + SLEnum.Value.ToString)
            End If
        End While

        '// regist Series
        Dim keysCompanyKeys As ICollection
        keysCompanyKeys = gHashStockCompany.Keys()
        For Each Key In keysCompanyKeys
            If gPrintChartCompanySeries.Contains(Key.ToString) = True Then
                Me.chartStock.Series.Add(Key.ToString)
                Me.chartStock.Series(Key.ToString).ChartType = DataVisualization.Charting.SeriesChartType.Line
                'Me.chartStock.Series(Key.ToString).ChartType = DataVisualization.Charting.SeriesChartType.Point
                Me.chartStock.Series(Key.ToString).IsValueShownAsLabel = True
                'Me.chartStock.Series(Key.ToString).ToolTip = " #VALX | #VALY"
                'System.Console.WriteLine(Key.ToString)
            End If
        Next

        '// for listView
        Dim item As ListViewItem
        lstView1.Items.Clear()

        '// draw chart
        Dim sCurrDate As String
        For Each _stockSellBuyInfoMain In gListStockSellBuyInfoMain
            sCurrDate = _stockSellBuyInfoMain.getCurrDate()
            For Each _stockSellBuySub In _stockSellBuyInfoMain.getStockSellBuyInfo
                If gPrintChartCompanySeries.Contains(_stockSellBuySub.getName) = True Then
                    If sCurrDate = Trim(txtEndDate1.Text) Then
                        item = New ListViewItem(sCurrDate)
                        item.SubItems.Add(_stockSellBuySub.getName)
                        item.SubItems.Add(CStr(_stockSellBuySub.getOnlyBuyCount))
                        lstView1.Items.Add(item)
                    End If
                    Me.chartStock.Series(_stockSellBuySub.getName).Points.AddXY(sCurrDate, _stockSellBuySub.getOnlyBuyCount)
                    System.Console.WriteLine("차트날짜 : " + sCurrDate)
                End If
            Next
        Next

        System.Console.WriteLine("차트 그리기 완성!!")

    End Sub

    Private Sub drawChartStockSellBuyBarChart()

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
        ElseIf eventArgs.sRQName = "일일기간분석" Then
            Call trProcSellBuyAnalData(sender, eventArgs)
        ElseIf eventArgs.sRQName = "주식기본정보요청" Then
            Call trProcTest(sender, eventArgs)
        ElseIf eventArgs.sRQName = "주식일봉차트조회" Then
            Call trStock900BongInfo(sender, eventArgs)
        ElseIf eventArgs.sRQName = "일일기간분석테스트" Then
            Call trProcSellBuyAnalDataTest(sender, eventArgs)
        End If

        Console.WriteLine("DisconnectRealData SrcNumber : " + eventArgs.sScrNo)
        KHOpenAPI.DisconnectRealData(eventArgs.sScrNo)

    End Sub

    Private Sub btnCmd1_Click(sender As Object, e As EventArgs) Handles btnCmd1.Click
        KHOpenAPI.SetInputValue("종목코드", Trim(txtStockCode.Text))
        KHOpenAPI.CommRqData("주식기본정보요청", "opt10001", CInt("0"), "8001")
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

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        frmChart.Show()
    End Sub

    Private Sub btnAnalBetween_Click(sender As Object, e As EventArgs) Handles btnAnalBetween.Click

        Dim chartSeries As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim nScreenNumber As Integer
        Dim strStartDate As String
        Dim strStartY, strStartM, strStartD As String
        Dim nPorgresValue As Integer = 0
        Dim nProgressMax As Integer = 0

        If Trim(txtStockCode.Text).Length = 0 Then
            MsgBox("선택된 종목이 없습니다")
            Return
        End If

        gHashStockCompany.Clear()
        gListStockSellBuyInfoMain.Clear()
        gHashScreenAndDate.Clear()
        gCompanySellBuyCount.Clear()
        gSortedListStockSellBuyInfo.Clear()
        gPrintChartCompanySeries.Clear()

        '// 미리 증권사 목록을 셋팅하고 0 값을 셋팅한다
        For k = 0 To gListStockCompanyCode.Count - 1
            gCompanySellBuyCount.Add(gListStockCompanyCode.Item(k), 0)
        Next

        '// 추출할 시작 날짜
        strStartY = txtStartDate1.Text.Substring(0, 4)
        strStartM = txtStartDate1.Text.Substring(4, 2)
        strStartD = txtStartDate1.Text.Substring(6, 2)

        gSendCommandCount = 0
        gRecvCommandCount = 0
        nScreenNumber = 1000
        gStore.setCleaerList()

        '// progress bar min max를 구하기 위해서 설정.
        Dim startDateProgress As DateTime = New DateTime(CInt(strStartY), CInt(strStartM), CInt(strStartD))
        Do While True
            strStartDate = startDateProgress.Year.ToString + checkMonthDay(CInt(startDateProgress.Month.ToString)) + checkMonthDay(CInt(startDateProgress.Day.ToString))
            If strStartDate = Trim(txtEndDate1.Text) Then
                Exit Do
            End If
            nProgressMax += 1
            startDateProgress = startDateProgress.AddDays(+1)
        Loop

        '// progress bar setting min, max
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = nProgressMax

        Dim startDate As DateTime = New DateTime(CInt(strStartY), CInt(strStartM), CInt(strStartD))
        Do While True
            strStartDate = startDate.Year.ToString + checkMonthDay(CInt(startDate.Month.ToString)) + checkMonthDay(CInt(startDate.Day.ToString))

            '// RecvTRdata에서 날짜를 가져오기 위해 셋팅
            gHashScreenAndDate.Add(CStr(nScreenNumber), strStartDate)
            If nScreenNumber = 1099 Then
                'nScreenNumber = 1000
                nScreenNumber += 1
            Else
                nScreenNumber += 1
            End If

            KHOpenAPI.SetInputValue("종목코드", Trim(txtStockCode.Text))
            KHOpenAPI.SetInputValue("수정주가구분", "1")
            KHOpenAPI.SetInputValue("조회구분", "0")
            KHOpenAPI.SetInputValue("시작일자", strStartDate)
            KHOpenAPI.SetInputValue("종료일자", strStartDate)
            KHOpenAPI.CommRqData("일일기간분석", "OPT10038", CInt("0"), CStr(nScreenNumber))
            Threading.Thread.Sleep(300)

            gSendCommandCount = gSendCommandCount + 1
            System.Console.WriteLine(strStartDate + " | SendCommandCount : " + CStr(gSendCommandCount) + " | ScrNumer : " + CStr(nScreenNumber))

            If strStartDate = Trim(txtEndDate1.Text) Then
                System.Console.WriteLine("서버 요청 종료")
                Exit Do
            End If

            '// 날짜를 하루씩 더한다.
            startDate = startDate.AddDays(+1)

            nPorgresValue += 1
            ProgressBar1.Value = nPorgresValue
        Loop

        '// 데이터 다 받을때까지 대기.
        Do While True
            If gSendCommandCount <= gRecvCommandCount Then
                Exit Do
            End If
            Threading.Thread.Sleep(200)
            System.Console.WriteLine("Wait OnRecvTRdata...S[" + CStr(gSendCommandCount) + "], R[" + CStr(gRecvCommandCount) + "]")
            Application.DoEvents()
        Loop

        '// 라인 차트를 그린다.
        Call drawChartStockSellBuyLineChart()

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
        'KHOpenAPI.SetInputValue("수정주가구분", "1")
        '/// 조회구분 = 0: 입력한 시작,종료 날짜로 조회
        '///            1: 전일, 조회지정일 입력(5일 ~ 120일)
        KHOpenAPI.SetInputValue("조회구분", "0")
        KHOpenAPI.SetInputValue("시작일자", Trim(txtAnalStartDate.Text))
        KHOpenAPI.SetInputValue("종료일자", Trim(txtAnalEndDate.Text))
        'KHOpenAPI.SetInputValue("기간", "5")

        'KHOpenAPI.CommRqData("종목별증권사순위요청기간", "OPT10038", CInt("0"), "1001")
        KHOpenAPI.CommRqData("종목별증권사순위요청기간단순", "OPT10038", CInt("0"), "4001")

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

    Private Sub cmbStock_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbStock.KeyPress
        'Dim index As Integer
        'index = cmbStock.FindString(cmbStock.Text)
        'cmbStock.SelectedIndex = index
    End Sub



    Private Sub txtSuggest_TextChanged(sender As Object, e As EventArgs) Handles txtSuggest.TextChanged
        'txtOut.Text = txtSuggest.Text
        txtStockCode.Text = gStockCodeTable(txtSuggest.Text)
    End Sub



    Private Sub cmbStock_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStock.SelectedIndexChanged

    End Sub

    Private Sub cmbStock_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbStock.SelectedValueChanged
        txtStockCode.Text = gStockCodeTable(cmbStock.SelectedText)
        MsgBox("22")
    End Sub

    Private Sub cmbStock_SystemColorsChanged(sender As Object, e As EventArgs) Handles cmbStock.SystemColorsChanged
        txtStockCode.Text = gStockCodeTable(cmbStock.SelectedText)
        MsgBox("11")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        KHOpenAPI.SetInputValue("종목코드", Trim(txtStockCode.Text))
        KHOpenAPI.SetInputValue("기준일자", "20160708")
        KHOpenAPI.SetInputValue("수정주가구분", "1")
        KHOpenAPI.CommRqData("주식일봉차트조회", "OPT10081", CInt("0"), "1004")
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim chartSeries As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim nScreenNumber As Integer
        Dim strStartDate As String
        Dim strStartY, strStartM, strStartD As String
        Dim nPorgresValue As Integer = 0
        Dim nProgressMax As Integer = 0
        Dim googleChart As New GoogleChart

        If Trim(txtStockCode.Text).Length = 0 Then
            MsgBox("선택된 종목이 없습니다")
            Return
        End If

        gHashStockCompany.Clear()
        gListStockSellBuyInfoMain.Clear()
        gHashScreenAndDate.Clear()
        gCompanySellBuyCount.Clear()
        gSortedListStockSellBuyInfo.Clear()
        gPrintChartCompanySeries.Clear()

        '// 미리 증권사 목록을 셋팅하고 0 값을 셋팅한다
        Dim k As Integer
        For k = 0 To gListStockCompanyCode.Count - 1
            gCompanySellBuyCount.Add(gListStockCompanyCode.Item(k), 0)
        Next

        '// 추출할 시작 날짜
        strStartY = txtStartDate1.Text.Substring(0, 4)
        strStartM = txtStartDate1.Text.Substring(4, 2)
        strStartD = txtStartDate1.Text.Substring(6, 2)

        gSendCommandCount = 0
        gRecvCommandCount = 0
        nScreenNumber = 1000
        gStore.setCleaerList()

        '// progress bar min max를 구하기 위해서 설정.
        Dim startDateProgress As DateTime = New DateTime(CInt(strStartY), CInt(strStartM), CInt(strStartD))
        Do While True
            strStartDate = startDateProgress.Year.ToString + checkMonthDay(CInt(startDateProgress.Month.ToString)) + checkMonthDay(CInt(startDateProgress.Day.ToString))
            If strStartDate = Trim(txtEndDate1.Text) Then
                Exit Do
            End If
            nProgressMax += 1
            startDateProgress = startDateProgress.AddDays(+1)
        Loop

        '// progress bar setting min, max
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = nProgressMax

        '// 증권사 서버에 실제 요청을 날린다.
        Dim startDate As DateTime = New DateTime(CInt(strStartY), CInt(strStartM), CInt(strStartD))
        Do While True
            strStartDate = startDate.Year.ToString + checkMonthDay(CInt(startDate.Month.ToString)) + checkMonthDay(CInt(startDate.Day.ToString))

            If nScreenNumber >= 1099 Then
                'nScreenNumber = 1000
                nScreenNumber += 1
            Else
                nScreenNumber += 1
            End If

            '// screen number 에 대응하는 날짜 셋팅
            gHashScreenAndDate.Add(CStr(nScreenNumber), strStartDate)
            Console.WriteLine("Add hash, screen num : " + CStr(nScreenNumber) + ", date : " + strStartDate)

            KHOpenAPI.SetInputValue("종목코드", Trim(txtStockCode.Text))
            KHOpenAPI.SetInputValue("수정주가구분", "1")
            KHOpenAPI.SetInputValue("조회구분", "0")
            KHOpenAPI.SetInputValue("시작일자", strStartDate)
            KHOpenAPI.SetInputValue("종료일자", strStartDate)
            'KHOpenAPI.CommRqData("일일기간분석", "OPT10038", CInt("0"), CStr(nScreenNumber))
            KHOpenAPI.CommRqData("일일기간분석테스트", "OPT10038", CInt("0"), CStr(nScreenNumber))

            Threading.Thread.Sleep(300)

            '// 시스템 제어권을 OS에 넘김.
            Application.DoEvents()

            Console.WriteLine(strStartDate + " | SendCommandCount : " + CStr(gSendCommandCount) + " | ScrNumer : " + CStr(nScreenNumber))

            gSendCommandCount = gSendCommandCount + 1

            If strStartDate = Trim(txtEndDate1.Text) Then
                System.Console.WriteLine("날짜 끝")
                Exit Do
            End If

            '// 날짜를 하루씩 더한다.
            startDate = startDate.AddDays(+1)
            '// progress bar +1 증가
            nPorgresValue += 1
            ProgressBar1.Value = nPorgresValue
        Loop

        Console.WriteLine("서버에 요청 완료")

        '// 데이터 다 받을때까지 대기.
        Dim totalRetryCount As Integer = 0
        Do While True
            If gSendCommandCount <= gRecvCommandCount Then
                Exit Do
            End If
            Threading.Thread.Sleep(300)
            Console.WriteLine("Wait OnRecvTRdata...S[" + CStr(gSendCommandCount) + "], R[" + CStr(gRecvCommandCount) + "]")
            Application.DoEvents()
            totalRetryCount += 1
            If totalRetryCount >= 100 Then
                MsgBox("데이터를 서버로 부터 모두 받지 못했습니다. 다시한번 실행해 주세요!")
                Return
            End If
        Loop

        '// 구글 차트 데이터를 생성하기 위해 class에 데이터를 셋팅한다.
        googleChart.setHashStockCompany(gHashStockCompany)
        googleChart.setListStockSellBuyInfoMain(gListStockSellBuyInfoMain)
        googleChart.setSortedListStockSellBuyInfo(gSortedListStockSellBuyInfo)

        '// 구글 차트 데이터를 생성한다.
        'Call googleChart.drawGoogleChart()
        Call googleChart.drawGoogleChartTest()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        GlobalDefine.runBrowser("F:\googleChart.html")
    End Sub
End Class
