Public Class StartPointInfo
    Private stockName As String
    Private stockCode As String
    Private curDate As String
    Private stockStatus As String
    Private nCompany1 As Integer
    Private nCompany2 As Integer
    Private nCompany3 As Integer
    Private nTradeCount As Integer
    Private nSPPrice As Integer

    Sub setData(ByVal name As String, ByVal code As String, ByVal curDate As String, ByVal status As String, ByVal company1 As Integer, _
                ByVal company2 As Integer, ByVal company3 As Integer, ByVal nTrade As Integer, ByVal nspPrice As Integer)
        Me.stockName = name
        Me.stockCode = code
        Me.curDate = curDate
        Me.stockStatus = status
        Me.nCompany1 = company1
        Me.nCompany2 = company2
        Me.nCompany3 = company3
        Me.nTradeCount = nTrade
        Me.nSPPrice = nspPrice
    End Sub

    Function getName()
        Return Me.stockName
    End Function

    Function getCode()
        Return Me.stockCode
    End Function

    Function getDate()
        Return Me.curDate
    End Function

    Function getStatus()
        Return Me.stockStatus
    End Function

    Function getCompany1()
        Return Me.nCompany1
    End Function

    Function getCompany2()
        Return Me.nCompany2
    End Function

    Function getCompany3()
        Return Me.nCompany3
    End Function

    Function getTradeCount()
        Return Me.nTradeCount
    End Function

    Function getSPPrice()
        Return Me.nSPPrice
    End Function

End Class
