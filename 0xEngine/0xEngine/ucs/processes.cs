using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenGine.ucs
{
    public partial class processes : UserControl
    {
        public event EventHandler OnProcessNextButtonClick;

        string selectedProcName = string.Empty;
        public processes()
        {
            InitializeComponent();
        }


        private void processes_Load(object sender, EventArgs e)
        {
            processes_list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            processes_list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            refreshProcs();
        }


        private void refresh_processes_list(object sender, EventArgs e)
        {
            refreshProcs();
        }

        private void selectedProcNext(object sender, EventArgs e)
        {
            if (processes_list.SelectedItems.Count == 1)
            {
                OnProcessNextButtonClick?.Invoke(this, new EventArgs());
            }
        }



        private void refreshProcs()
        {
            var _procs = engine.getProcs();
            processes_list.Items.Clear();

            foreach (var item in _procs)
            {
                ListViewItem lvl = new ListViewItem(item.ProcessName) { Font = new Font("Consolas", 12f, FontStyle.Bold) };
                lvl.SubItems.Add("ID : " + item.Id.ToString());

                processes_list.Items.Add(lvl);
            }
        }


        private void processes_list_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (processes_list.SelectedItems.Count  == 1)
            {
                ListViewItem item = processes_list.SelectedItems[0];
                if (item.Bounds.Contains(e.Location)) selectedProcNext(null,null);
            }
        }
    }
}
