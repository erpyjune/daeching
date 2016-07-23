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
        Me.KHOpenAPI = New AxKHOpenAPILib.AxKHOpenAPI()
        CType(Me.KHOpenAPI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lstViewStartPoint
        '
        Me.lstViewStartPoint.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.종목이름, Me.시작점날짜, Me.시작점유형, Me.종목상태, Me.주포1, Me.주포2, Me.주포3})
        Me.lstViewStartPoint.Location = New System.Drawing.Point(9, 60)
        Me.lstViewStartPoint.Name = "lstViewStartPoint"
        Me.lstViewStartPoint.Size = New System.Drawing.Size(1235, 572)
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
        Me.종목상태.Width = 177
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
        'KHOpenAPI
        '
        Me.KHOpenAPI.Enabled = True
        Me.KHOpenAPI.Location = New System.Drawing.Point(1158, 12)
        Me.KHOpenAPI.Name = "KHOpenAPI"
        Me.KHOpenAPI.OcxState = CType(resources.GetObject("KHOpenAPI.OcxState"), System.Windows.Forms.AxHost.State)
        Me.KHOpenAPI.Size = New System.Drawing.Size(66, 20)
        Me.KHOpenAPI.TabIndex = 1
        '
        'frmStartPoint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1251, 638)
        Me.Controls.Add(Me.KHOpenAPI)
        Me.Controls.Add(Me.lstViewStartPoint)
        Me.Name = "frmStartPoint"
        Me.Text = "시작점"
        CType(Me.KHOpenAPI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

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
End Class
