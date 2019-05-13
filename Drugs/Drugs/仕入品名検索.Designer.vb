<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 仕入品名検索
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.searchTextBox = New System.Windows.Forms.TextBox()
        Me.denDateLabel = New System.Windows.Forms.Label()
        Me.btnExecute = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dgvSearchResult = New System.Windows.Forms.DataGridView()
        CType(Me.dgvSearchResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(66, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "検索品名"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(66, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "伝票日付"
        '
        'searchTextBox
        '
        Me.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.searchTextBox.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.searchTextBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.searchTextBox.Location = New System.Drawing.Point(132, 45)
        Me.searchTextBox.Name = "searchTextBox"
        Me.searchTextBox.Size = New System.Drawing.Size(264, 14)
        Me.searchTextBox.TabIndex = 100
        '
        'denDateLabel
        '
        Me.denDateLabel.AutoSize = True
        Me.denDateLabel.Location = New System.Drawing.Point(130, 83)
        Me.denDateLabel.Name = "denDateLabel"
        Me.denDateLabel.Size = New System.Drawing.Size(0, 12)
        Me.denDateLabel.TabIndex = 4
        '
        'btnExecute
        '
        Me.btnExecute.Location = New System.Drawing.Point(453, 36)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Size = New System.Drawing.Size(68, 32)
        Me.btnExecute.TabIndex = 101
        Me.btnExecute.Text = "実行"
        Me.btnExecute.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(520, 36)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 32)
        Me.btnPrint.TabIndex = 6
        Me.btnPrint.Text = "印刷"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(553, 85)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 12)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "消費税含まず"
        '
        'dgvSearchResult
        '
        Me.dgvSearchResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSearchResult.Location = New System.Drawing.Point(27, 112)
        Me.dgvSearchResult.Name = "dgvSearchResult"
        Me.dgvSearchResult.RowTemplate.Height = 21
        Me.dgvSearchResult.Size = New System.Drawing.Size(684, 500)
        Me.dgvSearchResult.TabIndex = 8
        '
        '仕入品名検索
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(761, 656)
        Me.Controls.Add(Me.dgvSearchResult)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnExecute)
        Me.Controls.Add(Me.denDateLabel)
        Me.Controls.Add(Me.searchTextBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "仕入品名検索"
        Me.Text = "仕入品名検索"
        CType(Me.dgvSearchResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents searchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents denDateLabel As System.Windows.Forms.Label
    Friend WithEvents btnExecute As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dgvSearchResult As System.Windows.Forms.DataGridView
End Class
