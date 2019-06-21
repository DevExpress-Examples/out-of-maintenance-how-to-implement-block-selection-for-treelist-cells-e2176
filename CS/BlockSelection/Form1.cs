using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList;

namespace BlockSelection
{
    public partial class Form1 : Form
    {
        private BlockSelector selector;
        public Form1()
        {
            InitializeComponent();
            selector = new BlockSelector(treeList1);
        }

        private void treeList1_KeyDown(object sender, KeyEventArgs e)
        {
            TreeList list = (TreeList)sender;
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.Insert))
                if (selector != null && selector.List == list && !selector.Block.IsEmpty())
                {
                    Clipboard.SetDataObject(selector.GetSelectedValues());
                    e.Handled = true;
                }
        }

        DataTable CreateTable()
        {
            DataTable table = new DataTable();
            DataRow dataRow;
            table.Columns.Add("ID", typeof(System.Int32));
            table.Columns.Add("ID_2", typeof(System.Int32));
            table.Columns.Add("Name", typeof(System.String));
            table.Columns.Add("Priority", typeof(System.String));
            dataRow = table.NewRow();
            dataRow["ID"] = 1;
            dataRow["ID_2"] = 0;
            dataRow["Name"] = "Project A";
            dataRow["Priority"] = "High";
            table.Rows.Add(dataRow);

            dataRow = table.NewRow();
            dataRow["ID"] = 2;
            dataRow["ID_2"] = 1;
            dataRow["Name"] = "Deliverable 1";
            dataRow["Priority"] = "Normal";
            table.Rows.Add(dataRow);

            dataRow = table.NewRow();
            dataRow["ID"] = 3;
            dataRow["ID_2"] = 2;
            dataRow["Name"] = "This task is mine A";
            dataRow["Priority"] = "High";
            table.Rows.Add(dataRow);

            dataRow = table.NewRow();
            dataRow["ID"] = 4;
            dataRow["ID_2"] = 2;
            dataRow["Name"] = "This task isn't mine";
            dataRow["Priority"] = "Low";
            table.Rows.Add(dataRow);

            dataRow = table.NewRow();
            dataRow["ID"] = 5;
            dataRow["ID_2"] = 0;
            dataRow["Name"] = "Project B";
            dataRow["Priority"] = "Normal";
            table.Rows.Add(dataRow);

            dataRow = table.NewRow();
            dataRow["ID"] = 6;
            dataRow["ID_2"] = 5;
            dataRow["Name"] = "This task is mine B";
            dataRow["Priority"] = "High";
            table.Rows.Add(dataRow);

            return table;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            treeList1.DataSource = CreateTable();
        }
    }
}
