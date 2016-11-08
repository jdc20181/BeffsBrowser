'The following is to update to the latest version of the IE - Which powers BeffsBrowser NOT For USERS! This is just to save the code for the update!

'Use the following to call the class
WebBrowserHelper.FixBrowserVersion()
WebBrowserHelper.FixBrowserVersion("SomeAppName")
WebBrowserHelper.FixBrowserVersion("SomeAppName", intIeVer)

'


Public Class WebBrowserHelper


	Public Shared Function GetEmbVersion() As Integer
		Dim ieVer As Integer = GetBrowserVersion()

		If ieVer > 9 Then
			Return ieVer * 1000 + 1
		End If

		If ieVer > 7 Then
			Return ieVer * 1111
		End If

		Return 7000
	End Function
	' End Function GetEmbVersion
	Public Shared Sub FixBrowserVersion()
		Dim appName As String = System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location)
		FixBrowserVersion(appName)
	End Sub

	Public Shared Sub FixBrowserVersion(appName As String)
		FixBrowserVersion(appName, GetEmbVersion())
	End Sub
	' End Sub FixBrowserVersion
	' FixBrowserVersion("<YourAppName>", 9000);
	Public Shared Sub FixBrowserVersion(appName As String, ieVer As Integer)
		FixBrowserVersion_Internal("HKEY_LOCAL_MACHINE", appName & Convert.ToString(".exe"), ieVer)
		FixBrowserVersion_Internal("HKEY_CURRENT_USER", appName & Convert.ToString(".exe"), ieVer)
		FixBrowserVersion_Internal("HKEY_LOCAL_MACHINE", appName & Convert.ToString(".vshost.exe"), ieVer)
		FixBrowserVersion_Internal("HKEY_CURRENT_USER", appName & Convert.ToString(".vshost.exe"), ieVer)
	End Sub
	' End Sub FixBrowserVersion 
	Private Shared Sub FixBrowserVersion_Internal(root As String, appName As String, ieVer As Integer)
		Try
			'For 64 bit Machine 
			If Environment.Is64BitOperatingSystem Then
				Microsoft.Win32.Registry.SetValue(root & Convert.ToString("\Software\Wow6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION"), appName, ieVer)
			Else
				'For 32 bit Machine 
				Microsoft.Win32.Registry.SetValue(root & Convert.ToString("\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION"), appName, ieVer)


			End If
				' some config will hit access rights exceptions
				' this is why we try with both LOCAL_MACHINE and CURRENT_USER
		Catch generatedExceptionName As Exception
		End Try
	End Sub
	' End Sub FixBrowserVersion_Internal 
	Public Shared Function GetBrowserVersion() As Integer
		' string strKeyPath = @"HKLM\SOFTWARE\Microsoft\Internet Explorer";
		Dim strKeyPath As String = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer"
		Dim ls As String() = New String() {"svcVersion", "svcUpdateVersion", "Version", "W2kVersion"}

		Dim maxVer As Integer = 0
		For i As Integer = 0 To ls.Length - 1
			Dim objVal As Object = Microsoft.Win32.Registry.GetValue(strKeyPath, ls(i), "0")
			Dim strVal As String = System.Convert.ToString(objVal)
			If strVal IsNot Nothing Then
				Dim iPos As Integer = strVal.IndexOf("."C)
				If iPos > 0 Then
					strVal = strVal.Substring(0, iPos)
				End If

				Dim res As Integer = 0
				If Integer.TryParse(strVal, res) Then
					maxVer = Math.Max(maxVer, res)
				End If
				' End if (strVal != null)
			End If
		Next
		' Next i
		Return maxVer
	End Function
	' End Function GetBrowserVersion 

End Class
