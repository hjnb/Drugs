Imports System.Data.OleDb

Public Class 仕入品名検索

    ''' <summary>
    ''' 行ヘッダーのカレントセルを表す三角マークを非表示に設定する為のクラス。
    ''' </summary>
    ''' <remarks></remarks>
    Public Class dgvRowHeaderCell

        'DataGridViewRowHeaderCell を継承
        Inherits DataGridViewRowHeaderCell

        'DataGridViewHeaderCell.Paint をオーバーライドして行ヘッダーを描画
        Protected Overrides Sub Paint(ByVal graphics As Graphics, ByVal clipBounds As Rectangle, _
           ByVal cellBounds As Rectangle, ByVal rowIndex As Integer, ByVal cellState As DataGridViewElementStates, _
           ByVal value As Object, ByVal formattedValue As Object, ByVal errorText As String, _
           ByVal cellStyle As DataGridViewCellStyle, ByVal advancedBorderStyle As DataGridViewAdvancedBorderStyle, _
           ByVal paintParts As DataGridViewPaintParts)
            '標準セルの描画からセル内容の背景だけ除いた物を描画(-5)
            MyBase.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, _
                     formattedValue, errorText, cellStyle, advancedBorderStyle, _
                     Not DataGridViewPaintParts.ContentBackground)
        End Sub

    End Class

    Private Sub 仕入品名検索_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        '伝票日付ラベル設定
        initDenDateLabel()

        'データグリッドビュー初期設定
        initDgvSearchResult()

        '初期フォーカス
        searchTextBox.Focus()
    End Sub

    ''' <summary>
    ''' 伝票日付ラベル設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initDenDateLabel()
        '最新日、最古日を取得
        Dim cnn As New ADODB.Connection
        cnn.Open(TopForm.DB_Drugs)
        Dim rs As New ADODB.Recordset
        Dim sql As String = "select MIN(Ymd) as MinYmd, MAX(Ymd) as MaxYmd from SiireD"
        rs.Open(sql, cnn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
        If rs.RecordCount > 0 Then
            Dim first As String = Util.convADStrToWarekiStr(Util.checkDBNullValue(rs.Fields("MinYmd").Value))
            Dim last As String = Util.convADStrToWarekiStr(Util.checkDBNullValue(rs.Fields("MaxYmd").Value))
            denDateLabel.Text = first & " ～ " & last
        End If
        rs.Close()
        cnn.Close()
    End Sub

    ''' <summary>
    ''' データグリッドビュー初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initDgvSearchResult()
        Util.EnableDoubleBuffering(dgvSearchResult)

        With dgvSearchResult
            .AllowUserToAddRows = False '行追加禁止
            .AllowUserToResizeColumns = False '列の幅をユーザーが変更できないようにする
            .AllowUserToResizeRows = False '行の高さをユーザーが変更できないようにする
            .AllowUserToDeleteRows = False '行削除禁止
            .BorderStyle = BorderStyle.FixedSingle
            .MultiSelect = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
            .RowHeadersWidth = 35
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 18
            .RowTemplate.Height = 16
            .BackgroundColor = Color.FromKnownColor(KnownColor.Control)
            .RowTemplate.HeaderCell = New dgvRowHeaderCell() '行ヘッダの三角マークを非表示に
            .ShowCellToolTips = False
            .EnableHeadersVisualStyles = False
            '.Font = New Font("ＭＳ Ｐゴシック", 10)
            .ReadOnly = True
        End With
    End Sub

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <param name="inputSearchText">検索文字列</param>
    ''' <remarks></remarks>
    Private Sub displaySearchResult(inputSearchText As String)
        '内容クリア
        dgvSearchResult.Columns.Clear()

        Dim cnn As New ADODB.Connection
        cnn.Open(TopForm.DB_Drugs)
        Dim rs As New ADODB.Recordset
        Dim sql As String = "select autono, Ymd, Nam, Suryo, Tanka, Kingak, Siire from SiireD where Nam Like'%" & inputSearchText & "%' order by Ymd Desc"
        rs.Open(sql, cnn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly)
        Dim da As OleDbDataAdapter = New OleDbDataAdapter()
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, rs, "Search")
        dgvSearchResult.DataSource = ds.Tables("Search")
        cnn.Close()

        '行数
        Dim rowCount As Integer = dgvSearchResult.Rows.Count

        If rowCount > 0 Then
            '日付を和暦に変換
            For Each row As DataGridViewRow In dgvSearchResult.Rows
                row.Cells("Ymd").Value = Util.convADStrToWarekiStr(Util.checkDBNullValue(row.Cells("Ymd").Value))
            Next
        End If

        '幅設定等
        With dgvSearchResult
            '非表示列
            .Columns("autono").Visible = False

            With .Columns("Ymd")
                .HeaderText = "伝票日付"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 80
            End With
            With .Columns("Nam")
                .HeaderText = "品名"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 275
            End With
            With .Columns("Suryo")
                .HeaderText = "数量"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 45
                .HeaderCell.Style.Font = New Font("ＭＳ Ｐゴシック", 9)
                .DefaultCellStyle.Format = "#,0"
            End With
            With .Columns("Tanka")
                .HeaderText = "単価"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 65
                .DefaultCellStyle.Format = "#,0"
            End With
            With .Columns("Kingak")
                .HeaderText = "金額"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 65
                .DefaultCellStyle.Format = "#,0"
            End With
            With .Columns("Siire")
                .HeaderText = "仕入先"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                If rowCount <= 30 Then
                    .Width = 117
                Else
                    .Width = 100
                End If
            End With
        End With

        'フォーカス
        searchTextBox.Focus()
    End Sub

    ''' <summary>
    ''' 実行ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExecute_Click(sender As System.Object, e As System.EventArgs) Handles btnExecute.Click
        '検索文字列
        Dim inputSearchText As String = searchTextBox.Text

        '検索結果表示
        displaySearchResult(inputSearchText)
    End Sub

    ''' <summary>
    ''' CellFormattingイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvSearchResult_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvSearchResult.CellFormatting
        If e.RowIndex >= 1 AndAlso e.ColumnIndex = 1 Then
            '日付のグループ化
            If e.Value = dgvSearchResult("Ymd", e.RowIndex - 1).Value Then
                e.Value = ""
                e.FormattingApplied = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' セルマウスクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvSearchResult_CellMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvSearchResult.CellMouseClick
        If e.RowIndex >= 0 Then
            Dim nam As String = Util.checkDBNullValue(dgvSearchResult("Nam", e.RowIndex).Value)
            searchTextBox.Text = nam
            searchTextBox.Focus()
        End If
    End Sub

    ''' <summary>
    ''' CellPaintingイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvSearchResult_CellPainting(sender As Object, e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvSearchResult.CellPainting
        '行ヘッダーかどうか調べる
        If e.ColumnIndex < 0 AndAlso e.RowIndex >= 0 Then
            'セルを描画する
            e.Paint(e.ClipBounds, DataGridViewPaintParts.All)

            '行番号を描画する範囲を決定する
            'e.AdvancedBorderStyleやe.CellStyle.Paddingは無視しています
            Dim indexRect As Rectangle = e.CellBounds
            indexRect.Inflate(-2, -2)
            '行番号を描画する
            TextRenderer.DrawText(e.Graphics, _
                (e.RowIndex + 1).ToString(), _
                e.CellStyle.Font, _
                indexRect, _
                e.CellStyle.ForeColor, _
                TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
            '描画が完了したことを知らせる
            e.Handled = True
        End If
    End Sub

    ''' <summary>
    ''' 検索文字列ボックスkeyDownイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub searchTextBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles searchTextBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.SelectNextControl(Me.ActiveControl, Not e.Shift, True, True, True)
        End If
    End Sub

    ''' <summary>
    ''' 印刷ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click

    End Sub
End Class