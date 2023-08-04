Public Class Form10

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim a As String
        a = "1234567890"
        If TextBox1.Text = a Then
            Form6.Show()
            TextBox1.Text = " "
            Me.Hide()
        ElseIf TextBox1.Text = Nothing Then

            Label2.Text = "ce champ est vide !"
        Else
            Label2.Text = "votre code est incorrect !"
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Show()
    End Sub
End Class