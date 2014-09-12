﻿Imports System.Text.RegularExpressions
Imports System.Net
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Reflection
Imports Microsoft.Win32
Imports System.Security.AccessControl
Imports System.Security.Principal
Imports System.Security.Permissions
Imports System.Security
Public Class frmItemConfig
    Dim Detecting As String
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'MsgBox("This feature was originally going to be used to enable logging - But since the DLL is working again, this won't do anything yet. In the future (within the next 1 - 2 days), you'll be able to configure your Item Translation keys here. Thank you for your patience!")
        'Exit Sub
        Dim DirectoryString As String
        Dim pso2launchpath As String
        DirectoryString = frmMain.lblDirectory.Text
        pso2launchpath = DirectoryString.Replace("\data\win32", "")
        If File.Exists(pso2launchpath & "\translation.cfg") Then File.Delete(pso2launchpath & "\translation.cfg")
        Dim delay As Integer = NUDDelay.Value * 1000
        Dim data As String()
        Dim NumberKey As String
        Dim UselessString As String = cmbToggleKey.SelectedItem
        Dim SplitKey As String() = UselessString.Split("(")
        lblToggle.Text = "Toggle item patch ON/OFF: Control + " & SplitKey(0).Replace("   (", "")
        NumberKey = SplitKey(1).Replace(")", "")
        If chkLogging.Checked = True Then
            data = {"Delay:" & delay, "TranslationPath:translation.bin", "TranslationCachePath:", "LogPath:itemlog.txt", "LogLines:0", "KeyToggle:17", "KeyToggleCancel:16", "KeyDisable:" & NumberKey, "KeyDisableTree:114", "KeyDisableToggle:113"}
        Else
            data = {"Delay:" & delay, "TranslationPath:translation.bin", "TranslationCachePath:", "LogPath:", "LogLines:500", "KeyToggle:17", "KeyToggleCancel:16", "KeyDisable:" & NumberKey, "KeyDisableTree:114", "KeyDisableToggle:113"}
        End If
        File.WriteAllLines(pso2launchpath & "\translation.cfg", data)
        Me.Hide()
    End Sub

    Private Sub chkLogging_CheckedChanged(sender As Object, e As EventArgs) Handles chkLogging.CheckedChanged
        Dim DirectoryString As String
        Dim pso2launchpath As String
        DirectoryString = frmMain.lblDirectory.Text
        pso2launchpath = DirectoryString.Replace("\data\win32", "")
        If File.Exists(pso2launchpath & "\itemlog.txt") Then File.Delete(pso2launchpath & "\itemlog.txt")
        MsgBox("Please only turn this feature on if you are specifically asked to. If AIDA didn't tell you to turn it on, don't.")
    End Sub

    Private Sub ButtonX7_Click(sender As Object, e As EventArgs) Handles ButtonX7.Click
        cmbToggleKey.SelectedIndex = 0
    End Sub

    Private Sub cmbToggleKey_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbToggleKey.SelectedIndexChanged
        'F12   (123)
        'Numpad Add   (107)
        'Semi-Colon   (186)
        Dim NumberKey As String
        Dim UselessString As String = cmbToggleKey.SelectedItem
        Dim SplitKey As String() = UselessString.Split("(")
        lblToggle.Text = "Toggle item patch ON/OFF: Control + " & SplitKey(0).Replace("   (", "")
        NumberKey = SplitKey(1).Replace(")", "")
    End Sub
End Class