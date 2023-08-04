Imports System.Data.SqlClient
Public Class Form1
    Public dr As SqlDataReader
    Public cmd As New SqlCommand
    Dim a As String
    Dim s As String
    Sub qte()
        Dim qts1 As Integer
        Dim liste2 As Integer
        Dim liste As Integer
        Dim v As Integer
        Dim v1 As Integer

        connexion.login.Open()




        Dim requete11 As String = "select count(codebarre) from produit1 where codebarre in (select codebarre from achat1) and codebarre='" & a & "'"
        cmd = New SqlCommand(requete11, login)
        dr = cmd.ExecuteReader
        dr.Read()
        v = dr.Item(0)
        dr.Close()
        If v = 0 Then

            liste = 0
            Label13.Text = 0
        Else

            Dim requete As String = "SELECT sum(qachat) FROM achat1 where codebarre='" & a & "'"
            cmd = New SqlCommand(requete, login)
            dr = cmd.ExecuteReader

            dr.Read()
            liste = dr.Item(0)
            Label13.Text = liste
            dr.Close()


        End If

        Dim requete12 As String = "select count(codebarre) from produit1 where codebarre in (select codebarre from distribution1) and codebarre='" & a & "'"
        cmd = New SqlCommand(requete12, login)
        dr = cmd.ExecuteReader
        dr.Read()
        v1 = dr.Item(0)
        dr.Close()
        If v1 = 0 Then

            liste2 = 0
            Label11.Text = 0
        Else
            Dim requete2 As String = "SELECT sum(qdist) FROM distribution1 where codebarre='" & a & "'"
            cmd = New SqlCommand(requete2, login)
            dr = cmd.ExecuteReader

            dr.Read()
            liste2 = dr.Item(0)
            Label11.Text = liste2
            dr.Close()


        End If


        qts1 = liste - liste2


        connexion.login.Close()
        If qts1 > 0 Then
            connexion.login.Open()

            cmd.CommandText = "update produit1 set qts=" & qts1 & " where codebarre='" & a & "'"

            cmd.ExecuteNonQuery()


            connexion.login.Close()

            qts.Text = qts1
        ElseIf qts1 < 0 Then
            connexion.login.Open()
            cmd.CommandText = "update produit1 set qts=0 where codebarre='" & TextBox1.Text & "'"
            cmd.ExecuteNonQuery()
            connexion.login.Close()
            afficher()
            MsgBox("ereur ! stock est vide")
            qts.Text = "erreur"
            
        End If
       

        afficher()
    End Sub

    Sub afficher()

        connexion.login.Open()

        Dim liste As ListViewItem
        Dim requete As String = "SELECT * FROM produit1"
        cmd = New SqlCommand(requete, login)
        dr = cmd.ExecuteReader
        ListView1.Items.Clear()
        While (dr.Read)
            liste = Me.ListView1.Items.Add(dr("codebarre"))
            liste.SubItems.Add(dr("nomprod"))
            liste.SubItems.Add(dr("qts"))
            liste.SubItems.Add(dr("remarque"))

        End While
        dr.Close()
        connexion.login.Close()
        nbtotale()

    End Sub
    Sub enregistrer()
        If TextBox1.Text = Nothing Then
            Label5.Text = "erreur ! code barre vide."
        ElseIf TextBox2.Text = Nothing Then
            Label8.Text = "erreur ! nom de produit vide."
        Else
            Dim l As Integer
            login.Open()
            Dim requete As String = "SELECT codebarre FROM produit1 where codebarre='" & TextBox1.Text & "'"
            cmd = New SqlCommand(requete, login)
            dr = cmd.ExecuteReader
            If dr.Read Then
                Label15.Text = "erreur! ce codebarre est déja existé"
                dr.Close()
            Else
                l = 1
            End If
            login.Close()
            If l = 1 Then

                connexion.login.Open()

                cmd.CommandText = "insert into produit1(codebarre,nomprod,qts,remarque) values('" & TextBox1.Text & "','" & TextBox2.Text & "',0" & ",'" & TextBox3.Text & "')"

                cmd.ExecuteNonQuery()
                connexion.login.Close()
                Label15.Text = Nothing
            End If

            TextBox1.Text = Nothing
            TextBox2.Text = Nothing
            TextBox3.Text = Nothing
            Label5.Text = Nothing
            Label8.Text = Nothing

            afficher()
        End If
    End Sub
    Sub supprimer()
        connexion.login.Open()

        If ListView1.SelectedItems.Count = 0 Then
            MsgBox(" s'il voous plait ,selectioné un produit.")
        Else

            If MsgBox("Voulez vous vraiment supprimer ?", MsgBoxStyle.YesNo, "...") = MsgBoxResult.Yes Then


                cmd.CommandText = "delete from achat1 where codebarre='" & ListView1.SelectedItems(0).Text.ToString & "'"

                cmd.ExecuteNonQuery()
                cmd.CommandText = "delete from distribution1 where codebarre='" & ListView1.SelectedItems(0).Text.ToString & "'"

                cmd.ExecuteNonQuery()

                cmd.CommandText = "delete from produit1 where codebarre='" & ListView1.SelectedItems(0).Text.ToString & "'"

                cmd.ExecuteNonQuery()
                TextBox1.Text = Nothing
                TextBox2.Text = Nothing
                TextBox3.Text = Nothing
            End If
            End If
            connexion.login.Close()
    End Sub
   
    Sub nbtotale()
        connexion.login.Open()
        Dim requete As String = "SELECT count(codebarre) FROM produit1"
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
        afficher()
        afficher()
        afficher()

    End Sub

    Private Sub ListView1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.Click
        connexion.login.Open()

        If ListView1.SelectedItems.Count = 0 Then
            MsgBox(" s'il voous plait ,selectioné un produit.")
        Else

            cmd.CommandText = "select * from produit1 where codebarre='" & ListView1.SelectedItems(0).Text.ToString & "'"

            cmd.ExecuteNonQuery()

            Dim dr As SqlDataReader = cmd.ExecuteReader()

            If dr.Read Then
                Me.TextBox1.Text = dr.Item(0).ToString
                a = dr.Item(0).ToString
                Me.TextBox2.Text = dr.Item(1).ToString
                Me.qts.Text = dr.Item(2)
                Me.TextBox3.Text = dr.Item(3).ToString
                dr.Close()

            End If

        End If

        connexion.login.Close()

        qte()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        supprimer()
        afficher()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        If TextBox1.Text = Nothing Then
            Label5.Text = "erreur ! code barre vide."
        ElseIf TextBox2.Text = Nothing Then
            Label8.Text = "erreur ! nom de produit vide."
        Else
            Dim l As Integer
            login.Open()
            Dim requete As String = "SELECT codebarre FROM produit1 where codebarre='" & TextBox1.Text & "'"
            cmd = New SqlCommand(requete, login)
            dr = cmd.ExecuteReader
            If dr.Read And a <> TextBox1.Text Then
                Label15.Text = "erreur! ce codebarre est déja existé"
                dr.Close()
            Else
                l = 1
            End If
            login.Close()

            Dim m As Integer
            login.Open()
            Dim requete11 As String = "SELECT codebarre FROM distribution1 where codebarre='" & a & "'"
            cmd = New SqlCommand(requete11, login)
            dr = cmd.ExecuteReader
            If dr.Read Then
                m = 0
                dr.Close()
            Else
                m = 1
            End If
            login.Close()

            s = TextBox1.Text
            If m = 0 And TextBox1.Text <> a Then
                If MsgBox("Ce modification a été faire automatiquement sur  code barre qui se trouve dans achat et distribution ! Voulez vous vraiment modifier ?", MsgBoxStyle.YesNo, "...") = MsgBoxResult.Yes Then

                    enregistrer()
                    connexion.login.Open()
                    cmd.CommandText = "update achat1 set codebarre='" & s & "' where codebarre='" & a & "'"
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "update distribution1 set codebarre='" & s & "' where codebarre='" & a & "'"
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "delete from produit1 where codebarre='" & a & "'"
                    cmd.ExecuteNonQuery()
                    connexion.login.Close()
                    afficher()
                End If
            End If
            If l = 1 And m = 1 Then
                connexion.login.Open()

                cmd.CommandText = "update produit1 set codebarre='" & TextBox1.Text & "',nomprod='" & TextBox2.Text & "',remarque='" & TextBox3.Text & "' where codebarre='" & a & "'"
                cmd.ExecuteNonQuery()
                Label15.Text = " "
            End If
            connexion.login.Close()
            TextBox1.Text = Nothing
            TextBox2.Text = Nothing
            TextBox3.Text = Nothing
            Label5.Text = Nothing
            Label8.Text = Nothing

            afficher()
            End If

    End Sub



    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click

        If ComboBox1.Text = "codebarre" Then
            connexion.login.Open()

            Dim liste As ListViewItem
            Dim requete As String = "SELECT * FROM produit1 where codebarre like '" & TextBox5.Text & "%'"
            cmd = New SqlCommand(requete, login)

            dr = cmd.ExecuteReader

            ListView1.Items.Clear()

            While (dr.Read)
                liste = Me.ListView1.Items.Add(dr("codebarre"))
                liste.SubItems.Add(dr("nomprod"))
                liste.SubItems.Add(dr("qts"))
                liste.SubItems.Add(dr("remarque"))
            End While
            dr.Close()
            connexion.login.Close()
            nbtotale()
        Else
            connexion.login.Open()
            Dim liste As ListViewItem
            Dim requete As String = "SELECT * FROM produit1 where nomprod like '" & TextBox5.Text & "%'"
            cmd = New SqlCommand(requete, login)
            dr = cmd.ExecuteReader
            ListView1.Items.Clear()
            While (dr.Read)
                liste = Me.ListView1.Items.Add(dr("codebarre"))
                liste.SubItems.Add(dr("nomprod"))
                liste.SubItems.Add(dr("qts"))
                liste.SubItems.Add(dr("remarque"))
            End While
            dr.Close()
            connexion.login.Close()
        End If
    End Sub




    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
