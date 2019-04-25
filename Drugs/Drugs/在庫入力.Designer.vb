<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 在庫入力
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
        Me.YmdBox1 = New ymdBox.ymdBox()
        Me.cmbBasyo = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtZaiko = New System.Windows.Forms.TextBox()
        Me.txtSuuryou = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.txtNam = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'YmdBox1
        '
        Me.YmdBox1.boxType = 6
        Me.YmdBox1.DateText = ""
        Me.YmdBox1.EraLabelText = "H31"
        Me.YmdBox1.EraText = ""
        Me.YmdBox1.Location = New System.Drawing.Point(37, 26)
        Me.YmdBox1.MonthLabelText = "04"
        Me.YmdBox1.MonthText = ""
        Me.YmdBox1.Name = "YmdBox1"
        Me.YmdBox1.Size = New System.Drawing.Size(85, 34)
        Me.YmdBox1.TabIndex = 0
        '
        'cmbBasyo
        '
        Me.cmbBasyo.FormattingEnabled = True
        Me.cmbBasyo.Location = New System.Drawing.Point(151, 33)
        Me.cmbBasyo.Name = "cmbBasyo"
        Me.cmbBasyo.Size = New System.Drawing.Size(91, 20)
        Me.cmbBasyo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(99, 85)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "在庫ｺｰﾄﾞ"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(99, 117)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "数量"
        '
        'txtZaiko
        '
        Me.txtZaiko.Location = New System.Drawing.Point(167, 82)
        Me.txtZaiko.Name = "txtZaiko"
        Me.txtZaiko.Size = New System.Drawing.Size(75, 19)
        Me.txtZaiko.TabIndex = 4
        '
        'txtSuuryou
        '
        Me.txtSuuryou.Location = New System.Drawing.Point(167, 114)
        Me.txtSuuryou.Name = "txtSuuryou"
        Me.txtSuuryou.Size = New System.Drawing.Size(75, 19)
        Me.txtSuuryou.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(299, 138)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(65, 29)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "登録"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(385, 138)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(65, 29)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "棚卸表"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(470, 138)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(65, 29)
        Me.Button3.TabIndex = 8
        Me.Button3.Text = "月別集計"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(555, 138)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(65, 29)
        Me.Button4.TabIndex = 9
        Me.Button4.Text = "記入表"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(52, 194)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(612, 491)
        Me.DataGridView1.TabIndex = 10
        '
        'txtNam
        '
        Me.txtNam.AutoSize = True
        Me.txtNam.ForeColor = System.Drawing.Color.Blue
        Me.txtNam.Location = New System.Drawing.Point(297, 85)
        Me.txtNam.Name = "txtNam"
        Me.txtNam.Size = New System.Drawing.Size(11, 12)
        Me.txtNam.TabIndex = 11
        Me.txtNam.Text = "-"
        '
        '在庫入力
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1306, 766)
        Me.Controls.Add(Me.txtNam)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtSuuryou)
        Me.Controls.Add(Me.txtZaiko)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbBasyo)
        Me.Controls.Add(Me.YmdBox1)
        Me.Name = "在庫入力"
        Me.Text = "在庫入力"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents YmdBox1 As ymdBox.ymdBox
    Friend WithEvents cmbBasyo As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtZaiko As System.Windows.Forms.TextBox
    Friend WithEvents txtSuuryou As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents txtNam As System.Windows.Forms.Label
End Class
