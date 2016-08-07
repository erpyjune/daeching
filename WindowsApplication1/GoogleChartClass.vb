Public Class GoogleChart
    Private gSortedListStockSellBuyInfo As SortedList = New SortedList
    'Private gPrintChartCompanySeries As New Hashtable
    Private gListCompanyName As New List(Of String)()
    Private gHashStockCompany As New Hashtable
    Private gListStockSellBuyInfoMain As New List(Of StockSellBuyInfoMain)()

    Public Sub setSortedListStockSellBuyInfo(ByRef s As SortedList)
        Me.gSortedListStockSellBuyInfo = s
    End Sub
    Public Sub setHashStockCompany(ByRef h As Hashtable)
        Me.gHashStockCompany = h
    End Sub

    Public Sub setListStockSellBuyInfoMain(ByRef lst As List(Of StockSellBuyInfoMain))
        Me.gListStockSellBuyInfoMain = lst
    End Sub

    Public Sub drawGoogleChart()
        '// 차트 그리기 위한 종목 선정
        Dim SLEnum As IDictionaryEnumerator
        Dim listCount As Integer, chartPrintCountBreaek As Integer = 0
        Dim total As Integer = 0
        Dim nPrintStockCount As Integer = 0
        Const MAX_CHART As Integer = 4

        listCount = gSortedListStockSellBuyInfo.Count
        chartPrintCountBreaek = listCount - MAX_CHART
        SLEnum = gSortedListStockSellBuyInfo.GetEnumerator()
        gListCompanyName.Clear()

        While SLEnum.MoveNext
            total += 1
            'Console.WriteLine(" 정렬값 {0} : " + CStr(SLEnum.Key.ToString) + " | " + SLEnum.Value.ToString)
            If total >= chartPrintCountBreaek Then
                Me.gListCompanyName.Add(SLEnum.Value.ToString)
                nPrintStockCount += 1
                'Console.WriteLine(" 차트종목 : " + CStr(SLEnum.Key.ToString) + " | " + SLEnum.Value.ToString)
            End If
        End While

        '///
        Dim sDate As String, k As Integer
        Dim nChartIndex As Integer
        Dim nTotalChartIndex As Integer = 0
        Dim arrChartValue() As Long = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        Dim nLastData As Integer = 0
        Dim nPrintLinePeed As Integer
        Dim lstStockSellBuySub As New List(Of StockSellBuyInfoSub)()
        Dim stockSellBuySub As StockSellBuyInfoSub
        Dim FILE_NAME As String = "f:\googleChart.html"

        If System.IO.File.Exists(FILE_NAME) = True Then
            System.IO.File.Delete(FILE_NAME)
        End If

        Dim f As New System.IO.StreamWriter(FILE_NAME, True)
        f.WriteLine("<html>")
        f.WriteLine("<head>")
        f.WriteLine("<script type='text/javascript' src='https://www.gstatic.com/charts/loader.js'></script>")
        f.WriteLine("<script type='text/javascript'>")
        f.WriteLine("google.charts.load('current', {packages: ['corechart', 'line']});")
        f.WriteLine("google.charts.setOnLoadCallback(drawLineColors);")
        f.WriteLine("function drawLineColors() {")
        f.WriteLine("var data = new google.visualization.DataTable();")
        f.WriteLine("data.addColumn('string', '날짜');")
        For Each _sCompanyName In gListCompanyName
            f.WriteLine("data.addColumn('number', '" + _sCompanyName + "');")
        Next

        nLastData = gListStockSellBuyInfoMain.Count

        '// chart line 차트 데이터
        f.WriteLine("data.addRows([")
        For Each _stockSellBuyInfoMain In gListStockSellBuyInfoMain
            nChartIndex = 0
            arrChartValue = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
            sDate = _stockSellBuyInfoMain.getCurrDate()
            Console.WriteLine("날짜:" + sDate)
            lstStockSellBuySub = _stockSellBuyInfoMain.getStockSellBuyInfo
            For Each _sCompanyName In gListCompanyName
                For Each stockSellBuySub In lstStockSellBuySub
                    If _sCompanyName = stockSellBuySub.getName Then
                        arrChartValue(nChartIndex) = stockSellBuySub.getOnlyBuyCount
                        nChartIndex += 1
                        'Console.WriteLine("getOnlyBuyCount:" + CStr(stockSellBuySub.getOnlyBuyCount))
                        Console.WriteLine(sDate + ". " + stockSellBuySub.getName + ", " + CStr(stockSellBuySub.getOnlyBuyCount))
                    End If
                Next
            Next

            Console.WriteLine("====================================================")

            'f.Write("[" + CStr(nTotalChartIndex))
            f.Write("[")
            f.Write("'" + sDate + "'")
            For k = 0 To nPrintStockCount - 1
                f.Write(",")
                f.Write(arrChartValue(k))
            Next
            f.Write("]")
            nTotalChartIndex += 1
            If nLastData <> nTotalChartIndex Then
                f.Write(",")
            End If
            If nPrintLinePeed >= 3 Then
                nPrintLinePeed = 0
                f.WriteLine("")
            End If
            nPrintLinePeed += 1
        Next

        f.WriteLine("")
        f.WriteLine("]);")

        '// chart option
        f.WriteLine("var options = {")
        f.WriteLine("chart: {")
        Dim title As String, subTitle As String
        title = "title: '" + frmMain.txtSuggest.Text + ", "
        subTitle = "분석 시작 날짜 : " + frmMain.txtStartDate1.Text + " ~ " + frmMain.txtEndDate1.Text + "',"
        f.WriteLine(title + subTitle)
        f.WriteLine("subtitle: 'in millions of dollars (USD)'")
        f.WriteLine("},")
        f.WriteLine("width: 1800,")
        f.WriteLine("height: 1000")
        f.WriteLine("};")

        f.WriteLine("var chart = new google.visualization.LineChart(document.getElementById('chart_div'));")
        f.WriteLine("chart.draw(data, options);")
        f.WriteLine("}")
        f.WriteLine("</script>")

        f.WriteLine("</head>")
        f.WriteLine("<body>")
        f.WriteLine("<div id='chart_div'></div>")
        f.WriteLine("</body>")
        f.WriteLine("</html>")
        f.Close()

        MsgBox("차트 데이터 생성 완료!")

            '// run 브라우저
        GlobalDefine.runBrowser(FILE_NAME)


    End Sub

    Public Sub drawGoogleChartTest()
        '// 차트 그리기 위한 종목 선정
        Dim chartPrintCountBreaek As Integer = 0
        Dim total As Integer = 0
        Dim nPrintStockCount As Integer = 0

        Dim sDate As String
        Dim nTotalChartIndex As Integer = 0
        Dim arrChartValue() As Long = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        Dim nLastData As Integer = 0
        Dim nPrintLinePeed As Integer
        Dim lSellBuyTotal As Long = 0
        Dim nSameCount As Integer = 0

        Dim lstMain As New List(Of StockSellBuyInfoMain)()
        Dim stockMain As StockSellBuyInfoMain
        Dim stockSub As StockSellBuyInfoSub

        Dim lstStockSellBuySub As New List(Of StockSellBuyInfoSub)()
        Dim stockSellBuySub As StockSellBuyInfoSub
        Dim beforeStockSellBuySub As StockSellBuyInfoSub
        Dim beforeStockData As StockSellBuyInfoSub
        Dim hashBeforeStockSellBuySub As New Hashtable
        Dim hashAppendSellBuyValue As New Hashtable
        Dim hashRemoveDate As New Hashtable
        Dim FILE_NAME As String = "f:\googleChart.html"

        '// 누적 값을 구한다
        For Each _stockSellBuyInfoMain In gListStockSellBuyInfoMain
            lstStockSellBuySub = _stockSellBuyInfoMain.getStockSellBuyInfo
            sDate = _stockSellBuyInfoMain.getCurrDate
            nSameCount = 0
            stockMain = New StockSellBuyInfoMain
            '// 해당 날짜의 종목별 증권사 정보 리스트 순회
            For Each stockSellBuySub In lstStockSellBuySub
                '// 해당 날짜의 종목별 증권사 한개 처리
                If hashAppendSellBuyValue.Contains(stockSellBuySub.getName) = True Then

                    beforeStockData = hashBeforeStockSellBuySub(stockSellBuySub.getName)

                    If stockSellBuySub.getOnlyBuyCount = beforeStockData.getOnlyBuyCount And
                        stockSellBuySub.getBuyCount = beforeStockData.getBuyCount And
                        stockSellBuySub.getSellCount = beforeStockData.getSellCount Then

                        nSameCount = nSameCount + 1

                    Else
                        lSellBuyTotal = hashAppendSellBuyValue(stockSellBuySub.getName)
                        lSellBuyTotal = lSellBuyTotal + stockSellBuySub.getOnlyBuyCount
                        hashAppendSellBuyValue.Remove(stockSellBuySub.getName)
                        hashAppendSellBuyValue.Add(stockSellBuySub.getName, lSellBuyTotal)

                        '// 누적 순매수/순매도 값을 저장 (이 값을 차트로 보여준다)
                        stockSub = New StockSellBuyInfoSub
                        stockSub.setData(stockSellBuySub.getName, lSellBuyTotal, 0, 0)
                        stockMain.addStockSellBuyInfo(stockSub)

                        '// 이전값 비교를 위한 현재값 저장
                        beforeStockSellBuySub = New StockSellBuyInfoSub
                        beforeStockSellBuySub.setData(stockSellBuySub.getName, stockSellBuySub.getOnlyBuyCount, stockSellBuySub.getSellCount, stockSellBuySub.getBuyCount)
                        hashBeforeStockSellBuySub.Remove(stockSellBuySub.getName)
                        hashBeforeStockSellBuySub.Add(stockSellBuySub.getName, beforeStockSellBuySub)
                    End If
                Else
                    '// 누적 순매수/순매도 합산
                    hashAppendSellBuyValue.Add(stockSellBuySub.getName, stockSellBuySub.getOnlyBuyCount)

                    '// 누적 순매수/순매도 값을 저장 (이 값을 차트로 보여준다)
                    stockSub = New StockSellBuyInfoSub
                    stockSub.setData(stockSellBuySub.getName, stockSellBuySub.getOnlyBuyCount, 0, 0)
                    stockMain.addStockSellBuyInfo(stockSub)

                    '// 종목별 새로운 구조체 생성
                    beforeStockSellBuySub = New StockSellBuyInfoSub
                    '// 구조체에 값 셋팅
                    beforeStockSellBuySub.setData(stockSellBuySub.getName, stockSellBuySub.getOnlyBuyCount, stockSellBuySub.getSellCount, stockSellBuySub.getBuyCount)
                    '// hash에다가 현재 로딩된 종목 데이터 추가
                    hashBeforeStockSellBuySub.Remove(stockSellBuySub.getName)
                    hashBeforeStockSellBuySub.Add(stockSellBuySub.getName, beforeStockSellBuySub)
                End If
            Next

            '// main 클래스에 추가
            stockMain.setCurrDate(sDate)
            lstMain.Add(stockMain)

            If nSameCount = lstStockSellBuySub.Count Then
                hashRemoveDate.Add(sDate, "1")
                Console.WriteLine("skip:" + sDate)
            End If

        Next


        '// 정렬을 하기 위해서 hashtabel 복사
        Dim hashTemp As New Hashtable
        Dim lstSortedSellBuyInfoCompany As New List(Of String)()
        Dim lstSortedCompanyTemp As New List(Of String)()
        Dim hashLColl As ICollection
        Dim MAX_CHART_PRINT As Integer = 5
        Dim nPrintCount As Integer = 0
        hashLColl = hashAppendSellBuyValue.Keys
        For Each key In hashLColl
            hashTemp.Add(key.ToString, hashAppendSellBuyValue(key.ToString))
        Next

        '// hashtabel 정렬
        lstSortedCompanyTemp = GlobalDefine.sortHashtable(hashTemp)
        MAX_CHART_PRINT = CInt(Trim(frmMain.txtChartCount.Text))
        'Console.WriteLine("====================================================")
        For Each _sCompany In lstSortedCompanyTemp
            lstSortedSellBuyInfoCompany.Add(_sCompany)
            'Console.WriteLine("{0}, {1}", _sCompany, hashAppendSellBuyValue(_sCompany))
            nPrintCount += 1
            If nPrintCount >= MAX_CHART_PRINT Then
                Exit For
            End If
        Next

        If System.IO.File.Exists(FILE_NAME) = True Then
            System.IO.File.Delete(FILE_NAME)
        End If

        '// 차트 파일 오픈
        Dim f As New System.IO.StreamWriter(FILE_NAME, True)

        f.WriteLine("<html>")
        f.WriteLine("<head>")
        f.WriteLine("<script type='text/javascript' src='https://www.gstatic.com/charts/loader.js'></script>")
        f.WriteLine("<script type='text/javascript'>")
        f.WriteLine("google.charts.load('current', {packages: ['corechart', 'line']});")
        f.WriteLine("google.charts.setOnLoadCallback(drawLineColors);")
        f.WriteLine("function drawLineColors() {")
        f.WriteLine("var data = new google.visualization.DataTable();")
        f.WriteLine("data.addColumn('string', '날짜');")

        '// 차트에 표현할 증권사 추가 (순매수 많은 순)
        For Each _sCompanyName In lstSortedSellBuyInfoCompany
            f.WriteLine("data.addColumn('number', '" + _sCompanyName + "');")
        Next

        '// 차트 날짜 개수
        nLastData = lstMain.Count

        '// chart line 차트 데이터
        f.WriteLine("data.addRows([")
        For Each _stockSellBuyInfoMain In lstMain

            sDate = _stockSellBuyInfoMain.getCurrDate()

            If hashRemoveDate.Contains(sDate) = True Then
                Console.WriteLine("SKIP 날짜:" + sDate)
                Continue For
            End If

            Console.WriteLine("OK 날짜:" + sDate)

            '// 차트 데이터 넣기 시작.
            f.Write("[")
            f.Write("'" + sDate + "'")

            lstStockSellBuySub = _stockSellBuyInfoMain.getStockSellBuyInfo
            'For Each _sCompanyName In gListCompanyName
            For Each _sCompanyName In lstSortedSellBuyInfoCompany
                For Each stockSellBuySub In lstStockSellBuySub
                    If _sCompanyName = stockSellBuySub.getName Then
                        f.Write(",")
                        f.Write(stockSellBuySub.getOnlyBuyCount)
                        'Console.WriteLine(sDate + ". " + stockSellBuySub.getName + ", " + CStr(stockSellBuySub.getOnlyBuyCount))
                    End If
                Next
            Next

            f.Write("]")

            Console.WriteLine("====================================================")

            'f.Write("[")
            'f.Write("'" + sDate + "'")
            'For k = 0 To nPrintStockCount - 1
            '    f.Write(",")
            '    f.Write(arrChartValue(k))
            'Next
            'f.Write("]")
            nTotalChartIndex += 1
            If nLastData <> nTotalChartIndex Then
                f.Write(",")
            End If

            '// line peed
            If nPrintLinePeed >= 3 Then
                nPrintLinePeed = 0
                f.WriteLine("")
            End If
            nPrintLinePeed += 1
        Next

        f.WriteLine("")
        f.WriteLine("]);")

        '// chart option
        f.WriteLine("var options = {")
        f.WriteLine("chart: {")
        Dim title As String, subTitle As String
        title = "title: '" + frmMain.txtSuggest.Text + ", "
        subTitle = "분석 시작 날짜 : " + frmMain.txtStartDate1.Text + " ~ " + frmMain.txtEndDate1.Text + "',"
        f.WriteLine(title + subTitle)
        f.WriteLine("subtitle: 'in millions of dollars (USD)'")

        f.WriteLine("},")
        f.WriteLine("width: 1600,")
        f.WriteLine("height: 900")

        f.WriteLine("};")

        'f.WriteLine("var chart = new google.visualization.LineChart(document.getElementById('chart_div'));")
        f.WriteLine("var chart = new google.charts.Line(document.getElementById('chart_div'));")
        f.WriteLine("chart.draw(data, options);")
        f.WriteLine("}")
        f.WriteLine("</script>")

        f.WriteLine("</head>")
        f.WriteLine("<body>")
        f.WriteLine("<div id='chart_div'></div>")
        f.WriteLine("</body>")
        f.WriteLine("</html>")
        f.Close()

        Console.WriteLine("차트 데이터 생성 완료!")

        '// run 브라우저
        GlobalDefine.runBrowser(FILE_NAME)

    End Sub

    Public Sub drawHighStockChart()
        '// 차트 그리기 위한 종목 선정
        Dim chartPrintCountBreaek As Integer = 0
        Dim total As Integer = 0
        Dim nPrintStockCount As Integer = 0

        Dim sDate As String
        Dim nTotalChartIndex As Integer = 0
        Dim arrChartValue() As Long = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        Dim nLastData As Integer = 0
        Dim lSellBuyTotal As Long = 0
        Dim nSameCount As Integer = 0

        Dim lstMain As New List(Of StockSellBuyInfoMain)()
        Dim stockMain As StockSellBuyInfoMain
        Dim stockSub As StockSellBuyInfoSub

        Dim lstStockSellBuySub As New List(Of StockSellBuyInfoSub)()
        Dim stockSellBuySub As StockSellBuyInfoSub
        Dim beforeStockSellBuySub As StockSellBuyInfoSub
        Dim beforeStockData As StockSellBuyInfoSub
        Dim hashBeforeStockSellBuySub As New Hashtable
        Dim hashAppendSellBuyValue As New Hashtable
        Dim hashRemoveDate As New Hashtable
        Dim FILE_NAME As String = "C:\Temp\daeching\" + frmMain.txtSuggest.Text + ".html"

        '// 누적 값을 구한다
        For Each _stockSellBuyInfoMain In gListStockSellBuyInfoMain
            lstStockSellBuySub = _stockSellBuyInfoMain.getStockSellBuyInfo
            sDate = _stockSellBuyInfoMain.getCurrDate
            nSameCount = 0
            stockMain = New StockSellBuyInfoMain
            '// 해당 날짜의 종목별 증권사 정보 리스트 순회
            For Each stockSellBuySub In lstStockSellBuySub
                '// 해당 날짜의 종목별 증권사 한개 처리
                If hashAppendSellBuyValue.Contains(stockSellBuySub.getName) = True Then

                    beforeStockData = hashBeforeStockSellBuySub(stockSellBuySub.getName)

                    If stockSellBuySub.getOnlyBuyCount = beforeStockData.getOnlyBuyCount And
                        stockSellBuySub.getBuyCount = beforeStockData.getBuyCount And
                        stockSellBuySub.getSellCount = beforeStockData.getSellCount Then

                        nSameCount = nSameCount + 1

                    Else
                        lSellBuyTotal = hashAppendSellBuyValue(stockSellBuySub.getName)
                        lSellBuyTotal = lSellBuyTotal + stockSellBuySub.getOnlyBuyCount
                        hashAppendSellBuyValue.Remove(stockSellBuySub.getName)
                        hashAppendSellBuyValue.Add(stockSellBuySub.getName, lSellBuyTotal)

                        '// 누적 순매수/순매도 값을 저장 (이 값을 차트로 보여준다)
                        stockSub = New StockSellBuyInfoSub
                        stockSub.setData(stockSellBuySub.getName, lSellBuyTotal, 0, 0)
                        stockMain.addStockSellBuyInfo(stockSub)

                        '// 이전값 비교를 위한 현재값 저장
                        beforeStockSellBuySub = New StockSellBuyInfoSub
                        beforeStockSellBuySub.setData(stockSellBuySub.getName, stockSellBuySub.getOnlyBuyCount, stockSellBuySub.getSellCount, stockSellBuySub.getBuyCount)
                        hashBeforeStockSellBuySub.Remove(stockSellBuySub.getName)
                        hashBeforeStockSellBuySub.Add(stockSellBuySub.getName, beforeStockSellBuySub)
                    End If
                Else
                    '// 누적 순매수/순매도 합산
                    hashAppendSellBuyValue.Add(stockSellBuySub.getName, stockSellBuySub.getOnlyBuyCount)

                    '// 누적 순매수/순매도 값을 저장 (이 값을 차트로 보여준다)
                    stockSub = New StockSellBuyInfoSub
                    stockSub.setData(stockSellBuySub.getName, stockSellBuySub.getOnlyBuyCount, 0, 0)
                    stockMain.addStockSellBuyInfo(stockSub)

                    '// 종목별 새로운 구조체 생성
                    beforeStockSellBuySub = New StockSellBuyInfoSub
                    '// 구조체에 값 셋팅
                    beforeStockSellBuySub.setData(stockSellBuySub.getName, stockSellBuySub.getOnlyBuyCount, stockSellBuySub.getSellCount, stockSellBuySub.getBuyCount)
                    '// hash에다가 현재 로딩된 종목 데이터 추가
                    hashBeforeStockSellBuySub.Remove(stockSellBuySub.getName)
                    hashBeforeStockSellBuySub.Add(stockSellBuySub.getName, beforeStockSellBuySub)
                End If
            Next

            '// main 클래스에 추가
            stockMain.setCurrDate(sDate)
            lstMain.Add(stockMain)

            If nSameCount = lstStockSellBuySub.Count Then
                hashRemoveDate.Add(sDate, "1")
                Console.WriteLine("skip:" + sDate)
            End If

        Next


        '// 정렬을 하기 위해서 hashtabel 복사
        Dim hashTemp As New Hashtable
        Dim lstSortedSellBuyInfoCompany As New List(Of String)()
        Dim lstSortedCompanyTemp As New List(Of String)()
        Dim hashLColl As ICollection
        Dim MAX_CHART_PRINT As Integer = 5
        Dim nPrintCount As Integer = 0
        hashLColl = hashAppendSellBuyValue.Keys
        For Each key In hashLColl
            hashTemp.Add(key.ToString, hashAppendSellBuyValue(key.ToString))
        Next

        '// hashtabel 정렬
        lstSortedCompanyTemp = GlobalDefine.sortHashtable(hashTemp)
        MAX_CHART_PRINT = CInt(Trim(frmMain.txtChartCount.Text))
        'Console.WriteLine("====================================================")
        For Each _sCompany In lstSortedCompanyTemp
            lstSortedSellBuyInfoCompany.Add(_sCompany)
            'Console.WriteLine("{0}, {1}", _sCompany, hashAppendSellBuyValue(_sCompany))
            nPrintCount += 1
            If nPrintCount >= MAX_CHART_PRINT Then
                Exit For
            End If
        Next

        If System.IO.File.Exists(FILE_NAME) = True Then
            System.IO.File.Delete(FILE_NAME)
        End If

        '// 차트 파일 오픈
        Dim f As New System.IO.StreamWriter(FILE_NAME, True)

        f.WriteLine("<html>")
        f.WriteLine("<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />")
        'f.WriteLine("<meta http-equiv=""refresh"" content=""30"" />")
        f.WriteLine("<head>")
        f.WriteLine("<script src=""http://code.jquery.com/jquery-latest.js""></script>")
        f.WriteLine("<script src=""https://code.highcharts.com/stock/highstock.js""></script>")
        f.WriteLine("<script src=""https://code.highcharts.com/stock/modules/exporting.js""></script>")
        f.WriteLine("<script type='text/javascript'>")

        f.WriteLine("$(function () {")
        f.WriteLine("var seriesOptions = [],")
        f.WriteLine("seriesCounter = 0,")

        '// 회원사 리스트 나열
        f.WriteLine("names = [")
        Dim nCompCount As Integer = 0
        For Each _sCompanyName In lstSortedSellBuyInfoCompany
            nCompCount += 1
            If nCompCount < lstSortedSellBuyInfoCompany.Count Then
                f.WriteLine("'" + _sCompanyName + "',")
            Else
                f.WriteLine("'" + _sCompanyName + "'")
            End If
        Next
        f.WriteLine("];")

        f.WriteLine("function createChart() {")
        f.WriteLine("$('#container').highcharts('StockChart', {")
        f.WriteLine("rangeSelector: {")
        f.WriteLine("selected: 4")
        f.WriteLine("},")

        '// title
        f.WriteLine("title: {")
        f.WriteLine("   text: '종목이름 : " + frmMain.txtSuggest.Text + " (" + frmMain.txtStartDate1.Text + " ~ " + frmMain.txtEndDate1.Text + ")',")
        f.WriteLine("   x: -20")
        f.WriteLine("},")

        '// subtitle
        'f.WriteLine("subtitle: {")
        'f.WriteLine("   text: '분석기간 : " + frmMain.txtStartDate1.Text + " ~ " + frmMain.txtEndDate1.Text + ",'")
        'f.WriteLine("   x: -20")
        'f.WriteLine("},")

        '// xAxis
        f.WriteLine("xAxis: {")
        f.WriteLine("   labels: {")
        f.WriteLine("       format: '{value}'")
        f.WriteLine("   }")
        f.WriteLine("},")

        '// legend
        f.WriteLine("legend: {")
        f.WriteLine("   layout: 'vertical',")
        f.WriteLine("   align: 'right',")
        f.WriteLine("   verticalAlign: 'middle',")
        f.WriteLine("   borderWidth: 0")
        f.WriteLine("},")

        '// yAxis
        f.WriteLine("yAxis: {")
        f.WriteLine("   labels: {")
        f.WriteLine("       formatter: function () {")
        'f.WriteLine("return (this.value > 0 ? ' + ' : '') + this.value + '%';")
        f.WriteLine("           return this.value")
        f.WriteLine("       }")
        f.WriteLine("   },")
        f.WriteLine("   plotLines: [{")
        f.WriteLine("       value: 0,")
        f.WriteLine("       width: 2,")
        f.WriteLine("       color: 'silver'")
        f.WriteLine("   }]")
        f.WriteLine("   },")
        f.WriteLine("   plotOptions: {")
        f.WriteLine("       series: {")
        f.WriteLine("           compare: 'value'")
        f.WriteLine("       }")
        f.WriteLine("   },")
        f.WriteLine("   tooltip: {")
        f.WriteLine("       pointFormat: '<span style=""color:{series.color}"">{series.name}</span>: <b>{point.y}</b>, {point.x}<br/>',")
        f.WriteLine("       valueDecimals: 2")
        f.WriteLine("   },")
        f.WriteLine("   series: seriesOptions")
        f.WriteLine("});")
        f.WriteLine("}")

        f.WriteLine("$.each(names, function (i, name) {")

        Dim nCompanyCount As Integer = 0
        Dim nListCount As Integer = 0

        '// 프린트할 회원사별로 처음부터 끝까지 그린다.
        For Each _sCompanyName In lstSortedSellBuyInfoCompany
            f.WriteLine("  seriesOptions[" + CStr(nCompanyCount) + "] = {")
            f.WriteLine("       name: '" + _sCompanyName + "',")
            f.WriteLine("       marker : {")
            f.WriteLine("           enabled : true,")
            f.WriteLine("           radius : 3")
            f.WriteLine("       },")
            f.Write("     data: [")
            nListCount = 0
            For Each _stockSellBuyInfoMain In lstMain
                '// 날짜
                sDate = _stockSellBuyInfoMain.getCurrDate()
                '// 주말, 공휴일은 버린다.
                If hashRemoveDate.Contains(sDate) = True Then
                    Console.WriteLine("SKIP 날짜:" + sDate)
                    Continue For
                End If

                lstStockSellBuySub = _stockSellBuyInfoMain.getStockSellBuyInfo
                For Each stockSellBuySub In lstStockSellBuySub
                    If _sCompanyName = stockSellBuySub.getName Then
                        If nListCount = 0 Then
                            f.Write("[" + sDate + "," + CStr(stockSellBuySub.getOnlyBuyCount) + "]")
                        Else
                            f.Write(",[" + sDate + "," + CStr(stockSellBuySub.getOnlyBuyCount) + "]")
                        End If
                    End If
                Next
                nListCount += 1
            Next

            f.WriteLine("]") '// f.WriteLine("    data: [") 의 끝.
            f.WriteLine("   };")
            nCompanyCount += 1
        Next


        f.WriteLine("createChart();")
        f.WriteLine("});")
        f.WriteLine("});")
        f.WriteLine("</script>")

        f.WriteLine("</head>")
        f.WriteLine("<body>")
        'f.WriteLine("<div id=""container"" style=""height: 200px; min-width: 310px""></div>")
        f.WriteLine("<div id=""container"" style=""height: 90%; min-width: 70%""></div>")
        f.WriteLine("</body>")
        f.WriteLine("</html>")
        f.Close()

        Console.WriteLine("차트 데이터 생성 완료!")

        '// run 브라우저
        If frmMain.chkBrowser.Checked = True Then
            GlobalDefine.runBrowser(FILE_NAME)
        End If

    End Sub

End Class
