Friend Class AbilityDetails
    Public Sub New(abilityId As Object, abilityName As Object, abilityAbbreviation As Object)
        Me.AbilityId = CInt(abilityId)
        Me.AbilityName = CStr(abilityName)
        Me.AbilityAbbreviation = CStr(abilityAbbreviation)
    End Sub

    Public ReadOnly Property AbilityId As Integer
    Public ReadOnly Property AbilityName As String
    Public ReadOnly Property AbilityAbbreviation As String
End Class
