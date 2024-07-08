Imports System.Runtime.CompilerServices

Friend Module MoodExtensions
    <Extension>
    Friend Function ColorName(mood As Mood) As String
        Select Case mood
            Case Mood.Prompt
                Return "olive"
            Case Mood.Danger
                Return "red"
            Case Mood.Info
                Return "silver"
            Case Mood.Success
                Return "lime"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
