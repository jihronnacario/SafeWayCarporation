Imports MySql.Data.MySqlClient
Public Class Form12
    Dim connection As New MySqlConnection("server=localhost;userid=root;password=;database=dbRentCar")
    Public cmd, cmd1 As New MySqlCommand
    Public da As New MySqlDataAdapter

    Public Sub damagecar()
        Dim sql As String
        Dim TempTable As New DataTable

        sql = "select tblDMageCar.DCID, TblDMageCar.FirstName, tblDMageCar.LastName, tblDMagecar.Unit, tblDMagecar.CarType, tblDMagecar.Plate_No, tblCarremarks.CRM, tblcarremarks.Remarks, tblcarremarks.Remarks_Price from tbltrans " & _
                "inner join tblDMagecar on tblDMageCar.DCID = tbltrans.DCID " & _
                "inner join tblcarremarks on tblcarremarks.CRM = tbltrans.CRM;"
        'bind the connection and query
        With cmd
            .Connection = connection
            .CommandText = sql
        End With

        da.SelectCommand = cmd
        da.Fill(TempTable)

        DataGridView1.DataSource = TempTable

    End Sub
    Private Sub Form12_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        damagecar()
        RID()
        Remarks()
        auto_ID()
        petsa()
    End Sub

    Private Sub RID()
        Dim adapter As New MySqlDataAdapter("select * from tblreturncar", connection)
        Dim table As New DataTable()

        adapter.Fill(table)
        ComboBox1.DataSource = table
        ComboBox1.DisplayMember = "Borrower_Name"
        ComboBox1.ValueMember = "RCID"
    End Sub

    Private Sub Search()
        Dim search_command As New MySqlCommand("select `RCID`, `Borrower_Name`, `Borrower_LastName`, `Unit`, `CarType`, `Plate_No` from `tblreturncar` where `Borrower_Name` = @BN;", connection)
        search_command.Parameters.Add("@BN", MySqlDbType.VarChar).Value = ComboBox1.Text

        Dim adapter As New MySqlDataAdapter(search_command)
        Dim table As New DataTable()

        Try
            adapter.Fill(table)

            If table.Rows.Count > 0 Then

                Label1.Text = table(0)(0)
                Label7.Text = table(0)(1)
                Label8.Text = table(0)(2)
                Label9.Text = table(0)(3)
                Label10.Text = table(0)(4)
                Label11.Text = table(0)(5)
            Else
                Label7.Text = ""
                Label8.Text = ""
                Label9.Text = ""
                Label10.Text = ""
                Label11.Text = ""
                MessageBox.Show("No Data Found")
            End If
        Catch ex As Exception
            MsgBox("Fill the form properly", vbOKOnly + vbCritical, "Error")
        End Try
    End Sub

    Private Sub insert_into_DMageCar()
        Dim verify, status As String
        status = ""

        verify = MsgBox("All data input correct?", vbYesNo + vbQuestion, "Save Information")
        If verify = vbYes Then
            Try
                Using con = New MySqlConnection("server=localhost;userid=root;password=;database=dbRentCar")
                    Try
                        con.Open()
                        cmd.Connection = con
                        cmd.CommandText = "insert into tblDMageCar (FirstName, LastName, Unit, CarType, Plate_No) values (@FN, @LN, @UT, @CT, @PNo)"
                        cmd.Parameters.Clear()
                        cmd.Parameters.Add("@FN", MySqlDbType.VarChar).Value = Label7.Text
                        cmd.Parameters.Add("@LN", MySqlDbType.VarChar).Value = Label8.Text
                        cmd.Parameters.Add("@UT", MySqlDbType.VarChar).Value = Label9.Text
                        cmd.Parameters.Add("@CT", MySqlDbType.VarChar).Value = Label10.Text
                        cmd.Parameters.Add("@PNo", MySqlDbType.VarChar).Value = Label1.Text
                        cmd.ExecuteNonQuery()
                        con.Close()
                        status = "ok"
                    Catch ex As Exception
                        MsgBox(ex.ToString(), vbOKOnly + vbInformation, "Error")
                    End Try
                End Using
                If status = "ok" Then
                    Try
                        Using con = New MySqlConnection("server=localhost;userid=root;password=;database=dbRentCar")
                            Try
                                con.Open()
                                cmd1.Connection = con
                                cmd1.CommandText = "insert into tblcarremarks (Remarks, Remarks_Price) values (@RM, @RP);"
                                cmd1.Parameters.Clear()
                                cmd1.Parameters.Add("@RP", MySqlDbType.Double).Value = Label16.Text
                                cmd1.Parameters.Add("@RM", MySqlDbType.VarChar).Value = ComboBox2.Text
                                cmd1.ExecuteNonQuery()
                                con.Close()
                                MsgBox("Car Remarks Added", vbOKOnly + vbInformation, "")

                            Catch ex As Exception
                                MsgBox(ex.ToString(), vbOKOnly + vbInformation, "Error")
                            End Try
                        End Using
                    Catch ex As Exception
                        MsgBox(ex.ToString(), vbOKOnly + vbInformation, "Error")
                    End Try
                End If
            Catch ex As Exception
                MsgBox(ex.ToString(), vbOKOnly + vbInformation, "Error")
            End Try
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        insert_into_DMageCar()
        insert_carremarks()
        damagecar()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Search()
    End Sub

    Private Sub Remarks()
        Me.ComboBox2().Items.AddRange(New String() {"LossSidesMirror", "LossTire", "Scrath", "LossDoor"})
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim Remarks As String = ComboBox2.SelectedItem.ToString

        Select Case Remarks
            Case "LossSidesMirror" : Label16.Text = "1000"
            Case "LossTire" : Label16.Text = "9500"
            Case "Scrath" : Label16.Text = "5000"
            Case "LossDoor" : Label16.Text = "8000"
        End Select
    End Sub

    Private Sub insert_carremarks()
        Dim insert_command As New MySqlCommand("insert into tbltrans (RCID, CRM) values (@RD, @CM)", connection)
        insert_command.Parameters.Add("@RD", MySqlDbType.Double).Value = Label1.Text
        insert_command.Parameters.Add("@CM", MySqlDbType.Double).Value = Label13.Text

        connection.Open()
        If insert_command.ExecuteNonQuery() Then
            'MessageBox.Show("Save!")
        Else
            'MessageBox.Show("Unit is Empty")
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
        cmd.CommandText = "select Max(CRM) From tbltrans;"

        If IsDBNull(cmd.ExecuteScalar) Then
            number = 1
            Label13.Text = number
        Else
            number = cmd.ExecuteScalar + 1
            Label13.Text = number
        End If
        cmd.Dispose()
        cn.close()
        cn.Dispose()
    End Sub

    Private Sub petsa()

    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim FN, LN, UN, CT, PNo, RP, RMKS As String

        FN = Label7.Text
        LN = Label8.Text
        UN = Label9.Text
        CT = Label10.Text
        PNo = Label11.Text
        RP = Label16.Text
        RMKS = ComboBox2.Text

        Label25.Text = "Safe Way Carporation " & Chr(13) & _
                        "University Road Pamantasan ng" & Chr(13) & _
                        "Lungsod ng Muntinlupa " & Chr(13) & _
                        "Telepon No: 452144 " & Chr(13) & Chr(13) & _
                        "   BILLING INFORMATION" & Chr(13) & _
                        "FirstName: " & FN & Chr(13) & _
                        "LastNamw: " & LN & Chr(13) & _
                        "Unit: " & UN & Chr(13) & _
                        "CarType: " & CT & Chr(13) & _
                        "Plate No. " & PNo & Chr(13) & _
                        "Remarks_Price: " & RP & Chr(13) & _
                        "Remarks: " & RMKS & Chr(13) & Chr(13) & _
                        "See you again in our store!"

    End Sub
End Class