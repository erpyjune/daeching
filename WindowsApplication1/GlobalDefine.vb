Module GlobalDefine

    Dim strHowCommand As String
    Dim gStockCompany As New List(Of String)()

    Public Structure StockCompSub
        Dim vDate As String
        Dim vOnlyBuyCount As Long
        Dim vBuyCount As Long
        Dim vSaleCount As Long
    End Structure
    Public Structure StockCompMain
        Dim vName As String
        Dim stockByCompValue As List(Of StockCompSub)()
        'Dim stockByCompValue() As StockDateCount
    End Structure

    Function checkMonthDay(ByVal num As Integer)
        Return Format(num, "00").ToString
    End Function

    Public Sub runBrowser(ByVal path As String)
        Dim ie As Object
        ie = CreateObject("InternetExplorer.Application")
        ie.Visible = True
        ie.width = 800              ' 주소창 너비
        ie.height = 400             ' 주소창 높이
        ie.AddressBar = False  ' 주소입력창 숨기기
        ie.ToolBar = False        ' Toolbar 숨기기
        ie.MenuBar = False      ' 메뉴 숨기기
        ie.StatusBar = False     ' 상태바 숨기기
        ie.Resizable = True     ' 크기 조절 금지
        ie.Navigate(path)
        ie = Nothing
    End Sub

    '// hashtable의 value 기준으로 정렬한다.
    '// return은 정렬된 key array list 이다.
    Function sortHashtable(ByRef table As Hashtable)
        Dim hTable As New Hashtable
        Dim lstSortedSellBuyInfo As New List(Of String)()

        Dim listSortedSellBuyCompany As New SortedList
        Dim arrCompName(100) As String
        Dim arrCompValue(100) As Long
        Dim nArrTotal As Integer = 0
        Dim sMaxKey As String = ""
        Dim nMaxValue As Integer = 0
        Dim lstSortedCompany As New List(Of String)()
        Dim key As Object
        Dim htKeys As ICollection

        Do While True
            htKeys = table.Keys
            For Each key In htKeys
                If table(key.ToString) >= nMaxValue Then
                    sMaxKey = key.ToString
                    nMaxValue = table(key.ToString)
                End If
            Next
            lstSortedSellBuyInfo.Add(sMaxKey)
            'Console.WriteLine("key : {0}, {1}, {2}", sMaxKey, table(sMaxKey), table.Count)
            table.Remove(sMaxKey)
            nMaxValue = -99999999
            If table.Count = 0 Then
                Exit Do
            End If
            Application.DoEvents()
        Loop

        Return lstSortedSellBuyInfo

    End Function

End Module
