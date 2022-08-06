Imports MySql.Data.MySqlClient
Public Class Form2
    'Dim connection As New MySqlConnection("server=localhost;userid=root;password=;database=dbRentCar")
    Public MysqlConn As MySqlConnection
    Public cmd As New MySqlCommand
    Public da As New MySqlDataAdapter
    Public out As String

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
    Private Sub log_out()
        Dim ask_log_out As String

        ask_log_out = MsgBox("Are You Sure ?", vbYesNo + vbQuestion, "Logout")
        If ask_log_out = vbYes Then
            Me.Dispose()
            Form7.Show()
            Form7.TextBox1.Focus()
        Else
            ask_log_out = vbCancel
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Me.Dispose()
        Form4.Show()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MysqlConnection()
        'getdatetimeout()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        getdatetimeout()
        updatetime()
        log_out()
    End Sub
    Public Sub updatetime()
        Dim result As String
        Try
            'MysqlConn.Open()
            With cmd
                .Connection = MysqlConn
                .CommandText = "update tblloginhistory set " _
                    & "Logout='" & Form7.Label5.Text & "'" _
                    & "where username='" & Form7.TextBox1.Text & "'"

                result = cmd.ExecuteNonQuery
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'MysqlConn.Close()
    End Sub

    Public Sub getdatetimeout()
        Dim sql As String
        Dim TempTable As New DataTable

        sql = "select date_format(now(), '%Y-%m-%e %h:%i:%s') "

        With cmd
            .Connection = MysqlConn
            .CommandText = sql
        End With

        da.SelectCommand = cmd
        da.Fill(TempTable)
        ' MsgBox(a)

        Form7.Label5.Text = TempTable.Rows(0).Item(0)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Dispose()
        Form3.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Dispose()
        Form6.Show()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Me.Dispose()
        Form16.Show()
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click_2(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Form15.Show()
    End Sub

    Private Sub Button4_Click_2(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Dispose()
        Form14.Show()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Form26.Show()
    End Sub

End Class