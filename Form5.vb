Imports MySql.Data.MySqlClient
Public Class Form5
    Dim readerObj As MySqlDataReader
    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        Dim SDA As New MySqlDataAdapter
        Dim Name = ""
        Dim query2 As String
        query2 = "SELECT fname from bfar.users where username='" & Form1.TextBox1.Text & "' "
        COMMAND2 = New MySqlCommand(query2, Form1.myconn)
        readerObj = COMMAND2.ExecuteReader

        While readerObj.Read
            Name = readerObj("fname").ToString
        End While
        Label1.Text = "Hi, " + Name
        'username

        Label5.Text = Form1.TextBox1.Text

        If Form1.myconn.State = ConnectionState.Open Then
            Form1.myconn.Close()
        End If
        'email
        Form1.GetDbConnection()
        Form1.myconn.Open()
        Dim COMMAND3 As New MySqlCommand
        Dim SDA3 As New MySqlDataAdapter
        Dim em = ""
        Dim query3 As String
        query3 = "SELECT email from bfar.users where username='" & Form1.TextBox1.Text & "' "
        COMMAND3 = New MySqlCommand(query3, Form1.myconn)
        readerObj = COMMAND3.ExecuteReader

        While readerObj.Read
            em = readerObj("email").ToString
        End While
        Label6.Text = em
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.Hide()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form8.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Form7.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Form2.WindowState = FormWindowState.Normal
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Form9.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub
End Class