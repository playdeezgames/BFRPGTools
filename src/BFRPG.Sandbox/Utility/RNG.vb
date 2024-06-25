Friend Module RNG
    Private ReadOnly random As New Random

    Friend Function RollDice(dieCount As Integer, dieSize As Integer) As Integer
        Dim total = 0
        For Each dummy In Enumerable.Range(1, dieCount)
            total += RollDie(dieSize)
        Next
        Return total
    End Function

    Private Function RollDie(dieSize As Integer) As Integer
        Return random.Next(1, dieSize + 1)
    End Function
End Module
