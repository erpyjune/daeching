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

End Module
