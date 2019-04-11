<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 仕入状況
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
        Me.siireBox = New System.Windows.Forms.ComboBox()
        Me.namLabel = New System.Windows.Forms.Label()
        Me.namListBox = New System.Windows.Forms.ListBox()
        Me.listRowCountLabel = New System.Windows.Forms.Label()
        Me.fromYmdBox = New ymdBox.ymdBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.toYmdBox = New ymdBox.ymdBox()
        Me.dgvSiire = New Drugs.SiireDataGridView(Me.components)
        Me.btnDisplay = New System.Windows.Forms.Button()
        Me.resultRowCountLabel = New System.Windows.Forms.Label()
        CType(Me.dgvSiire, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'siireBox
        '
        Me.siireBox.FormattingEnabled = True
        Me.siireBox.Location = New System.Drawing.Point(45, 35)
        Me.siireBox.Name = "siireBox"
        Me.siireBox.Size = New System.Drawing.Size(121, 20)
        Me.siireBox.TabIndex = 0
        '
        'namLabel
        '
        Me.namLabel.AutoSize = True
        Me.namLabel.Location = New System.Drawing.Point(43, 64)
        Me.namLabel.Name = "namLabel"
        Me.namLabel.Size = New System.Drawing.Size(0, 12)
        Me.namLabel.TabIndex = 1
        '
        'namListBox
        '
        Me.namListBox.BackColor = System.Drawing.SystemColors.Control
        Me.namListBox.FormattingEnabled = True
        Me.namListBox.ItemHeight = 12
        Me.namListBox.Location = New System.Drawing.Point(45, 78)
        Me.namListBox.Name = "namListBox"
        Me.namListBox.Size = New System.Drawing.Size(290, 556)
        Me.namListBox.TabIndex = 2
        '
        'listRowCountLabel
        '
        Me.listRowCountLabel.AutoSize = True
        Me.listRowCountLabel.Location = New System.Drawing.Point(52, 640)
        Me.listRowCountLabel.Name = "listRowCountLabel"
        Me.listRowCountLabel.Size = New System.Drawing.Size(0, 12)
        Me.listRowCountLabel.TabIndex = 3
        '
        'fromYmdBox
        '
        Me.fromYmdBox.boxType = 2
        Me.fromYmdBox.DateText = ""
        Me.fromYmdBox.EraLabelText = "H31"
        Me.fromYmdBox.EraText = ""
        Me.fromYmdBox.Location = New System.Drawing.Point(352, 27)
        Me.fromYmdBox.MonthLabelText = "04"
        Me.fromYmdBox.MonthText = ""
        Me.fromYmdBox.Name = "fromYmdBox"
        Me.fromYmdBox.Size = New System.Drawing.Size(110, 34)
        Me.fromYmdBox.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(475, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(21, 14)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "～"
        '
        'toYmdBox
        '
        Me.toYmdBox.boxType = 2
        Me.toYmdBox.DateText = ""
        Me.toYmdBox.EraLabelText = "H31"
        Me.toYmdBox.EraText = ""
        Me.toYmdBox.Location = New System.Drawing.Point(506, 27)
        Me.toYmdBox.MonthLabelText = "04"
        Me.toYmdBox.MonthText = ""
        Me.toYmdBox.Name = "toYmdBox"
        Me.toYmdBox.Size = New System.Drawing.Size(110, 34)
        Me.toYmdBox.TabIndex = 6
        '
        'dgvSiire
        '
        Me.dgvSiire.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSiire.Location = New System.Drawing.Point(352, 78)
        Me.dgvSiire.Name = "dgvSiire"
        Me.dgvSiire.RowTemplate.Height = 21
        Me.dgvSiire.Size = New System.Drawing.Size(304, 556)
        Me.dgvSiire.TabIndex = 7
        '
        'btnDisplay
        '
        Me.btnDisplay.Location = New System.Drawing.Point(672, 101)
        Me.btnDisplay.Name = "btnDisplay"
        Me.btnDisplay.Size = New System.Drawing.Size(87, 34)
        Me.btnDisplay.TabIndex = 8
        Me.btnDisplay.Text = "表示"
        Me.btnDisplay.UseVisualStyleBackColor = True
        '
        'resultRowCountLabel
        '
        Me.resultRowCountLabel.AutoSize = True
        Me.resultRowCountLabel.Location = New System.Drawing.Point(363, 640)
        Me.resultRowCountLabel.Name = "resultRowCountLabel"
        Me.resultRowCountLabel.Size = New System.Drawing.Size(0, 12)
        Me.resultRowCountLabel.TabIndex = 9
        '
        '仕入状況
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(770, 677)
        Me.Controls.Add(Me.resultRowCountLabel)
        Me.Controls.Add(Me.btnDisplay)
        Me.Controls.Add(Me.dgvSiire)
        Me.Controls.Add(Me.toYmdBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.fromYmdBox)
        Me.Controls.Add(Me.listRowCountLabel)
        Me.Controls.Add(Me.namListBox)
        Me.Controls.Add(Me.namLabel)
        Me.Controls.Add(Me.siireBox)
        Me.Name = "仕入状況"
        Me.Text = "仕入状況"
        CType(Me.dgvSiire, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents siireBox As System.Windows.Forms.ComboBox
    Friend WithEvents namLabel As System.Windows.Forms.Label
    Friend WithEvents namListBox As System.Windows.Forms.ListBox
    Friend WithEvents listRowCountLabel As System.Windows.Forms.Label
    Friend WithEvents fromYmdBox As ymdBox.ymdBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents toYmdBox As ymdBox.ymdBox
    Friend WithEvents dgvSiire As Drugs.SiireDataGridView
    Friend WithEvents btnDisplay As System.Windows.Forms.Button
    Friend WithEvents resultRowCountLabel As System.Windows.Forms.Label
End Class
