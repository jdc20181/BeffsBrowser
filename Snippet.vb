not for production.
If CType(Tabcontrol1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url = New Uri("https://jdc20181.github.io/StartPage/Startpage2.html") Then
            'Declare an index placeholder for the name and url (default starts at 0)
            Dim nameIndex, urlIndex As Integer

            'Declare a collection to hold all of the names and their respective URLS
            Dim names() As String = {My.Settings.StartPageName1, My.Settings.StartPageName2, My.Settings.StartPageName3, My.Settings.StartPageName4, My.Settings.StartPageName5}
            Dim urls() As String = {My.Settings.StartPage1, My.Settings.StartPage2, My.Settings.StartPage3, My.Settings.StartPage4, My.Settings.StartPage5}

            'Loop through every HTML element in the document

            For Each ele As HtmlElement In CType(Tabcontrol1.SelectedTab.Controls.Item(0), WebBrowser).Document.All
                'If the class equals "name" or "url" then set the element's InnerText (and HREF if url) equal to the current item in the respective collection
                If ele.GetAttribute("className") = "site" AndAlso nameIndex < names.Length Then
                    ele.InnerHtml = names(nameIndex)
                    nameIndex += 1
                ElseIf ele.GetAttribute("className") = "url" AndAlso urlIndex < urls.Length Then
                    ele.InnerHtml = urls(urlIndex)
                    ele.SetAttribute("HREF", urls(urlIndex))
                    urlIndex += 1
                End If
            Next
        End If
