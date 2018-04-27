Imports MySql.Data.MySqlClient
Public Class Form10
    Dim readerObj As MySqlDataReader
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        Form2.WindowState = FormWindowState.Normal
    End Sub



    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Form4.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Form4.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Public Sub Form10_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Form1.myconn.State = ConnectionState.Open Then
            Form1.myconn.Close()
        End If
        Form1.GetDbConnection()
        Form1.myconn.Open()
        Dim COMMAND As New MySqlCommand
        Dim SDA As New MySqlDataAdapter
        Dim dbDataSet As New DataTable
        Dim bSource As New BindingSource
        Dim query As String
        query = "Select username from users where userid!=1"
        COMMAND = New MySqlCommand(query, Form1.myconn)
        SDA.SelectCommand = COMMAND
        SDA.Fill(dbDataSet)
        bSource.DataSource = dbDataSet
        DataGridView1.DataSource = bSource
        SDA.Update(dbDataSet)
        Form1.myconn.Close()


    End Sub
    Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim value As Object = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value

        If IsDBNull(value) Then
            TextBox1.Text = "" ' blank if dbnull values
        Else
            TextBox1.Text = CType(value, String)
        End If





    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Form1.myconn.State = ConnectionState.Open Then
            Form1.myconn.Close()
        End If

        Form1.GetDbConnection()
        Form1.myconn.Open()
        Dim COMMAND2 As New MySqlCommand
        Dim SDA As New MySqlDataAdapter
        Dim query2 As String
        query2 = "delete from users where username='" & TextBox1.Text & "' "
        COMMAND2 = New MySqlCommand(query2, Form1.myconn)
        readerObj = COMMAND2.ExecuteReader
        Form10_Load(e, e)

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub
End Class