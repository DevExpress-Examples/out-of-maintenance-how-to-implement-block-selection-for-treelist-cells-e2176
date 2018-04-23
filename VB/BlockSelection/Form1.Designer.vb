Imports Microsoft.VisualBasic
Imports System
Namespace BlockSelection
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Me.treeList1 = New DevExpress.XtraTreeList.TreeList()
			Me.table1BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
			Me.treeListDataBaseDataSet = New BlockSelection.TreeListDataBaseDataSet()
			Me.table1TableAdapter1 = New BlockSelection.TreeListDataBaseDataSetTableAdapters.Table1TableAdapter()
			Me.colName = New DevExpress.XtraTreeList.Columns.TreeListColumn()
			Me.colPriority = New DevExpress.XtraTreeList.Columns.TreeListColumn()
			CType(Me.treeList1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.table1BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.treeListDataBaseDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' treeList1
			' 
			Me.treeList1.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() { Me.colName, Me.colPriority})
			Me.treeList1.DataSource = Me.table1BindingSource1
			Me.treeList1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.treeList1.Location = New System.Drawing.Point(0, 0)
			Me.treeList1.Name = "treeList1"
			Me.treeList1.OptionsBehavior.Editable = False
			Me.treeList1.OptionsSelection.EnableAppearanceFocusedRow = False
			Me.treeList1.ParentFieldName = "ID_2"
			Me.treeList1.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowOnlyInEditor
			Me.treeList1.Size = New System.Drawing.Size(452, 350)
			Me.treeList1.TabIndex = 0
'			Me.treeList1.KeyDown += New System.Windows.Forms.KeyEventHandler(Me.treeList1_KeyDown);
			' 
			' table1BindingSource1
			' 
			Me.table1BindingSource1.DataMember = "Table1"
			Me.table1BindingSource1.DataSource = Me.treeListDataBaseDataSet
			' 
			' treeListDataBaseDataSet
			' 
			Me.treeListDataBaseDataSet.DataSetName = "TreeListDataBaseDataSet"
			Me.treeListDataBaseDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
			' 
			' table1TableAdapter1
			' 
			Me.table1TableAdapter1.ClearBeforeFill = True
			' 
			' colName
			' 
			Me.colName.Caption = "Name"
			Me.colName.FieldName = "Name"
			Me.colName.Name = "colName"
			Me.colName.Visible = True
			Me.colName.VisibleIndex = 0
			Me.colName.Width = 108
			' 
			' colPriority
			' 
			Me.colPriority.Caption = "Priority"
			Me.colPriority.FieldName = "Priority"
			Me.colPriority.Name = "colPriority"
			Me.colPriority.Visible = True
			Me.colPriority.VisibleIndex = 1
			Me.colPriority.Width = 107
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(452, 350)
			Me.Controls.Add(Me.treeList1)
			Me.Name = "Form1"
			Me.Text = "Form1"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
			CType(Me.treeList1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.table1BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.treeListDataBaseDataSet, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private WithEvents treeList1 As DevExpress.XtraTreeList.TreeList
		Private treeListDataBaseDataSet As TreeListDataBaseDataSet
		Private table1BindingSource1 As System.Windows.Forms.BindingSource
		Private table1TableAdapter1 As BlockSelection.TreeListDataBaseDataSetTableAdapters.Table1TableAdapter
		Private colName As DevExpress.XtraTreeList.Columns.TreeListColumn
		Private colPriority As DevExpress.XtraTreeList.Columns.TreeListColumn
	End Class
End Namespace

