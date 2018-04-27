Imports MySql.Data.MySqlClient
Imports itextsharp.text.pdf
Imports itextsharp.text
Imports System.IO

Public Class Form2
    Dim readerObj As MySqlDataReader
    Dim m2 = ""
    Dim m1 = ""
    Dim date1 = ""
    Dim date2 = ""
    Dim date3 = ""
    Dim s1 As String = Form6.DateTimePicker1.Value.Year()
    Dim s2 As String = Form6.DateTimePicker1.Value.Month()
    Dim s3 As String = Form6.DateTimePicker1.Value.Day()
    Dim f1 As String = Form6.DateTimePicker2.Value.Year()
    Dim f2 As String = Form6.DateTimePicker2.Value.Month()
    Dim f3 As String = Form6.DateTimePicker2.Value.Day()
    Dim s4 As String = s1 + "-" + s2 + "-" + s3
    Dim f4 As String = f1 + "-" + f2 + "-" + f3
    Public Function getQuery(q As String)
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
        query = q
        COMMAND = New MySqlCommand(query, Form1.myconn)
        SDA.SelectCommand = COMMAND
        SDA.Fill(dbDataSet)
        bSource.DataSource = dbDataSet
        DataGridView1.DataSource = bSource
        SDA.Update(dbDataSet)
        Form1.myconn.Close()

    End Function

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToolTip1.SetToolTip(Me.PictureBox2, "Profile")
        ToolTip1.SetToolTip(Me.PictureBox3, "logout")
        ToolTip1.SetToolTip(Me.PictureBox4, "Manage Users")

        If f2 = 1 Then
            m2 = "Jan"
        ElseIf f2 = 2 Then
            m2 = "Feb"
        ElseIf f2 = 3 Then
            m2 = "Mar"
        ElseIf f2 = 4 Then
            m2 = "Apr"
        ElseIf f2 = 5 Then
            m2 = "May"
        ElseIf f2 = 6 Then
            m2 = "Jun"
        ElseIf f2 = 7 Then
            m2 = "Jul"
        ElseIf f2 = 8 Then
            m2 = "Aug"
        ElseIf f2 = 9 Then
            m2 = "Sept"
        ElseIf f2 = 10 Then
            m2 = "Oct"
        ElseIf f2 = 11 Then
            m2 = "Nov"
        ElseIf f2 = 12 Then
            m2 = "Dec"

        End If

        If s2 = 1 Then
            m1 = "Jan"
        ElseIf s2 = 2 Then
            m1 = "Feb"
        ElseIf s2 = 3 Then
            m1 = "Mar"
        ElseIf s2 = 4 Then
            m1 = "Apr"
        ElseIf s2 = 5 Then
            m1 = "May"
        ElseIf s2 = 6 Then
            m1 = "Jun"
        ElseIf s2 = 7 Then
            m1 = "Jul"
        ElseIf s2 = 8 Then
            m1 = "Aug"
        ElseIf s2 = 9 Then
            m1 = "Sept"
        ElseIf s2 = 10 Then
            m1 = "Oct"
        ElseIf s2 = 11 Then
            m1 = "Nov"
        ElseIf s2 = 12 Then
            m1 = "Dec"

        End If

        date1 = m1 + " " + s3 + ", " + s1
        date2 = m2 + " " + f3 + ", " + f1
        date3 = "Sandfish Report (" + date1 + " - " + date2 + ")"

        getQuery("select id,weight as 'weight (g)',status,temperature as 'temperature(°C)' ,salinty as 'salinity(ppt)',date,time(time) as 'time',day from bfar.sandfish where date between '" & s4 & "' and '" & f4 & "'")


        LinkLabel4.LinkBehavior = LinkBehavior.NeverUnderline
        LinkLabel4.Text = "Hi, " + Form1.names

        'for admin

        If Form1.myconn.State = ConnectionState.Open Then
            Form1.myconn.Close()
        End If
        Form1.GetDbConnection()
        Form1.myconn.Open()
        Dim COMMAND As New MySqlCommand
        Dim query As String
        query = "select * from bfar.users where username='" & Form1.names & "'"
        COMMAND = New MySqlCommand(query, Form1.myconn)
        readerObj = COMMAND.ExecuteReader
        Dim count As Integer
        count = 0

        While readerObj.Read
            count = count + 1
        End While
        If count = 1 Then
            PictureBox4.Visible = True

        ElseIf count > 1 Then
            PictureBox4.Visible = False
        Else
            PictureBox4.Visible = False
        End If
        Form1.myconn.Close()
        'for total no of scabra
        If Form1.myconn.State = ConnectionState.Open Then
            Form1.myconn.Close()
        End If
        Form1.GetDbConnection()
        Form1.myconn.Open()
        Dim COMMAND1 As New MySqlCommand
        Dim query1 As String
        query1 = "select * from sandfish where date between '" & s4 & "' and '" & f4 & "'"
        COMMAND1 = New MySqlCommand(query1, Form1.myconn)
        readerObj = COMMAND1.ExecuteReader
        Dim count1 As Integer
        count1 = 0

        While readerObj.Read
            count1 = count1 + 1
        End While
        Label1.Text = count1.ToString
        Form1.myconn.Close()






    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)

    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Dim Paragraph As New Paragraph
        Dim pdfFile As New Document(itextsharp.text.PageSize.LETTER.Rotate(), 150, 70, 70, 70)
        Dim write As PdfWriter = PdfWriter.GetInstance(pdfFile, New FileStream("seacucumber.pdf", FileMode.Create))
        pdfFile.Open()

        Dim pTitle As New Font(itextsharp.text.Font.FontFamily.HELVETICA, 14, itextsharp.text.Font.BOLD, BaseColor.BLACK)
        Dim ptable As New Font(itextsharp.text.Font.FontFamily.HELVETICA, 12, itextsharp.text.Font.NORMAL, BaseColor.BLACK)

        Paragraph = New Paragraph(New Chunk("Bureau of Fisheries and Aquatic Resources", pTitle))
        Paragraph.Alignment = Element.ALIGN_MIDDLE
        Paragraph.SpacingAfter = 1.0F
        pdfFile.Add(Paragraph)

        Paragraph = New Paragraph(New Chunk(date3.ToString, pTitle))
        Paragraph.Alignment = Element.ALIGN_MIDDLE
        Paragraph.SpacingAfter = 10.0F

        pdfFile.Add(Paragraph)
        Dim pdfTable = New PdfPTable(DataGridView1.Columns.Count)

        pdfTable.TotalWidth = 500.0F
        pdfTable.LockedWidth = True

        Dim widths(0 To DataGridView1.Columns.Count - 1) As Single
        For i As Integer = 0 To DataGridView1.Columns.Count - 1
            widths(i) = 3.0F
        Next

        pdfTable.SetWidths(widths)
        pdfTable.HorizontalAlignment = 0
        pdfTable.SpacingBefore = 5.0F

        Dim pdfcell As New PdfPCell



        For i As Integer = 0 To DataGridView1.Columns.Count - 1
            pdfcell = New PdfPCell(New Phrase(New Chunk(DataGridView1.Columns(i).HeaderText)))
            pdfcell.HorizontalAlignment = PdfPCell.ALIGN_MIDDLE

            pdfcell.HorizontalAlignment = 1
            pdfTable.AddCell(pdfcell)
        Next

        For i As Integer = 0 To DataGridView1.Rows.Count - 2

            For j As Integer = 0 To DataGridView1.Columns.Count - 1
                pdfcell = New PdfPCell(New Phrase(DataGridView1(j, i).Value.ToString(), ptable))
                pdfTable.HorizontalAlignment = PdfPCell.ALIGN_MIDDLE
                pdfTable.AddCell(pdfcell)

            Next
        Next

        pdfFile.Add(pdfTable)
        pdfFile.Close()
        MessageBox.Show("PDF exported successfully.")

    End Sub





    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Form5.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Me.Close()
        Form6.Close()
        Form1.Show()
        Form1.TextBox1.Clear()
        Form1.TextBox2.Clear()
        Form1.myconn.Close()
    End Sub


    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Form10.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub
End Class