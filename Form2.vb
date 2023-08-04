Imports System.Data.SqlClient
Public Class Form2
    Public dr As SqlDataReader
    Public cmd As New SqlCommand
    Dim s As String
   

    Sub afficher()

        connexion.login.Open()

        Dim liste As ListViewItem
        Dim requete As String = "SELECT * FROM achat1"
        cmd = New SqlCommand(requete, login)
        dr = cmd.ExecuteReader
        ListView1.Items.Clear()
       
        While (dr.Read)
            liste = Me.ListView1.Items.Add(dr("idachat"))
            liste.SubItems.Add(dr("qachat"))
            liste.SubItems.Add(dr("unite")).ToString()
            liste.SubItems.Add(dr("codebarre"))
            liste.SubItems.Add(dr("dateachat"))
            liste.SubItems.Add(dr("idfor"))
            liste.SubItems.Add(dr("iduser"))
            liste.SubItems.Add(dr("rachat"))
        End While
        dr.Close()
        connexion.login.Close()
        nbtotale()

    End Sub
    Sub enregistrer()
        

        If Text1.Text = Nothing Then
            Label5.Text = "erreur ! id-achat vide."
        ElseIf Text2.Text = Nothing Then
            Label8.Text = "erreur ! un champ vide."
        Else
            Dim l As Integer
            Dim m As Integer
            Dim n As Integer
            login.Open()

            Dim requete12 As String = "SELECT idachat FROM achat1 where idachat='" & Text1.Text & "'"
            cmd = New SqlCommand(requete12, login)
            dr = cmd.ExecuteReader
            If dr.Read Then
                Label24.Text = "erreur! ce N° d'achat est déja existé "

            Else
                Label24.Text = Nothing
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
                Label22.Text = Nothing
               
            Else
                Label22.Text = "erreur! ce codebarre n'existe pas "
            End If
            login.Close()
            login.Open()

            Dim requete0 As String = "SELECT idfor FROM for1 where idfor='" & Text6.Text & "'"
            cmd = New SqlCommand(requete0, login)
            dr = cmd.ExecuteReader
            If dr.Read Then
                m = 1
                dr.Close()
                Label23.Text = Nothing

            Else
                Label23.Text = "erreur! ce num_marché n'existe pas "
            End If
            login.Close()
            login.Open()
            If l = 1 And m = 1 And n = 1 Then
                cmd.CommandText = "insert into achat1(idachat,qachat,unite,codebarre,dateachat,idfor,iduser,rachat)values('" & Text1.Text & "'," & Text2.Text & ",'" & Text3.Text & "','" & Text4.Text & "','" & date5.Text & "','" & Text6.Text & "','" & Text7.Text & "','" & Text8.Text & "')"
                cmd.ExecuteNonQuery()
                Text1.Text = Nothing
                Text2.Text = Nothing
                Text3.Text = Nothing
                Label5.Text = Nothing
                Label8.Text = Nothing
                Label22.Text = Nothing
                Label22.Text = Nothing
                Label24.Text = Nothing
                MsgBox("ajouter avec succés")
            End If

            login.Close()
           

            End If
        afficher()
        nbtotale()


    End Sub
    Sub supprimer()
        connexion.login.Open()

        If ListView1.SelectedItems.Count = 0 Then
            MsgBox(" s'il voous plait ,selectioné un produit.")
        Else
            If MsgBox("Voulez vous vraiment supprimer ?", MsgBoxStyle.YesNo, "...") = MsgBoxResult.Yes Then


                cmd.CommandText = "delete from achat1 where idachat='" & ListView1.SelectedItems(0).Text.ToString & "'"

                cmd.ExecuteNonQuery()
            End If
            End If
            connexion.login.Close()
    End Sub
    
    Sub nbtotale()
        connexion.login.Open()
        Dim requete As String = "SELECT count(idachat) FROM achat1"
        cmd = New SqlCommand(requete, login)
        dr = cmd.ExecuteReader

        dr.Read()
        Me.TextBox6.Text = dr.Item(0).ToString

        dr.Close()
        connexion.login.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        enregistrer()
        afficher()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        afficher()
    End Sub

    Private Sub ListView1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.Click
        connexion.login.Open()

        If ListView1.SelectedItems.Count = 0 Then
            MsgBox(" s'il voous plait ,selectioné un produit.")
        Else

            cmd.CommandText = "select * from achat1 where idachat='" & ListView1.SelectedItems(0).Text.ToString & "'"

            cmd.ExecuteNonQuery()

            Dim dr As SqlDataReader = cmd.ExecuteReader()

            If dr.Read Then
                Me.Text1.Text = dr.Item(0).ToString
                s = dr.Item(0).ToString
                Me.date5.Text = dr.Item(1).ToString
                Me.Text2.Text = dr.Item(2)
                Me.Text4.Text = dr.Item(3).ToString
                Me.Text7.Text = dr.Item(4).ToString
                Me.Text6.Text = dr.Item(5).ToString
                Me.Text3.Text = dr.Item(6).ToString
                Me.Text8.Text = dr.Item(7).ToString
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
            Label5.Text = "erreur ! N° d'achat vide."
        ElseIf Text2.Text = Nothing Then
            Label8.Text = "erreur ! quantité ou autre champ vide."
        Else
            Dim n As Integer
            Dim l As Integer
            Dim m As Integer
            login.Open()

            Dim requete12 As String = "SELECT idachat FROM achat1 where idachat='" & Text1.Text & "'"
            cmd = New SqlCommand(requete12, login)
            dr = cmd.ExecuteReader
            If dr.Read And s <> Text1.Text Then
                Label24.Text = "erreur! ce N° d'achat est déja existé "
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

            Dim requete0 As String = "SELECT idfor FROM for1 where idfor='" & Text6.Text & "'"
            cmd = New SqlCommand(requete0, login)
            dr = cmd.ExecuteReader
            If dr.Read Then
                m = 1
                dr.Close()
                Label23.Text = Nothing
            Else
                Label23.Text = "erreur! ce num_marché n'existe pas "
            End If
            login.Close()
            connexion.login.Open()
            If l = 1 And m = 1 And n = 1 Then


                cmd.CommandText = "update achat1 set idachat='" & Text1.Text & "',qachat=" & Text2.Text & ",unite='" & Text3.Text & "',codebarre='" & Text4.Text & "',dateachat='" & date5.Text & "',idfor='" & Text6.Text & "',iduser='" & Text7.Text & "',rachat='" & Text8.Text & "' where idachat='" & s & "'"
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

        If ComboBox1.Text = "N° achat" Then
            connexion.login.Open()

            Dim liste As ListViewItem
            Dim requete As String = "SELECT * FROM achat1 where idachat like '" & TextBox5.Text & "%'"
            cmd = New SqlCommand(requete, login)

            dr = cmd.ExecuteReader

            ListView1.Items.Clear()

            While (dr.Read)
                liste = Me.ListView1.Items.Add(dr("idachat"))
                liste.SubItems.Add(dr("qachat"))
                liste.SubItems.Add(dr("unite")).ToString()
                liste.SubItems.Add(dr("codebarre"))
                liste.SubItems.Add(dr("dateachat"))
                liste.SubItems.Add(dr("idfor"))
                liste.SubItems.Add(dr("iduser"))
                liste.SubItems.Add(dr("rachat"))
            End While
            dr.Close()
            connexion.login.Close()
            nbtotale()
        ElseIf ComboBox1.Text = "codebarre" Then
            connexion.login.Open()
            Dim liste As ListViewItem
            Dim requete As String = "SELECT * FROM achat1 where codebarre like '" & TextBox5.Text & "%'"
            cmd = New SqlCommand(requete, login)
            dr = cmd.ExecuteReader
            ListView1.Items.Clear()
            While (dr.Read)
                liste = Me.ListView1.Items.Add(dr("idachat"))
                liste.SubItems.Add(dr("qachat"))
                liste.SubItems.Add(dr("unite")).ToString()
                liste.SubItems.Add(dr("codebarre"))
                liste.SubItems.Add(dr("dateachat"))
                liste.SubItems.Add(dr("idfor"))
                liste.SubItems.Add(dr("iduser"))
                liste.SubItems.Add(dr("rachat"))
            End While
            dr.Close()
        Else

            Dim liste As ListViewItem
            Dim requete As String = "SELECT * FROM achat1 where idfor like '" & TextBox5.Text & "%'"
            cmd = New SqlCommand(requete, login)
            dr = cmd.ExecuteReader
            ListView1.Items.Clear()
            While (dr.Read)
                liste = Me.ListView1.Items.Add(dr("idachat"))
                liste.SubItems.Add(dr("qachat"))
                liste.SubItems.Add(dr("unite")).ToString()
                liste.SubItems.Add(dr("codebarre"))
                liste.SubItems.Add(dr("dateachat"))
                liste.SubItems.Add(dr("idfor"))
                liste.SubItems.Add(dr("iduser"))
                liste.SubItems.Add(dr("rachat"))
            End While
            dr.Close()
            connexion.login.Close()
        End If
    End Sub

   
  
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub
End Class