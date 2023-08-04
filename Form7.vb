Imports System.Data.SqlClient
Public Class Form7
    Public dr As SqlDataReader
    Public cmd As New SqlCommand
    Dim a As Integer

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        connexion.login.Open()
        Dim requete As String = "SELECT * FROM user1 where iduser='" & Text1.Text & "'"
        cmd = New SqlCommand(requete, login)
        dr = cmd.ExecuteReader
        If dr.Read Then
            connexion.cin = dr.Item("iduser").ToString
            connexion.nu = dr.Item("nomuser").ToString
            connexion.pu = dr.Item("prenomuser").ToString
            Label1.Text = Nothing
            a = a + 1
        Else
            Label1.Text = "Adresse incorrect"
            a = 0
        End If
        dr.Close()
        connexion.login.Close()
        connexion.login.Open()
        Dim requete1 As String = "SELECT * FROM user1 where mpasse='" & Text2.Text & "'"
        cmd = New SqlCommand(requete1, login)
        dr = cmd.ExecuteReader
        If dr.Read Then

            connexion.mdp = dr.Item("mpasse").ToString
            Label2.Text = Nothing
            a = a + 1
            
        Else
            a = 0
            Label2.Text = " Mot de passe incorrect"
        End If
        dr.Close()
        If a = 2 Then
            Text1.Text = Nothing
            Text2.Text = Nothing
            Me.Hide()
            Form8.Show()
        End If
        connexion.login.Close()
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        Form10.Show()
        Me.Hide()

    End Sub

    Private Sub Text1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Text1.MouseMove
        label4.text = " Entrez CIN "
    End Sub

    
   
End Class