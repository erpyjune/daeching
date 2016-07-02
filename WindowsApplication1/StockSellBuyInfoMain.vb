Public Class StockSellBuyInfoMain
    Private currDate As String
    Private stockSellBuyInfoSub As New List(Of StockSellBuyInfoSub)()

    Function getCurrDate()
        Return Me.currDate
    End Function

    Function getStockSellBuyInfo()
        Return Me.stockSellBuyInfoSub
    End Function

    Sub addStockSellBuyInfo(ByVal stockInfo As StockSellBuyInfoSub)
        Me.stockSellBuyInfoSub.Add(stockInfo)
    End Sub

    Sub setCurrDate(ByVal d As String)
        Me.currDate = d
    End Sub

End Class
