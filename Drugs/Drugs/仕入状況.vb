Imports System.Data.OleDb

Public Class 仕入状況

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
    Private Sub 仕入状況_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        '仕入先ボックス初期設定
        initSiireBox()

        '日付ボックス初期値設定
        initYmdBox()

        'データグリッドビュー初期設定
        initDgvSiire()
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
    ''' 日付ボックス初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initYmdBox()
        '現在日付
        Dim nowDate As String = Today.ToString("yyyy/MM/dd")
        Dim firstDate As New DateTime(CInt(nowDate.Split("/")(0)), CInt(nowDate.Split("/")(1)), 1)

        'from初期値
        Dim fromYmd As String = firstDate.AddYears(-1).ToString("yyyy/MM/dd")

        'to初期値
        Dim toYmd As String = firstDate.AddDays(-1).ToString("yyyy/MM/dd")

        '値をセット
        fromYmdBox.setADStr(fromYmd)
        toYmdBox.setADStr(toYmd)
    End Sub

    ''' <summary>
    ''' データグリッドビュー初期設定
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
    ''' リスト表示
    ''' </summary>
    ''' <param name="siire">仕入先</param>
    ''' <param name="fromYmd">from日付</param>
    ''' <param name="toYmd">to日付</param>
    ''' <remarks></remarks>
    Private Sub displayNamList(siire As String, fromYmd As String, toYmd As String)
        'リストクリア
        namListBox.Items.Clear()
        namLabel.Text = ""
        listRowCountLabel.Text = ""

        'データ取得、表示
        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Drugs)
        Dim sql As String = "select distinct Nam from SiireD where Siire = '" & siire & "' and ('" & fromYmd & "' <= Ymd and Ymd <= '" & toYmd & "') order by Nam"
        Dim rs As New ADODB.Recordset
        rs.Open(sql, cn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic)
        Dim tmpNam As String = ""
        While Not rs.EOF
            Dim nam As String = Util.checkDBNullValue(rs.Fields("Nam").Value).Replace(" ", "").Replace("　", "")
            If tmpNam <> nam Then
                namListBox.Items.Add(nam)
                tmpNam = nam
            End If
            rs.MoveNext()
        End While
        rs.Close()
        cn.Close()

        'リスト数表示
        If namListBox.Items.Count <> 0 Then
            listRowCountLabel.Text = namListBox.Items.Count
        End If
    End Sub

    ''' <summary>
    ''' 対象の品名の仕入状況表示
    ''' </summary>
    ''' <param name="nam">品名</param>
    ''' <param name="fromYmd">from日付</param>
    ''' <param name="toYmd">to日付</param>
    ''' <remarks></remarks>
    Private Sub displayDgvSiire(nam As String, fromYmd As String, toYmd As String)

    End Sub

    Private Sub initSiireW(siire As String)
        '既存データ削除
        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Drugs)
        Dim cmd As New ADODB.Command()
        cmd.ActiveConnection = cn
        cmd.CommandText = "delete from SiireW"
        cmd.Execute()

        '仕入先データ取得
        Dim sql As String = "select * from SiireD where Siire = '" & siire & "' order by Nam"
        Dim rsD As New ADODB.Recordset()
        rsD.Open(sql, cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockPessimistic)

        'SiireWテーブルに登録
        Dim tmpNam As String = ""
        Dim rsW As New ADODB.Recordset()
        rsW.Open("SiireW", cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockPessimistic)
        While Not rsD.EOF
            '品名の空白を削除
            Dim nam As String = Util.checkDBNullValue(rsD.Fields("Nam").Value).Replace(" ", "").Replace("　", "")

            rsW.AddNew()
            rsW.Fields("autono").Value = rsD.Fields("autono").Value
            rsW.Fields("Ymd").Value = Util.checkDBNullValue(rsD.Fields("Ymd").Value)
            rsW.Fields("Siire").Value = Util.checkDBNullValue(rsD.Fields("Siire").Value)
            rsW.Fields("Denno").Value = Util.checkDBNullValue(rsD.Fields("Denno").Value)
            rsW.Fields("Cod").Value = Util.checkDBNullValue(rsD.Fields("Cod").Value)
            rsW.Fields("Nam").Value = nam
            rsW.Fields("Suryo").Value = rsD.Fields("Suryo").Value
            rsW.Fields("Tanka").Value = rsD.Fields("Tanka").Value
            rsW.Fields("Kingak").Value = rsD.Fields("Kingak").Value
            rsW.Fields("Zei").Value = rsD.Fields("Zei").Value
            rsW.Fields("Gokei").Value = rsD.Fields("Gokei").Value
            tmpNam = nam
            rsD.MoveNext()
        End While
        rsW.Update()
        rsW.Close()
        rsD.Close()
        cn.Close()

    End Sub

    ''' <summary>
    ''' 仕入先ボックス値変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub siireBox_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles siireBox.SelectedValueChanged
        Dim siire As String = siireBox.Text
        If siire <> "" Then
            displayNamList(siire, fromYmdBox.getADStr(), toYmdBox.getADStr())
            initSiireW(siire)
        End If
    End Sub

    ''' <summary>
    ''' リスト選択値変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub namListBox_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles namListBox.SelectedValueChanged
        namLabel.Text = namListBox.Text
    End Sub
End Class