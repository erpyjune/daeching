Public Class StockBizInfo

    Private nStockTotalCount As Integer     '// 상장주식
    Private nCompanyTotalValue As Integer   '// 시가총액
    Private nSaleProfitValue As Integer     '// 영업이익
    Private nSaleNetProfitValue As Integer  '// 당기순이익

    Sub setData(ByVal stockTotalCount As Integer, ByVal companyTotalValue As Integer, ByVal saleProfit As Integer, ByVal saleNetProfit As Integer)
        Me.nStockTotalCount = stockTotalCount
        Me.nCompanyTotalValue = companyTotalValue
        Me.nSaleProfitValue = saleProfit
        Me.nSaleNetProfitValue = saleNetProfit
    End Sub

    Function getStockTotalCount()
        Return Me.nStockTotalCount
    End Function

    Function getCompayTotalValue()
        Return Me.nCompanyTotalValue
    End Function

    Function getSaleProfitValue()
        Return Me.nSaleProfitValue
    End Function
    Function getSaleNetProfitValue()
        Return Me.nSaleNetProfitValue
    End Function

End Class
