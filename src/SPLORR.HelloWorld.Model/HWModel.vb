Imports SPLORR.HelloWorld.Persistence

Public Class HWModel
    Private worldData As WorldData = Nothing
    ReadOnly Property World As IWorld
        Get
            Return If(HasWorld, New World(worldData), Nothing)
        End Get
    End Property
    Public Sub New()
    End Sub
    ReadOnly Property HasWorld As Boolean
        Get
            Return worldData IsNot Nothing
        End Get
    End Property
    Sub CreateWorld()
        worldData = New WorldData
    End Sub

    Public Sub AbandonWorld()
        worldData = Nothing
    End Sub
End Class
