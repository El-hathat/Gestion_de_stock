Imports System.Data.SqlClient
Public Class Form4
    Public dr As SqlDataReader
    Public cmd As New SqlCommand
    Dim a As String
    Dim s As String

    Sub afficher()

        connexion.login.Open()

        Dim liste As ListViewItem
        Dim requete As String = "SELECT * FROM etable1"
        cmd = New SqlCommand(requete, login)
        dr = cmd.ExecuteReader
        ListView1.Items.Clear()

        While (dr.Read)
            liste = Me.ListView1.Items.Add(dr("idetabl"))
            liste.SubItems.Add(dr("nometabl"))
            liste.SubItems.Add(dr("addetabl"))
            liste.SubItems.Add(dr("teletabl"))
            liste.SubItems.Add(dr("nomdir"))
            liste.SubItems.Add(dr("email"))
            liste.SubItems.Add(dr("remarqueetabl"))
        End While

        dr.Close()
        connexion.login.Close()
        nbtotale()
    End Sub
    Sub enregistrer()
        If Text1.Text = Nothing Then
            Label5.Text = "erreur ! CODE GRISA vide."
        ElseIf Text2.Text = Nothing Then
            Label8.Text = "erreur ! NOM D'ETABLISSEMENT vide."
        Else
            Dim l As Integer
            login.Open()
            Dim requete As String = "SELECT idetabl FROM etable1 where idetabl='" & Text1.Text & "'"
            cmd = New SqlCommand(requete, login)
            dr = cmd.ExecuteReader
            If dr.Read Then
                Label11.Text = "erreur! ce Code Grésa est déja existé"
                dr.Close()
            Else
                l = 1
            End If
            login.Close()
            If l = 1 Then
                connexion.login.Open()

                cmd.CommandText = "insert into etable1(idetabl,nometabl,addetabl,teletabl,nomdir,email,remarqueetabl)values('" & Text1.Text & "','" & Text2.Text & "','" & Text3.Text & "','" & Text4.Text & "','" & Text5.Text & "','" & Text6.Text & "','" & Text7.Text & "')"


                cmd.ExecuteNonQuery()
                Label11.Text = Nothing

            End If
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

            End If
            nbtotale()
    End Sub
    Sub supprimer()
        connexion.login.Open()
        Dim v As Integer

        Dim requete11 As String = "select count(idetabl) from etable1 where idetabl in (select idetabl from distribution1) and idetabl='" & Text1.Text & "'"
        cmd = New SqlCommand(requete11, login)
        dr = cmd.ExecuteReader
        dr.Read()
        v = dr.Item(0)
        dr.Close()

        If ListView1.SelectedItems.Count = 0 Then
            MsgBox(" s'il voous plait ,selectioné un produit.")
        ElseIf v = 0 Then
            cmd.CommandText = "delete from etable1 where idetabl='" & ListView1.SelectedItems(0).Text.ToString & "'"

            cmd.ExecuteNonQuery()
        Else
            MsgBox("erreur ! cette etablissement liée au produit dans la distribution")


        End If

        connexion.login.Close()
    End Sub
    
    Sub nbtotale()
        connexion.login.Open()
        Dim requete As String = "SELECT count(idetabl) FROM etable1"
        cmd = New SqlCommand(requete, login)
        dr = cmd.ExecuteReader

        dr.Read()
        Me.TextBox6.Text = dr.Item(0).ToString

        dr.Close()
        connexion.login.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        enregistrer()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        afficher()
    End Sub

    Private Sub ListView1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.Click
        connexion.login.Open()

        If ListView1.SelectedItems.Count = 0 Then
            MsgBox(" s'il voous plait ,selectioné un produit.")
        Else

            cmd.CommandText = "select * from etable1 where idetabl='" & ListView1.SelectedItems(0).Text.ToString & "'"

            cmd.ExecuteNonQuery()

            Dim dr As SqlDataReader = cmd.ExecuteReader()

            If dr.Read Then
                Me.Text1.Text = dr.Item(0).ToString 'idetabl
                a = dr.Item(0).ToString
                Me.Text2.Text = dr.Item(1).ToString 'nometabl
                Me.Text3.Text = dr.Item(2).ToString         'addetabl
                Me.Text4.Text = dr.Item(3).ToString 'teletabl
                Me.Text5.Text = dr.Item(4).ToString 'nomdir
                Me.Text6.Text = dr.Item(6).ToString 'email
                Me.Text7.Text = dr.Item(5).ToString 'remarqueetabl

                dr.Close()

            End If

        End If
        connexion.login.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        supprimer()
        afficher()
    End Sub

   Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        s = Text1.Text
        If Text1.Text = Nothing Then
            Label5.Text = "erreur ! code Grisa vide."
        ElseIf Text2.Text = Nothing Then
            Label8.Text = "erreur ! Nom D'établissement ou autre champ vide."
        Else
            Dim l As Integer
            login.Open()
            Dim requete As String = "SELECT idetabl FROM etable1 where idetabl='" & Text1.Text & "'"
            cmd = New SqlCommand(requete, login)
            dr = cmd.ExecuteReader
            If dr.Read Then
                Label11.Text = "erreur! ce Code Grésa est déja existé"
                dr.Close()
            Else
                l = 1
            End If
            login.Close()

            Dim m As Integer
            login.Open()
            Dim requete11 As String = "SELECT idetabl FROM distribution1 where idetabl='" & a & "'"
            cmd = New SqlCommand(requete11, login)
            dr = cmd.ExecuteReader
            If dr.Read Then
                m = 0
                dr.Close()
            Else
                m = 1
            End If
            login.Close()
            s = Text1.Text

            If m = 0 Then
                If MsgBox("Ce modification a été faire automatiquement sur  Code Grésa qui se trouve dans Distribution ! Voulez vous vraiment modifier ?", MsgBoxStyle.YesNo, "...") = MsgBoxResult.Yes Then

                    enregistrer()
                    connexion.login.Open()
                    cmd.CommandText = "update distribution1 set idetabl='" & s & "' where idetabl='" & a & "'"
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "delete from etable1 where idetabl='" & a & "'"
                    cmd.ExecuteNonQuery()
                    connexion.login.Close()
                    afficher()
                End If
            Else
                If l = 1 And m = 1 Then
                    connexion.login.Open()

                    cmd.CommandText = "update etable1 set idetabl='" & Text1.Text & "',nometabl='" & Text2.Text & "',addetabl='" & Text3.Text & "',teletabl='" & Text4.Text & "',nomdir='" & Text5.Text & "',email='" & Text6.Text & "',remarqueetabl='" & Text7.Text & "' where idetabl='" & a & "'"
                    cmd.ExecuteNonQuery()
                    Label11.Text = " "
                    MsgBox("modifier avec succés")
                End If
                connexion.login.Close()
                Text1.Text = Nothing
                Text2.Text = Nothing
                Text3.Text = Nothing
                Text4.Text = Nothing
                Text5.Text = Nothing
                Text6.Text = Nothing
                Text7.Text = Nothing
                Label5.Text = Nothing
                afficher()
            End If
        End If
    End Sub



    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click

        If ComboBox1.Text = "Code Gresa" Then
            connexion.login.Open()

            Dim liste As ListViewItem
            Dim requete As String = "SELECT * FROM etable1 where idetabl like '" & TextBox5.Text & "%'"
            cmd = New SqlCommand(requete, login)

            dr = cmd.ExecuteReader

            ListView1.Items.Clear()
            While (dr.Read)
                liste = Me.ListView1.Items.Add(dr("idetabl"))
                liste.SubItems.Add(dr("nometabl"))
                liste.SubItems.Add(dr("addetabl")).ToString()
                liste.SubItems.Add(dr("teletabl"))
                liste.SubItems.Add(dr("nomdir"))
                liste.SubItems.Add(dr("email"))
                liste.SubItems.Add(dr("remarqueetabl"))
            End While
            dr.Close()
            connexion.login.Close()
            nbtotale()
        Else
            connexion.login.Open()
            Dim liste As ListViewItem
            Dim requete As String = "SELECT * FROM etable1 where nometabl like '" & TextBox5.Text & "%'"
            cmd = New SqlCommand(requete, login)
            dr = cmd.ExecuteReader
            ListView1.Items.Clear()
            While (dr.Read)
                liste = Me.ListView1.Items.Add(dr("idetabl"))
                liste.SubItems.Add(dr("nometabl"))
                liste.SubItems.Add(dr("addetabl")).ToString()
                liste.SubItems.Add(dr("teletabl"))
                liste.SubItems.Add(dr("nomdir"))
                liste.SubItems.Add(dr("email"))
                liste.SubItems.Add(dr("remarqueetabl"))
            End While
            dr.Close()
            connexion.login.Close()
        End If
    End Sub



    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub
End Class