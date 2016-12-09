'Imports System
'Imports System.Data
'Namespace BaseObjects

'    ''' <summary>
'    ''' Class inherited by objects in EntityCollections
'    ''' </summary>
'    Public MustInherit Class DataEntityObject
'        Inherits DatabaseObject

'        ''' <summary>
'        ''' data store reference
'        ''' </summary>
'        Protected datarow As DataRow

'        Public Sub New(d As DataRow)
'            MyBase.New()
'            datarow = d
'        End Sub

'        ''' <summary>
'        ''' Adds updatestatement to save changes
'        ''' </summary>
'        ''' <returns>success</returns>
'        Public MustOverride Function DeleteMe() As Boolean

'        ''' <summary>
'        ''' Adds deletestatement to delete data from Table
'        ''' </summary>
'        ''' <returns>success</returns>
'        Public MustOverride Function InsertMe() As Boolean

'        ''' <summary>
'        ''' Adds insertstatement to insert data into Table
'        ''' </summary>
'        ''' <returns>success</returns>
'        Public MustOverride Function UpdateMe() As Boolean

'    End Class

'End Namespace