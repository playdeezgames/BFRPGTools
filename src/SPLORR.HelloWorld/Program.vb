Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Input

Module Program
    ReadOnly palette As IReadOnlyDictionary(Of Hue, Color) = New Dictionary(Of Hue, Color) From
            {
                {Hue.Black, New Color(0, 0, 0)},
                {Hue.Blue, New Color(42, 75, 215)},
                {Hue.Green, New Color(29, 105, 20)},
                {Hue.Cyan, New Color(41, 208, 208)},
                {Hue.Red, New Color(173, 35, 35)},
                {Hue.Purple, New Color(129, 38, 192)},
                {Hue.Brown, New Color(129, 74, 25)},
                {Hue.LightGray, New Color(160, 160, 160)},
                {Hue.DarkGray, New Color(87, 87, 87)},
                {Hue.LightBlue, New Color(157, 175, 255)},
                {Hue.LightGreen, New Color(129, 197, 122)},
                {Hue.Orange, New Color(255, 146, 51)},
                {Hue.Pink, New Color(255, 205, 243)},
                {Hue.Tan, New Color(233, 222, 187)},
                {Hue.Yellow, New Color(255, 238, 51)},
                {Hue.White, New Color(255, 255, 255)}
            }
    ReadOnly commandTable As IReadOnlyDictionary(Of Command, Func(Of KeyboardState, GamePadState, Boolean)) = New Dictionary(Of Command, Func(Of KeyboardState, GamePadState, Boolean)) From
            {
                {Command.A, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Space) OrElse keyboard.IsKeyDown(Keys.Enter) OrElse gamePad.IsButtonDown(Buttons.A)},
                {Command.B, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Escape) OrElse keyboard.IsKeyDown(Keys.NumPad0) OrElse gamePad.IsButtonDown(Buttons.B)},
                {Command.Up, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Up) OrElse keyboard.IsKeyDown(Keys.NumPad8) OrElse gamePad.DPad.Up = Global.Microsoft.Xna.Framework.Input.ButtonState.Pressed},
                {Command.Down, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Down) OrElse keyboard.IsKeyDown(Keys.NumPad2) OrElse gamePad.DPad.Down = Global.Microsoft.Xna.Framework.Input.ButtonState.Pressed},
                {Command.Left, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Left) OrElse keyboard.IsKeyDown(Keys.NumPad4) OrElse gamePad.DPad.Left = Global.Microsoft.Xna.Framework.Input.ButtonState.Pressed},
                {Command.Right, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Right) OrElse keyboard.IsKeyDown(Keys.NumPad6) OrElse gamePad.DPad.Right = Global.Microsoft.Xna.Framework.Input.ButtonState.Pressed},
                {Command.[Select], Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Tab) OrElse gamePad.IsButtonDown(Buttons.Back)},
                {Command.Start, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Enter) OrElse gamePad.IsButtonDown(Buttons.Start)}
            }
    ReadOnly sfxFilenames As IReadOnlyDictionary(Of Sfx, String) = New Dictionary(Of Sfx, String) From
            {
                {Sfx.Ok, "Content/WooHoo.wav"}
            }
    Sub Main(args As String())
        Using host As New Host(Of Hue, Command, Sfx, Model.HWModel, HWAssets)(
            New GameController(New HostConfig(), New MenuStateConfig(Of Hue, Command, HWAssets) With
                               {
                                .BackgroundHue = Hue.Black,
                                .FooterHue = Hue.LightGray,
                                .HeaderHue = Hue.Orange,
                                .HiliteHue = Hue.LightBlue,
                                .FooterText = "Up/Down/Select | A/Start/Space | B/Esc",
                                .GetFont = Function(a) a.Font,
                                .CancelCommand = Function(cmd) cmd = Command.B,
                                .ChooseCommand = Function(cmd) cmd = Command.A OrElse cmd = Command.Start,
                                .NextItemCommand = Function(cmd) cmd = Command.Down OrElse cmd = Command.Select,
                                .PreviousItemCommand = Function(cmd) cmd = Command.Up
                               }),
            New PaletteRenderer(Of Hue, Color)(palette),
            New BaseInputManager(Of Command)(commandTable),
            New BaseSfxManager(Of Sfx)(sfxFilenames))
            host.Run()
        End Using
    End Sub
End Module
