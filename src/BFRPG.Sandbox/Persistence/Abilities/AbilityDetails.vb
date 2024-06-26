Friend Class AbilityDetails
    Public Sub New(abilityId As Integer, abilityName As String, abilityAbbreviation As String)
        Me.AbilityId = abilityId
        Me.AbilityName = abilityName
        Me.AbilityAbbreviation = abilityAbbreviation
    End Sub

    Public ReadOnly Property AbilityId As Integer
    Public ReadOnly Property AbilityName As String
    Public ReadOnly Property AbilityAbbreviation As String
End Class
