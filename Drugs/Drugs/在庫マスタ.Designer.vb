<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 在庫マスタ
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblTannka = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtZaiko = New System.Windows.Forms.TextBox()
        Me.txtNam = New System.Windows.Forms.TextBox()
        Me.txtCod = New System.Windows.Forms.TextBox()
        Me.txtBunrui = New System.Windows.Forms.TextBox()
        Me.cmbSiire = New System.Windows.Forms.ComboBox()
        Me.txtTani = New System.Windows.Forms.TextBox()
        Me.txtKonyu = New System.Windows.Forms.TextBox()
        Me.txtSokB = New System.Windows.Forms.TextBox()
        Me.txtYakB = New System.Windows.Forms.TextBox()
        Me.txtGaiB = New System.Windows.Forms.TextBox()
        Me.txtByoB = New System.Windows.Forms.TextBox()
        Me.txtText = New System.Windows.Forms.TextBox()
        Me.btbTouroku = New System.Windows.Forms.Button()
        Me.btnSakujo = New System.Windows.Forms.Button()
        Me.btnNenngetuSakujo = New System.Windows.Forms.Button()
        Me.btnInnsatu = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.btnLastMonthCopy = New System.Windows.Forms.Button()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'YmdBox1
        '
        Me.YmdBox1.boxType = 6
        Me.YmdBox1.DateText = ""
        Me.YmdBox1.EraLabelText = "R01"
        Me.YmdBox1.EraText = ""
        Me.YmdBox1.Location = New System.Drawing.Point(28, 21)
        Me.YmdBox1.MonthLabelText = "05"
        Me.YmdBox1.MonthText = ""
        Me.YmdBox1.Name = "YmdBox1"
        Me.YmdBox1.Size = New System.Drawing.Size(85, 34)
        Me.YmdBox1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(145, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "在庫コード"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(301, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "品名"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(643, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(24, 12)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "カナ"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(752, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 12)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "分類"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(886, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 12)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "1 内服"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Blue
        Me.Label6.Location = New System.Drawing.Point(886, 28)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 12)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "3 注射"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(886, 43)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 12)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "5 外用"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Blue
        Me.Label8.Location = New System.Drawing.Point(886, 58)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(46, 12)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "9 その他"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(145, 75)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 12)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "仕入先"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(301, 75)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(29, 12)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "単位"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(448, 75)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(41, 12)
        Me.Label11.TabIndex = 11
        Me.Label11.Text = "購入額"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(643, 75)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 12)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "単位単価"
        '
        'lblTannka
        '
        Me.lblTannka.AutoSize = True
        Me.lblTannka.Location = New System.Drawing.Point(720, 75)
        Me.lblTannka.Name = "lblTannka"
        Me.lblTannka.Size = New System.Drawing.Size(11, 12)
        Me.lblTannka.TabIndex = 13
        Me.lblTannka.Text = "-"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(144, 120)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 12)
        Me.Label13.TabIndex = 14
        Me.Label13.Text = "在庫場所"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(177, 151)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(41, 12)
        Me.Label14.TabIndex = 15
        Me.Label14.Text = "薬品庫"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(237, 151)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(29, 12)
        Me.Label15.TabIndex = 16
        Me.Label15.Text = "薬局"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(292, 151)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(29, 12)
        Me.Label16.TabIndex = 17
        Me.Label16.Text = "外来"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(347, 151)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(29, 12)
        Me.Label17.TabIndex = 18
        Me.Label17.Text = "病棟"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.ForeColor = System.Drawing.Color.Blue
        Me.Label18.Location = New System.Drawing.Point(400, 155)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(49, 24)
        Me.Label18.TabIndex = 19
        Me.Label18.Text = "1.該当" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "0.非該当" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(643, 116)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(22, 12)
        Me.Label19.TabIndex = 20
        Me.Label19.Text = "メモ"
        '
        'txtZaiko
        '
        Me.txtZaiko.Location = New System.Drawing.Point(207, 30)
        Me.txtZaiko.Name = "txtZaiko"
        Me.txtZaiko.Size = New System.Drawing.Size(72, 19)
        Me.txtZaiko.TabIndex = 21
        '
        'txtNam
        '
        Me.txtNam.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtNam.Location = New System.Drawing.Point(337, 30)
        Me.txtNam.Name = "txtNam"
        Me.txtNam.Size = New System.Drawing.Size(283, 19)
        Me.txtNam.TabIndex = 22
        '
        'txtCod
        '
        Me.txtCod.ImeMode = System.Windows.Forms.ImeMode.Katakana
        Me.txtCod.Location = New System.Drawing.Point(673, 30)
        Me.txtCod.Name = "txtCod"
        Me.txtCod.Size = New System.Drawing.Size(50, 19)
        Me.txtCod.TabIndex = 23
        '
        'txtBunrui
        '
        Me.txtBunrui.Location = New System.Drawing.Point(801, 30)
        Me.txtBunrui.Name = "txtBunrui"
        Me.txtBunrui.Size = New System.Drawing.Size(45, 19)
        Me.txtBunrui.TabIndex = 24
        Me.txtBunrui.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmbSiire
        '
        Me.cmbSiire.FormattingEnabled = True
        Me.cmbSiire.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.cmbSiire.Items.AddRange(New Object() {"モロオ", "東邦", "ｼｵｻﾞﾜ"})
        Me.cmbSiire.Location = New System.Drawing.Point(207, 72)
        Me.cmbSiire.Name = "cmbSiire"
        Me.cmbSiire.Size = New System.Drawing.Size(72, 20)
        Me.cmbSiire.TabIndex = 25
        '
        'txtTani
        '
        Me.txtTani.Location = New System.Drawing.Point(337, 72)
        Me.txtTani.Name = "txtTani"
        Me.txtTani.Size = New System.Drawing.Size(49, 19)
        Me.txtTani.TabIndex = 26
        '
        'txtKonyu
        '
        Me.txtKonyu.Location = New System.Drawing.Point(495, 72)
        Me.txtKonyu.Name = "txtKonyu"
        Me.txtKonyu.Size = New System.Drawing.Size(61, 19)
        Me.txtKonyu.TabIndex = 27
        '
        'txtSokB
        '
        Me.txtSokB.Location = New System.Drawing.Point(183, 166)
        Me.txtSokB.Name = "txtSokB"
        Me.txtSokB.Size = New System.Drawing.Size(26, 19)
        Me.txtSokB.TabIndex = 28
        Me.txtSokB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtYakB
        '
        Me.txtYakB.Location = New System.Drawing.Point(238, 166)
        Me.txtYakB.Name = "txtYakB"
        Me.txtYakB.Size = New System.Drawing.Size(26, 19)
        Me.txtYakB.TabIndex = 29
        Me.txtYakB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtGaiB
        '
        Me.txtGaiB.Location = New System.Drawing.Point(293, 166)
        Me.txtGaiB.Name = "txtGaiB"
        Me.txtGaiB.Size = New System.Drawing.Size(26, 19)
        Me.txtGaiB.TabIndex = 30
        Me.txtGaiB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtByoB
        '
        Me.txtByoB.Location = New System.Drawing.Point(348, 166)
        Me.txtByoB.Name = "txtByoB"
        Me.txtByoB.Size = New System.Drawing.Size(26, 19)
        Me.txtByoB.TabIndex = 31
        Me.txtByoB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtText
        '
        Me.txtText.Location = New System.Drawing.Point(645, 138)
        Me.txtText.Name = "txtText"
        Me.txtText.Size = New System.Drawing.Size(240, 19)
        Me.txtText.TabIndex = 32
        '
        'btbTouroku
        '
        Me.btbTouroku.Location = New System.Drawing.Point(589, 185)
        Me.btbTouroku.Name = "btbTouroku"
        Me.btbTouroku.Size = New System.Drawing.Size(73, 30)
        Me.btbTouroku.TabIndex = 33
        Me.btbTouroku.Text = "登録"
        Me.btbTouroku.UseVisualStyleBackColor = True
        '
        'btnSakujo
        '
        Me.btnSakujo.Location = New System.Drawing.Point(668, 185)
        Me.btnSakujo.Name = "btnSakujo"
        Me.btnSakujo.Size = New System.Drawing.Size(73, 30)
        Me.btnSakujo.TabIndex = 34
        Me.btnSakujo.Text = "削除"
        Me.btnSakujo.UseVisualStyleBackColor = True
        '
        'btnNenngetuSakujo
        '
        Me.btnNenngetuSakujo.Location = New System.Drawing.Point(747, 185)
        Me.btnNenngetuSakujo.Name = "btnNenngetuSakujo"
        Me.btnNenngetuSakujo.Size = New System.Drawing.Size(73, 30)
        Me.btnNenngetuSakujo.TabIndex = 35
        Me.btnNenngetuSakujo.Text = "年月・削除"
        Me.btnNenngetuSakujo.UseVisualStyleBackColor = True
        '
        'btnInnsatu
        '
        Me.btnInnsatu.Location = New System.Drawing.Point(826, 185)
        Me.btnInnsatu.Name = "btnInnsatu"
        Me.btnInnsatu.Size = New System.Drawing.Size(73, 30)
        Me.btnInnsatu.TabIndex = 36
        Me.btnInnsatu.Text = "印刷"
        Me.btnInnsatu.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.ColumnHeadersHeight = 20
        Me.DataGridView1.Location = New System.Drawing.Point(37, 238)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 21
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(838, 405)
        Me.DataGridView1.TabIndex = 37
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.Black
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(474, 125)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 90)
        Me.Label24.TabIndex = 54
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.Black
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(159, 138)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 77)
        Me.Label23.TabIndex = 53
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Black
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(159, 214)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(316, 1)
        Me.Label21.TabIndex = 52
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Black
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(201, 125)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(274, 1)
        Me.Label20.TabIndex = 55
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(47, 80)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(44, 12)
        Me.Label22.TabIndex = 56
        Me.Label22.Text = "Label22"
        Me.Label22.Visible = False
        '
        'btnLastMonthCopy
        '
        Me.btnLastMonthCopy.Location = New System.Drawing.Point(983, 25)
        Me.btnLastMonthCopy.Name = "btnLastMonthCopy"
        Me.btnLastMonthCopy.Size = New System.Drawing.Size(78, 30)
        Me.btnLastMonthCopy.TabIndex = 57
        Me.btnLastMonthCopy.Text = "前月コピー"
        Me.btnLastMonthCopy.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(993, 39)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowTemplate.Height = 21
        Me.DataGridView2.Size = New System.Drawing.Size(10, 10)
        Me.DataGridView2.TabIndex = 58
        '
        '在庫マスタ
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1483, 795)
        Me.Controls.Add(Me.btnLastMonthCopy)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btnInnsatu)
        Me.Controls.Add(Me.btnNenngetuSakujo)
        Me.Controls.Add(Me.btnSakujo)
        Me.Controls.Add(Me.btbTouroku)
        Me.Controls.Add(Me.txtText)
        Me.Controls.Add(Me.txtByoB)
        Me.Controls.Add(Me.txtGaiB)
        Me.Controls.Add(Me.txtYakB)
        Me.Controls.Add(Me.txtSokB)
        Me.Controls.Add(Me.txtKonyu)
        Me.Controls.Add(Me.txtTani)
        Me.Controls.Add(Me.cmbSiire)
        Me.Controls.Add(Me.txtBunrui)
        Me.Controls.Add(Me.txtCod)
        Me.Controls.Add(Me.txtNam)
        Me.Controls.Add(Me.txtZaiko)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.lblTannka)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.YmdBox1)
        Me.Name = "在庫マスタ"
        Me.Text = "在庫マスタ"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents YmdBox1 As ymdBox.ymdBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblTannka As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtZaiko As System.Windows.Forms.TextBox
    Friend WithEvents txtNam As System.Windows.Forms.TextBox
    Friend WithEvents txtCod As System.Windows.Forms.TextBox
    Friend WithEvents txtBunrui As System.Windows.Forms.TextBox
    Friend WithEvents cmbSiire As System.Windows.Forms.ComboBox
    Friend WithEvents txtTani As System.Windows.Forms.TextBox
    Friend WithEvents txtKonyu As System.Windows.Forms.TextBox
    Friend WithEvents txtSokB As System.Windows.Forms.TextBox
    Friend WithEvents txtYakB As System.Windows.Forms.TextBox
    Friend WithEvents txtGaiB As System.Windows.Forms.TextBox
    Friend WithEvents txtByoB As System.Windows.Forms.TextBox
    Friend WithEvents txtText As System.Windows.Forms.TextBox
    Friend WithEvents btbTouroku As System.Windows.Forms.Button
    Friend WithEvents btnSakujo As System.Windows.Forms.Button
    Friend WithEvents btnNenngetuSakujo As System.Windows.Forms.Button
    Friend WithEvents btnInnsatu As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents btnLastMonthCopy As System.Windows.Forms.Button
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
End Class
