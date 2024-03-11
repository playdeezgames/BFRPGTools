Imports Microsoft.Xna.Framework.Audio

Public Class BaseSfxManager(Of TSfx)
    Implements ISfxManager(Of TSfx)
    Private ReadOnly fileNames As IReadOnlyDictionary(Of TSfx, String)
    Private ReadOnly soundEffects As New Dictionary(Of TSfx, SoundEffect)
    Sub New(fileNames As IReadOnlyDictionary(Of TSfx, String))
        Me.fileNames = fileNames
    End Sub
    Public Sub Play(sfx As TSfx, volume As Single) Implements ISfxManager(Of TSfx).Play
        If fileNames.ContainsKey(sfx) Then
            Dim soundEffect As SoundEffect = Nothing
            If Not soundEffects.TryGetValue(sfx, soundEffect) Then
                soundEffect = SoundEffect.FromFile(fileNames(sfx))
                soundEffects(sfx) = soundEffect
            End If
            soundEffect.Play(volume, 0.0F, 0.0F)
        End If
    End Sub
End Class
