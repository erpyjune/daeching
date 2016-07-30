Public Class frmStartPoint

    Dim gHashScreenStock As New Hashtable
    Dim gHashCompanyOnlyBuy As New Hashtable '// 종목별 1포, 2포, 3포 순매수 저장
    Dim gHashCompanyBizInfo As New Hashtable '// 상장주식수, 영업이익, 순이익, 매출
    Dim gHashStockTable As New Hashtable        '// 주식 종목 테이블
    Dim gSendCount As Integer
    Dim gRecvCount As Integer
    Dim gHashStockStatus As New Hashtable       '// 종목별 상태값 저장

    'Dim gListStartPointHighOver As New List(Of StartPointInfo)  '// 오늘 종가가 시작점 상대 위에
    'Dim gListStartPointLowOver As New List(Of StartPointInfo)   '// 오늘 종가가 시작점 하대 위에
    'Dim gListStartPointHigh As New List(Of StartPointInfo)      '// 오늘 종가가 시작점 상향 돌파
    'Dim gListStartPointLow As New List(Of StartPointInfo)       '// 오늘 종가가 시작점 하향 돌파
    'Dim gListStartPointHighNear As New List(Of StartPointInfo)  '// 오늘 종가가 시작점 상대 근처
    'Dim gListStartPointLowNear As New List(Of StartPointInfo)   '// 오늘 종가가 시작점 하대 근처

    Dim gListSPHadaeNear As New List(Of StartPointInfo)      '// 하대 근접
    Dim gListSPHadaeSurpass As New List(Of StartPointInfo)   '// 하대 돌파
    Dim gListSPHadeaDrop As New List(Of StartPointInfo)      '// 하대 하향
    Dim gListSPSangdaeNear As New List(Of StartPointInfo)    '// 상대 근접
    Dim gListSPSangdaeSurpass As New List(Of StartPointInfo) '// 상대 돌파
    Dim gListSPSangdaeOver As New List(Of StartPointInfo)    '// 상대 이상
    Dim gListSPSangdaeDrop As New List(Of StartPointInfo)    '// 상대 하향
    Dim gListSPSangHaSurpass As New List(Of StartPointInfo)  '// 대박 돌파 
    Dim gListSPSangHaDrop As New List(Of StartPointInfo)     '// 대박 하향
    Sub requestTROnlyBuy(ByVal stockCode As String, ByVal screenNo As Integer)
        KHOpenAPI.SetInputValue("종목코드", Trim(stockCode))
        KHOpenAPI.SetInputValue("조회구분", "0")
        KHOpenAPI.SetInputValue("시작일자", "20160101")
        KHOpenAPI.SetInputValue("종료일자", frmMain.txtEndDate1.Text)
        KHOpenAPI.CommRqData("시작점종목별증권사순위", "OPT10038", CInt("0"), CStr(screenNo))
    End Sub
    Sub requestTRStockInfo(ByVal stockCode As String, ByVal screenNo As Integer)
        KHOpenAPI.SetInputValue("종목코드", Trim(stockCode))
        KHOpenAPI.CommRqData("주식기본정보", "OPT10001", CInt("0"), CStr(screenNo))
    End Sub
    Private Sub frmStartPoint_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KHOpenAPI = frmMain.getAPI

        gHashStockTable = frmMain.getStockCodeTable

    End Sub
    Sub printStartPoint2()
        Dim fStartPer As Single, fEndPer As Single
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
        Dim stockCode As String
        Dim sMsg As String
        Dim nTotalFindedStartPointStock As Integer = 0
        Dim stockCodeTable As New Hashtable

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

        '// 시작점을 찾고, 오늘 주가의 시가, 종가를 저장한다
        lbMsg1.Text = "시작점 찾는 중..."
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
                If frmMain.txtSPToday.Text = stockValueInfo.getCurDate Then
                    stockVInfo = New StockValueInfo
                    stockVInfo.setStockValue(stockValueInfo.getCurDate, stockValueInfo.getStartV, stockValueInfo.getEndV, stockValueInfo.getTradeV, stockValueInfo.getName, "")
                    hashTodayStartEndValue.Add(stockValueInfo.getName, stockVInfo)
                End If

                '// progress bar add
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                lbMsg2.Text = stockValueInfo.getName
                Application.DoEvents()
                'Console.WriteLine("{0},{1},{2},{3},{4}", key.ToString, stockValueInfo.getCurDate, stockValueInfo.getTradeV, stockValueInfo.getStartV, stockValueInfo.getEndV)
            Next

            '// 종목에 대한 정보를 api를 통해서 가져와서 저장.
            stockCodeTable = frmMain.getStockCodeTable
            stockCode = stockCodeTable(stockValueInfo.getName)
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

            ''// 찾은 시작점 list에 추가.
            'stockVInfo = New StockValueInfo
            'stockVInfo.setStockValue(maxStockValueInfo.getCurDate, maxStockValueInfo.getStartV, maxStockValueInfo.getEndV, maxStockValueInfo.getTradeV, maxStockValueInfo.getName, "")
            'listSijackStockValueInfo.Add(stockVInfo)
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
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = listSijackStockValueInfo.Count

        Dim nPrintCount As Integer = 0
        Dim sName As String

        '////////////////////////////////////////////////////
        '// 오늘 주가가 시작점 위치인것 찾기
        '////////////////////////////////////////////////////
        lbMsg1.Text = "시작점 종목 찾는중..."
        For Each stockStartValue In listSijackStockValueInfo
            '// 시작점의 어떤 종목
            sName = stockStartValue.getName
            '// 시작점 종목의 오늘 주가 정보
            stockTodayValue = hashTodayStartEndValue(sName)
            lbMsg2.Text = sName

            '/////////////////////////////////////////////////////////////////////////////////////////////////////
            '// 시작점 상승, 오늘주가 상승
            '/////////////////////////////////////////////////////////////////////////////////////////////////////
            If stockStartValue.getStartV <= stockStartValue.getEndV And stockTodayValue.getStartV <= stockTodayValue.getEndV Then
                '// 시작점 상대 돌파
                If stockStartValue.getEndV <= stockTodayValue.getEndV And _
                    stockStartValue.getEndV >= stockTodayValue.getStartV And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
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
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
                    gListSPHadaeSurpass.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("하대돌파 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 오늘 종가의 시작점 시가 근접도
                fStartPer = stockStartValue.getStartV / stockTodayValue.getEndV * 100
                '// 오늘 종가의 시작점 종가 근접도
                fEndPer = stockStartValue.getEndV / stockTodayValue.getEndV * 100

                '// 오늘 종가가 시작점 상대 근접한 경우
                If fEndPer <= 101 And fEndPer >= 99 And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
                    gListSPSangdaeNear.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("상대근접 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 오늘 종가가 시작점 하대 근접한 경우
                If fStartPer <= 101 And fStartPer >= 99 And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
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
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
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
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
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
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
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
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
                    gListSPHadeaDrop.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("하대하향 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 오늘 종가의 시작점 시가 근접도
                fStartPer = stockStartValue.getStartV / stockTodayValue.getEndV * 100
                '// 오늘 종가의 시작점 종가 근접도
                fEndPer = stockStartValue.getEndV / stockTodayValue.getEndV * 100

                '// 오늘 종가가 시작점 상대 근접한 경우
                If fEndPer <= 101 And fEndPer >= 99 And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
                    gListSPSangdaeNear.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("상대근접 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 오늘 종가가 시작점 하대 근접한 경우
                If fStartPer <= 101 And fStartPer >= 99 And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
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
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
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
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
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
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
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
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
                    gListSPHadaeSurpass.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("하대돌파 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 오늘 종가의 시작점 시가 근접도
                fStartPer = stockStartValue.getStartV / stockTodayValue.getEndV * 100
                '// 오늘 종가의 시작점 종가 근접도
                fEndPer = stockStartValue.getEndV / stockTodayValue.getEndV * 100

                '// 오늘 종가가 시작점 하대 근접한 경우 (시작점 주가 하향이기 때문에 종가가 하대임)
                If fEndPer <= 101 And fEndPer >= 99 And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
                    gListSPHadaeNear.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("하대근접 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 오늘 종가가 시작점 상대 근접한 경우 (시작점 주가가 하향이기 때문에 시가가 상대임)
                If fStartPer <= 101 And fStartPer >= 99 And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
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
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
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
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
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
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
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
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
                    gListSPSangdaeDrop.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("상대하향 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 오늘 종가의 시작점 시가 근접도
                fStartPer = stockStartValue.getStartV / stockTodayValue.getEndV * 100
                '// 오늘 종가의 시작점 종가 근접도
                fEndPer = stockStartValue.getEndV / stockTodayValue.getEndV * 100

                '// 오늘 종가가 시작점 하대 근접한 경우 (시작점 주가가 하향이기 때문에 종가가 하대임)
                If fEndPer <= 101 And fEndPer >= 99 And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
                    gListSPHadaeNear.Add(startPointInfo)
                    '// 찾은 종목 저장
                    hashPrintedStock.Add(stockStartValue.getName, 1)
                    nTotalFindedStartPointStock += 1
                    Console.WriteLine("하대근접 {0}, {1}", stockStartValue.getName, stockStartValue.getCurDate)
                End If

                '// 오늘 종가가 시작점 상대 근접한 경우 (시작점 주가가 하향이기 때문에 시가가 상대임)
                If fStartPer <= 101 And fStartPer >= 99 And hashPrintedStock.Contains(stockStartValue.getName) = False Then
                    startPointInfo = New StartPointInfo
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
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
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
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
                    startPointInfo.setData(stockStartValue.getName, CStr(frmMain.getStockCode(stockStartValue.getName)), _
                                           stockStartValue.getCurDate, frmMain.getStockStatus(Trim(stockStartValue.getName)), 0, 0, 0)
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
            ProgressBar1.Value = nProgressValue
        Next

        '///////////////////////////////////////////////////////////////////////////////////////////////////
        '// 종목별회원사 순매수 가져오기
        '///////////////////////////////////////////////////////////////////////////////////////////////////
        gSendCount = 0
        gRecvCount = 0
        gHashCompanyOnlyBuy.Clear()
        gHashScreenStock.Clear()

        '// progress bar 관련 초기화
        nProgressValue = 0
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = nTotalFindedStartPointStock

        '// 시작점 하대 근접
        Dim nScreenNo As Integer = 7000
        Dim st As New StartPointInfo
        lbMsg1.Text = "순매수 정보 가져오는중..."
        If gListSPHadaeNear.Count > 0 Then
            For Each st In gListSPHadaeNear
                Console.WriteLine("순매수 하대근접 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(210)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 하대 돌파
        If gListSPHadaeSurpass.Count > 0 Then
            For Each st In gListSPHadaeSurpass
                Console.WriteLine("순매수 하대돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(210)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 하대 하향
        If gListSPHadeaDrop.Count > 0 Then
            For Each st In gListSPHadeaDrop
                Console.WriteLine("순매수 하대하향 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(210)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 상대 근접
        If gListSPSangdaeNear.Count > 0 Then
            For Each st In gListSPSangdaeNear
                Console.WriteLine("순매수 상대근접 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(210)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 상대 돌파
        If gListSPSangdaeSurpass.Count > 0 Then
            For Each st In gListSPSangdaeSurpass
                Console.WriteLine("순매수 상대돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(210)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 상대 이상
        If gListSPSangdaeOver.Count > 0 Then
            For Each st In gListSPSangdaeOver
                Console.WriteLine("순매수 상대이상 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(210)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 상대 하향
        If gListSPSangdaeDrop.Count > 0 Then
            For Each st In gListSPSangdaeDrop
                Console.WriteLine("순매수 상대하향 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(210)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 대박 돌파
        If gListSPSangHaSurpass.Count > 0 Then
            For Each st In gListSPSangHaSurpass
                Console.WriteLine("순매수 대박돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(210)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 대박 하향
        If gListSPSangHaDrop.Count > 0 Then
            For Each st In gListSPSangHaDrop
                Console.WriteLine("순매수 대박하향 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTROnlyBuy(st.getCode, nScreenNo)
                Threading.Thread.Sleep(210)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                lbMsg2.Text = st.getName
            Next
        End If

        '// 주포1,2,3 데이터 다 받을때까지 대기.
        Dim totalRetryCount As Integer = 0
        Do While True
            If gSendCount <= gRecvCount Then
                Exit Do
            End If
            Threading.Thread.Sleep(210)
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
        '// progress bar 관련 초기화
        nProgressValue = 0
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = nTotalFindedStartPointStock

        '// 시작점 하대 근접
        lbMsg1.Text = "영업이익,당기순익 가져오는 중..."
        If gListSPHadaeNear.Count > 0 Then
            For Each st In gListSPHadaeNear
                Console.WriteLine("영업이익 하대근접 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(210)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 하대 돌파
        If gListSPHadaeSurpass.Count > 0 Then
            For Each st In gListSPHadaeSurpass
                Console.WriteLine("영업이익 하대돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(210)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 하대 하향
        If gListSPHadeaDrop.Count > 0 Then
            For Each st In gListSPHadeaDrop
                Console.WriteLine("영업이익 하대하향 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(210)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 상대 근접
        If gListSPSangdaeNear.Count > 0 Then
            For Each st In gListSPSangdaeNear
                Console.WriteLine("영업이익 상대근접 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(210)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 상대 돌파
        If gListSPSangdaeSurpass.Count > 0 Then
            For Each st In gListSPSangdaeSurpass
                Console.WriteLine("영업이익 상대돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(210)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 상대 이상
        If gListSPSangdaeOver.Count > 0 Then
            For Each st In gListSPSangdaeOver
                Console.WriteLine("영업이익 상대이상 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(210)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 상대 하향
        If gListSPSangdaeDrop.Count > 0 Then
            For Each st In gListSPSangdaeDrop
                Console.WriteLine("영업이익 상대하향 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(210)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 대박 돌파
        If gListSPSangHaSurpass.Count > 0 Then
            For Each st In gListSPSangHaSurpass
                Console.WriteLine("영업이익 대박 돌파 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(210)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                lbMsg2.Text = st.getName
            Next
        End If

        '// 시작점 대박 하향
        If gListSPSangHaDrop.Count > 0 Then
            For Each st In gListSPSangHaDrop
                Console.WriteLine("영업이익 대박 하향 {0}, {1}, {2}, {3}", st.getName, st.getCode, st.getDate, st.getStatus)
                gHashScreenStock.Add(CStr(nScreenNo), st.getName)
                '// 순매수량 요청
                requestTRStockInfo(st.getCode, nScreenNo)
                Threading.Thread.Sleep(210)
                Application.DoEvents()
                gSendCount += 1
                nScreenNo += 1
                '// progress bar
                nProgressValue += 1
                ProgressBar1.Value = nProgressValue
                lbMsg2.Text = st.getName
            Next
        End If

        '// 종목별 영업이익, 순이익 가져올때까지 대기 
        totalRetryCount = 0
        Do While True
            If gSendCount <= gRecvCount Then
                Exit Do
            End If
            Threading.Thread.Sleep(210)
            Console.WriteLine("Wait OnRecvTRdata...S[" + CStr(gSendCount) + "], R[" + CStr(gRecvCount) + "]")
            Application.DoEvents()
            totalRetryCount += 1
            If totalRetryCount >= 100 Then
                MsgBox("데이터를 서버로 부터 모두 받지 못했습니다. 다시한번 실행해 주세요!")
                Return
            End If
        Loop

        lbMsg1.Text = "분석 완료"
        lbMsg2.Text = ""

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
        Dim bCompanySaleProfit As Boolean
        Dim bCompanySaleNetPorfit As Boolean

        '// 상대돌파
        If gListSPSangdaeSurpass.Count > 0 Then
            For Each st In gListSPSangdaeSurpass

                bCompanySaleProfit = True
                bCompanySaleNetPorfit = True

                stStockBiz = gHashCompanyBizInfo(st.getName)

                '// 영업이익 체크
                If frmMain.chkBizValue.Checked = True Then
                    If stStockBiz.getSaleProfitValue < 0 Then
                        bCompanySaleProfit = False
                    End If
                End If

                '// 당기순익 체크
                If frmMain.chkBizSunValue.Checked = True Then
                    If stStockBiz.getSaleNetProfitValue < 0 Then
                        bCompanySaleNetPorfit = False
                    End If
                End If

                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                If CInt(frmMain.txtJupo1.Text) <= stOnlyBuy.getCompany1 And bCompanySaleProfit = True And bCompanySaleNetPorfit = True Then
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

                    lstViewStartPoint.Items.Add(item)
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
                If frmMain.chkBizValue.Checked = True Then
                    If stStockBiz.getSaleProfitValue < 0 Then
                        bCompanySaleProfit = False
                    End If
                End If

                '// 당기순익 체크
                If frmMain.chkBizSunValue.Checked = True Then
                    If stStockBiz.getSaleNetProfitValue < 0 Then
                        bCompanySaleNetPorfit = False
                    End If
                End If

                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                If CInt(frmMain.txtJupo1.Text) <= stOnlyBuy.getCompany1 And bCompanySaleProfit = True And bCompanySaleNetPorfit = True Then
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

                    lstViewStartPoint.Items.Add(item)
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
                If frmMain.chkBizValue.Checked = True Then
                    If stStockBiz.getSaleProfitValue < 0 Then
                        bCompanySaleProfit = False
                    End If
                End If

                '// 당기순익 체크
                If frmMain.chkBizSunValue.Checked = True Then
                    If stStockBiz.getSaleNetProfitValue < 0 Then
                        bCompanySaleNetPorfit = False
                    End If
                End If

                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                If CInt(frmMain.txtJupo1.Text) <= stOnlyBuy.getCompany1 And bCompanySaleProfit = True And bCompanySaleNetPorfit = True Then
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

                    lstViewStartPoint.Items.Add(item)
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
                If frmMain.chkBizValue.Checked = True Then
                    If stStockBiz.getSaleProfitValue < 0 Then
                        bCompanySaleProfit = False
                    End If
                End If

                '// 당기순익 체크
                If frmMain.chkBizSunValue.Checked = True Then
                    If stStockBiz.getSaleNetProfitValue < 0 Then
                        bCompanySaleNetPorfit = False
                    End If
                End If

                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                If CInt(frmMain.txtJupo1.Text) <= stOnlyBuy.getCompany1 And bCompanySaleProfit = True And bCompanySaleNetPorfit = True Then
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

                    lstViewStartPoint.Items.Add(item)
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
                If frmMain.chkBizValue.Checked = True Then
                    If stStockBiz.getSaleProfitValue < 0 Then
                        bCompanySaleProfit = False
                    End If
                End If

                '// 당기순익 체크
                If frmMain.chkBizSunValue.Checked = True Then
                    If stStockBiz.getSaleNetProfitValue < 0 Then
                        bCompanySaleNetPorfit = False
                    End If
                End If

                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                If CInt(frmMain.txtJupo1.Text) <= stOnlyBuy.getCompany1 And bCompanySaleProfit = True And bCompanySaleNetPorfit = True Then
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

                    lstViewStartPoint.Items.Add(item)
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
                If frmMain.chkBizValue.Checked = True Then
                    If stStockBiz.getSaleProfitValue < 0 Then
                        bCompanySaleProfit = False
                    End If
                End If

                '// 당기순익 체크
                If frmMain.chkBizSunValue.Checked = True Then
                    If stStockBiz.getSaleNetProfitValue < 0 Then
                        bCompanySaleNetPorfit = False
                    End If
                End If

                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                If CInt(frmMain.txtJupo1.Text) <= stOnlyBuy.getCompany1 And bCompanySaleProfit = True And bCompanySaleNetPorfit = True Then
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

                    lstViewStartPoint.Items.Add(item)
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
                If frmMain.chkBizValue.Checked = True Then
                    If stStockBiz.getSaleProfitValue < 0 Then
                        bCompanySaleProfit = False
                    End If
                End If

                '// 당기순익 체크
                If frmMain.chkBizSunValue.Checked = True Then
                    If stStockBiz.getSaleNetProfitValue < 0 Then
                        bCompanySaleNetPorfit = False
                    End If
                End If

                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                If CInt(frmMain.txtJupo1.Text) <= stOnlyBuy.getCompany1 And bCompanySaleProfit = True And bCompanySaleNetPorfit = True Then
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

                    lstViewStartPoint.Items.Add(item)
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
                If frmMain.chkBizValue.Checked = True Then
                    If stStockBiz.getSaleProfitValue < 0 Then
                        bCompanySaleProfit = False
                    End If
                End If

                '// 당기순익 체크
                If frmMain.chkBizSunValue.Checked = True Then
                    If stStockBiz.getSaleNetProfitValue < 0 Then
                        bCompanySaleNetPorfit = False
                    End If
                End If

                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                If CInt(frmMain.txtJupo1.Text) <= stOnlyBuy.getCompany1 And bCompanySaleProfit = True And bCompanySaleNetPorfit = True Then
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

                    lstViewStartPoint.Items.Add(item)
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
                If frmMain.chkBizValue.Checked = True Then
                    If stStockBiz.getSaleProfitValue < 0 Then
                        bCompanySaleProfit = False
                    End If
                End If

                '// 당기순익 체크
                If frmMain.chkBizSunValue.Checked = True Then
                    If stStockBiz.getSaleNetProfitValue < 0 Then
                        bCompanySaleNetPorfit = False
                    End If
                End If

                stOnlyBuy = gHashCompanyOnlyBuy(st.getName)
                If CInt(frmMain.txtJupo1.Text) <= stOnlyBuy.getCompany1 And bCompanySaleProfit = True And bCompanySaleNetPorfit = True Then
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

                    lstViewStartPoint.Items.Add(item)
                End If

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
        startPointInfo.setData(sStockName, "", "", "", nCmp1, nCmp2, nCmp3)
        gHashCompanyOnlyBuy.Add(Trim(sStockName), startPointInfo)
        gRecvCount += 1
        Console.WriteLine("Recv {0}. {1}, {2}, {3}", sStockName, nCmp1, nCmp2, nCmp3)

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
    Private Sub frmStartPoint_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Call printStartPoint2()
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