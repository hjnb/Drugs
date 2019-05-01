Imports System.Data.OleDb
Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Core

Public Class 在庫入力

    Private Sub 在庫入力_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        YmdBox1.setADStr(Today.ToString("yyyy/MM/dd"))
        KeyPreview = True
        lblnam.visible = False


        Util.EnableDoubleBuffering(DataGridView1)
        DataGridView1.RowTemplate.Height = 25

    End Sub

    Private Sub 在庫入力_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
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

    Private Sub cmbBasyo_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbBasyo.SelectedIndexChanged
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
            .Columns(4).Width = 60
            .Columns(5).Width = 60
            .Columns(6).Width = 60
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
        
    End Sub

    Private Sub FormUpdate()

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

                FormUpdate()

                Exit Sub

            End If
        Next

        MsgBox("在庫ｺｰﾄﾞは登録されていません")

    End Sub

    Private Sub btnTanaorosi_Click(sender As System.Object, e As System.EventArgs) Handles btnTanaorosi.Click
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
        oSheet.Range("N" & nowpage * 41 - 2).Value = zaikoTotal

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

    Private Sub btnGetumatusyuukei_Click(sender As System.Object, e As System.EventArgs) Handles btnGetumatusyuukei.Click

    End Sub

    Private Sub btnKinyuuhyou_Click(sender As System.Object, e As System.EventArgs) Handles btnKinyuuhyou.Click
        Dim Ym As String = YmdBox1.getADYmStr()
        Dim Cn2 As New OleDbConnection(TopForm.DB_Drugs)
        Dim SQLCm2 As OleDbCommand = Cn2.CreateCommand
        Dim Adapter2 As New OleDbDataAdapter(SQLCm2)
        Dim Table2 As New DataTable

        SQLCm2.CommandText = "select Zaiko as ｺｰﾄﾞ, Bunrui, Cod as カナ, Nam as 品名, Siire, Tani, Konyu, Tanka, SokS, YakS, GaiS, ByoS, ZaikoK, Ym, SokK, YakK, GaiK, ByoK from ZaikoM WHERE Ym = '" & Ym & "' Order by Bunrui, Nam"
        Adapter2.Fill(Table2)
        DataGridView2.DataSource = Table2

        Dim dgv2rowcount As Integer = DataGridView2.Rows.Count

        If dgv2rowcount = 0 Then
            MsgBox("印刷対象のデータがありません")
            Return
        End If


        'Dim Cn3 As New OleDbConnection(TopForm.DB_Drugs)
        'Dim SQLCm3 As OleDbCommand = Cn3.CreateCommand
        'Dim Adapter3 As New OleDbDataAdapter(SQLCm3)
        'Dim Table3 As New DataTable

        'SQLCm3.CommandText = "select Zaiko as ｺｰﾄﾞ, Bunrui, Cod as カナ, Nam as 品名, Siire, Tani, Konyu, Tanka, SokS, YakS, GaiS, ByoS, ZaikoK, Ym, SokK, YakK, GaiK, ByoK from ZaikoM WHERE Ym = '" & Ym & "' Order by Bunrui, Nam"
        'Adapter3.Fill(Table3)
        'DataGridView3.DataSource = Table3

        'Dim dgv3rowcount As Integer = DataGridView3.Rows.Count

        'If dgv3rowcount = 0 Then
        '    MsgBox("印刷対象のデータがありません")
        '    Return
        'End If

        'Dim Cn4 As New OleDbConnection(TopForm.DB_Drugs)
        'Dim SQLCm4 As OleDbCommand = Cn4.CreateCommand
        'Dim Adapter4 As New OleDbDataAdapter(SQLCm4)
        'Dim Table4 As New DataTable

        'SQLCm4.CommandText = "select Zaiko as ｺｰﾄﾞ, Bunrui, Cod as カナ, Nam as 品名, Siire, Tani, Konyu, Tanka, SokS, YakS, GaiS, ByoS, ZaikoK, Ym, SokK, YakK, GaiK, ByoK from ZaikoM WHERE Ym = '" & Ym & "' Order by Bunrui, Nam"
        'Adapter4.Fill(Table4)
        'DataGridView4.DataSource = Table4

        'Dim dgv4rowcount As Integer = DataGridView4.Rows.Count

        'If dgv4rowcount = 0 Then
        '    MsgBox("印刷対象のデータがありません")
        '    Return
        'End If

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

        Dim page As Integer = dgv2rowcount \ 60

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

        For row As Integer = 0 To dgv2rowcount - 1
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
End Class