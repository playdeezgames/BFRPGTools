Friend Class DataContext
    Implements IDataContext
    Friend ReadOnly Property Connection As MySqlConnection Implements IDataContext.Connection

    Public ReadOnly Property Abilities As IAbilities Implements IDataContext.Abilities
        Get
            Return New Abilities(Connection)
        End Get
    End Property

    Public ReadOnly Property Characters As ICharacters Implements IDataContext.Characters
        Get
            Return New Characters(Connection)
        End Get
    End Property

    Public ReadOnly Property Players As IPlayers Implements IDataContext.Players
        Get
            Return New Players(Connection)
        End Get
    End Property

    Public ReadOnly Property Races As IRaces Implements IDataContext.Races
        Get
            Return New Races(Connection)
        End Get
    End Property

    Sub New(connection As MySqlConnection)
        Me.Connection = connection
    End Sub
End Class
