Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices

Public Class 仕入集計

    ''' <summary>
    ''' loadイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 仕入集計_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        '仕入先ボックス初期設定
        initSiireBox()

        '日付ボックス初期値設定
        initYmdBox()

        '初期フォーカス
        fromYmdBox.Focus()
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

        'エンターキー押下イベント制御用
        fromYmdBox.canEnterKeyDown = True
        toYmdBox.canEnterKeyDown = True
    End Sub

    ''' <summary>
    ''' 日付ボックスエンターキー押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub YmdBox_keyDownEnter(sender As Object, e As System.EventArgs) Handles fromYmdBox.keyDownEnterOrDown, toYmdBox.keyDownEnterOrDown
        Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
    End Sub

    ''' <summary>
    ''' 実行ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExecute_Click(sender As System.Object, e As System.EventArgs) Handles btnExecute.Click
        Dim fromYmd As String = fromYmdBox.getADStr() 'from日付
        Dim toYmd As String = toYmdBox.getADStr() 'to日付
        If rbtnSuryo.Checked OrElse rbtnKingak.Checked Then '数量順印刷 or 金額順印刷
            'BestWテーブル作成
            initBestW(fromYmd, toYmd)

            'データ取得
            Dim cnn As New ADODB.Connection
            cnn.Open(TopForm.DB_Drugs)
            Dim rs As New ADODB.Recordset
            Dim sql As String = ""
            If rbtnSuryo.Checked Then
                sql = "select Nam, Siire, Suryo, Gokei from BestW order by Suryo Desc"
            Else
                sql = "select Nam, Siire, Suryo, Gokei from BestW order by Gokei Desc"
            End If
            rs.Open(sql, cnn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockPessimistic)
            If rs.RecordCount <= 0 Then
                MsgBox("該当がありません。", MsgBoxStyle.Exclamation)
                rs.Close()
                cnn.Close()
                Return
            End If
            Dim rsSum As New ADODB.Recordset()
            sql = "select Sum(Suryo) as SS, Sum(Gokei) as SG from BestW"
            rsSum.Open(sql, cnn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockPessimistic)
            Dim totalSuryo As Integer = rsSum.Fields("SS").Value '合計数量
            Dim totalGokei As Integer = rsSum.Fields("SG").Value '合計仕入金額

            '書き込みデータ作成
            Dim dataArray(49, 6) As String
            Dim noCount As Integer = 1
            Dim rowIndex As Integer = 0
            Dim suryo As Integer = 0
            Dim gokei As Integer = 0
            While Not rs.EOF
                dataArray(rowIndex, 0) = noCount
                dataArray(rowIndex, 1) = Util.checkDBNullValue(rs.Fields("Nam").Value)
                dataArray(rowIndex, 2) = Util.checkDBNullValue(rs.Fields("Siire").Value)
                dataArray(rowIndex, 3) = CInt(rs.Fields("Suryo").Value).ToString("#,0")
                dataArray(rowIndex, 4) = CInt(rs.Fields("Gokei").Value).ToString("#,0")
                Dim percentStr As String = If(rbtnSuryo.Checked, formatNum(rs.Fields("Suryo").Value / totalSuryo * 100), formatNum(rs.Fields("Gokei").Value / totalGokei * 100))
                dataArray(rowIndex, 5) = percentStr
                suryo += CInt(rs.Fields("Suryo").Value)
                gokei += CInt(rs.Fields("Gokei").Value)
                If noCount Mod 5 = 0 Then
                    dataArray(rowIndex, 6) = If(rbtnSuryo.Checked, formatNum(suryo / totalSuryo * 100), formatNum(gokei / totalGokei * 100))
                End If

                noCount += 1
                If noCount = 51 Then
                    Exit While
                End If
                rowIndex += 1
                rs.MoveNext()
            End While
            rs.Close()
            cnn.Close()

            'エクセル準備
            Dim objExcel As Excel.Application = CreateObject("Excel.Application")
            Dim objWorkBooks As Excel.Workbooks = objExcel.Workbooks
            Dim objWorkBook As Excel.Workbook = objWorkBooks.Open(TopForm.excelFilePass)
            Dim oSheet As Excel.Worksheet = objWorkBook.Worksheets("仕入ベスト改")
            objExcel.Calculation = Excel.XlCalculation.xlCalculationManual
            objExcel.ScreenUpdating = False

            'データ書き込み
            oSheet.Range("D2").Value = If(rbtnSuryo.Checked, "仕入数量順", "仕入金額順") '区分
            oSheet.Range("F2").Value = Util.convADStrToWarekiStr(fromYmd) & " ～ " & Util.convADStrToWarekiStr(toYmd) '期間
            oSheet.Range("B5", "H54").Value = dataArray
            oSheet.Range("E55").Value = suryo.ToString("#,0")
            oSheet.Range("F55").Value = gokei.ToString("#,0")
            oSheet.Range("G55").Value = If(rbtnSuryo.Checked, formatNum(suryo / totalSuryo * 100), formatNum(gokei / totalGokei * 100))
            oSheet.Range("E56").Value = totalSuryo.ToString("#,0")
            oSheet.Range("F56").Value = totalGokei.ToString("#,0")

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
        ElseIf rbtnNam.Checked Then '品名別／月別　仕入れ数量印刷
            '対象仕入先、期間でSiireWテーブル作成
            Dim siire As String = siireBox.Text
            If siire = "" Then
                MsgBox("仕入先を選択して下さい。", MsgBoxStyle.Exclamation)
                Return
            End If
            initSiireW(fromYmd, toYmd, siire)

            'データ取得
            Dim cnn As New ADODB.Connection
            cnn.Open(TopForm.DB_Drugs)
            Dim rs As New ADODB.Recordset
            Dim sql As String = "select * from SiireW order by Nam, Tanka"
            rs.Open(sql, cnn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
            If rs.RecordCount <= 0 Then
                MsgBox("該当がありません。", MsgBoxStyle.Exclamation)
                rs.Close()
                cnn.Close()
                Return
            End If

            '書き込みデータ作成
            Dim dataList As New List(Of String(,))
            Dim noCount As Integer = 0
            Dim tmpNam As String = ""
            Dim dataArray(39, 16) As String
            Dim arrayRowIndex As Integer = -1
            Dim kingak As Integer = 0
            Dim suryo As Integer = 0
            While Not rs.EOF
                Dim nam As String = Util.checkDBNullValue(rs.Fields("Nam").Value)
                If nam <> tmpNam Then
                    '更新
                    noCount += 1
                    arrayRowIndex += 1
                    kingak = 0
                    suryo = 0
                    tmpNam = nam
                    If arrayRowIndex = 40 Then
                        For i As Integer = 0 To 39
                            For j As Integer = 2 To 16
                                dataArray(i, j) = CInt(dataArray(i, j)).ToString("#,0")
                                If dataArray(i, j) = "0" Then
                                    dataArray(i, j) = ""
                                End If
                            Next
                        Next
                        dataList.Add(dataArray.Clone())
                        Array.Clear(dataArray, 0, dataArray.Length)
                        arrayRowIndex = 0
                    End If

                    dataArray(arrayRowIndex, 0) = noCount 'No.
                    dataArray(arrayRowIndex, 1) = nam '品名
                    dataArray(arrayRowIndex, 15) = rs.Fields("Tanka").Value '単価
                End If

                Dim monthNum As Integer = CInt(rs.Fields("Ymd").Value.ToString().Split("/")(1)) '月
                Dim arrayColumnIndex As Integer = If(monthNum >= 4, monthNum - 2, monthNum + 10) '加算する列
                dataArray(arrayRowIndex, arrayColumnIndex) = dataArray(arrayRowIndex, arrayColumnIndex) + rs.Fields("Suryo").Value

                kingak += rs.Fields("Kingak").Value
                suryo += rs.Fields("Suryo").Value
                dataArray(arrayRowIndex, 16) = kingak '金額
                dataArray(arrayRowIndex, 14) = suryo '数量

                rs.MoveNext()
            End While
            For i As Integer = 0 To 39
                For j As Integer = 2 To 16
                    dataArray(i, j) = CInt(dataArray(i, j)).ToString("#,0")
                    If dataArray(i, j) = "0" Then
                        dataArray(i, j) = ""
                    End If
                Next
            Next
            dataList.Add(dataArray)

            rs.Close()
            cnn.Close()

            'エクセル準備
            Dim objExcel As Excel.Application = CreateObject("Excel.Application")
            Dim objWorkBooks As Excel.Workbooks = objExcel.Workbooks
            Dim objWorkBook As Excel.Workbook = objWorkBooks.Open(TopForm.excelFilePass)
            Dim oSheet As Excel.Worksheet = objWorkBook.Worksheets("仕入明細改")
            objExcel.Calculation = Excel.XlCalculation.xlCalculationManual
            objExcel.ScreenUpdating = False

            '共通部分
            oSheet.Range("F2").Value = siire '仕入先
            oSheet.Range("H2").Value = Util.convADStrToWarekiStr(fromYmd) & " ～ " & Util.convADStrToWarekiStr(toYmd) '期間
            oSheet.Range("R2").Value = "1頁" 'ページ数

            '必要枚数コピペ
            Dim loopCount As Integer
            If noCount Mod 40 = 0 Then
                loopCount = noCount \ 40 - 2
            Else
                loopCount = noCount \ 40 - 1
            End If
            For i As Integer = 0 To loopCount
                Dim xlPasteRange As Excel.Range = oSheet.Range("A" & (46 + (45 * i))) 'ペースト先
                oSheet.Rows("1:45").copy(xlPasteRange)
                oSheet.HPageBreaks.Add(oSheet.Range("A" & (46 + (45 * i)))) '改ページ
                oSheet.Range("R" & (47 + (45 * i))).Value = (i + 2) & "頁" 'ページ数
            Next

            'データ書き込み
            For i As Integer = 0 To dataList.Count - 1
                oSheet.Range("B" & (4 + (45 * i)), "R" & (43 + (45 * i))).Value = dataList(i)
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
        End If
    End Sub

    ''' <summary>
    ''' パーセント表記の変換処理
    ''' </summary>
    ''' <param name="calcResult"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function formatNum(calcResult As Double) As String
        Dim result As String = Math.Round(calcResult, 1, MidpointRounding.AwayFromZero)
        result = If(result.IndexOf(".") >= 0, result & "%", result & ".0%")
        Return result
    End Function

    ''' <summary>
    ''' SiireWテーブル作成
    ''' </summary>
    ''' <param name="fromYmd">from日付</param>
    ''' <param name="toYmd">to日付</param>
    ''' <param name="siire">仕入先</param>
    ''' <remarks></remarks>
    Private Sub initSiireW(fromYmd As String, toYmd As String, Optional siire As String = "")
        '既存データ削除
        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Drugs)
        Dim cmd As New ADODB.Command()
        cmd.ActiveConnection = cn
        cmd.CommandText = "delete from SiireW"
        cmd.Execute()

        '仕入先データ取得
        Dim sql As String
        If siire <> "" Then
            sql = "select * from SiireD where Siire = '" & siire & "' and ('" & fromYmd & "' <= Ymd and Ymd <= '" & toYmd & "')"
        Else
            sql = "select * from SiireD where ('" & fromYmd & "' <= Ymd and Ymd <= '" & toYmd & "')"
        End If
        Dim rsD As New ADODB.Recordset()
        rsD.Open(sql, cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockPessimistic)
        If rsD.RecordCount <= 0 Then
            rsD.Close()
            cn.Close()
            Return
        End If

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
    ''' BestWテーブル作成
    ''' </summary>
    ''' <param name="fromYmd">from日付</param>
    ''' <param name="toYmd">to日付</param>
    ''' <remarks></remarks>
    Private Sub initBestW(fromYmd As String, toYmd As String)
        '指定期間のSiireWテーブル作成
        initSiireW(fromYmd, toYmd)

        '既存BestWテーブル削除
        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Drugs)
        Dim cmd As New ADODB.Command()
        cmd.ActiveConnection = cn
        cmd.CommandText = "delete from BestW"
        cmd.Execute()

        'SiireWテーブルからBestWテーブル作成
        Dim tmpNam As String = ""
        Dim tmpSiire As String = ""
        Dim suryo As Integer = 0
        Dim gokei As Integer = 0
        Dim rsSiireW As New ADODB.Recordset()
        Dim sql As String = "select * from SiireW order by Nam, Siire"
        rsSiireW.Open(sql, cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockPessimistic)
        If rsSiireW.RecordCount <= 0 Then
            rsSiireW.Close()
            cn.Close()
            Return
        End If
        Dim rsBestW As New ADODB.Recordset()
        rsBestW.Open("BestW", cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockPessimistic)
        While Not rsSiireW.EOF
            Dim nam As String = Util.checkDBNullValue(rsSiireW.Fields("Nam").Value)
            Dim siire As String = Util.checkDBNullValue(rsSiireW.Fields("Siire").Value)
            If (nam <> tmpNam) OrElse (nam = tmpNam AndAlso siire <> tmpSiire) Then
                rsBestW.AddNew()
                rsBestW.Fields("Nam").Value = nam
                rsBestW.Fields("Siire").Value = siire

                '更新
                tmpNam = nam
                tmpSiire = siire
                suryo = 0
                gokei = 0
            End If
            suryo += rsSiireW.Fields("Suryo").Value
            gokei += rsSiireW.Fields("Gokei").Value
            rsBestW.Fields("Suryo").Value = suryo
            rsBestW.Fields("Gokei").Value = gokei

            rsSiireW.MoveNext()
        End While
        rsBestW.Update()
        rsBestW.Close()
        rsSiireW.Close()
        cn.Close()

    End Sub
End Class