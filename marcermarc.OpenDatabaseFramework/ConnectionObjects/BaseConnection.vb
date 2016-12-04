Imports System.Data.Common

Namespace ConnectionObjects

    Friend MustInherit Class BaseConnection
        Protected Connection As DbConnection
        Protected Command As DbCommand
        Protected DataAdapter As DbDataAdapter
        Protected Datareader As DbDataReader
        Protected Transaction As DbTransaction

        Protected Sub New(ByRef conn As DbConnection, ByRef comm As DbCommand, ByRef da As DbDataAdapter, ByRef dr As DbDataReader, ByRef trans As DbTransaction)
            Connection = conn
            Command = comm
            DataAdapter = da
            Datareader = dr
            Transaction = trans
        End Sub

    End Class

End Namespace