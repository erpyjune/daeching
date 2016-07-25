Public Class StoreClass
    Dim listStockSaleBuyInfo As New List(Of StockSellBuyInfoSub)()

    Sub addStockSaleBuyInfo(ByVal data As StockSellBuyInfoSub)
        Me.listStockSaleBuyInfo.Add(data)
    End Sub

    Function getStockSaleBuyInfo()
        Return Me.listStockSaleBuyInfo
    End Function

    Function getStockSaleBuyListCount()
        Return Me.listStockSaleBuyInfo.Count
    End Function

    Sub setCleaerList()
        Me.listStockSaleBuyInfo.Clear()
    End Sub

End Class
