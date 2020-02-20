Imports INIControl.INIControl
Imports Microsoft.Win32
Public Class Form1
    Private Declare Function key Lib "user32" Alias "GetAsyncKeyState" (ByVal key As Keys) As Keys
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If key(Keys.Left) And key(Keys.Home) Then
            SendKeys.Send(ComboBox1.Text)
            SendKeys.Send("{TAB}")
            SendKeys.Send(ComboBox2.Text)
        End If
        If key(Keys.ControlKey) And key(Keys.ShiftKey) And key(Keys.A) Then
            Shell("SS.exe")
        End If
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        WriteINI("Current", "ID", ComboBox3.Text)
        WriteINI("Current", "Username", ComboBox1.Text)
        WriteINI("Current", "Password", ComboBox2.Text)

    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Visible = False
        Try
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "Auto pw", "c:\Tools\Auto pw.exe")
        Catch ex As Exception

        End Try
        SelectINI("C:\Config\Launcher.ini")
        If IO.File.Exists("C:\Config") = False Then
            IO.Directory.CreateDirectory("C:\Config\")


        End If
        Try
            ComboBox3.Text = ReadINI("Current", "ID")
            ComboBox1.Text = ReadINI("Current", "Username")
            ComboBox2.Text = ReadINI("Current", "Password")
            Dim reader1 As New IO.StreamReader("C:\Config\User.ini")
            Dim reader2 As New IO.StreamReader("C:\Config\pw.ini")
            Dim reader3 As New IO.StreamReader("C:\Config\ID.ini")
            While (reader1.Peek() > -1)
                ComboBox1.Items.Add(reader1.ReadLine)

            End While
            While (reader2.Peek() > -1)
                ComboBox2.Items.Add(reader2.ReadLine)

            End While
            While (reader3.Peek() > -1)
                ComboBox3.Items.Add(reader3.ReadLine)

            End While
            reader1.Close()
            reader2.Close()
            reader3.Close()
        Catch ex As Exception

        End Try

        Try
            IO.File.WriteAllBytes("SS.exe", My.Resources.SS)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Process.Start("C:\Config\user.ini")
        Process.Start("C:\Config\pw.ini")
        Process.Start("C:\Config\ID.ini")
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        If Me.Visible = True Then
            Me.Visible = False
            Me.WindowState = FormWindowState.Minimized
        ElseIf Me.Visible = False Then
            Me.Visible = True
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

  
 

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ComboBox1.Items.Add(ComboBox1.Text)
        ComboBox2.Items.Add(ComboBox2.Text)
        ComboBox3.Items.Add(ComboBox3.Text)
      WriteINI("Current", "Username", ComboBox1.Text)
            WriteINI("Current", "Password", ComboBox2.Text)
            Dim user As New IO.StreamWriter("C:\Config\User.ini")
            Dim i As Integer
            For i = 0 To ComboBox1.Items.Count - 1
                user.WriteLine(ComboBox1.Items(i))
            Next
            user.Close()
            Dim pw As New IO.StreamWriter("C:\Config\pw.ini")
            For i = 0 To ComboBox2.Items.Count - 1
                pw.WriteLine(ComboBox2.Items(i))
            Next
        pw.Close()
        Dim ID As New IO.StreamWriter("C:\Config\ID.ini")
        For i = 0 To ComboBox3.Items.Count - 1
            ID.WriteLine(ComboBox3.Items(i))
        Next
        ID.Close()
     
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        Try
            ComboBox1.SelectedIndex = ComboBox3.SelectedIndex
            ComboBox2.SelectedIndex = ComboBox3.SelectedIndex
        Catch ex As Exception

        End Try
    End Sub

    Private Sub QuitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuitToolStripMenuItem.Click
        Me.Close()
    End Sub

   

 
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If Label4.Text = "On" Then
            Shell("Net stop uxsms", AppWinStyle.Hide)
            Label4.Text = "Off"
        Else
            Shell("Net start Uxsms", AppWinStyle.Hide)
            Label4.Text = "On"
        End If
    End Sub

    Private Sub Form1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDoubleClick
        Me.Visible = False
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub ComboBox3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox3.TextChanged
        ComboBox1.Text = Nothing
        ComboBox2.Text = Nothing
    End Sub
End Class
