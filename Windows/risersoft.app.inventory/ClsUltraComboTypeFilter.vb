Public Class ClsUltraComboTypeFilter
    'Dim UltraComboControl As Infragistics.Win.UltraWinGrid.UltraCombo
    'Dim KeyColumn As String
    'Dim _FilterImage As Image

    'Public Sub New(ByVal UltraCombo As Infragistics.Win.UltraWinGrid.UltraCombo, ByVal ColumnToFilter As String)
    '    UltraComboControl = UltraCombo

    '    AddHandler UltraComboControl.KeyUp, AddressOf ucbo_KeyUp
    '    AddHandler UltraComboControl.AfterCloseUp, AddressOf ucbo_AfterCloseUp
    '    AddHandler UltraComboControl.TextChanged, AddressOf ucbo_TextChanged
    '    AddHandler UltraComboControl.BeforeDropDown, AddressOf ucbo_BeforeDropDown

    '    KeyColumn = ColumnToFilter

    '    FilterImage = My.Resources.FilterIcon() 'the filter icon is storred as an embedded resource in the resource file

    '    'turn off automatic value completion as it can potentially interfere at times with the search/filter functionality

    '    UltraComboControl.AutoEdit = False
    '    HideFilterIcon()
    '    UltraComboControl.Appearance.ImageHAlign = Infragistics.Win.HAlign.Right 'filter icon will be always displayed on the right side of the text area of the control
    '    ClearCustomPartFilter() 'by default, clear filters
    'End Sub

    'Private Sub ShowFilterIcon()
    '    UltraComboControl.Appearance.Image = FilterImage
    'End Sub

    'Private Sub HideFilterIcon()
    '    UltraComboControl.Appearance.Image = Nothing
    'End Sub

    'Private Sub ucbo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If Trim(UltraComboControl.Text) = "" Then
    '        ClearCustomPartFilter() 'if there are no characters in the textbox (from dropdown) then remove filters
    '    End If
    'End Sub

    'Private Sub ClearCustomPartFilter()
    '    'clear any filters if they exist 
    '    UltraComboControl.DisplayLayout.Bands(0).ColumnFilters.ClearAllFilters()
    '    HideFilterIcon()
    'End Sub

    'Private Sub DoPartDropDownFilter()
    '    UltraComboControl.DisplayLayout.Bands(0).ColumnFilters.ClearAllFilters()
    '    UltraComboControl.DisplayLayout.Bands(0).ColumnFilters(KeyColumn).FilterConditions.Add(Infragistics.Win.UltraWinGrid.FilterComparisionOperator.Like, "*" & UltraComboControl.Text & "*")
    '    ShowFilterIcon()
    'End Sub

    'Private Sub ucbo_BeforeDropDown(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
    '    ClearCustomPartFilter()
    'End Sub


    'Private Sub ucbo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    Dim IgnoreKeys As New List(Of Integer)
    '    IgnoreKeys.Add(Keys.Left)
    '    IgnoreKeys.Add(Keys.Right)
    '    IgnoreKeys.Add(Keys.Up)
    '    IgnoreKeys.Add(Keys.Down)
    '    IgnoreKeys.Add(Keys.Escape)
    '    IgnoreKeys.Add(Keys.Enter)

    '    If IgnoreKeys.Contains(e.KeyCode) = False Then
    '        'if inputted key press is valid for drop down filtering
    '        Dim iSelLoc As Integer = UltraComboControl.Textbox.SelectionStart 'get location of cursor

    '        If UltraComboControl.IsDroppedDown = False Then
    '            UltraComboControl.ToggleDropdown()
    '            'toggling drop down causes all text to be highlighted so we will deselect it and put the cursor position back where it was instead of being at 0
    '            UltraComboControl.Textbox.SelectionLength = 0
    '            UltraComboControl.Textbox.SelectionStart = iSelLoc
    '        End If
    '        DoPartDropDownFilter()
    '    End If
    'End Sub

    'Public Property FilterImage() As image
    '    Get
    '        Return _FilterImage
    '    End Get

    '    Set(ByVal value As Image)
    '        _FilterImage = value
    '    End Set
    'End Property

    'Private Sub ucbo_AfterCloseUp(ByVal sender As Object, ByVal e As System.EventArgs)
    '    ClearCustomPartFilter()
    'End Sub

    'Protected Overrides Sub Finalize()
    '    RemoveHandler UltraComboControl.KeyUp, AddressOf ucbo_KeyUp
    '    RemoveHandler UltraComboControl.AfterCloseUp, AddressOf ucbo_AfterCloseUp
    '    RemoveHandler UltraComboControl.TextChanged, AddressOf ucbo_TextChanged
    '    RemoveHandler UltraComboControl.BeforeDropDown, AddressOf ucbo_BeforeDropDown
    '    MyBase.Finalize()
    'End Sub
End Class
