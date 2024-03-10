Imports Microsoft.Xna.Framework.Input

Public Class InputManager
    Implements IInputManager(Of Command)

    Public Function GetCommands(keyboardState As KeyboardState, gamePadState As GamePadState) As IEnumerable(Of Command) Implements IInputManager(Of Command).GetCommands
        Return Array.Empty(Of Command)
    End Function
End Class
