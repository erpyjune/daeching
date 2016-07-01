<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    Public WithEvents KHOpenAPI As AxKHOpenAPILib.AxKHOpenAPI
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))


    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.KHOpenAPI = New AxKHOpenAPILib.AxKHOpenAPI()
        Me.lstMsg = New System.Windows.Forms.ListBox()
        Me.txtStockCode = New System.Windows.Forms.TextBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.lstView1 = New System.Windows.Forms.ListView()
        Me.날짜 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.증권사 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.누적순매수 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.매수 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.매도 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCmd1 = New System.Windows.Forms.Button()
        Me.txtStartDate1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtEndDate1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtEndDate2 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtStartDate2 = New System.Windows.Forms.TextBox()
        Me.btnCmd2 = New System.Windows.Forms.Button()
        Me.lstView2 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtEndDate3 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtStartDate3 = New System.Windows.Forms.TextBox()
        Me.btnCmd3 = New System.Windows.Forms.Button()
        Me.lstView3 = New System.Windows.Forms.ListView()
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chartStock = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.KHOpenAPI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnLogin
        '
        Me.btnLogin.Location = New System.Drawing.Point(12, 12)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(100, 25)
        Me.btnLogin.TabIndex = 0
        Me.btnLogin.Text = "로그인"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'KHOpenAPI
        '
        Me.KHOpenAPI.Enabled = True
        Me.KHOpenAPI.Location = New System.Drawing.Point(12, 43)
        Me.KHOpenAPI.Name = "KHOpenAPI"
        Me.KHOpenAPI.OcxState = CType(resources.GetObject("KHOpenAPI.OcxState"), System.Windows.Forms.AxHost.State)
        Me.KHOpenAPI.Size = New System.Drawing.Size(66, 20)
        Me.KHOpenAPI.TabIndex = 0
        '
        'lstMsg
        '
        Me.lstMsg.FormattingEnabled = True
        Me.lstMsg.ItemHeight = 12
        Me.lstMsg.Location = New System.Drawing.Point(118, 12)
        Me.lstMsg.Name = "lstMsg"
        Me.lstMsg.Size = New System.Drawing.Size(407, 52)
        Me.lstMsg.TabIndex = 1
        '
        'txtStockCode
        '
        Me.txtStockCode.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtStockCode.Location = New System.Drawing.Point(46, 89)
        Me.txtStockCode.Name = "txtStockCode"
        Me.txtStockCode.Size = New System.Drawing.Size(91, 22)
        Me.txtStockCode.TabIndex = 2
        Me.txtStockCode.Text = "088910"
        Me.txtStockCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(604, 17)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(95, 20)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "조회"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'lstView1
        '
        Me.lstView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.날짜, Me.증권사, Me.누적순매수, Me.매수, Me.매도})
        Me.lstView1.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lstView1.Location = New System.Drawing.Point(12, 167)
        Me.lstView1.Name = "lstView1"
        Me.lstView1.Size = New System.Drawing.Size(513, 184)
        Me.lstView1.TabIndex = 4
        Me.lstView1.UseCompatibleStateImageBehavior = False
        Me.lstView1.View = System.Windows.Forms.View.Details
        '
        '날짜
        '
        Me.날짜.Text = "시작날짜"
        Me.날짜.Width = 78
        '
        '증권사
        '
        Me.증권사.Text = "증권사"
        Me.증권사.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.증권사.Width = 102
        '
        '누적순매수
        '
        Me.누적순매수.Text = "누적순매수"
        Me.누적순매수.Width = 110
        '
        '매수
        '
        Me.매수.Text = "매수"
        Me.매수.Width = 101
        '
        '매도
        '
        Me.매도.Text = "매도"
        Me.매도.Width = 100
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 92)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 12)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "종목"
        '
        'btnCmd1
        '
        Me.btnCmd1.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnCmd1.Location = New System.Drawing.Point(12, 132)
        Me.btnCmd1.Name = "btnCmd1"
        Me.btnCmd1.Size = New System.Drawing.Size(86, 29)
        Me.btnCmd1.TabIndex = 6
        Me.btnCmd1.Text = "차트/보기"
        Me.btnCmd1.UseVisualStyleBackColor = True
        '
        'txtStartDate1
        '
        Me.txtStartDate1.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtStartDate1.Location = New System.Drawing.Point(185, 137)
        Me.txtStartDate1.Name = "txtStartDate1"
        Me.txtStartDate1.Size = New System.Drawing.Size(89, 22)
        Me.txtStartDate1.TabIndex = 7
        Me.txtStartDate1.Text = "20160101"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.Location = New System.Drawing.Point(126, 140)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "시작날짜"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.Location = New System.Drawing.Point(297, 140)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "종료날짜"
        '
        'txtEndDate1
        '
        Me.txtEndDate1.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtEndDate1.Location = New System.Drawing.Point(356, 137)
        Me.txtEndDate1.Name = "txtEndDate1"
        Me.txtEndDate1.Size = New System.Drawing.Size(89, 22)
        Me.txtEndDate1.TabIndex = 9
        Me.txtEndDate1.Text = "20160630"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.Location = New System.Drawing.Point(297, 365)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "종료날짜"
        '
        'txtEndDate2
        '
        Me.txtEndDate2.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtEndDate2.Location = New System.Drawing.Point(356, 362)
        Me.txtEndDate2.Name = "txtEndDate2"
        Me.txtEndDate2.Size = New System.Drawing.Size(89, 22)
        Me.txtEndDate2.TabIndex = 15
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.Location = New System.Drawing.Point(126, 365)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "시작날짜"
        '
        'txtStartDate2
        '
        Me.txtStartDate2.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtStartDate2.Location = New System.Drawing.Point(185, 362)
        Me.txtStartDate2.Name = "txtStartDate2"
        Me.txtStartDate2.Size = New System.Drawing.Size(89, 22)
        Me.txtStartDate2.TabIndex = 13
        '
        'btnCmd2
        '
        Me.btnCmd2.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnCmd2.Location = New System.Drawing.Point(12, 357)
        Me.btnCmd2.Name = "btnCmd2"
        Me.btnCmd2.Size = New System.Drawing.Size(86, 29)
        Me.btnCmd2.TabIndex = 12
        Me.btnCmd2.Text = "보기"
        Me.btnCmd2.UseVisualStyleBackColor = True
        '
        'lstView2
        '
        Me.lstView2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.lstView2.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lstView2.Location = New System.Drawing.Point(12, 392)
        Me.lstView2.Name = "lstView2"
        Me.lstView2.Size = New System.Drawing.Size(513, 187)
        Me.lstView2.TabIndex = 11
        Me.lstView2.UseCompatibleStateImageBehavior = False
        Me.lstView2.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "시작날짜"
        Me.ColumnHeader1.Width = 75
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "증권사"
        Me.ColumnHeader2.Width = 106
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "누적순매수"
        Me.ColumnHeader3.Width = 125
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "매수"
        Me.ColumnHeader4.Width = 104
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "매도"
        Me.ColumnHeader5.Width = 99
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.Location = New System.Drawing.Point(297, 593)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "종료날짜"
        '
        'txtEndDate3
        '
        Me.txtEndDate3.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtEndDate3.Location = New System.Drawing.Point(356, 590)
        Me.txtEndDate3.Name = "txtEndDate3"
        Me.txtEndDate3.Size = New System.Drawing.Size(89, 22)
        Me.txtEndDate3.TabIndex = 21
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.Location = New System.Drawing.Point(126, 593)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 13)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "시작날짜"
        '
        'txtStartDate3
        '
        Me.txtStartDate3.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtStartDate3.Location = New System.Drawing.Point(185, 590)
        Me.txtStartDate3.Name = "txtStartDate3"
        Me.txtStartDate3.Size = New System.Drawing.Size(89, 22)
        Me.txtStartDate3.TabIndex = 19
        '
        'btnCmd3
        '
        Me.btnCmd3.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnCmd3.Location = New System.Drawing.Point(12, 585)
        Me.btnCmd3.Name = "btnCmd3"
        Me.btnCmd3.Size = New System.Drawing.Size(86, 29)
        Me.btnCmd3.TabIndex = 18
        Me.btnCmd3.Text = "보기"
        Me.btnCmd3.UseVisualStyleBackColor = True
        '
        'lstView3
        '
        Me.lstView3.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10})
        Me.lstView3.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lstView3.Location = New System.Drawing.Point(12, 620)
        Me.lstView3.Name = "lstView3"
        Me.lstView3.Size = New System.Drawing.Size(513, 168)
        Me.lstView3.TabIndex = 17
        Me.lstView3.UseCompatibleStateImageBehavior = False
        Me.lstView3.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "시작날짜"
        Me.ColumnHeader6.Width = 82
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "증권사"
        Me.ColumnHeader7.Width = 100
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "누적순매수"
        Me.ColumnHeader8.Width = 121
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "매수"
        Me.ColumnHeader9.Width = 119
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "매도"
        Me.ColumnHeader10.Width = 83
        '
        'chartStock
        '
        ChartArea1.Name = "ChartArea1"
        Me.chartStock.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.chartStock.Legends.Add(Legend1)
        Me.chartStock.Location = New System.Drawing.Point(531, 167)
        Me.chartStock.Name = "chartStock"
        Me.chartStock.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.chartStock.Series.Add(Series1)
        Me.chartStock.Size = New System.Drawing.Size(776, 621)
        Me.chartStock.TabIndex = 23
        Me.chartStock.Text = "Stock"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(601, 48)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(97, 30)
        Me.Button1.TabIndex = 24
        Me.Button1.Text = "chart"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1319, 800)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.chartStock)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtEndDate3)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtStartDate3)
        Me.Controls.Add(Me.btnCmd3)
        Me.Controls.Add(Me.lstView3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtEndDate2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtStartDate2)
        Me.Controls.Add(Me.btnCmd2)
        Me.Controls.Add(Me.lstView2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtEndDate1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtStartDate1)
        Me.Controls.Add(Me.btnCmd1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstView1)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.txtStockCode)
        Me.Controls.Add(Me.lstMsg)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.KHOpenAPI)
        Me.Name = "frmMain"
        Me.Text = "대칭이론"
        CType(Me.KHOpenAPI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents lstMsg As System.Windows.Forms.ListBox

    Private Sub KHOpenAPI_OnEventConnect(sender As Object, e As AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent) Handles KHOpenAPI.OnEventConnect
        ' 로그인 성공
        If e.nErrCode = 0 Then
            lstMsg.Items.Add(("로그인 성공!!!"))
            btnLogin.Text = "로그아웃"
            bLoginStatus = True
            ' 로그인 실패
        Else
            lstMsg.Items.Add(("로그인 실패!!!"))
            bLoginStatus = False
        End If
    End Sub
    Friend WithEvents txtStockCode As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lstView1 As System.Windows.Forms.ListView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCmd1 As System.Windows.Forms.Button
    Friend WithEvents txtStartDate1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtEndDate1 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtEndDate2 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtStartDate2 As System.Windows.Forms.TextBox
    Friend WithEvents btnCmd2 As System.Windows.Forms.Button
    Friend WithEvents lstView2 As System.Windows.Forms.ListView
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtEndDate3 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtStartDate3 As System.Windows.Forms.TextBox
    Friend WithEvents btnCmd3 As System.Windows.Forms.Button
    Friend WithEvents lstView3 As System.Windows.Forms.ListView
    Friend WithEvents 날짜 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 증권사 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 누적순매수 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 매수 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 매도 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents chartStock As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
