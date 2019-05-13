<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 仕入集計
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.siireBox = New System.Windows.Forms.ComboBox()
        Me.rbtnNam = New System.Windows.Forms.RadioButton()
        Me.rbtnKingak = New System.Windows.Forms.RadioButton()
        Me.rbtnSuryo = New System.Windows.Forms.RadioButton()
        Me.fromYmdBox = New ymdBox.ymdBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.toYmdBox = New ymdBox.ymdBox()
        Me.btnExecute = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.siireBox)
        Me.GroupBox1.Controls.Add(Me.rbtnNam)
        Me.GroupBox1.Controls.Add(Me.rbtnKingak)
        Me.GroupBox1.Controls.Add(Me.rbtnSuryo)
        Me.GroupBox1.Location = New System.Drawing.Point(42, 46)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(349, 177)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'siireBox
        '
        Me.siireBox.FormattingEnabled = True
        Me.siireBox.Location = New System.Drawing.Point(205, 77)
        Me.siireBox.Name = "siireBox"
        Me.siireBox.Size = New System.Drawing.Size(111, 20)
        Me.siireBox.TabIndex = 3
        '
        'rbtnNam
        '
        Me.rbtnNam.AutoSize = True
        Me.rbtnNam.Location = New System.Drawing.Point(33, 79)
        Me.rbtnNam.Name = "rbtnNam"
        Me.rbtnNam.Size = New System.Drawing.Size(162, 16)
        Me.rbtnNam.TabIndex = 2
        Me.rbtnNam.TabStop = True
        Me.rbtnNam.Text = "品名別／月別　仕入れ数量"
        Me.rbtnNam.UseVisualStyleBackColor = True
        '
        'rbtnKingak
        '
        Me.rbtnKingak.AutoSize = True
        Me.rbtnKingak.Location = New System.Drawing.Point(33, 51)
        Me.rbtnKingak.Name = "rbtnKingak"
        Me.rbtnKingak.Size = New System.Drawing.Size(141, 16)
        Me.rbtnKingak.TabIndex = 1
        Me.rbtnKingak.TabStop = True
        Me.rbtnKingak.Text = "金額順　仕入れベスト50"
        Me.rbtnKingak.UseVisualStyleBackColor = True
        '
        'rbtnSuryo
        '
        Me.rbtnSuryo.AutoSize = True
        Me.rbtnSuryo.Location = New System.Drawing.Point(33, 23)
        Me.rbtnSuryo.Name = "rbtnSuryo"
        Me.rbtnSuryo.Size = New System.Drawing.Size(141, 16)
        Me.rbtnSuryo.TabIndex = 0
        Me.rbtnSuryo.TabStop = True
        Me.rbtnSuryo.Text = "数量順　仕入れベスト50"
        Me.rbtnSuryo.UseVisualStyleBackColor = True
        '
        'fromYmdBox
        '
        Me.fromYmdBox.boxType = 2
        Me.fromYmdBox.DateText = ""
        Me.fromYmdBox.EraLabelText = "H31"
        Me.fromYmdBox.EraText = ""
        Me.fromYmdBox.Location = New System.Drawing.Point(89, 251)
        Me.fromYmdBox.MonthLabelText = "04"
        Me.fromYmdBox.MonthText = ""
        Me.fromYmdBox.Name = "fromYmdBox"
        Me.fromYmdBox.Size = New System.Drawing.Size(110, 34)
        Me.fromYmdBox.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(213, 262)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(17, 12)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "～"
        '
        'toYmdBox
        '
        Me.toYmdBox.boxType = 2
        Me.toYmdBox.DateText = ""
        Me.toYmdBox.EraLabelText = "H31"
        Me.toYmdBox.EraText = ""
        Me.toYmdBox.Location = New System.Drawing.Point(241, 251)
        Me.toYmdBox.MonthLabelText = "04"
        Me.toYmdBox.MonthText = ""
        Me.toYmdBox.Name = "toYmdBox"
        Me.toYmdBox.Size = New System.Drawing.Size(110, 34)
        Me.toYmdBox.TabIndex = 4
        '
        'btnExecute
        '
        Me.btnExecute.Location = New System.Drawing.Point(312, 313)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Size = New System.Drawing.Size(79, 36)
        Me.btnExecute.TabIndex = 5
        Me.btnExecute.Text = "実行"
        Me.btnExecute.UseVisualStyleBackColor = True
        '
        '仕入集計
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(639, 499)
        Me.Controls.Add(Me.btnExecute)
        Me.Controls.Add(Me.toYmdBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.fromYmdBox)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "仕入集計"
        Me.Text = "仕入集計"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents siireBox As System.Windows.Forms.ComboBox
    Friend WithEvents rbtnNam As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnKingak As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnSuryo As System.Windows.Forms.RadioButton
    Friend WithEvents fromYmdBox As ymdBox.ymdBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents toYmdBox As ymdBox.ymdBox
    Friend WithEvents btnExecute As System.Windows.Forms.Button
End Class
