Imports System.Data

Public MustInherit Class Command
    Private m_lock As Object 'For Synclock

    Protected Friend m_Command As IDbCommand


End Class
