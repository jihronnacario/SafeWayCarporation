Imports MySql.Data.MySqlClient
Public Class Form6
    Dim connection As New MySqlConnection("server=localhost;userid=root;password=;database=dbRentCar")
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Update_Record()
        'clearAll()
        Form3.Show()
    End Sub

    Public command As New MySqlCommand
    Private Sub Update_Record()
        'Dim command As New MySqlCommand
        Dim command As New MySqlCommand("UPDATE `tblbarrower` SET `Firstname`= @FN,`Middlename`= @MN,`Lastname` = @LN,`BirthDate` = @BD,`Address` = @Add,`Gender` = @GD WHERE `BID` = @Id", connection)
        command.Parameters.Add("@Id", MySqlDbType.Int64).Value = TextBox1.Text


        command.Parameters.Add("@FN", MySqlDbType.VarChar).Value = TextBox2.Text
        command.Parameters.Add("@MN", MySqlDbType.VarChar).Value = TextBox3.Text
        command.Parameters.Add("@LN", MySqlDbType.VarChar).Value = TextBox4.Text
        'command.Parameters.Add("@BD", MySqlDbType.VarChar).Value = TextBox5.Text
        command.Parameters.Add("@BD", MySqlDbType.Date).Value = DateTimePicker1.Value
        command.Parameters.Add("@Add", MySqlDbType.VarChar).Value = TextBox5.Text
        command.Parameters.Add("@GD", MySqlDbType.VarChar).Value = TextBox6.Text


        connection.Open()

        If command.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Data Updated")
        Else
            MessageBox.Show("ERROR!")
        End If

        connection.Close()
    End Sub

    Function check_null_input()
        Dim flag As Integer

        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or
            TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Then
            flag = 0
        Else
            flag = 1
        End If
        Return flag
    End Function


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        back_btn()
    End Sub

    Private Sub back_btn()
        Me.Dispose()
        Form3.Show()
    End Sub

    Private Sub clear_txtbx1()
        TextBox1.Clear()
        TextBox1.Focus()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        searh_record()
    End Sub

    'Public ID As Integer
    'Public cmd, cmd2 As New MySqlCommand
    'Public Reader, Reader2 As MySqlDataReader

    Private Sub searh_record()
        Dim search_command As New MySqlCommand("SELECT * FROM `tblBarrower` WHERE `BID` = @BID", connection)
        search_command.Parameters.Add("@BID", MySqlDbType.Int64).Value = TextBox1.Text

        Dim adapter As New MySqlDataAdapter(search_command)
        Dim table As New DataTable()

        Try
            adapter.Fill(table)

            If table.Rows.Count > 0 Then

                TextBox2.Text = table(0)(1)
                TextBox3.Text = table(0)(2)
                TextBox4.Text = table(0)(3)
                DateTimePicker1.Value = table(0)(4)
                TextBox5.Text = table(0)(5)
                TextBox6.Text = table(0)(6)

            Else
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
                TextBox6.Text = ""
                MessageBox.Show("No Data Found")

            End If
        Catch ex As Exception
            MessageBox.Show("ERROR")
        End Try

    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'GroupBox1_Enter("")
    End Sub

    Private Sub clearAll()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
    End Sub


    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        Form2.Show()
    End Sub

End Class