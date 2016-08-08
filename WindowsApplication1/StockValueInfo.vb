Public Class StockValueInfo
    Private name As String      '// 종목이름
    Private code As String      '// 종목코드
    Private curDate As String   '// 날짜
    Private startV As Integer   '// 시가
    Private sstartV As String   '// +- 시가
    Private endV As Integer     '// 종가
    Private sendV As String     '// +- 종가
    Private tv As Integer       '// 거래량
    Sub setName(ByVal stockName As String)
        Me.name = stockName
    End Sub
    Sub setCode(ByVal stockCode As String)
        Me.code = stockCode
    End Sub
    Sub setCurDate(ByVal curDate As String)
        Me.curDate = curDate
    End Sub
    Sub setStartV(ByVal nStartV As Integer)
        Me.startV = nStartV
    End Sub
    Sub setStrStartV(ByVal strStartV As String)
        Me.sstartV = strStartV
    End Sub
    Sub setEndV(ByVal nEndV As Integer)
        Me.endV = nEndV
    End Sub
    Sub setStrEndV(ByVal strEndV As String)
        Me.sendV = strEndV
    End Sub
    Sub setTV(ByVal nTradeValue As Integer)
        Me.tv = nTradeValue
    End Sub
    Sub setStockValue(ByVal curDate As String, ByVal startVal As Integer, ByVal endVal As Integer, ByVal tradeVal As Integer, ByVal name As String, ByVal code As String)
        Me.curDate = curDate
        Me.startV = startVal
        Me.endV = endVal
        Me.tv = tradeVal
        Me.name = name
        Me.code = code
    End Sub

    Function getCurDate()
        Return Me.curDate
    End Function

    Function getStartV()
        Return Me.startV
    End Function
    Function getStrStartV()
        Return Me.sstartV
    End Function

    Function getEndV()
        Return Me.endV
    End Function
    Function getStrEndV()
        Return Me.sendV
    End Function

    Function getTradeV()
        Return Me.tv
    End Function

    Function getName()
        Return Me.name
    End Function

    Function getCode()
        Return Me.code
    End Function

End Class
