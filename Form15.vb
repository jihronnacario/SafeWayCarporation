Imports MySql.Data.MySqlClient
Public Class Form15
    Public cmd As New MySqlCommand
    Public da As New MySqlDataAdapter
    Dim connection As New MySqlConnection("server=localhost;userid=root;password=;database=dbRentCar")

    Private Sub dataG1()
        'InserToGrid1()
        Dim sql As String
        Dim TempTable As New DataTable

        sql = "select * from tblCarUnits;"

        With cmd
            .Connection = connection
            .CommandText = sql
        End With

        da.SelectCommand = cmd
        da.Fill(TempTable)

        DataGridView1.DataSource = TempTable
    End Sub
    Private Sub Form15_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dataG1()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Form2.Show()
    End Sub
End Class