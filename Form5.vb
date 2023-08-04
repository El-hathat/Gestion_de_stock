Imports System.Data.SqlClient
Public Class Form5
    Public dr As SqlDataReader
    Public cmd As New SqlCommand
    Dim a As String
    Dim s As String
    Sub afficher()

        connexion.login.Open()

        Dim liste As ListViewItem
        Dim requete As String = "SELECT * FROM for1"
        cmd = New SqlCommand(requete, login)
        dr = cmd.ExecuteReader
        ListView1.Items.Clear()
        While (dr.Read)
            liste = Me.ListView1.Items.Add(dr("idfor"))
            liste.SubItems.Add(dr("num_marche"))
            liste.SubItems.Add(dr("addfor"))
            liste.SubItems.Add(dr("telfor"))
            liste.SubItems.Add(dr("ice"))
            liste.SubItems.Add(dr("fax"))
            liste.SubItems.Add(dr("remarquefor"))
        End While

        dr.Close()
        connexion.login.Close()
        nbtotale()
    End Sub
    Sub enregistrer()
        If Text1.Text = Nothing Then
            Label5.Text = "erreur ! nom de fornisseur vide."
        ElseIf Text2.Text = Nothing Then
            Label8.Text = "erreur ! num_marche vide."
        Else
            Dim l As Integer
            login.Open()
            Dim requete As String = "SELECT idfor FROM for1 where idfor='" & Text1.Text & "'"
            cmd = New SqlCommand(requete, login)
            dr = cmd.ExecuteReader
            If dr.Read Then
                Label12.Text = "erreur! ce Nom_Fournisseur est déja existé"
                dr.Close()
            Else
                l = 1
            End If
            login.Close()
            If l = 1 Then
                connexion.login.Open()

                cmd.CommandText = "insert into for1(idfor,num_marche,addfor,telfor,ice,fax,remarquefor) values('" & Text1.Text & "','" & Text2.Text & "','" & Text3.Text & "','" & Text4.Text & "','" & Text5.Text & "','" & Text6.Text & "','" & Text7.Text & "')"
                cmd.ExecuteNonQuery()
                Label12.Text = " "

            End If
            connexion.login.Close()
            Text1.Text = " "
            Text2.Text = " "
            Text4.Text = " "
            Text3.Text = " "
            Text5.Text = " "
            Text6.Text = " "
            Text7.Text = " "
            Label5.Text = " "
            Label8.Text = " "
            afficher()

            End If
            nbtotale()
    End Sub
    Sub supprimer()
        connexion.login.Open()

        If ListView1.SelectedItems.Count = 0 Then
            MsgBox(" s'il voous plait ,selectioné un fornisseur.")
        Else
            Dim v As Integer

            Dim requete11 As String = "select count(idfor) from for1 where idfor in (select idfor from achat1) and idfor='" & Text1.Text & "'"
            cmd = New SqlCommand(requete11, login)
            dr = cmd.ExecuteReader
            dr.Read()
            v = dr.Item(0)
            dr.Close()
            If v = 0 Then
                cmd.CommandText = "delete from for1 where idfor='" & ListView1.SelectedItems(0).Text.ToString & "'"

                cmd.ExecuteNonQuery()
            Else
                MsgBox("erreur ! ce fournisseur lié au produit dans l'achat")

            End If
        End If
        connexion.login.Close()
    End Sub
   
    Sub nbtotale()
        connexion.login.Open()
        Dim requete As String = "SELECT count(idfor) FROM for1"
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
            MsgBox(" s'il voous plait ,selectioné un fornisseur.")
        Else

            cmd.CommandText = "select * from for1 where idfor='" & ListView1.SelectedItems(0).Text.ToString & "'"

            cmd.ExecuteNonQuery()

            Dim dr As SqlDataReader = cmd.ExecuteReader()

            If dr.Read Then
                Me.Text1.Text = dr.Item(0).ToString 'idetabl
                a = dr.Item(0).ToString
                Me.Text2.Text = dr.Item(1).ToString 'nometabl
                Me.Text3.Text = dr.Item(2).ToString         'addetabl
                Me.Text4.Text = dr.Item(3).ToString 'teletabl
                Me.Text5.Text = dr.Item(4).ToString 'nomdir
                Me.Text6.Text = dr.Item(5).ToString 'email
                Me.Text7.Text = dr.Item(6).ToString 'remarqueetabl

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

        If Text1.Text = Nothing Then
            Label5.Text = "erreur ! Non de Fournisseur vide."
        ElseIf Text2.Text = Nothing Then
            Label8.Text = "erreur ! Num_marché ou autre champ vide."
        Else
            Dim l As Integer
            login.Open()
            Dim requete As String = "SELECT idfor FROM for1 where idfor='" & Text1.Text & "'"
            cmd = New SqlCommand(requete, login)
            dr = cmd.ExecuteReader
            If dr.Read And a <> Text1.Text Then
                Label12.Text = "erreur! ce Nom_Fournisseur est déja existé"
                dr.Close()
            Else
                l = 1
            End If
            login.Close()
            Dim m As Integer
            login.Open()
            Dim requete11 As String = "SELECT idfor FROM achat1 where idfor='" & a & "'"
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
                If MsgBox("Ce modification a été faire automatiquement sur  Nom_Fournisseur qui se trouve dans achat ! Voulez vous vraiment modifier ?", MsgBoxStyle.YesNo, "...") = MsgBoxResult.Yes Then

                    enregistrer()
                    connexion.login.Open()
                    cmd.CommandText = "update achat1 set idfor='" & s & "' where idfor='" & a & "'"
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "delete from for1 where idfor='" & a & "'"
                    cmd.ExecuteNonQuery()
                    connexion.login.Close()
                    afficher()
                End If
            Else
                If l = 1 And m = 1 Then
                    connexion.login.Open()
                    cmd.CommandText = "update for1 set idfor='" & Text1.Text & "',num_marche='" & Text2.Text & "',addfor='" & Text3.Text & "',telfor='" & Text4.Text & "',ice='" & Text5.Text & "',fax='" & Text6.Text & "',remarquefor='" & Text7.Text & "' where idfor='" & a & "'"
                    cmd.ExecuteNonQuery()
                    Label12.Text = Nothing
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
                Label8.Text = Nothing
                afficher()
            End If
            End If
    End Sub



    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click

        If ComboBox1.Text = "Nom.F" Then
            connexion.login.Open()

            Dim liste As ListViewItem
            Dim requete As String = "SELECT * FROM for1 where idfor like '" & TextBox5.Text & "%'"
            cmd = New SqlCommand(requete, login)

            dr = cmd.ExecuteReader

            ListView1.Items.Clear()
            While (dr.Read)
                liste = Me.ListView1.Items.Add(dr("idfor"))
                liste.SubItems.Add(dr("num_marche"))
                liste.SubItems.Add(dr("addfor")).ToString()
                liste.SubItems.Add(dr("telfor"))
                liste.SubItems.Add(dr("ice"))
                liste.SubItems.Add(dr("fax"))
                liste.SubItems.Add(dr("remarquefor"))
            End While
            dr.Close()
            connexion.login.Close()
            nbtotale()
        Else
            connexion.login.Open()
            Dim liste As ListViewItem
            Dim requete As String = "SELECT * FROM for1 where num_marche like '" & TextBox5.Text & "%'"
            cmd = New SqlCommand(requete, login)
            dr = cmd.ExecuteReader
            ListView1.Items.Clear()
            While (dr.Read)
                liste = Me.ListView1.Items.Add(dr("idfor"))
                liste.SubItems.Add(dr("num_marche"))
                liste.SubItems.Add(dr("addfor")).ToString()
                liste.SubItems.Add(dr("telfor"))
                liste.SubItems.Add(dr("ice"))
                liste.SubItems.Add(dr("fax"))
                liste.SubItems.Add(dr("remarquefor"))
            End While
            dr.Close()
            connexion.login.Close()
        End If
    End Sub



    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub
End Class