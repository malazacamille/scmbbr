Imports System.Net.Mail
Imports MySql.Data.MySqlClient

Public Class Form3
    Dim readerObj As MySqlDataReader
    Dim pool As String = "0123456789qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM"
    Dim count = 0
    Dim cc As New Random
    Dim str = ""
    Dim result = ""
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        Me.Hide()
        Form1.WindowState = FormWindowState.Normal
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Form1.myconn.State = ConnectionState.Open Then
            Form1.myconn.Close()
        End If


        While count <= 8
                str = cc.Next(0, pool.Length)
                result = result + pool(str)
            count = count + 1
        End While

        Form1.GetDbConnection()
        Form1.myconn.Open()

        Dim COMMAND As New MySqlCommand
        Dim query As String
        Dim dbName = ""
        Dim counter = 0
        query = "select users.username from bfar.users where email='" & TextBox3.Text & "' and lname='" & TextBox2.Text & "'  or fname='" & TextBox1.Text & "'"
        COMMAND = New MySqlCommand(query, Form1.myconn)
        readerObj = COMMAND.ExecuteReader

        While readerObj.Read
            dbName = readerObj("username").ToString
            counter = counter + 1
        End While


        If counter = 1 Then
            Try
                Label5.Visible = False
                Dim Smtp_Server As New SmtpClient
                Dim e_mail As New MailMessage()
                Smtp_Server.UseDefaultCredentials = False
                Smtp_Server.Credentials = New Net.NetworkCredential("your email")
                Smtp_Server.Port = 587
                Smtp_Server.EnableSsl = True
                Smtp_Server.Host = "smtp.gmail.com"

                e_mail = New MailMessage()
                e_mail.From = New MailAddress("your email")
                e_mail.To.Add(TextBox3.Text)
                e_mail.Subject = "FORGOT PASSWORD"
                e_mail.IsBodyHtml = False
                e_mail.Body = "Your Username is: " & dbName & vbNewLine & "your new password is: " & result
                Smtp_Server.Send(e_mail)
                MsgBox("Mail Sent")
                Form1.myconn.Close()
                Me.Hide()
                Form1.WindowState = FormWindowState.Normal
            Catch error_t As Exception
                MsgBox(error_t.ToString)
            End Try
            Form1.myconn.Open()
            Dim COMMAND1 As New MySqlCommand
            Dim query1 As String
            query1 = "update bfar.users set password='" & result & "' where username='" & dbName & "'"
            COMMAND1 = New MySqlCommand(query1, Form1.myconn)
            COMMAND1.ExecuteNonQuery()

        Else
            Label5.Visible = True
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
        End If


    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
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
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button2_MouseHover(sender As Object, e As EventArgs) Handles Button2.MouseHover
        Button2.BackColor = Color.FromArgb(42, 65, 96)
        Button2.ForeColor = Color.FromArgb(131, 158, 195)
    End Sub





End Class