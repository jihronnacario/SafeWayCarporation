Imports MySql.Data.MySqlClient
Public Class Form13

    Public command As New MySqlCommand
    Dim connection As New MySqlConnection("server=localhost;userid=root;password=;database=dbRentCar")
    Public cmd As New MySqlCommand
    Public da As New MySqlDataAdapter

    Private Sub FirstName()
        Dim adapter As New MySqlDataAdapter("select * from tblbarrower", connection)
        Dim table As New DataTable()

        adapter.Fill(table)
        ComboBox1.DataSource = table
        ComboBox1.DisplayMember = "FirstName"
        ComboBox1.ValueMember = "BID"
    End Sub

    Private Sub Search1()
        Dim adapter2 As New MySqlDataAdapter("select `BID`,`FirstName`, `MiddleName`, `LastName`, `BirthDate`, `Address`, `Gender` " & _
        "from tblbarrower WHERE BID = " & ComboBox1.SelectedValue, connection)
        Dim table As New DataTable()

        adapter2.Fill(table)
        Label7.Text = table(0)(0)
        Label26.Text = table(0)(1)
        Label27.Text = table(0)(3)
        Label28.Text = table(0)(5)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Search1()
        FilterDara(ComboBox1.Text)
    End Sub

    Private Sub VID()
        Dim adapter As New MySqlDataAdapter("select * from tblrentedcars", connection)
        Dim table As New DataTable()

        adapter.Fill(table)
        ComboBox3.DataSource = table
        ComboBox3.DisplayMember = "RID"
        ComboBox3.ValueMember = "RID"
    End Sub

    Private Sub search2()
        Dim adapter As New MySqlDataAdapter("select `RID`,`Unit`, `CarType`, `Plate_No`, `Daily_Rate`, `Cus_licence_No`, `No_Of_Days`, `Amount`, `Payment_Type`, `Date_Borrow`, `Due_Date`" & _
        " from tblrentedcars WHERE RID = " & ComboBox3.SelectedValue, connection)

        Dim table As New DataTable()
        adapter.Fill(table)

        Label8.Text = table(0)(6)
        Label9.Text = table(0)(8)
        Label10.Text = table(0)(1)
        Label18.Text = table(0)(2)
        Label17.Text = table(0)(3)
        Label3.Text = table(0)(4)
        Label24.Text = table(0)(9)
        Label25.Text = table(0)(10)
    End Sub

    Private Sub Form13_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisplayAllRecord1()
        DisplayAllRecord()
        FirstName()
        FilterDara("")
        VID()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        search2()
    End Sub

    Private Sub insert()
        Dim date_string, status, verify As String
        Dim date_, date_1 As Date

        date_ = Label24.Text()
        date_1 = Label25.Text()
        date_string = date_.ToString("yyyy-MM-dd")
        status = ""

        If check_null_input() = 1 Then
            verify = MsgBox("Car Return Succesfull?", vbYesNo + vbQuestion, "Save Information")
            If verify = vbYes Then
                Try
                    Using con = New MySqlConnection("server=localhost;userid=root;password=;database=dbRentCar")
                        Try
                            con.Open()
                            command = New MySqlCommand
                            command.Connection = con
                            command.CommandText = "insert into `tblreturncar` (`Unit`, `CarType`, `Plate_No`, `Daily_Rate`, `Days_Of_Rent`, `Type_Of_Payment`, `Date_Borrow`, `Date_Return`, `Borrower_Name`, `Borrower_LastName`, `Address`) values " & _
                                                                                "(@UN, @CT, @PNO, @DLR, @DOR, @TP, @DB, @DRN, @BN, @BL, @ADD);"
                            command.Parameters.Clear()
                            command.Parameters.Add("@UN", MySqlDbType.VarChar).Value = Label10.Text
                            command.Parameters.Add("@CT", MySqlDbType.VarChar).Value = Label18.Text
                            command.Parameters.Add("@PNO", MySqlDbType.VarChar).Value = Label17.Text
                            command.Parameters.Add("@DLR", MySqlDbType.Double).Value = Label3.Text
                            command.Parameters.Add("@DOR", MySqlDbType.VarChar).Value = Label8.Text
                            command.Parameters.Add("@TP", MySqlDbType.VarChar).Value = Label19.Text
                            command.Parameters.Add("@DB", MySqlDbType.Date).Value = Label24.Text
                            command.Parameters.Add("@DRN", MySqlDbType.Date).Value = Label25.Text
                            command.Parameters.Add("@BN", MySqlDbType.VarChar).Value = Label26.Text
                            command.Parameters.Add("@BL", MySqlDbType.VarChar).Value = Label27.Text
                            command.Parameters.Add("@ADD", MySqlDbType.VarChar).Value = Label28.Text
                            command.ExecuteNonQuery()
                            con.Close()

                            status = "ok"
                        Catch ex As Exception
                            MsgBox(ex.ToString(), vbOKOnly + vbInformation, "Error")
                        End Try
                    End Using
                Catch ex As Exception
                    MsgBox(ex.ToString(), vbOKOnly + vbInformation, "Error")
                End Try
            Else
                verify = vbCancel
            End If
        Else
            MsgBox("Fill the form properly", vbOKOnly + vbCritical, "Erro")
            Form16.DisplayAllRecord()
        End If
    End Sub

    Function check_null_input()
        Dim flag As Integer
        If Label10.Text = "" Or Label18.Text = "" Or Label17.Text = "" Or Label13.Text = "" Or
            Label18.Text = "" Or Label19.Text = "" Or Label24.Text = "" Or Label25.Text = "" Or
            Label26.Text = "" Or Label27.Text = "" Or Label28.Text = "" Then
            flag = 0
        Else
            flag = 1
        End If
        Return flag
    End Function

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        insert()
        Delete_Record()
        Delete_Record1()
        Delete_Record2()
        DisplayAllRecord()
        DisplayAllRecord1()
    End Sub

    Private Sub Delete_Record()
        Dim command As New MySqlCommand("DELETE FROM `tblbarrower` WHERE `BID` = @ID", connection)

        command.Parameters.Add("@ID", MySqlDbType.Int64).Value = Label7.Text

        connection.Open()

        Try
            If command.ExecuteNonQuery() = 1 Then
                'MessageBox.Show("Data Deleted")
            Else
                'MessageBox.Show("ERROR")
            End If
        Catch ex As Exception
            'MessageBox.Show("Something Wrong")
        End Try
        connection.Close()
    End Sub

    Private Sub Delete_Record1()
        Dim command As New MySqlCommand("DELETE FROM `tblRentedcars` WHERE `RID` = @ID", connection)

        command.Parameters.Add("@ID", MySqlDbType.Int64).Value = ComboBox3.Text

        connection.Open()

        Try
            If command.ExecuteNonQuery() = 1 Then
                'MessageBox.Show("Data Deleted")
            Else
                'MessageBox.Show("ERROR")
            End If
        Catch ex As Exception
            'MessageBox.Show("Something Wrong")
        End Try
        connection.Close()
    End Sub

    Private Sub Delete_Record2()
        Dim command As New MySqlCommand("DELETE FROM `tbltrans` WHERE `BID` = @ID or `RID` = @RD;", connection)

        command.Parameters.Add("@ID", MySqlDbType.Int64).Value = Label7.Text
        command.Parameters.Add("@RD", MySqlDbType.Int64).Value = ComboBox3.Text

        connection.Open()

        Try
            If command.ExecuteNonQuery() = 1 Then
                'MessageBox.Show("Data Deleted")
            Else
                'MessageBox.Show("ERROR")
            End If
        Catch ex As Exception
            'MessageBox.Show("Something Wrong")
        End Try
        connection.Close()
    End Sub

    Public Sub DisplayAllRecord()
        Dim sql As String
        Dim TempTable As New DataTable

        sql = "Select tblbarrower.BID, tblbarrower.FirstName, tblrentedcars.RID from tbltrans " & _
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
            Dim searchQuery As String = "Select tblbarrower.BID, tblbarrower.FirstName, tblrentedcars.RID from tbltrans " & _
                                            "inner join tblbarrower on tblbarrower.BID = tbltrans.BID " & _
                                            "inner join tblrentedcars on tblrentedcars.RID = tbltrans.RID where tblbarrower.Firstname = '" & valueToSearch & "';"
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

    Public Sub DisplayAllRecord1()
        Dim sql As String
        Dim TempTable As New DataTable

        sql = "select RCID, Unit, Cartype, Plate_No, Daily_Rate, Days_Of_Rent, Type_Of_Payment," & _
                "Date_Return, Borrower_Name, Borrower_LastName, Address from tblreturncar;"
        'bind the connection and query
        With cmd
            .Connection = connection
            .CommandText = sql
        End With

        da.SelectCommand = cmd
        da.Fill(TempTable)

        DataGridView2.DataSource = TempTable

    End Sub

    Public Sub damagecar()
        Dim sql As String
        Dim TempTable As New DataTable

        sql = "Select * From tblDMageCar"
        'bind the connection and query
        With command
            .Connection = connection
            .CommandText = sql
        End With

        da.SelectCommand = command
        da.Fill(TempTable)

        DataGridView2.DataSource = TempTable

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form12.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Dispose()
        Form14.Show()
    End Sub
End Class