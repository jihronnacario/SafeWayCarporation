Imports MySql.Data.MySqlClient

Public Class Form4
    Public command As New MySqlCommand

    Dim connection As New MySqlConnection("server=localhost;userid=root;password=;database=dbRentCar")
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Insert()
        clear_all()
    End Sub

    Private Sub clear_all()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox2.Focus()
    End Sub

    Private Sub Insert()
        Dim date_string, status, verify As String
        Dim date_ As Date

        date_ = DateTimePicker1.Value()
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
                            command.CommandText = "INSERT INTO `tblbarrower`(`Firstname`, `Middlename`, `Lastname`, `BirthDate`, `Address`, `Gender`) VALUES (@FN,@MD,@LN,@BD,@Add,@GD);"
                            command.Parameters.Clear()
                            command.Parameters.Add("@FN", MySqlDbType.VarChar).Value = TextBox2.Text
                            command.Parameters.Add("@MD", MySqlDbType.VarChar).Value = TextBox3.Text
                            command.Parameters.Add("@LN", MySqlDbType.VarChar).Value = TextBox4.Text
                            command.Parameters.Add("@BD", MySqlDbType.Date).Value = DateTimePicker1.Value
                            command.Parameters.Add("@Add", MySqlDbType.VarChar).Value = TextBox5.Text
                            command.Parameters.Add("@GD", MySqlDbType.VarChar).Value = TextBox6.Text
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
        End If
    End Sub

    Function check_null_input()
        Dim flag As Integer
        If TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or
            TextBox6.Text = "" Then
            flag = 0
        Else
            flag = 1
        End If
        Return flag
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        cancel_btn()
    End Sub

    Private Sub cancel_btn()
        Me.Dispose()
        Form3.Show()
    End Sub

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class