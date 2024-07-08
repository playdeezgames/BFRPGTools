Friend Class CharacterHitDice
    Implements ICharacterHitDice

    Private connection As MySqlConnection
    Private characterId As Integer

    Public Sub New(connection As MySqlConnection, characterId As Integer)
        Me.connection = connection
        Me.characterId = characterId
    End Sub

    Friend Shared Sub Write(
                    connection As MySqlConnection,
                    characterId As Integer,
                    die As Integer,
                    dieRoll As Integer)
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
