Imports MySql.Data.MySqlClient

Public Class Form8
    Dim connection As New MySqlConnection("server=localhost;userid=root;password=;database=dbRentCar")
    Public da As New MySqlDataAdapter
    Public cmd As New MySqlCommand
    Private Sub dataG1()
        'InserToGrid1()
        Dim sql As String
        Dim TempTable As New DataTable

        sql = "select * from tblloginHistory;"

        With cmd
            .Connection = connection
            .CommandText = sql
        End With

        da.SelectCommand = cmd
        da.Fill(TempTable)

        DataGridView1.DataSource = TempTable
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Form7.Show()
    End Sub

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dataG1()
    End Sub
End Class