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

    Sub New(connection As MySqlConnection)
        Me.Connection = connection
    End Sub
End Class
