Public Interface IInputManager(Of TCommand)
    Function GetCommands(keyboardState As KeyboardState, gamePadState As GamePadState) As IEnumerable(Of TCommand)
End Interface
