<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSign
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
        Me.lstSign = New System.Windows.Forms.ListView()
        Me.종목명 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.체결시간 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.체결량 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.거래대금 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.분봉시작점 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.주가등락 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.출현횟수 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.거래량 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.주포1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.주포2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.주포3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.체결강도 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.현재가 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pBar = New System.Windows.Forms.ProgressBar()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lstSign
        '
        Me.lstSign.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.종목명, Me.체결시간, Me.체결량, Me.현재가, Me.거래대금, Me.분봉시작점, Me.주가등락, Me.출현횟수, Me.거래량, Me.주포1, Me.주포2, Me.주포3, Me.체결강도})
        Me.lstSign.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lstSign.Location = New System.Drawing.Point(10, 70)
        Me.lstSign.Margin = New System.Windows.Forms.Padding(2)
        Me.lstSign.Name = "lstSign"
        Me.lstSign.Size = New System.Drawing.Size(962, 443)
        Me.lstSign.TabIndex = 0
        Me.lstSign.UseCompatibleStateImageBehavior = False
        Me.lstSign.View = System.Windows.Forms.View.Details
        '
        '종목명
        '
        Me.종목명.Text = "종목명"
        Me.종목명.Width = 119
        '
        '체결시간
        '
        Me.체결시간.Text = "체결시간"
        Me.체결시간.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.체결시간.Width = 69
        '
        '체결량
        '
        Me.체결량.Text = "체결량"
        Me.체결량.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.체결량.Width = 64
        '
        '거래대금
        '
        Me.거래대금.DisplayIndex = 12
        Me.거래대금.Text = "거래대금"
        Me.거래대금.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.거래대금.Width = 71
        '
        '분봉시작점
        '
        Me.분봉시작점.DisplayIndex = 7
        Me.분봉시작점.Text = "분봉시작점"
        Me.분봉시작점.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.분봉시작점.Width = 76
        '
        '주가등락
        '
        Me.주가등락.DisplayIndex = 3
        Me.주가등락.Text = "주가등락"
        Me.주가등락.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.주가등락.Width = 61
        '
        '출현횟수
        '
        Me.출현횟수.DisplayIndex = 6
        Me.출현횟수.Text = "출현횟수"
        Me.출현횟수.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.출현횟수.Width = 61
        '
        '거래량
        '
        Me.거래량.DisplayIndex = 11
        Me.거래량.Text = "거래량"
        Me.거래량.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.거래량.Width = 72
        '
        '주포1
        '
        Me.주포1.DisplayIndex = 8
        Me.주포1.Text = "주포1"
        Me.주포1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        '주포2
        '
        Me.주포2.DisplayIndex = 9
        Me.주포2.Text = "주포2"
        Me.주포2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        '주포3
        '
        Me.주포3.DisplayIndex = 10
        Me.주포3.Text = "주포3"
        Me.주포3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        '체결강도
        '
        Me.체결강도.DisplayIndex = 4
        Me.체결강도.Text = "체결강도"
        Me.체결강도.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.체결강도.Width = 64
        '
        '현재가
        '
        Me.현재가.DisplayIndex = 5
        Me.현재가.Text = "현재가"
        Me.현재가.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.현재가.Width = 71
        '
        'pBar
        '
        Me.pBar.Location = New System.Drawing.Point(13, 39)
        Me.pBar.Margin = New System.Windows.Forms.Padding(2)
        Me.pBar.Name = "pBar"
        Me.pBar.Size = New System.Drawing.Size(947, 27)
        Me.pBar.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(885, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(74, 24)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "멈춤"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmSign
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(976, 518)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.pBar)
        Me.Controls.Add(Me.lstSign)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "frmSign"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "신호등"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstSign As System.Windows.Forms.ListView
    Friend WithEvents 종목명 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 체결시간 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 체결량 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 주가등락 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 체결강도 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 현재가 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 출현횟수 As System.Windows.Forms.ColumnHeader
    Friend WithEvents pBar As System.Windows.Forms.ProgressBar
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents 거래대금 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 분봉시작점 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 거래량 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 주포1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 주포2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 주포3 As System.Windows.Forms.ColumnHeader
End Class
