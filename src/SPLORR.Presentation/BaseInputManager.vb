Public Class BaseInputManager(Of TCommand)
    Implements IInputManager(Of TCommand)
    Sub New(commandTable As IReadOnlyDictionary(Of TCommand, Func(Of KeyboardState, GamePadState, Boolean)))
        Me.commandTable = commandTable
    End Sub
    Private ReadOnly commandTable As IReadOnlyDictionary(Of TCommand, Func(Of KeyboardState, GamePadState, Boolean))
    Private ReadOnly nextCommandTimes As New Dictionary(Of TCommand, DateTimeOffset)
    Public Function GetCommands(keyboardState As KeyboardState, gamePadState As GamePadState) As IEnumerable(Of TCommand) Implements IInputManager(Of TCommand).GetCommands
        Dim result As New HashSet(Of TCommand)
        For Each entry In commandTable
            CheckForCommands(result, entry.Value(keyboardState, gamePadState), entry.Key)
        Next
        Return result.ToArray
    End Function
    Private Sub CheckForCommands(commands As HashSet(Of TCommand), isPressed As Boolean, command As TCommand)
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
End Class
