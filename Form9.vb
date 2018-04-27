Imports MySql.Data.MySqlClient
Public Class Form9
    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Form5.WindowState = FormWindowState.Normal
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Form1.myconn.State = ConnectionState.Open Then
            Form1.myconn.Close()
        End If

        Form1.GetDbConnection()
        Form1.myconn.Open()
        Dim COMMAND As New MySqlCommand
        Dim query As String
        query = "UPDATE bfar.users SET email='" & TextBox1.Text & "' WHERE username ='" & Form1.TextBox1.Text & "' "
        COMMAND = New MySqlCommand(query, Form1.myconn)
        COMMAND.ExecuteNonQuery()
        MessageBox.Show("Your email has been changed succesfully")
        Me.Hide()
        Form5.WindowState = FormWindowState.Normal
    End Sub

    Private Sub Button3_MouseHover(sender As Object, e As EventArgs) Handles Button3.MouseHover
        Button3.BackColor = Color.FromArgb(42, 65, 96)
        Button3.ForeColor = Color.FromArgb(131, 158, 195)
    End Sub
End Class