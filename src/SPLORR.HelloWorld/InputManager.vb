Imports Microsoft.Xna.Framework.Input

Public Class InputManager
    Implements IInputManager(Of Command)

    Public Function GetCommands(keyboardState As KeyboardState, gamePadState As GamePadState) As IEnumerable(Of Command) Implements IInputManager(Of Command).GetCommands
        Dim result As New HashSet(Of Command)
        For Each entry In commandTable
            CheckForCommands(result, entry.Value(keyboardState, gamePadState), entry.Key)
        Next
        Return result.ToArray
    End Function
    Private Sub CheckForCommands(commands As HashSet(Of Command), isPressed As Boolean, command As Command)
        If isPressed Then
            Dim nextCommandTime As DateTimeOffset = Nothing
            Dim currentCommandTime = DateTimeOffset.Now
            If nextCommandTimes.TryGetValue(command, nextCommandTime) Then
                If currentCommandTime > nextCommandTime Then
                    commands.Add(command)
                    nextCommandTimes(command) = currentCommandTime.AddSeconds(0.3)
                End If
            Else
                commands.Add(command)
                nextCommandTimes(command) = currentCommandTime.AddSeconds(1.0)
            End If
        Else
            nextCommandTimes.Remove(command)
        End If
    End Sub
    Private ReadOnly nextCommandTimes As New Dictionary(Of Command, DateTimeOffset)
    Private ReadOnly commandTable As IReadOnlyDictionary(Of Command, Func(Of KeyboardState, GamePadState, Boolean)) =
    New Dictionary(Of Command, Func(Of KeyboardState, GamePadState, Boolean)) From
    {
        {Command.A, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Space) OrElse keyboard.IsKeyDown(Keys.Enter) OrElse gamePad.IsButtonDown(Buttons.A)},
        {Command.B, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Escape) OrElse keyboard.IsKeyDown(Keys.NumPad0) OrElse gamePad.IsButtonDown(Buttons.B)},
        {Command.Up, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Up) OrElse keyboard.IsKeyDown(Keys.NumPad8) OrElse gamePad.DPad.Up = ButtonState.Pressed},
        {Command.Down, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Down) OrElse keyboard.IsKeyDown(Keys.NumPad2) OrElse gamePad.DPad.Down = ButtonState.Pressed},
        {Command.Left, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Left) OrElse keyboard.IsKeyDown(Keys.NumPad4) OrElse gamePad.DPad.Left = ButtonState.Pressed},
        {Command.Right, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Right) OrElse keyboard.IsKeyDown(Keys.NumPad6) OrElse gamePad.DPad.Right = ButtonState.Pressed},
        {Command.[Select], Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Tab) OrElse gamePad.IsButtonDown(Buttons.Back)},
        {Command.Start, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Enter) OrElse gamePad.IsButtonDown(Buttons.Start)}
    }
End Class
