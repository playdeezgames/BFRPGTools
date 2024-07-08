Friend Class CharacterHitDice
    Implements ICharacterHitDice

    Private ReadOnly connection As MySqlConnection
    Private ReadOnly characterId As Integer

    Public Sub New(connection As MySqlConnection, characterId As Integer)
        Me.connection = connection
        Me.characterId = characterId
    End Sub

    Public Sub Write(die As Integer, dieRoll As Integer) Implements ICharacterHitDice.Write
        Using command = connection.CreateCommand
            command.CommandText = $"
INSERT IGNORE INTO `{Tables.CharacterHitDice}`
(
    `{Columns.CharacterId}`,
    `{Columns.Die}`,
    `{Columns.DieRoll}`
)
VALUES
(
    @{Columns.CharacterId},
    @{Columns.Die},
    @{Columns.DieRoll}
);"
            command.Parameters.AddWithValue(Columns.CharacterId, characterId)
            command.Parameters.AddWithValue(Columns.Die, die)
            command.Parameters.AddWithValue(Columns.DieRoll, dieRoll)
            command.ExecuteNonQuery()
        End Using
    End Sub
End Class
