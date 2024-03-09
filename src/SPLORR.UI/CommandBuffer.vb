Public Class CommandBuffer(Of TCommand)
    Implements ICommandBuffer(Of TCommand)
    Dim commands As New Queue(Of TCommand)

    Public ReadOnly Property HasCommand As Boolean Implements ICommandBuffer(Of TCommand).HasCommand
        Get
            Return commands.Any
        End Get
    End Property

    Public Sub WriteCommand(command As TCommand) Implements ICommandBuffer(Of TCommand).WriteCommand
        commands.Enqueue(command)
    End Sub

    Public Function ReadCommand() As TCommand Implements ICommandBuffer(Of TCommand).ReadCommand
        Return commands.Dequeue()
    End Function
End Class
