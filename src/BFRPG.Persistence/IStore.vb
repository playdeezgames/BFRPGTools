Public Interface IStore
    Function ReadAll(
                    columns As IEnumerable(Of String),
                    viewName As String,
                    Optional forColumns As IReadOnlyDictionary(Of String, Object) = Nothing) As IEnumerable(Of IReadOnlyDictionary(Of String, Object))
    Sub Delete(
              tableName As String,
              forColumns As IReadOnlyDictionary(Of String, Object))
    Sub Update(
              tableName As String,
              updateColumns As IReadOnlyDictionary(Of String, Object),
              forColumns As IReadOnlyDictionary(Of String, Object))
    Function Insert(
                tableName As String,
                insertColumns As IReadOnlyDictionary(Of String, Object),
                Optional returnColumns As IEnumerable(Of String) = Nothing,
                Optional updateColumns As IEnumerable(Of String) = Nothing) As IReadOnlyDictionary(Of String, Object)
End Interface
