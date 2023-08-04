Imports System.Data.SqlClient
Public Class Form6
    Public dr As SqlDataReader
    Public cmd As New SqlCommand
    Dim a As String
    Sub afficher()

        connexion.login.Open()

        Dim liste As ListViewItem
        Dim requete3 As String = "SELECT * FROM user1"
        cmd = New SqlCommand(requete3, login)
        dr = cmd.ExecuteReader
        ListView1.Items.Clear()

        While (dr.Read)
            liste = Me.ListView1.Items.Add(dr("iduser"))
            liste.SubItems.Add(dr("nomdp"))
            liste.SubItems.Add(dr("mpasse"))
            liste.SubItems.Add(dr("nomuser"))
            liste.SubItems.Add(dr("prenomuser"))
            liste.SubItems.Add(dr("tel"))
            liste.SubItems.Add(dr("remarque"))
        End While

        dr.Close()
        connexion.login.Close()
        nbtotale()
    End Sub
    Sub enregistrer()
        If Text1.Text = Nothing Then
            Label5.Text = "erreur ! code barre vide."
        ElseIf Text2.Text = Nothing Then
            Label8.Text = "erreur ! nom de produit vide."
        Else
            Dim l As Integer
            login.Open()
            Dim requete As String = "SELECT codebarre FROM produit1 where codebarre='" & Text1.Text & "'"
            cmd = New SqlCommand(requete, login)
            dr = cmd.ExecuteReader
            If dr.Read Then
                Label15.Text = "erreur! CIN  est déja existé"
                dr.Close()
            Else
                l = 1
            End If
            login.Close()
            If l = 1 Then

                connexion.login.Open()
                cmd.CommandText = "insert into user1(iduser,nomdp,mpasse,nomuser,prenomuser,tel,remarque) values('" & Text1.Text & "','" & Text2.Text & "','" & Text3.Text & "','" & Text4.Text & "','" & Text5.Text & "','" & Text6.Text & "','" & Text7.Text & "')"
                cmd.ExecuteNonQuery()
                connexion.login.Close()
                Text1.Text = Nothing
                Text2.Text = Nothing
                Text4.Text = Nothing
                Text3.Text = Nothing
                Text5.Text = Nothing
                Text6.Text = Nothing
                Text7.Text = Nothing
                Label5.Text = Nothing
                Label8.Text = Nothing
                afficher()
                MsgBox("ajouter avec succés")
            End If
        End If
        nbtotale()
        afficher()
    End Sub
    Sub supprimer()
        connexion.login.Open()

        If ListView1.SelectedItems.Count = 0 Then
            MsgBox(" s'il voous plait ,selectioné un utilisateur.")
        Else
            Dim v As Integer

            Dim requete11 As String = "select count(iduser) from user1 where iduser in (select iduser from achat1) and iduser='" & Text1.Text & "'"
            cmd = New SqlCommand(requete11, login)
            dr = cmd.ExecuteReader
            dr.Read()
            v = dr.Item(0)
            dr.Close()
            If v = 0 Then
                cmd.CommandText = "delete from user1 where iduser='" & ListView1.SelectedItems(0).Text.ToString & "'"

                cmd.ExecuteNonQuery()
            Else
                MsgBox("erreur ! ce utilisateur lié au produit dans l'achat")

            End If
        End If
        connexion.login.Close()
    End Sub

    Sub nbtotale()
        connexion.login.Open()
        Dim requete As String = "SELECT count(iduser) FROM user1"
        cmd = New SqlCommand(requete, login)
        dr = cmd.ExecuteReader

        dr.Read()
        Me.TextBox6.Text = dr.Item(0).ToString

        dr.Close()
        connexion.login.Close()
    End Sub

    Private Sub ListView1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.Click
        connexion.login.Open()

        If ListView1.SelectedItems.Count = 0 Then
            MsgBox(" s'il voous plait ,selectioné un utilisateur.")
        Else

            cmd.CommandText = "select * from user1 where iduser='" & ListView1.SelectedItems(0).Text.ToString & "'"

            cmd.ExecuteNonQuery()

            Dim dr As SqlDataReader = cmd.ExecuteReader()

            If dr.Read Then
                Me.Text1.Text = dr.Item(0).ToString 'idetabl
                Me.Text2.Text = dr.Item(1).ToString 'nometabl
                Me.Text3.Text = dr.Item(2).ToString         'addetabl
                Me.Text4.Text = dr.Item(3).ToString 'teletabl
                Me.Text5.Text = dr.Item(4).ToString 'nomdir
                Me.Text6.Text = dr.Item(5).ToString 'email
                Me.Text7.Text = dr.Item(6).ToString 'remarqueetabl

                dr.Close()
                a = Text1.Text
            End If

        End If
        connexion.login.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        supprimer()
        afficher()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        If Text1.Text = Nothing Then
            Label5.Text = "erreur ! CIN vide."
        ElseIf Text2.Text = Nothing Then
            Label8.Text = "erreur !  autre champ vide."
        Else
            connexion.login.Open()


            cmd.CommandText = "update user1 set iduser='" & Text1.Text & "',nomdp='" & Text2.Text & "',mpasse='" & Text3.Text & "',nomuser='" & Text4.Text & "',prenomuser='" & Text5.Text & "',tel='" & Text6.Text & "',remarque='" & Text7.Text & "' where iduser='" & a & "'"
            cmd.ExecuteNonQuery()


            connexion.login.Close()
            Text1.Text = " "
            Text2.Text = " "
            Text3.Text = " "
            Text4.Text = " "
            Text5.Text = " "
            Text6.Text = " "
            Text7.Text = " "
            Label5.Text = " "
            Label8.Text = " "
            afficher()
        End If
    End Sub



    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        If ComboBox1.Text = "CIN" Then
            connexion.login.Open()

            Dim liste As ListViewItem
            Dim requete As String = "SELECT * FROM  user1 where iduser like '" & TextBox5.Text & "%'"
            cmd = New SqlCommand(requete, login)

            dr = cmd.ExecuteReader

            ListView1.Items.Clear()

            While (dr.Read)
                liste = Me.ListView1.Items.Add(dr("iduser"))
                liste.SubItems.Add(dr("nomdp"))
                liste.SubItems.Add(dr("mpasse"))
                liste.SubItems.Add(dr("nomuser"))
                liste.SubItems.Add(dr("prenomuser"))
                liste.SubItems.Add(dr("tel"))
                liste.SubItems.Add(dr("remarque"))
            End While
            dr.Close()
            connexion.login.Close()
            nbtotale()
        Else
            connexion.login.Open()
            Dim liste As ListViewItem
            Dim requete As String = "SELECT * FROM user1 where nomuser like '" & TextBox5.Text & "%'"
            cmd = New SqlCommand(requete, login)
            dr = cmd.ExecuteReader
            ListView1.Items.Clear()
            While (dr.Read)
                liste = Me.ListView1.Items.Add(dr("iduser"))
                liste.SubItems.Add(dr("nomdp"))
                liste.SubItems.Add(dr("mpasse"))
                liste.SubItems.Add(dr("nomuser"))
                liste.SubItems.Add(dr("prenomuser"))
                liste.SubItems.Add(dr("tel"))
                liste.SubItems.Add(dr("remarque"))
            End While
            dr.Close()
            connexion.login.Close()
        End If
    End Sub



    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click

        Form7.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        afficher()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        enregistrer()
    End Sub
End Class