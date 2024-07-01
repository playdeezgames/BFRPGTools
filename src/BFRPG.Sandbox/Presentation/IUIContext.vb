﻿Public Interface IUIContext
    Sub Clear()
    Function Choose(Of TResult)(
                               prompt As (Mood As Mood, Text As String),
                               ParamArray choices As (Text As String, Value As TResult)()) As TResult
    Function Choose(
                    prompt As (Mood As Mood, Text As String),
                    ParamArray choices As String()) As String
    Function Confirm(prompt As (Mood As Mood, Text As String)) As Boolean
End Interface
