Imports System.Text.Json
Imports System.IO
Public Class HWAssets
    Private _font As Font = Nothing
    Private _pipes As Font = Nothing
    Private Function LoadFont(filename As String) As Font
        Return New Font(JsonSerializer.Deserialize(Of FontData)(File.ReadAllText(filename)))
    End Function
    ReadOnly Property Font As Font
        Get
            If _font Is Nothing Then
                _font = LoadFont("Content/CyFont4x6.json")
            End If
            Return _font
        End Get
    End Property
    ReadOnly Property Pipes As Font
        Get
            If _pipes Is Nothing Then
                _pipes = LoadFont("Content/pipes.json")
            End If
            Return _pipes
        End Get
    End Property
End Class
