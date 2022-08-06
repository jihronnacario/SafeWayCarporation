Imports MySql.Data.MySqlClient
Public Class Form3
    Public MysqlConn As MySqlConnection
    Public cmd As New MySqlCommand
    Public da As New MySqlDataAdapter

    Public Sub MysqlConnection()
        MysqlConn = New MySqlConnection()

        'Connection String
        MysqlConn.ConnectionString = "server=localhost;" _
        & "user id=root;" _
        & "password=;" _
        & "database=dbRENTCAR"

        'OPENING THE MysqlConnNECTION
        MysqlConn.Open()
    End Sub

    Public Sub showTable()
        Dim sql As String
        Dim TempTable As New DataTable

        sql = "Select * From tblbarrower"
        'bind the connection and query
        With cmd
            .Connection = MysqlConn
            .CommandText = sql
        End With

        da.SelectCommand = cmd
        da.Fill(TempTable)

        DataGridView1.DataSource = TempTable

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        showTable()
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MysqlConnection()
        FilterDara("")
        showTable()
    End Sub

    Dim connection As New MySqlConnection("server=localhost;userid=root;password=;database=dbRentCar")
    Private Sub back_btn()
        Dim ask_back As String

        ask_back = MsgBox("Are You Sure?", vbYesNo + vbQuestion, "Back")
        If ask_back = vbYes Then
            Me.Dispose()
            Form2.Show()
        Else
            ask_back = vbCancel
        End If
    End Sub

    Public Sub FilterDara(valueToSearch As String)
        Dim connection As New MySqlConnection("server=localhost;userid=root;password=;database=dbRentCar")
        Try
            connection.Open()
            Dim searchQuery As String = "select * from tblbarrower where tblbarrower.BID = '" & valueToSearch & "' or tblbarrower.Firstname = '" & valueToSearch & "' or tblbarrower.lastname = '" & valueToSearch & "' or tblbarrower.Address = '" & valueToSearch & "' or tblbarrower.Birthdate = '" & valueToSearch & "' or tblbarrower.MiddleName = '" & valueToSearch & "'"
            Dim command As New MySqlCommand(searchQuery, connection)
            Dim adapter As New MySqlDataAdapter(command)
            Dim table As New DataTable()

            adapter.Fill(table)

            DataGridView1.DataSource = table
            connection.Close()
        Catch ex As Exception
            MsgBox(ex.ToString(), vbOKOnly + vbInformation, "Error")
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Delete_Record()
    End Sub

    Private Sub Delete_Record()
        Dim command As New MySqlCommand("DELETE FROM `tblbarrower` WHERE `BID` = @ID", connection)

        command.Parameters.Add("@ID", MySqlDbType.Int64).Value = TextBox1.Text

        connection.Open()

        Try
            If command.ExecuteNonQuery() = 1 Then
                MessageBox.Show("Data Deleted")
            Else
                MessageBox.Show("ERROR")
            End If
        Catch ex As Exception
            MessageBox.Show("Something Wrong")
        End Try
        connection.Close()
    End Sub

    Private Sub sort_asc()
        DataGridView1.Sort(DataGridView1.Columns(0),
        System.ComponentModel.ListSortDirection.Ascending)
    End Sub

    Private Sub sort_desc()
        DataGridView1.Sort(DataGridView1.Columns(0),
        System.ComponentModel.ListSortDirection.Descending)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        FilterDara(TextBox1.Text)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        back_btn()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Dispose()
        Form4.Show()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Dispose()
        Form24.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Dispose()
        Form6.Show()
    End Sub
End Class