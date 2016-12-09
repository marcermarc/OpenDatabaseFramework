Imports System
Imports System.Data

Public MustInherit Class Connection
    Implements IDisposable

    Private m_lock As Object 'For Synclock

    Protected Friend m_Connection As IDbConnection

    Protected m_ConnectionStirng As String

    Private m_DefaultTransaction As IUnique(Of Transaction)

    Protected Sub New(in_ThreadUniqueTransactions As Boolean, in_ThreadUniqueCommands As Boolean)
        If in_ThreadUniqueTransactions Then
            m_DefaultTransaction = New ThreadUnique(Of Transaction)
        Else
            m_DefaultTransaction = New Unique(Of Transaction)
        End If
    End Sub



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
