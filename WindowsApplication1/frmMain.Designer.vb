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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.KHOpenAPI = New AxKHOpenAPILib.AxKHOpenAPI()
        Me.lstMsg = New System.Windows.Forms.ListBox()
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
        Me.btnAnalBetween = New System.Windows.Forms.Button()
        Me.txtAnalStartDate = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtAnalEndDate = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.btnTodayCompany = New System.Windows.Forms.Button()
        Me.chkRefresh = New System.Windows.Forms.CheckBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnTimer = New System.Windows.Forms.Button()
        Me.cmbStock = New System.Windows.Forms.ComboBox()
        Me.txtSuggest = New System.Windows.Forms.TextBox()
        Me.txtStockCode = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Button4 = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.txtChartCount = New System.Windows.Forms.TextBox()
        Me.chkBrowser = New System.Windows.Forms.CheckBox()
        Me.btnOnlySellBuyAll = New System.Windows.Forms.Button()
        Me.btnStockCompanySort = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.btnMinBong = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.lstInfo = New System.Windows.Forms.ListBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.btnStockInitInfo = New System.Windows.Forms.Button()
        Me.txtSPToday = New System.Windows.Forms.TextBox()
        Me.Button14 = New System.Windows.Forms.Button()
        Me.txtSignValue = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Button15 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtSPTradeCount = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtEndPrice = New System.Windows.Forms.TextBox()
        Me.txtStartPrice = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtSPPer = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtJupoEndDate = New System.Windows.Forms.TextBox()
        Me.txtJupo1 = New System.Windows.Forms.TextBox()
        Me.txtJupoStartDate = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtSPBong = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.chkBizSunValue = New System.Windows.Forms.CheckBox()
        Me.chkBizValue = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtBunBongJupoSunCount = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtBunBongAnalEndDate = New System.Windows.Forms.TextBox()
        Me.txtBunBongAnalStartDate = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtBunBongEndPrice = New System.Windows.Forms.TextBox()
        Me.txtBunBongStartPrice = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtSignTradeValue = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtBunBongStartPer = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtAnalBunBong = New System.Windows.Forms.TextBox()
        Me.Button18 = New System.Windows.Forms.Button()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.ddd = New System.Windows.Forms.Label()
        Me.Button17 = New System.Windows.Forms.Button()
        Me.txtTradePrice = New System.Windows.Forms.TextBox()
        Me.Button16 = New System.Windows.Forms.Button()
        CType(Me.KHOpenAPI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnLogin
        '
        Me.btnLogin.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnLogin.Location = New System.Drawing.Point(8, 10)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(100, 25)
        Me.btnLogin.TabIndex = 0
        Me.btnLogin.Text = "로그인"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'KHOpenAPI
        '
        Me.KHOpenAPI.Enabled = True
        Me.KHOpenAPI.Location = New System.Drawing.Point(837, 666)
        Me.KHOpenAPI.Name = "KHOpenAPI"
        Me.KHOpenAPI.OcxState = CType(resources.GetObject("KHOpenAPI.OcxState"), System.Windows.Forms.AxHost.State)
        Me.KHOpenAPI.Size = New System.Drawing.Size(63, 18)
        Me.KHOpenAPI.TabIndex = 0
        '
        'lstMsg
        '
        Me.lstMsg.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lstMsg.FormattingEnabled = True
        Me.lstMsg.ItemHeight = 12
        Me.lstMsg.Location = New System.Drawing.Point(118, 12)
        Me.lstMsg.Name = "lstMsg"
        Me.lstMsg.Size = New System.Drawing.Size(199, 52)
        Me.lstMsg.TabIndex = 1
        '
        'btnSearch
        '
        Me.btnSearch.Enabled = False
        Me.btnSearch.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(97, 627)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(95, 27)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "조회"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'lstView1
        '
        Me.lstView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.날짜, Me.증권사, Me.누적순매수, Me.매수, Me.매도})
        Me.lstView1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lstView1.Location = New System.Drawing.Point(8, 156)
        Me.lstView1.Name = "lstView1"
        Me.lstView1.Size = New System.Drawing.Size(495, 123)
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
        Me.누적순매수.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.누적순매수.Width = 92
        '
        '매수
        '
        Me.매수.Text = "매수"
        Me.매수.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.매수.Width = 88
        '
        '매도
        '
        Me.매도.Text = "매도"
        Me.매도.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.매도.Width = 107
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "종목명"
        '
        'btnCmd1
        '
        Me.btnCmd1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnCmd1.Location = New System.Drawing.Point(1021, 351)
        Me.btnCmd1.Name = "btnCmd1"
        Me.btnCmd1.Size = New System.Drawing.Size(107, 29)
        Me.btnCmd1.TabIndex = 6
        Me.btnCmd1.Text = "코스닥분봉로딩"
        Me.btnCmd1.UseVisualStyleBackColor = True
        '
        'txtStartDate1
        '
        Me.txtStartDate1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtStartDate1.Location = New System.Drawing.Point(712, 43)
        Me.txtStartDate1.Name = "txtStartDate1"
        Me.txtStartDate1.Size = New System.Drawing.Size(82, 21)
        Me.txtStartDate1.TabIndex = 7
        Me.txtStartDate1.Text = "20160101"
        Me.txtStartDate1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.Location = New System.Drawing.Point(655, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "시작날짜"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.Location = New System.Drawing.Point(803, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "종료날짜"
        '
        'txtEndDate1
        '
        Me.txtEndDate1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtEndDate1.Location = New System.Drawing.Point(861, 43)
        Me.txtEndDate1.Name = "txtEndDate1"
        Me.txtEndDate1.Size = New System.Drawing.Size(89, 21)
        Me.txtEndDate1.TabIndex = 9
        Me.txtEndDate1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.Location = New System.Drawing.Point(347, 283)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "종료날짜"
        '
        'txtEndDate2
        '
        Me.txtEndDate2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtEndDate2.Location = New System.Drawing.Point(414, 280)
        Me.txtEndDate2.Name = "txtEndDate2"
        Me.txtEndDate2.Size = New System.Drawing.Size(89, 21)
        Me.txtEndDate2.TabIndex = 15
        Me.txtEndDate2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.Location = New System.Drawing.Point(204, 283)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "시작날짜"
        '
        'txtStartDate2
        '
        Me.txtStartDate2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtStartDate2.Location = New System.Drawing.Point(272, 281)
        Me.txtStartDate2.Name = "txtStartDate2"
        Me.txtStartDate2.Size = New System.Drawing.Size(75, 21)
        Me.txtStartDate2.TabIndex = 13
        Me.txtStartDate2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnCmd2
        '
        Me.btnCmd2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnCmd2.Location = New System.Drawing.Point(8, 281)
        Me.btnCmd2.Name = "btnCmd2"
        Me.btnCmd2.Size = New System.Drawing.Size(136, 20)
        Me.btnCmd2.TabIndex = 12
        Me.btnCmd2.Text = "종목별회원사현황2"
        Me.btnCmd2.UseVisualStyleBackColor = True
        '
        'lstView2
        '
        Me.lstView2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.lstView2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lstView2.Location = New System.Drawing.Point(8, 305)
        Me.lstView2.Name = "lstView2"
        Me.lstView2.Size = New System.Drawing.Size(495, 121)
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
        Me.ColumnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader2.Width = 113
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "누적순매수"
        Me.ColumnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader3.Width = 105
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "매수"
        Me.ColumnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader4.Width = 88
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "매도"
        Me.ColumnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader5.Width = 99
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.Location = New System.Drawing.Point(349, 431)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 12)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "종료날짜"
        '
        'txtEndDate3
        '
        Me.txtEndDate3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtEndDate3.Location = New System.Drawing.Point(414, 428)
        Me.txtEndDate3.Name = "txtEndDate3"
        Me.txtEndDate3.Size = New System.Drawing.Size(89, 21)
        Me.txtEndDate3.TabIndex = 21
        Me.txtEndDate3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.Location = New System.Drawing.Point(206, 431)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 12)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "시작날짜"
        '
        'txtStartDate3
        '
        Me.txtStartDate3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtStartDate3.Location = New System.Drawing.Point(272, 428)
        Me.txtStartDate3.Name = "txtStartDate3"
        Me.txtStartDate3.Size = New System.Drawing.Size(75, 21)
        Me.txtStartDate3.TabIndex = 19
        Me.txtStartDate3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnCmd3
        '
        Me.btnCmd3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnCmd3.Location = New System.Drawing.Point(7, 428)
        Me.btnCmd3.Name = "btnCmd3"
        Me.btnCmd3.Size = New System.Drawing.Size(142, 20)
        Me.btnCmd3.TabIndex = 18
        Me.btnCmd3.Text = "종목별 회원사현황3"
        Me.btnCmd3.UseVisualStyleBackColor = True
        '
        'lstView3
        '
        Me.lstView3.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10})
        Me.lstView3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lstView3.Location = New System.Drawing.Point(7, 453)
        Me.lstView3.Name = "lstView3"
        Me.lstView3.Size = New System.Drawing.Size(495, 121)
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
        Me.ColumnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader7.Width = 100
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "누적순매수"
        Me.ColumnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader8.Width = 97
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "매수"
        Me.ColumnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader9.Width = 101
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "매도"
        Me.ColumnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader10.Width = 91
        '
        'chartStock
        '
        ChartArea1.Name = "ChartArea1"
        Me.chartStock.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.chartStock.Legends.Add(Legend1)
        Me.chartStock.Location = New System.Drawing.Point(509, 93)
        Me.chartStock.Name = "chartStock"
        Me.chartStock.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright
        Me.chartStock.Size = New System.Drawing.Size(281, 160)
        Me.chartStock.TabIndex = 23
        Me.chartStock.Text = "Stock"
        '
        'Button1
        '
        Me.Button1.Enabled = False
        Me.Button1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button1.Location = New System.Drawing.Point(198, 629)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(97, 27)
        Me.Button1.TabIndex = 24
        Me.Button1.Text = "chart"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnAnalBetween
        '
        Me.btnAnalBetween.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAnalBetween.Enabled = False
        Me.btnAnalBetween.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnAnalBetween.Location = New System.Drawing.Point(198, 657)
        Me.btnAnalBetween.Name = "btnAnalBetween"
        Me.btnAnalBetween.Size = New System.Drawing.Size(87, 27)
        Me.btnAnalBetween.TabIndex = 25
        Me.btnAnalBetween.Text = "기간일일분석"
        Me.btnAnalBetween.UseVisualStyleBackColor = True
        '
        'txtAnalStartDate
        '
        Me.txtAnalStartDate.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtAnalStartDate.Location = New System.Drawing.Point(300, 131)
        Me.txtAnalStartDate.Name = "txtAnalStartDate"
        Me.txtAnalStartDate.Size = New System.Drawing.Size(67, 21)
        Me.txtAnalStartDate.TabIndex = 26
        Me.txtAnalStartDate.Text = "20160101"
        Me.txtAnalStartDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.Location = New System.Drawing.Point(234, 133)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 12)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "시작날짜"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.Location = New System.Drawing.Point(368, 134)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 12)
        Me.Label9.TabIndex = 29
        Me.Label9.Text = "종료날짜"
        '
        'txtAnalEndDate
        '
        Me.txtAnalEndDate.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtAnalEndDate.Location = New System.Drawing.Point(433, 130)
        Me.txtAnalEndDate.Name = "txtAnalEndDate"
        Me.txtAnalEndDate.Size = New System.Drawing.Size(69, 21)
        Me.txtAnalEndDate.TabIndex = 28
        Me.txtAnalEndDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button3.Location = New System.Drawing.Point(8, 127)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(119, 23)
        Me.Button3.TabIndex = 31
        Me.Button3.Text = "종목별회원사현황1"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'btnTodayCompany
        '
        Me.btnTodayCompany.Enabled = False
        Me.btnTodayCompany.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnTodayCompany.Location = New System.Drawing.Point(392, 630)
        Me.btnTodayCompany.Name = "btnTodayCompany"
        Me.btnTodayCompany.Size = New System.Drawing.Size(109, 26)
        Me.btnTodayCompany.TabIndex = 32
        Me.btnTodayCompany.Text = "당일주요거래원"
        Me.btnTodayCompany.UseVisualStyleBackColor = True
        '
        'chkRefresh
        '
        Me.chkRefresh.AutoSize = True
        Me.chkRefresh.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkRefresh.Location = New System.Drawing.Point(134, 131)
        Me.chkRefresh.Name = "chkRefresh"
        Me.chkRefresh.Size = New System.Drawing.Size(96, 16)
        Me.chkRefresh.TabIndex = 33
        Me.chkRefresh.Text = "Auto Refresh"
        Me.chkRefresh.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'btnTimer
        '
        Me.btnTimer.Enabled = False
        Me.btnTimer.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnTimer.Location = New System.Drawing.Point(299, 629)
        Me.btnTimer.Name = "btnTimer"
        Me.btnTimer.Size = New System.Drawing.Size(87, 27)
        Me.btnTimer.TabIndex = 34
        Me.btnTimer.Text = "타이머테스트"
        Me.btnTimer.UseVisualStyleBackColor = True
        '
        'cmbStock
        '
        Me.cmbStock.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cmbStock.Enabled = False
        Me.cmbStock.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cmbStock.FormattingEnabled = True
        Me.cmbStock.Location = New System.Drawing.Point(480, 662)
        Me.cmbStock.Name = "cmbStock"
        Me.cmbStock.Size = New System.Drawing.Size(145, 20)
        Me.cmbStock.Sorted = True
        Me.cmbStock.TabIndex = 36
        '
        'txtSuggest
        '
        Me.txtSuggest.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtSuggest.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtSuggest.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSuggest.Location = New System.Drawing.Point(73, 71)
        Me.txtSuggest.Name = "txtSuggest"
        Me.txtSuggest.Size = New System.Drawing.Size(91, 21)
        Me.txtSuggest.TabIndex = 37
        Me.txtSuggest.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStockCode
        '
        Me.txtStockCode.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtStockCode.Location = New System.Drawing.Point(73, 96)
        Me.txtStockCode.Name = "txtStockCode"
        Me.txtStockCode.Size = New System.Drawing.Size(91, 21)
        Me.txtStockCode.TabIndex = 38
        Me.txtStockCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label11.Location = New System.Drawing.Point(6, 98)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(53, 12)
        Me.Label11.TabIndex = 39
        Me.Label11.Text = "종목코드"
        '
        'Button4
        '
        Me.Button4.Enabled = False
        Me.Button4.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button4.Location = New System.Drawing.Point(727, 628)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(91, 28)
        Me.Button4.TabIndex = 40
        Me.Button4.Text = "900일봉"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(509, 70)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(764, 18)
        Me.ProgressBar1.TabIndex = 41
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button5.Location = New System.Drawing.Point(517, 39)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(91, 25)
        Me.Button5.TabIndex = 42
        Me.Button5.Text = "차트일일분석"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Enabled = False
        Me.Button6.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button6.Location = New System.Drawing.Point(631, 662)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(83, 31)
        Me.Button6.TabIndex = 43
        Me.Button6.Text = "웹실행"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'txtChartCount
        '
        Me.txtChartCount.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtChartCount.Location = New System.Drawing.Point(614, 42)
        Me.txtChartCount.Name = "txtChartCount"
        Me.txtChartCount.Size = New System.Drawing.Size(35, 21)
        Me.txtChartCount.TabIndex = 44
        Me.txtChartCount.Text = "5"
        Me.txtChartCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkBrowser
        '
        Me.chkBrowser.AutoSize = True
        Me.chkBrowser.Checked = True
        Me.chkBrowser.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBrowser.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkBrowser.Location = New System.Drawing.Point(517, 17)
        Me.chkBrowser.Name = "chkBrowser"
        Me.chkBrowser.Size = New System.Drawing.Size(96, 16)
        Me.chkBrowser.TabIndex = 45
        Me.chkBrowser.Text = "브라우저실행"
        Me.chkBrowser.UseVisualStyleBackColor = True
        '
        'btnOnlySellBuyAll
        '
        Me.btnOnlySellBuyAll.Enabled = False
        Me.btnOnlySellBuyAll.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnOnlySellBuyAll.Location = New System.Drawing.Point(381, 687)
        Me.btnOnlySellBuyAll.Name = "btnOnlySellBuyAll"
        Me.btnOnlySellBuyAll.Size = New System.Drawing.Size(87, 21)
        Me.btnOnlySellBuyAll.TabIndex = 46
        Me.btnOnlySellBuyAll.Text = "순매수량"
        Me.btnOnlySellBuyAll.UseVisualStyleBackColor = True
        '
        'btnStockCompanySort
        '
        Me.btnStockCompanySort.Enabled = False
        Me.btnStockCompanySort.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnStockCompanySort.Location = New System.Drawing.Point(291, 657)
        Me.btnStockCompanySort.Name = "btnStockCompanySort"
        Me.btnStockCompanySort.Size = New System.Drawing.Size(147, 27)
        Me.btnStockCompanySort.TabIndex = 47
        Me.btnStockCompanySort.Text = "증권사별매매상위요청"
        Me.btnStockCompanySort.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button7.Location = New System.Drawing.Point(168, 71)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(80, 25)
        Me.Button7.TabIndex = 48
        Me.Button7.Text = "주식상태"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'btnMinBong
        '
        Me.btnMinBong.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnMinBong.Location = New System.Drawing.Point(1111, 288)
        Me.btnMinBong.Name = "btnMinBong"
        Me.btnMinBong.Size = New System.Drawing.Size(88, 26)
        Me.btnMinBong.TabIndex = 49
        Me.btnMinBong.Text = "분봉조회"
        Me.btnMinBong.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.BackColor = System.Drawing.SystemColors.HotTrack
        Me.Button8.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button8.ForeColor = System.Drawing.SystemColors.Window
        Me.Button8.Location = New System.Drawing.Point(251, 71)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(66, 25)
        Me.Button8.TabIndex = 50
        Me.Button8.Text = "중단"
        Me.Button8.UseVisualStyleBackColor = False
        '
        'lstInfo
        '
        Me.lstInfo.FormattingEnabled = True
        Me.lstInfo.ItemHeight = 12
        Me.lstInfo.Location = New System.Drawing.Point(796, 93)
        Me.lstInfo.Name = "lstInfo"
        Me.lstInfo.Size = New System.Drawing.Size(479, 160)
        Me.lstInfo.TabIndex = 51
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button2.Location = New System.Drawing.Point(8, 15)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(89, 24)
        Me.Button2.TabIndex = 52
        Me.Button2.Text = "시작점찾기"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Enabled = False
        Me.Button9.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button9.Location = New System.Drawing.Point(507, 630)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(96, 25)
        Me.Button9.TabIndex = 53
        Me.Button9.Text = "몇일뒤날짜"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button10
        '
        Me.Button10.Enabled = False
        Me.Button10.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button10.Location = New System.Drawing.Point(97, 657)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(97, 27)
        Me.Button10.TabIndex = 54
        Me.Button10.Text = "실시간종목"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'Button11
        '
        Me.Button11.Enabled = False
        Me.Button11.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button11.Location = New System.Drawing.Point(97, 687)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(79, 22)
        Me.Button11.TabIndex = 55
        Me.Button11.Text = "API"
        Me.Button11.UseVisualStyleBackColor = True
        '
        'Button12
        '
        Me.Button12.Enabled = False
        Me.Button12.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button12.Location = New System.Drawing.Point(182, 686)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(88, 22)
        Me.Button12.TabIndex = 56
        Me.Button12.Text = "종목정보"
        Me.Button12.UseVisualStyleBackColor = True
        '
        'btnStockInitInfo
        '
        Me.btnStockInitInfo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnStockInitInfo.Location = New System.Drawing.Point(727, 658)
        Me.btnStockInitInfo.Name = "btnStockInitInfo"
        Me.btnStockInitInfo.Size = New System.Drawing.Size(88, 26)
        Me.btnStockInitInfo.TabIndex = 57
        Me.btnStockInitInfo.Text = "주식기본정보"
        Me.btnStockInitInfo.UseVisualStyleBackColor = True
        '
        'txtSPToday
        '
        Me.txtSPToday.Location = New System.Drawing.Point(160, 16)
        Me.txtSPToday.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSPToday.Name = "txtSPToday"
        Me.txtSPToday.Size = New System.Drawing.Size(70, 21)
        Me.txtSPToday.TabIndex = 58
        Me.txtSPToday.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button14
        '
        Me.Button14.AutoSize = True
        Me.Button14.Location = New System.Drawing.Point(823, 628)
        Me.Button14.Margin = New System.Windows.Forms.Padding(2)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(90, 28)
        Me.Button14.TabIndex = 59
        Me.Button14.Text = "체결정보"
        Me.Button14.UseVisualStyleBackColor = True
        '
        'txtSignValue
        '
        Me.txtSignValue.Location = New System.Drawing.Point(61, 49)
        Me.txtSignValue.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSignValue.Name = "txtSignValue"
        Me.txtSignValue.Size = New System.Drawing.Size(46, 21)
        Me.txtSignValue.TabIndex = 60
        Me.txtSignValue.Text = "3000"
        Me.txtSignValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(4, 54)
        Me.Label12.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(41, 12)
        Me.Label12.TabIndex = 61
        Me.Label12.Text = "체결량"
        '
        'Button15
        '
        Me.Button15.Location = New System.Drawing.Point(1181, 258)
        Me.Button15.Margin = New System.Windows.Forms.Padding(2)
        Me.Button15.Name = "Button15"
        Me.Button15.Size = New System.Drawing.Size(90, 21)
        Me.Button15.TabIndex = 62
        Me.Button15.Text = "지우기"
        Me.Button15.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtSPTradeCount)
        Me.GroupBox1.Controls.Add(Me.Label33)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.txtEndPrice)
        Me.GroupBox1.Controls.Add(Me.txtStartPrice)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.txtSPPer)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtJupoEndDate)
        Me.GroupBox1.Controls.Add(Me.txtJupo1)
        Me.GroupBox1.Controls.Add(Me.txtJupoStartDate)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.txtSPBong)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.chkBizSunValue)
        Me.GroupBox1.Controls.Add(Me.chkBizValue)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.txtSPToday)
        Me.GroupBox1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(511, 267)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(243, 233)
        Me.GroupBox1.TabIndex = 63
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "시작점찾기"
        '
        'txtSPTradeCount
        '
        Me.txtSPTradeCount.Location = New System.Drawing.Point(101, 203)
        Me.txtSPTradeCount.Name = "txtSPTradeCount"
        Me.txtSPTradeCount.Size = New System.Drawing.Size(63, 21)
        Me.txtSPTradeCount.TabIndex = 77
        Me.txtSPTradeCount.Text = "9000000"
        Me.txtSPTradeCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(13, 208)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(81, 12)
        Me.Label33.TabIndex = 76
        Me.Label33.Text = "기준날 거래량"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(129, 180)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(14, 12)
        Me.Label20.TabIndex = 75
        Me.Label20.Text = "~"
        '
        'txtEndPrice
        '
        Me.txtEndPrice.Location = New System.Drawing.Point(151, 175)
        Me.txtEndPrice.Name = "txtEndPrice"
        Me.txtEndPrice.Size = New System.Drawing.Size(73, 21)
        Me.txtEndPrice.TabIndex = 66
        Me.txtEndPrice.Text = "50000"
        Me.txtEndPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStartPrice
        '
        Me.txtStartPrice.Location = New System.Drawing.Point(47, 175)
        Me.txtStartPrice.Name = "txtStartPrice"
        Me.txtStartPrice.Size = New System.Drawing.Size(73, 21)
        Me.txtStartPrice.TabIndex = 74
        Me.txtStartPrice.Text = "1000"
        Me.txtStartPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(10, 180)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(29, 12)
        Me.Label19.TabIndex = 73
        Me.Label19.Text = "주가"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(209, 98)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(15, 12)
        Me.Label18.TabIndex = 72
        Me.Label18.Text = "%"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(99, 97)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(65, 12)
        Me.Label17.TabIndex = 71
        Me.Label17.Text = "시작점근접"
        '
        'txtSPPer
        '
        Me.txtSPPer.Location = New System.Drawing.Point(171, 93)
        Me.txtSPPer.Name = "txtSPPer"
        Me.txtSPPer.Size = New System.Drawing.Size(35, 21)
        Me.txtSPPer.TabIndex = 70
        Me.txtSPPer.Text = "1.29"
        Me.txtSPPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(7, 149)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(105, 12)
        Me.Label16.TabIndex = 69
        Me.Label16.Text = "주포분석 종료날짜"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(7, 126)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(105, 12)
        Me.Label10.TabIndex = 68
        Me.Label10.Text = "주포분석 시작날짜"
        '
        'txtJupoEndDate
        '
        Me.txtJupoEndDate.Location = New System.Drawing.Point(120, 145)
        Me.txtJupoEndDate.Name = "txtJupoEndDate"
        Me.txtJupoEndDate.Size = New System.Drawing.Size(74, 21)
        Me.txtJupoEndDate.TabIndex = 67
        Me.txtJupoEndDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtJupo1
        '
        Me.txtJupo1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtJupo1.Location = New System.Drawing.Point(104, 66)
        Me.txtJupo1.Margin = New System.Windows.Forms.Padding(2)
        Me.txtJupo1.Name = "txtJupo1"
        Me.txtJupo1.Size = New System.Drawing.Size(69, 21)
        Me.txtJupo1.TabIndex = 63
        Me.txtJupo1.Text = "1000000"
        Me.txtJupo1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtJupoStartDate
        '
        Me.txtJupoStartDate.Location = New System.Drawing.Point(120, 121)
        Me.txtJupoStartDate.Name = "txtJupoStartDate"
        Me.txtJupoStartDate.Size = New System.Drawing.Size(73, 21)
        Me.txtJupoStartDate.TabIndex = 66
        Me.txtJupoStartDate.Text = "20160101"
        Me.txtJupoStartDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(67, 50)
        Me.Label15.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(17, 12)
        Me.Label15.TabIndex = 65
        Me.Label15.Text = "봉"
        '
        'txtSPBong
        '
        Me.txtSPBong.Location = New System.Drawing.Point(11, 47)
        Me.txtSPBong.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSPBong.Name = "txtSPBong"
        Me.txtSPBong.Size = New System.Drawing.Size(53, 21)
        Me.txtSPBong.TabIndex = 64
        Me.txtSPBong.Text = "250"
        Me.txtSPBong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(103, 49)
        Me.Label14.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(99, 12)
        Me.Label14.TabIndex = 62
        Me.Label14.Text = "주포1순매수 이상"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(103, 21)
        Me.Label13.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 12)
        Me.Label13.TabIndex = 61
        Me.Label13.Text = "기준날짜"
        '
        'chkBizSunValue
        '
        Me.chkBizSunValue.AutoSize = True
        Me.chkBizSunValue.Checked = True
        Me.chkBizSunValue.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBizSunValue.Location = New System.Drawing.Point(8, 91)
        Me.chkBizSunValue.Margin = New System.Windows.Forms.Padding(2)
        Me.chkBizSunValue.Name = "chkBizSunValue"
        Me.chkBizSunValue.Size = New System.Drawing.Size(78, 16)
        Me.chkBizSunValue.TabIndex = 60
        Me.chkBizSunValue.Text = "당기순익+"
        Me.chkBizSunValue.UseVisualStyleBackColor = True
        '
        'chkBizValue
        '
        Me.chkBizValue.AutoSize = True
        Me.chkBizValue.Checked = True
        Me.chkBizValue.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBizValue.Location = New System.Drawing.Point(8, 71)
        Me.chkBizValue.Margin = New System.Windows.Forms.Padding(2)
        Me.chkBizValue.Name = "chkBizValue"
        Me.chkBizValue.Size = New System.Drawing.Size(78, 16)
        Me.chkBizValue.TabIndex = 59
        Me.chkBizValue.Text = "영업이익+"
        Me.chkBizValue.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtBunBongJupoSunCount)
        Me.GroupBox2.Controls.Add(Me.Label32)
        Me.GroupBox2.Controls.Add(Me.txtBunBongAnalEndDate)
        Me.GroupBox2.Controls.Add(Me.txtBunBongAnalStartDate)
        Me.GroupBox2.Controls.Add(Me.Label31)
        Me.GroupBox2.Controls.Add(Me.Label30)
        Me.GroupBox2.Controls.Add(Me.Label29)
        Me.GroupBox2.Controls.Add(Me.txtBunBongEndPrice)
        Me.GroupBox2.Controls.Add(Me.txtBunBongStartPrice)
        Me.GroupBox2.Controls.Add(Me.Label28)
        Me.GroupBox2.Controls.Add(Me.Label27)
        Me.GroupBox2.Controls.Add(Me.txtSignTradeValue)
        Me.GroupBox2.Controls.Add(Me.Label26)
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.txtBunBongStartPer)
        Me.GroupBox2.Controls.Add(Me.Label23)
        Me.GroupBox2.Controls.Add(Me.txtAnalBunBong)
        Me.GroupBox2.Controls.Add(Me.Button18)
        Me.GroupBox2.Controls.Add(Me.Label22)
        Me.GroupBox2.Controls.Add(Me.Label21)
        Me.GroupBox2.Controls.Add(Me.ddd)
        Me.GroupBox2.Controls.Add(Me.Button17)
        Me.GroupBox2.Controls.Add(Me.txtTradePrice)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.txtSignValue)
        Me.GroupBox2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(758, 267)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Size = New System.Drawing.Size(192, 293)
        Me.GroupBox2.TabIndex = 64
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "신호등"
        '
        'txtBunBongJupoSunCount
        '
        Me.txtBunBongJupoSunCount.Location = New System.Drawing.Point(115, 258)
        Me.txtBunBongJupoSunCount.Name = "txtBunBongJupoSunCount"
        Me.txtBunBongJupoSunCount.Size = New System.Drawing.Size(55, 21)
        Me.txtBunBongJupoSunCount.TabIndex = 88
        Me.txtBunBongJupoSunCount.Text = "1000000"
        Me.txtBunBongJupoSunCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(5, 263)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(103, 12)
        Me.Label32.TabIndex = 87
        Me.Label32.Text = "주포1 순매수 이상"
        '
        'txtBunBongAnalEndDate
        '
        Me.txtBunBongAnalEndDate.Location = New System.Drawing.Point(118, 232)
        Me.txtBunBongAnalEndDate.Name = "txtBunBongAnalEndDate"
        Me.txtBunBongAnalEndDate.Size = New System.Drawing.Size(65, 21)
        Me.txtBunBongAnalEndDate.TabIndex = 86
        Me.txtBunBongAnalEndDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtBunBongAnalStartDate
        '
        Me.txtBunBongAnalStartDate.Location = New System.Drawing.Point(118, 205)
        Me.txtBunBongAnalStartDate.Name = "txtBunBongAnalStartDate"
        Me.txtBunBongAnalStartDate.Size = New System.Drawing.Size(65, 21)
        Me.txtBunBongAnalStartDate.TabIndex = 85
        Me.txtBunBongAnalStartDate.Text = "20160101"
        Me.txtBunBongAnalStartDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(5, 237)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(105, 12)
        Me.Label31.TabIndex = 84
        Me.Label31.Text = "주포분석 종료날짜"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(5, 210)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(105, 12)
        Me.Label30.TabIndex = 83
        Me.Label30.Text = "주포분석 시작날짜"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(89, 182)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(14, 12)
        Me.Label29.TabIndex = 76
        Me.Label29.Text = "~"
        '
        'txtBunBongEndPrice
        '
        Me.txtBunBongEndPrice.Location = New System.Drawing.Point(106, 177)
        Me.txtBunBongEndPrice.Name = "txtBunBongEndPrice"
        Me.txtBunBongEndPrice.Size = New System.Drawing.Size(51, 21)
        Me.txtBunBongEndPrice.TabIndex = 82
        Me.txtBunBongEndPrice.Text = "50000"
        Me.txtBunBongEndPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtBunBongStartPrice
        '
        Me.txtBunBongStartPrice.Location = New System.Drawing.Point(35, 178)
        Me.txtBunBongStartPrice.Name = "txtBunBongStartPrice"
        Me.txtBunBongStartPrice.Size = New System.Drawing.Size(51, 21)
        Me.txtBunBongStartPrice.TabIndex = 81
        Me.txtBunBongStartPrice.Text = "1000"
        Me.txtBunBongStartPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(4, 183)
        Me.Label28.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(29, 12)
        Me.Label28.TabIndex = 80
        Me.Label28.Text = "주가"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(128, 157)
        Me.Label27.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(29, 12)
        Me.Label27.TabIndex = 79
        Me.Label27.Text = "이상"
        '
        'txtSignTradeValue
        '
        Me.txtSignTradeValue.Location = New System.Drawing.Point(72, 152)
        Me.txtSignTradeValue.Name = "txtSignTradeValue"
        Me.txtSignTradeValue.Size = New System.Drawing.Size(52, 21)
        Me.txtSignTradeValue.TabIndex = 78
        Me.txtSignTradeValue.Text = "200000"
        Me.txtSignTradeValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(4, 157)
        Me.Label26.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(65, 12)
        Me.Label26.TabIndex = 77
        Me.Label26.Text = "오늘거래량"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(134, 131)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(15, 12)
        Me.Label25.TabIndex = 76
        Me.Label25.Text = "%"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(4, 131)
        Me.Label24.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(89, 12)
        Me.Label24.TabIndex = 74
        Me.Label24.Text = "분봉시작점근접"
        '
        'txtBunBongStartPer
        '
        Me.txtBunBongStartPer.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBunBongStartPer.Location = New System.Drawing.Point(96, 126)
        Me.txtBunBongStartPer.Name = "txtBunBongStartPer"
        Me.txtBunBongStartPer.Size = New System.Drawing.Size(34, 22)
        Me.txtBunBongStartPer.TabIndex = 73
        Me.txtBunBongStartPer.Text = "2.1"
        Me.txtBunBongStartPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(4, 105)
        Me.Label23.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(81, 12)
        Me.Label23.TabIndex = 72
        Me.Label23.Text = "분석할 분봉수"
        '
        'txtAnalBunBong
        '
        Me.txtAnalBunBong.Location = New System.Drawing.Point(93, 100)
        Me.txtAnalBunBong.Name = "txtAnalBunBong"
        Me.txtAnalBunBong.Size = New System.Drawing.Size(46, 21)
        Me.txtAnalBunBong.TabIndex = 71
        Me.txtAnalBunBong.Text = "500"
        Me.txtAnalBunBong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button18
        '
        Me.Button18.Location = New System.Drawing.Point(99, 19)
        Me.Button18.Name = "Button18"
        Me.Button18.Size = New System.Drawing.Size(53, 25)
        Me.Button18.TabIndex = 70
        Me.Button18.Text = "멈춤"
        Me.Button18.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(111, 54)
        Me.Label22.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(17, 12)
        Me.Label22.TabIndex = 69
        Me.Label22.Text = "건"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(110, 79)
        Me.Label21.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(29, 12)
        Me.Label21.TabIndex = 68
        Me.Label21.Text = "만원"
        '
        'ddd
        '
        Me.ddd.AutoSize = True
        Me.ddd.Location = New System.Drawing.Point(4, 80)
        Me.ddd.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ddd.Name = "ddd"
        Me.ddd.Size = New System.Drawing.Size(53, 12)
        Me.ddd.TabIndex = 67
        Me.ddd.Text = "거래대금"
        '
        'Button17
        '
        Me.Button17.Location = New System.Drawing.Point(5, 18)
        Me.Button17.Name = "Button17"
        Me.Button17.Size = New System.Drawing.Size(90, 27)
        Me.Button17.TabIndex = 66
        Me.Button17.Text = "신호등시작"
        Me.Button17.UseVisualStyleBackColor = True
        '
        'txtTradePrice
        '
        Me.txtTradePrice.Location = New System.Drawing.Point(61, 74)
        Me.txtTradePrice.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTradePrice.Name = "txtTradePrice"
        Me.txtTradePrice.Size = New System.Drawing.Size(46, 21)
        Me.txtTradePrice.TabIndex = 62
        Me.txtTradePrice.Text = "5000"
        Me.txtTradePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button16
        '
        Me.Button16.Enabled = False
        Me.Button16.Location = New System.Drawing.Point(631, 699)
        Me.Button16.Name = "Button16"
        Me.Button16.Size = New System.Drawing.Size(79, 26)
        Me.Button16.TabIndex = 65
        Me.Button16.Text = "PER"
        Me.Button16.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1281, 578)
        Me.Controls.Add(Me.Button14)
        Me.Controls.Add(Me.Button16)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button15)
        Me.Controls.Add(Me.btnStockInitInfo)
        Me.Controls.Add(Me.Button12)
        Me.Controls.Add(Me.Button11)
        Me.Controls.Add(Me.Button10)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.lstInfo)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.btnMinBong)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.btnStockCompanySort)
        Me.Controls.Add(Me.btnOnlySellBuyAll)
        Me.Controls.Add(Me.chkBrowser)
        Me.Controls.Add(Me.txtChartCount)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtStockCode)
        Me.Controls.Add(Me.txtSuggest)
        Me.Controls.Add(Me.cmbStock)
        Me.Controls.Add(Me.btnTimer)
        Me.Controls.Add(Me.chkRefresh)
        Me.Controls.Add(Me.btnTodayCompany)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtAnalEndDate)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtAnalStartDate)
        Me.Controls.Add(Me.btnAnalBetween)
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
        Me.Controls.Add(Me.lstMsg)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.KHOpenAPI)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "대칭이론"
        CType(Me.KHOpenAPI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
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
    Friend WithEvents btnAnalBetween As System.Windows.Forms.Button
    Friend WithEvents txtAnalStartDate As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtAnalEndDate As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents btnTodayCompany As System.Windows.Forms.Button
    Friend WithEvents chkRefresh As System.Windows.Forms.CheckBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btnTimer As System.Windows.Forms.Button
    Friend WithEvents cmbStock As System.Windows.Forms.ComboBox
    Friend WithEvents txtSuggest As System.Windows.Forms.TextBox
    Friend WithEvents txtStockCode As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents txtChartCount As System.Windows.Forms.TextBox
    Friend WithEvents chkBrowser As System.Windows.Forms.CheckBox
    Friend WithEvents btnOnlySellBuyAll As System.Windows.Forms.Button
    Friend WithEvents btnStockCompanySort As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents btnMinBong As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents lstInfo As System.Windows.Forms.ListBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents btnStockInitInfo As System.Windows.Forms.Button
    Friend WithEvents txtSPToday As System.Windows.Forms.TextBox
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents txtSignValue As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Button15 As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtJupo1 As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents chkBizSunValue As System.Windows.Forms.CheckBox
    Friend WithEvents chkBizValue As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtSPBong As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtSPPer As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtJupoEndDate As System.Windows.Forms.TextBox
    Friend WithEvents txtJupoStartDate As System.Windows.Forms.TextBox
    Friend WithEvents Button16 As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtEndPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtStartPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtTradePrice As System.Windows.Forms.TextBox
    Friend WithEvents Button17 As System.Windows.Forms.Button
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents ddd As System.Windows.Forms.Label
    Friend WithEvents Button18 As System.Windows.Forms.Button
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtAnalBunBong As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtBunBongStartPer As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtBunBongEndPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtBunBongStartPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtSignTradeValue As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtBunBongJupoSunCount As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtBunBongAnalEndDate As System.Windows.Forms.TextBox
    Friend WithEvents txtBunBongAnalStartDate As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtSPTradeCount As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
End Class
