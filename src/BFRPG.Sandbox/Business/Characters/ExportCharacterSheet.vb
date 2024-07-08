﻿Imports System.IO
Imports System.Text

Friend Module ExportCharacterSheet
    Friend Sub Run(context As DataContext, characterId As Integer)
        Dim details = context.Characters.ReadDetails(characterId)
        Dim generatedOn = DateTimeOffset.Now
        Dim filename = $"{details.CharacterName} - {details.RaceName} - {details.ClassName} - {details.Level} - {generatedOn:yyyyMMddHHmmss}.html"
        Dim builder As New StringBuilder
        With builder
            .Append("<html>")
            .Append($"<title>{details.CharacterName} - {details.RaceName} - {details.ClassName} - {details.Level}</title>")
            .Append("<body>")
            .Append("<table border=""border"">")

            .Append("<tr>")
            .Append($"<td colspan=""2"">Name: {details.CharacterName}</td>")
            .Append($"<td>Player: {details.PlayerName}</td>")
            .Append("</tr>")

            .Append("<tr>")
            .Append($"<td>Race: {details.RaceName}</td>")
            .Append($"<td>XP: {details.ExperiencePoints}</td>")
            .Append($"<td rowspan=""2"">Desc: {details.CharacterDescription}</td>")
            .Append("</tr>")

            .Append("<tr>")
            .Append($"<td>Class: {details.ClassName}</td>")
            .Append($"<td>Level: {details.Level}</td>")
            .Append("</tr>")

            .Append("<tr>")

            .Append("<td>")
            .Append("<table border=""border""><tr><th>Ability</th><th>Score</th><th>Modifier</th></tr>")
            Dim abilityDetails = CharacterAbilities.ReadAllDetailsForCharacter(context.Connection, characterId)
            For Each abilityDetail In abilityDetails
                .Append($"<tr><td>{abilityDetail.AbilityAbbreviation}</td><td>{abilityDetail.AbilityScore}</td><td>{abilityDetail.Modifier}</td></tr>")
            Next
            .Append("</table>")
            .Append("</td>")

            .Append("<td>")
            .Append("<table>")
            .Append($"<tr><td>AC: TODO</td></tr>")
            .Append($"<tr><td>HP: {details.HitPoints}</td></tr>")
            .Append($"<tr><td>AB: TODO</td></tr>")
            .Append("</table>")
            .Append("</td>")

            .Append("<td>")
            .Append("<table>")
            .Append($"<tr><td>Movement: TODO</td></tr>")
            .Append($"<tr><td>Money: TODO</td></tr>")
            .Append("</table>")
            .Append("</td>")

            .Append("</tr>")

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
    End Sub
End Module
