Public Class 仕入状況

    Private Sub 仕入状況_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        '仕入先ボックス初期設定
        initSiireBox()
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

End Class