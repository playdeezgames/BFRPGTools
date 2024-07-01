Imports System.Runtime.CompilerServices

Friend Module MoodExtensions
    <Extension>
    Friend Function ColorName(mood As Mood) As String
        Select Case mood
            Case Mood.Prompt
                Return "olive"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
