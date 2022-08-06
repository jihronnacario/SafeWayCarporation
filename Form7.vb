Imports MySql.Data.MySqlClient
Public Class Form7
    'Dim connection As New MySqlConnection("server=localhost;userid=root;password=;database=dbRentCar")
    Public MysqlConn As MySqlConnection
    Public cmd As New MySqlCommand
    Public da As New MySqlDataAdapter
    Public timein As String
    Public result As String

    Public Sub MysqlConnection()
        MysqlConn = New MySqlConnection()

        'Connection String
        MysqlConn.ConnectionString = "server=localhost;" _
        & "user id=root;" _
        & "password=;" _
        & "database=dbrentcar"

        'OPENING THE MysqlConnNECTION
        MysqlConn.Open()
    End Sub

    Private Sub getDateTime()
        'insertime()
        'updatetime()
        Dim sql As String
        Dim TempTable As New DataTable
        Dim MySqlConn As New MySqlConnection

        sql = "select DATE_FORMAT(now(), '%W'), " _
            & "date_format(now(), '%M %e, %Y'), " _
            & "time_format(now(), '%h'), time_format(now(), '%i'), " _
            & "time_format(now(), '%s'), time_format(now(), '%p');"
        With cmd
            .Connection = MySqlConn
            .CommandText = sql
        End With
        da.SelectCommand = cmd
        da.Fill(TempTable)

        Label4.Text = TempTable.Rows(0).Item(0) + "  " +
            TempTable.Rows(0).Item(1) + "  " +
            TempTable.Rows(0).Item(2) + "  " +
            TempTable.Rows(0).Item(3) + "  " +
            TempTable.Rows(0).Item(4) + "  " +
            TempTable.Rows(0).Item(5)
    End Sub

    Public Sub insertime()
        Try

            'MysqlConn.Open()
            With cmd
                .Connection = MysqlConn
                .CommandText = "insert into tblloginhistory (username,Login) values " _
                    & "('" & TextBox1.Text & "', " _
                    & "'" & Label5.Text & "') "
                result = cmd.ExecuteNonQuery
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'MysqlConn.Close()
    End Sub



    Public Sub getdatetimein()
        Dim sql As String
        Dim TempTable As New DataTable

        sql = "select date_format(now(), '%Y-%m-%e %h:%i:%s')"

        With cmd
            .Connection = MysqlConn
            .CommandText = sql
        End With

        da.SelectCommand = cmd
        da.Fill(TempTable)
        'MsgBox(x)

        Label5.Text = TempTable.Rows(0).Item(0)


    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        getDateTime()
        'insertime()
        'updatetime()
        getdatetimein()
    End Sub
    Private Sub login()
        Dim command As New MySqlCommand("SELECT `id`, `username` from tbluser;", MysqlConn)
        command.Parameters.Add("@un", MySqlDbType.VarChar).Value = TextBox1.Text
        command.Parameters.Add("@userpassword", MySqlDbType.VarChar).Value = TextBox2.Text

        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        adapter.Fill(table)

        If table.Rows.Count = 0 Then
            MessageBox.Show("Invalid Username or Password")
        Else
            MessageBox.Show("Welcome Admin")

            Dim newform As New Form2()
            newform.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub quit_application()
        Dim ask_cancel As String

        ask_cancel = MsgBox("Are you sure?", vbYesNo + vbQuestion, "Confirm")
        If ask_cancel = vbYes Then
            Application.Exit()
        Else
            ask_cancel = vbCancel
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        quit_application()
        'updatetime()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        login()
        'getdatetimein()
        insertime()
    End Sub

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MysqlConnection()
        getDateTime()
        'insertime()
        'updatetime()
        getdatetimein()
        'getdatetimeout()
    End Sub

    Private Sub clear_all()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox1.Focus()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form8.Show()
    End Sub
End Class
