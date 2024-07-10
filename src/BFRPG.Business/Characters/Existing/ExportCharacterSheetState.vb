﻿Imports System.IO
Imports System.Text
Imports System.Web

Friend Class ExportCharacterSheetState
    Inherits BaseState

    Private ReadOnly characterId As Integer

    Public Sub New(data As IDataContext, ui As IUIContext, endState As IState, characterId As Integer)
        MyBase.New(data, ui, endState)
        Me.characterId = characterId
    End Sub

    Private Function TD(
                       text As String,
                       Optional colSpan As Integer = 1,
                       Optional rowSpan As Integer = 1,
                       Optional htmlEncode As Boolean = True,
                       Optional header As Boolean = False) As String
        Return $"<{If(header, "th", "td")} colspan=""{colSpan}"" rowspan=""{rowSpan}"">{If(htmlEncode, HttpUtility.HtmlEncode(text), text)}</{If(header, "th", "td")}>"
    End Function
    Private Function TR(ParamArray lines As String()) As String
        Return $"<tr>{String.Join(String.Empty, lines)}</tr>"
    End Function
    Private Function TABLE(border As Boolean, ParamArray lines As String()) As String
        Return $"<table{If(border," border=""border""","")}>{String.Join(String.Empty, lines)}</table>"
    End Function

    Public Overrides Function Run() As IState
        Dim details = data.Characters.ReadDetails(characterId)
        Dim generatedOn = DateTimeOffset.Now
        Dim filename = $"{details.CharacterName} - {details.RaceName} - {details.ClassName} - {details.Level} - {generatedOn:yyyyMMddHHmmss}.html"
        Dim builder As New StringBuilder
        Dim abilityScoresTable As New List(Of String) From
                {
                    TR(
                        TD("Ability", header:=True),
                        TD("Score", header:=True),
                        TD("Modifier", header:=True))
                }
        abilityScoresTable.AddRange(
                data.Characters.Abilities(characterId).ReadAllDetailsForCharacter().Select(Function(abilityDetail) TR(
                        TD($"{abilityDetail.AbilityAbbreviation}"),
                        TD($"{abilityDetail.AbilityScore}"),
                        TD($"{abilityDetail.Modifier}"))))
        With builder
            .Append("<html>")
            .Append($"<head><title>{details.CharacterName} - {details.RaceName} - {details.ClassName} - {details.Level}</title></head>")
            .Append("<body>")
            .Append("<table border=""border"">")
            .Append(TABLE(True,
                TR(
                    TD($"Name: {details.CharacterName}", colSpan:=2),
                    TD($"Player: {details.PlayerName}")),
                TR(
                    TD($"Race: {details.RaceName}"),
                    TD($"XP: {details.ExperiencePoints}"),
                    TD($"Desc: {details.CharacterDescription}", rowSpan:=2)),
                TR(
                    TD($"Class: {details.ClassName}"),
                    TD($"Level: {details.Level}")),
                TR(
                    TD(
                        TABLE(
                            True,
                            abilityScoresTable.ToArray), htmlEncode:=False),
                    TD(
                        TABLE(
                            False,
                            TR(TD($"AC: TODO")),
                            TR(TD($"HP: {details.HitPoints}")),
                            TR(TD($"AB: {details.AttackBonus}"))), htmlEncode:=False),
                    TD(TABLE(False, TR(TD($"Movement: TODO")), TR(TD($"Money: {details.Money}"))), htmlEncode:=False))
                    ))

            .Append("<tr>")

            .Append("<td rowspan=""2"">")
            .Append("<table>")
            .Append("<tr><th>Spells/Abilities:</th></tr>")
            'spells/abilities
            .Append("</table>")
            .Append("</td>")

            .Append("<td colspan=""2"">")
            .Append("<table>")
            .Append("<tr><th>Saving Throws:</th></tr>")
            'saving throws
            .Append("</table>")
            .Append("</td>")

            .Append("</tr>")

            .Append("<tr>")

            .Append("<td colspan=""2"">")
            .Append("<table>")
            .Append("<tr><th>Weapon</th><th>AB</th><th>Damage</th><th>Range</th></tr>")
            'weapons
            .Append("</table>")
            .Append("</td>")

            .Append("</tr>")

            .Append("</table>")
            .Append("</body>")
            .Append("</html>")
        End With
        File.WriteAllText(filename, builder.ToString)
        Dim p As New Process() With
            {
                .StartInfo = New ProcessStartInfo(filename) With
                {
                    .UseShellExecute = True
                }
            }
        p.Start()
        Return endState
    End Function
End Class
