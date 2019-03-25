Imports System.Data.OleDb

Public Class 仕入データ入力

    '検索タイプ
    Private Const SEARCH_TYPE_COD As Integer = 1 'カナ検索用
    Private Const SEARCH_TYPE_NAM As Integer = 2 '品名検索用

    '消費税率配列
    Private taxArray() As String = {"0.05", "0.08", "0.10"}

    ''' <summary>
    ''' loadイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 仕入データ入力_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        '現在日付セット
        YmdBox.setADStr(Today.ToString("yyyy/MM/dd"))

        '消費税率ボックス初期設定
        initTaxBox()

        '仕入先ボックス初期設定
        initSiireBox()

        'データグリッドビュー（右上）の初期設定
        initDgvSearch()
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
            .DefaultCellStyle.ForeColor = Color.Black
            .DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Control)
            .DefaultCellStyle.SelectionForeColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.Black
            .ShowCellToolTips = False
            .EnableHeadersVisualStyles = False
            .Font = New Font("ＭＳ Ｐゴシック", 10)
            .ReadOnly = True
            .ScrollBars = ScrollBars.None
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
            End With
        End With
    End Sub

    Private Sub codBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles codBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim inputStr As String = codBox.Text
            If inputStr <> "" Then
                displayDgvSearch(inputStr, SEARCH_TYPE_COD)
            End If
        End If
    End Sub

    Private Sub namBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles namBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim inputStr As String = namBox.Text
            If inputStr <> "" Then
                displayDgvSearch(inputStr, SEARCH_TYPE_NAM)
            End If
        End If
    End Sub
End Class