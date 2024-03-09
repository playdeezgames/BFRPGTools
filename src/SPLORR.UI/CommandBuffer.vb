Public Class CommandBuffer(Of TCommand)
    Implements ICommandBuffer(Of TCommand)

    Public ReadOnly Property HasCommand As Boolean Implements ICommandBuffer(Of TCommand).HasCommand
        Get
            Return False
        End Get
    End Property
End Class
