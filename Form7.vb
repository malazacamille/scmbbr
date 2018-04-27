Imports MySql.Data.MySqlClient
Public Class Form7
    Dim readerObj As MySqlDataReader
    Dim pwd = ""

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim p As New Drawing2D.GraphicsPath
        p.StartFigure()
        p.AddArc(New Rectangle(0, 0, 40, 40), 180, 90)
        p.AddLine(40, 0, Me.Width - 40, 0)
        p.AddArc(New Rectangle(Me.Width - 40, 0, 40, 40), -90, 90)
        p.AddLine(Me.Width, 40, Me.Width, Me.Height - 40)
        p.AddArc(New Rectangle(Me.Width - 40, Me.Height - 40, 40, 40), 0, 90)
        p.AddLine(Me.Width - 40, Me.Height, 40, Me.Height)
        p.AddArc(New Rectangle(0, Me.Height - 40, 40, 40), 90, 90)
        p.CloseFigure()
        Me.Region = New Region(p)

        If Form1.myconn.State = ConnectionState.Open Then
            Form1.myconn.Close()
        End If

        Form1.GetDbConnection()
        Form1.myconn.Open()
        Dim COMMAND2 As New MySqlCommand

        Dim query2 As String
        query2 = "SELECT password from bfar.users where username='" & Form1.TextBox1.Text & "' "
        COMMAND2 = New MySqlCommand(query2, Form1.myconn)
        readerObj = COMMAND2.ExecuteReader

        While readerObj.Read
            pwd = readerObj("password").ToString
        End While





    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        Me.Hide()
        Form5.WindowState = FormWindowState.Normal
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox2.Text = TextBox3.Text And TextBox1.Text = pwd And TextBox1.Text <> "" And TextBox2.Text <> "" And TextBox3.Text <> "" Then
            Label7.Visible = False
            Label8.Visible = False
            If Form1.myconn.State = ConnectionState.Open Then
                Form1.myconn.Close()
            End If

            Form1.GetDbConnection()
            Form1.myconn.Open()
            Dim COMMAND As New MySqlCommand
            Dim query As String
            query = "UPDATE bfar.users SET password='" & TextBox2.Text & "' WHERE username ='" & Form1.TextBox1.Text & "' "
            COMMAND = New MySqlCommand(query, Form1.myconn)
            COMMAND.ExecuteNonQuery()
            MessageBox.Show("Your password has been changed succesfully! Login Again")
            Me.Hide()
            Form2.Hide()
            Form5.Hide()
            Form1.TextBox1.Clear()
            Form1.TextBox2.Clear()
            Form1.myconn.Close() 	
            Form1.Show()
        End If
        If TextBox1.Text <> pwd Then
            Label7.Text = "Incorrect Password"
            Label7.Visible = True
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()

        End If
        If TextBox2.Text <> TextBox3.Text Then
            Label8.Visible = True
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
        End If
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            Label7.Text = "please complete all required  fields"
            Label7.Visible = True
        End If
    End Sub

    Private Sub Button3_MouseHover(sender As Object, e As EventArgs) Handles Button3.MouseHover
        Button3.BackColor = Color.FromArgb(42, 65, 96)
        Button3.ForeColor = Color.FromArgb(131, 158, 195)
    End Sub
End Class