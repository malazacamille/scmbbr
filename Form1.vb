
Imports MySql.Data.MySqlClient
Public Class Form1
    Public myconn As New MySqlConnection
    Dim reader As MySqlDataReader
    Public names As String

    Public Function GetDbConnection() As MySqlConnection
        myconn.ConnectionString = "mysqlConfig"
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Clear()
        TextBox2.Clear()

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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Try
            GetDbConnection()
            myconn.Open()
            Dim COMMAND As New MySqlCommand
            Dim query As String
            query = "select * from bfar.users where username='" & TextBox1.Text & "' and password='" & TextBox2.Text & "' "
            COMMAND = New MySqlCommand(query, myconn)
            reader = COMMAND.ExecuteReader
            Dim count As Integer
            count = 0

            While reader.Read
                count = count + 1
            End While
            If count = 1 Then
                Form6.Show()
                Me.Hide()

            ElseIf count > 1 Then
                MessageBox.Show("username and password are duplicate")
            Else
                MessageBox.Show("username and password are incorrect")
            End If
            myconn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            myconn.Dispose()
        End Try
        If myconn.State = ConnectionState.Open Then
            myconn.Close()
        End If

        GetDbConnection()
        myconn.Open()
        Dim COMMAND2 As New MySqlCommand
        Dim SDA As New MySqlDataAdapter
        Dim query2 As String
        query2 = "SELECT fname from bfar.users where username='" & TextBox1.Text & "' "
        COMMAND2 = New MySqlCommand(query2, myconn)
        reader = COMMAND2.ExecuteReader

        While reader.Read
            names = reader("fname").ToString
        End While

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub





    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Form3.Show()
        Me.WindowState = FormWindowState.Minimized

    End Sub

    Private Sub Button3_MouseHover(sender As Object, e As EventArgs) Handles Button3.MouseHover
        Button3.BackColor = Color.FromArgb(42, 65, 96)
        Button3.ForeColor = Color.FromArgb(131, 158, 195)
    End Sub
End Class
