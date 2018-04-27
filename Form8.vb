Imports MySql.Data.MySqlClient
Public Class Form8
    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        TextBox1.Clear()
        Me.Hide()
        Form5.WindowState = FormWindowState.Normal
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text <> "" Then
            If Form1.myconn.State = ConnectionState.Open Then
                Form1.myconn.Close()
            End If

            Form1.GetDbConnection()
            Form1.myconn.Open()
            Dim COMMAND As New MySqlCommand
            Dim query As String
            query = "UPDATE bfar.users SET username='" & TextBox1.Text & "' WHERE username ='" & Form1.TextBox1.Text & "' "
            COMMAND = New MySqlCommand(query, Form1.myconn)
            COMMAND.ExecuteNonQuery()
            MessageBox.Show("Your username has been changed succesfully! Login Again")
            Me.Hide()
            Form5.Hide()
            Form2.Hide()
            Form1.Show()
            Form1.TextBox1.Clear()
            Form1.TextBox2.Clear()
            Form1.myconn.Close()
        Else
            Label9.Visible = True
        End If
    End Sub

    Private Sub Button3_MouseHover(sender As Object, e As EventArgs) Handles Button3.MouseHover
        Button3.BackColor = Color.FromArgb(42, 65, 96)
        Button3.ForeColor = Color.FromArgb(131, 158, 195)
    End Sub
End Class