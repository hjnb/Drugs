<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TopForm
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
        Me.btnSiireData = New System.Windows.Forms.Button()
        Me.btnSiiresyukei = New System.Windows.Forms.Button()
        Me.btnSiireJyokyo = New System.Windows.Forms.Button()
        Me.btnSiirekensaku = New System.Windows.Forms.Button()
        Me.btnDB = New System.Windows.Forms.Button()
        Me.btnZaikoKonyu = New System.Windows.Forms.Button()
        Me.btnZaikoNyuryoku = New System.Windows.Forms.Button()
        Me.btnZaikoM = New System.Windows.Forms.Button()
        Me.rbtnPreview = New System.Windows.Forms.RadioButton()
        Me.rbtnPrintout = New System.Windows.Forms.RadioButton()
        Me.topPicture = New System.Windows.Forms.PictureBox()
        CType(Me.topPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSiireData
        '
        Me.btnSiireData.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnSiireData.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSiireData.Location = New System.Drawing.Point(46, 44)
        Me.btnSiireData.Name = "btnSiireData"
        Me.btnSiireData.Size = New System.Drawing.Size(246, 79)
        Me.btnSiireData.TabIndex = 0
        Me.btnSiireData.Text = "仕入データ入力"
        Me.btnSiireData.UseVisualStyleBackColor = False
        '
        'btnSiiresyukei
        '
        Me.btnSiiresyukei.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnSiiresyukei.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSiiresyukei.Location = New System.Drawing.Point(46, 122)
        Me.btnSiiresyukei.Name = "btnSiiresyukei"
        Me.btnSiiresyukei.Size = New System.Drawing.Size(246, 79)
        Me.btnSiiresyukei.TabIndex = 1
        Me.btnSiiresyukei.Text = "仕入集計"
        Me.btnSiiresyukei.UseVisualStyleBackColor = False
        '
        'btnSiireJyokyo
        '
        Me.btnSiireJyokyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnSiireJyokyo.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSiireJyokyo.Location = New System.Drawing.Point(46, 201)
        Me.btnSiireJyokyo.Name = "btnSiireJyokyo"
        Me.btnSiireJyokyo.Size = New System.Drawing.Size(246, 79)
        Me.btnSiireJyokyo.TabIndex = 2
        Me.btnSiireJyokyo.Text = "仕入状況"
        Me.btnSiireJyokyo.UseVisualStyleBackColor = False
        '
        'btnSiirekensaku
        '
        Me.btnSiirekensaku.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnSiirekensaku.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSiirekensaku.Location = New System.Drawing.Point(46, 280)
        Me.btnSiirekensaku.Name = "btnSiirekensaku"
        Me.btnSiirekensaku.Size = New System.Drawing.Size(246, 79)
        Me.btnSiirekensaku.TabIndex = 3
        Me.btnSiirekensaku.Text = "仕入品名検索"
        Me.btnSiirekensaku.UseVisualStyleBackColor = False
        '
        'btnDB
        '
        Me.btnDB.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnDB.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDB.Location = New System.Drawing.Point(291, 280)
        Me.btnDB.Name = "btnDB"
        Me.btnDB.Size = New System.Drawing.Size(246, 79)
        Me.btnDB.TabIndex = 7
        Me.btnDB.Text = "ＤＢ整理"
        Me.btnDB.UseVisualStyleBackColor = False
        '
        'btnZaikoKonyu
        '
        Me.btnZaikoKonyu.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnZaikoKonyu.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnZaikoKonyu.Location = New System.Drawing.Point(291, 201)
        Me.btnZaikoKonyu.Name = "btnZaikoKonyu"
        Me.btnZaikoKonyu.Size = New System.Drawing.Size(246, 79)
        Me.btnZaikoKonyu.TabIndex = 6
        Me.btnZaikoKonyu.Text = "在庫購入価"
        Me.btnZaikoKonyu.UseVisualStyleBackColor = False
        '
        'btnZaikoNyuryoku
        '
        Me.btnZaikoNyuryoku.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnZaikoNyuryoku.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnZaikoNyuryoku.Location = New System.Drawing.Point(291, 122)
        Me.btnZaikoNyuryoku.Name = "btnZaikoNyuryoku"
        Me.btnZaikoNyuryoku.Size = New System.Drawing.Size(246, 79)
        Me.btnZaikoNyuryoku.TabIndex = 5
        Me.btnZaikoNyuryoku.Text = "在庫入力"
        Me.btnZaikoNyuryoku.UseVisualStyleBackColor = False
        '
        'btnZaikoM
        '
        Me.btnZaikoM.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnZaikoM.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnZaikoM.Location = New System.Drawing.Point(291, 44)
        Me.btnZaikoM.Name = "btnZaikoM"
        Me.btnZaikoM.Size = New System.Drawing.Size(246, 79)
        Me.btnZaikoM.TabIndex = 4
        Me.btnZaikoM.Text = "在庫マスタ"
        Me.btnZaikoM.UseVisualStyleBackColor = False
        '
        'rbtnPreview
        '
        Me.rbtnPreview.AutoSize = True
        Me.rbtnPreview.Location = New System.Drawing.Point(619, 216)
        Me.rbtnPreview.Name = "rbtnPreview"
        Me.rbtnPreview.Size = New System.Drawing.Size(63, 16)
        Me.rbtnPreview.TabIndex = 8
        Me.rbtnPreview.TabStop = True
        Me.rbtnPreview.Text = "ﾌﾟﾚﾋﾞｭｰ"
        Me.rbtnPreview.UseVisualStyleBackColor = True
        '
        'rbtnPrintout
        '
        Me.rbtnPrintout.AutoSize = True
        Me.rbtnPrintout.Location = New System.Drawing.Point(619, 245)
        Me.rbtnPrintout.Name = "rbtnPrintout"
        Me.rbtnPrintout.Size = New System.Drawing.Size(47, 16)
        Me.rbtnPrintout.TabIndex = 9
        Me.rbtnPrintout.TabStop = True
        Me.rbtnPrintout.Text = "印刷"
        Me.rbtnPrintout.UseVisualStyleBackColor = True
        '
        'topPicture
        '
        Me.topPicture.Location = New System.Drawing.Point(592, 44)
        Me.topPicture.Name = "topPicture"
        Me.topPicture.Size = New System.Drawing.Size(131, 123)
        Me.topPicture.TabIndex = 10
        Me.topPicture.TabStop = False
        '
        'TopForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(776, 656)
        Me.Controls.Add(Me.topPicture)
        Me.Controls.Add(Me.rbtnPrintout)
        Me.Controls.Add(Me.rbtnPreview)
        Me.Controls.Add(Me.btnDB)
        Me.Controls.Add(Me.btnZaikoKonyu)
        Me.Controls.Add(Me.btnZaikoNyuryoku)
        Me.Controls.Add(Me.btnZaikoM)
        Me.Controls.Add(Me.btnSiirekensaku)
        Me.Controls.Add(Me.btnSiireJyokyo)
        Me.Controls.Add(Me.btnSiiresyukei)
        Me.Controls.Add(Me.btnSiireData)
        Me.Name = "TopForm"
        Me.Text = "薬剤管理"
        CType(Me.topPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSiireData As System.Windows.Forms.Button
    Friend WithEvents btnSiiresyukei As System.Windows.Forms.Button
    Friend WithEvents btnSiireJyokyo As System.Windows.Forms.Button
    Friend WithEvents btnSiirekensaku As System.Windows.Forms.Button
    Friend WithEvents btnDB As System.Windows.Forms.Button
    Friend WithEvents btnZaikoKonyu As System.Windows.Forms.Button
    Friend WithEvents btnZaikoNyuryoku As System.Windows.Forms.Button
    Friend WithEvents btnZaikoM As System.Windows.Forms.Button
    Friend WithEvents rbtnPreview As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnPrintout As System.Windows.Forms.RadioButton
    Friend WithEvents topPicture As System.Windows.Forms.PictureBox

End Class
