Public Class SiireDataGridView
    Inherits DataGridView

    Public Event keyDownEnter(ByVal sender As Object, ByVal e As EventArgs)

    Protected Overrides Function ProcessDataGridViewKey(e As System.Windows.Forms.KeyEventArgs) As Boolean
        If e.KeyCode = Keys.Enter Then
            RaiseEvent keyDownEnter(Me, New EventArgs)
            Return False
        Else
            Return MyBase.ProcessDataGridViewKey(e)
        End If
    End Function

End Class
