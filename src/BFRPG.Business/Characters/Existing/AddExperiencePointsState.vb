Friend Class AddExperiencePointsState
    Inherits BaseState

    Private ReadOnly characterId As Integer

    Public Sub New(data As IDataContext, ui As IUIContext, endState As IState, characterId As Integer)
        MyBase.New(data, ui, endState)
        Me.characterId = characterId
    End Sub

    Public Overrides Function Run() As IState
        Dim xp As Integer = ui.Ask((Mood.Prompt, "How many XP?"), 0)
        If xp < 0 Then
            Return endState
        End If
        data.Characters.AddXP(characterId, xp)
        Return endState
    End Function
End Class
