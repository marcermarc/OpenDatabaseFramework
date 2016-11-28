Imports System.Collections.Generic
Imports System.Threading

Public Class ThreadUnique(Of T)
    Implements IDisposable

    Private m_lock As Object 'Object to synclock
    Private m_values As Dictionary(Of Integer, T)

    Public Property Value As T
        Get
            SyncLock m_lock
                If m_values.ContainsKey(Thread.CurrentThread.ManagedThreadId) Then
                    Return m_values(Thread.CurrentThread.ManagedThreadId)
                Else
                    Return Nothing
                End If
            End SyncLock
        End Get
        Set(value As T)
            SyncLock m_lock
                If m_values.ContainsKey(Thread.CurrentThread.ManagedThreadId) Then
                    m_values(Thread.CurrentThread.ManagedThreadId) = value
                Else
                    m_values.Add(Thread.CurrentThread.ManagedThreadId, value)
                End If
            End SyncLock
        End Set
    End Property



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
