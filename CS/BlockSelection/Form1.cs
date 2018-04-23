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

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'treeListDataBaseDataSet.Table1' table. You can move, or remove it, as needed.
            this.table1TableAdapter1.Fill(this.treeListDataBaseDataSet.Table1);

        }
    }
}
