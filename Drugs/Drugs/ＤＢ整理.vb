Public Class ＤＢ整理

    Public Sub New()
        InitializeComponent()
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.MinimizeBox = False
        Me.MaximizeBox = False
    End Sub

    Private Sub ＤＢ整理_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    ''' <summary>
    ''' 実行ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExecute_Click(sender As System.Object, e As System.EventArgs) Handles btnExecute.Click
        deleteProgressBar.Minimum = 0
        deleteProgressBar.Maximum = 100
        deleteProgressBar.Value = 0

        '現在年月日
        Dim nowYmdStr As String = Today.ToString("yyyy/MM/dd")
        '５年前年月日
        Dim targetYmdStr As String = Today.AddYears(-5).ToString("yyyy/MM/dd")
        Dim targetYmStr As String = Today.AddYears(-5).ToString("yyyy/MM")

        Dim cnn As New ADODB.Connection
        cnn.Open(TopForm.DB_Drugs)

        '仕入データ削除
        Dim cmd As New ADODB.Command()
        cmd.ActiveConnection = cnn
        cmd.CommandText = "delete from SiireD where Ymd < '" & targetYmdStr & "'"
        cmd.Execute()
        deleteProgressBar.Value = 50

        '在庫マスタデータ削除
        cmd.CommandText = "delete from ZaikoM where Ym < '" & targetYmStr & "'"
        cmd.Execute()
        cnn.Close()

        deleteProgressBar.Value = 100

        MsgBox("データを削除しました。" & Environment.NewLine & "単独モードでDBCompactを実行して下さい。", MsgBoxStyle.Information)
        Me.Close()
    End Sub

End Class