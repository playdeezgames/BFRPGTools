Friend Interface ICharacters
    Sub Delete(
              characterId As Integer)
    Sub Rename(
              characterId As Integer,
              characterName As String)
    Sub Transfer(
                characterId As Integer,
                playerId As Integer)
    Sub AddXP(
            characterId As Integer,
            experiencePoints As Integer)
    Function Create(
            playerId As Integer,
            characterName As String,
            raceClassId As Integer,
            experiencePoints As Integer,
            characterDescription As String) As Integer?
    Function ReadDetails(
                        characterId As Integer) As CharacterDetails
    Function AllForPlayer(
                         playerId As Integer) As IEnumerable(Of CharacterDetails)
    Function FindForPlayerAndName(
                                 playerId As Integer,
                                 characterName As String) As Integer?
    Function HitDice(characterId As Integer) As ICharacterHitDice
    Function Abilities(characterId As Integer) As ICharacterAbilities
End Interface
