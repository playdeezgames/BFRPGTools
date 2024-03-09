Public Interface ICommandBuffer(Of TCommand)
    ReadOnly Property HasCommand As Boolean
    Sub WriteCommand(command As TCommand)
    Function ReadCommand() As TCommand
End Interface
