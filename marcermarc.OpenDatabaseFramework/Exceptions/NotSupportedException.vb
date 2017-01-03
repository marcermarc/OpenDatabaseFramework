﻿Imports System
Imports System.Runtime.Serialization

Namespace Exceptions
    <Serializable>
    Friend Class NotSupportedException
        Inherits System.NotSupportedException

        Public Sub New()
        End Sub

        Public Sub New(message As String)
            MyBase.New(message)
        End Sub

        Public Sub New(message As String, innerException As Exception)
            MyBase.New(message, innerException)
        End Sub

        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
        End Sub
    End Class
End Namespace
