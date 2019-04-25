Imports System.Data.OleDb
Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Core

Public Class 在庫入力

    Private Sub 在庫入力_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        YmdBox1.setADStr(Today.ToString("yyyy/MM/dd"))
        KeyPreview = True

        Util.EnableDoubleBuffering(DataGridView1)
        DataGridView1.RowTemplate.Height = 18
    End Sub

    Private Sub 在庫入力_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

    End Sub

   




End Class