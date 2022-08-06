Imports MySql.Data.MySqlClient
Public Class Form24
    Public command As New MySqlCommand
    Dim connection As New MySqlConnection("server=localhost;userid=root;password=;database=dbRentCar")

    Private Sub Search()
        Dim search_command As New MySqlCommand("select `Unit`, `CarType`, `Plate_No`, `Daily_Rate` from `tblcarunits` where `CARID` = @ID;", connection)
        search_command.Parameters.Add("@ID", MySqlDbType.Int64).Value = TextBox1.Text

        Dim adapter As New MySqlDataAdapter(search_command)
        Dim table As New DataTable()

        Try
            adapter.Fill(table)

            If table.Rows.Count > 0 Then

                TextBox2.Text = table(0)(0)
                TextBox3.Text = table(0)(1)
                TextBox4.Text = table(0)(2)
                TextBox5.Text = table(0)(3)
            Else
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
                MessageBox.Show("No Data Found")
            End If
        Catch ex As Exception
            MsgBox("Fill the form properly", vbOKOnly + vbCritical, "Error")
        End Try
    End Sub

    Private Sub insert()
        Dim date_string, status, verify As String
        Dim date_, date_1 As Date

        date_ = DateTimePicker1.Value()
        date_1 = DateTimePicker2.Value()
        date_string = date_.ToString("yyyy-MM-dd")
        status = ""

        If check_null_input() = 1 Then
            verify = MsgBox("All data input correct?", vbYesNo + vbQuestion, "Save Information")
            If verify = vbYes Then
                Try
                    Using con = New MySqlConnection("server=localhost;userid=root;password=;database=dbRentCar")
                        Try
                            con.Open()
                            command = New MySqlCommand
                            command.Connection = con
                            command.CommandText = "insert into `tblrentedcars` (`Unit`, `CarType`, `Plate_No`, `Daily_Rate`, `Cus_licence_No`, `No_Of_Days`, `Amount`, `Payment_Type`, `Date_Borrow`, `Due_Date`) values " & _
                                                                                "(@UN, @CT, @PNo, @DRate, @Clicence_No, @Nodys, @AT, @PT, @DB, @DD);"
                            command.Parameters.Clear()
                            command.Parameters.Add("@UN", MySqlDbType.VarChar).Value = TextBox2.Text
                            command.Parameters.Add("@CT", MySqlDbType.VarChar).Value = TextBox3.Text
                            command.Parameters.Add("@PNo", MySqlDbType.VarChar).Value = TextBox4.Text
                            command.Parameters.Add("@DRate", MySqlDbType.Double).Value = TextBox5.Text
                            command.Parameters.Add("@Clicence_No", MySqlDbType.VarChar).Value = TextBox6.Text
                            command.Parameters.Add("@Nodys", MySqlDbType.VarChar).Value = TextBox7.Text
                            command.Parameters.Add("@AT", MySqlDbType.Double).Value = TextBox8.Text
                            command.Parameters.Add("@PT", MySqlDbType.VarChar).Value = ComboBox1.Text
                            command.Parameters.Add("@DB", MySqlDbType.Date).Value = DateTimePicker1.Value
                            command.Parameters.Add("@DD", MySqlDbType.Date).Value = DateTimePicker2.Value
                            command.ExecuteNonQuery()
                            con.Close()
                            'auto_ID()
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

    Private Sub insert_TransRID_BID()
        Dim insert_command As New MySqlCommand("insert into tbltrans (BID, RID) values (@BD, @RD)", connection)
        insert_command.Parameters.Add("@BD", MySqlDbType.Double).Value = Label18.Text
        insert_command.Parameters.Add("@RD", MySqlDbType.Double).Value = TextBox9.Text

        connection.Open()
        If insert_command.ExecuteNonQuery() Then

        Else
            MessageBox.Show("Unit is Empty")
        End If
        connection.Close()
    End Sub

    Private Sub auto_ID()
        Dim cn As New MysqlConnection
        Dim cmd As New MySqlCommand

        cn.ConnectionString = "server=localhost;userid=root;password=;database=dbRentCar"
        cmd.Connection = cn
        cn.Open()

        Dim number As Integer
        cmd.CommandText = "select Max(RID) From tbltrans;"

        If IsDBNull(cmd.ExecuteScalar) Then
            number = 1
            TextBox9.Text = number
        Else
            number = cmd.ExecuteScalar + 1
            TextBox9.Text = number
        End If
        cmd.Dispose()
        cn.close()
        cn.Dispose()
    End Sub

    Private Sub Form24_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select_type_of_Payment()
        auto_ID()
        DisplayAllRecord()
        FirstName()
    End Sub

    Private Sub FirstName()
        Dim adapter As New MySqlDataAdapter("select * from tblbarrower", connection)
        Dim table As New DataTable()

        adapter.Fill(table)
        ComboBox3.DataSource = table
        ComboBox3.DisplayMember = "FirstName"
        ComboBox3.ValueMember = "BID"
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
        search_borrower()
    End Sub

    Private Sub search_borrower()
        Dim adapter2 As New MySqlDataAdapter("select `BID` " & _
        " from tblbarrower WHERE BID = " & ComboBox3.SelectedValue, connection)
        Dim table As New DataTable()

        adapter2.Fill(table)

        Label18.Text = table(0)(0)

    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles Button1.Click
        multiply_day()
    End Sub

    Public Sub multiply_day()

        Dim num1, num2, Product As Double

        num1 = TextBox5.Text
        num2 = TextBox7.Text

        Product = num1 * num2

        TextBox8.Text = Product
    End Sub

    Private Sub clear_all()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox2.Focus()
    End Sub

    Private Sub Select_type_of_Payment()
        Me.ComboBox1().Items.AddRange(New String() {"Credit Card", "Debit Card", "Cash", "Installment"})
    End Sub

    Function check_null_input()
        Dim flag As Integer
        If TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or
            TextBox6.Text = "" Or TextBox7.Text = "" Or TextBox8.Text = "" Or ComboBox1.Text = "" Then
            flag = 0
        Else
            flag = 1
        End If
        Return flag
    End Function

    Public da As New MySqlDataAdapter
    Public Sub DisplayAllRecord()
        Dim sql As String
        Dim TempTable As New DataTable

        sql = "Select CARID, Unit, Cartype from tblcarunits;"
        'bind the connection and query
        With command
            .Connection = connection
            .CommandText = sql
        End With

        da.SelectCommand = command
        da.Fill(TempTable)

        DataGridView1.DataSource = TempTable

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        'insert()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Dispose()
        Form14.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        insert()
        clear_all()
        insert_TransRID_BID()
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Search()
    End Sub

    Private Sub TextBox7_KeyPress(sender As Object, e As EventArgs) Handles TextBox7.KeyPress
        numberonlys(e)
    End Sub

    Public Sub numberonlys(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (Asc(e.KeyChar) >= 48 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 46 Or Asc(e.KeyChar) = 8 Then
        Else
            e.Handled = True
            MsgBox("Allow Numbers Only!")
        End If
    End Sub

    Private Sub TextBox8_KeyPress(sender As Object, e As EventArgs) Handles TextBox8.KeyPress
        numberonly(e)
    End Sub

    Public Sub numberonly(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (Asc(e.KeyChar) >= 48 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 46 Or Asc(e.KeyChar) = 8 Then
        Else
            e.Handled = True
            MsgBox("Allow Numbers Only!")
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As EventArgs) Handles TextBox8.KeyPress
        numberonlyx(e)
    End Sub
    Public Sub numberonlyx(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (Asc(e.KeyChar) >= 48 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 46 Or Asc(e.KeyChar) = 8 Then
        Else
            e.Handled = True
            MsgBox("Allow Numbers Only!")
        End If
    End Sub

    Private Sub TextBox9_KeyPress(sender As Object, e As EventArgs) Handles TextBox8.KeyPress
        numberonlyz(e)
    End Sub
    Public Sub numberonlyz(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (Asc(e.KeyChar) >= 48 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 46 Or Asc(e.KeyChar) = 8 Then
        Else
            e.Handled = True
            MsgBox("Allow Numbers Only!")
        End If
    End Sub
End Class