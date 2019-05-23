Imports System.Data.OleDb
Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Core

Public Class 在庫入力
    Private y As Integer = 0

    Private Sub 在庫入力_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        YmdBox1.setADStr(Today.ToString("yyyy/MM/dd"))
        KeyPreview = True
        lblnam.visible = False


        Util.EnableDoubleBuffering(DataGridView1)
        DataGridView1.RowTemplate.Height = 25

    End Sub

    Private Sub 在庫入力_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Dim DGV1rowcount As Integer = DataGridView1.Rows.Count

        If e.KeyCode = Keys.Enter Then
            If txtZaiko.Focused = True Then
                For i As Integer = 0 To DGV1rowcount - 1
                    If txtZaiko.Text = DataGridView1(1, i).Value Then
                        lblNam.Text = DataGridView1(0, i).Value
                        lblNam.Visible = True
                        If System.Text.RegularExpressions.Regex.IsMatch(DataGridView1(1, i).Value.ToString, txtZaiko.Text) = True Then
                            '見つかった場合は、その行に移動します。
                            DataGridView1.Rows(i).Selected = True
                            DataGridView1.FirstDisplayedScrollingRowIndex = i
                            '見つかった時点で繰り返し処理を中止します。
                            y = i
                        End If
                        If cmbBasyo.Text = "薬品庫" Then
                            txtSuuryou.Text = DataGridView1(3, i).Value
                            txtKome.Text = DataGridView1(12, i).Value
                            Exit For
                        ElseIf cmbBasyo.Text = "薬局" Then
                            txtSuuryou.Text = DataGridView1(4, i).Value
                            txtKome.Text = DataGridView1(13, i).Value
                            Exit For
                        ElseIf cmbBasyo.Text = "病棟" Then
                            txtSuuryou.Text = DataGridView1(5, i).Value
                            txtKome.Text = DataGridView1(14, i).Value
                            Exit For
                        ElseIf cmbBasyo.Text = "外来" Then
                            txtSuuryou.Text = DataGridView1(6, i).Value
                            txtKome.Text = DataGridView1(15, i).Value
                            Exit For
                        End If
                    End If
                Next
            End If

            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            Me.SelectNextControl(Me.ActiveControl, forward, True, True, True)
            e.Handled = True
        End If


    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Dim slctrow As Integer = DataGridView1.CurrentRow.Index
        lblNam.Visible = True
        lblNam.Text = DataGridView1(0, slctrow).Value
        txtZaiko.Text = DataGridView1(1, slctrow).Value

        If cmbBasyo.Text = "薬品庫" Then
            txtSuuryou.Text = DataGridView1(3, slctrow).FormattedValue
            txtKome.Text = DataGridView1(12, slctrow).Value
        ElseIf cmbBasyo.Text = "薬局" Then
            txtSuuryou.Text = DataGridView1(4, slctrow).FormattedValue
            txtKome.Text = DataGridView1(13, slctrow).Value
        ElseIf cmbBasyo.Text = "病棟" Then
            txtSuuryou.Text = DataGridView1(5, slctrow).FormattedValue
            txtKome.Text = DataGridView1(14, slctrow).Value
        ElseIf cmbBasyo.Text = "外来" Then
            txtSuuryou.Text = DataGridView1(6, slctrow).FormattedValue
            txtKome.Text = DataGridView1(15, slctrow).Value
        Else
            txtSuuryou.Text = ""
        End If



    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting

        If DataGridView1.Columns(e.ColumnIndex).Name = "SokS" OrElse DataGridView1.Columns(e.ColumnIndex).Name = "YakS" OrElse DataGridView1.Columns(e.ColumnIndex).Name = "GaiS" OrElse DataGridView1.Columns(e.ColumnIndex).Name = "ByoS" Then
            If e.Value = 0 Then
                e.Value = ""
                e.FormattingApplied = True
            End If
        End If
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

    Private Sub DGV1Show(Optional zaiko As Integer = 0)
        Dim Ym As String = YmdBox1.getADYmStr()
        Dim Cn As New OleDbConnection(TopForm.DB_Drugs)
        Dim SQLCm As OleDbCommand = Cn.CreateCommand
        Dim Adapter As New OleDbDataAdapter(SQLCm)
        Dim Table As New DataTable

        Dim basyo As String
        If cmbBasyo.Text = "薬品庫" Then
            basyo = "SokB"
        ElseIf cmbBasyo.Text = "薬局" Then
            basyo = "YakB"
        ElseIf cmbBasyo.Text = "外来" Then
            basyo = "GaiB"
        ElseIf cmbBasyo.Text = "病棟" Then
            basyo = "ByoB"
        Else
            basyo = cmbBasyo.Text
        End If

        SQLCm.CommandText = "select Nam as 品名, Zaiko as ｺｰﾄﾞ, Cod as カナ, SokS, YakS, GaiS, ByoS, ZaikoK, SokK, YakK, GaiK, ByoK, SokT, YakT, GaiT, ByoT, Ym, Tanka from ZaikoM WHERE Ym = '" & Ym & "' and " & basyo & " = 1 Order by Bunrui, Cod"
        Adapter.Fill(Table)
        DataGridView1.DataSource = Table

        With DataGridView1
            .RowHeadersWidth = 30
            .Columns(0).Width = 375
            .Columns(1).Width = 70
            .Columns(2).Visible = False
            .Columns(3).Width = 60
            .Columns(4).Visible = False
            .Columns(5).Visible = False
            .Columns(6).Visible = False
            .Columns(7).Visible = False
            .Columns(8).Visible = False
            .Columns(9).Visible = False
            .Columns(10).Visible = False
            .Columns(11).Visible = False
            .Columns(12).Width = 300
            .Columns(13).Width = 300
            .Columns(14).Width = 300
            .Columns(15).Width = 300
            .Columns(16).Visible = False
            .Columns(17).Visible = False
            .Columns(3).HeaderText = "数量"
            .Columns(4).HeaderText = "数量"
            .Columns(5).HeaderText = "数量"
            .Columns(6).HeaderText = "数量"
            .Columns(12).HeaderText = "ｺﾒﾝﾄ"
            .Columns(13).HeaderText = "ｺﾒﾝﾄ"
            .Columns(14).HeaderText = "ｺﾒﾝﾄ"
            .Columns(15).HeaderText = "ｺﾒﾝﾄ"
        End With

        If cmbBasyo.Text = "薬品庫" Then
            With DataGridView1
                .Columns(3).Visible = True
                .Columns(4).Visible = False
                .Columns(5).Visible = False
                .Columns(6).Visible = False
                .Columns(12).Visible = True
                .Columns(13).Visible = False
                .Columns(14).Visible = False
                .Columns(15).Visible = False
            End With
        ElseIf cmbBasyo.Text = "薬局" Then
            With DataGridView1
                .Columns(3).Visible = False
                .Columns(4).Visible = True
                .Columns(5).Visible = False
                .Columns(6).Visible = False
                .Columns(12).Visible = False
                .Columns(13).Visible = True
                .Columns(14).Visible = False
                .Columns(15).Visible = False
            End With
        ElseIf cmbBasyo.Text = "外来" Then
            With DataGridView1
                .Columns(3).Visible = False
                .Columns(4).Visible = False
                .Columns(5).Visible = True
                .Columns(6).Visible = False
                .Columns(12).Visible = False
                .Columns(13).Visible = False
                .Columns(14).Visible = True
                .Columns(15).Visible = False
            End With
        ElseIf cmbBasyo.Text = "病棟" Then
            With DataGridView1
                .Columns(3).Visible = False
                .Columns(4).Visible = False
                .Columns(5).Visible = False
                .Columns(6).Visible = True
                .Columns(12).Visible = False
                .Columns(13).Visible = False
                .Columns(14).Visible = False
                .Columns(15).Visible = True
            End With
        Else
            With DataGridView1
                .Columns(3).Visible = True
                .Columns(4).Visible = False
                .Columns(5).Visible = False
                .Columns(6).Visible = False
                .Columns(12).Visible = True
                .Columns(13).Visible = False
                .Columns(14).Visible = False
                .Columns(15).Visible = False
            End With
        End If

        For c As Integer = 0 To 16
            If c = 0 OrElse c = 12 OrElse c = 13 OrElse c = 14 OrElse c = 15 Then
                DataGridView1.Columns(c).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            ElseIf c = 1 Then
                DataGridView1.Columns(c).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            ElseIf c = 3 OrElse c = 4 OrElse c = 5 OrElse c = 6 Then
                DataGridView1.Columns(c).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
        Next

        DataGridView1.FirstDisplayedScrollingRowIndex = y
        Dim DGV1rowcount As Integer = DataGridView1.Rows.Count
        For r As Integer = 0 To DGV1rowcount - 1
            If DataGridView1(1, r).Value = zaiko Then
                DataGridView1.Rows(r).Selected = True
            End If
        Next
    End Sub

    Private Sub cmbBasyo_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbBasyo.SelectedIndexChanged
        DGV1Show()
        
    End Sub

    Private Sub FormUpdate()
        txtZaiko.Text = ""
        lblNam.Text = ""
        txtSuuryou.Text = ""
        txtKome.Text = ""

        DGV1Show()

    End Sub

    Private Sub btnTouroku_Click(sender As System.Object, e As System.EventArgs) Handles btnTouroku.Click
        Dim dgv1rowcount As Integer = DataGridView1.Rows.Count
        Dim cnn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        cnn.Open(TopForm.DB_Drugs)
        Dim SQL As String = ""
        Dim updateSQL As String = ""

        If txtZaiko.Text = "" Then
            MsgBox("ｺｰﾄﾞを正しく入力してください")
            Return
        End If

        For i As Integer = 0 To dgv1rowcount - 1
            If txtZaiko.Text = DataGridView1(1, i).Value Then
                Dim sokk, yakk, gaik, byok, zaikok As Integer

                Dim basyo As String
                If cmbBasyo.Text = "薬品庫" Then
                    basyo = "Sok"
                    sokk = Val(DataGridView1("Tanka", i).Value) * Val(txtSuuryou.Text)
                    zaikok = sokk + Val(DataGridView1("yakk", i).Value) + Val(DataGridView1("gaik", i).Value) + Val(DataGridView1("byok", i).Value)
                    updateSQL = "UPDATE ZaikoM SET " & basyo & "S = " & txtSuuryou.Text & ", SokK = " & sokk & ", ZaikoK = " & zaikok & ", " & basyo & "T = '" & txtKome.Text & "' WHERE (Zaiko = " & txtZaiko.Text & ") And (YM='" & YmdBox1.getADYmStr() & "')"
                ElseIf cmbBasyo.Text = "薬局" Then
                    basyo = "Yak"
                    yakk = Val(DataGridView1("Tanka", i).Value) * Val(txtSuuryou.Text)
                    zaikok = Val(DataGridView1("SokK", i).Value) + yakk + Val(DataGridView1("gaik", i).Value) + Val(DataGridView1("byok", i).Value)
                    updateSQL = "UPDATE ZaikoM SET " & basyo & "S = " & txtSuuryou.Text & ", Yakk = " & yakk & ", ZaikoK = " & zaikok & ", " & basyo & "T = '" & txtKome.Text & "' WHERE (Zaiko = " & txtZaiko.Text & ") And (YM='" & YmdBox1.getADYmStr() & "')"
                ElseIf cmbBasyo.Text = "外来" Then
                    basyo = "Gai"
                    gaik = Val(DataGridView1("Tanka", i).Value) * Val(txtSuuryou.Text)
                    zaikok = Val(DataGridView1("SokK", i).Value) + Val(DataGridView1("yakk", i).Value) + gaik + Val(DataGridView1("byok", i).Value)
                    updateSQL = "UPDATE ZaikoM SET " & basyo & "S = " & txtSuuryou.Text & ", Gaik = " & gaik & ", ZaikoK = " & zaikok & ", " & basyo & "T = '" & txtKome.Text & "' WHERE (Zaiko = " & txtZaiko.Text & ") And (YM='" & YmdBox1.getADYmStr() & "')"
                ElseIf cmbBasyo.Text = "病棟" Then
                    basyo = "Byo"
                    byok = Val(DataGridView1("Tanka", i).Value) * Val(txtSuuryou.Text)
                    zaikok = Val(DataGridView1("SokK", i).Value) + Val(DataGridView1("yakk", i).Value) + Val(DataGridView1("gaik", i).Value) + byok
                    updateSQL = "UPDATE ZaikoM SET " & basyo & "S = " & txtSuuryou.Text & ", Byok = " & byok & ", ZaikoK = " & zaikok & ", " & basyo & "T = '" & txtKome.Text & "' WHERE (Zaiko = " & txtZaiko.Text & ") And (YM='" & YmdBox1.getADYmStr() & "')"
                Else
                    MsgBox("保管場所を正しく入力してください")
                    Return
                End If

                cnn.Execute(updateSQL)
                cnn.Close()

                Dim f As Form = New Form1()
                f.Owner = Me
                f.Show()
                f.Close()

                DGV1Show(txtZaiko.Text)

                txtZaiko.Text = ""
                lblNam.Text = ""
                txtSuuryou.Text = ""
                txtKome.Text = ""

                txtZaiko.Focus()

                Exit Sub

            End If
        Next

        MsgBox("在庫ｺｰﾄﾞは登録されていません")

    End Sub

    Private Sub btnTanaorosi_Click(sender As System.Object, e As System.EventArgs) Handles btnTanaorosi.Click
        If MsgBox("印刷してよろしいですか？", MsgBoxStyle.YesNo + vbExclamation, "印刷確認") = MsgBoxResult.Yes Then
            Dim Ym As String = YmdBox1.getADYmStr()
            Dim Cn As New OleDbConnection(TopForm.DB_Drugs)
            Dim SQLCm As OleDbCommand = Cn.CreateCommand
            Dim Adapter As New OleDbDataAdapter(SQLCm)
            Dim Table As New DataTable

            SQLCm.CommandText = "select Zaiko as ｺｰﾄﾞ, Bunrui, Cod as カナ, Nam as 品名, Siire, Tani, Konyu, Tanka, SokS, YakS, GaiS, ByoS, ZaikoK, Ym, SokK, YakK, GaiK, ByoK from ZaikoM WHERE Ym = '" & Ym & "' Order by Bunrui, Nam"
            Adapter.Fill(Table)
            DataGridView2.DataSource = Table

            Dim dgv2rowcount As Integer = DataGridView2.Rows.Count

            If dgv2rowcount = 0 Then
                MsgBox("印刷対象のデータがありません")
                Return
            End If

            DataGridView2.Columns(6).DefaultCellStyle.Format = "#,0"
            DataGridView2.Columns(7).DefaultCellStyle.Format = "#,0.00"

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
            oSheet = objWorkBook.Worksheets("棚卸表改")

            objExcel.Calculation = Excel.XlCalculation.xlCalculationManual
            objExcel.ScreenUpdating = False

            Dim page As Integer = dgv2rowcount \ 36

            If page > 0 Then
                Dim xlRange As Excel.Range = oSheet.Cells.Range("A1:O41")
                xlRange.Copy()
                For i As Integer = 1 To page
                    Dim xlPasteRange As Excel.Range = oSheet.Range("A" & 41 * i + 1) 'ペースト先
                    oSheet.rows("1:41").copy(xlPasteRange)
                Next
            End If

            Dim cell(35, 12) As String

            Dim nowpage As Integer = 1
            Dim rowindex As Integer = 0
            Dim sokTotal, yakTotal, gaiTotal, byoTotal, zaikototal As Integer

            For row As Integer = 0 To dgv2rowcount - 1
                If rowindex = 36 Then
                    oSheet.Range("E" & nowpage * 41 - 40).Value = DataGridView2(13, 0).Value
                    oSheet.Range("N" & nowpage * 41 - 40).Value = nowpage & "頁"
                    oSheet.Range("B" & nowpage * 41 - 38, "N" & nowpage * 41 - 3).Value = cell
                    For r As Integer = 0 To 35
                        For c As Integer = 0 To 12
                            cell(r, c) = ""
                        Next
                    Next
                    nowpage = nowpage + 1
                    rowindex = 0
                End If

                For col As Integer = 0 To 12
                    If col = 1 Then
                        cell(rowindex, col) = Util.checkDBNullValue(Strings.Left(DataGridView2(1, row).Value, 1))
                    ElseIf col = 2 Then
                        cell(rowindex, col) = Util.checkDBNullValue(Strings.Left(DataGridView2(2, row).Value, 2))
                    ElseIf col >= 6 Then
                        If Util.checkDBNullValue(DataGridView2(col, row).Value) = 0 Then
                            cell(rowindex, col) = ""
                        Else
                            cell(rowindex, col) = Util.checkDBNullValue(DataGridView2(col, row).FormattedValue)
                        End If
                    Else
                        cell(rowindex, col) = Util.checkDBNullValue(DataGridView2(col, row).Value)
                    End If
                Next

                rowindex = rowindex + 1

                sokTotal = sokTotal + DataGridView2(14, row).Value
                yakTotal = yakTotal + DataGridView2(15, row).Value
                gaiTotal = gaiTotal + DataGridView2(16, row).Value
                byoTotal = byoTotal + DataGridView2(17, row).Value
                zaikototal = zaikototal + DataGridView2(12, row).Value
            Next

            oSheet.Range("E" & nowpage * 41 - 40).Value = DataGridView2(13, 0).Value
            oSheet.Range("N" & nowpage * 41 - 40).Value = nowpage & "頁"
            oSheet.Range("B" & nowpage * 41 - 38, "N" & nowpage * 41 - 3).Value = cell
            oSheet.Range("J" & nowpage * 41 - 2).Value = sokTotal
            oSheet.Range("K" & nowpage * 41 - 2).Value = yakTotal
            oSheet.Range("L" & nowpage * 41 - 2).Value = gaiTotal
            oSheet.Range("M" & nowpage * 41 - 2).Value = byoTotal
            oSheet.Range("N" & nowpage * 41 - 2).Value = zaikototal

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
        End If
    End Sub

    Private Sub btnGetumatusyuukei_Click(sender As System.Object, e As System.EventArgs) Handles btnGetumatusyuukei.Click
        If MsgBox("印刷してよろしいですか？", MsgBoxStyle.YesNo + vbExclamation, "印刷確認") = MsgBoxResult.Yes Then
            Dim ymd As Date = YmdBox1.getADYmStr() & "/01"
            Dim cell(11, 5) As String
            Dim nowcell(11, 2) As Integer
            Dim lastcell(11, 2) As Integer
            Dim wareki As String = ""
            For row As Integer = 0 To 11
                Dim ymprev As Date = ymd.AddMonths(-(11 - row))
                Dim Cn2 As New OleDbConnection(TopForm.DB_Drugs)
                Dim SQLCm2 As OleDbCommand = Cn2.CreateCommand
                Dim Adapter2 As New OleDbDataAdapter(SQLCm2)
                Dim Table2 As New DataTable
                SQLCm2.CommandText = "select sum(SokK), sum(YakK), sum(GaiK), sum(ByoK), sum(ZaikoK) from ZaikoM WHERE Ym = '" & ymprev.ToString("yyyy/MM") & "'"
                Adapter2.Fill(Table2)
                Dim dtb2rowcount As Integer = Table2.Rows.Count
                DataGridView2.DataSource = Table2

                Dim Cn3 As New OleDbConnection(TopForm.DB_Drugs)
                Dim SQLCm3 As OleDbCommand = Cn3.CreateCommand
                Dim Adapter3 As New OleDbDataAdapter(SQLCm3)
                Dim Table3 As New DataTable
                SQLCm3.CommandText = "select sum(Kingak) from SiireD WHERE Ymd LIKE '%" & ymprev.ToString("yyyy/MM") & "%'"
                Adapter3.Fill(Table3)
                Dim dtb3rowcount As Integer = Table3.Rows.Count
                DataGridView3.DataSource = Table3

                For col As Integer = 0 To 5
                    If col = 0 Then
                        cell(row, col) = Util.getKanji(Util.convADStrToWarekiStr(ymprev.ToString("yyyy/MM") & "/01")) & Strings.Mid(Util.convADStrToWarekiStr(ymprev.ToString("yyyy/MM") & "/01"), 2, 2) & "年"
                        cell(row, col + 1) = Strings.Mid(Util.convADStrToWarekiStr(ymprev.ToString("yyyy/MM") & "/01"), 5, 2) & "月"
                        If Util.checkDBNullValue(DataGridView3(0, 0).Value) = "" Then
                            nowcell(row, 1) = 0
                        Else
                            nowcell(row, 1) = DataGridView3(0, 0).Value
                        End If
                        If Util.checkDBNullValue(DataGridView2(4, 0).Value) = "" Then
                            nowcell(row, 0) = 0
                        Else
                            nowcell(row, 0) = DataGridView2(4, 0).Value
                        End If
                        If row > 0 Then
                            If cell(row, col) = wareki Then
                                cell(row, col) = ""
                            Else
                                cell(row, col) = Util.getKanji(Util.convADStrToWarekiStr(ymprev.ToString("yyyy/MM") & "/01")) & Strings.Mid(Util.convADStrToWarekiStr(ymprev.ToString("yyyy/MM") & "/01"), 2, 2) & "年"
                                wareki = Util.getKanji(Util.convADStrToWarekiStr(ymprev.ToString("yyyy/MM") & "/01")) & Strings.Mid(Util.convADStrToWarekiStr(ymprev.ToString("yyyy/MM") & "/01"), 2, 2) & "年"
                            End If
                            nowcell(row, 2) = Val(nowcell(row, 1)) - Val((Val(nowcell(row, 0)) - Val(nowcell(row - 1, 0))))
                        Else
                            wareki = Util.getKanji(Util.convADStrToWarekiStr(ymprev.ToString("yyyy/MM") & "/01")) & Strings.Mid(Util.convADStrToWarekiStr(ymprev.ToString("yyyy/MM") & "/01"), 2, 2) & "年"
                        End If
                    ElseIf 2 <= col AndAlso col <= 5 Then
                        cell(row, col) = Util.checkDBNullValue(DataGridView2(col - 2, 0).Value)
                    End If
                Next

                Dim lastymprev As Date = ymd.AddMonths(-(23 - row))
                Dim Cn4 As New OleDbConnection(TopForm.DB_Drugs)
                Dim SQLCm4 As OleDbCommand = Cn4.CreateCommand
                Dim Adapter4 As New OleDbDataAdapter(SQLCm4)
                Dim Table4 As New DataTable
                SQLCm4.CommandText = "select sum(ZaikoK) from ZaikoM WHERE Ym = '" & lastymprev.ToString("yyyy/MM") & "'"
                Adapter4.Fill(Table4)
                Dim dtb4rowcount As Integer = Table4.Rows.Count
                DataGridView4.DataSource = Table4

                Dim Cn5 As New OleDbConnection(TopForm.DB_Drugs)
                Dim SQLCm5 As OleDbCommand = Cn5.CreateCommand
                Dim Adapter5 As New OleDbDataAdapter(SQLCm5)
                Dim Table5 As New DataTable
                SQLCm5.CommandText = "select sum(Kingak) from SiireD WHERE Ymd LIKE '%" & lastymprev.ToString("yyyy/MM") & "%'"
                Adapter5.Fill(Table5)
                Dim dtb5rowcount As Integer = Table5.Rows.Count
                DataGridView5.DataSource = Table5

                '配列に代入
                lastcell(row, 0) = DataGridView4(0, 0).Value
                lastcell(row, 1) = DataGridView5(0, 0).Value
                If row > 0 Then
                    lastcell(row, 2) = Val(lastcell(row, 1)) - Val((Val(lastcell(row, 0)) - Val(lastcell(row - 1, 0))))
                End If

            Next

            Dim Cn6 As New OleDbConnection(TopForm.DB_Drugs)
            Dim SQLCm6 As OleDbCommand = Cn6.CreateCommand
            Dim Adapter6 As New OleDbDataAdapter(SQLCm6)
            Dim Table6 As New DataTable
            SQLCm6.CommandText = "select sum(ZaikoK) from ZaikoM WHERE Ym = '" & ymd.AddMonths(-24).ToString("yyyy/MM") & "'"
            Adapter6.Fill(Table6)
            Dim dtb6rowcount As Integer = Table6.Rows.Count
            DataGridView6.DataSource = Table6

            nowcell(0, 2) = Val(nowcell(0, 1)) - Val(Val(nowcell(0, 0)) - Val(lastcell(11, 0)))
            lastcell(0, 2) = Val(lastcell(0, 1)) - Val(Val(lastcell(0, 0)) - Val(Util.checkDBNullValue(DataGridView6(0, 0).Value)))

            For r As Integer = 0 To 11
                For c As Integer = 2 To 5
                    If Util.checkDBNullValue(cell(r, c)) = "" Then
                        cell(r, c) = ""
                        'Continue For
                    Else
                        cell(r, c) = CInt(cell(r, c)).ToString("#,0")
                    End If
                Next
            Next

            For r As Integer = 0 To 11
                For c As Integer = 0 To 2
                    If Util.checkDBNullValue(lastcell(r, c)) = "" Then
                        lastcell(r, c) = ""
                        'Else
                        '    lastcell(r, c) = CInt(lastcell(r, c)).ToString("#,0")
                    End If
                Next
            Next

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
            oSheet = objWorkBook.Worksheets("月別集計２改")

            objExcel.Calculation = Excel.XlCalculation.xlCalculationManual
            objExcel.ScreenUpdating = False

            oSheet.Range("E2").Value = Util.getKanji(Util.convADStrToWarekiStr(YmdBox1.getADYmStr & "/01")) & Strings.Mid(Util.convADStrToWarekiStr(YmdBox1.getADYmStr & "/01"), 2, 2) & "年" & Strings.Mid(Util.convADStrToWarekiStr(YmdBox1.getADYmStr & "/01"), 5, 2) & "月"
            oSheet.Range("B5", "G16").Value = cell
            oSheet.Range("H5", "J16").Value = nowcell
            oSheet.Range("K5", "M16").Value = lastcell

            objExcel.Calculation = Excel.XlCalculation.xlCalculationAutomatic
            objExcel.ScreenUpdating = True

            '保存
            objExcel.DisplayAlerts = False

            ' エクセル表示
            objExcel.Visible = True

            'objWorkBook.charts(1).refresh()

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
        End If
    End Sub

    Private Sub btnKinyuuhyou_Click(sender As System.Object, e As System.EventArgs) Handles btnKinyuuhyou.Click
        If MsgBox("印刷してよろしいですか？", MsgBoxStyle.YesNo + vbExclamation, "印刷確認") = MsgBoxResult.Yes Then
            Dim basyo, basyojpn As String
            Dim Ym As String = YmdBox1.getADYmStr()
            For basyoNo As Integer = 1 To 4
                If basyoNo = 1 Then '薬品庫
                    basyo = "Sok"
                    basyojpn = "薬品庫"
                ElseIf basyoNo = 2 Then '薬局
                    basyo = "Yak"
                    basyojpn = "薬局"
                ElseIf basyoNo = 3 Then '外来
                    basyo = "Gai"
                    basyojpn = "外来"
                ElseIf basyoNo = 4 Then '病棟
                    basyo = "Byo"
                    basyojpn = "病棟"
                Else
                    basyo = "Sok"
                    basyojpn = "薬品庫"
                End If

                Dim Cn2 As New OleDbConnection(TopForm.DB_Drugs)
                Dim SQLCm2 As OleDbCommand = Cn2.CreateCommand
                Dim Adapter2 As New OleDbDataAdapter(SQLCm2)
                Dim Table2 As New DataTable
                SQLCm2.CommandText = "select Nam as 品名, Zaiko as ｺｰﾄﾞ, Cod as カナ, Siire, Tani, Konyu, Tanka, SokS, YakS, GaiS, ByoS, ZaikoK, Ym, SokK, YakK, GaiK, ByoK, Bunrui from ZaikoM WHERE Ym = '" & Ym & "' and " & basyo & "B = 1 and Bunrui = '外用' Order by Nam"
                Adapter2.Fill(Table2)
                Dim dtb2rowcount As Integer = Table2.Rows.Count
                If dtb2rowcount > 0 Then
                    If dtb2rowcount Mod 22 <> 0 Then
                        For i As Integer = 1 To 21
                            If dtb2rowcount Mod 22 = 0 Then
                                Exit For
                            End If
                            Table2.Rows.Add()
                            dtb2rowcount = dtb2rowcount + 1
                        Next
                    End If
                End If

                DataGridView2.DataSource = Table2

                Dim Cn3 As New OleDbConnection(TopForm.DB_Drugs)
                Dim SQLCm3 As OleDbCommand = Cn3.CreateCommand
                Dim Adapter3 As New OleDbDataAdapter(SQLCm3)
                Dim Table3 As New DataTable
                SQLCm3.CommandText = "select Nam as 品名, Zaiko as ｺｰﾄﾞ, Cod as カナ, Siire, Tani, Konyu, Tanka, SokS, YakS, GaiS, ByoS, ZaikoK, Ym, SokK, YakK, GaiK, ByoK, Bunrui from ZaikoM WHERE Ym = '" & Ym & "' and " & basyo & "B = 1 and Bunrui = '注射' Order by Nam"
                Adapter3.Fill(Table3)
                Dim dtb3rowcount As Integer = Table3.Rows.Count
                If dtb3rowcount > 0 Then
                    If dtb3rowcount Mod 22 <> 0 Then
                        For i As Integer = 1 To 21
                            If dtb3rowcount Mod 22 = 0 Then
                                Exit For
                            End If
                            Table3.Rows.Add()
                            dtb3rowcount = dtb3rowcount + 1
                        Next
                    End If
                End If


                Dim Cn4 As New OleDbConnection(TopForm.DB_Drugs)
                Dim SQLCm4 As OleDbCommand = Cn4.CreateCommand
                Dim Adapter4 As New OleDbDataAdapter(SQLCm4)
                Dim Table4 As New DataTable
                SQLCm4.CommandText = "select Nam as 品名, Zaiko as ｺｰﾄﾞ, Cod as カナ, Siire, Tani, Konyu, Tanka, SokS, YakS, GaiS, ByoS, ZaikoK, Ym, SokK, YakK, GaiK, ByoK, Bunrui from ZaikoM WHERE Ym = '" & Ym & "' and " & basyo & "B = 1 and Bunrui = '内服' Order by Nam"
                Adapter4.Fill(Table4)
                Dim dtb4rowcount As Integer = Table4.Rows.Count
                If dtb4rowcount > 0 Then
                    If dtb4rowcount Mod 22 <> 0 Then
                        For i As Integer = 1 To 21
                            If dtb4rowcount Mod 22 = 0 Then
                                Exit For
                            End If
                            Table4.Rows.Add()
                            dtb4rowcount = dtb4rowcount + 1
                        Next
                    End If
                End If


                'DataTable2にTable3のデータとTable4のデータをくっつける
                Table2.Merge(Table3)
                Table2.Merge(Table4)


                Dim lastYm As String = YmdBox1.getPrevYmStr()
                Dim Cn5 As New OleDbConnection(TopForm.DB_Drugs)
                Dim SQLCm5 As OleDbCommand = Cn5.CreateCommand
                Dim Adapter5 As New OleDbDataAdapter(SQLCm5)
                Dim Table5 As New DataTable
                SQLCm5.CommandText = "select Nam as 品名, Zaiko as ｺｰﾄﾞ, Cod as カナ, Siire, Tani, Konyu, Tanka, SokS, YakS, GaiS, ByoS, ZaikoK, Ym, SokK, YakK, GaiK, ByoK, Bunrui, SokT, YakT, GaiT, ByoT from ZaikoM WHERE Ym = '" & YmdBox1.getPrevYm() & "' and " & basyo & "B = 1 and Bunrui = '外用' Order by Nam"
                Adapter5.Fill(Table5)
                Dim dtb5rowcount As Integer = Table5.Rows.Count
                If dtb5rowcount Mod 22 <> 0 Then
                    For i As Integer = 1 To 21
                        If dtb5rowcount Mod 22 = 0 Then
                            Exit For
                        End If
                        Table5.Rows.Add()
                        dtb5rowcount = dtb5rowcount + 1
                    Next
                End If
                DataGridView5.DataSource = Table5

                Dim Cn6 As New OleDbConnection(TopForm.DB_Drugs)
                Dim SQLCm6 As OleDbCommand = Cn6.CreateCommand
                Dim Adapter6 As New OleDbDataAdapter(SQLCm6)
                Dim Table6 As New DataTable
                SQLCm6.CommandText = "select Nam as 品名, Zaiko as ｺｰﾄﾞ, Cod as カナ, Siire, Tani, Konyu, Tanka, SokS, YakS, GaiS, ByoS, ZaikoK, Ym, SokK, YakK, GaiK, ByoK, Bunrui, SokT, YakT, GaiT, ByoT from ZaikoM WHERE Ym = '" & YmdBox1.getPrevYm() & "' and " & basyo & "B = 1 and Bunrui = '注射' Order by Nam"
                Adapter6.Fill(Table6)
                Dim dtb6rowcount As Integer = Table6.Rows.Count
                If dtb6rowcount Mod 22 <> 0 Then
                    For i As Integer = 1 To 21
                        If dtb6rowcount Mod 22 = 0 Then
                            Exit For
                        End If
                        Table6.Rows.Add()
                        dtb6rowcount = dtb6rowcount + 1
                    Next
                End If

                Dim Cn7 As New OleDbConnection(TopForm.DB_Drugs)
                Dim SQLCm7 As OleDbCommand = Cn4.CreateCommand
                Dim Adapter7 As New OleDbDataAdapter(SQLCm7)
                Dim Table7 As New DataTable
                SQLCm7.CommandText = "select Nam as 品名, Zaiko as ｺｰﾄﾞ, Cod as カナ, Siire, Tani, Konyu, Tanka, SokS, YakS, GaiS, ByoS, ZaikoK, Ym, SokK, YakK, GaiK, ByoK, Bunrui, SokT, YakT, GaiT, ByoT from ZaikoM WHERE Ym = '" & YmdBox1.getPrevYm() & "' and " & basyo & "B = 1 and Bunrui = '内服' Order by Nam"
                Adapter7.Fill(Table7)
                Dim dtb7rowcount As Integer = Table7.Rows.Count
                If dtb7rowcount Mod 22 <> 0 Then
                    For i As Integer = 1 To 21
                        If dtb7rowcount Mod 22 = 0 Then
                            Exit For
                        End If
                        Table7.Rows.Add()
                        dtb7rowcount = dtb7rowcount + 1
                    Next
                End If

                'DataTable5にTable6のデータとTable7のデータをくっつける
                Table5.Merge(Table6)
                Table5.Merge(Table7)

                Dim dgv2rowcount As Integer = DataGridView2.Rows.Count

                If dgv2rowcount = 0 Then
                    MsgBox("当月のデータが存在しません。在庫マスタよりデータを作成してください。")
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
                oSheet = objWorkBook.Worksheets("記入票改")

                objExcel.Calculation = Excel.XlCalculation.xlCalculationManual
                objExcel.ScreenUpdating = False



                Dim page As Integer = dgv2rowcount \ 23

                If page > 0 Then
                    Dim xlRange As Excel.Range = oSheet.Cells.Range("A1:H25")
                    xlRange.Copy()
                    For i As Integer = 1 To page
                        Dim xlPasteRange As Excel.Range = oSheet.Range("A" & 25 * i + 1) 'ペースト先
                        oSheet.rows("1:25").copy(xlPasteRange)
                    Next
                End If

                Dim cell(21, 5) As String

                Dim nowpage As Integer = 1
                Dim rowindex As Integer = 0
                Dim dgv5rowcount As Integer = DataGridView5.Rows.Count

                For row As Integer = 0 To dgv2rowcount - 1
                    If rowindex = 22 Then
                        oSheet.Range("C" & nowpage * 25 - 24).Value = Util.getKanji(Util.convADStrToWarekiStr(YmdBox1.getADYmStr & "/01")) & Strings.Mid(Util.convADStrToWarekiStr(YmdBox1.getADYmStr & "/01"), 2, 2) & "年" & Strings.Mid(Util.convADStrToWarekiStr(YmdBox1.getADYmStr & "/01"), 5, 2) & "月末"
                        oSheet.Range("D" & nowpage * 25 - 24).Value = basyojpn
                        oSheet.Range("E" & nowpage * 25 - 24).Value = DataGridView2(17, (nowpage - 1) * 22).Value
                        oSheet.Range("G" & nowpage * 25 - 24).Value = nowpage & "頁"
                        oSheet.Range("B" & nowpage * 25 - 22, "G" & nowpage * 25 - 1).Value = cell
                        For r As Integer = 0 To 21
                            For c As Integer = 0 To 5
                                cell(r, c) = ""
                            Next
                        Next
                        nowpage = nowpage + 1
                        rowindex = 0
                    End If

                    For col As Integer = 0 To 4
                        If col = 0 Then
                            cell(rowindex, col) = Util.checkDBNullValue(DataGridView2(col, row).Value)
                            cell(rowindex, 1) = ""
                            cell(rowindex, 3) = ""
                        ElseIf col = 2 Then
                            cell(rowindex, col) = Util.checkDBNullValue(DataGridView2(col - 1, row).Value)
                        ElseIf col = 4 Then
                            For r As Integer = 0 To dgv5rowcount - 1
                                If Util.checkDBNullValue(DataGridView2(0, row).Value) = Util.checkDBNullValue(DataGridView5(0, r).Value) Then
                                    If Util.checkDBNullValue(DataGridView5(basyoNo + 6, r).Value) = "0" Then
                                        cell(rowindex, 4) = ""
                                    Else
                                        cell(rowindex, 4) = Util.checkDBNullValue(DataGridView5(basyoNo + 6, r).Value)
                                    End If
                                    cell(rowindex, 5) = Util.checkDBNullValue(DataGridView5(basyoNo + 17, r).Value)
                                End If
                            Next
                        End If
                    Next

                    rowindex = rowindex + 1

                Next

                oSheet.Range("C" & nowpage * 25 - 24).Value = Util.getKanji(Util.convADStrToWarekiStr(YmdBox1.getADYmStr & "/01")) & Strings.Mid(Util.convADStrToWarekiStr(YmdBox1.getADYmStr & "/01"), 2, 2) & "年" & Strings.Mid(Util.convADStrToWarekiStr(YmdBox1.getADYmStr & "/01"), 5, 2) & "月末"
                oSheet.Range("D" & nowpage * 25 - 24).Value = basyojpn
                oSheet.Range("E" & nowpage * 25 - 24).Value = DataGridView2(17, (nowpage - 1) * 22).Value
                oSheet.Range("G" & nowpage * 25 - 24).Value = nowpage & "頁"
                oSheet.Range("B" & nowpage * 25 - 22, "G" & nowpage * 25 - 1).Value = cell

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
            Next
        End If
    End Sub

    Private Sub txtSuuryou_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSuuryou.KeyDown
        If e.KeyCode = Keys.Up Then
            txtZaiko.Focus()
        End If
    End Sub

    
End Class