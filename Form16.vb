Imports MySql.Data.MySqlClient
Public Class Form16
    Public cmd As New MySqlCommand
    Public da As New MySqlDataAdapter

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FilterDara("")
        DisplayAllRecord()
        sort_asc()

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

    Public Sub DisplayAllRecord()
        Dim sql As String
        Dim TempTable As New DataTable

        sql = "select tbltrans.CID, tblbarrower.BID, tblbarrower.FirstName, tblbarrower.LastName, tblbarrower.Address,tblrentedcars.RID, tblrentedcars.Unit, tblrentedcars.CarType, " & _
                "tblrentedcars.Plate_No, tblrentedcars.Cus_licence_No, tblrentedCars.No_Of_Days, tblrentedcars.Amount, tblrentedcars.Payment_Type, " & _
                "tblrentedcars.Date_Borrow, tblrentedcars.Due_Date from tblbarrower " & _
                "inner join tbltrans on tbltrans.BID = tblbarrower.BID " & _
                "inner join tblrentedcars on tblrentedcars.RID = tbltrans.RID;"
        'bind the connection and query
        With cmd
            .Connection = connection
            .CommandText = sql
        End With

        da.SelectCommand = cmd
        da.Fill(TempTable)

        DataGridView1.DataSource = TempTable

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        FilterDara("")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FilterDara(TextBox1.Text)
    End Sub

    Public Sub FilterDara(valueToSearch As String)
        Dim searchQuery As String = "select tbltrans.CID, tblbarrower.BID, tblbarrower.FirstName, tblbarrower.LastName, tblbarrower.Address,tblrentedcars.RID, tblrentedcars.Unit, tblrentedcars.CarType, " & _
                                    "tblrentedcars.Plate_No, tblrentedcars.Cus_licence_No, tblrentedCars.No_Of_Days, tblrentedcars.Amount, tblrentedcars.Payment_Type, " & _
                                    "tblrentedcars.Date_Borrow, tblrentedcars.Due_Date from tblbarrower " & _
                                    "inner join tbltrans on tbltrans.BID = tblbarrower.BID " & _
                                    "inner join tblrentedcars on tblrentedcars.RID = tbltrans.RID " & _
                                    "WHERE tblbarrower.BID = '" & valueToSearch & "' or tblbarrower.Firstname = '" & valueToSearch & "' or tblbarrower.LastName = '" & valueToSearch & "' or tblbarrower.Address = '" & valueToSearch & "' or tblrentedcars.Unit = '" & valueToSearch & "' or tblrentedcars.CarType = '" & valueToSearch & "' or tblrentedcars.Cus_licence_No = '" & valueToSearch & "';"
            Dim command As New MySqlCommand(searchQuery, connection)
            Dim adapter As New MySqlDataAdapter(command)
            Dim table As New DataTable()

            adapter.Fill(table)

            DataGridView1.DataSource = table
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Delete_Record()
    End Sub

    Private Sub Delete_Record()
        Dim command As New MySqlCommand("DELETE FROM `tblbarrower` WHERE `BID` = @ID", connection)

        command.Parameters.Add("@id", MySqlDbType.Int64).Value = TextBox1.Text

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
        DisplayAllRecord()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        Form2.Show()
    End Sub


End Class