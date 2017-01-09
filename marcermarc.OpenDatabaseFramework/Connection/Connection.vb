Imports System
Imports System.Collections.Generic
Imports System.Data

'Comments comming soon

Public MustInherit Class Connection
    Implements IDisposable

    Private m_lock As Object 'For Synclock

    Protected Friend m_Connection As IDbConnection

    Protected m_ConnectionStirng As String

    Private m_DefaultTransaction As IUnique(Of Transaction)

    Private ReadOnly m_ThreadUniqueCommands As Boolean

    Protected Sub New(threadUniqueTransactions As Boolean, threadUniqueCommands As Boolean)
        If threadUniqueTransactions Then
            m_DefaultTransaction = New ThreadUnique(Of Transaction)
        Else
            m_DefaultTransaction = New Unique(Of Transaction)
        End If
        m_ThreadUniqueCommands = threadUniqueCommands
    End Sub

#Region "Public"
    ReadOnly Property DefaultTransaction As Transaction
        Get
            If m_DefaultTransaction.Value Is Nothing Then
                m_DefaultTransaction.Value = NewTransaction()
            End If

            Return m_DefaultTransaction.Value
        End Get
    End Property

    Function Execute(statement As SqlStatement) As DataTable
        Return DefaultTransaction.Execute(statement)
    End Function

    Function Execute(statements As IList(Of SqlStatement)) As DataSet
        Return m_DefaultTransaction.Value.Execute(statements)
    End Function

    Function ExecuteWithoutResult(statement As SqlStatement) As Integer
        Return DefaultTransaction.ExecuteWithoutResult(statement)
    End Function

    Function ExecuteWithoutResult(statements As IList(Of SqlStatement)) As Integer
        Return m_DefaultTransaction.Value.ExecuteWithoutResult(statements)
    End Function
#End Region

#Region "MustOverride"
    Protected MustOverride Sub InitConnection()
    Protected MustOverride Function NewTransaction() As Transaction
    Protected MustOverride Function NewCommand() As Command
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Dient zur Erkennung redundanter Aufrufe.

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: verwalteten Zustand (verwaltete Objekte) entsorgen.
            End If

            ' TODO: nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalize() weiter unten überschreiben.
            ' TODO: große Felder auf Null setzen.
        End If
        disposedValue = True
    End Sub

    ' TODO: Finalize() nur überschreiben, wenn Dispose(disposing As Boolean) weiter oben Code zur Bereinigung nicht verwalteter Ressourcen enthält.
    'Protected Overrides Sub Finalize()
    '    ' Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(disposing As Boolean) weiter oben ein.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Dieser Code wird von Visual Basic hinzugefügt, um das Dispose-Muster richtig zu implementieren.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(disposing As Boolean) weiter oben ein.
        Dispose(True)
        ' TODO: Auskommentierung der folgenden Zeile aufheben, wenn Finalize() oben überschrieben wird.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
