Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraTreeList

Namespace BlockSelection
	Partial Public Class Form1
		Inherits Form

		Private selector As BlockSelector
		Public Sub New()
			InitializeComponent()
			selector = New BlockSelector(treeList1)
		End Sub

		Private Sub treeList1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles treeList1.KeyDown
			Dim list As TreeList = DirectCast(sender, TreeList)
			If e.Control AndAlso (e.KeyCode = Keys.C OrElse e.KeyCode = Keys.Insert) Then
				If selector IsNot Nothing AndAlso selector.List Is list AndAlso Not selector.Block.IsEmpty() Then
					Clipboard.SetDataObject(selector.GetSelectedValues())
					e.Handled = True
				End If
			End If
		End Sub

		Private Function CreateTable() As DataTable
			Dim table As New DataTable()
			Dim dataRow As DataRow
			table.Columns.Add("ID", GetType(System.Int32))
			table.Columns.Add("ID_2", GetType(System.Int32))
			table.Columns.Add("Name", GetType(System.String))
			table.Columns.Add("Priority", GetType(System.String))
			dataRow = table.NewRow()
			dataRow("ID") = 1
			dataRow("ID_2") = 0
			dataRow("Name") = "Project A"
			dataRow("Priority") = "High"
			table.Rows.Add(dataRow)

			dataRow = table.NewRow()
			dataRow("ID") = 2
			dataRow("ID_2") = 1
			dataRow("Name") = "Deliverable 1"
			dataRow("Priority") = "Normal"
			table.Rows.Add(dataRow)

			dataRow = table.NewRow()
			dataRow("ID") = 3
			dataRow("ID_2") = 2
			dataRow("Name") = "This task is mine A"
			dataRow("Priority") = "High"
			table.Rows.Add(dataRow)

			dataRow = table.NewRow()
			dataRow("ID") = 4
			dataRow("ID_2") = 2
			dataRow("Name") = "This task isn't mine"
			dataRow("Priority") = "Low"
			table.Rows.Add(dataRow)

			dataRow = table.NewRow()
			dataRow("ID") = 5
			dataRow("ID_2") = 0
			dataRow("Name") = "Project B"
			dataRow("Priority") = "Normal"
			table.Rows.Add(dataRow)

			dataRow = table.NewRow()
			dataRow("ID") = 6
			dataRow("ID_2") = 5
			dataRow("Name") = "This task is mine B"
			dataRow("Priority") = "High"
			table.Rows.Add(dataRow)

			Return table
		End Function

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			treeList1.DataSource = CreateTable()
		End Sub
	End Class
End Namespace
