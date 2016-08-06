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
        Me.주가등락 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.체결강도 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.현재가 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.출현횟수 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pBar = New System.Windows.Forms.ProgressBar()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lstSign
        '
        Me.lstSign.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.종목명, Me.체결시간, Me.체결량, Me.주가등락, Me.체결강도, Me.현재가, Me.출현횟수})
        Me.lstSign.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lstSign.Location = New System.Drawing.Point(10, 70)
        Me.lstSign.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.lstSign.Name = "lstSign"
        Me.lstSign.Size = New System.Drawing.Size(951, 443)
        Me.lstSign.TabIndex = 0
        Me.lstSign.UseCompatibleStateImageBehavior = False
        Me.lstSign.View = System.Windows.Forms.View.Details
        '
        '종목명
        '
        Me.종목명.Text = "종목명"
        Me.종목명.Width = 161
        '
        '체결시간
        '
        Me.체결시간.Text = "체결시간"
        Me.체결시간.Width = 128
        '
        '체결량
        '
        Me.체결량.Text = "체결량"
        Me.체결량.Width = 111
        '
        '주가등락
        '
        Me.주가등락.Text = "주가등락"
        Me.주가등락.Width = 113
        '
        '체결강도
        '
        Me.체결강도.Text = "체결강도"
        Me.체결강도.Width = 109
        '
        '현재가
        '
        Me.현재가.Text = "현재가"
        Me.현재가.Width = 95
        '
        '출현횟수
        '
        Me.출현횟수.Text = "출현횟수"
        Me.출현횟수.Width = 108
        '
        'pBar
        '
        Me.pBar.Location = New System.Drawing.Point(13, 39)
        Me.pBar.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
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
        Me.ClientSize = New System.Drawing.Size(965, 518)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.pBar)
        Me.Controls.Add(Me.lstSign)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
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
End Class
