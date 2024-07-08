Friend Class CharacterAbilities
    Implements ICharacterAbilities

    Private ReadOnly connection As MySqlConnection
    Private ReadOnly characterId As Integer
    Private ReadOnly store As IStore

    Public Sub New(connection As MySqlConnection, store As IStore, characterId As Integer)
        Me.connection = connection
        Me.characterId = characterId
        Me.store = store
    End Sub

    Public Sub Write(abilityId As Integer, abilityScore As Integer) Implements ICharacterAbilities.Write
        Using command = connection.CreateCommand
            command.CommandText = $"
INSERT INTO `{Tables.CharacterAbilities}`
(
    `{Columns.CharacterId}`, 
    `{Columns.AbilityId}`, 
    `{Columns.AbilityScore}`
) 
VALUES
(
    @{Columns.CharacterId}, 
    @{Columns.AbilityId}, 
    @{Columns.AbilityScore}
) 
ON DUPLICATE KEY UPDATE 
    `{Columns.AbilityScore}`=@{Columns.AbilityScore};"
            command.Parameters.AddWithValue(Columns.CharacterId, characterId)
            command.Parameters.AddWithValue(Columns.AbilityId, abilityId)
            command.Parameters.AddWithValue(Columns.AbilityScore, abilityScore)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub DeleteForCharacter() Implements ICharacterAbilities.DeleteForCharacter
        store.Delete(Tables.CharacterAbilities, New Dictionary(Of String, Object) From {{Columns.CharacterId, characterId}})
    End Sub

    Public Function ReadAllDetailsForCharacter() As IEnumerable(Of CharacterAbilityDetails) Implements ICharacterAbilities.ReadAllDetailsForCharacter
        Dim result As New List(Of CharacterAbilityDetails)
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{Columns.CharacterId}`, 
    `{Columns.CharacterName}`, 
    `{Columns.AbilityId}`, 
    `{Columns.AbilityName}`, 
    `{Columns.AbilityAbbreviation}`, 
    `{Columns.AbilityScore}` 
FROM 
    `{Views.CharacterAbilityDetails}` 
WHERE 
    `{Columns.CharacterId}`=@{Columns.CharacterId};"
            command.Parameters.AddWithValue(Columns.CharacterId, characterId)
            Using reader = command.ExecuteReader
                While reader.Read
                    result.Add(
                        New CharacterAbilityDetails(
                            reader(Columns.CharacterId),
                            reader(Columns.CharacterName),
                            reader(Columns.AbilityId),
                            reader(Columns.AbilityName),
                            reader(Columns.AbilityAbbreviation),
                            reader(Columns.AbilityScore)))
                End While
            End Using
        End Using
        Return result
    End Function
End Class
