<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAPI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAPI))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.KHOpenAPI = New AxKHOpenAPILib.AxKHOpenAPI()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.KHOpenAPI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(63, 92)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(199, 58)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "테스트"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'KHOpenAPI
        '
        Me.KHOpenAPI.Enabled = True
        Me.KHOpenAPI.Location = New System.Drawing.Point(216, 45)
        Me.KHOpenAPI.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.KHOpenAPI.Name = "KHOpenAPI"
        Me.KHOpenAPI.OcxState = CType(resources.GetObject("KHOpenAPI.OcxState"), System.Windows.Forms.AxHost.State)
        Me.KHOpenAPI.Size = New System.Drawing.Size(99, 30)
        Me.KHOpenAPI.TabIndex = 1
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(54, 32)
        Me.txtCode.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(113, 28)
        Me.txtCode.TabIndex = 2
        Me.txtCode.Text = "058820"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(109, 207)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(127, 49)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'frmAPI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(517, 429)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.txtCode)
        Me.Controls.Add(Me.KHOpenAPI)
        Me.Controls.Add(Me.Button1)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmAPI"
        Me.Text = "frmAPI"
        CType(Me.KHOpenAPI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Public WithEvents KHOpenAPI As AxKHOpenAPILib.AxKHOpenAPI
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
