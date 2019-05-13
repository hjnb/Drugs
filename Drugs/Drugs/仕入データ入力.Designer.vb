<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 仕入データ入力
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
        Me.components = New System.ComponentModel.Container()
        Me.GroupBox = New System.Windows.Forms.GroupBox()
        Me.taxBox = New System.Windows.Forms.ComboBox()
        Me.tankaBox = New System.Windows.Forms.TextBox()
        Me.suryoBox = New System.Windows.Forms.TextBox()
        Me.namBox = New System.Windows.Forms.TextBox()
        Me.codBox = New System.Windows.Forms.TextBox()
        Me.dennoBox = New System.Windows.Forms.TextBox()
        Me.siireBox = New System.Windows.Forms.ComboBox()
        Me.YmdBox = New ymdBox.ymdBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnChange = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.dgvSearch = New Drugs.SiireDataGridView(Me.components)
        Me.dgvSiire = New Drugs.SiireDataGridView(Me.components)
        Me.GroupBox.SuspendLayout()
        CType(Me.dgvSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSiire, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox
        '
        Me.GroupBox.Controls.Add(Me.taxBox)
        Me.GroupBox.Controls.Add(Me.tankaBox)
        Me.GroupBox.Controls.Add(Me.suryoBox)
        Me.GroupBox.Controls.Add(Me.namBox)
        Me.GroupBox.Controls.Add(Me.codBox)
        Me.GroupBox.Controls.Add(Me.dennoBox)
        Me.GroupBox.Controls.Add(Me.siireBox)
        Me.GroupBox.Controls.Add(Me.YmdBox)
        Me.GroupBox.Controls.Add(Me.Label8)
        Me.GroupBox.Controls.Add(Me.Label7)
        Me.GroupBox.Controls.Add(Me.Label6)
        Me.GroupBox.Controls.Add(Me.Label5)
        Me.GroupBox.Controls.Add(Me.Label4)
        Me.GroupBox.Controls.Add(Me.Label3)
        Me.GroupBox.Controls.Add(Me.Label2)
        Me.GroupBox.Controls.Add(Me.Label1)
        Me.GroupBox.Location = New System.Drawing.Point(18, 25)
        Me.GroupBox.Name = "GroupBox"
        Me.GroupBox.Size = New System.Drawing.Size(393, 223)
        Me.GroupBox.TabIndex = 1
        Me.GroupBox.TabStop = False
        '
        'taxBox
        '
        Me.taxBox.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.taxBox.FormattingEnabled = True
        Me.taxBox.Location = New System.Drawing.Point(289, 18)
        Me.taxBox.Name = "taxBox"
        Me.taxBox.Size = New System.Drawing.Size(67, 20)
        Me.taxBox.TabIndex = 100
        '
        'tankaBox
        '
        Me.tankaBox.Font = New System.Drawing.Font("MS UI Gothic", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.tankaBox.Location = New System.Drawing.Point(83, 184)
        Me.tankaBox.Name = "tankaBox"
        Me.tankaBox.Size = New System.Drawing.Size(100, 22)
        Me.tankaBox.TabIndex = 106
        '
        'suryoBox
        '
        Me.suryoBox.Font = New System.Drawing.Font("MS UI Gothic", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.suryoBox.Location = New System.Drawing.Point(83, 159)
        Me.suryoBox.Name = "suryoBox"
        Me.suryoBox.Size = New System.Drawing.Size(100, 22)
        Me.suryoBox.TabIndex = 105
        '
        'namBox
        '
        Me.namBox.Font = New System.Drawing.Font("MS UI Gothic", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.namBox.Location = New System.Drawing.Point(83, 134)
        Me.namBox.Name = "namBox"
        Me.namBox.Size = New System.Drawing.Size(304, 22)
        Me.namBox.TabIndex = 104
        '
        'codBox
        '
        Me.codBox.Font = New System.Drawing.Font("MS UI Gothic", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.codBox.Location = New System.Drawing.Point(83, 109)
        Me.codBox.Name = "codBox"
        Me.codBox.Size = New System.Drawing.Size(100, 22)
        Me.codBox.TabIndex = 103
        '
        'dennoBox
        '
        Me.dennoBox.Font = New System.Drawing.Font("MS UI Gothic", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dennoBox.Location = New System.Drawing.Point(83, 77)
        Me.dennoBox.Name = "dennoBox"
        Me.dennoBox.Size = New System.Drawing.Size(100, 22)
        Me.dennoBox.TabIndex = 102
        '
        'siireBox
        '
        Me.siireBox.Font = New System.Drawing.Font("MS UI Gothic", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.siireBox.FormattingEnabled = True
        Me.siireBox.Location = New System.Drawing.Point(83, 49)
        Me.siireBox.Name = "siireBox"
        Me.siireBox.Size = New System.Drawing.Size(121, 23)
        Me.siireBox.TabIndex = 101
        '
        'YmdBox
        '
        Me.YmdBox.boxType = 2
        Me.YmdBox.DateText = ""
        Me.YmdBox.EraLabelText = "H31"
        Me.YmdBox.EraText = ""
        Me.YmdBox.Location = New System.Drawing.Point(82, 13)
        Me.YmdBox.MonthLabelText = "04"
        Me.YmdBox.MonthText = ""
        Me.YmdBox.Name = "YmdBox"
        Me.YmdBox.Size = New System.Drawing.Size(110, 34)
        Me.YmdBox.TabIndex = 99
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(227, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 12)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "消費税率"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(34, 190)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 12)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "単価"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(34, 166)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "数量"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(34, 140)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 12)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "品名"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(34, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(24, 12)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "カナ"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(26, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 12)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "伝票No."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(26, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "仕入先"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "日付"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(466, 208)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(86, 37)
        Me.btnAdd.TabIndex = 2
        Me.btnAdd.Text = "追加"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnChange
        '
        Me.btnChange.Location = New System.Drawing.Point(551, 208)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(86, 37)
        Me.btnChange.TabIndex = 3
        Me.btnChange.Text = "変更"
        Me.btnChange.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(636, 208)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(86, 37)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "削除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(721, 208)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(86, 37)
        Me.btnPrint.TabIndex = 5
        Me.btnPrint.Text = "印刷"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'dgvSearch
        '
        Me.dgvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSearch.Location = New System.Drawing.Point(417, 31)
        Me.dgvSearch.Name = "dgvSearch"
        Me.dgvSearch.RowTemplate.Height = 21
        Me.dgvSearch.Size = New System.Drawing.Size(598, 151)
        Me.dgvSearch.TabIndex = 8
        '
        'dgvSiire
        '
        Me.dgvSiire.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSiire.Location = New System.Drawing.Point(18, 277)
        Me.dgvSiire.Name = "dgvSiire"
        Me.dgvSiire.RowTemplate.Height = 21
        Me.dgvSiire.Size = New System.Drawing.Size(975, 385)
        Me.dgvSiire.TabIndex = 7
        '
        '仕入データ入力
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1050, 697)
        Me.Controls.Add(Me.dgvSearch)
        Me.Controls.Add(Me.dgvSiire)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnChange)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.GroupBox)
        Me.Name = "仕入データ入力"
        Me.Text = "仕入データ入力"
        Me.GroupBox.ResumeLayout(False)
        Me.GroupBox.PerformLayout()
        CType(Me.dgvSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSiire, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tankaBox As System.Windows.Forms.TextBox
    Friend WithEvents suryoBox As System.Windows.Forms.TextBox
    Friend WithEvents namBox As System.Windows.Forms.TextBox
    Friend WithEvents codBox As System.Windows.Forms.TextBox
    Friend WithEvents dennoBox As System.Windows.Forms.TextBox
    Friend WithEvents siireBox As System.Windows.Forms.ComboBox
    Friend WithEvents YmdBox As ymdBox.ymdBox
    Friend WithEvents taxBox As System.Windows.Forms.ComboBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnChange As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents dgvSiire As Drugs.SiireDataGridView
    Friend WithEvents dgvSearch As Drugs.SiireDataGridView
End Class
