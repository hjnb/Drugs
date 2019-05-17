Imports System.Data.OleDb
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices

Public Class 仕入データ入力

    '検索タイプ
    Private Const SEARCH_TYPE_COD As Integer = 1 'カナ検索用
    Private Const SEARCH_TYPE_NAM As Integer = 2 '品名検索用

    '消費税率配列
    Private taxArray() As String = {"0.05", "0.08", "0.10"}

    'テキストボックスのマウスダウンイベント制御用
    Private mdFlag As Boolean = False

    '選択行インデックス保持用
    Private selectedRowIndex As Integer = -1

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

        '消費税率ボックス初期設定
        initTaxBox()

        '仕入先ボックス初期設定
        initSiireBox()

        'データグリッドビュー（右上）の初期設定
        initDgvSearch()

        'データグリッドビュー（下）の初期設定
        initDgvSiire()

        '入力テキストボックス
        initInputTextBox()

        '現在日付セット、現在日付データ表示
        YmdBox.setADStr(Today.ToString("yyyy/MM/dd"))
        displayDgvSiire(YmdBox.getADStr())

        '初期フォーカス
        codBox.Focus()
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

        taxBox.ImeMode = Windows.Forms.ImeMode.Disable
    End Sub

    ''' <summary>
    ''' 仕入先ボックス初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initSiireBox()
        siireBox.ImeMode = Windows.Forms.ImeMode.Hiragana
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
    ''' 入力テキストボックス初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initInputTextBox()
        '伝票No.
        dennoBox.ImeMode = Windows.Forms.ImeMode.Disable

        'カナ
        codBox.ImeMode = Windows.Forms.ImeMode.Hiragana

        '品名
        namBox.ImeMode = Windows.Forms.ImeMode.Hiragana

        '数量
        suryoBox.ImeMode = Windows.Forms.ImeMode.Disable

        '単価
        tankaBox.ImeMode = Windows.Forms.ImeMode.Disable

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
        Dim dt As DataTable = ds.Tables("Search")
        If dt.Rows.Count >= 2 Then
            For i As Integer = dt.Rows.Count - 1 To 1 Step -1
                If dt.Rows(i).Item("Nam") = dt.Rows(i - 1).Item("Nam") Then
                    dt.Rows(i).Delete()
                End If
            Next
        End If
        dgvSearch.DataSource = dt
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

        dgvSearch.Focus()
    End Sub

    ''' <summary>
    ''' 仕入データ表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub displayDgvSiire(ymd As String)
        '入力テキストクリア
        codBox.Text = ""
        namBox.Text = ""
        suryoBox.Text = ""
        tankaBox.Text = ""

        '内容クリア
        dgvSiire.Columns.Clear()

        Dim cnn As New ADODB.Connection
        cnn.Open(TopForm.DB_Drugs)
        Dim rs As New ADODB.Recordset
        Dim sql As String = "select autono, Ymd, Siire, Denno, Cod, Nam, Suryo, Tanka, Kingak, Zei, Gokei from SiireD where Ymd='" & ymd & "' order by Autono Desc"
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
            '非表示列
            .Columns("autono").Visible = False

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

        'カナボックスにフォーカス
        codBox.Focus()

        '選択行インデックス（保持用）を初期値に
        selectedRowIndex = -1
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
            Else
                Me.SelectNextControl(Me.ActiveControl, Not e.Shift, True, True, True)
            End If
        ElseIf e.KeyCode = Keys.Up Then
            dennoBox.Focus()
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
        ElseIf e.KeyCode = Keys.Up Then
            codBox.Focus()
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
    ''' データグリッドビュー（右上）cellFormattingイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvSearch_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvSearch.CellFormatting
        '日付列の値を和暦に変換
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = 0 Then
            e.Value = Util.convADStrToWarekiStr(e.Value)
            e.FormattingApplied = True
        End If
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

            '数量を1でセット
            suryoBox.Text = "1"
            suryoBox.Focus()
        End If
    End Sub

    ''' <summary>
    ''' データグリッドビュー（下）cellFormattingイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvSiire_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvSiire.CellFormatting
        '日付列の値を和暦に変換
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = 1 Then
            e.Value = Util.convADStrToWarekiStr(e.Value)
            e.FormattingApplied = True
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

            selectedRowIndex = e.RowIndex
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

    ''' <summary>
    ''' 入力テキストボックスエンターイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub textBox_Enter(sender As Object, e As System.EventArgs) Handles dennoBox.Enter, codBox.Enter, namBox.Enter, suryoBox.Enter, tankaBox.Enter
        Dim tb As TextBox = CType(sender, TextBox)
        tb.SelectAll()
        mdFlag = True
    End Sub

    ''' <summary>
    ''' 入力テキストボックスマウスダウンイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub textBox_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles dennoBox.MouseDown, codBox.MouseDown, namBox.MouseDown, suryoBox.MouseDown, tankaBox.MouseDown
        If mdFlag = True Then
            Dim tb As TextBox = CType(sender, TextBox)
            tb.SelectAll()
            mdFlag = False
        End If
    End Sub

    ''' <summary>
    ''' データグリッドビュー右上でエンターキーkeyDownイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvSearch_keyDownEnter(sender As Object, e As System.EventArgs) Handles dgvSearch.keyDownEnter
        If Not IsNothing(dgvSearch.CurrentCell) Then
            Dim rowIndex As Integer = dgvSearch.CurrentCell.RowIndex
            Dim cod As String = Util.checkDBNullValue(dgvSearch("Cod", rowIndex).Value) 'カナ
            Dim nam As String = Util.checkDBNullValue(dgvSearch("Nam", rowIndex).Value) '品名
            Dim tanka As String = Util.checkDBNullValue(dgvSearch("Tanka", rowIndex).FormattedValue) '単価

            '各ボックスへセット
            codBox.Text = cod
            namBox.Text = nam
            tankaBox.Text = tanka

            '数量を1でセット
            suryoBox.Text = "1"
            suryoBox.Focus()
        End If
    End Sub

    Private Sub siireBox_GotFocus(sender As Object, e As System.EventArgs) Handles siireBox.GotFocus
        siireBox.DroppedDown = True
    End Sub

    ''' <summary>
    ''' 仕入先ボックスマウスクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub siireBox_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles siireBox.MouseClick
        If siireBox.Text = "" Then
            siireBox.DroppedDown = True
        End If
    End Sub

    ''' <summary>
    ''' 入力テキストボックスkeydownイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub textBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles siireBox.KeyDown, dennoBox.KeyDown, suryoBox.KeyDown, tankaBox.KeyDown
        Dim name As String = sender.Name
        If e.KeyCode = Keys.Enter Then
            Me.SelectNextControl(Me.ActiveControl, Not e.Shift, True, True, True)
        ElseIf e.KeyCode = Keys.Up Then
            If name = "dennoBox" Then
                siireBox.Focus()
            ElseIf name = "suryoBox" Then
                namBox.Focus()
            ElseIf name = "tankaBox" Then
                suryoBox.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 追加ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd.Click
        '入力内容
        Dim ymd As String = YmdBox.getADStr() '日付
        Dim tax As String = taxBox.Text '消費税率
        Dim siire As String = siireBox.Text '仕入先
        Dim denno As String = dennoBox.Text '伝票No.
        Dim cod As String = codBox.Text 'カナ(ｺｰﾄﾞ)
        Dim nam As String = namBox.Text '品名
        Dim suryo As String = suryoBox.Text.Replace(",", "") '数量
        Dim tanka As String = tankaBox.Text.Replace(",", "") '単価

        '入力チェック
        If tax = "" Then
            MsgBox("消費税率を選択して下さい。", MsgBoxStyle.Exclamation)
            taxBox.Focus()
            taxBox.DroppedDown = True
            Return
        End If
        If Not System.Text.RegularExpressions.Regex.IsMatch(tax, "^\d+(\.\d+)?$") Then
            MsgBox("消費税率を正しく入力して下さい。(例：0.08)", MsgBoxStyle.Exclamation)
            taxBox.Focus()
            Return
        End If
        If siire = "" Then
            MsgBox("仕入先を選択して下さい。", MsgBoxStyle.Exclamation)
            siireBox.Focus()
            siireBox.DroppedDown = True
            Return
        End If
        If denno = "" Then
            MsgBox("伝票Noを入力して下さい。", MsgBoxStyle.Exclamation)
            dennoBox.Focus()
            Return
        End If
        If cod = "" Then
            MsgBox("ｺｰﾄﾞを入力して下さい。", MsgBoxStyle.Exclamation)
            codBox.Focus()
            Return
        End If
        If nam = "" Then
            MsgBox("品名を入力して下さい。", MsgBoxStyle.Exclamation)
            namBox.Focus()
            Return
        End If
        If suryo = "" Then
            MsgBox("数量を入力して下さい。", MsgBoxStyle.Exclamation)
            suryoBox.Focus()
            Return
        End If
        If Not System.Text.RegularExpressions.Regex.IsMatch(suryo, "^-?\d+$") Then
            MsgBox("数量は数値を入力して下さい。", MsgBoxStyle.Exclamation)
            suryoBox.Focus()
            Return
        End If
        If tanka = "" Then
            MsgBox("単価を入力して下さい。", MsgBoxStyle.Exclamation)
            tankaBox.Focus()
            Return
        End If
        If Not System.Text.RegularExpressions.Regex.IsMatch(tanka, "^-?\d+$") Then
            MsgBox("単価は数値を入力して下さい。", MsgBoxStyle.Exclamation)
            suryoBox.Focus()
            Return
        End If

        '登録データ作成
        Dim kingak As Decimal = CDec(tanka) * CDec(suryo) '金額
        Dim zei As Decimal = Math.Round(kingak * CDec(tax), 0, MidpointRounding.AwayFromZero) '消費税
        Dim gokei As Decimal = kingak + zei '合計

        '登録
        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Drugs)
        Dim rs As New ADODB.Recordset
        rs.Open("SiireD", cn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic)
        rs.AddNew()
        rs.Fields("Ymd").Value = ymd
        rs.Fields("Siire").Value = siire
        rs.Fields("Denno").Value = denno
        rs.Fields("Cod").Value = cod
        rs.Fields("Nam").Value = nam
        rs.Fields("Suryo").Value = suryo
        rs.Fields("Tanka").Value = tanka
        rs.Fields("Kingak").Value = kingak
        rs.Fields("Zei").Value = zei
        rs.Fields("Gokei").Value = gokei
        rs.Update()

        rs.Close()
        cn.Close()

        '検索結果クリア
        dgvSearch.Columns.Clear()

        '再表示
        displayDgvSiire(ymd)
    End Sub

    ''' <summary>
    ''' 変更ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnChange_Click(sender As System.Object, e As System.EventArgs) Handles btnChange.Click
        '変更対象行を未選択
        If selectedRowIndex = -1 Then
            MsgBox("変更ﾃﾞｰﾀが選択されていません。", MsgBoxStyle.Exclamation)
            Return
        End If

        '入力内容取得
        Dim ymd As String = YmdBox.getADStr() '日付
        Dim tax As String = taxBox.Text '消費税率
        Dim siire As String = siireBox.Text '仕入先
        Dim denno As String = dennoBox.Text '伝票No.
        Dim cod As String = codBox.Text 'カナ(ｺｰﾄﾞ)
        Dim nam As String = namBox.Text '品名
        Dim suryo As String = suryoBox.Text.Replace(",", "") '数量
        Dim tanka As String = tankaBox.Text.Replace(",", "") '単価

        '入力チェック
        If tax = "" Then
            MsgBox("消費税率を選択して下さい。", MsgBoxStyle.Exclamation)
            taxBox.Focus()
            taxBox.DroppedDown = True
            Return
        End If
        If Not System.Text.RegularExpressions.Regex.IsMatch(tax, "^\d+(\.\d+)?$") Then
            MsgBox("消費税率を正しく入力して下さい。(例：0.08)", MsgBoxStyle.Exclamation)
            taxBox.Focus()
            Return
        End If
        If siire = "" Then
            MsgBox("仕入先を選択して下さい。", MsgBoxStyle.Exclamation)
            siireBox.Focus()
            siireBox.DroppedDown = True
            Return
        End If
        If denno = "" Then
            MsgBox("伝票Noを入力して下さい。", MsgBoxStyle.Exclamation)
            dennoBox.Focus()
            Return
        End If
        If cod = "" Then
            MsgBox("ｺｰﾄﾞを入力して下さい。", MsgBoxStyle.Exclamation)
            codBox.Focus()
            Return
        End If
        If nam = "" Then
            MsgBox("品名を入力して下さい。", MsgBoxStyle.Exclamation)
            namBox.Focus()
            Return
        End If
        If suryo = "" Then
            MsgBox("数量を入力して下さい。", MsgBoxStyle.Exclamation)
            suryoBox.Focus()
            Return
        End If
        If Not System.Text.RegularExpressions.Regex.IsMatch(suryo, "^-?\d+$") Then
            MsgBox("数量は数値を入力して下さい。", MsgBoxStyle.Exclamation)
            suryoBox.Focus()
            Return
        End If
        If tanka = "" Then
            MsgBox("単価を入力して下さい。", MsgBoxStyle.Exclamation)
            tankaBox.Focus()
            Return
        End If
        If Not System.Text.RegularExpressions.Regex.IsMatch(tanka, "^-?\d+$") Then
            MsgBox("単価は数値を入力して下さい。", MsgBoxStyle.Exclamation)
            suryoBox.Focus()
            Return
        End If

        '金額、消費税、合計計算
        Dim kingak As Decimal = CDec(tanka) * CDec(suryo) '金額
        Dim zei As Decimal = Math.Round(kingak * CDec(tax), 0, MidpointRounding.AwayFromZero) '消費税
        Dim gokei As Decimal = kingak + zei '合計

        '変更行のautono取得
        Dim autono As Integer = dgvSiire("autono", selectedRowIndex).Value

        '登録
        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Drugs)
        Dim rs As New ADODB.Recordset
        Dim sql As String = "select * from SiireD where autono=" & autono
        rs.Open(sql, cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)
        If rs.RecordCount = 1 Then
            '更新
            rs.Fields("Ymd").Value = ymd
            rs.Fields("Siire").Value = siire
            rs.Fields("Denno").Value = denno
            rs.Fields("Cod").Value = cod
            rs.Fields("Nam").Value = nam
            rs.Fields("Suryo").Value = suryo
            rs.Fields("Tanka").Value = tanka
            rs.Fields("Kingak").Value = kingak
            rs.Fields("Zei").Value = zei
            rs.Fields("Gokei").Value = gokei
            rs.Update()

            rs.Close()
            cn.Close()

            '検索結果クリア
            dgvSearch.Columns.Clear()

            '再表示
            displayDgvSiire(ymd)
        Else
            rs.Close()
            cn.Close()
            MsgBox("選択行のデータが存在しません。", MsgBoxStyle.Exclamation)
            Return
        End If
    End Sub

    ''' <summary>
    ''' 削除ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click
        '削除対象行を未選択
        If selectedRowIndex = -1 Then
            MsgBox("削除ﾃﾞｰﾀが選択されていません。", MsgBoxStyle.Exclamation)
            Return
        End If

        Dim result As DialogResult = MessageBox.Show("削除してよろしいですか？", "削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = Windows.Forms.DialogResult.Yes Then
            '削除行のautono取得
            Dim autono As Integer = dgvSiire("autono", selectedRowIndex).Value

            '削除
            Dim cn As New ADODB.Connection()
            cn.Open(TopForm.DB_Drugs)
            Dim cmd As New ADODB.Command()
            cmd.ActiveConnection = cn
            cmd.CommandText = "delete from SiireD where autono = " & autono
            cmd.Execute()

            cn.Close()

            '検索結果クリア
            dgvSearch.Columns.Clear()

            '再表示
            displayDgvSiire(YmdBox.getADStr())
        End If
    End Sub

    ''' <summary>
    ''' 印刷ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click
        Dim ym As String = YmdBox.getADYmStr() '年月(yyyy/MM)
        Dim year As Integer = CInt(ym.Split("/")(0)) '年
        Dim month As Integer = CInt(ym.Split("/")(1)) '月
        Dim daysInMonth As Integer = DateTime.DaysInMonth(year, month) '月の日数
        Dim fromYmd As String = ym & "/01" 'from日付
        Dim toYmd As String = ym & "/" & daysInMonth 'to日付
        Dim wareki As String = Util.convADStrToWarekiStr(fromYmd)
        Dim ymFormattedStr As String = Util.getKanji(wareki) & " " & CInt(wareki.Substring(1, 2)) & " 年 " & CInt(wareki.Split("/")(1)) & " 月"

        '対象年月のデータ取得
        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Drugs)
        Dim rs As New ADODB.Recordset
        Dim sql As String = "select * from SiireD where ('" & fromYmd & "' <= Ymd and Ymd <= '" & toYmd & "') order by Siire, Ymd, Denno,autono"
        rs.Open(sql, cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)
        If rs.RecordCount <= 0 Then
            rs.Close()
            cn.Close()
            MsgBox("該当がありません。", MsgBoxStyle.Exclamation)
            Return
        End If

        '書き込みデータ作成
        Dim dataList As New List(Of String(,))
        Dim dataArray(63, 8) As String
        Dim rowIndex As Integer = 0
        Dim tmpYmd As String = ""
        Dim tmpDenno As String = ""
        Dim tmpSiire As String = ""
        Dim denGokei() As Integer = {0, 0, 0}
        Dim dateGokei() As Integer = {0, 0, 0}
        Dim siireGokei() As Integer = {0, 0, 0}
        Dim page As Integer = 0
        While Not rs.EOF
            If rowIndex = 64 Then
                dataList.Add(dataArray.Clone())
                Array.Clear(dataArray, 0, dataArray.Length)
                page += 1
                'ヘッダーテキスト部分作成
                dataArray(0, 0) = "仕入先:"
                dataArray(0, 1) = tmpSiire
                dataArray(0, 4) = ymFormattedStr
                dataArray(0, 8) = page & " 頁"
                dataArray(1, 0) = "納入日"
                dataArray(1, 1) = "伝票No."
                dataArray(1, 2) = "ｺｰﾄﾞ"
                dataArray(1, 3) = "薬品名"
                dataArray(1, 4) = "数量"
                dataArray(1, 5) = "購入価"
                dataArray(1, 6) = "合計"
                dataArray(1, 7) = "消費税"
                dataArray(1, 8) = "税込合計"
                rowIndex = 2
            End If

            Dim siire As String = Util.checkDBNullValue(rs.Fields("Siire").Value)
            Dim ymd As String = Util.convADStrToWarekiStr(Util.checkDBNullValue(rs.Fields("Ymd").Value))
            Dim denno As String = Util.checkDBNullValue(rs.Fields("Denno").Value)
            Dim cod As String = Util.checkDBNullValue(rs.Fields("Cod").Value)
            Dim nam As String = Util.checkDBNullValue(rs.Fields("Nam").Value)
            Dim suryo As Integer = rs.Fields("Suryo").Value
            Dim tanka As Integer = rs.Fields("Tanka").Value
            Dim kingak As Integer = rs.Fields("Kingak").Value
            Dim zei As Integer = rs.Fields("Zei").Value
            Dim gokei As Integer = rs.Fields("Gokei").Value
            If siire <> tmpSiire Then '仕入先が変わる場合、伝票計データと納入日計データと仕入先計データ追加
                page += 1
                If page = 1 Then
                    'ヘッダーテキスト部分作成
                    dataArray(0, 0) = "仕入先:"
                    dataArray(0, 1) = siire
                    dataArray(0, 4) = ymFormattedStr
                    dataArray(0, 8) = page & " 頁"
                    dataArray(1, 0) = "納入日"
                    dataArray(1, 1) = "伝票No."
                    dataArray(1, 2) = "ｺｰﾄﾞ"
                    dataArray(1, 3) = "薬品名"
                    dataArray(1, 4) = "数量"
                    dataArray(1, 5) = "購入価"
                    dataArray(1, 6) = "合計"
                    dataArray(1, 7) = "消費税"
                    dataArray(1, 8) = "税込合計"
                    rowIndex = 2

                    tmpSiire = siire
                    tmpYmd = ymd
                    tmpDenno = denno
                Else
                    '伝票計データ追加
                    dataArray(rowIndex, 3) = " * 伝票計 * " & tmpDenno
                    dataArray(rowIndex, 6) = denGokei(0)
                    dataArray(rowIndex, 7) = denGokei(1)
                    dataArray(rowIndex, 8) = denGokei(2)

                    '更新
                    rowIndex += 1
                    tmpDenno = denno
                    denGokei(0) = 0
                    denGokei(1) = 0
                    denGokei(2) = 0

                    If rowIndex = 64 Then
                        dataList.Add(dataArray.Clone())
                        Array.Clear(dataArray, 0, dataArray.Length)
                        page += 1
                        'ヘッダーテキスト部分作成
                        dataArray(0, 0) = "仕入先:"
                        dataArray(0, 1) = tmpSiire
                        dataArray(0, 4) = ymFormattedStr
                        dataArray(0, 8) = page & " 頁"
                        dataArray(1, 0) = "納入日"
                        dataArray(1, 1) = "伝票No."
                        dataArray(1, 2) = "ｺｰﾄﾞ"
                        dataArray(1, 3) = "薬品名"
                        dataArray(1, 4) = "数量"
                        dataArray(1, 5) = "購入価"
                        dataArray(1, 6) = "合計"
                        dataArray(1, 7) = "消費税"
                        dataArray(1, 8) = "税込合計"
                        rowIndex = 2
                    End If

                    '納入日計データ追加
                    dataArray(rowIndex, 3) = " * * 納入日計 * * " & tmpYmd
                    dataArray(rowIndex, 6) = dateGokei(0)
                    dataArray(rowIndex, 7) = dateGokei(1)
                    dataArray(rowIndex, 8) = dateGokei(2)

                    '更新
                    rowIndex += 1
                    tmpYmd = ymd
                    dateGokei(0) = 0
                    dateGokei(1) = 0
                    dateGokei(2) = 0

                    If rowIndex = 64 Then
                        dataList.Add(dataArray.Clone())
                        Array.Clear(dataArray, 0, dataArray.Length)
                        page += 1
                        'ヘッダーテキスト部分作成
                        dataArray(0, 0) = "仕入先:"
                        dataArray(0, 1) = tmpSiire
                        dataArray(0, 4) = ymFormattedStr
                        dataArray(0, 8) = page & " 頁"
                        dataArray(1, 0) = "納入日"
                        dataArray(1, 1) = "伝票No."
                        dataArray(1, 2) = "ｺｰﾄﾞ"
                        dataArray(1, 3) = "薬品名"
                        dataArray(1, 4) = "数量"
                        dataArray(1, 5) = "購入価"
                        dataArray(1, 6) = "合計"
                        dataArray(1, 7) = "消費税"
                        dataArray(1, 8) = "税込合計"
                        rowIndex = 2
                    End If

                    '仕入先計データ追加
                    dataArray(rowIndex, 3) = " * * * 仕入先計 * * * " & tmpSiire
                    dataArray(rowIndex, 6) = siireGokei(0)
                    dataArray(rowIndex, 7) = siireGokei(1)
                    dataArray(rowIndex, 8) = siireGokei(2)

                    '更新
                    rowIndex += 1
                    tmpSiire = siire
                    siireGokei(0) = 0
                    siireGokei(1) = 0
                    siireGokei(2) = 0

                    '
                    dataList.Add(dataArray.Clone())
                    Array.Clear(dataArray, 0, dataArray.Length)

                    'ヘッダーテキスト部分作成
                    dataArray(0, 0) = "仕入先:"
                    dataArray(0, 1) = siire
                    dataArray(0, 4) = ymFormattedStr
                    dataArray(0, 8) = page & " 頁"
                    dataArray(1, 0) = "納入日"
                    dataArray(1, 1) = "伝票No."
                    dataArray(1, 2) = "ｺｰﾄﾞ"
                    dataArray(1, 3) = "薬品名"
                    dataArray(1, 4) = "数量"
                    dataArray(1, 5) = "購入価"
                    dataArray(1, 6) = "合計"
                    dataArray(1, 7) = "消費税"
                    dataArray(1, 8) = "税込合計"
                    rowIndex = 2
                End If
            ElseIf ymd <> tmpYmd Then '日付が変わる場合、伝票計データと納入日計データ追加
                '伝票計データ追加
                dataArray(rowIndex, 3) = " * 伝票計 * " & tmpDenno
                dataArray(rowIndex, 6) = denGokei(0)
                dataArray(rowIndex, 7) = denGokei(1)
                dataArray(rowIndex, 8) = denGokei(2)

                '更新
                rowIndex += 1
                tmpDenno = denno
                denGokei(0) = 0
                denGokei(1) = 0
                denGokei(2) = 0

                If rowIndex = 64 Then
                    dataList.Add(dataArray.Clone())
                    Array.Clear(dataArray, 0, dataArray.Length)
                    page += 1
                    'ヘッダーテキスト部分作成
                    dataArray(0, 0) = "仕入先:"
                    dataArray(0, 1) = tmpSiire
                    dataArray(0, 4) = ymFormattedStr
                    dataArray(0, 8) = page & " 頁"
                    dataArray(1, 0) = "納入日"
                    dataArray(1, 1) = "伝票No."
                    dataArray(1, 2) = "ｺｰﾄﾞ"
                    dataArray(1, 3) = "薬品名"
                    dataArray(1, 4) = "数量"
                    dataArray(1, 5) = "購入価"
                    dataArray(1, 6) = "合計"
                    dataArray(1, 7) = "消費税"
                    dataArray(1, 8) = "税込合計"
                    rowIndex = 2
                End If

                '納入日計データ追加
                dataArray(rowIndex, 3) = " * * 納入日計 * * " & tmpYmd
                dataArray(rowIndex, 6) = dateGokei(0)
                dataArray(rowIndex, 7) = dateGokei(1)
                dataArray(rowIndex, 8) = dateGokei(2)

                '更新
                rowIndex += 1
                tmpYmd = ymd
                dateGokei(0) = 0
                dateGokei(1) = 0
                dateGokei(2) = 0

                If rowIndex = 64 Then
                    dataList.Add(dataArray.Clone())
                    Array.Clear(dataArray, 0, dataArray.Length)
                    page += 1
                    'ヘッダーテキスト部分作成
                    dataArray(0, 0) = "仕入先:"
                    dataArray(0, 1) = tmpSiire
                    dataArray(0, 4) = ymFormattedStr
                    dataArray(0, 8) = page & " 頁"
                    dataArray(1, 0) = "納入日"
                    dataArray(1, 1) = "伝票No."
                    dataArray(1, 2) = "ｺｰﾄﾞ"
                    dataArray(1, 3) = "薬品名"
                    dataArray(1, 4) = "数量"
                    dataArray(1, 5) = "購入価"
                    dataArray(1, 6) = "合計"
                    dataArray(1, 7) = "消費税"
                    dataArray(1, 8) = "税込合計"
                    rowIndex = 2
                End If
            ElseIf denno <> tmpDenno Then '伝票No.が変わる場合、伝票計データ追加
                '伝票計データ追加
                dataArray(rowIndex, 3) = " * 伝票計 * " & tmpDenno
                dataArray(rowIndex, 6) = denGokei(0)
                dataArray(rowIndex, 7) = denGokei(1)
                dataArray(rowIndex, 8) = denGokei(2)

                '更新
                rowIndex += 1
                tmpDenno = denno
                denGokei(0) = 0
                denGokei(1) = 0
                denGokei(2) = 0

                If rowIndex = 64 Then
                    dataList.Add(dataArray.Clone())
                    Array.Clear(dataArray, 0, dataArray.Length)
                    page += 1
                    'ヘッダーテキスト部分作成
                    dataArray(0, 0) = "仕入先:"
                    dataArray(0, 1) = tmpSiire
                    dataArray(0, 4) = ymFormattedStr
                    dataArray(0, 8) = page & " 頁"
                    dataArray(1, 0) = "納入日"
                    dataArray(1, 1) = "伝票No."
                    dataArray(1, 2) = "ｺｰﾄﾞ"
                    dataArray(1, 3) = "薬品名"
                    dataArray(1, 4) = "数量"
                    dataArray(1, 5) = "購入価"
                    dataArray(1, 6) = "合計"
                    dataArray(1, 7) = "消費税"
                    dataArray(1, 8) = "税込合計"
                    rowIndex = 2
                End If
            End If
            dataArray(rowIndex, 0) = ymd '納入日
            dataArray(rowIndex, 1) = denno '伝票No
            dataArray(rowIndex, 2) = cod 'コード
            dataArray(rowIndex, 3) = nam '薬品名
            dataArray(rowIndex, 4) = suryo '数量
            dataArray(rowIndex, 5) = tanka '購入価
            dataArray(rowIndex, 6) = kingak '合計
            dataArray(rowIndex, 7) = zei '消費税
            dataArray(rowIndex, 8) = gokei '税込合計

            '値更新
            rowIndex += 1
            denGokei(0) += kingak
            denGokei(1) += zei
            denGokei(2) += gokei
            dateGokei(0) += kingak
            dateGokei(1) += zei
            dateGokei(2) += gokei
            siireGokei(0) += kingak
            siireGokei(1) += zei
            siireGokei(2) += gokei

            rs.MoveNext()
        End While
        If rowIndex = 64 Then
            dataList.Add(dataArray.Clone())
            Array.Clear(dataArray, 0, dataArray.Length)
            page += 1
            'ヘッダーテキスト部分作成
            dataArray(0, 0) = "仕入先:"
            dataArray(0, 1) = tmpSiire
            dataArray(0, 4) = ymFormattedStr
            dataArray(0, 8) = page & " 頁"
            dataArray(1, 0) = "納入日"
            dataArray(1, 1) = "伝票No."
            dataArray(1, 2) = "ｺｰﾄﾞ"
            dataArray(1, 3) = "薬品名"
            dataArray(1, 4) = "数量"
            dataArray(1, 5) = "購入価"
            dataArray(1, 6) = "合計"
            dataArray(1, 7) = "消費税"
            dataArray(1, 8) = "税込合計"
            rowIndex = 2
        End If
        '伝票計データ追加
        dataArray(rowIndex, 3) = " * 伝票計 * " & tmpDenno
        dataArray(rowIndex, 6) = denGokei(0)
        dataArray(rowIndex, 7) = denGokei(1)
        dataArray(rowIndex, 8) = denGokei(2)
        rowIndex += 1

        If rowIndex = 64 Then
            dataList.Add(dataArray.Clone())
            Array.Clear(dataArray, 0, dataArray.Length)
            page += 1
            'ヘッダーテキスト部分作成
            dataArray(0, 0) = "仕入先:"
            dataArray(0, 1) = tmpSiire
            dataArray(0, 4) = ymFormattedStr
            dataArray(0, 8) = page & " 頁"
            dataArray(1, 0) = "納入日"
            dataArray(1, 1) = "伝票No."
            dataArray(1, 2) = "ｺｰﾄﾞ"
            dataArray(1, 3) = "薬品名"
            dataArray(1, 4) = "数量"
            dataArray(1, 5) = "購入価"
            dataArray(1, 6) = "合計"
            dataArray(1, 7) = "消費税"
            dataArray(1, 8) = "税込合計"
            rowIndex = 2
        End If

        '納入日計データ追加
        dataArray(rowIndex, 3) = " * * 納入日計 * * " & tmpYmd
        dataArray(rowIndex, 6) = dateGokei(0)
        dataArray(rowIndex, 7) = dateGokei(1)
        dataArray(rowIndex, 8) = dateGokei(2)
        rowIndex += 1

        If rowIndex = 64 Then
            dataList.Add(dataArray.Clone())
            Array.Clear(dataArray, 0, dataArray.Length)
            page += 1
            'ヘッダーテキスト部分作成
            dataArray(0, 0) = "仕入先:"
            dataArray(0, 1) = tmpSiire
            dataArray(0, 4) = ymFormattedStr
            dataArray(0, 8) = page & " 頁"
            dataArray(1, 0) = "納入日"
            dataArray(1, 1) = "伝票No."
            dataArray(1, 2) = "ｺｰﾄﾞ"
            dataArray(1, 3) = "薬品名"
            dataArray(1, 4) = "数量"
            dataArray(1, 5) = "購入価"
            dataArray(1, 6) = "合計"
            dataArray(1, 7) = "消費税"
            dataArray(1, 8) = "税込合計"
            rowIndex = 2
        End If

        '仕入先計データ追加
        dataArray(rowIndex, 3) = " * * * 仕入先計 * * * " & tmpSiire
        dataArray(rowIndex, 6) = siireGokei(0)
        dataArray(rowIndex, 7) = siireGokei(1)
        dataArray(rowIndex, 8) = siireGokei(2)
        dataList.Add(dataArray.Clone())

        rs.Close()
        cn.Close()

        '作成データから"0"を削除
        For i As Integer = 0 To dataList.Count - 1
            For j As Integer = 2 To 63
                For k As Integer = 5 To 8
                    If dataList(i)(j, k) <> "" Then
                        dataList(i)(j, k) = CInt(dataList(i)(j, k)).ToString("#,0")
                    End If
                    If k = 7 AndAlso dataList(i)(j, k) = "0" Then
                        dataList(i)(j, k) = ""
                    End If
                Next

            Next
        Next

        'エクセル準備
        Dim objExcel As Excel.Application = CreateObject("Excel.Application")
        Dim objWorkBooks As Excel.Workbooks = objExcel.Workbooks
        Dim objWorkBook As Excel.Workbook = objWorkBooks.Open(TopForm.excelFilePass)
        Dim oSheet As Excel.Worksheet = objWorkBook.Worksheets("仕入改")
        objExcel.Calculation = Excel.XlCalculation.xlCalculationManual
        objExcel.ScreenUpdating = False

        '必要枚数コピペ
        For i As Integer = 0 To dataList.Count - 2
            Dim xlPasteRange As Excel.Range = oSheet.Range("A" & (69 + (68 * i))) 'ペースト先
            oSheet.Rows("1:68").copy(xlPasteRange)
            oSheet.HPageBreaks.Add(oSheet.Range("A" & (69 + (68 * i)))) '改ページ
        Next

        'データ書き込み
        For i As Integer = 0 To dataList.Count - 1
            oSheet.Range("B" & (3 + 68 * i), "J" & (66 + 68 * i)).Value = dataList(i)
        Next

        objExcel.Calculation = Excel.XlCalculation.xlCalculationAutomatic
        objExcel.ScreenUpdating = True

        '変更保存確認ダイアログ非表示
        objExcel.DisplayAlerts = False

        '印刷
        If TopForm.rbtnPrintout.Checked = True Then
            oSheet.PrintOut()
        ElseIf TopForm.rbtnPreview.Checked = True Then
            objExcel.Visible = True
            oSheet.PrintPreview(1)
        End If

        ' EXCEL解放
        objExcel.Quit()
        Marshal.ReleaseComObject(objWorkBook)
        Marshal.ReleaseComObject(objExcel)
        oSheet = Nothing
        objWorkBook = Nothing
        objExcel = Nothing

    End Sub
End Class