Imports MySql.Data.MySqlClient
Public Class Form26
    Public command As New MySqlCommand
    Dim connection As New MySqlConnection("server=localhost;userid=root;password=;database=dbRentCar")

    Private Sub Search()
        Dim adapter As New MySqlDataAdapter("select * from tblbarrower", connection)
        Dim table As New DataTable()

        adapter.Fill(table)
        ComboBox1.DataSource = table
        ComboBox1.DisplayMember = "FirstName"
        ComboBox1.ValueMember = "BID"
    End Sub
    Private Sub Form26_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Search()
        Search2()
        DisplayAllRecord()
        FilterDara("")
    End Sub

    Private Sub Search1()
        Dim adapter2 As New MySqlDataAdapter("select `BID`,`FirstName`, `MiddleName`, `LastName`, `BirthDate`, `Address`, `Gender` " & _
        "from tblbarrower WHERE BID = " & ComboBox1.SelectedValue, connection)
        Dim table As New DataTable()

        adapter2.Fill(table)
        Label8.Text = table(0)(3)
    End Sub

    Private Sub Search2()
        Dim adapter As New MySqlDataAdapter("select * from tblrentedcars", connection)
        Dim table As New DataTable()

        adapter.Fill(table)
        ComboBox2.DataSource = table
        ComboBox2.DisplayMember = "RID"
        ComboBox2.ValueMember = "RID"
    End Sub

    Private Sub Search3()
        Dim adapter2 As New MySqlDataAdapter("select `RID`,`Unit`, `CarType`, `Plate_No`, `Daily_Rate`, `Cus_licence_No`, `Amount`, `Payment_Type`, `Date_Borrow`, `Due_Date`" & _
        " from tblrentedcars WHERE RID = " & ComboBox2.SelectedValue, connection)
        Dim table As New DataTable()

        adapter2.Fill(table)

        Label17.Text = table(0)(5)
        Label27.Text = table(0)(6)
        Label29.Text = table(0)(7)
        Label14.Text = table(0)(8)
        Label18.Text = table(0)(9)

        Label19.Text = table(0)(0)
        Label20.Text = table(0)(3)
        Label22.Text = table(0)(1)
        Label23.Text = table(0)(2)
        Label24.Text = table(0)(4)
    End Sub


    Public cmd As New MySqlCommand
    Public da As New MySqlDataAdapter
    Public Sub DisplayAllRecord()
        Dim sql As String
        Dim TempTable As New DataTable

        sql = "Select tblbarrower.BID, tblbarrower.FirstName, tblbarrower.LastName, tblbarrower.Address, tblrentedcars.RID from tbltrans " & _
            "inner join tblbarrower on tblbarrower.BID = tbltrans.BID " & _
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

    Public Sub FilterDara(valueToSearch As String)
        Dim connection As New MySqlConnection("server=localhost;userid=root;password=;database=dbRentCar")
        Try
            connection.Open()
            Dim searchQuery As String = "Select tblbarrower.BID, tblbarrower.FirstName, tblbarrower.LastName, tblbarrower.Address, tblrentedcars.RID from tbltrans " & _
                                            "inner join tblbarrower on tblbarrower.BID = tbltrans.BID " & _
                                            "inner join tblrentedcars on tblrentedcars.RID = tbltrans.RID where tblbarrower.Firstname = '" & valueToSearch & "' or tblbarrower.lastname = '" & valueToSearch & "' or tblbarrower.Address = '" & valueToSearch & "' or tblrentedCars.RID = '" & valueToSearch & "';"
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

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Search1()
        FilterDara(ComboBox1.Text)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Search3()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Cust_Name As String
        Dim LName As String
        Dim CARID As String
        Dim LNo As String
        Dim Tamount As String
        Dim Ptype As String
        Dim INdate As String
        Dim ComDate As String

        Dim Car_ID As String
        Dim PlNo As String
        Dim Unit As String
        Dim carType As String
        Dim DalType As String

        Cust_Name = ComboBox1.Text
        LName = Label8.Text
        CARID = ComboBox2.Text
        LNo = Label17.Text
        Tamount = Label27.Text
        Ptype = Label29.Text
        INdate = Label14.Text
        ComDate = Label18.Text

        Car_ID = Label19.Text
        PlNo = Label20.Text
        Unit = Label22.Text
        carType = Label23.Text
        DalType = Label24.Text

        Label25.Text = "Safe Way Carporation " & Chr(13) & _
                        "University Road Pamantasan ng" & Chr(13) & _
                        "Lungsod ng Muntinlupa " & Chr(13) & _
                        "Telepon No: 452144 " & Chr(13) & Chr(13) & _
                        "   BILLING INFORMATION" & Chr(13) & _
                        "Cust Name: " & Cust_Name & Chr(13) & _
                        "LastName: " & LName & Chr(13) & _
                        "Car ID: " & CARID & Chr(13) & _
                        "Licence No. " & LNo & Chr(13) & _
                        "Total Amount: " & Tamount & Chr(13) & _
                        "Payment Type: " & Ptype & Chr(13) & _
                        "Invoice Date: " & INdate & Chr(13) & _
                        "Completion Date: " & ComDate & Chr(13) & Chr(13) & _
                        "   VEHICLE INFORMATION" & Chr(13) & _
                        "Car ID: " & Car_ID & Chr(13) & _
                        "Plate No. " & PlNo & Chr(13) & _
                        "Unit: " & Unit & Chr(13) & _
                        "CarType: " & carType & Chr(13) & _
                        "Daily: " & DalType & Chr(13) & _
                        "See you again in our store!"
    End Sub
End Class