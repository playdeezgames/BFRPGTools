﻿Friend Module MainMenu
    Friend Sub Run(context As DataContext, ui As IUIContext)
        Do
            ui.Clear()
            Select Case ui.Choose(
                (Mood.Prompt, Prompts.MainMenu),
                Choices.Players,
                Choices.Quit)
                Case Choices.Quit
                    If ui.Confirm((Mood.Danger, Confirms.Quit)) Then
                        Exit Do
                    End If
                Case Choices.Players
                    PlayersMenu.Run(context)
            End Select
        Loop
    End Sub
End Module
