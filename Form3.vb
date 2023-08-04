Imports System.Data.SqlClient
Public Class Form3
    Public dr As SqlDataReader
    Public cmd As New SqlCommand
    Dim a As String
    Dim s As String
    Sub totale()

        connexion.login.Open()
        Dim liste As Integer
        Dim requete As String = "SELECT qts FROM produit1 where codebarre='" & Text4.Text & "'"
        cmd = New SqlCommand(requete, login)
        dr = cmd.ExecuteReader
        dr.Read()
        liste = Me.dr.Item(0)
        dr.Close()
        connexion.login.Close()

        connexion.login.Open()
        Dim liste2 As Integer
        Dim requete2 As String = "SELECT sum(qdist) FROM distribution1 where codebarre='" & Text4.Text & "'"
        cmd = New SqlCommand(requete2, login)
        dr = cmd.ExecuteReader
        dr.Read()
        liste2 = Me.dr.Item(0)
        dr.Close()
        liste = liste - liste2
        connexion.login.Close()
        connexion.login.Open()
        Dim requete3 As String = "update produit1 set qts=" & liste & " where codebarre='" & Text4.Text & "'"
        cmd = New SqlCommand(requete3, login)
        dr = cmd.ExecuteReader
        connexion.login.Close()
    End Sub

    Sub afficher()

        connexion.login.Open()

        Dim liste As ListViewItem
        Dim requete As String = "SELECT * FROM distribution1"
        cmd = New SqlCommand(requete, login)
        dr = cmd.ExecuteReader
        ListView1.Items.Clear()

        While (dr.Read)
            liste = Me.ListView1.Items.Add(dr("iddist"))
            liste.SubItems.Add(dr("qdist"))
            liste.SubItems.Add(dr("united")).ToString()
            liste.SubItems.Add(dr("codebarre"))
            liste.SubItems.Add(dr("datedist"))
            liste.SubItems.Add(dr("idetabl"))
            liste.SubItems.Add(dr("iduser"))
            liste.SubItems.Add(dr("remarqe"))
        End While

        dr.Close()
        connexion.login.Close()
        nbtotale()
    End Sub
    Sub enregistrer()
        If Text1.Text = Nothing Then
            Label5.Text = "erreur ! id-achat vide."
        ElseIf Text2.Text = Nothing Then
            Label8.Text = "erreur ! nom de produit vide."
        Else
            Dim l As Integer
            Dim m As Integer
            Dim n As Integer
            login.Open()

            Dim requete12 As String = "SELECT iddist FROM distribution1 where iddist='" & Text1.Text & "'"
            cmd = New SqlCommand(requete12, login)
            dr = cmd.ExecuteReader
            If dr.Read Then
                Label24.Text = "erreur! ce n° de Reçu est déja existé "
            Else
                n = 1
                dr.Close()
            End If
            login.Close()
            login.Open()
            Dim requete As String = "SELECT codebarre FROM produit1 where codebarre='" & Text4.Text & "'"
            cmd = New SqlCommand(requete, login)
            dr = cmd.ExecuteReader
            If dr.Read Then
                l = 1
                dr.Close()
            Else
                Label22.Text = "erreur! ce codebarre n'existe pas "
            End If
            login.Close()
            login.Open()

            Dim requete0 As String = "SELECT idetabl FROM etable1 where idetabl='" & Text6.Text & "'"
            cmd = New SqlCommand(requete0, login)
            dr = cmd.ExecuteReader
            If dr.Read Then
                m = 1
                dr.Close()
            Else
                Label23.Text = "erreur! ce Code Grésa n'existe pas "
            End If
            login.Close()

            If l = 1 And m = 1 And n = 1 Then
                connexion.login.Open()

                cmd.CommandText = "insert into distribution1(iddist,qdist,united,codebarre,datedist,idetabl,iduser,remarqe)values('" & Text1.Text & "','" & Text2.Text & "','" & Text3.Text & "','" & Text4.Text & "','" & date5.Text & "','" & Text6.Text & "','" & Text7.Text & "','" & Text8.Text & "')"


                cmd.ExecuteNonQuery()
                Label22.Text = Nothing
                Label23.Text = Nothing
                Label24.Text = Nothing
                MsgBox("ajouter avec succés")
            End If
            connexion.login.Close()
            totale()
            Text1.Text = Nothing
            Text2.Text = Nothing
            Text4.Text = Nothing
            Text3.Text = Nothing
            Text6.Text = Nothing
            Text7.Text = Nothing
            Text8.Text = Nothing
            Label5.Text = Nothing
            Label8.Text = Nothing
            
            afficher()

            End If
            nbtotale()
    End Sub
    Sub supprimer()
        connexion.login.Open()

        If ListView1.SelectedItems.Count = 0 Then
            MsgBox(" s'il voous plait ,selectioné un produit.")
        Else
            If MsgBox("Voulez vous vraiment supprimer ?", MsgBoxStyle.YesNo, "...") = MsgBoxResult.Yes Then


                cmd.CommandText = "delete from distribution1 where iddist='" & ListView1.SelectedItems(0).Text.ToString & "'"

                cmd.ExecuteNonQuery()
            End If
            End If
            connexion.login.Close()
    End Sub
   
    Sub nbtotale()
        connexion.login.Open()
        Dim requete As String = "SELECT count(iddist) FROM distribution1"
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

            cmd.CommandText = "select * from distribution1 where iddist='" & ListView1.SelectedItems(0).Text.ToString & "'"

            cmd.ExecuteNonQuery()

            Dim dr As SqlDataReader = cmd.ExecuteReader()

            If dr.Read Then
                Me.Text1.Text = dr.Item(0).ToString 'iddist
                s = dr.Item(0).ToString
                Me.date5.Text = dr.Item(1).ToString 'datedist
                Me.Text2.Text = dr.Item(2)          'qauntite
                Me.Text4.Text = dr.Item(3).ToString 'code barre
                Me.Text6.Text = dr.Item(4).ToString 'idetabl
                Me.Text7.Text = dr.Item(5).ToString 'iduser
                Me.Text8.Text = dr.Item(6).ToString 'remarque
                Me.Text3.Text = dr.Item(7).ToString 'unite
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
            Label5.Text = "erreur ! N° de reçu vide."
        ElseIf Text2.Text = Nothing Then
            Label8.Text = "erreur ! quantité ou autre champ vide."
        Else
            Dim l As Integer
            Dim m As Integer
            Dim n As Integer
            login.Open()

            Dim requete12 As String = "SELECT iddist FROM distribution1 where iddist='" & Text1.Text & "'"
            cmd = New SqlCommand(requete12, login)
            dr = cmd.ExecuteReader
            If dr.Read And s <> Text1.Text Then
                Label24.Text = "erreur! ce n° de Reçu est déja existé "
            Else
                n = 1
                dr.Close()
                Label24.Text = Nothing
            End If
            login.Close()
            login.Open()
            Dim requete As String = "SELECT codebarre FROM produit1 where codebarre='" & Text4.Text & "'"
            cmd = New SqlCommand(requete, login)
            dr = cmd.ExecuteReader
            If dr.Read Then
                l = 1
                dr.Close()
                Label22.Text = Nothing
            Else
                Label22.Text = "erreur! ce codebarre n'existe pas "
            End If
            login.Close()
            login.Open()

            Dim requete0 As String = "SELECT idetabl FROM etable1 where idetabl='" & Text6.Text & "'"
            cmd = New SqlCommand(requete0, login)
            dr = cmd.ExecuteReader
            If dr.Read Then
                m = 1
                dr.Close()
                Label23.Text = Nothing
            Else
                Label23.Text = "erreur! ce Code Grésa n'existe pas "
            End If
            login.Close()

            If l = 1 And m = 1 And n = 1 Then

                connexion.login.Open()

                cmd.CommandText = "update distribution1 set iddist='" & Text1.Text & "',qdist=" & Text2.Text & ",united='" & Text3.Text & "',codebarre='" & Text4.Text & "',datedist='" & date5.Text & "',idetabl='" & Text6.Text & "',iduser='" & Text7.Text & "',remarqe='" & Text8.Text & "' where iddist='" & s & "'"
                cmd.ExecuteNonQuery()
                Text1.Text = Nothing
                Text2.Text = Nothing
                Text3.Text = Nothing
                Text4.Text = Nothing
                Text6.Text = Nothing
                Text7.Text = Nothing
                Text8.Text = Nothing
                Label5.Text = Nothing
                Label8.Text = Nothing
                Label22.Text = Nothing
                Label23.Text = Nothing
                Label24.Text = Nothing
                MsgBox("modifier avec succés")
            End If

            connexion.login.Close()
         
            
            afficher()
            End If
    End Sub



    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click

        If ComboBox1.Text = "Reçu N°" Then
            connexion.login.Open()

            Dim liste As ListViewItem
            Dim requete As String = "SELECT * FROM distribution1 where iddist like '" & TextBox5.Text & "%'"
            cmd = New SqlCommand(requete, login)

            dr = cmd.ExecuteReader

            ListView1.Items.Clear()

            While (dr.Read)
                liste = Me.ListView1.Items.Add(dr("iddist"))
                liste.SubItems.Add(dr("qdist"))
                liste.SubItems.Add(dr("united")).ToString()
                liste.SubItems.Add(dr("codebarre"))
                liste.SubItems.Add(dr("datedist"))
                liste.SubItems.Add(dr("idetabl"))
                liste.SubItems.Add(dr("iduser"))
                liste.SubItems.Add(dr("remarqe"))
            End While
            dr.Close()
            connexion.login.Close()
            nbtotale()
        ElseIf ComboBox1.Text = "codebarre" Then
            connexion.login.Open()

            Dim liste As ListViewItem
            Dim requete As String = "SELECT * FROM distribution1 where codebarre like '" & TextBox5.Text & "%'"
            cmd = New SqlCommand(requete, login)

            dr = cmd.ExecuteReader

            ListView1.Items.Clear()

            While (dr.Read)
                liste = Me.ListView1.Items.Add(dr("iddist"))
                liste.SubItems.Add(dr("qdist"))
                liste.SubItems.Add(dr("united")).ToString()
                liste.SubItems.Add(dr("codebarre"))
                liste.SubItems.Add(dr("datedist"))
                liste.SubItems.Add(dr("idetabl"))
                liste.SubItems.Add(dr("iduser"))
                liste.SubItems.Add(dr("remarqe"))
            End While
            dr.Close()
            connexion.login.Close()
        ElseIf ComboBox1.Text = "Code Gresa" Then
            connexion.login.Open()
            Dim liste As ListViewItem
            Dim requete As String = "SELECT * FROM distribution1 where idetabl like '" & TextBox5.Text & "%'"
            cmd = New SqlCommand(requete, login)
            dr = cmd.ExecuteReader
            ListView1.Items.Clear()
            While (dr.Read)
                liste = Me.ListView1.Items.Add(dr("iddist"))
                liste.SubItems.Add(dr("qdist"))
                liste.SubItems.Add(dr("united")).ToString()
                liste.SubItems.Add(dr("codebarre"))
                liste.SubItems.Add(dr("datedist"))
                liste.SubItems.Add(dr("idetabl"))
                liste.SubItems.Add(dr("iduser"))
                liste.SubItems.Add(dr("remarqe"))
            End While
            dr.Close()
            connexion.login.Close()
        End If
    End Sub



    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub
End Class