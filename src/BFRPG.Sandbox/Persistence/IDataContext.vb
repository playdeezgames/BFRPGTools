Friend Interface IDataContext
    ReadOnly Property Connection As MySqlConnection
    ReadOnly Property Abilities As IAbilities
    ReadOnly Property Characters As ICharacters
    ReadOnly Property Players As IPlayers
    ReadOnly Property Races As IRaces
End Interface
