Imports System.Data.OleDb

Public Class 仕入データ入力

    '検索タイプ
    Private Const SEARCH_TYPE_COD As Integer = 1 'カナ検索用
    Private Const SEARCH_TYPE_NAM As Integer = 2 '品名検索用

    '消費税率配列
    Private taxArray() As String = {"0.05", "0.08", "0.10"}

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

    ''' <summary>
    ''' loadイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 仕入データ入力_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        '日付ボックスのエンターキー押下イベント用
        YmdBox.canEnterKeyDown = True

        '現在日付セット
        YmdBox.setADStr(Today.ToString("yyyy/MM/dd"))

        '消費税率ボックス初期設定
        initTaxBox()

        '仕入先ボックス初期設定
        initSiireBox()

        'データグリッドビュー（右上）の初期設定
        initDgvSearch()

        'データグリッドビュー（下）の初期設定
        initDgvSiire()
    End Sub

    ''' <summary>
    ''' 消費税率ボックス初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initTaxBox()
        '選択項目セット
        taxBox.Items.Clear()
        taxBox.Items.AddRange(taxArray)

        'iniファイルから読み込み、初期選択値を設定
        Dim tax As String = Util.getIniString("System", "Tax", TopForm.iniFilePath)
        taxBox.SelectedText = tax
    End Sub

    ''' <summary>
    ''' 仕入先ボックス初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initSiireBox()
        siireBox.Items.Clear()
        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Drugs)
        Dim sql As String = "select * from EtcM order by Seq"
        Dim rs As New ADODB.Recordset
        rs.Open(sql, cn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic)
        While Not rs.EOF
            Dim txt As String = Util.checkDBNullValue(rs.Fields("Text").Value)
            siireBox.Items.Add(txt)
            rs.MoveNext()
        End While
        rs.Close()
        cn.Close()
    End Sub

    ''' <summary>
    ''' データグリッドビュー（右上）の初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initDgvSearch()
        Util.EnableDoubleBuffering(dgvSearch)

        With dgvSearch
            .AllowUserToAddRows = False '行追加禁止
            .AllowUserToResizeColumns = False '列の幅をユーザーが変更できないようにする
            .AllowUserToResizeRows = False '行の高さをユーザーが変更できないようにする
            .AllowUserToDeleteRows = False '行削除禁止
            .BorderStyle = BorderStyle.FixedSingle
            .MultiSelect = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .RowHeadersVisible = False
            .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .RowTemplate.Height = 18
            .BackgroundColor = Color.FromKnownColor(KnownColor.Control)
            .ShowCellToolTips = False
            .EnableHeadersVisualStyles = False
            .Font = New Font("ＭＳ Ｐゴシック", 10)
            .ReadOnly = True
            .ScrollBars = ScrollBars.None
        End With
    End Sub

    ''' <summary>
    ''' データグリッドビュー（下）の初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initDgvSiire()
        Util.EnableDoubleBuffering(dgvSiire)

        With dgvSiire
            .AllowUserToAddRows = False '行追加禁止
            .AllowUserToResizeColumns = False '列の幅をユーザーが変更できないようにする
            .AllowUserToResizeRows = False '行の高さをユーザーが変更できないようにする
            .AllowUserToDeleteRows = False '行削除禁止
            .BorderStyle = BorderStyle.FixedSingle
            .MultiSelect = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .RowTemplate.Height = 18
            .BackgroundColor = Color.FromKnownColor(KnownColor.Control)
            .RowTemplate.HeaderCell = New dgvRowHeaderCell() '行ヘッダの三角マークを非表示に
            .ShowCellToolTips = False
            .EnableHeadersVisualStyles = False
            .Font = New Font("ＭＳ Ｐゴシック", 10)
            .ReadOnly = True
        End With
    End Sub

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub displayDgvSearch(searchStr As String, searchType As Integer)
        '内容クリア
        dgvSearch.Columns.Clear()

        'データ取得、表示
        Dim sql As String = ""
        If searchType = SEARCH_TYPE_COD Then
            'カナ検索
            sql = "select distinct Ymd, Siire, Cod, Nam, Tanka from SiireD where Cod='" & searchStr & "' order by Ymd Desc"
        ElseIf searchType = SEARCH_TYPE_NAM Then
            '品名検索
            sql = "select distinct Ymd, Siire, Cod, Nam, Tanka from SiireD where Nam Like '%" & searchStr & "%' order by Ymd Desc"
        Else
            Return
        End If
        Dim cnn As New ADODB.Connection
        cnn.Open(TopForm.DB_Drugs)
        Dim rs As New ADODB.Recordset
        rs.Open(sql, cnn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly)
        Dim da As OleDbDataAdapter = New OleDbDataAdapter()
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, rs, "Search")
        dgvSearch.DataSource = ds.Tables("Search")
        cnn.Close()

        '幅設定等
        With dgvSearch
            With .Columns("Ymd")
                .HeaderText = "日付"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 90
            End With
            With .Columns("Siire")
                .HeaderText = "仕入先"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 115
            End With
            With .Columns("Cod")
                .HeaderText = "カナ"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 35
                .HeaderCell.Style.Font = New Font("ＭＳ Ｐゴシック", 9)
            End With
            With .Columns("Nam")
                .HeaderText = "品名"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 285
            End With
            With .Columns("Tanka")
                .HeaderText = "単価"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 70
                .DefaultCellStyle.Format = "#,0"
            End With
        End With
    End Sub

    ''' <summary>
    ''' 仕入データ表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub displayDgvSiire(ymd As String)
        '内容クリア
        dgvSiire.Columns.Clear()

        Dim cnn As New ADODB.Connection
        cnn.Open(TopForm.DB_Drugs)
        Dim rs As New ADODB.Recordset
        Dim sql As String = "select Ymd, Siire, Denno, Cod, Nam, Suryo, Tanka, Kingak, Zei, Gokei from SiireD where Ymd='" & ymd & "' order by Denno, Autono Desc"
        rs.Open(sql, cnn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly)
        Dim da As OleDbDataAdapter = New OleDbDataAdapter()
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, rs, "Siire")
        dgvSiire.DataSource = ds.Tables("Siire")
        cnn.Close()

        '行数
        Dim rowCount As Integer = dgvSiire.Rows.Count

        '幅設定等
        With dgvSiire
            With .Columns("Ymd")
                .HeaderText = "日付"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 90
            End With
            With .Columns("Siire")
                .HeaderText = "仕入先"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 115
            End With
            With .Columns("Denno")
                .HeaderText = "伝票No."
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 70
            End With
            With .Columns("Cod")
                .HeaderText = "カナ"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 40
                .HeaderCell.Style.Font = New Font("ＭＳ Ｐゴシック", 9)
            End With
            With .Columns("Nam")
                .HeaderText = "品名"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 290
            End With
            With .Columns("Suryo")
                .HeaderText = "数量"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 40
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
            With .Columns("Zei")
                .HeaderText = "消費税"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 65
                .DefaultCellStyle.Format = "#,0"
            End With
            With .Columns("Gokei")
                .HeaderText = "合計"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
                If rowCount > 20 Then
                    .Width = 75
                Else
                    .Width = 92
                End If
                .DefaultCellStyle.Format = "#,0"
            End With
        End With
    End Sub

    ''' <summary>
    ''' カナボックスkeyDownイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub codBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles codBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim inputStr As String = codBox.Text
            If inputStr <> "" Then
                displayDgvSearch(inputStr, SEARCH_TYPE_COD)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 品名ボックスkeyDownイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub namBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles namBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim inputStr As String = namBox.Text
            If inputStr <> "" Then
                displayDgvSearch(inputStr, SEARCH_TYPE_NAM)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 日付ボックスでエンターキー押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub YmdBox_keyDownEnterOrDown(sender As Object, e As System.EventArgs) Handles YmdBox.keyDownEnterOrDown
        Dim ymd As String = YmdBox.getADStr()
        displayDgvSiire(ymd)
    End Sub

    ''' <summary>
    ''' データグリッドビュー（右上）セルマウスクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvSearch_CellMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvSearch.CellMouseClick
        If e.RowIndex >= 0 Then
            Dim cod As String = Util.checkDBNullValue(dgvSearch("Cod", e.RowIndex).Value) 'カナ
            Dim nam As String = Util.checkDBNullValue(dgvSearch("Nam", e.RowIndex).Value) '品名
            Dim tanka As String = Util.checkDBNullValue(dgvSearch("Tanka", e.RowIndex).FormattedValue) '単価

            '各ボックスへセット
            codBox.Text = cod
            namBox.Text = nam
            tankaBox.Text = tanka
        End If
    End Sub

    ''' <summary>
    ''' データグリッドビュー（下）セルマウスクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvSiire_CellMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvSiire.CellMouseClick
        If e.RowIndex >= 0 Then
            Dim siire As String = Util.checkDBNullValue(dgvSiire("Siire", e.RowIndex).Value) '仕入先
            Dim denno As String = Util.checkDBNullValue(dgvSiire("Denno", e.RowIndex).Value) '伝票No.
            Dim cod As String = Util.checkDBNullValue(dgvSiire("Cod", e.RowIndex).Value) 'カナ
            Dim nam As String = Util.checkDBNullValue(dgvSiire("Nam", e.RowIndex).Value) '品名
            Dim suryo As String = Util.checkDBNullValue(dgvSiire("Suryo", e.RowIndex).FormattedValue) '数量
            Dim tanka As String = Util.checkDBNullValue(dgvSiire("Tanka", e.RowIndex).FormattedValue) '単価

            '各ボックスへセット
            siireBox.Text = siire
            dennoBox.Text = denno
            codBox.Text = cod
            namBox.Text = nam
            suryoBox.Text = suryo
            tankaBox.Text = tanka
        End If
    End Sub

    ''' <summary>
    ''' CellPaintingイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvSiire_CellPainting(sender As Object, e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvSiire.CellPainting
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
End Class