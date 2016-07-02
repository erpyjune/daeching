Public Class StockSellBuyInfoSub
    Private companyName As String '// 증권사명
    Private onlyBuyCount As Long '// 순매수 누적
    Private sellCount As Long    '// 매수 
    Private buyCount As Long     '// 매도

    Sub setData(ByVal name As String, ByVal obuy As Long, ByVal sell As Long, ByVal buy As Long)
        Me.companyName = name
        Me.onlyBuyCount = obuy
        Me.sellCount = sell
        Me.buyCount = buy
    End Sub

    Function getName()
        Return Me.companyName
    End Function

    Function getOnlyBuyCount()
        Return Me.onlyBuyCount
    End Function

    Function getSellCount()
        Return Me.sellCount
    End Function

    Function getBuyCount()
        Return Me.buyCount
    End Function

End Class
