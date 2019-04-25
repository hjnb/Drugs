Imports System.Data.OleDb
Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Core
Public Class 在庫マスタ

    Private Sub 在庫マスタ_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        YmdBox1.setADStr(Today.ToString("yyyy/MM/dd"))
        KeyPreview = True

        Util.EnableDoubleBuffering(DataGridView1)
        DataGridView1.RowTemplate.Height = 18


    End Sub

    Private Sub 在庫マスタ_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If Label22.Text = "0" AndAlso e.KeyCode = Keys.Enter Then
            Dim Ym As String = YmdBox1.getADYmStr()
            Dim Cn As New OleDbConnection(TopForm.DB_Drugs)
            Dim SQLCm As OleDbCommand = Cn.CreateCommand
            Dim Adapter As New OleDbDataAdapter(SQLCm)
            Dim Table As New DataTable
            SQLCm.CommandText = "select Zaiko as ｺｰﾄﾞ, Nam as 品名, Cod as カナ, Bunrui as 分類, Siire as 仕入先, Tani as 単位, Konyu as 購入価, Tanka as 単位単価, SokB as 薬品庫, YakB as 薬局, GaiB as 外来, ByoB as 病棟, [Text] as ﾒﾓ, SokT, YakT, GaiT, ByoT, Ym from ZaikoM WHERE Ym = '" & Ym & "' Order by Zaiko"
            Adapter.Fill(Table)
            DataGridView1.DataSource = Table

            With DataGridView1
                .RowHeadersWidth = 30
                .Columns(0).Width = 45
                .Columns(1).Width = 230
                .Columns(2).Width = 35
                .Columns(3).Width = 40
                .Columns(4).Width = 50
                .Columns(5).Width = 40
                .Columns(6).Width = 50
                .Columns(6).DefaultCellStyle.Format = "#,0"
                .Columns(7).Width = 70
                .Columns(7).DefaultCellStyle.Format = "#,0.00"
                .Columns(8).Width = 50
                .Columns(9).Width = 50
                .Columns(10).Width = 50
                .Columns(11).Width = 50
                .Columns(12).Width = 130
                .Columns(13).Width = 50
                .Columns(14).Width = 50
                .Columns(15).Width = 50
                .Columns(16).Width = 50
                .Columns(17).Visible = False
            End With

            For c As Integer = 0 To 16
                If c = 1 OrElse c = 12 Then
                    DataGridView1.Columns(c).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                ElseIf c = 5 OrElse c = 6 OrElse c = 7 Then
                    DataGridView1.Columns(c).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Else
                    DataGridView1.Columns(c).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If
            Next

        End If

        Dim dgv1rowcount As Integer = DataGridView1.Rows.Count
        If dgv1rowcount > 0 Then
            If txtZaiko.Focused = True AndAlso e.KeyCode = Keys.Enter Then
                For r As Integer = 0 To dgv1rowcount - 1
                    If txtZaiko.Text = (DataGridView1(0, r).Value).ToString Then
                        txtNam.Text = DataGridView1(1, r).Value
                        txtCod.Text = DataGridView1(2, r).Value
                        txtBunrui.Text = DataGridView1(3, r).Value
                        cmbSiire.Text = DataGridView1(4, r).Value
                        txtTani.Text = DataGridView1(5, r).Value
                        txtKonyu.Text = DataGridView1(6, r).Value
                        lblTannka.Text = DataGridView1(7, r).Value
                        txtSokB.Text = DataGridView1(8, r).Value
                        txtYakB.Text = DataGridView1(9, r).Value
                        txtGaiB.Text = DataGridView1(10, r).Value
                        txtByoB.Text = DataGridView1(11, r).Value
                        txtText.Text = DataGridView1(12, r).Value
                    End If
                Next
            End If
        End If
        

        If txtKonyu.Focused = True AndAlso e.KeyCode = Keys.Enter Then
            lblTannka.Text = (Val(txtKonyu.Text) / Val(txtTani.Text)).ToString("0.00")
            Dim a() As String = lblTannka.Text.Split(".")
            lblTannka.Text = CInt(a(0)).ToString("#,0") & "." & a(1)
        End If

        If e.KeyCode = Keys.Enter Then
            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            Me.SelectNextControl(Me.ActiveControl, forward, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Dim slctrow As Integer = DataGridView1.CurrentRow.Index
        txtZaiko.Text = DataGridView1(0, slctrow).Value
        txtNam.Text = DataGridView1(1, slctrow).Value
        txtCod.Text = DataGridView1(2, slctrow).Value
        txtBunrui.Text = DataGridView1(3, slctrow).Value
        cmbSiire.Text = DataGridView1(4, slctrow).Value
        txtTani.Text = DataGridView1(5, slctrow).Value
        txtKonyu.Text = DataGridView1(6, slctrow).Value
        lblTannka.Text = DataGridView1(7, slctrow).Value
        txtSokB.Text = DataGridView1(8, slctrow).Value
        txtYakB.Text = DataGridView1(9, slctrow).Value
        txtGaiB.Text = DataGridView1(10, slctrow).Value
        txtByoB.Text = DataGridView1(11, slctrow).Value
        txtText.Text = DataGridView1(12, slctrow).Value
    End Sub

    Private Sub DataGridView1_CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
        '列ヘッダを対象外しておかないと列ヘッダにも番号を表示してしまう

        If e.ColumnIndex < 0 And e.RowIndex >= 0 Then

            'セルを描画する

            e.Paint(e.ClipBounds, DataGridViewPaintParts.All)

            '行番号を描画する範囲を決定する

            Dim idxRect As Rectangle = e.CellBounds

            'e.Padding(値を表示する境界線からの間隔)を考慮して描画位置を決める

            Dim rectHeight As Long = e.CellStyle.Padding.Top

            Dim rectLeft As Long = e.CellStyle.Padding.Left

            idxRect.Inflate(rectLeft, rectHeight)

            '行番号を描画する

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.CellStyle.Font, idxRect, e.CellStyle.ForeColor, TextFormatFlags.Right Or TextFormatFlags.VerticalCenter)

            '描画完了の通知

            e.Handled = True

        End If

    End Sub

    Private Sub txtBunrui_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtBunrui.KeyPress
        If e.KeyChar = Chr(13) Then 'chr(13)はEnterキー
            Dim strWork As String
            strWork = txtBunrui.Text
            If strWork = "1" Then
                strWork = strWork.Replace("1", "内服")
            ElseIf strWork = "3" Then
                strWork = strWork.Replace("3", "注射")
            ElseIf strWork = "5" Then
                strWork = strWork.Replace("5", "外用")
            ElseIf strWork = "9" Then
                strWork = strWork.Replace("9", "その他")
            Else
                MsgBox("正しく入力してください")
                txtBunrui.Focus()
                Return
            End If

            txtBunrui.Text = strWork

        End If
    End Sub

    Private Sub YmdBox1_YmdGotFocus(sender As Object, e As System.EventArgs) Handles YmdBox1.YmdGotFocus
        Label22.Text = "0"
    End Sub

    Private Sub foucsPosition(sender As Object, e As System.EventArgs) Handles txtZaiko.Enter, txtNam.Enter, txtCod.Enter, txtBunrui.Enter, cmbSiire.Enter, txtTani.Enter, txtKonyu.Enter, txtText.Enter, txtSokB.Enter, txtYakB.Enter, txtGaiB.Enter, txtByoB.Enter
        Label22.Text = "1"
    End Sub

    Private Sub Clear()
        txtZaiko.Text = ""
        txtNam.Text = ""
        txtCod.Text = ""
        txtBunrui.Text = ""
        cmbSiire.Text = ""
        txtTani.Text = ""
        txtKonyu.Text = ""
        lblTannka.Text = "-"
        txtSokB.Text = ""
        txtYakB.Text = ""
        txtGaiB.Text = ""
        txtByoB.Text = ""
        txtText.Text = ""
    End Sub

    Private Sub FormUpdate()
        Clear()

        Dim Ym As String = YmdBox1.getADYmStr()
        Dim Cn As New OleDbConnection(TopForm.DB_Drugs)
        Dim SQLCm As OleDbCommand = Cn.CreateCommand
        Dim Adapter As New OleDbDataAdapter(SQLCm)
        Dim Table As New DataTable
        SQLCm.CommandText = "select Zaiko as ｺｰﾄﾞ, Nam as 品名, Cod as カナ, Bunrui as 分類, Siire as 仕入先, Tani as 単位, Konyu as 購入価, Tanka as 単位単価, SokB as 薬品庫, YakB as 薬局, GaiB as 外来, ByoB as 病棟, [Text] as ﾒﾓ, SokT, YakT, GaiT, ByoT from ZaikoM WHERE Ym = '" & Ym & "' Order by Zaiko"
        Adapter.Fill(Table)
        DataGridView1.DataSource = Table

        With DataGridView1
            .RowHeadersWidth = 30
            .Columns(0).Width = 45
            .Columns(1).Width = 230
            .Columns(2).Width = 35
            .Columns(3).Width = 40
            .Columns(4).Width = 50
            .Columns(5).Width = 40
            .Columns(6).Width = 50
            .Columns(6).DefaultCellStyle.Format = "#,0"
            .Columns(7).Width = 70
            .Columns(7).DefaultCellStyle.Format = "#,0.00"
            .Columns(8).Width = 50
            .Columns(9).Width = 50
            .Columns(10).Width = 50
            .Columns(11).Width = 50
            .Columns(12).Width = 130
            .Columns(13).Width = 50
            .Columns(14).Width = 50
            .Columns(15).Width = 50
            .Columns(16).Width = 50
        End With

        For c As Integer = 0 To 16
            If c = 1 OrElse c = 12 Then
                DataGridView1.Columns(c).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            ElseIf c = 5 OrElse c = 6 OrElse c = 7 Then
                DataGridView1.Columns(c).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Else
                DataGridView1.Columns(c).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
        Next
    End Sub

    Private Sub btbTouroku_Click(sender As System.Object, e As System.EventArgs) Handles btbTouroku.Click
        Dim dgv1rowcount As Integer = DataGridView1.Rows.Count

        Dim codbunrui As String = Strings.Left(txtZaiko.Text, 1)

        If txtZaiko.Text = "" Then
            MsgBox("登録したい在庫ｺｰﾄﾞを入力してください")
            txtZaiko.Focus()
            Return
        End If
        If txtNam.Text = "" OrElse txtCod.Text = "" Then
            MsgBox("品名とカナを正しく入力してください")
            txtNam.Focus()
            Return
        End If
        If (codbunrui = "1" AndAlso txtBunrui.Text <> "内服") OrElse (codbunrui = "3" AndAlso txtBunrui.Text <> "注射") OrElse (codbunrui = "5" AndAlso txtBunrui.Text <> "外用") OrElse (codbunrui = "9" AndAlso txtBunrui.Text <> "その他") Then
            MsgBox("在庫ｺｰﾄﾞと分類が矛盾しています")
            txtBunrui.Focus()
            Return
        End If
        If cmbSiire.Text = "" OrElse txtTani.Text = "" OrElse txtKonyu.Text = "" Then
            MsgBox("仕入先・単位・購入価を入力してください")
            cmbSiire.Focus()
            Return
        End If
        If txtSokB.Text <> "0" AndAlso txtSokB.Text <> "1" Then
            MsgBox("薬品庫を正しく入力してください")
            txtSokB.Focus()
            Return
        End If
        If txtYakB.Text <> "0" AndAlso txtYakB.Text <> "1" Then
            MsgBox("薬局を正しく入力してください")
            txtYakB.Focus()
            Return
        End If
        If txtGaiB.Text <> "0" AndAlso txtGaiB.Text <> "1" Then
            MsgBox("外来を正しく入力してください")
            txtGaiB.Focus()
            Return
        End If
        If txtByoB.Text <> "0" AndAlso txtByoB.Text <> "1" Then
            MsgBox("病棟を正しく入力してください")
            txtByoB.Focus()
            Return
        End If

        For row As Integer = 0 To dgv1rowcount - 1
            If YmdBox1.getADYmStr() = DataGridView1(17, row).Value AndAlso txtZaiko.Text = DataGridView1(0, row).Value Then
                henkou()

                Update()

                Exit Sub
            End If
        Next

        tuika()

        Update()

    End Sub

    Private Sub henkou()
        Dim DGV1rowcount As Integer = DataGridView1.Rows.Count

        If MsgBox("変更してよろしいですか？", MsgBoxStyle.YesNo + vbExclamation, "登録確認") = MsgBoxResult.Yes Then
            Dim cnn As New ADODB.Connection
            cnn.Open(TopForm.DB_Drugs)

            Dim SQL As String = ""
            SQL = "DELETE FROM ZaikoM WHERE Zaiko = " & txtZaiko.Text & " AND Ym = '" & YmdBox1.getADYmStr() & "'"
            cnn.Execute(SQL)

            tuika()

            cnn.Close()

            FormUpdate()
            Exit Sub

        End If
    End Sub

    Private Sub tuika()
        Dim soks, yaks, gais, byos, zaikok, sokk, yakk, gaik, byok, flag As Integer
        Dim sokt, yakt, gait, byot As String

        soks = 0
        yaks = 0
        gais = 0
        byos = 0
        zaikok = 0
        sokk = 0
        yakk = 0
        gaik = 0
        byok = 0
        flag = 0
        sokt = ""
        yakt = ""
        gait = ""
        byot = ""

        Dim cnn As New ADODB.Connection
        cnn.Open(TopForm.DB_Drugs)

        Dim SQL As String = ""
        SQL = "INSERT INTO ZaikoM ([Ym], [Zaiko], [Cod], [Nam], [Siire], [Bunrui], [SokB], [YakB], [GaiB], [ByoB], [Tani], [Konyu], [Tanka], [SokS], [YakS], [GaiS], [ByoS], [ZaikoK], [SokK], [YakK], [GaiK], [ByoK], [Text], [Flag], [SokT], [YakT], [GaiT], [ByoT]) VALUES ("
        SQL &= "'" & YmdBox1.getADYmStr() & "', "
        SQL &= txtZaiko.Text & ", "
        SQL &= "'" & txtCod.Text & "', "
        SQL &= "'" & txtNam.Text & "', "
        SQL &= "'" & cmbSiire.Text & "', "
        SQL &= "'" & txtBunrui.Text & "', "
        SQL &= "'" & txtSokB.Text & "', "
        SQL &= "'" & txtYakB.Text & "', "
        SQL &= "'" & txtGaiB.Text & "', "
        SQL &= "'" & txtByoB.Text & "', "
        SQL &= "'" & txtTani.Text & "', "
        SQL &= "'" & txtKonyu.Text & "', "
        SQL &= "'" & lblTannka.Text & "', "
        SQL &= soks & ", "
        SQL &= yaks & ", "
        SQL &= gais & ", "
        SQL &= byos & ", "
        SQL &= zaikok & ", "
        SQL &= sokk & ", "
        SQL &= yakk & ", "
        SQL &= gaik & ", "
        SQL &= byok & ", "
        SQL &= "'" & txtText.Text & "', "
        SQL &= flag & ", "
        SQL &= "'" & sokt & "', "
        SQL &= "'" & yakt & "', "
        SQL &= "'" & gait & "', "
        SQL &= "'" & byot & "' "
        SQL &= ")"
        cnn.Execute(SQL)

        cnn.Close()

        FormUpdate()
    End Sub

    Private Sub btnSakujo_Click(sender As System.Object, e As System.EventArgs) Handles btnSakujo.Click
        Dim DGV1rowcount As Integer = DataGridView1.Rows.Count
        If txtZaiko.Text = "" Then
            MsgBox("削除したい在庫ｺｰﾄﾞを入力してください")
            Return
        End If

        For i As Integer = 0 To DGV1rowcount - 1
            If txtZaiko.Text = DataGridView1(0, i).Value Then
                If MsgBox("削除してよろしいですか？", MsgBoxStyle.YesNo + vbExclamation, "削除確認") = MsgBoxResult.Yes Then
                    Dim cnn As New ADODB.Connection
                    cnn.Open(TopForm.DB_Drugs)

                    Dim SQL As String = ""

                    SQL = "DELETE FROM ZaikoM WHERE Zaiko = " & txtZaiko.Text & " AND Ym = '" & YmdBox1.getADYmStr() & "'"

                    cnn.Execute(SQL)
                    cnn.Close()

                    FormUpdate()
                    Exit Sub

                End If
            End If
        Next

        MsgBox("登録されていません")
    End Sub

    Private Sub btnNenngetuSakujo_Click(sender As System.Object, e As System.EventArgs) Handles btnNenngetuSakujo.Click
        Dim DGV1rowcount As Integer = DataGridView1.Rows.Count
        If DGV1rowcount = 0 Then
            MsgBox("当月のデータはありません")
            Return
        End If

        If MsgBox("当月のデータを削除してよろしいですか？", MsgBoxStyle.YesNo + vbExclamation, "削除確認") = MsgBoxResult.Yes Then
            Dim cnn As New ADODB.Connection
            cnn.Open(TopForm.DB_Drugs)

            Dim SQL As String = ""

            SQL = "DELETE FROM ZaikoM WHERE Ym = '" & YmdBox1.getADYmStr() & "'"

            cnn.Execute(SQL)
            cnn.Close()

            FormUpdate()
            Exit Sub

        End If

    End Sub

    Private Sub btnInnsatu_Click(sender As System.Object, e As System.EventArgs) Handles btnInnsatu.Click
        Dim dgv1rowcount As Integer = DataGridView1.Rows.Count

        If dgv1rowcount = 0 Then
            MsgBox("印刷対象のデータがありません")
            Return
        End If

        Dim objExcel As Object
        Dim objWorkBooks As Object
        Dim objWorkBook As Object
        Dim oSheets As Object
        Dim oSheet As Object
        Dim day As DateTime = DateTime.Today

        objExcel = CreateObject("Excel.Application")
        objWorkBooks = objExcel.Workbooks
        objWorkBook = objWorkBooks.Open(TopForm.excelFilePass)
        oSheets = objWorkBook.Worksheets
        oSheet = objWorkBook.Worksheets("在庫改")

        objExcel.Calculation = Excel.XlCalculation.xlCalculationManual
        objExcel.ScreenUpdating = False

        Dim page As Integer = dgv1rowcount \ 60

        If page > 0 Then
            Dim xlRange As Excel.Range = oSheet.Cells.Range("A1:N63")
            xlRange.Copy()
            For i As Integer = 1 To page
                Dim xlPasteRange As Excel.Range = oSheet.Range("A" & 63 * i + 1) 'ペースト先
                oSheet.rows("1:63").copy(xlPasteRange)
            Next
        End If

        Dim cell(59, 11) As String

        Dim nowpage As Integer = 1
        Dim rowindex As Integer = 0

        For row As Integer = 0 To dgv1rowcount - 1
            If rowindex = 60 Then
                oSheet.Range("E" & nowpage * 63 - 62).Value = DataGridView1(17, 0).Value
                oSheet.Range("J" & nowpage * 63 - 62).Value = nowpage & "頁"
                oSheet.Range("B" & nowpage * 63 - 60, "M" & nowpage * 63 - 1).Value = cell
                For r As Integer = 0 To 59
                    For c As Integer = 0 To 11
                        cell(r, c) = ""
                    Next
                Next
                nowpage = nowpage + 1
                rowindex = 0
            End If

            For col As Integer = 0 To 11
                If col = 1 Then
                    cell(rowindex, col) = Util.checkDBNullValue(Strings.Left(DataGridView1(3, row).Value, 1))
                ElseIf col = 3 Then
                    cell(rowindex, col) = Util.checkDBNullValue(DataGridView1(1, row).Value)
                ElseIf col >= 8 Then
                    If DataGridView1(col, row).Value = 1 Then
                        cell(rowindex, col) = "*"
                    Else
                        cell(rowindex, col) = ""
                    End If
                Else
                    cell(rowindex, col) = Util.checkDBNullValue(DataGridView1(col, row).FormattedValue)
                End If
            Next

            rowindex = rowindex + 1

        Next

        oSheet.Range("E" & nowpage * 63 - 62).Value = DataGridView1(17, 0).Value
        oSheet.Range("J" & nowpage * 63 - 62).Value = nowpage & "頁"
        oSheet.Range("B" & nowpage * 63 - 60, "M" & nowpage * 63 - 1).Value = cell

        objExcel.Calculation = Excel.XlCalculation.xlCalculationAutomatic
        objExcel.ScreenUpdating = True

        '保存
        objExcel.DisplayAlerts = False

        ' エクセル表示
        objExcel.Visible = True

        ''印刷
        'If TopForm.rbnPreview.Checked = True Then
        oSheet.PrintPreview(1)
        'ElseIf TopForm.rbnPrintout.Checked = True Then
        '    oSheet.Printout(1)
        'End If

        ' EXCEL解放
        objExcel.Quit()
        Marshal.ReleaseComObject(oSheet)
        Marshal.ReleaseComObject(objWorkBook)
        Marshal.ReleaseComObject(objExcel)
        oSheet = Nothing
        objWorkBook = Nothing
        objExcel = Nothing
    End Sub

    Private Sub btnLastMonthCopy_Click(sender As System.Object, e As System.EventArgs) Handles btnLastMonthCopy.Click
        Dim Ym As String = YmdBox1.getADYmStr()
        Dim Cn As New OleDbConnection(TopForm.DB_Drugs)
        Dim SQLCm As OleDbCommand = Cn.CreateCommand
        Dim Adapter As New OleDbDataAdapter(SQLCm)
        Dim Table As New DataTable
        SQLCm.CommandText = "select Zaiko as ｺｰﾄﾞ, Nam as 品名, Cod as カナ, Bunrui as 分類, Siire as 仕入先, Tani as 単位, Konyu as 購入価, Tanka as 単位単価, SokB as 薬品庫, YakB as 薬局, GaiB as 外来, ByoB as 病棟, [Text] as ﾒﾓ, SokT, YakT, GaiT, ByoT, Ym from ZaikoM WHERE Ym = '" & Ym & "' Order by Zaiko"
        Adapter.Fill(Table)
        DataGridView1.DataSource = Table

        With DataGridView1
            .RowHeadersWidth = 30
            .Columns(0).Width = 45
            .Columns(1).Width = 230
            .Columns(2).Width = 35
            .Columns(3).Width = 40
            .Columns(4).Width = 50
            .Columns(5).Width = 40
            .Columns(6).Width = 50
            .Columns(6).DefaultCellStyle.Format = "#,0"
            .Columns(7).Width = 70
            .Columns(7).DefaultCellStyle.Format = "#,0.00"
            .Columns(8).Width = 50
            .Columns(9).Width = 50
            .Columns(10).Width = 50
            .Columns(11).Width = 50
            .Columns(12).Width = 130
            .Columns(13).Width = 50
            .Columns(14).Width = 50
            .Columns(15).Width = 50
            .Columns(16).Width = 50
            .Columns(17).Visible = False
        End With

        For c As Integer = 0 To 16
            If c = 1 OrElse c = 12 Then
                DataGridView1.Columns(c).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            ElseIf c = 5 OrElse c = 6 OrElse c = 7 Then
                DataGridView1.Columns(c).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Else
                DataGridView1.Columns(c).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
        Next

        Dim ymd As Date = YmdBox1.getADStr()
        ymd = ymd.AddMonths(-1)
        Dim lastym As String = Strings.Left(ymd, 7)

        Dim SQLCm2 As OleDbCommand = Cn.CreateCommand
        Dim Adapter2 As New OleDbDataAdapter(SQLCm2)
        Dim Table2 As New DataTable
        SQLCm2.CommandText = "select * from ZaikoM WHERE Ym = '" & lastym & "' Order by Zaiko"
        Adapter2.Fill(Table2)
        DataGridView2.DataSource = Table2

        If DataGridView1.Rows.Count <> 0 Then
            If MsgBox("当月分のデータがあります。上書きしてよろしいですか？", MsgBoxStyle.YesNo + vbExclamation, "コピー作業確認") = MsgBoxResult.No Then
                Return
            End If
        End If

        btnNenngetuSakujo.PerformClick()

        If MsgBox(YmdBox1.getADYmStr() & " に前月分のデータをコピーしてよろしいですか？", MsgBoxStyle.YesNo + vbExclamation, "コピー作業確認") = MsgBoxResult.Yes Then
            Dim cnn As New ADODB.Connection
            Dim rs As New ADODB.Recordset
            cnn.Open(TopForm.DB_Drugs)

            rs.Open("ZaikoM", cnn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

            Dim dgv2rowcount As Integer = DataGridView2.Rows.Count

            For i As Integer = 0 To dgv2rowcount - 1
                rs.AddNew()
                rs.Fields("Ym").Value = YmdBox1.getADYmStr()
                rs.Fields("Zaiko").Value = Util.checkDBNullValue(DataGridView2(2, i).Value)
                rs.Fields("Cod").Value = Util.checkDBNullValue(DataGridView2(3, i).Value)
                rs.Fields("Nam").Value = Util.checkDBNullValue(DataGridView2(4, i).Value)
                rs.Fields("Siire").Value = Util.checkDBNullValue(DataGridView2(5, i).Value)
                rs.Fields("Bunrui").Value = Util.checkDBNullValue(DataGridView2(6, i).Value)
                rs.Fields("SokB").Value = Util.checkDBNullValue(DataGridView2(7, i).Value)
                rs.Fields("YakB").Value = Util.checkDBNullValue(DataGridView2(8, i).Value)
                rs.Fields("GaiB").Value = Util.checkDBNullValue(DataGridView2(9, i).Value)
                rs.Fields("ByoB").Value = Util.checkDBNullValue(DataGridView2(10, i).Value)
                rs.Fields("Tani").Value = Util.checkDBNullValue(DataGridView2(11, i).Value)
                rs.Fields("Konyu").Value = Util.checkDBNullValue(DataGridView2(12, i).Value)
                rs.Fields("Tanka").Value = Util.checkDBNullValue(DataGridView2(13, i).Value)
                rs.Fields("SokS").Value = Util.checkDBNullValue(DataGridView2(14, i).Value)
                rs.Fields("YakS").Value = Util.checkDBNullValue(DataGridView2(15, i).Value)
                rs.Fields("GaiS").Value = Util.checkDBNullValue(DataGridView2(16, i).Value)
                rs.Fields("ByoS").Value = Util.checkDBNullValue(DataGridView2(17, i).Value)
                rs.Fields("ZaikoK").Value = Util.checkDBNullValue(DataGridView2(18, i).Value)
                rs.Fields("SokK").Value = Util.checkDBNullValue(DataGridView2(19, i).Value)
                rs.Fields("YakK").Value = Util.checkDBNullValue(DataGridView2(20, i).Value)
                rs.Fields("GaiK").Value = Util.checkDBNullValue(DataGridView2(21, i).Value)
                rs.Fields("ByoK").Value = Util.checkDBNullValue(DataGridView2(22, i).Value)
                rs.Fields("Text").Value = Util.checkDBNullValue(DataGridView2(23, i).Value)
                rs.Fields("Flag").Value = Util.checkDBNullValue(DataGridView2(24, i).Value)
                rs.Fields("SokT").Value = Util.checkDBNullValue(DataGridView2(25, i).Value)
                rs.Fields("YakT").Value = Util.checkDBNullValue(DataGridView2(26, i).Value)
                rs.Fields("GaiT").Value = Util.checkDBNullValue(DataGridView2(27, i).Value)
                rs.Fields("ByoT").Value = Util.checkDBNullValue(DataGridView2(28, i).Value)
            Next
            rs.Update()

            cnn.Close()
        End If

        FormUpdate()

    End Sub
End Class