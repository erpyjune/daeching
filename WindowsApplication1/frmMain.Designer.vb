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
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
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
        Me.Button13 = New System.Windows.Forms.Button()
        Me.txtSPToday = New System.Windows.Forms.TextBox()
        Me.Button14 = New System.Windows.Forms.Button()
        Me.txtSignValue = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Button15 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
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
        Me.btnLogin.Location = New System.Drawing.Point(11, 15)
        Me.btnLogin.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(143, 38)
        Me.btnLogin.TabIndex = 0
        Me.btnLogin.Text = "로그인"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'KHOpenAPI
        '
        Me.KHOpenAPI.Enabled = True
        Me.KHOpenAPI.Location = New System.Drawing.Point(1230, 15)
        Me.KHOpenAPI.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.KHOpenAPI.Name = "KHOpenAPI"
        Me.KHOpenAPI.OcxState = CType(resources.GetObject("KHOpenAPI.OcxState"), System.Windows.Forms.AxHost.State)
        Me.KHOpenAPI.Size = New System.Drawing.Size(112, 32)
        Me.KHOpenAPI.TabIndex = 0
        '
        'lstMsg
        '
        Me.lstMsg.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lstMsg.FormattingEnabled = True
        Me.lstMsg.ItemHeight = 18
        Me.lstMsg.Location = New System.Drawing.Point(169, 18)
        Me.lstMsg.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lstMsg.Name = "lstMsg"
        Me.lstMsg.Size = New System.Drawing.Size(283, 76)
        Me.lstMsg.TabIndex = 1
        '
        'btnSearch
        '
        Me.btnSearch.Enabled = False
        Me.btnSearch.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(139, 940)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(136, 40)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "조회"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'lstView1
        '
        Me.lstView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.날짜, Me.증권사, Me.누적순매수, Me.매수, Me.매도})
        Me.lstView1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lstView1.Location = New System.Drawing.Point(11, 234)
        Me.lstView1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lstView1.Name = "lstView1"
        Me.lstView1.Size = New System.Drawing.Size(705, 182)
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
        Me.Label1.Location = New System.Drawing.Point(9, 110)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 18)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "종목명"
        '
        'btnCmd1
        '
        Me.btnCmd1.Enabled = False
        Me.btnCmd1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnCmd1.Location = New System.Drawing.Point(899, 942)
        Me.btnCmd1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnCmd1.Name = "btnCmd1"
        Me.btnCmd1.Size = New System.Drawing.Size(123, 44)
        Me.btnCmd1.TabIndex = 6
        Me.btnCmd1.Text = "보기1"
        Me.btnCmd1.UseVisualStyleBackColor = True
        '
        'txtStartDate1
        '
        Me.txtStartDate1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtStartDate1.Location = New System.Drawing.Point(1017, 64)
        Me.txtStartDate1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtStartDate1.Name = "txtStartDate1"
        Me.txtStartDate1.Size = New System.Drawing.Size(115, 28)
        Me.txtStartDate1.TabIndex = 7
        Me.txtStartDate1.Text = "20160620"
        Me.txtStartDate1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.Location = New System.Drawing.Point(936, 72)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 18)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "시작날짜"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.Location = New System.Drawing.Point(1147, 72)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 18)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "종료날짜"
        '
        'txtEndDate1
        '
        Me.txtEndDate1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtEndDate1.Location = New System.Drawing.Point(1230, 64)
        Me.txtEndDate1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtEndDate1.Name = "txtEndDate1"
        Me.txtEndDate1.Size = New System.Drawing.Size(125, 28)
        Me.txtEndDate1.TabIndex = 9
        Me.txtEndDate1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.Location = New System.Drawing.Point(496, 424)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 18)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "종료날짜"
        '
        'txtEndDate2
        '
        Me.txtEndDate2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtEndDate2.Location = New System.Drawing.Point(591, 420)
        Me.txtEndDate2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtEndDate2.Name = "txtEndDate2"
        Me.txtEndDate2.Size = New System.Drawing.Size(125, 28)
        Me.txtEndDate2.TabIndex = 15
        Me.txtEndDate2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.Location = New System.Drawing.Point(291, 424)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 18)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "시작날짜"
        '
        'txtStartDate2
        '
        Me.txtStartDate2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtStartDate2.Location = New System.Drawing.Point(389, 422)
        Me.txtStartDate2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtStartDate2.Name = "txtStartDate2"
        Me.txtStartDate2.Size = New System.Drawing.Size(105, 28)
        Me.txtStartDate2.TabIndex = 13
        Me.txtStartDate2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnCmd2
        '
        Me.btnCmd2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnCmd2.Location = New System.Drawing.Point(11, 422)
        Me.btnCmd2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnCmd2.Name = "btnCmd2"
        Me.btnCmd2.Size = New System.Drawing.Size(194, 30)
        Me.btnCmd2.TabIndex = 12
        Me.btnCmd2.Text = "종목별회원사현황2"
        Me.btnCmd2.UseVisualStyleBackColor = True
        '
        'lstView2
        '
        Me.lstView2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.lstView2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lstView2.Location = New System.Drawing.Point(11, 458)
        Me.lstView2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lstView2.Name = "lstView2"
        Me.lstView2.Size = New System.Drawing.Size(705, 180)
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
        Me.Label6.Location = New System.Drawing.Point(499, 646)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 18)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "종료날짜"
        '
        'txtEndDate3
        '
        Me.txtEndDate3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtEndDate3.Location = New System.Drawing.Point(591, 642)
        Me.txtEndDate3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtEndDate3.Name = "txtEndDate3"
        Me.txtEndDate3.Size = New System.Drawing.Size(125, 28)
        Me.txtEndDate3.TabIndex = 21
        Me.txtEndDate3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.Location = New System.Drawing.Point(294, 646)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 18)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "시작날짜"
        '
        'txtStartDate3
        '
        Me.txtStartDate3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtStartDate3.Location = New System.Drawing.Point(389, 642)
        Me.txtStartDate3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtStartDate3.Name = "txtStartDate3"
        Me.txtStartDate3.Size = New System.Drawing.Size(105, 28)
        Me.txtStartDate3.TabIndex = 19
        Me.txtStartDate3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnCmd3
        '
        Me.btnCmd3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnCmd3.Location = New System.Drawing.Point(10, 642)
        Me.btnCmd3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnCmd3.Name = "btnCmd3"
        Me.btnCmd3.Size = New System.Drawing.Size(203, 30)
        Me.btnCmd3.TabIndex = 18
        Me.btnCmd3.Text = "종목별 회원사현황3"
        Me.btnCmd3.UseVisualStyleBackColor = True
        '
        'lstView3
        '
        Me.lstView3.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10})
        Me.lstView3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lstView3.Location = New System.Drawing.Point(10, 680)
        Me.lstView3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lstView3.Name = "lstView3"
        Me.lstView3.Size = New System.Drawing.Size(705, 180)
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
        ChartArea2.Name = "ChartArea1"
        Me.chartStock.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        Me.chartStock.Legends.Add(Legend2)
        Me.chartStock.Location = New System.Drawing.Point(727, 140)
        Me.chartStock.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chartStock.Name = "chartStock"
        Me.chartStock.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright
        Me.chartStock.Size = New System.Drawing.Size(401, 240)
        Me.chartStock.TabIndex = 23
        Me.chartStock.Text = "Stock"
        '
        'Button1
        '
        Me.Button1.Enabled = False
        Me.Button1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button1.Location = New System.Drawing.Point(283, 944)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(139, 40)
        Me.Button1.TabIndex = 24
        Me.Button1.Text = "chart"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnAnalBetween
        '
        Me.btnAnalBetween.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAnalBetween.Enabled = False
        Me.btnAnalBetween.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnAnalBetween.Location = New System.Drawing.Point(283, 986)
        Me.btnAnalBetween.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnAnalBetween.Name = "btnAnalBetween"
        Me.btnAnalBetween.Size = New System.Drawing.Size(124, 40)
        Me.btnAnalBetween.TabIndex = 25
        Me.btnAnalBetween.Text = "기간일일분석"
        Me.btnAnalBetween.UseVisualStyleBackColor = True
        '
        'txtAnalStartDate
        '
        Me.txtAnalStartDate.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtAnalStartDate.Location = New System.Drawing.Point(429, 196)
        Me.txtAnalStartDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtAnalStartDate.Name = "txtAnalStartDate"
        Me.txtAnalStartDate.Size = New System.Drawing.Size(94, 28)
        Me.txtAnalStartDate.TabIndex = 26
        Me.txtAnalStartDate.Text = "20160101"
        Me.txtAnalStartDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.Location = New System.Drawing.Point(334, 200)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 18)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "시작날짜"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.Location = New System.Drawing.Point(526, 201)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 18)
        Me.Label9.TabIndex = 29
        Me.Label9.Text = "종료날짜"
        '
        'txtAnalEndDate
        '
        Me.txtAnalEndDate.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtAnalEndDate.Location = New System.Drawing.Point(619, 195)
        Me.txtAnalEndDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtAnalEndDate.Name = "txtAnalEndDate"
        Me.txtAnalEndDate.Size = New System.Drawing.Size(97, 28)
        Me.txtAnalEndDate.TabIndex = 28
        Me.txtAnalEndDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button3.Location = New System.Drawing.Point(11, 190)
        Me.Button3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(170, 34)
        Me.Button3.TabIndex = 31
        Me.Button3.Text = "종목별회원사현황1"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'btnTodayCompany
        '
        Me.btnTodayCompany.Enabled = False
        Me.btnTodayCompany.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnTodayCompany.Location = New System.Drawing.Point(560, 945)
        Me.btnTodayCompany.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnTodayCompany.Name = "btnTodayCompany"
        Me.btnTodayCompany.Size = New System.Drawing.Size(156, 39)
        Me.btnTodayCompany.TabIndex = 32
        Me.btnTodayCompany.Text = "당일주요거래원"
        Me.btnTodayCompany.UseVisualStyleBackColor = True
        '
        'chkRefresh
        '
        Me.chkRefresh.AutoSize = True
        Me.chkRefresh.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkRefresh.Location = New System.Drawing.Point(191, 196)
        Me.chkRefresh.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkRefresh.Name = "chkRefresh"
        Me.chkRefresh.Size = New System.Drawing.Size(140, 22)
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
        Me.btnTimer.Location = New System.Drawing.Point(427, 944)
        Me.btnTimer.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnTimer.Name = "btnTimer"
        Me.btnTimer.Size = New System.Drawing.Size(124, 40)
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
        Me.cmbStock.Location = New System.Drawing.Point(686, 993)
        Me.cmbStock.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbStock.Name = "cmbStock"
        Me.cmbStock.Size = New System.Drawing.Size(205, 26)
        Me.cmbStock.Sorted = True
        Me.cmbStock.TabIndex = 36
        '
        'txtSuggest
        '
        Me.txtSuggest.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtSuggest.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtSuggest.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSuggest.Location = New System.Drawing.Point(104, 106)
        Me.txtSuggest.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtSuggest.Name = "txtSuggest"
        Me.txtSuggest.Size = New System.Drawing.Size(128, 28)
        Me.txtSuggest.TabIndex = 37
        Me.txtSuggest.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStockCode
        '
        Me.txtStockCode.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtStockCode.Location = New System.Drawing.Point(104, 144)
        Me.txtStockCode.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtStockCode.Name = "txtStockCode"
        Me.txtStockCode.Size = New System.Drawing.Size(128, 28)
        Me.txtStockCode.TabIndex = 38
        Me.txtStockCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label11.Location = New System.Drawing.Point(9, 147)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(80, 18)
        Me.Label11.TabIndex = 39
        Me.Label11.Text = "종목코드"
        '
        'Button4
        '
        Me.Button4.Enabled = False
        Me.Button4.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button4.Location = New System.Drawing.Point(1039, 942)
        Me.Button4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(130, 42)
        Me.Button4.TabIndex = 40
        Me.Button4.Text = "900일봉"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(727, 105)
        Me.ProgressBar1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(1091, 27)
        Me.ProgressBar1.TabIndex = 41
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button5.Location = New System.Drawing.Point(739, 58)
        Me.Button5.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(130, 38)
        Me.Button5.TabIndex = 42
        Me.Button5.Text = "차트일일분석"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Enabled = False
        Me.Button6.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button6.Location = New System.Drawing.Point(901, 993)
        Me.Button6.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(119, 46)
        Me.Button6.TabIndex = 43
        Me.Button6.Text = "웹실행"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'txtChartCount
        '
        Me.txtChartCount.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtChartCount.Location = New System.Drawing.Point(877, 63)
        Me.txtChartCount.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtChartCount.Name = "txtChartCount"
        Me.txtChartCount.Size = New System.Drawing.Size(48, 28)
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
        Me.chkBrowser.Location = New System.Drawing.Point(739, 26)
        Me.chkBrowser.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkBrowser.Name = "chkBrowser"
        Me.chkBrowser.Size = New System.Drawing.Size(142, 22)
        Me.chkBrowser.TabIndex = 45
        Me.chkBrowser.Text = "브라우저실행"
        Me.chkBrowser.UseVisualStyleBackColor = True
        '
        'btnOnlySellBuyAll
        '
        Me.btnOnlySellBuyAll.Enabled = False
        Me.btnOnlySellBuyAll.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnOnlySellBuyAll.Location = New System.Drawing.Point(544, 1030)
        Me.btnOnlySellBuyAll.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnOnlySellBuyAll.Name = "btnOnlySellBuyAll"
        Me.btnOnlySellBuyAll.Size = New System.Drawing.Size(124, 32)
        Me.btnOnlySellBuyAll.TabIndex = 46
        Me.btnOnlySellBuyAll.Text = "순매수량"
        Me.btnOnlySellBuyAll.UseVisualStyleBackColor = True
        '
        'btnStockCompanySort
        '
        Me.btnStockCompanySort.Enabled = False
        Me.btnStockCompanySort.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnStockCompanySort.Location = New System.Drawing.Point(416, 986)
        Me.btnStockCompanySort.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnStockCompanySort.Name = "btnStockCompanySort"
        Me.btnStockCompanySort.Size = New System.Drawing.Size(210, 40)
        Me.btnStockCompanySort.TabIndex = 47
        Me.btnStockCompanySort.Text = "증권사별매매상위요청"
        Me.btnStockCompanySort.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button7.Location = New System.Drawing.Point(240, 106)
        Me.Button7.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(114, 38)
        Me.Button7.TabIndex = 48
        Me.Button7.Text = "주식상태"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'btnMinBong
        '
        Me.btnMinBong.Enabled = False
        Me.btnMinBong.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnMinBong.Location = New System.Drawing.Point(1041, 999)
        Me.btnMinBong.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnMinBong.Name = "btnMinBong"
        Me.btnMinBong.Size = New System.Drawing.Size(126, 39)
        Me.btnMinBong.TabIndex = 49
        Me.btnMinBong.Text = "분봉조회"
        Me.btnMinBong.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.BackColor = System.Drawing.SystemColors.HotTrack
        Me.Button8.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button8.ForeColor = System.Drawing.SystemColors.Window
        Me.Button8.Location = New System.Drawing.Point(359, 106)
        Me.Button8.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(94, 38)
        Me.Button8.TabIndex = 50
        Me.Button8.Text = "중단"
        Me.Button8.UseVisualStyleBackColor = False
        '
        'lstInfo
        '
        Me.lstInfo.FormattingEnabled = True
        Me.lstInfo.ItemHeight = 18
        Me.lstInfo.Location = New System.Drawing.Point(1137, 140)
        Me.lstInfo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lstInfo.Name = "lstInfo"
        Me.lstInfo.Size = New System.Drawing.Size(683, 238)
        Me.lstInfo.TabIndex = 51
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button2.Location = New System.Drawing.Point(11, 22)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(127, 36)
        Me.Button2.TabIndex = 52
        Me.Button2.Text = "시작점찾기"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Enabled = False
        Me.Button9.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button9.Location = New System.Drawing.Point(724, 945)
        Me.Button9.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(137, 38)
        Me.Button9.TabIndex = 53
        Me.Button9.Text = "몇일뒤날짜"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button10
        '
        Me.Button10.Enabled = False
        Me.Button10.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button10.Location = New System.Drawing.Point(139, 986)
        Me.Button10.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(139, 40)
        Me.Button10.TabIndex = 54
        Me.Button10.Text = "실시간종목"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'Button11
        '
        Me.Button11.Enabled = False
        Me.Button11.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button11.Location = New System.Drawing.Point(139, 1030)
        Me.Button11.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(113, 33)
        Me.Button11.TabIndex = 55
        Me.Button11.Text = "API"
        Me.Button11.UseVisualStyleBackColor = True
        '
        'Button12
        '
        Me.Button12.Enabled = False
        Me.Button12.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button12.Location = New System.Drawing.Point(260, 1029)
        Me.Button12.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(126, 33)
        Me.Button12.TabIndex = 56
        Me.Button12.Text = "종목정보"
        Me.Button12.UseVisualStyleBackColor = True
        '
        'Button13
        '
        Me.Button13.Enabled = False
        Me.Button13.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button13.Location = New System.Drawing.Point(393, 1030)
        Me.Button13.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(143, 32)
        Me.Button13.TabIndex = 57
        Me.Button13.Text = "주식기본정보"
        Me.Button13.UseVisualStyleBackColor = True
        '
        'txtSPToday
        '
        Me.txtSPToday.Location = New System.Drawing.Point(229, 24)
        Me.txtSPToday.Name = "txtSPToday"
        Me.txtSPToday.Size = New System.Drawing.Size(98, 28)
        Me.txtSPToday.TabIndex = 58
        Me.txtSPToday.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button14
        '
        Me.Button14.AutoSize = True
        Me.Button14.Location = New System.Drawing.Point(9, 32)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(129, 42)
        Me.Button14.TabIndex = 59
        Me.Button14.Text = "체결정보"
        Me.Button14.UseVisualStyleBackColor = True
        '
        'txtSignValue
        '
        Me.txtSignValue.Location = New System.Drawing.Point(161, 41)
        Me.txtSignValue.Name = "txtSignValue"
        Me.txtSignValue.Size = New System.Drawing.Size(67, 28)
        Me.txtSignValue.TabIndex = 60
        Me.txtSignValue.Text = "10000"
        Me.txtSignValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(159, 21)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(62, 18)
        Me.Label12.TabIndex = 61
        Me.Label12.Text = "체결량"
        '
        'Button15
        '
        Me.Button15.Location = New System.Drawing.Point(1687, 387)
        Me.Button15.Name = "Button15"
        Me.Button15.Size = New System.Drawing.Size(129, 32)
        Me.Button15.TabIndex = 62
        Me.Button15.Text = "지우기"
        Me.Button15.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
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
        Me.GroupBox1.Location = New System.Drawing.Point(730, 400)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(347, 312)
        Me.GroupBox1.TabIndex = 63
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "시작점찾기"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(184, 270)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(22, 18)
        Me.Label20.TabIndex = 75
        Me.Label20.Text = "~"
        '
        'txtEndPrice
        '
        Me.txtEndPrice.Location = New System.Drawing.Point(216, 262)
        Me.txtEndPrice.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtEndPrice.Name = "txtEndPrice"
        Me.txtEndPrice.Size = New System.Drawing.Size(103, 28)
        Me.txtEndPrice.TabIndex = 66
        Me.txtEndPrice.Text = "50000"
        Me.txtEndPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStartPrice
        '
        Me.txtStartPrice.Location = New System.Drawing.Point(67, 262)
        Me.txtStartPrice.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtStartPrice.Name = "txtStartPrice"
        Me.txtStartPrice.Size = New System.Drawing.Size(103, 28)
        Me.txtStartPrice.TabIndex = 74
        Me.txtStartPrice.Text = "1000"
        Me.txtStartPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(14, 270)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(44, 18)
        Me.Label19.TabIndex = 73
        Me.Label19.Text = "주가"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(299, 147)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(22, 18)
        Me.Label18.TabIndex = 72
        Me.Label18.Text = "%"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(147, 146)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(98, 18)
        Me.Label17.TabIndex = 71
        Me.Label17.Text = "시자점근접"
        '
        'txtSPPer
        '
        Me.txtSPPer.Location = New System.Drawing.Point(244, 140)
        Me.txtSPPer.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtSPPer.Name = "txtSPPer"
        Me.txtSPPer.Size = New System.Drawing.Size(48, 28)
        Me.txtSPPer.TabIndex = 70
        Me.txtSPPer.Text = "1.29"
        Me.txtSPPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(10, 224)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(158, 18)
        Me.Label16.TabIndex = 69
        Me.Label16.Text = "주포분석 종료날짜"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(10, 189)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(158, 18)
        Me.Label10.TabIndex = 68
        Me.Label10.Text = "주포분석 시작날짜"
        '
        'txtJupoEndDate
        '
        Me.txtJupoEndDate.Location = New System.Drawing.Point(170, 218)
        Me.txtJupoEndDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtJupoEndDate.Name = "txtJupoEndDate"
        Me.txtJupoEndDate.Size = New System.Drawing.Size(104, 28)
        Me.txtJupoEndDate.TabIndex = 67
        Me.txtJupoEndDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtJupo1
        '
        Me.txtJupo1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtJupo1.Location = New System.Drawing.Point(149, 99)
        Me.txtJupo1.Name = "txtJupo1"
        Me.txtJupo1.Size = New System.Drawing.Size(97, 28)
        Me.txtJupo1.TabIndex = 63
        Me.txtJupo1.Text = "1000000"
        Me.txtJupo1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtJupoStartDate
        '
        Me.txtJupoStartDate.Location = New System.Drawing.Point(170, 182)
        Me.txtJupoStartDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtJupoStartDate.Name = "txtJupoStartDate"
        Me.txtJupoStartDate.Size = New System.Drawing.Size(103, 28)
        Me.txtJupoStartDate.TabIndex = 66
        Me.txtJupoStartDate.Text = "20160101"
        Me.txtJupoStartDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(96, 75)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(26, 18)
        Me.Label15.TabIndex = 65
        Me.Label15.Text = "봉"
        '
        'txtSPBong
        '
        Me.txtSPBong.Location = New System.Drawing.Point(16, 70)
        Me.txtSPBong.Name = "txtSPBong"
        Me.txtSPBong.Size = New System.Drawing.Size(74, 28)
        Me.txtSPBong.TabIndex = 64
        Me.txtSPBong.Text = "250"
        Me.txtSPBong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(147, 74)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(150, 18)
        Me.Label14.TabIndex = 62
        Me.Label14.Text = "주포1순매수 이상"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(147, 32)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(80, 18)
        Me.Label13.TabIndex = 61
        Me.Label13.Text = "기준날짜"
        '
        'chkBizSunValue
        '
        Me.chkBizSunValue.AutoSize = True
        Me.chkBizSunValue.Checked = True
        Me.chkBizSunValue.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBizSunValue.Location = New System.Drawing.Point(11, 136)
        Me.chkBizSunValue.Name = "chkBizSunValue"
        Me.chkBizSunValue.Size = New System.Drawing.Size(115, 22)
        Me.chkBizSunValue.TabIndex = 60
        Me.chkBizSunValue.Text = "당기순익+"
        Me.chkBizSunValue.UseVisualStyleBackColor = True
        '
        'chkBizValue
        '
        Me.chkBizValue.AutoSize = True
        Me.chkBizValue.Checked = True
        Me.chkBizValue.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBizValue.Location = New System.Drawing.Point(11, 106)
        Me.chkBizValue.Name = "chkBizValue"
        Me.chkBizValue.Size = New System.Drawing.Size(115, 22)
        Me.chkBizValue.TabIndex = 59
        Me.chkBizValue.Text = "영업이익+"
        Me.chkBizValue.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button14)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.txtSignValue)
        Me.GroupBox2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(1096, 405)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(246, 88)
        Me.GroupBox2.TabIndex = 64
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "체결정보찾기"
        '
        'Button16
        '
        Me.Button16.Enabled = False
        Me.Button16.Location = New System.Drawing.Point(901, 1048)
        Me.Button16.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button16.Name = "Button16"
        Me.Button16.Size = New System.Drawing.Size(113, 39)
        Me.Button16.TabIndex = 65
        Me.Button16.Text = "PER"
        Me.Button16.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1830, 870)
        Me.Controls.Add(Me.Button16)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button15)
        Me.Controls.Add(Me.Button13)
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
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
    Friend WithEvents Button13 As System.Windows.Forms.Button
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
End Class
