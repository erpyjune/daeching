Public Class StockValueInfo
    Private name As String
    Private code As String
    Private curDate As String
    Private startV As Integer
    Private endV As Integer
    Private tv As Integer

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

    Function getEndV()
        Return Me.endV
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
