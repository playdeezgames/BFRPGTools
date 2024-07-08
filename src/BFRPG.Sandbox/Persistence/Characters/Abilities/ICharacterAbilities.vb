Friend Interface ICharacterAbilities
    Sub Write(
                    abilityId As Integer,
                    abilityScore As Integer)
    Sub DeleteForCharacter()
    Function ReadAllDetailsForCharacter() As IEnumerable(Of CharacterAbilityDetails)
End Interface
