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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.YmdBox1 = New ymdBox.ymdBox()
        Me.cmbBasyo = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtZaiko = New System.Windows.Forms.TextBox()
        Me.txtSuuryou = New System.Windows.Forms.TextBox()
        Me.btnTouroku = New System.Windows.Forms.Button()
        Me.btnTanaorosi = New System.Windows.Forms.Button()
        Me.btnGetumatusyuukei = New System.Windows.Forms.Button()
        Me.btnKinyuuhyou = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.lblNam = New System.Windows.Forms.Label()
        Me.txtKome = New System.Windows.Forms.TextBox()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.DataGridView3 = New System.Windows.Forms.DataGridView()
        Me.DataGridView4 = New System.Windows.Forms.DataGridView()
        Me.DataGridView5 = New System.Windows.Forms.DataGridView()
        Me.DataGridView6 = New System.Windows.Forms.DataGridView()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'YmdBox1
        '
        Me.YmdBox1.boxType = 6
        Me.YmdBox1.DateText = ""
        Me.YmdBox1.EraLabelText = "R01"
        Me.YmdBox1.EraText = ""
        Me.YmdBox1.Location = New System.Drawing.Point(37, 26)
        Me.YmdBox1.MonthLabelText = "05"
        Me.YmdBox1.MonthText = ""
        Me.YmdBox1.Name = "YmdBox1"
        Me.YmdBox1.Size = New System.Drawing.Size(85, 34)
        Me.YmdBox1.TabIndex = 0
        '
        'cmbBasyo
        '
        Me.cmbBasyo.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBasyo.FormattingEnabled = True
        Me.cmbBasyo.Items.AddRange(New Object() {"薬品庫", "薬局", "外来", "病棟"})
        Me.cmbBasyo.Location = New System.Drawing.Point(151, 33)
        Me.cmbBasyo.Name = "cmbBasyo"
        Me.cmbBasyo.Size = New System.Drawing.Size(91, 24)
        Me.cmbBasyo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(99, 85)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "在庫ｺｰﾄﾞ"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(99, 117)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "数量"
        '
        'txtZaiko
        '
        Me.txtZaiko.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtZaiko.Location = New System.Drawing.Point(167, 82)
        Me.txtZaiko.Name = "txtZaiko"
        Me.txtZaiko.Size = New System.Drawing.Size(75, 23)
        Me.txtZaiko.TabIndex = 4
        '
        'txtSuuryou
        '
        Me.txtSuuryou.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSuuryou.Location = New System.Drawing.Point(167, 114)
        Me.txtSuuryou.Name = "txtSuuryou"
        Me.txtSuuryou.Size = New System.Drawing.Size(75, 23)
        Me.txtSuuryou.TabIndex = 5
        '
        'btnTouroku
        '
        Me.btnTouroku.Location = New System.Drawing.Point(299, 146)
        Me.btnTouroku.Name = "btnTouroku"
        Me.btnTouroku.Size = New System.Drawing.Size(65, 29)
        Me.btnTouroku.TabIndex = 6
        Me.btnTouroku.Text = "登録"
        Me.btnTouroku.UseVisualStyleBackColor = True
        '
        'btnTanaorosi
        '
        Me.btnTanaorosi.Location = New System.Drawing.Point(385, 146)
        Me.btnTanaorosi.Name = "btnTanaorosi"
        Me.btnTanaorosi.Size = New System.Drawing.Size(65, 29)
        Me.btnTanaorosi.TabIndex = 7
        Me.btnTanaorosi.Text = "棚卸表"
        Me.btnTanaorosi.UseVisualStyleBackColor = True
        '
        'btnGetumatusyuukei
        '
        Me.btnGetumatusyuukei.Location = New System.Drawing.Point(470, 146)
        Me.btnGetumatusyuukei.Name = "btnGetumatusyuukei"
        Me.btnGetumatusyuukei.Size = New System.Drawing.Size(65, 29)
        Me.btnGetumatusyuukei.TabIndex = 8
        Me.btnGetumatusyuukei.Text = "月別集計"
        Me.btnGetumatusyuukei.UseVisualStyleBackColor = True
        '
        'btnKinyuuhyou
        '
        Me.btnKinyuuhyou.Location = New System.Drawing.Point(555, 146)
        Me.btnKinyuuhyou.Name = "btnKinyuuhyou"
        Me.btnKinyuuhyou.Size = New System.Drawing.Size(65, 29)
        Me.btnKinyuuhyou.TabIndex = 9
        Me.btnKinyuuhyou.Text = "記入表"
        Me.btnKinyuuhyou.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("MS UI Gothic", 14.0!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1.ColumnHeadersHeight = 28
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("MS UI Gothic", 14.0!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView1.Location = New System.Drawing.Point(24, 192)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(856, 491)
        Me.DataGridView1.TabIndex = 10
        '
        'lblNam
        '
        Me.lblNam.AutoSize = True
        Me.lblNam.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNam.ForeColor = System.Drawing.Color.Blue
        Me.lblNam.Location = New System.Drawing.Point(296, 81)
        Me.lblNam.Name = "lblNam"
        Me.lblNam.Size = New System.Drawing.Size(16, 16)
        Me.lblNam.TabIndex = 11
        Me.lblNam.Text = "-"
        '
        'txtKome
        '
        Me.txtKome.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKome.Location = New System.Drawing.Point(273, 114)
        Me.txtKome.Name = "txtKome"
        Me.txtKome.Size = New System.Drawing.Size(315, 23)
        Me.txtKome.TabIndex = 12
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AllowUserToResizeColumns = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(792, 192)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(10, 10)
        Me.DataGridView2.TabIndex = 13
        '
        'DataGridView3
        '
        Me.DataGridView3.AllowUserToAddRows = False
        Me.DataGridView3.AllowUserToDeleteRows = False
        Me.DataGridView3.AllowUserToResizeColumns = False
        Me.DataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView3.Location = New System.Drawing.Point(850, 192)
        Me.DataGridView3.Name = "DataGridView3"
        Me.DataGridView3.Size = New System.Drawing.Size(10, 10)
        Me.DataGridView3.TabIndex = 14
        '
        'DataGridView4
        '
        Me.DataGridView4.AllowUserToAddRows = False
        Me.DataGridView4.AllowUserToDeleteRows = False
        Me.DataGridView4.AllowUserToResizeColumns = False
        Me.DataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView4.Location = New System.Drawing.Point(808, 192)
        Me.DataGridView4.Name = "DataGridView4"
        Me.DataGridView4.Size = New System.Drawing.Size(10, 10)
        Me.DataGridView4.TabIndex = 15
        '
        'DataGridView5
        '
        Me.DataGridView5.AllowUserToAddRows = False
        Me.DataGridView5.AllowUserToDeleteRows = False
        Me.DataGridView5.AllowUserToResizeColumns = False
        Me.DataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView5.Location = New System.Drawing.Point(834, 192)
        Me.DataGridView5.Name = "DataGridView5"
        Me.DataGridView5.Size = New System.Drawing.Size(10, 10)
        Me.DataGridView5.TabIndex = 16
        '
        'DataGridView6
        '
        Me.DataGridView6.AllowUserToAddRows = False
        Me.DataGridView6.AllowUserToDeleteRows = False
        Me.DataGridView6.AllowUserToResizeColumns = False
        Me.DataGridView6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView6.Location = New System.Drawing.Point(870, 192)
        Me.DataGridView6.Name = "DataGridView6"
        Me.DataGridView6.Size = New System.Drawing.Size(10, 10)
        Me.DataGridView6.TabIndex = 20
        '
        '在庫入力
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1466, 766)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.DataGridView6)
        Me.Controls.Add(Me.DataGridView5)
        Me.Controls.Add(Me.DataGridView4)
        Me.Controls.Add(Me.DataGridView3)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.txtKome)
        Me.Controls.Add(Me.lblNam)
        Me.Controls.Add(Me.btnKinyuuhyou)
        Me.Controls.Add(Me.btnGetumatusyuukei)
        Me.Controls.Add(Me.btnTanaorosi)
        Me.Controls.Add(Me.btnTouroku)
        Me.Controls.Add(Me.txtSuuryou)
        Me.Controls.Add(Me.txtZaiko)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbBasyo)
        Me.Controls.Add(Me.YmdBox1)
        Me.Name = "在庫入力"
        Me.Text = "在庫入力"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents YmdBox1 As ymdBox.ymdBox
    Friend WithEvents cmbBasyo As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtZaiko As System.Windows.Forms.TextBox
    Friend WithEvents txtSuuryou As System.Windows.Forms.TextBox
    Friend WithEvents btnTouroku As System.Windows.Forms.Button
    Friend WithEvents btnTanaorosi As System.Windows.Forms.Button
    Friend WithEvents btnGetumatusyuukei As System.Windows.Forms.Button
    Friend WithEvents btnKinyuuhyou As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents lblNam As System.Windows.Forms.Label
    Friend WithEvents txtKome As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView3 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView4 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView5 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView6 As System.Windows.Forms.DataGridView
End Class
