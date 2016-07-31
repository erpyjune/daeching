<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStartPoint
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStartPoint))
        Me.lstViewStartPoint = New System.Windows.Forms.ListView()
        Me.종목이름 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.시작점날짜 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.시작점유형 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.종목상태 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.주포1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.주포2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.주포3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.상장주식수 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.시가총액 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.영업이익 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.당기순이익 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.KHOpenAPI = New AxKHOpenAPILib.AxKHOpenAPI()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.lbMsg1 = New System.Windows.Forms.Label()
        Me.lbMsg2 = New System.Windows.Forms.Label()
        CType(Me.KHOpenAPI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lstViewStartPoint
        '
        Me.lstViewStartPoint.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.종목이름, Me.시작점날짜, Me.시작점유형, Me.종목상태, Me.주포1, Me.주포2, Me.주포3, Me.상장주식수, Me.시가총액, Me.영업이익, Me.당기순이익})
        Me.lstViewStartPoint.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lstViewStartPoint.Location = New System.Drawing.Point(13, 100)
        Me.lstViewStartPoint.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lstViewStartPoint.Name = "lstViewStartPoint"
        Me.lstViewStartPoint.Size = New System.Drawing.Size(1458, 608)
        Me.lstViewStartPoint.TabIndex = 0
        Me.lstViewStartPoint.UseCompatibleStateImageBehavior = False
        Me.lstViewStartPoint.View = System.Windows.Forms.View.Details
        '
        '종목이름
        '
        Me.종목이름.Text = "종목이름"
        Me.종목이름.Width = 92
        '
        '시작점날짜
        '
        Me.시작점날짜.Text = "시작점날짜"
        Me.시작점날짜.Width = 105
        '
        '시작점유형
        '
        Me.시작점유형.Text = "시작점유형"
        Me.시작점유형.Width = 102
        '
        '종목상태
        '
        Me.종목상태.Text = "종목상태"
        Me.종목상태.Width = 169
        '
        '주포1
        '
        Me.주포1.Text = "주포1"
        Me.주포1.Width = 78
        '
        '주포2
        '
        Me.주포2.Text = "주포2"
        Me.주포2.Width = 72
        '
        '주포3
        '
        Me.주포3.Text = "주포3"
        Me.주포3.Width = 66
        '
        '상장주식수
        '
        Me.상장주식수.Text = "상장주식수"
        Me.상장주식수.Width = 76
        '
        '시가총액
        '
        Me.시가총액.Text = "시가총액"
        '
        '영업이익
        '
        Me.영업이익.Text = "영업이익"
        '
        '당기순이익
        '
        Me.당기순이익.Text = "당기순이익"
        Me.당기순이익.Width = 80
        '
        'KHOpenAPI
        '
        Me.KHOpenAPI.Enabled = True
        Me.KHOpenAPI.Location = New System.Drawing.Point(965, -1)
        Me.KHOpenAPI.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.KHOpenAPI.Name = "KHOpenAPI"
        Me.KHOpenAPI.OcxState = CType(resources.GetObject("KHOpenAPI.OcxState"), System.Windows.Forms.AxHost.State)
        Me.KHOpenAPI.Size = New System.Drawing.Size(148, 45)
        Me.KHOpenAPI.TabIndex = 1
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(13, 54)
        Me.ProgressBar1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(1460, 38)
        Me.ProgressBar1.TabIndex = 2
        '
        'lbMsg1
        '
        Me.lbMsg1.AutoSize = True
        Me.lbMsg1.Font = New System.Drawing.Font("굴림", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbMsg1.Location = New System.Drawing.Point(11, 22)
        Me.lbMsg1.Name = "lbMsg1"
        Me.lbMsg1.Size = New System.Drawing.Size(55, 20)
        Me.lbMsg1.TabIndex = 3
        Me.lbMsg1.Text = "Msg1"
        '
        'lbMsg2
        '
        Me.lbMsg2.AutoSize = True
        Me.lbMsg2.Font = New System.Drawing.Font("굴림", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbMsg2.Location = New System.Drawing.Point(340, 22)
        Me.lbMsg2.Name = "lbMsg2"
        Me.lbMsg2.Size = New System.Drawing.Size(55, 20)
        Me.lbMsg2.TabIndex = 4
        Me.lbMsg2.Text = "Msg2"
        '
        'frmStartPoint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1476, 720)
        Me.Controls.Add(Me.lbMsg2)
        Me.Controls.Add(Me.lbMsg1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.KHOpenAPI)
        Me.Controls.Add(Me.lstViewStartPoint)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmStartPoint"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "시작점"
        CType(Me.KHOpenAPI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstViewStartPoint As System.Windows.Forms.ListView
    Friend WithEvents 종목이름 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 시작점날짜 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 시작점유형 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 종목상태 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 주포1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 주포2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 주포3 As System.Windows.Forms.ColumnHeader
    Public WithEvents KHOpenAPI As AxKHOpenAPILib.AxKHOpenAPI
    Friend WithEvents 상장주식수 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 시가총액 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 영업이익 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 당기순이익 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lbMsg1 As System.Windows.Forms.Label
    Friend WithEvents lbMsg2 As System.Windows.Forms.Label
End Class
