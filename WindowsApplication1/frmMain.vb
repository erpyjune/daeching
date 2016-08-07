Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Windows.Forms.DataVisualization.Charting.Chart
Imports System.IO.StreamWriter
'Imports System.String

Public Class frmMain
    Dim nRequestDelayTime As Integer = 250
    Dim bStop As Boolean
    Dim gStrDate As String
    Dim gStore As StoreClass
    Dim lRefreshCount As Long = 0
    Dim bLoginStatus As Boolean
    Dim gStockCodeTable As New Hashtable    '// 코스피 + 코스닥 종목 테이블, key = 종목명, value = 종목코드
    Dim gStockNameTable As New Hashtable    '// 코스피 + 코스닥 종목 테이블, key = 종목코드, value = 종목명
    Dim gStockCompanyCodeTable As New Hashtable
    Dim gKosdaqStockCodeTable As New Hashtable
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
    Dim gHashOnlyBuyValueAllStock As New Hashtable '// 모든 종목의 순매수 정보 저장
    Dim gHashScreenNoAndStock As New Hashtable '// 전종목 순매수 정도 가져올때 screen num과 종목 매칭할 임시 저장소
    Dim gHashSijaMain As New Hashtable '// 시작점 메인 테이블
    Dim gHashStockStatus As New Hashtable '// 주식 상태 정보
    Dim gnStartPrice As Integer
    Dim gnEndPrice As Integer

    Dim gListSPHadaeNear As New List(Of StartPointInfo)      '// 하대 근접
    Dim gListSPHadaeSurpass As New List(Of StartPointInfo)   '// 하대 돌파
    Dim gListSPHadeaDrop As New List(Of StartPointInfo)      '// 하대 하향
    Dim gListSPSangdaeNear As New List(Of StartPointInfo)    '// 상대 근접
    Dim gListSPSangdaeSurpass As New List(Of StartPointInfo) '// 상대 돌파
    Dim gListSPSangdaeOver As New List(Of StartPointInfo)    '// 상대 이상
    Dim gListSPSangdaeDrop As New List(Of StartPointInfo)    '// 상대 하향
    Dim gListSPSangHaSurpass As New List(Of StartPointInfo)  '// 대박 돌파 
    Dim gListSPSangHaDrop As New List(Of StartPointInfo)     '// 대박 하향
    Dim gHashCompanyOnlyBuy As New Hashtable '// 종목별 1포, 2포, 3포 순매수 저장
    Dim gHashCompanyBizInfo As New Hashtable '// 상장주식수, 영업이익, 순이익, 매출

    Dim gHashNameByCurPrice As New Hashtable  '// 종목명 + 현재가격
    Dim gHashNameByStartPointEndPrice As New Hashtable  '// 종목명 + 시작점 종가

    Dim gCosdaqStrListArr(16) As String '// 실시간 정보를 받아 오기위한 코스닥 코드 리스트 배열
    Dim gnCosdaqStrListCount As Integer '// gCosdaqStrListArr 배열 count
    '// 신호등 검색에 필요
    Dim gnFilterSignValue As Integer    '// 순간체결량 이상, 이하값 필터링 조건
    Dim gnFilterTradePrice As Integer     '// 거래대금 필터링 조건
    Dim gnFilterTradeValue As Integer     '// 거래량 필터링 조건
    Dim gnFilterAnalStartPrice As Integer   '// 주가 이상 필터링 조건
    Dim gnFilterAnalEndPrice As Integer     '// 주가 이하 필터링 조건
    Dim gnFilterAnalCount As Integer        '// 분봉 몇개를 분석할지. 1일 39봉 (9시 ~ 3시30분)
    Dim gnFilterJupo1SunBuy As Integer     '// 주포1 순매수 필터링 조건
    Dim gsFilterSPNearPer As Single    '// 분봉 시작점 근접 % 

    Dim gsSPDate As String          '// 분봉 시작점 날짜
    Dim gnSPStartV As Integer       '// 분봉 시작점 시가
    Dim gnSPEndV As Integer         '// 분봉 시작점 종가
    Dim gnSinhoOnlyBuyCount(5) As Integer       '// 주포1,2,3 순매수
    Dim gnSinhoStartPointHow As Short    '// 1:상대근접, 2:하대근접, 0:근접아님
    Dim gnSinhoTradeValue As Integer        '// 서버에서 가져온 거래량
    Dim gnSinhoCurPrice As Integer       '// 서버에서 가져온 현재가
    Dim gHashSinhoAppearanceCount As New Hashtable    '// 신호등에 몇번 노출되었는지 카운트 한다

    Function getAPI()
        Return KHOpenAPI
    End Function
    Function getHashSijaMain()
        Return gHashSijaMain
    End Function

    Function getHashSijaMainData(ByVal stockName As String)
        Return gHashSijaMain(stockName)
    End Function
    Function getStockCodeTable()
        Return gStockCodeTable
    End Function

    Function getStockCode(ByVal stockName As String)
        Return gStockCodeTable(stockName)
    End Function
    Function getStockStatus(ByVal stockName As String)
        Return gHashStockStatus(stockName)
    End Function
    Sub setStop(ByVal act As Boolean)
        Me.bStop = act
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '// main 초기화
        Call mainInitialize()
        '// 종목 코드 로딩.
        Call loadingStockCodeData()
        '// 코스닥만 따로 로딩
        Call loadingKosdaqStockCodeData()
        '// 회원사 코드 로딩
        Call loadingStockCompanyCodeData()

    End Sub
    Private Sub mainInitialize()
        gStore = New StoreClass
        gBefStockSellBuyInfoSub = New StockSellBuyInfoSub

        txtEndDate1.Text = Format(Now, "yyyyMMdd")
        txtAnalEndDate.Text = Format(Now, "yyyyMMdd")
        txtJupoEndDate.Text = Format(Now, "yyyyMMdd")
        txtBunBongAnalEndDate.Text = Format(Now, "yyyyMMdd")
        txtBunBongAnalEndDate.Text = Format(Now, "yyyyMMdd")
        'txtSPToday.Text = Format(Now, "yyyyMMdd")

    End Sub

    Private Sub loadingStockCompanyCodeData()
        Dim strCodeFilePath As String = Application.StartupPath + "\_sotck_company_code.txt"
        Dim TArr() As String
        Dim strCode As String = "", strName As String
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
                        'System.Console.WriteLine(strName)
                    End If
                Next
            End While
        Catch ex As System.IO.IOException
            MsgBox(strCodeFilePath + " 회원사별 종목코드 파일을 찾을 수 없습니다.")
            Return
        End Try

    End Sub
    Private Sub loadingKosdaqStockCodeData()
        Dim strCodeFilePath As String = Application.StartupPath + "\_kosdaq_code.txt"
        Dim TArr() As String
        Dim strCode As String = "", strName As String
        Dim fileReader As System.IO.StreamReader
        Dim stringReader As String
        Dim nTotal As Integer = 1
        Dim sb As New System.Text.StringBuilder()

        gnCosdaqStrListCount = 0

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
                        gKosdaqStockCodeTable.Add(strName, strCode)

                        If sb.Length = 0 Then
                            sb.Append(strCode)
                        Else
                            sb.Append(";").Append(strCode)
                        End If

                        If nTotal Mod 97 = 0 Then
                            gCosdaqStrListArr(gnCosdaqStrListCount) = sb.ToString
                            sb.Clear()
                            gnCosdaqStrListCount += 1
                        End If

                    End If
                Next

                nTotal += 1

            End While

            If sb.Length > 0 Then
                gCosdaqStrListArr(gnCosdaqStrListCount) = sb.ToString
            End If

        Catch ex As System.IO.IOException
            MsgBox(strCodeFilePath + " 코스닥 종목코드 파일을 찾을 수 없습니다.")
            Return
        End Try


        For i = 0 To gnCosdaqStrListCount
            Console.WriteLine("[{0}]종목코드:[{1}] {2}", nTotal, i, gCosdaqStrListArr(i))
        Next

    End Sub

    Private Sub loadingStockCodeData()
        Dim strCodeFilePath As String
        Dim TArr() As String
        Dim strCode As String = "", strName As String
        Dim HSource As New AutoCompleteStringCollection()

        'strCodeFilePath = "C:\Temp\_stock_code.txt"
        strCodeFilePath = Application.StartupPath + "\_stock_code.txt"
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
                        gStockNameTable.Add(strCode, strName)
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
        Dim nBong As Integer = 0
        Dim strItemValue As String
        Dim sName As String, sCurDate As String
        Dim nStartV As Integer, nEndV As Integer, nTV As Integer
        Dim stockValueInfo As StockValueInfo
        Dim listStockValueInfo As New List(Of StockValueInfo)()

        '// 몇일전 날짜 
        'sBeforeDate = GlobalDefine.getBeforeAfterDate(-250)

        '// 종목 이름
        sName = Trim(gHashScreenNoAndStock(CStr(eventArgs.sScrNo)))

        nCnt = KHOpenAPI.GetRepeatCnt(eventArgs.sTrCode, eventArgs.sRQName)
        For i = 0 To (nCnt - 1)

            strItemValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "현재가"))
            nEndV = CInt(strItemValue)

            '// 주가 범위가 아니면 skip 처리 한다.
            If gnStartPrice > nEndV Or gnEndPrice < nEndV Then
                Exit For
            End If

            sCurDate = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "일자"))
            'Console.WriteLine("일자:" + sCurDate)

            strItemValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "거래량"))
            nTV = CInt(strItemValue)
            'Console.WriteLine("거래량:" + strItemValue)

            strItemValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "시가"))
            nStartV = CInt(strItemValue)
            'Console.WriteLine("시가:" + strItemValue)


            'Console.WriteLine("종가(현재가):" + strItemValue)

            'strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "저가")
            'Console.WriteLine("저가:" + strItemValue)

            'strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "고가")
            'Console.WriteLine("고가:" + strItemValue)

            'strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "거래대금")
            'Console.WriteLine("거래대금:" + strItemValue)

            stockValueInfo = New StockValueInfo
            stockValueInfo.setStockValue(sCurDate, nStartV, nEndV, nTV, sName, "")
            listStockValueInfo.Add(stockValueInfo)
            If nBong > CInt(Trim(txtSPBong.Text)) Then
                Console.WriteLine("end of 250 bong...")
                Exit For
            End If
            nBong += 1
        Next

        gRecvCommandCount += 1
        gHashSijaMain.Add(sName, listStockValueInfo)
        gHashScreenNoAndStock.Remove(eventArgs.sScrNo)
        Console.WriteLine("Recv count {0}, {1}, {2}", gRecvCommandCount, sName, CStr(listStockValueInfo.Count))

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
    Private Sub trProcStockTradeValue(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)

        Dim nCnt As Short, i As Short
        Dim strItemValue As String

        nCnt = KHOpenAPI.GetRepeatCnt(eventArgs.sTrCode, eventArgs.sRQName)
        For i = 0 To (nCnt - 1)

            strItemValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "거래량")).Replace("+", "").Replace("-", "")
            gnSinhoTradeValue = CInt(strItemValue)
            Console.WriteLine("거래량 {0}", gnSinhoTradeValue)

            strItemValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "현재가")).Replace("+", "").Replace("-", "")
            gnSinhoCurPrice = CInt(strItemValue)
            Console.WriteLine("현재가 {0}", strItemValue)

        Next

        gRecvCommandCount += 1

    End Sub
    Private Sub trProcSellBuyAnalDataTest(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim tDate As String
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
    Private Sub trProcTodayPrimaryCompany(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim nCnt As Short, i As Short
        Dim strItemValue As String
        Dim sSunbun As String
        Dim sCompany As String
        Dim lOnlyBuy As Long
        Dim lTotalOnlyBuy As Long = 0
        Dim sStockName As String

        nCnt = KHOpenAPI.GetRepeatCnt(eventArgs.sTrCode, eventArgs.sRQName)
        For i = 0 To (nCnt - 1)

            sSunbun = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "순위")

            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "회원사명")
            sCompany = Trim(strItemValue).Replace(" ", "").Replace(".", "")

            '// 누적순매수
            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "누적순매수수량")
            lOnlyBuy = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))

            lTotalOnlyBuy += lOnlyBuy

            'If lOnlyBuy > 0 Then
            '    lTotalOnlyBuy += lOnlyBuy
            'End If

            'Console.WriteLine("{0} {1} {2}, {3}", sSunbun, sCompany, CStr(lOnlyBuy), CStr(lTotalOnlyBuy))

            ''// 매수수량
            'strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "매수수량")
            'lBuy = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))

            ''// 매도수량
            'strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "매도수량")
            'lSell = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))
            'Console.WriteLine("{0}, {1}", sCompany, lOnlyBuy)

            If i >= 1 Then
                Exit For
            End If
        Next

        Console.WriteLine("========================================================================")

        sStockName = ""
        sStockName = gHashScreenNoAndStock(CStr(eventArgs.sScrNo))
        If sStockName.Length = 0 Then
            MsgBox("스크린 번호에 해당되는 종목이 없습니다")
            Return
        End If

        gHashOnlyBuyValueAllStock.Add(sStockName, lTotalOnlyBuy)
        gHashScreenNoAndStock.Remove(CStr(eventArgs.sScrNo))
        gRecvCommandCount = gRecvCommandCount + 1
        Console.WriteLine("받음, {0} : {1}, 받은수 {2}", sStockName, CStr(lTotalOnlyBuy), CStr(gRecvCommandCount))
        Application.DoEvents()

    End Sub

    Private Sub trProcSellBuyAnalDataAllStock(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim nCnt As Short, i As Short
        Dim strItemValue As String
        Dim sSunbun As String
        Dim sCompany As String
        Dim lOnlyBuy As Long
        Dim lTotalOnlyBuy As Long = 0
        Dim sStockName As String

        nCnt = KHOpenAPI.GetRepeatCnt(eventArgs.sTrCode, eventArgs.sRQName)
        For i = 0 To (nCnt - 1)

            sSunbun = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "순위")

            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "회원사명")
            sCompany = Trim(strItemValue).Replace(" ", "").Replace(".", "")

            '// 누적순매수
            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "누적순매수수량")
            lOnlyBuy = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))

            lTotalOnlyBuy += lOnlyBuy

            'If lOnlyBuy > 0 Then
            '    lTotalOnlyBuy += lOnlyBuy
            'End If

            'Console.WriteLine("{0} {1} {2}, {3}", sSunbun, sCompany, CStr(lOnlyBuy), CStr(lTotalOnlyBuy))

            ''// 매수수량
            'strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "매수수량")
            'lBuy = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))

            ''// 매도수량
            'strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "매도수량")
            'lSell = CLng(Trim(strItemValue).Replace("+-", "-").Replace(",", ""))
            'Console.WriteLine("{0}, {1}", sCompany, lOnlyBuy)

            If i >= 1 Then
                Exit For
            End If
        Next

        Console.WriteLine("========================================================================")

        sStockName = ""
        sStockName = gHashScreenNoAndStock(CStr(eventArgs.sScrNo))
        If sStockName.Length = 0 Then
            MsgBox("스크린 번호에 해당되는 종목이 없습니다")
            Return
        End If

        gHashOnlyBuyValueAllStock.Add(sStockName, lTotalOnlyBuy)
        gHashScreenNoAndStock.Remove(CStr(eventArgs.sScrNo))
        gRecvCommandCount = gRecvCommandCount + 1
        Console.WriteLine("받음, {0} : {1}, 받은수 {2}", sStockName, CStr(lTotalOnlyBuy), CStr(gRecvCommandCount))
        Application.DoEvents()

    End Sub
    Private Sub trProcStockSign(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim item As ListViewItem
        Dim nCnt As Short, i As Short
        Dim nSignValue As Integer = 0, nFindTotal As Integer = 0
        Dim sStockName As String
        Dim sTime As String, sHighLow As String, sPower As String, sCurPrice As String, sSignValue As String

        sStockName = gHashScreenNoAndStock(CStr(eventArgs.sScrNo))

        nCnt = KHOpenAPI.GetRepeatCnt(eventArgs.sTrCode, eventArgs.sRQName) - 1
        If nCnt < 0 Then
            Console.WriteLine("API 데이터가 없습니다")
            Return
        End If

        For i = nCnt To 0 Step -1
            'Next

            'For i = 0 To (nCnt - 1)

            sSignValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "체결거래량"))
            nSignValue = CInt(Trim(sSignValue).Replace("+", "").Replace("-", ""))
            If nSignValue >= CInt(Trim(txtSignValue.Text)) Then

                nFindTotal += 1

                Console.WriteLine("체결거래량 : " + sSignValue)

                sTime = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "시간"))
                Console.WriteLine("시간 : " + sTime)

                sHighLow = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "대비율"))
                Console.WriteLine("등락율 : " + sHighLow)

                sPower = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "체결강도"))
                Console.WriteLine("체결강도 : " + sPower)

                sCurPrice = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "현재가"))
                Console.WriteLine("현재가 : " + sCurPrice)

                lstInfo.Items.Add(sStockName + ", 시간[" + sTime + "], 체결량[" + sSignValue + "] 등락률[" + sHighLow + "] 체결강도[" + sPower + "] 현재가[" + sCurPrice + "]")

                '// list view
                item = New ListViewItem(sStockName)     '// 종목명
                item.SubItems.Add(sTime)                '// 체결시간
                item.SubItems.Add(sSignValue)           '// 체결량
                If CInt(sSignValue) > 0 Then
                    item.UseItemStyleForSubItems = False
                    item.SubItems(2).ForeColor = Color.Blue
                Else
                    item.UseItemStyleForSubItems = False
                    item.SubItems(2).ForeColor = Color.Red
                End If

                item.SubItems.Add(sHighLow)             '// 주가등락
                If CInt(sHighLow) > 0 Then
                    item.UseItemStyleForSubItems = False
                    item.SubItems(3).ForeColor = Color.Blue
                Else
                    item.UseItemStyleForSubItems = False
                    item.SubItems(3).ForeColor = Color.Red
                End If

                item.SubItems.Add(sPower)               '// 체결강도
                item.SubItems.Add(sCurPrice)            '// 현재가
                If CInt(sCurPrice) > 0 Then
                    item.UseItemStyleForSubItems = False
                    item.SubItems(5).ForeColor = Color.Red
                Else
                    item.UseItemStyleForSubItems = False
                    item.SubItems(5).ForeColor = Color.Blue
                End If
                item.SubItems.Add(CStr(nFindTotal))           '// 출현횟수

                'frmSign.lstSign.Items.Add(item)
                frmSign.lstSign.Items.Insert(0, item)

            End If
        Next

        gRecvCommandCount += 1
        gHashScreenNoAndStock.Remove(eventArgs.sScrNo)
        Console.WriteLine("Recv count {0}, {1}", gRecvCommandCount, sStockName)

    End Sub
    Private Sub trProcCompanyOnlyBuy(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)

        Dim nCnt As Integer, nIndex As Integer = 0
        Dim nCmp1 As Integer, nCmp2 As Integer, nCmp3 As Integer
        Dim strItemValue As String
        Dim sStockName As String
        Dim startPointInfo As StartPointInfo

        sStockName = gHashScreenNoAndStock(CStr(eventArgs.sScrNo))
        gHashScreenNoAndStock.Remove(eventArgs.sScrNo)

        nCnt = KHOpenAPI.GetRepeatCnt(eventArgs.sTrCode, eventArgs.sRQName)
        For i = 0 To (nCnt - 1)
            'strItemValue = KHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "회원사명")
            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "누적순매수수량")

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
        startPointInfo.setData(sStockName, "", "", "", nCmp1, nCmp2, nCmp3, 0, 0)
        gHashCompanyOnlyBuy.Add(Trim(sStockName), startPointInfo)
        gRecvCommandCount += 1
        Console.WriteLine("Recv {0}. {1}, {2}, {3}", sStockName, nCmp1, nCmp2, nCmp3)

    End Sub
    Private Sub trProcStartPointStockInfo(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)

        Dim nCnt As Short, i As Short
        Dim strItemValue As String, sStockName As String
        Dim stockBiz As StockBizInfo
        Dim nStockTotalCount As Integer = 0, nCompanyTotalValue As Integer = 0
        Dim nSaleProfitValue As Integer = 0, nSaleNetProfitValue As Integer = 0

        sStockName = gHashScreenNoAndStock(CStr(eventArgs.sScrNo))
        gHashScreenNoAndStock.Remove(eventArgs.sScrNo)

        nCnt = KHOpenAPI.GetRepeatCnt(eventArgs.sTrCode, eventArgs.sRQName)
        For i = 0 To (nCnt - 1)
            'strItemValue = Trim(KHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "종목코드"))
            'Console.WriteLine("종목코드 : " + strItemValue)

            'strItemValue = Trim(KHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "종목명"))
            'Console.WriteLine("종목명 : " + strItemValue)

            strItemValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "상장주식"))
            Console.WriteLine("상장주식 : " + strItemValue)
            If strItemValue.Length = 0 Then
                strItemValue = "0"
            End If
            nStockTotalCount = CInt(strItemValue)

            strItemValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "시가총액"))
            Console.WriteLine("시가총액 : " + strItemValue)
            If strItemValue.Length = 0 Then
                strItemValue = "0"
            End If
            nCompanyTotalValue = CInt(strItemValue)

            strItemValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "영업이익"))
            Console.WriteLine("영업이익 : " + strItemValue)
            If strItemValue.Length = 0 Then
                strItemValue = "0"
            End If
            nSaleProfitValue = CInt(strItemValue)

            strItemValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "당기순이익"))
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
        gRecvCommandCount += 1

    End Sub


    Private Sub trProcStockInfo(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim nCnt As Short, i As Short
        Dim strItemValue As String
        Dim lTotalOnlyBuy As Long = 0

        nCnt = KHOpenAPI.GetRepeatCnt(eventArgs.sTrCode, eventArgs.sRQName)
        For i = 0 To (nCnt - 1)
            strItemValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "종목코드"))
            Console.WriteLine("종목코드 : " + strItemValue)
            lstInfo.Items.Add("종목코드 : " + strItemValue)

            strItemValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "종목명"))
            Console.WriteLine("종목명 : " + strItemValue)
            lstInfo.Items.Add("종목명 : " + strItemValue)

            strItemValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "상장주식"))
            Console.WriteLine("상장주식 : " + strItemValue)
            lstInfo.Items.Add("상장주식 : " + strItemValue)

            strItemValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "시가총액"))
            Console.WriteLine("시가총액 : " + strItemValue)
            lstInfo.Items.Add("시가총액 : " + strItemValue)

            strItemValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "영업이익"))
            Console.WriteLine("영업이익 : " + strItemValue)
            lstInfo.Items.Add("영업이익 : " + strItemValue)

            strItemValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "당기순이익"))
            Console.WriteLine("당기순이익 : " + strItemValue)
            lstInfo.Items.Add("당기순이익 : " + strItemValue)

        Next
    End Sub
    Private Sub trProcBunBong(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim nCnt As Short, i As Short
        Dim nProcCnt As Integer = 0
        Dim strItemValue As String, sDate As String
        Dim nMaxTradeValue As Integer = 0, nTradeValue As Integer
        Dim comparison As StringComparison = StringComparison.InvariantCulture


        nCnt = KHOpenAPI.GetRepeatCnt(eventArgs.sTrCode, eventArgs.sRQName)
        For i = 0 To (nCnt - 1)
            strItemValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "거래량"))
            sDate = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "체결시간"))

            If sDate.EndsWith("153000") = False Then
                nTradeValue = CInt(Trim(strItemValue))
                If nMaxTradeValue < nTradeValue Then
                    nMaxTradeValue = nTradeValue
                    gsSPDate = sDate
                    gnSPStartV = CInt(Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "시가")))
                    gnSPEndV = CInt(Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "현재가")))
                End If
            End If

            'strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "체결시간")
            'Console.WriteLine("체결시간 : " + strItemValue)

            'strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "현재가")
            'Console.WriteLine("현재가 : " + strItemValue)

            'strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "시가")
            'Console.WriteLine("시가 : " + strItemValue)

            'strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "고가")
            'Console.WriteLine("고가 : " + strItemValue)

            'strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "저가")
            'Console.WriteLine("저가 : " + strItemValue)

            Application.DoEvents()

            '// 분봉 현재부터 몇개까지 분석할지 정한다.
            If gnFilterAnalCount <= nProcCnt Then
                Exit For
            End If

            nProcCnt += 1

        Next

        gRecvCommandCount += 1

        Console.WriteLine("분봉시작점날짜 {0}, 시가{1}, 종가{2}", gsSPDate, gnSPStartV, gnSPEndV)

    End Sub
    Private Sub trProcStartPointBunBong(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim nCnt As Short, i As Short
        Dim nProcCnt As Integer = 0
        Dim strItemValue As String, sDate As String
        Dim nMaxTradeValue As Integer = 0, nTradeValue As Integer
        Dim comparison As StringComparison = StringComparison.InvariantCulture


        nCnt = KHOpenAPI.GetRepeatCnt(eventArgs.sTrCode, eventArgs.sRQName)
        For i = 0 To (nCnt - 1)
            strItemValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "거래량"))
            sDate = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "체결시간"))

            If sDate.EndsWith("153000") = False Then '// 종가 체결량이 혼선을 줘서 뺐다.
                nTradeValue = CInt(Trim(strItemValue))
                If nMaxTradeValue < nTradeValue Then
                    nMaxTradeValue = nTradeValue
                    gsSPDate = sDate
                    gnSPStartV = CInt(Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "시가")))
                    gnSPEndV = CInt(Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "현재가")))
                End If
            End If

            'strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "체결시간")
            'Console.WriteLine("체결시간 : " + strItemValue)

            'strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "현재가")
            'Console.WriteLine("현재가 : " + strItemValue)

            'strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "시가")
            'Console.WriteLine("시가 : " + strItemValue)

            'strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "고가")
            'Console.WriteLine("고가 : " + strItemValue)

            'strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "저가")
            'Console.WriteLine("저가 : " + strItemValue)

            Application.DoEvents()

            If gnFilterAnalCount <= nProcCnt Then
                Exit For
            End If

            nProcCnt += 1

        Next

        gRecvCommandCount += 1

        Console.WriteLine("분봉시작점날짜 {0}, 시가{1}, 종가{2}", gsSPDate, gnSPStartV, gnSPEndV)

    End Sub

    Private Sub trProcCompanySellBuyHigh(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim nCnt As Short, i As Short
        Dim strItemValue As String
        Dim sCompany As String, sCode As String
        Dim lOnlyBuy As Long
        Dim lTotalOnlyBuy As Long = 0

        nCnt = KHOpenAPI.GetRepeatCnt(eventArgs.sTrCode, eventArgs.sRQName)
        For i = 0 To (nCnt - 1)
            sCode = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "종목코드")

            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "종목명")
            sCompany = Trim(strItemValue).Replace(" ", "").Replace(".", "")

            strItemValue = KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "순매수")
            lOnlyBuy = CInt(Trim(strItemValue))

            Console.WriteLine("받음 - {0} : {1}", sCompany, strItemValue)

        Next

        Console.WriteLine("========================================================================")

        'sStockName = ""
        'sStockName = gHashScreenNoAndStock(CStr(eventArgs.sScrNo))
        'If sStockName.Length = 0 Then
        '    MsgBox("스크린 번호에 해당되는 종목이 없습니다")
        '    Return
        'End If

        'gHashOnlyBuyValueAllStock.Add(sStockName, lTotalOnlyBuy)
        'gHashScreenNoAndStock.Remove(CStr(eventArgs.sScrNo))
        gRecvCommandCount = gRecvCommandCount + 1
        'Console.WriteLine("받음, {0} : {1}, 받은수 {2}", sStockName, CStr(lTotalOnlyBuy), CStr(gRecvCommandCount))
        Application.DoEvents()

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
        Dim item As ListViewItem = Nothing
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
    Private Sub trProcBunBongStockCompanySunCount(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim nCnt As Integer
        Dim nArrIndex As Integer = 0
        Dim strItemValue As String

        nCnt = KHOpenAPI.GetRepeatCnt(eventArgs.sTrCode, eventArgs.sRQName)
        For i = 0 To (nCnt - 1)
            strItemValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "회원사명"))
            System.Console.WriteLine("회원사 : " + strItemValue)

            strItemValue = Trim(KHOpenAPI.GetCommData(eventArgs.sTrCode, eventArgs.sRQName, i, "누적순매수수량")).Replace("+-", "").Replace(",", "").Replace("+", "")
            gnSinhoOnlyBuyCount(nArrIndex) = CInt(strItemValue)

            nArrIndex += 1

            If nArrIndex >= 3 Then
                Exit For
            End If
        Next

        gRecvCommandCount += 1

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
    Private Sub KHOpenAPI_OnReceiveRealData(sender As Object, e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent) Handles KHOpenAPI.OnReceiveRealData
        Dim strName As String
        Dim strSignDate As String
        Dim nSignValue As Integer
        Dim sHighLow As String
        Dim sCurPrice As String, nCurPrice As Integer
        Dim sStockCode As String
        Dim nTradePrice As Integer
        Dim nAppearanceCount As Integer
        Dim item As ListViewItem

        'Console.WriteLine("OnReceiveRealData::종목코드 : {0},  RealType : {1},  RealData : {2}", _
        '                  e.sRealKey, e.sRealType, e.sRealData)

        If e.sRealType = "주식체결" Then
            '// get 종목이름
            strName = Trim(KHOpenAPI.GetCommRealData(e.sRealType, 302))
            '// 체결량
            nSignValue = CInt(Trim(KHOpenAPI.GetCommRealData(e.sRealType, 15)).Replace("+", "").Replace("-", ""))
            '// 현재가
            sCurPrice = Trim(KHOpenAPI.GetCommRealData(e.sRealType, 10))
            nCurPrice = CInt(sCurPrice.Replace("-", "").Replace("+", ""))
            '// 거래대금
            nTradePrice = CInt(Trim(sCurPrice).Replace("+", "").Replace("-", "")) * nSignValue

            Console.WriteLine("[주식체결] 종목이름 : {0}, 체결시간 : {1}, 거래량 : {2}, 등락률 : {3}, 현재가 : {4} ", _
                              strName, _
                              KHOpenAPI.GetCommRealData(e.sRealType, 20), _
                              KHOpenAPI.GetCommRealData(e.sRealType, 15), _
                              KHOpenAPI.GetCommRealData(e.sRealType, 12), _
                              KHOpenAPI.GetCommRealData(e.sRealType, 10))

            '// 순간체결량 & 거래대금 조건이 맞아야 출력된다
            If nSignValue >= gnFilterSignValue And nTradePrice >= gnFilterTradePrice And _
                nCurPrice >= gnFilterAnalStartPrice And nCurPrice <= gnFilterAnalEndPrice Then

                strSignDate = Trim(KHOpenAPI.GetCommRealData(e.sRealType, 20))
                nSignValue = CInt(Trim(KHOpenAPI.GetCommRealData(e.sRealType, 15)))
                sHighLow = Trim(KHOpenAPI.GetCommRealData(e.sRealType, 12))

                '// 종목코드
                sStockCode = Trim(KHOpenAPI.GetCommRealData(e.sRealType, 9001))

                '// 순매수정보, 주가정보를 가져온다
                Call getSinhoData(sStockCode)

                'Console.WriteLine("[주식체결] 종목코드 : {0}, 체결시간 : {1}, 현재가 : {2}, 등락률 : {3}, 거래량 : {4} ", _
                '  strName, _
                '  KHOpenAPI.GetCommRealData(e.sRealType, 20), _
                '  KHOpenAPI.GetCommRealData(e.sRealType, 10), _
                '  KHOpenAPI.GetCommRealData(e.sRealType, 12), _
                '  KHOpenAPI.GetCommRealData(e.sRealType, 15))

                '// 종목명
                item = New ListViewItem(strName)

                '// 체결시간
                item.SubItems.Add(strSignDate)

                '// 체결량
                item.SubItems.Add(nSignValue)
                If CInt(nSignValue) > 0 Then
                    item.UseItemStyleForSubItems = False
                    item.SubItems(2).ForeColor = Color.Blue
                Else
                    item.UseItemStyleForSubItems = False
                    item.SubItems(2).ForeColor = Color.Red
                End If

                '// 현재가
                item.SubItems.Add(sCurPrice)
                If CInt(sCurPrice) > 0 Then
                    item.UseItemStyleForSubItems = False
                    item.SubItems(3).ForeColor = Color.Red
                Else
                    item.UseItemStyleForSubItems = False
                    item.SubItems(3).ForeColor = Color.Blue
                End If

                '// 거래대금
                item.SubItems.Add(CStr(nTradePrice))

                '// 분봉시작점
                If gnSinhoStartPointHow = 2 Then
                    item.SubItems.Add("상대근접")
                    item.UseItemStyleForSubItems = False
                    item.SubItems(5).ForeColor = Color.Blue
                ElseIf gnSinhoStartPointHow = 1 Then
                    item.SubItems.Add("하대근접")
                    item.UseItemStyleForSubItems = False
                    item.SubItems(5).ForeColor = Color.Blue
                Else
                    item.SubItems.Add("근접아님")
                End If

                '// 주가 등락
                item.SubItems.Add(sHighLow)
                If CInt(sHighLow) > 0 Then
                    item.UseItemStyleForSubItems = False
                    item.SubItems(6).ForeColor = Color.Blue
                Else
                    item.UseItemStyleForSubItems = False
                    item.SubItems(6).ForeColor = Color.Red
                End If

                '// 출현횟수
                If gHashSinhoAppearanceCount.Contains(sStockCode) = True Then
                    nAppearanceCount = gHashSinhoAppearanceCount(sStockCode)
                    nAppearanceCount += 1
                    gHashSinhoAppearanceCount.Remove(sStockCode)
                    gHashSinhoAppearanceCount.Add(sStockCode, nAppearanceCount)
                    item.SubItems.Add(CStr(nAppearanceCount))
                Else
                    nAppearanceCount = 1
                    gHashSinhoAppearanceCount.Add(sStockCode, nAppearanceCount)
                    item.SubItems.Add(CStr(nAppearanceCount))
                End If

                '// 거래량
                item.SubItems.Add(CStr(gnSinhoTradeValue))

                '// 주포1
                item.SubItems.Add(CStr(gnSinhoOnlyBuyCount(0)))

                '// 주포2
                item.SubItems.Add(CStr(gnSinhoOnlyBuyCount(1)))

                '// 주포3
                item.SubItems.Add(CStr(gnSinhoOnlyBuyCount(2)))

                '// 체결강도
                item.SubItems.Add(Trim(KHOpenAPI.GetCommRealData(e.sRealType, 228)))

                frmSign.lstSign.Items.Insert(0, item)

                Application.DoEvents()



                'lstInfo.Items.Insert(0, "[주식체결] 종목코드 : " + strName + ", 체결시간 : " + KHOpenAPI.GetCommRealData(e.sRealType, 20) + ", 거래량 : " + KHOpenAPI.GetCommRealData(e.sRealType, 15) + _
                '                  ", 등락률 : " + KHOpenAPI.GetCommRealData(e.sRealType, 12) + ", 현재가 : " + KHOpenAPI.GetCommRealData(e.sRealType, 10))

                'lstInfo.Items.Add("[주식체결] 종목코드 : " + strName + ", 체결시간 : " + KHOpenAPI.GetCommRealData(e.sRealType, 20) + ", 거래량 : " + KHOpenAPI.GetCommRealData(e.sRealType, 15) + _
                '                  ", 등락률 : " + KHOpenAPI.GetCommRealData(e.sRealType, 12) + ", 현재가 : " + KHOpenAPI.GetCommRealData(e.sRealType, 10))
            End If
        End If

    End Sub
    Private Sub KHOpenAPI_OnReceiveTrData(sender As Object, eventArgs As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent) Handles KHOpenAPI.OnReceiveTrData

        If eventArgs.sRQName = "종목별증권사순위요청기간1" Or eventArgs.sRQName = "종목별증권사순위요청기간2" Or eventArgs.sRQName = "종목별증권사순위요청기간3" Then
            Call trProcStockStandard(sender, eventArgs)
            KHOpenAPI.DisconnectRealData(eventArgs.sScrNo)
            Console.WriteLine("frmMain DisconnectRealData SrcNumber : " + eventArgs.sScrNo)
        ElseIf eventArgs.sRQName = "종목별증권사순위요청기간단순" Then
            Call trProcStock(sender, eventArgs)
            KHOpenAPI.DisconnectRealData(eventArgs.sScrNo)
            Console.WriteLine("frmMain DisconnectRealData SrcNumber : " + eventArgs.sScrNo)
        ElseIf eventArgs.sRQName = "일일기간분석" Then
            Call trProcSellBuyAnalData(sender, eventArgs)
            KHOpenAPI.DisconnectRealData(eventArgs.sScrNo)
            Console.WriteLine("frmMain DisconnectRealData SrcNumber : " + eventArgs.sScrNo)
        ElseIf eventArgs.sRQName = "주식기본정보요청" Then
            Call trProcTest(sender, eventArgs)
            KHOpenAPI.DisconnectRealData(eventArgs.sScrNo)
            Console.WriteLine("frmMain DisconnectRealData SrcNumber : " + eventArgs.sScrNo)
        ElseIf eventArgs.sRQName = "주식일봉차트조회" Then
            Call trStock900BongInfo(sender, eventArgs)
            KHOpenAPI.DisconnectRealData(eventArgs.sScrNo)
            Console.WriteLine("frmMain DisconnectRealData SrcNumber : " + eventArgs.sScrNo)
        ElseIf eventArgs.sRQName = "일일기간분석테스트" Then
            Call trProcSellBuyAnalDataTest(sender, eventArgs)
            KHOpenAPI.DisconnectRealData(eventArgs.sScrNo)
            Console.WriteLine("frmMain DisconnectRealData SrcNumber : " + eventArgs.sScrNo)
        ElseIf eventArgs.sRQName = "누적순매수정보추출" Then
            Call trProcSellBuyAnalDataAllStock(sender, eventArgs)
            KHOpenAPI.DisconnectRealData(eventArgs.sScrNo)
            Console.WriteLine("frmMain DisconnectRealData SrcNumber : " + eventArgs.sScrNo)
        ElseIf eventArgs.sRQName = "증권사별매매상위요청" Then
            Call trProcCompanySellBuyHigh(sender, eventArgs)
            KHOpenAPI.DisconnectRealData(eventArgs.sScrNo)
            Console.WriteLine("frmMain DisconnectRealData SrcNumber : " + eventArgs.sScrNo)
        ElseIf eventArgs.sRQName = "주식분봉차트조회요청" Then
            Call trProcBunBong(sender, eventArgs)
            KHOpenAPI.DisconnectRealData(eventArgs.sScrNo)
            Console.WriteLine("frmMain DisconnectRealData SrcNumber : " + eventArgs.sScrNo)
        ElseIf eventArgs.sRQName = "주식기본정보테스트" Then
            Call trProcStockInfo(sender, eventArgs)
            KHOpenAPI.DisconnectRealData(eventArgs.sScrNo)
            Console.WriteLine("frmMain DisconnectRealData SrcNumber : " + eventArgs.sScrNo)
        ElseIf eventArgs.sRQName = "체결정보요청" Then
            Call trProcStockSign(sender, eventArgs)
            KHOpenAPI.DisconnectRealData(eventArgs.sScrNo)
            Console.WriteLine("frmMain DisconnectRealData SrcNumber : " + eventArgs.sScrNo)
        ElseIf eventArgs.sRQName = "시작점종목별증권사순위" Then
            Call trProcCompanyOnlyBuy(sender, eventArgs)
            KHOpenAPI.DisconnectRealData(eventArgs.sScrNo)
            Console.WriteLine("frmMain DisconnectRealData SrcNumber : " + eventArgs.sScrNo)
        ElseIf eventArgs.sRQName = "시작점주식기본정보" Then
            Call trProcStartPointStockInfo(sender, eventArgs)
            KHOpenAPI.DisconnectRealData(eventArgs.sScrNo)
            Console.WriteLine("frmMain DisconnectRealData SrcNumber : " + eventArgs.sScrNo)
        ElseIf eventArgs.sRQName = "주식거래량" Then
            Call trProcStockTradeValue(sender, eventArgs)
            KHOpenAPI.DisconnectRealData(eventArgs.sScrNo)
            Console.WriteLine("frmMain DisconnectRealData SrcNumber : " + eventArgs.sScrNo)
        ElseIf eventArgs.sRQName = "시작점분봉차트조회" Then
            Call trProcStartPointBunBong(sender, eventArgs)
            KHOpenAPI.DisconnectRealData(eventArgs.sScrNo)
            Console.WriteLine("frmMain DisconnectRealData SrcNumber : " + eventArgs.sScrNo)
        ElseIf eventArgs.sRQName = "신호등종목별증권사조회" Then
            Call trProcBunBongStockCompanySunCount(sender, eventArgs)
            KHOpenAPI.DisconnectRealData(eventArgs.sScrNo)
            Console.WriteLine("frmMain DisconnectRealData SrcNumber : " + eventArgs.sScrNo)
        End If

    End Sub

    Private Sub btnCmd1_Click(sender As Object, e As EventArgs) Handles btnCmd1.Click

    End Sub

    Public Sub btnCmd2_Click(sender As Object, e As EventArgs) Handles btnCmd2.Click
        If bLoginStatus = False Then
            MsgBox("로그인이 필요합니다!!")
            Return
        End If

        If Trim(txtStockCode.Text).Length = 0 Then
            MsgBox("선택된 종목이 없습니다")
            Return
        End If

        If Trim(txtStartDate1.Text).Length <> 8 Then
            MsgBox("분석 시작 날짜를 20160101 처럼 8자리의 숫자로 넣어 주세요")
            Return
        End If

        If Trim(txtEndDate1.Text).Length <> 8 Then
            MsgBox("분석 종료 날짜를 20160101 처럼 8자리의 숫자로 넣어 주세요")
            Return
        End If

        If Trim(txtStockCode.Text).Length <> 6 Then
            MsgBox("종목코드 입력이 6자리가 아닙니다. 종목을 다시 입력해 주세요")
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

    Public Sub btnCmd3_Click(sender As Object, e As EventArgs) Handles btnCmd3.Click
        If bLoginStatus = False Then
            MsgBox("로그인이 필요합니다!!")
            Return
        End If

        If Trim(txtStockCode.Text).Length = 0 Then
            MsgBox("선택된 종목이 없습니다")
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

        If bLoginStatus = False Then
            MsgBox("로그인이 필요합니다!!")
            Return
        End If

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
            Threading.Thread.Sleep(nRequestDelayTime)

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

    Public Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If bLoginStatus = False Then
            MsgBox("로그인이 필요합니다!!")
            Return
        End If

        If Trim(txtStockCode.Text).Length = 0 Then
            MsgBox("선택된 종목이 없습니다")
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
        Dim MyKeys As ICollection
        Dim Key As Object
        Dim nScreenNumber As Integer = 1000
        Dim nProgressMax As Integer, nPorgresValue As Integer = 0

        If bLoginStatus = False Then
            MsgBox("로그인이 필요합니다!!")
            Return
        End If

        bStop = False
        gSendCommandCount = 0
        gHashOnlyBuyValueAllStock.Clear()
        gHashScreenNoAndStock.Clear()

        '// Prograss Bar 셋팅
        nProgressMax = gKosdaqStockCodeTable.Count
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = nProgressMax

        '// 코스닥 정보만 구한다.
        MyKeys = gKosdaqStockCodeTable.Keys()
        For Each Key In MyKeys
            If nScreenNumber >= 1099 Then
                nScreenNumber = 1000
            Else
                nScreenNumber += 1
            End If

            KHOpenAPI.SetInputValue("종목코드", gKosdaqStockCodeTable(Key.ToString))
            KHOpenAPI.CommRqData("당일주요거래원", "OPT10040", CInt("0"), CStr(nScreenNumber))

            '// 스크린넘버와 종목명 매핑

            gHashScreenNoAndStock.Add(CStr(nScreenNumber), Key.ToString)
            '// 서버에 보낸 요청 개수
            gSendCommandCount += 1

            Threading.Thread.Sleep(nRequestDelayTime)
            Application.DoEvents()
            Console.WriteLine("서버 명령 종목:{0}, 코드:{1}, 요청수{2}", Key.ToString, gStockCodeTable(Key.ToString), CStr(gSendCommandCount))
            nPorgresValue += 1
            ProgressBar1.Value = nPorgresValue

            If bStop = True Then
                MsgBox("멈춤!!")
                bStop = False
                Exit For
            End If
        Next

        Dim totalRetryCount As Integer = 0
        Do While True
            If gSendCommandCount <= gRecvCommandCount Then
                Exit Do
            End If
            Threading.Thread.Sleep(nRequestDelayTime)
            Console.WriteLine("Wait OnRecvTRdata...S[" + CStr(gSendCommandCount) + "], R[" + CStr(gRecvCommandCount) + "]")
            Application.DoEvents()
            totalRetryCount += 1
            If totalRetryCount >= 100 Then
                MsgBox("데이터를 서버로 부터 모두 받지 못했습니다. 다시한번 실행해 주세요!")
                Return
            End If
        Loop

        Console.WriteLine("=============================================================================")
        Console.WriteLine("=============================================================================")
        Console.WriteLine("=============================================================================")
        Console.WriteLine("=============================================================================")
        Console.WriteLine("순매수 많은 종목순")

        '// 순매수 정보가 많은 순으로 정렬한다.
        Dim lstSortedOnlyBuyStockAll As New List(Of String)()
        Dim hashLColl As ICollection
        Dim hashTemp As New Hashtable

        '// 해시 테이블 복사
        hashLColl = gHashOnlyBuyValueAllStock.Keys
        For Each Key In hashLColl
            hashTemp.Add(Key.ToString, gHashOnlyBuyValueAllStock(Key.ToString))
        Next

        '// 임시 해시테이블을 이용해서 hash value로 정렬한다.
        lstSortedOnlyBuyStockAll = GlobalDefine.sortHashtable(hashTemp)
        '// 정렬된 list 순으로 값을 찍어라
        lstInfo.Items.Clear()
        For Each _sStockName In lstSortedOnlyBuyStockAll
            lstInfo.Items.Add(_sStockName + ", " + CStr(gHashOnlyBuyValueAllStock(_sStockName)))
            Console.WriteLine("{0}, {1}", _sStockName, gHashOnlyBuyValueAllStock(_sStockName))
        Next
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
        Dim nScreenNumber As Integer
        Dim strStartDate As String
        Dim strStartY, strStartM, strStartD As String
        Dim nPorgresValue As Integer = 0
        Dim nProgressMax As Integer = 0
        Dim googleChart As New GoogleChart

        If bLoginStatus = False Then
            MsgBox("로그인이 필요합니다!!")
            Return
        End If

        If Trim(txtStockCode.Text).Length = 0 Then
            MsgBox("선택된 종목이 없습니다")
            Return
        End If

        If Trim(txtStockCode.Text).Length = 0 Then
            MsgBox("선택된 종목이 없습니다")
            Return
        End If

        If Trim(txtStartDate1.Text).Length <> 8 Then
            MsgBox("분석 시작 날짜를 20160101 처럼 8자리의 숫자로 넣어 주세요")
            Return
        End If

        If Trim(txtEndDate1.Text).Length <> 8 Then
            MsgBox("분석 종료 날짜를 20160101 처럼 8자리의 숫자로 넣어 주세요")
            Return
        End If

        If Trim(txtStockCode.Text).Length <> 6 Then
            MsgBox("종목코드 입력이 6자리가 아닙니다. 종목을 다시 입력해 주세요")
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

            Threading.Thread.Sleep(nRequestDelayTime)

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
            Threading.Thread.Sleep(nRequestDelayTime)
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
        'Call googleChart.drawGoogleChartTest()
        Call googleChart.drawHighStockChart()
        'Call googleChart.drawHighNormalChart()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        GlobalDefine.runBrowser("F:\googleChart.html")
    End Sub

    Private Sub btnOnlySellBuyAll_Click(sender As Object, e As EventArgs) Handles btnOnlySellBuyAll.Click
        Dim MyKeys As ICollection
        Dim Key As Object
        Dim nScreenNumber As Integer = 1000
        Dim nProgressMax As Integer, nPorgresValue As Integer = 0

        If bLoginStatus = False Then
            MsgBox("로그인이 필요합니다!!")
            Return
        End If

        bStop = False
        gSendCommandCount = 0
        gHashOnlyBuyValueAllStock.Clear()
        gHashScreenNoAndStock.Clear()

        '// Prograss Bar 셋팅
        nProgressMax = gKosdaqStockCodeTable.Count
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = nProgressMax

        '// 코스닥 정보만 구한다.
        MyKeys = gKosdaqStockCodeTable.Keys()
        For Each Key In MyKeys
            If nScreenNumber >= 1099 Then
                nScreenNumber = 1000
            Else
                nScreenNumber += 1
            End If

            KHOpenAPI.SetInputValue("종목코드", gKosdaqStockCodeTable(Key.ToString))
            KHOpenAPI.SetInputValue("수정주가구분", "1")
            KHOpenAPI.SetInputValue("조회구분", "0")
            KHOpenAPI.SetInputValue("시작일자", txtStartDate1.Text)
            KHOpenAPI.SetInputValue("종료일자", txtEndDate1.Text)
            KHOpenAPI.CommRqData("누적순매수정보추출", "OPT10038", CInt("0"), CStr(nScreenNumber))

            '// 스크린넘버와 종목명 매핑

            gHashScreenNoAndStock.Add(CStr(nScreenNumber), Key.ToString)
            '// 서버에 보낸 요청 개수
            gSendCommandCount += 1

            Threading.Thread.Sleep(nRequestDelayTime)
            Application.DoEvents()
            Console.WriteLine("서버 명령 종목:{0}, 코드:{1}, 요청수{2}", Key.ToString, gStockCodeTable(Key.ToString), CStr(gSendCommandCount))
            nPorgresValue += 1
            ProgressBar1.Value = nPorgresValue

            If bStop = True Then
                MsgBox("멈춤!!")
                bStop = False
                Exit For
            End If
        Next

        Dim totalRetryCount As Integer = 0
        Do While True
            If gSendCommandCount <= gRecvCommandCount Then
                Exit Do
            End If
            Threading.Thread.Sleep(nRequestDelayTime)
            Console.WriteLine("Wait OnRecvTRdata...S[" + CStr(gSendCommandCount) + "], R[" + CStr(gRecvCommandCount) + "]")
            Application.DoEvents()
            totalRetryCount += 1
            If totalRetryCount >= 100 Then
                MsgBox("데이터를 서버로 부터 모두 받지 못했습니다. 다시한번 실행해 주세요!")
                Return
            End If
        Loop

        Console.WriteLine("=============================================================================")
        Console.WriteLine("=============================================================================")
        Console.WriteLine("=============================================================================")
        Console.WriteLine("=============================================================================")
        Console.WriteLine("순매수 많은 종목순")

        '// 순매수 정보가 많은 순으로 정렬한다.
        Dim lstSortedOnlyBuyStockAll As New List(Of String)()
        Dim hashLColl As ICollection
        Dim hashTemp As New Hashtable

        '// 해시 테이블 복사
        hashLColl = gHashOnlyBuyValueAllStock.Keys
        For Each Key In hashLColl
            hashTemp.Add(Key.ToString, gHashOnlyBuyValueAllStock(Key.ToString))
        Next

        '// 임시 해시테이블을 이용해서 hash value로 정렬한다.
        lstSortedOnlyBuyStockAll = GlobalDefine.sortHashtable(hashTemp)

        '// 정렬된 list 순으로 값을 찍어라
        '// 파일로도 남긴다.
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter("c:\temp\daeching\SunMaeDoSort.txt", True)
        lstInfo.Items.Clear()
        For Each _sStockName In lstSortedOnlyBuyStockAll
            lstInfo.Items.Add(_sStockName + ", " + CStr(gHashOnlyBuyValueAllStock(_sStockName)))
            Console.WriteLine("{0}, {1}", _sStockName, gHashOnlyBuyValueAllStock(_sStockName))
            file.WriteLine("{0}, {1}", _sStockName, gHashOnlyBuyValueAllStock(_sStockName))
        Next

        file.Close()

    End Sub

    Private Sub btnStockCompanySort_Click(sender As Object, e As EventArgs) Handles btnStockCompanySort.Click
        Dim chartSeries As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim nScreenNumber As Integer
        Dim nPorgresValue As Integer = 0
        Dim nProgressMax As Integer = 0
        Dim googleChart As New GoogleChart

        If bLoginStatus = False Then
            MsgBox("로그인이 필요합니다!!")
            Return
        End If

        If Trim(txtStockCode.Text).Length = 0 Then
            MsgBox("선택된 종목이 없습니다")
            Return
        End If

        '// 미리 증권사 목록을 셋팅하고 0 값을 셋팅한다
        Dim k As Integer
        For k = 0 To gListStockCompanyCode.Count - 1
            gCompanySellBuyCount.Add(gListStockCompanyCode.Item(k), 0)
        Next

        gSendCommandCount = 0
        gRecvCommandCount = 0
        nScreenNumber = 1000

        '// progress bar setting min, max
        'ProgressBar1.Minimum = 0
        'ProgressBar1.Maximum = nProgressMax

        Dim MyKeys As ICollection
        Dim Key As Object
        MyKeys = gStockCompanyCodeTable.Keys()
        For Each Key In MyKeys
            If nScreenNumber >= 1099 Then
                nScreenNumber = 1000
            Else
                nScreenNumber += 1
            End If

            KHOpenAPI.SetInputValue("회원사코드", gStockCompanyCodeTable(Key.ToString))
            KHOpenAPI.SetInputValue("거래량구분", "0")
            KHOpenAPI.SetInputValue("매매구분", "1")
            KHOpenAPI.SetInputValue("기간", "5")
            KHOpenAPI.CommRqData("증권사별매매상위요청", "OPT10039", CInt("0"), CStr(nScreenNumber))

            Console.WriteLine("{0} : {1}", Key.ToString, gStockCompanyCodeTable(Key.ToString))
            Threading.Thread.Sleep(nRequestDelayTime)
            Application.DoEvents()
            gSendCommandCount = gSendCommandCount + 1
            '// progress bar +1 증가
            nPorgresValue += 1
            'ProgressBar1.Value = nPorgresValue

            'gHashScreenNoAndStock.Add(Key.ToString, CStr(nScreenNumber))

        Next

        Console.WriteLine("서버에 요청 완료")

        '// 데이터 다 받을때까지 대기.
        Dim totalRetryCount As Integer = 0
        Do While True
            If gSendCommandCount <= gRecvCommandCount Then
                Exit Do
            End If
            Threading.Thread.Sleep(nRequestDelayTime)
            Console.WriteLine("Wait OnRecvTRdata...S[" + CStr(gSendCommandCount) + "], R[" + CStr(gRecvCommandCount) + "]")
            Application.DoEvents()
            totalRetryCount += 1
            If totalRetryCount >= 100 Then
                MsgBox("데이터를 서버로 부터 모두 받지 못했습니다. 다시한번 실행해 주세요!")
                Return
            End If
        Loop

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim sMsg As String

        sMsg = KHOpenAPI.GetMasterStockState(txtStockCode.Text)
        MsgBox(sMsg)

    End Sub

    Public Sub btnMinBong_Click(sender As Object, e As EventArgs) Handles btnMinBong.Click
        Dim tt As TimeStd

        tt = New TimeStd
        tt.startTime()

        Call getBunBongStartPoint()

        tt.endTime()

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        bStop = True
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Dim key As Object
        Dim nScreenNumber As Integer = 1000
        Dim gKosdaqStockCodeTableKeys As ICollection
        Dim nProcValue As Integer = 0

        bStop = False

        If bLoginStatus = False Then
            MsgBox("로그인이 필요합니다!!")
            Return
        End If

        If Trim(txtSPToday.Text).Length = 0 Then
            MsgBox("시작점 찾을 기준날짜를 입력하세요")
            Return
        End If

        If Trim(txtJupoStartDate.Text).Length = 0 Then
            MsgBox("주포 매집수 분석을 위한 시작날짜를 입력하세요")
            Return
        End If

        If Trim(txtJupoEndDate.Text).Length = 0 Then
            MsgBox("주포 매집수 분석을 위한 종료날짜를 입력하세요")
            Return
        End If

        gSendCommandCount = 0
        gRecvCommandCount = 0
        gHashSijaMain.Clear()
        gHashScreenNoAndStock.Clear()
        gHashStockStatus.Clear() '// 주식 상태 정보
        gHashNameByCurPrice.Clear()   '// 시작점 기준일 현재가격

        '// progress bar
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = gKosdaqStockCodeTable.Count + 1

        '// 주가 범위
        gnStartPrice = CInt(Trim(txtStartPrice.Text))
        gnEndPrice = CInt(Trim(txtEndPrice.Text))

        '// 코스닥 종목에 대해서 일봉 데이터를 가져온다.
        gKosdaqStockCodeTableKeys = gKosdaqStockCodeTable.Keys
        For Each key In gKosdaqStockCodeTableKeys
            If key.ToString.Contains("스팩") = False Then

                If nScreenNumber >= 1099 Then
                    nScreenNumber = 1000
                End If

                gHashScreenNoAndStock.Add(CStr(nScreenNumber), key.ToString)

                KHOpenAPI.SetInputValue("종목코드", gKosdaqStockCodeTable(key.ToString))
                KHOpenAPI.SetInputValue("기준일자", Trim(txtEndDate1.Text))
                KHOpenAPI.SetInputValue("수정주가구분", "1")
                KHOpenAPI.CommRqData("주식일봉차트조회", "OPT10081", CInt("0"), CStr(nScreenNumber))
                Threading.Thread.Sleep(nRequestDelayTime)
                Application.DoEvents()

                gSendCommandCount += 1
                nScreenNumber += 1
                nProcValue += 1
                ProgressBar1.Value = nProcValue
                Console.WriteLine("Send count {0}, {1}", gSendCommandCount, key.ToString)
                If bStop = True Then
                    MsgBox("멈춤!!")
                    Exit For
                End If

            End If '// 스팩 skip
        Next

        '// 일봉 데이터 다 받을때까지 대기.
        Dim totalRetryCount As Integer = 0
        Do While True
            If gSendCommandCount <= gRecvCommandCount Then
                Exit Do
            End If
            Threading.Thread.Sleep(nRequestDelayTime)
            Console.WriteLine("Wait OnRecvTRdata...S[" + CStr(gSendCommandCount) + "], R[" + CStr(gRecvCommandCount) + "]")
            Application.DoEvents()
            totalRetryCount += 1
            If totalRetryCount >= 100 Then
                MsgBox("데이터를 서버로 부터 모두 받지 못했습니다. 다시한번 실행해 주세요!")
                Return
            End If
        Loop

        '// 새창으로 시작점 계산 후 보여준다.
        'frmStartPoint.Show()

        '// 시작점 찾기
        Call findStartPoint()

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        MsgBox(GlobalDefine.getBeforeAfterDate(-100))
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        KHOpenAPI.SetRealReg("4000", "039490;080580", "10;12", "0")
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        frmAPI.Show()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim msg As String
        msg = KHOpenAPI.GetMasterStockState(Trim(txtStockCode.Text))
        MsgBox(msg)
    End Sub

    Public Sub Button13_Click(sender As Object, e As EventArgs) Handles btnStockInitInfo.Click
        KHOpenAPI.SetInputValue("종목코드", txtStockCode.Text)
        KHOpenAPI.CommRqData("주식거래량", "opt10001", "0", 1000)
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim MyKeys As ICollection
        Dim nScreenNumber As Integer = 0
        Dim nProcValue As Integer = 0

        If bLoginStatus = False Then
            MsgBox("로그인이 필요합니다!!")
            Return
        End If

        '// 체결량 정보 출력용 form
        frmSign.Show()
        frmSign.lstSign.Items.Clear()

        bStop = False
        gSendCommandCount = 0
        gRecvCommandCount = 0
        gHashScreenNoAndStock.Clear()
        '// progress bar
        frmSign.pBar.Minimum = 0
        frmSign.pBar.Maximum = gKosdaqStockCodeTable.Count

        '// 체결량 정보를 뿌린다.
        frmSign.lstSign.Columns(0).TextAlign = HorizontalAlignment.Center   '// 종목명
        frmSign.lstSign.Columns(1).TextAlign = HorizontalAlignment.Center   '// 체결시간
        frmSign.lstSign.Columns(2).TextAlign = HorizontalAlignment.Center   '// 체결량
        frmSign.lstSign.Columns(3).TextAlign = HorizontalAlignment.Center   '// 주가등락
        frmSign.lstSign.Columns(4).TextAlign = HorizontalAlignment.Center   '// 체결강도
        frmSign.lstSign.Columns(5).TextAlign = HorizontalAlignment.Center   '// 현재가
        frmSign.lstSign.Columns(6).TextAlign = HorizontalAlignment.Center   '// 출현횟수

        MyKeys = gKosdaqStockCodeTable.Keys()
        For Each Key In MyKeys
            If nScreenNumber >= 1099 Then
                nScreenNumber = 1000
            End If

            gHashScreenNoAndStock.Add(CStr(nScreenNumber), Key.ToString)

            KHOpenAPI.SetInputValue("종목코드", gKosdaqStockCodeTable(Key.ToString))
            KHOpenAPI.CommRqData("체결정보요청", "opt10003", "0", nScreenNumber)
            Threading.Thread.Sleep(nRequestDelayTime)
            Application.DoEvents()

            gSendCommandCount += 1
            nScreenNumber += 1
            '// progress bar
            nProcValue += 1
            frmSign.pBar.Value = nProcValue
            Console.WriteLine("Send count {0}, {1}", gSendCommandCount, Key.ToString)
            If bStop = True Then
                MsgBox("스톱!!")
                Exit For
            End If

        Next

        '// 데이터 다 받을때까지 대기.
        Dim totalRetryCount As Integer = 0
        Do While True
            If gSendCommandCount <= gRecvCommandCount Then
                Exit Do
            End If
            Threading.Thread.Sleep(nRequestDelayTime)
            Console.WriteLine("Wait OnRecvTRdata...S[" + CStr(gSendCommandCount) + "], R[" + CStr(gRecvCommandCount) + "]")
            Application.DoEvents()
            totalRetryCount += 1
            If totalRetryCount >= 100 Then
                MsgBox("데이터를 서버로 부터 모두 받지 못했습니다. 다시한번 실행해 주세요!")
                Return
            End If
        Loop

    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        lstInfo.Items.Clear()
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Dim sgPer As Single, sgPer100 As Single, sgIncreaseVal As Single
        Dim sgMaxVal As Single, sgMinVal As Single
        Dim a As Integer, b As Integer

        a = 1260
        b = 3140

        sgPer = Convert.ToSingle(Trim(txtSPPer.Text))
        sgPer100 = sgPer / 100

        Console.WriteLine("sgPer {0}", sgPer)
        Console.WriteLine("sgPer100 {0}", sgPer100)

        sgIncreaseVal = a * sgPer100
        sgMaxVal = a + sgIncreaseVal
        sgMinVal = a - sgIncreaseVal
        Console.WriteLine("{0}, {1}, {2}", sgMaxVal, a, sgMinVal)

        sgIncreaseVal = b * sgPer100
        sgMaxVal = b + sgIncreaseVal
        sgMinVal = b - sgIncreaseVal
        Console.WriteLine("{0}, {1}, {2}", sgMaxVal, b, sgMinVal)

    End Sub
    Sub findStartPoint()
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
        Dim nTradeCount As Integer
        Dim nMaxTV As Integer
        Dim stockCode As String
        Dim sMsg As String
        Dim nTotalFindedStartPointStock As Integer = 0
        '// 근접 퍼센트 계산
        Dim sgPer As Single, sgPer100 As Single, sgIncreaseVal As Single
        Dim sgMaxPrice As Single, sgMinPrice As Single

        '/////////////////////////////////////////////////////////////////////////////////////////////
        '// 시작점 찾기
        '/////////////////////////////////////////////////////////////////////////////////////////////
        gListSPHadaeNear.Clear()
        gListSPHadaeSurpass.Clear()
        gListSPHadeaDrop.Clear()
        gListSPSangdaeDrop.Clear()
        gListSPSangdaeNear.Clear()
        gListSPSangdaeOver.Clear()
        gListSPSangdaeSurpass.Clear()
        gListSPSangHaSurpass.Clear()
        gListSPSangHaDrop.Clear()
        gHashStockStatus.Clear()

        '// progress bar를 위해서 미리 처리할 개수를 count 한다.
        Dim nProgressCount As Integer = 0
        gHashSijaMainKeys = gHashSijaMain.Keys
        For Each key In gHashSijaMainKeys
            listStockValueInfo = gHashSijaMain(key.ToString)
            For Each stockValueInfo In listStockValueInfo
                nProgressCount += 1
            Next
        Next


        '// frm loading.
        frmStartPoint.Show()


        '// progress bar 초기화
        frmStartPoint.ProgressBar1.Minimum = 0
        frmStartPoint.ProgressBar1.Maximum = nProgressCount
        Dim nProgressValue As Integer = 0
        Dim sStockName As String

        '// 시작점을 찾고, 오늘 주가의 시가, 종가를 저장한다
        frmStartPoint.lbMsg1.Text = "시작점 찾는 중..."
        gHashSijaMainKeys = gHashSijaMain.Keys
        For Each key In gHashSijaMainKeys
            listStockValueInfo = gHashSijaMain(key.ToString)
            nMaxTV = 0
            For Each stockValueInfo In listStockValueInfo

                If nMaxTV < stockValueInfo.getTradeV Then
                    maxStockValueInfo.setStockValue(stockValueInfo.getCurDate, stockValueInfo.getStartV, stockValueInfo.getEndV, stockValueInfo.getTradeV, stockValueInfo.getName, "")
                    nMaxTV = stockValueInfo.getTradeV
                End If

                '// 오늘 주가 정보 저장 - 오늘 시작점 위치인것 찾을때 사용
                '// 주가 시작 ~ 끝 사이에 있는 주식만 찾는다
                If txtSPToday.Text = stockValueInfo.getCurDate Then
                    stockVInfo = New StockValueInfo
                    stockVInfo.setStockValue(stockValueInfo.getCurDate, stockValueInfo.getStartV, stockValueInfo.getEndV, stockValueInfo.getTradeV, stockValueInfo.getName, "")
                    hashTodayStartEndValue.Add(stockValueInfo.getName, stockVInfo)
                    '// 종목별 현재가 저장
                    gHashNameByCurPrice.Add(stockValueInfo.getName, stockValueInfo.getEndV)
                End If

                '// progress bar add
                nProgressValue += 1
                frmStartPoint.ProgressBar1.Value = nProgressValue
                frmStartPoint.lbMsg2.Text = stockValueInfo.getName
                Application.DoEvents()
                'Console.WriteLine("{0},{1},{2},{3},{4}", key.ToString, stockValueInfo.getCurDate, stockValueInfo.getTradeV, stockValueInfo.getStartV, stockValueInfo.getEndV)
            Next

            '// 종목에 대한 정보를 api를 통해서 가져와서 저장.
            sStockName = maxStockValueInfo.getName
            If sStockName <> Nothing Then
                stockCode = gStockCodeTable(sStockName)
                If stockCode <> Nothing Then
                    If gHashStockStatus.Contains(stockValueInfo.getName) = False Then
                        sMsg = KHOpenAPI.GetMasterStockState(stockCode)
                        gHashStockStatus.Add(stockValueInfo.getName, sMsg)

                        '// 찾은 시작점 list에 추가.
                        stockVInfo = New StockValueInfo
                        stockVInfo.setStockValue(maxStockValueInfo.getCurDate, maxStockValueInfo.getStartV, maxStockValueInfo.getEndV, maxStockValueInfo.getTradeV, maxStockValueInfo.getName, "")
                        listSijackStockValueInfo.Add(stockVInfo)
                        Console.WriteLine("{0} {1}", stockValueInfo.getName, sMsg)
                    End If
                Else
                    '// 정보없는 종목은 삭제한다. 주식 정보가 없기 때문에 이상한 종목임.
                    hashTodayStartEndValue.Remove(stockValueInfo.getName)
                    Console.WriteLine("종목코드없음:" + stockValueInfo.getName)
                End If
            Else
                Console.WriteLine("종목 코드가 없습니다 - {0}", sStockName)
            End If

        Next

        Console.WriteLine("================================================================")
        Console.WriteLine("===================== 종목별 시작점 날짜 =======================")
        Console.WriteLine("================================================================")

        '// 동일 종목을 계산하지 않음.
        hashPrintedStock.Clear()

        If listSijackStockValueInfo.Count = 0 Then
            MsgBox("시작점 데이터를 찾지 못했습니다.")
            Return
        End If

        '// progress bar 초기화
        nProgressValue = 0
        frmStartPoint.ProgressBar1.Minimum = 0
        frmStartPoint.ProgressBar1.Maximum = listSijackStockValueInfo.Count

        '// 근접 찾기 위한 percent 계산
        sgPer = Convert.ToSingle(Trim(txtSPPer.Text))
        sgPer100 = sgPer / 100
        '// 기준 거래량
        nTradeCount = CInt(Trim(txtSPTradeCount.Text))

        Dim nPrintCount As Integer = 0
        Dim sName As String

        '////////////////////////////////////////////////////
        '// 오늘 주가가 시작점 위치인것 찾기
        '////////////////////////////////////////////////////
        frmStartPoint.lbMsg1.Text = "시작점 종목 찾는중..."
        For Each stockStartValue In listSijackStockValueInfo
            '// 시작점의 어떤 종목
            sName = stockStartValue.getName
            '// 시작점 종목의 오늘 주가 정보
            stockTodayValue = hashTodayStartEndValue(sName)
            frmStartPoint.lbMsg2.Text = sName

            '// 거래량 100만 이하는 skip
            If stockTodayValue.getTradeV < nTradeCount Then
                Continue For
            End If

            '/////////////////////////////////////////////////////////////////////////////////////////////////////
            '// 시작점 상승, 오늘주가 상승
            '/////////////////////////////////////////////////////////////////////////////////////////////////////
            If stockStartValue.getStartV <= stockStartValue.getEndV And stockTodayValue.getStartV <= stockTodayValue.getEndV Then
                '// 시작점 상대 돌파
                If stockStartValue.getEndV <= stockTodayValue.getEndV And _
                    stockStartValue.getEndV >= stockTodayValue.getStartV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockTodayValue.getStartV)
                    gListSPSangdaeSurpass.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("상대돌파 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 시작점 하대 돌파
                If stockStartValue.getStartV <= stockTodayValue.getEndV And _
                    stockStartValue.getStartV >= stockTodayValue.getStartV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getStartV)
                    gListSPHadaeSurpass.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("하대돌파 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 근접 계산을 위한 퍼센트 계산
                sgIncreaseVal = stockTodayValue.getEndV * sgPer100
                sgMaxPrice = stockTodayValue.getEndV + sgIncreaseVal
                sgMinPrice = stockTodayValue.getEndV - sgIncreaseVal

                '// 오늘 종가가 시작점 상대 근접한 경우
                If sgMaxPrice >= stockStartValue.getEndV And sgMinPrice <= stockStartValue.getEndV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getEndV)
                    gListSPSangdaeNear.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("상대근접 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 오늘 종가가 시작점 하대 근접한 경우
                If sgMaxPrice >= stockStartValue.getStartV And sgMinPrice <= stockStartValue.getStartV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getEndV)
                    gListSPHadaeNear.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("하대근접 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 상대 이상 찾기
                If stockStartValue.getEndV <= stockTodayValue.getStartV And _
                    stockStartValue.getEndV <= stockTodayValue.getEndV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getEndV)
                    gListSPSangdaeOver.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("상대이상 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 대박 돌파
                If stockStartValue.getStartV >= stockTodayValue.getStartV And _
                    stockStartValue.getEndV <= stockTodayValue.getEndV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getStartV)
                    gListSPSangHaSurpass.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("대박돌파 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If
            ElseIf stockStartValue.getStartV <= stockStartValue.getEndV And stockTodayValue.getStartV >= stockTodayValue.getEndV Then
                '///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                '// 시작점 상승 & 오늘 주가 하락
                '///////////////////////////////////////////////////////////////////////////////////////////////////////////////

                '// 시작점 상대 하향
                If stockStartValue.getEndV <= stockTodayValue.getStartV And _
                    stockStartValue.getEndV >= stockTodayValue.getEndV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getEndV)
                    gListSPSangdaeDrop.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("상대하향 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 시작점 하대 하향
                If stockStartValue.getStartV <= stockTodayValue.getStartV And _
                    stockStartValue.getStartV >= stockTodayValue.getEndV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getStartV)
                    gListSPHadeaDrop.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("하대하향 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 근접 계산을 위한 퍼센트 계산
                sgIncreaseVal = stockTodayValue.getEndV * sgPer100
                sgMaxPrice = stockTodayValue.getEndV + sgIncreaseVal
                sgMinPrice = stockTodayValue.getEndV - sgIncreaseVal

                '// 오늘 종가가 시작점 상대 근접한 경우
                If sgMaxPrice >= stockStartValue.getEndV And sgMinPrice <= stockStartValue.getEndV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getEndV)
                    gListSPSangdaeNear.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("상대근접 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 오늘 종가가 시작점 하대 근접한 경우
                If sgMaxPrice >= stockStartValue.getStartV And sgMinPrice <= stockStartValue.getStartV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getStartV)
                    gListSPHadaeNear.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("하대근접 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 상대 이상 찾기
                If stockStartValue.getEndV <= stockTodayValue.getStartV And _
                    stockStartValue.getEndV <= stockTodayValue.getEndV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getEndV)
                    gListSPSangdaeOver.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("상대이상 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 대박 하향
                If stockStartValue.getEndV <= stockTodayValue.getStartV And _
                    stockStartValue.getStartV >= stockTodayValue.getEndV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getEndV)
                    gListSPSangHaDrop.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("대박하향 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If
            ElseIf stockStartValue.getStartV >= stockStartValue.getEndV And stockTodayValue.getStartV <= stockTodayValue.getEndV Then
                '///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                '// 시작점 하락 & 오늘 주가 상승
                '///////////////////////////////////////////////////////////////////////////////////////////////////////////////

                '// 시작점 상대 돌파
                If stockStartValue.getStartV <= stockTodayValue.getEndV And _
                    stockStartValue.getStartV >= stockTodayValue.getStartV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getStartV)
                    gListSPSangdaeSurpass.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("상대돌파 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 시작점 하대 돌파
                If stockStartValue.getEndV <= stockTodayValue.getEndV And _
                    stockStartValue.getEndV >= stockTodayValue.getStartV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getEndV)
                    gListSPHadaeSurpass.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("하대돌파 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 근접 계산을 위한 퍼센트 계산
                sgIncreaseVal = stockTodayValue.getEndV * sgPer100
                sgMaxPrice = stockTodayValue.getEndV + sgIncreaseVal
                sgMinPrice = stockTodayValue.getEndV - sgIncreaseVal

                '// 오늘 종가가 시작점 하대 근접한 경우 (시작점 주가 하향이기 때문에 종가가 하대임)
                '// end per
                If sgMaxPrice >= stockStartValue.getEndV And sgMinPrice <= stockStartValue.getEndV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getEndV)
                    gListSPHadaeNear.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("하대근접 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 오늘 종가가 시작점 상대 근접한 경우 (시작점 주가가 하향이기 때문에 시가가 상대임)
                '// start per
                If sgMaxPrice >= stockStartValue.getStartV And sgMinPrice <= stockStartValue.getStartV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getStartV)
                    gListSPSangdaeNear.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("상대근접 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 상대 이상 찾기
                If stockStartValue.getStartV <= stockTodayValue.getStartV And _
                    stockStartValue.getStartV <= stockTodayValue.getEndV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getStartV)
                    gListSPSangdaeOver.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("상대이상 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 대박 돌파
                If stockStartValue.getEndV >= stockTodayValue.getStartV And _
                    stockStartValue.getStartV <= stockTodayValue.getEndV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getEndV)
                    gListSPSangHaSurpass.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("대박돌파 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If
            ElseIf stockStartValue.getStartV >= stockStartValue.getEndV And stockTodayValue.getStartV >= stockTodayValue.getEndV Then
                '///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                '// 시작점 하락 & 오늘 주가 하락
                '///////////////////////////////////////////////////////////////////////////////////////////////////////////////

                '// 시작점 하대 하향 (시작점 주가가 하향이기 때문에 종가가 하대임)
                If stockStartValue.getEndV <= stockTodayValue.getStartV And _
                    stockStartValue.getEndV >= stockTodayValue.getEndV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getStartV)
                    gListSPHadeaDrop.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("하대하향 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 시작점 상대 하향 (시작점 주가가 하향이기 때문에 시가가 상대임)
                If stockStartValue.getStartV <= stockTodayValue.getStartV And _
                    stockStartValue.getStartV >= stockTodayValue.getEndV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getStartV)
                    gListSPSangdaeDrop.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("상대하향 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 근접 계산을 위한 퍼센트 계산
                sgIncreaseVal = stockTodayValue.getEndV * sgPer100
                sgMaxPrice = stockTodayValue.getEndV + sgIncreaseVal
                sgMinPrice = stockTodayValue.getEndV - sgIncreaseVal

                '// 오늘 종가가 시작점 하대 근접한 경우 (시작점 주가가 하향이기 때문에 종가가 하대임)
                '// end per
                If sgMaxPrice >= stockStartValue.getEndV And sgMinPrice <= stockStartValue.getEndV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getEndV)
                    gListSPHadaeNear.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("하대근접 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 오늘 종가가 시작점 상대 근접한 경우 (시작점 주가가 하향이기 때문에 시가가 상대임)
                '// start per
                If sgMaxPrice >= stockStartValue.getStartV And sgMinPrice <= stockStartValue.getStartV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getStartV)
                    gListSPSangdaeNear.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("상대근접 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 상대 이상 찾기
                If stockStartValue.getStartV <= stockTodayValue.getStartV And _
                    stockStartValue.getStartV <= stockTodayValue.getEndV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getStartV)
                    gListSPSangdaeOver.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("상대이상 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 대박 하향
                If stockStartValue.getStartV <= stockTodayValue.getStartV And _
                    stockStartValue.getEndV >= stockTodayValue.getEndV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(gStockCodeTable(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, gHashStockStatus(Trim(stockStartValue.getName)), 0, 0, 0, stockTodayValue.getTradeV, stockStartValue.getStartV)
                    gListSPSangHaDrop.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("대박하향 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If
            Else
                Console.WriteLine("조건에 해당되는 주식이 아닙니다 [{0}]", stockTodayValue.getName)
            End If

            nProgressValue += 1
            frmStartPoint.ProgressBar1.Value = nProgressValue
        Next

        '///////////////////////////////////////////////////////////////////////////////////////////////////
        '// 종목별회원사 순매수 가져오기
        '///////////////////////////////////////////////////////////////////////////////////////////////////
        gSendCommandCount = 0
        gRecvCommandCount = 0
        gHashCompanyOnlyBuy.Clear()
        gHashScreenNoAndStock.Clear()

        '// progress bar 관련 초기화
        nProgressValue = 0
        frmStartPoint.ProgressBar1.Minimum = 0
        frmStartPoint.ProgressBar1.Maximum = nTotalFindedStartPointStock

        '// 시작점 하대 근접
        Dim nScreenNo As Integer = 7000
        Dim st As New StartPointInfo
        frmStartPoint.lbMsg1.Text = "순매수 정보 가져오는중..."
        If gListSPHadaeNear.Count > 0 Then
            For Each st In gListSPHadaeNear
                Console.WriteLine("순매수 하대근접 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenNoAndStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(nRequestDelayTime)
                Application.DoEvents()
                gSendCommandCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                frmStartPoint.ProgressBar1.Value = nProgressValue
                frmStartPoint.lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 하대 돌파
        If gListSPHadaeSurpass.Count > 0 Then
            For Each st In gListSPHadaeSurpass
                Console.WriteLine("순매수 하대돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenNoAndStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(nRequestDelayTime)
                Application.DoEvents()
                gSendCommandCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                frmStartPoint.ProgressBar1.Value = nProgressValue
                frmStartPoint.lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 하대 하향
        If gListSPHadeaDrop.Count > 0 Then
            For Each st In gListSPHadeaDrop
                Console.WriteLine("순매수 하대하향 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenNoAndStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(nRequestDelayTime)
                Application.DoEvents()
                gSendCommandCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                frmStartPoint.ProgressBar1.Value = nProgressValue
                frmStartPoint.lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 상대 근접
        If gListSPSangdaeNear.Count > 0 Then
            For Each st In gListSPSangdaeNear
                Console.WriteLine("순매수 상대근접 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenNoAndStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(nRequestDelayTime)
                Application.DoEvents()
                gSendCommandCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                frmStartPoint.ProgressBar1.Value = nProgressValue
                frmStartPoint.lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 상대 돌파
        If gListSPSangdaeSurpass.Count > 0 Then
            For Each st In gListSPSangdaeSurpass
                Console.WriteLine("순매수 상대돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenNoAndStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(nRequestDelayTime)
                Application.DoEvents()
                gSendCommandCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                frmStartPoint.ProgressBar1.Value = nProgressValue
                frmStartPoint.lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 상대 이상
        If gListSPSangdaeOver.Count > 0 Then
            For Each st In gListSPSangdaeOver
                Console.WriteLine("순매수 상대이상 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenNoAndStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(nRequestDelayTime)
                Application.DoEvents()
                gSendCommandCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                frmStartPoint.ProgressBar1.Value = nProgressValue
                frmStartPoint.lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 상대 하향
        If gListSPSangdaeDrop.Count > 0 Then
            For Each st In gListSPSangdaeDrop
                Console.WriteLine("순매수 상대하향 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenNoAndStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(nRequestDelayTime)
                Application.DoEvents()
                gSendCommandCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                frmStartPoint.ProgressBar1.Value = nProgressValue
                frmStartPoint.lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 대박 돌파
        If gListSPSangHaSurpass.Count > 0 Then
            For Each st In gListSPSangHaSurpass
                Console.WriteLine("순매수 대박돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenNoAndStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(nRequestDelayTime)
                Application.DoEvents()
                gSendCommandCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                frmStartPoint.ProgressBar1.Value = nProgressValue
                frmStartPoint.lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 대박 하향
        If gListSPSangHaDrop.Count > 0 Then
            For Each st In gListSPSangHaDrop
                Console.WriteLine("순매수 대박하향 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenNoAndStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(nRequestDelayTime)
                Application.DoEvents()
                gSendCommandCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                frmStartPoint.ProgressBar1.Value = nProgressValue
                frmStartPoint.lbMsg2.Text = st.getName
            Next
        End If

        '// 주포1,2,3 데이터 다 받을때까지 대기.
        Dim totalRetryCount As Integer = 0
        Do While True
            If gSendCommandCount <= gRecvCommandCount Then
                Exit Do
            End If
            Threading.Thread.Sleep(nRequestDelayTime)
            Console.WriteLine("Wait OnRecvTRdata...S[" + CStr(gSendCommandCount) + "], R[" + CStr(gRecvCommandCount) + "]")
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
        gSendCommandCount = 0
        gRecvCommandCount = 0
        nScreenNo = 7000
        gHashScreenNoAndStock.Clear()
        gHashCompanyBizInfo.Clear()
        '// progress bar 관련 초기화
        nProgressValue = 0
        frmStartPoint.ProgressBar1.Minimum = 0
        frmStartPoint.ProgressBar1.Maximum = nTotalFindedStartPointStock

        '// 시작점 하대 근접
        frmStartPoint.lbMsg1.Text = "영업이익,당기순익 가져오는 중..."
        If gListSPHadaeNear.Count > 0 Then
            For Each st In gListSPHadaeNear
                Console.WriteLine("영업이익 하대근접 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenNoAndStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(nRequestDelayTime)
                Application.DoEvents()
                gSendCommandCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                frmStartPoint.ProgressBar1.Value = nProgressValue
                frmStartPoint.lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 하대 돌파
        If gListSPHadaeSurpass.Count > 0 Then
            For Each st In gListSPHadaeSurpass
                Console.WriteLine("영업이익 하대돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenNoAndStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(nRequestDelayTime)
                Application.DoEvents()
                gSendCommandCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                frmStartPoint.ProgressBar1.Value = nProgressValue
                frmStartPoint.lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 하대 하향
        If gListSPHadeaDrop.Count > 0 Then
            For Each st In gListSPHadeaDrop
                Console.WriteLine("영업이익 하대하향 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenNoAndStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(nRequestDelayTime)
                Application.DoEvents()
                gSendCommandCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                frmStartPoint.ProgressBar1.Value = nProgressValue
                frmStartPoint.lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 상대 근접
        If gListSPSangdaeNear.Count > 0 Then
            For Each st In gListSPSangdaeNear
                Console.WriteLine("영업이익 상대근접 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenNoAndStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(nRequestDelayTime)
                Application.DoEvents()
                gSendCommandCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                frmStartPoint.ProgressBar1.Value = nProgressValue
                frmStartPoint.lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 상대 돌파
        If gListSPSangdaeSurpass.Count > 0 Then
            For Each st In gListSPSangdaeSurpass
                Console.WriteLine("영업이익 상대돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenNoAndStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(nRequestDelayTime)
                Application.DoEvents()
                gSendCommandCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                frmStartPoint.ProgressBar1.Value = nProgressValue
                frmStartPoint.lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 상대 이상
        If gListSPSangdaeOver.Count > 0 Then
            For Each st In gListSPSangdaeOver
                Console.WriteLine("영업이익 상대이상 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenNoAndStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(nRequestDelayTime)
                Application.DoEvents()
                gSendCommandCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                frmStartPoint.ProgressBar1.Value = nProgressValue
                frmStartPoint.lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 상대 하향
        If gListSPSangdaeDrop.Count > 0 Then
            For Each st In gListSPSangdaeDrop
                Console.WriteLine("영업이익 상대하향 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenNoAndStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(nRequestDelayTime)
                Application.DoEvents()
                gSendCommandCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                frmStartPoint.ProgressBar1.Value = nProgressValue
                frmStartPoint.lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 대박 돌파
        If gListSPSangHaSurpass.Count > 0 Then
            For Each st In gListSPSangHaSurpass
                Console.WriteLine("영업이익 대박 돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenNoAndStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(nRequestDelayTime)
                Application.DoEvents()
                gSendCommandCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                frmStartPoint.ProgressBar1.Value = nProgressValue
                frmStartPoint.lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 대박 하향
        If gListSPSangHaDrop.Count > 0 Then
            For Each st In gListSPSangHaDrop
                Console.WriteLine("영업이익 대박 하향 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenNoAndStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(nRequestDelayTime)
                Application.DoEvents()
                gSendCommandCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                frmStartPoint.ProgressBar1.Value = nProgressValue
                frmStartPoint.lbMsg2.Text = st.getName
            Next
        End If

        '// 종목별 영업이익, 순이익 가져올때까지 대기 
        totalRetryCount = 0
        Do While True
            If gSendCommandCount <= gRecvCommandCount Then
                Exit Do
            End If
            Threading.Thread.Sleep(nRequestDelayTime)
            Console.WriteLine("Wait OnRecvTRdata...S[" + CStr(gSendCommandCount) + "], R[" + CStr(gRecvCommandCount) + "]")
            Application.DoEvents()
            totalRetryCount += 1
            If totalRetryCount >= 100 Then
                MsgBox("데이터를 서버로 부터 모두 받지 못했습니다. 다시한번 실행해 주세요!")
                Return
            End If
        Loop

        frmStartPoint.lbMsg1.Text = "분석 완료"
        frmStartPoint.lbMsg2.Text = ""

        '// 리스트뷰에 채운다.
        Call printStartPointListView()

    End Sub

    Sub requestTROnlyBuy(ByVal stockCode As String, ByVal screenNo As Integer)
        KHOpenAPI.SetInputValue("종목코드", Trim(stockCode))
        KHOpenAPI.SetInputValue("조회구분", "0")
        KHOpenAPI.SetInputValue("시작일자", Trim(txtJupoStartDate.Text))
        KHOpenAPI.SetInputValue("종료일자", Trim(txtJupoEndDate.Text))
        KHOpenAPI.CommRqData("시작점종목별증권사순위", "OPT10038", 0, CStr(screenNo))
    End Sub
    Sub requestTRStockInfo(ByVal stockCode As String, ByVal screenNo As Integer)
        KHOpenAPI.SetInputValue("종목코드", Trim(stockCode))
        KHOpenAPI.CommRqData("시작점주식기본정보", "OPT10001", 0, CStr(screenNo))
    End Sub
    Sub printStartPointListView()
        '// setting listView
        Dim item As ListViewItem
        frmStartPoint.lstViewStartPoint.Items.Clear()
        frmStartPoint.lstViewStartPoint.Columns(0).TextAlign = HorizontalAlignment.Center
        frmStartPoint.lstViewStartPoint.Columns(1).TextAlign = HorizontalAlignment.Center
        frmStartPoint.lstViewStartPoint.Columns(2).TextAlign = HorizontalAlignment.Center
        frmStartPoint.lstViewStartPoint.Columns(3).TextAlign = HorizontalAlignment.Left   '// 주식 상태
        frmStartPoint.lstViewStartPoint.Columns(4).TextAlign = HorizontalAlignment.Right  '// 순매수 주포1
        frmStartPoint.lstViewStartPoint.Columns(5).TextAlign = HorizontalAlignment.Right  '// 순매수 주포2
        frmStartPoint.lstViewStartPoint.Columns(6).TextAlign = HorizontalAlignment.Right  '// 순매수 주포3
        frmStartPoint.lstViewStartPoint.Columns(7).TextAlign = HorizontalAlignment.Right     '// 상장주식수
        frmStartPoint.lstViewStartPoint.Columns(8).TextAlign = HorizontalAlignment.Right     '// 시가총액
        frmStartPoint.lstViewStartPoint.Columns(9).TextAlign = HorizontalAlignment.Right     '// 영업이익
        frmStartPoint.lstViewStartPoint.Columns(10).TextAlign = HorizontalAlignment.Right    '// 당기순이익
        frmStartPoint.lstViewStartPoint.Columns(11).TextAlign = HorizontalAlignment.Right    '// 거래량
        frmStartPoint.lstViewStartPoint.Columns(11).TextAlign = HorizontalAlignment.Right    '// 주가
        frmStartPoint.lstViewStartPoint.Columns(11).TextAlign = HorizontalAlignment.Right    '// 시작가

        '// 시작점 상향 돌파 
        Dim st As New StartPointInfo
        Dim stOnlyBuy As New StartPointInfo
        Dim stStockBiz As New StockBizInfo
        Dim bCompanySaleProfit As Boolean
        Dim bCompanySaleNetPorfit As Boolean

        '// 상대돌파
        If gListSPSangdaeSurpass.Count > 0 Then
            For Each st In gListSPSangdaeSurpass

                bCompanySaleProfit = True
                bCompanySaleNetPorfit = True

                stStockBiz = gHashCompanyBizInfo(st.getName)

                '// 영업이익 체크
                If Me.chkBizValue.Checked = True Then
                    If stStockBiz.getSaleProfitValue < 0 Then
                        bCompanySaleProfit = False
                    End If
                End If

                '// 당기순익 체크
                If Me.chkBizSunValue.Checked = True Then
                    If stStockBiz.getSaleNetProfitValue < 0 Then
                        bCompanySaleNetPorfit = False
                    End If
                End If

                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                If CInt(Me.txtJupo1.Text) <= stOnlyBuy.getCompany1 And bCompanySaleProfit = True And bCompanySaleNetPorfit = True Then
                    Console.WriteLine("상대돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                    item = New ListViewItem(CStr(st.getName))
                    item.SubItems.Add(CStr(st.getDate))
                    item.SubItems.Add("상대돌파")
                    item.UseItemStyleForSubItems = False
                    item.SubItems(2).ForeColor = Color.Blue
                    item.SubItems.Add(gHashStockStatus(Trim(st.getName)))
                    '// 주포1, 2, 3 순매수
                    item.SubItems.Add(CStr(stOnlyBuy.getCompany1))
                    item.SubItems.Add(CStr(stOnlyBuy.getCompany2))
                    item.SubItems.Add(CStr(stOnlyBuy.getCompany3))
                    '// 상장주식수, 시가총액, 영업이익, 당기순이익
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

                    '// 거래량
                    item.SubItems.Add(CStr(st.getTradeCount))
                    '// 주가
                    item.SubItems.Add(CStr(gHashNameByCurPrice(st.getName)))
                    '// 시작가
                    item.SubItems.Add(CStr(st.getSPPrice))

                    frmStartPoint.lstViewStartPoint.Items.Add(item)
                End If
            Next
        End If

        '// 하대 돌파
        If gListSPHadaeSurpass.Count > 0 Then
            For Each st In gListSPHadaeSurpass

                bCompanySaleProfit = True
                bCompanySaleNetPorfit = True

                stStockBiz = gHashCompanyBizInfo(st.getName)

                '// 영업이익 체크
                If Me.chkBizValue.Checked = True Then
                    If stStockBiz.getSaleProfitValue < 0 Then
                        bCompanySaleProfit = False
                    End If
                End If

                '// 당기순익 체크
                If Me.chkBizSunValue.Checked = True Then
                    If stStockBiz.getSaleNetProfitValue < 0 Then
                        bCompanySaleNetPorfit = False
                    End If
                End If

                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                If CInt(Me.txtJupo1.Text) <= stOnlyBuy.getCompany1 And bCompanySaleProfit = True And bCompanySaleNetPorfit = True Then
                    Console.WriteLine("하대돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                    item = New ListViewItem(CStr(st.getName))
                    item.SubItems.Add(CStr(st.getDate))
                    item.SubItems.Add("하대돌파")
                    item.UseItemStyleForSubItems = False
                    item.SubItems(2).ForeColor = Color.Blue
                    item.SubItems.Add(gHashStockStatus(Trim(st.getName)))
                    '// 주포1, 2, 3 순매수
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

                    '// 거래량
                    item.SubItems.Add(CStr(st.getTradeCount))
                    '// 현재가격
                    item.SubItems.Add(CStr(gHashNameByCurPrice(st.getName)))
                    '// 시작가
                    item.SubItems.Add(CStr(st.getSPPrice))

                    frmStartPoint.lstViewStartPoint.Items.Add(item)
                End If

            Next
        End If

        '// 상대 근접
        If gListSPSangdaeNear.Count > 0 Then
            For Each st In gListSPSangdaeNear

                bCompanySaleProfit = True
                bCompanySaleNetPorfit = True

                stStockBiz = gHashCompanyBizInfo(st.getName)

                '// 영업이익 체크
                If Me.chkBizValue.Checked = True Then
                    If stStockBiz.getSaleProfitValue < 0 Then
                        bCompanySaleProfit = False
                    End If
                End If

                '// 당기순익 체크
                If Me.chkBizSunValue.Checked = True Then
                    If stStockBiz.getSaleNetProfitValue < 0 Then
                        bCompanySaleNetPorfit = False
                    End If
                End If

                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                If CInt(Me.txtJupo1.Text) <= stOnlyBuy.getCompany1 And bCompanySaleProfit = True And bCompanySaleNetPorfit = True Then
                    Console.WriteLine("상대근접 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                    item = New ListViewItem(CStr(st.getName))
                    item.SubItems.Add(CStr(st.getDate))
                    item.SubItems.Add("상대근접")
                    item.UseItemStyleForSubItems = False
                    item.SubItems(2).ForeColor = Color.Blue
                    item.SubItems.Add(gHashStockStatus(Trim(st.getName)))
                    '// 주포1, 2, 3 순매수
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

                    '// 거래량
                    item.SubItems.Add(CStr(st.getTradeCount))
                    '// 현재가격
                    item.SubItems.Add(CStr(gHashNameByCurPrice(st.getName)))
                    '// 시작가
                    item.SubItems.Add(CStr(st.getSPPrice))

                    frmStartPoint.lstViewStartPoint.Items.Add(item)
                End If

            Next
        End If

        '// 하대근접
        If gListSPHadaeNear.Count > 0 Then
            For Each st In gListSPHadaeNear

                bCompanySaleProfit = True
                bCompanySaleNetPorfit = True

                stStockBiz = gHashCompanyBizInfo(st.getName)

                '// 영업이익 체크
                If Me.chkBizValue.Checked = True Then
                    If stStockBiz.getSaleProfitValue < 0 Then
                        bCompanySaleProfit = False
                    End If
                End If

                '// 당기순익 체크
                If Me.chkBizSunValue.Checked = True Then
                    If stStockBiz.getSaleNetProfitValue < 0 Then
                        bCompanySaleNetPorfit = False
                    End If
                End If

                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                If CInt(Me.txtJupo1.Text) <= stOnlyBuy.getCompany1 And bCompanySaleProfit = True And bCompanySaleNetPorfit = True Then
                    Console.WriteLine("하대근접 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                    item = New ListViewItem(CStr(st.getName))
                    item.SubItems.Add(CStr(st.getDate))
                    item.SubItems.Add("하대근접")
                    item.UseItemStyleForSubItems = False
                    item.SubItems(2).ForeColor = Color.Blue
                    item.SubItems.Add(gHashStockStatus(Trim(st.getName)))
                    '// 주포1, 2, 3 순매수
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

                    '// 거래량
                    item.SubItems.Add(CStr(st.getTradeCount))
                    '// 현재가격
                    item.SubItems.Add(CStr(gHashNameByCurPrice(st.getName)))
                    '// 시작가
                    item.SubItems.Add(CStr(st.getSPPrice))

                    frmStartPoint.lstViewStartPoint.Items.Add(item)
                End If

            Next
        End If

        '// 상대이상
        If gListSPSangdaeOver.Count > 0 Then
            For Each st In gListSPSangdaeOver

                bCompanySaleProfit = True
                bCompanySaleNetPorfit = True

                stStockBiz = gHashCompanyBizInfo(st.getName)

                '// 영업이익 체크
                If Me.chkBizValue.Checked = True Then
                    If stStockBiz.getSaleProfitValue < 0 Then
                        bCompanySaleProfit = False
                    End If
                End If

                '// 당기순익 체크
                If Me.chkBizSunValue.Checked = True Then
                    If stStockBiz.getSaleNetProfitValue < 0 Then
                        bCompanySaleNetPorfit = False
                    End If
                End If

                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                If CInt(Me.txtJupo1.Text) <= stOnlyBuy.getCompany1 And bCompanySaleProfit = True And bCompanySaleNetPorfit = True Then
                    Console.WriteLine("상대이상 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                    item = New ListViewItem(CStr(st.getName))
                    item.SubItems.Add(CStr(st.getDate))
                    item.SubItems.Add("상대이상")
                    item.UseItemStyleForSubItems = False
                    item.SubItems(2).ForeColor = Color.Blue
                    item.SubItems.Add(gHashStockStatus(Trim(st.getName)))
                    '// 주포1, 2, 3 순매수
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

                    '// 거래량
                    item.SubItems.Add(CStr(st.getTradeCount))
                    '// 현재가격
                    item.SubItems.Add(CStr(gHashNameByCurPrice(st.getName)))
                    '// 시작가
                    item.SubItems.Add(CStr(st.getSPPrice))

                    frmStartPoint.lstViewStartPoint.Items.Add(item)
                End If

            Next
        End If

        '// 하대하향
        If gListSPHadeaDrop.Count > 0 Then
            For Each st In gListSPHadeaDrop

                bCompanySaleProfit = True
                bCompanySaleNetPorfit = True

                stStockBiz = gHashCompanyBizInfo(st.getName)

                '// 영업이익 체크
                If Me.chkBizValue.Checked = True Then
                    If stStockBiz.getSaleProfitValue < 0 Then
                        bCompanySaleProfit = False
                    End If
                End If

                '// 당기순익 체크
                If Me.chkBizSunValue.Checked = True Then
                    If stStockBiz.getSaleNetProfitValue < 0 Then
                        bCompanySaleNetPorfit = False
                    End If
                End If

                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                If CInt(Me.txtJupo1.Text) <= stOnlyBuy.getCompany1 And bCompanySaleProfit = True And bCompanySaleNetPorfit = True Then
                    Console.WriteLine("하대하향 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                    item = New ListViewItem(CStr(st.getName))
                    item.SubItems.Add(CStr(st.getDate))
                    item.SubItems.Add("하대하향")
                    item.UseItemStyleForSubItems = False
                    item.SubItems(2).ForeColor = Color.Red
                    item.SubItems.Add(gHashStockStatus(Trim(st.getName)))
                    '// 주포1, 2, 3 순매수
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

                    '// 거래량
                    item.SubItems.Add(CStr(st.getTradeCount))
                    '// 현재가격
                    item.SubItems.Add(CStr(gHashNameByCurPrice(st.getName)))
                    '// 시작가
                    item.SubItems.Add(CStr(st.getSPPrice))

                    frmStartPoint.lstViewStartPoint.Items.Add(item)
                End If

            Next
        End If

        '// 상대하향
        If gListSPSangdaeDrop.Count > 0 Then
            For Each st In gListSPSangdaeDrop

                bCompanySaleProfit = True
                bCompanySaleNetPorfit = True

                stStockBiz = gHashCompanyBizInfo(st.getName)

                '// 영업이익 체크
                If Me.chkBizValue.Checked = True Then
                    If stStockBiz.getSaleProfitValue < 0 Then
                        bCompanySaleProfit = False
                    End If
                End If

                '// 당기순익 체크
                If Me.chkBizSunValue.Checked = True Then
                    If stStockBiz.getSaleNetProfitValue < 0 Then
                        bCompanySaleNetPorfit = False
                    End If
                End If

                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                If CInt(Me.txtJupo1.Text) <= stOnlyBuy.getCompany1 And bCompanySaleProfit = True And bCompanySaleNetPorfit = True Then
                    Console.WriteLine("상대하향 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                    item = New ListViewItem(CStr(st.getName))
                    item.SubItems.Add(CStr(st.getDate))
                    item.SubItems.Add("상대하향")
                    item.UseItemStyleForSubItems = False
                    item.SubItems(2).ForeColor = Color.Red
                    item.SubItems.Add(gHashStockStatus(Trim(st.getName)))
                    '// 주포1, 2, 3 순매수
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

                    '// 거래량
                    item.SubItems.Add(CStr(st.getTradeCount))
                    '// 현재가격
                    item.SubItems.Add(CStr(gHashNameByCurPrice(st.getName)))
                    '// 시작가
                    item.SubItems.Add(CStr(st.getSPPrice))

                    frmStartPoint.lstViewStartPoint.Items.Add(item)
                End If

            Next
        End If

        '// 대박돌파
        If gListSPSangHaSurpass.Count > 0 Then
            For Each st In gListSPSangHaSurpass

                bCompanySaleProfit = True
                bCompanySaleNetPorfit = True

                stStockBiz = gHashCompanyBizInfo(st.getName)

                '// 영업이익 체크
                If Me.chkBizValue.Checked = True Then
                    If stStockBiz.getSaleProfitValue < 0 Then
                        bCompanySaleProfit = False
                    End If
                End If

                '// 당기순익 체크
                If Me.chkBizSunValue.Checked = True Then
                    If stStockBiz.getSaleNetProfitValue < 0 Then
                        bCompanySaleNetPorfit = False
                    End If
                End If

                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                If CInt(Me.txtJupo1.Text) <= stOnlyBuy.getCompany1 And bCompanySaleProfit = True And bCompanySaleNetPorfit = True Then
                    Console.WriteLine("대박돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                    item = New ListViewItem(CStr(st.getName))
                    item.SubItems.Add(CStr(st.getDate))
                    item.SubItems.Add("대박돌파")
                    item.UseItemStyleForSubItems = False
                    item.SubItems(2).ForeColor = Color.Blue
                    item.SubItems.Add(gHashStockStatus(Trim(st.getName)))
                    '// 주포1, 2, 3 순매수
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

                    '// 거래량
                    item.SubItems.Add(CStr(st.getTradeCount))
                    '// 현재가격
                    item.SubItems.Add(CStr(gHashNameByCurPrice(st.getName)))
                    '// 시작가
                    item.SubItems.Add(CStr(st.getSPPrice))

                    frmStartPoint.lstViewStartPoint.Items.Add(item)
                End If

            Next
        End If

        '// 대박하향
        If gListSPSangHaDrop.Count > 0 Then
            For Each st In gListSPSangHaDrop

                bCompanySaleProfit = True
                bCompanySaleNetPorfit = True

                stStockBiz = gHashCompanyBizInfo(st.getName)

                '// 영업이익 체크
                If Me.chkBizValue.Checked = True Then
                    If stStockBiz.getSaleProfitValue < 0 Then
                        bCompanySaleProfit = False
                    End If
                End If

                '// 당기순익 체크
                If Me.chkBizSunValue.Checked = True Then
                    If stStockBiz.getSaleNetProfitValue < 0 Then
                        bCompanySaleNetPorfit = False
                    End If
                End If

                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                If CInt(Me.txtJupo1.Text) <= stOnlyBuy.getCompany1 And bCompanySaleProfit = True And bCompanySaleNetPorfit = True Then
                    Console.WriteLine("대박하향 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                    item = New ListViewItem(CStr(st.getName))
                    item.SubItems.Add(CStr(st.getDate))
                    item.SubItems.Add("대박하향")
                    item.UseItemStyleForSubItems = False
                    item.SubItems(2).ForeColor = Color.Red
                    item.SubItems.Add(gHashStockStatus(Trim(st.getName)))
                    '// 주포1, 2, 3 순매수
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

                    '// 거래량
                    item.SubItems.Add(CStr(st.getTradeCount))
                    '// 현재가격
                    item.SubItems.Add(CStr(gHashNameByCurPrice(st.getName)))
                    '// 시작가
                    item.SubItems.Add(CStr(st.getSPPrice))

                    frmStartPoint.lstViewStartPoint.Items.Add(item)
                End If

            Next
        End If

    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Dim nRet As Integer
        Dim nScreenNumber As Integer = 1000

        '// 302 : 종목명
        '// 20  : 체결시간
        '// 10  : 현재가
        '// 12  : 등락율
        '// 13  : 누적거래량
        '// 15  : 순간거래량
        '// 32  : 거래비용
        '// 26  : 전일거래량대비 (계약,주)
        '// 210 : 순매수수량
        '// 211 : 순매수수량증감
        '// 212 : 순매수금액
        '// 213 : 순매수금약증감
        '// 217 : 이전순매수수량
        '// 228 : 체결강도
        '// 561 : 누적순매수금액
        '// 562 : 누적순매수수량
        '// 9001 : 종목코드

        If bLoginStatus = False Then
            MsgBox("로그인이 필요합니다!!")
            Return
        End If

        '// 실시간 조회 데이터 등록
        For i = 0 To gnCosdaqStrListCount
            nRet = KHOpenAPI.SetRealReg(CStr(nScreenNumber), gCosdaqStrListArr(i), "302;20;10;12;13;15;32;26;210;211;212;213;217;228;561;562;9001", "0")
            If nRet <> 0 Then
                MsgBox("등록 실패")
                Exit For
            End If

            Console.WriteLine("[{0}] {1}", CStr(nScreenNumber), gCosdaqStrListArr(i))
            nScreenNumber += 1
        Next

        '////////////////////////////////////////////////////////////////////////////
        '// 필터링 조건 값
        '////////////////////////////////////////////////////////////////////////////
        '// 비교할 체결량
        gnFilterSignValue = CInt(Trim(txtSignValue.Text))
        '// 거래대금
        gnFilterTradePrice = CInt(Trim(txtTradePrice.Text))
        '// 분봉 시작점 찾을때 몇일전까지 찾을지 날짜 정해줌
        gnFilterAnalCount = CInt(Trim(txtAnalBunBong.Text))
        '// 분봉 시작점 근접 퍼센트
        gsFilterSPNearPer = Convert.ToSingle(Trim(txtBunBongStartPer.Text))
        '// 거래량 필터링 조건
        gnFilterTradeValue = CInt(Trim(txtSignTradeValue.Text))
        '// 주가 필터링 조건 :: 시작 가격
        gnFilterAnalStartPrice = CInt(Trim(txtBunBongStartPrice.Text))
        '// 주가 필터링 조건 :: 종료 가격
        gnFilterAnalEndPrice = CInt(Trim(txtBunBongEndPrice.Text))
        '// 주포1 순매수
        gnFilterJupo1SunBuy = CInt(Trim(txtBunBongJupoSunCount.Text))

        MsgBox("등록 성공")

        frmSign.Show()

    End Sub
    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Dim nRet As Integer

        If bLoginStatus = False Then
            MsgBox("로그인이 필요합니다!!")
            Return
        End If

        KHOpenAPI.SetRealRemove("ALL", "ALL")
        If nRet <> 0 Then
            MsgBox("해제 실패")
        Else
            MsgBox("신호등 찾기를 멈췄습니다.")
        End If
    End Sub

    Private Sub getBunBongStartPoint()
        If bLoginStatus = False Then
            MsgBox("로그인이 필요합니다!!")
            Return
        End If

        If Trim(txtStockCode.Text).Length = 0 Then
            MsgBox("선택된 종목이 없습니다")
            Return
        End If

        '///////////////////////////////////////////////////////////////////////////////////////////////////////
        '// 분봉 차트 조회
        '///////////////////////////////////////////////////////////////////////////////////////////////////////
        '// 비교할 체결량
        gnFilterSignValue = CInt(Trim(txtSignValue.Text))
        gnFilterTradePrice = CInt(Trim(txtTradePrice.Text))
        '// 분봉 시작점 찾을때 몇일전까지 찾을지 날짜 정해줌
        gnFilterAnalCount = CInt(Trim(txtAnalBunBong.Text))
        gSendCommandCount = 0
        gRecvCommandCount = 0

        KHOpenAPI.SetInputValue("종목코드", Trim(txtStockCode.Text))
        KHOpenAPI.SetInputValue("틱범위", "10")
        KHOpenAPI.SetInputValue("수정주가구분", "1")
        KHOpenAPI.CommRqData("시작점분봉차트조회", "OPT10080", 0, "2777")
        gSendCommandCount += 1
        Application.DoEvents()

        '///////////////////////////////////////////////////////////////////////////////////////////////////////
        '// 주식거래량 조회
        '///////////////////////////////////////////////////////////////////////////////////////////////////////
        KHOpenAPI.SetInputValue("종목코드", Trim(txtStockCode.Text))
        KHOpenAPI.CommRqData("주식거래량", "opt10001", 0, "1777")
        Application.DoEvents()
        gSendCommandCount += 1

        '///////////////////////////////////////////////////////////////////////////////////////////////////////
        '// 주포 순매수 죄회
        '///////////////////////////////////////////////////////////////////////////////////////////////////////
        KHOpenAPI.SetInputValue("종목코드", Trim(txtStockCode.Text))
        KHOpenAPI.SetInputValue("조회구분", "0")
        KHOpenAPI.SetInputValue("시작일자", Trim(txtAnalStartDate.Text))
        KHOpenAPI.SetInputValue("종료일자", Trim(txtAnalEndDate.Text))
        KHOpenAPI.CommRqData("신호등종목별증권사조회", "OPT10038", 0, "1778")
        gSendCommandCount += 1

        '// 데이터 다 받을때까지 대기.
        Dim totalRetryCount As Integer = 0
        Do While True
            If gSendCommandCount <= gRecvCommandCount Then
                Exit Do
            End If
            Threading.Thread.Sleep(nRequestDelayTime)
            Console.WriteLine("Wait OnRecvTRdata...S[" + CStr(gSendCommandCount) + "], R[" + CStr(gRecvCommandCount) + "]")
            Application.DoEvents()
            totalRetryCount += 1
            If totalRetryCount >= 100 Then
                Console.WriteLine("데이터를 서버로 부터 모두 받지 못했습니다. 다시한번 실행해 주세요!")
                Return
            End If
        Loop

        '///////////////////////////////////////////////////////////////////////////////////////////////////////
        '// 분봉 시작점 상대/하대 근접 찾기
        '///////////////////////////////////////////////////////////////////////////////////////////////////////
        Dim nRet As Short
        nRet = calculateHighLowPer(gnSPStartV, gnSPEndV, gnSinhoCurPrice)
        If nRet = 2 Then
            Console.WriteLine("하대 근접")
        ElseIf nRet = 1 Then
            Console.WriteLine("상대 근접")
        Else
            Console.WriteLine("근접 않함")
        End If

        '///////////////////////////////////////////////////////////////////////////////////////////////////////
        '// 주포 1 ~ 3 프린트
        '///////////////////////////////////////////////////////////////////////////////////////////////////////

        Console.WriteLine("주포1{0}, 주포2 {1}, 주포3 {2}", gnSinhoOnlyBuyCount(0), gnSinhoOnlyBuyCount(1), gnSinhoOnlyBuyCount(2))

    End Sub
    Private Sub getSinhoData(ByVal sStockCode As String)

        '// 변수 초기값. 에러나면 초기값이 전달됨.
        gnSinhoCurPrice = -12345
        gnSinhoTradeValue = -12345
        gnSinhoOnlyBuyCount(0) = -12345
        gnSinhoOnlyBuyCount(1) = -12345
        gnSinhoOnlyBuyCount(2) = -12345
        gnSinhoStartPointHow = 0

        If bLoginStatus = False Then
            Console.WriteLine("로그인이 필요합니다!!")
            Return
        End If

        If Trim(txtStockCode.Text).Length = 0 Then
            Console.WriteLine("선택된 종목이 없습니다")
            Return
        End If

        If sStockCode.Length = 0 Then
            Console.WriteLine("종목 코드가 없습니다")
            Return
        End If

        '///////////////////////////////////////////////////////////////////////////////////////////////////////
        '// 분봉 차트 조회
        '///////////////////////////////////////////////////////////////////////////////////////////////////////
        '// 비교할 체결량
        gnFilterSignValue = CInt(Trim(txtSignValue.Text))
        gnFilterTradePrice = CInt(Trim(txtTradePrice.Text))
        '// 분봉 시작점 찾을때 몇일전까지 찾을지 날짜 정해줌
        gnFilterAnalCount = CInt(Trim(txtAnalBunBong.Text))
        gSendCommandCount = 0
        gRecvCommandCount = 0

        KHOpenAPI.SetInputValue("종목코드", sStockCode)
        KHOpenAPI.SetInputValue("틱범위", "10")
        KHOpenAPI.SetInputValue("수정주가구분", "1")
        KHOpenAPI.CommRqData("시작점분봉차트조회", "OPT10080", 0, "1777")
        gSendCommandCount += 1
        Application.DoEvents()

        '///////////////////////////////////////////////////////////////////////////////////////////////////////
        '// 주식거래량 조회
        '///////////////////////////////////////////////////////////////////////////////////////////////////////
        KHOpenAPI.SetInputValue("종목코드", sStockCode)
        KHOpenAPI.CommRqData("주식거래량", "opt10001", 0, "1778")
        Application.DoEvents()
        gSendCommandCount += 1

        '///////////////////////////////////////////////////////////////////////////////////////////////////////
        '// 주포 순매수 죄회
        '///////////////////////////////////////////////////////////////////////////////////////////////////////
        KHOpenAPI.SetInputValue("종목코드", sStockCode)
        KHOpenAPI.SetInputValue("조회구분", "0")
        KHOpenAPI.SetInputValue("시작일자", Trim(txtAnalStartDate.Text))
        KHOpenAPI.SetInputValue("종료일자", Trim(txtAnalEndDate.Text))
        KHOpenAPI.CommRqData("신호등종목별증권사조회", "OPT10038", 0, "1779")
        gSendCommandCount += 1

        '// 데이터 다 받을때까지 대기.
        Dim totalRetryCount As Integer = 0
        Do While True
            If gSendCommandCount <= gRecvCommandCount Then
                Exit Do
            End If
            Threading.Thread.Sleep(nRequestDelayTime)
            Console.WriteLine("Wait OnRecvTRdata...S[" + CStr(gSendCommandCount) + "], R[" + CStr(gRecvCommandCount) + "]")
            Application.DoEvents()
            totalRetryCount += 1
            If totalRetryCount >= 20 Then
                Console.WriteLine("데이터를 서버로 부터 모두 받지 못했습니다. 다시한번 실행해 주세요!")
                Return
            End If
        Loop

        '///////////////////////////////////////////////////////////////////////////////////////////////////////
        '// 분봉 시작점 상대/하대 근접 찾기
        '///////////////////////////////////////////////////////////////////////////////////////////////////////
        gnSinhoStartPointHow = calculateHighLowPer(gnSPStartV, gnSPEndV, gnSinhoCurPrice)
        If gnSinhoStartPointHow = 2 Then
            Console.WriteLine("하대 근접")
        ElseIf gnSinhoStartPointHow = 1 Then
            Console.WriteLine("상대 근접")
        Else
            Console.WriteLine("근접 아님")
        End If

        '///////////////////////////////////////////////////////////////////////////////////////////////////////
        '// 주포 1 ~ 3 프린트
        '///////////////////////////////////////////////////////////////////////////////////////////////////////
        Console.WriteLine("주포1{0}, 주포2 {1}, 주포3 {2}", gnSinhoOnlyBuyCount(0), gnSinhoOnlyBuyCount(1), gnSinhoOnlyBuyCount(2))

    End Sub

    '// return : 1 이면 상대 유효 %에 근접,  2이면 하대 유효 %에 근접,  0이면 유효범위 밖에 있음
    Private Function calculateHighLowPer(ByVal nSPStartV As Integer, ByVal nSPEndV As Integer, ByVal nCurPrice As Integer)

        Dim sgPer100 As Single
        Dim sgIncreaseVal As Single
        Dim sgSPSangDaeHighPrice As Single, sgSPSangDaeLowPrice As Single
        Dim sgSPHaDaeHighPrice As Single, sgSPHaDaeLowPrice As Single

        sgPer100 = gsFilterSPNearPer / 100

        '// 시작점 종가가 높을때 (가격 상승)
        If nSPEndV >= nSPStartV Then
            '// 상대 유효범위 %에 있는지 체크
            sgIncreaseVal = nSPEndV * sgPer100
            sgSPSangDaeHighPrice = nSPEndV + sgIncreaseVal
            sgSPSangDaeLowPrice = nSPEndV - sgIncreaseVal
            If sgSPSangDaeHighPrice >= nCurPrice And sgSPSangDaeLowPrice <= nCurPrice Then
                Return 1
            End If

            '// 하대 유효 %에 있는지 체크
            sgIncreaseVal = nSPStartV * sgPer100
            sgSPHaDaeHighPrice = nSPStartV + sgIncreaseVal
            sgSPHaDaeLowPrice = nSPStartV - sgIncreaseVal
            If sgSPHaDaeHighPrice >= nCurPrice And sgSPHaDaeLowPrice <= nCurPrice Then
                Return 2
            End If
        Else '// 시작점 가격 하락
            '// 상대 유효범위 %에 있는지 체크
            sgIncreaseVal = nSPEndV * sgPer100
            sgSPHaDaeHighPrice = nSPEndV + sgIncreaseVal
            sgSPHaDaeLowPrice = nSPEndV - sgIncreaseVal
            If sgSPHaDaeHighPrice >= nCurPrice And sgSPHaDaeLowPrice <= nCurPrice Then
                Return 2
            End If

            '// 하대 유효 %에 있는지 체크
            sgIncreaseVal = nSPStartV * sgPer100
            sgSPSangDaeHighPrice = nSPStartV + sgIncreaseVal
            sgSPSangDaeLowPrice = nSPStartV - sgIncreaseVal
            If sgSPSangDaeHighPrice >= nCurPrice And sgSPSangDaeLowPrice <= nCurPrice Then
                Return 1
            End If
        End If

        Return 0

    End Function
End Class
