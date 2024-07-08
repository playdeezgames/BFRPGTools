Public Interface IStore
    Function ReadAll(columns As IEnumerable(Of String), tableName As String) As IEnumerable(Of IReadOnlyDictionary(Of String, Object))
End Interface
