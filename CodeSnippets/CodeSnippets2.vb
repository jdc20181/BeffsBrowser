Public Class NativeMethods
    
    Private Declare Function InternetSetOption Lib "wininet.dll" (ByVal hInternet As IntPtr, ByVal dwOption As Integer, ByVal lpBuffer As IntPtr, ByVal lpdwBufferLength As Integer) As Boolean
    
    Public Shared Sub SuppressCookiePersist()
        Dim dwOption As Integer = 81
        'INTERNET_OPTION_SUPPRESS_BEHAVIOR
        Dim option As Integer = 3
        ' INTERNET_SUPPRESS_COOKIE_PERSIST
        Dim optionPtr As IntPtr = Marshal.AllocHGlobal(sizeof, int)
        Marshal.WriteInt32(optionPtr, option)
        NativeMethods.InternetSetOption(IntPtr.Zero, dwOption, optionPtr, sizeof, int)
        Marshal.FreeHGlobal(optionPtr)
    End Sub
End Class

 'To reset use int option = 4; //INTERNET_SUPPRESS_COOKIE_PERSIST_RESET
