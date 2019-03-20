Public Class TopForm
    'データベースのパス
    Public dbFilePath As String = My.Application.Info.DirectoryPath & "\Drugs.mdb"
    Public DB_Drugs As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & dbFilePath

    'エクセルのパス
    Public excelFilePass As String = My.Application.Info.DirectoryPath & "\Drugs.xls"

    '.iniファイルのパス
    Public iniFilePath As String = My.Application.Info.DirectoryPath & "\Drugs.ini"

    '画像パス
    Public imageFilePath As String = My.Application.Info.DirectoryPath & "\Drugs.png"

    '各フォーム
    Private siireDataForm As 仕入データ入力
    Private siireSyukeiForm As 仕入集計
    Private siireJyokyoForm As 仕入状況
    Private siireKensakuForm As 仕入品名検索
    Private zaikoMForm As 在庫マスタ
    Private zaikoNyuryokuForm As 在庫入力
    Private zaikoKonyuForm As 在庫購入価
    Private dbSeiriForm As ＤＢ整理

    ''' <summary>
    ''' loadイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TopForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'データベース、エクセル、構成ファイルの存在チェック
        If Not System.IO.File.Exists(dbFilePath) Then
            MsgBox("データベースファイルが存在しません。ファイルを配置して下さい。")
            Me.Close()
            Exit Sub
        End If

        If Not System.IO.File.Exists(excelFilePass) Then
            MsgBox("エクセルファイルが存在しません。ファイルを配置して下さい。")
            Me.Close()
            Exit Sub
        End If

        If Not System.IO.File.Exists(iniFilePath) Then
            MsgBox("構成ファイルが存在しません。ファイルを配置して下さい。")
            Me.Close()
            Exit Sub
        End If

        If Not System.IO.File.Exists(imageFilePath) Then
            MsgBox("画像ファイルが存在しません。ファイルを配置して下さい。")
            Me.Close()
            Exit Sub
        End If

        '画面サイズ等
        Me.WindowState = FormWindowState.Maximized
        Me.MinimizeBox = False
        Me.MaximizeBox = False

        '画像の配置処理
        topPicture.ImageLocation = imageFilePath

        '印刷ラジオボタンの初期設定
        initPrintState()
    End Sub

    ''' <summary>
    ''' 印刷ラジオボタン初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initPrintState()
        Dim state As String = Util.getIniString("System", "Printer", iniFilePath)
        If state = "Y" Then
            rbtnPrintout.Checked = True
        Else
            rbtnPreview.Checked = True
        End If
    End Sub

    Private Sub rbtnPreview_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbtnPreview.CheckedChanged
        If rbtnPreview.Checked = True Then
            Util.putIniString("System", "Printer", "N", iniFilePath)
        End If
    End Sub

    Private Sub rbtnPrint_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbtnPrintout.CheckedChanged
        If rbtnPrintout.Checked = True Then
            Util.putIniString("System", "Printer", "Y", iniFilePath)
        End If
    End Sub

    ''' <summary>
    ''' 画像クリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub topPicture_Click(sender As System.Object, e As System.EventArgs) Handles topPicture.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' 仕入データ入力ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSiireData_Click(sender As System.Object, e As System.EventArgs) Handles btnSiireData.Click
        If IsNothing(siireDataForm) OrElse siireDataForm.IsDisposed Then
            siireDataForm = New 仕入データ入力()
            siireDataForm.Owner = Me
            siireDataForm.Show()
        End If
    End Sub

    ''' <summary>
    ''' 仕入集計ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSiiresyukei_Click(sender As System.Object, e As System.EventArgs) Handles btnSiiresyukei.Click
        If IsNothing(siireSyukeiForm) OrElse siireSyukeiForm.IsDisposed Then
            siireSyukeiForm = New 仕入集計()
            siireSyukeiForm.Owner = Me
            siireSyukeiForm.Show()
        End If
    End Sub

    ''' <summary>
    ''' 仕入状況ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSiireJyokyo_Click(sender As System.Object, e As System.EventArgs) Handles btnSiireJyokyo.Click
        If IsNothing(siireJyokyoForm) OrElse siireJyokyoForm.IsDisposed Then
            siireJyokyoForm = New 仕入状況()
            siireJyokyoForm.Owner = Me
            siireJyokyoForm.Show()
        End If
    End Sub

    ''' <summary>
    ''' 仕入品名検索ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSiirekensaku_Click(sender As System.Object, e As System.EventArgs) Handles btnSiirekensaku.Click
        If IsNothing(siireKensakuForm) OrElse siireKensakuForm.IsDisposed Then
            siireKensakuForm = New 仕入品名検索()
            siireKensakuForm.Owner = Me
            siireKensakuForm.Show()
        End If
    End Sub

    ''' <summary>
    ''' 在庫マスタボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnZaikoM_Click(sender As System.Object, e As System.EventArgs) Handles btnZaikoM.Click
        If IsNothing(zaikoMForm) OrElse zaikoMForm.IsDisposed Then
            zaikoMForm = New 在庫マスタ()
            zaikoMForm.Owner = Me
            zaikoMForm.Show()
        End If
    End Sub

    ''' <summary>
    ''' 在庫入力ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnZaikoNyuryoku_Click(sender As System.Object, e As System.EventArgs) Handles btnZaikoNyuryoku.Click
        If IsNothing(zaikoNyuryokuForm) OrElse zaikoNyuryokuForm.IsDisposed Then
            zaikoNyuryokuForm = New 在庫入力()
            zaikoNyuryokuForm.Owner = Me
            zaikoNyuryokuForm.Show()
        End If
    End Sub

    ''' <summary>
    ''' 在庫購入価ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnZaikoKonyu_Click(sender As System.Object, e As System.EventArgs) Handles btnZaikoKonyu.Click
        If IsNothing(zaikoKonyuForm) OrElse zaikoKonyuForm.IsDisposed Then
            zaikoKonyuForm = New 在庫購入価()
            zaikoKonyuForm.Owner = Me
            zaikoKonyuForm.Show()
        End If
    End Sub

    ''' <summary>
    ''' ＤＢ整理ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDB_Click(sender As System.Object, e As System.EventArgs) Handles btnDB.Click
        If IsNothing(dbSeiriForm) OrElse dbSeiriForm.IsDisposed Then
            dbSeiriForm = New ＤＢ整理()
            dbSeiriForm.Owner = Me
            dbSeiriForm.Show()
        End If
    End Sub
End Class
