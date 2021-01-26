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
    public partial class ctables : UserControl
    {
        recent_Opened_Cts _LastOpened;
        
        public ctables()
        {
            _LastOpened = new recent_Opened_Cts()
            {
                Tag = "Recent Opened CT", // hada ghykoon title
                Dock = DockStyle.Fill

            };

            _LastOpened.onCloseBtnClick += (o, ev) => {
                _LastOpened.Dispose();
                InitializeComponent();
                ctables_Load(this, new EventArgs());
            };

            _LastOpened.onFileClick += (o, ev) =>
            {
                string sel_file = _LastOpened.GetSelectedRecentFile;
                if (!string.IsNullOrEmpty(sel_file))
                {
                    List<ct_class> tmp = engine.extract_ct(sel_file);
                    if (tmp != null)
                    {
                        tmp = null;
                        InitializeComponent();
                        ctables_Load(this, new EventArgs());
                        FillDgv(sel_file);
                        _LastOpened.Dispose();
                    }
                }
            };

            Controls.Add(_LastOpened);
        }

        private void ctables_Load(object sender, EventArgs e)
        {




            dgv.AllowDrop = true;

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ColumnHeadersVisible = false;
            dgv.BackgroundColor = Color.FromArgb(255, 40, 40, 40);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //dgv.RowTemplate.Resizable = DataGridViewTriState.True;
            dgv.RowTemplate.Height = 40;
            dgv.Columns[0].Width = 35;
            dgv.Columns[2].Width = 60;

            dgv.ScrollBars = ScrollBars.None;


            dgv.MouseWheel += (o, args) =>
            {
                if (args.Delta > 0 && dgv.FirstDisplayedScrollingRowIndex > 0)
                {
                    dgv.FirstDisplayedScrollingRowIndex--;
                }
                else if (args.Delta < 0)
                {
                    dgv.FirstDisplayedScrollingRowIndex++;
                }
            };


            dgv.DragDrop += CheatTablesList_DragDrop;
            dgv.DragEnter += CheatTablesList_DragEnter;
        }



        private void FillDgv(string path)
        {
            if (path != null)
            {
                List<ct_class> cheats_tables = engine.extract_ct(path).OrderBy(each => each.ID).ToList();

                if (cheats_tables.Count > 0)
                {
                    dgv.Rows.Clear();

                    foreach (var cheat in cheats_tables)
                    {
                        dgv.Rows.Add(new object[] { cheat.ID, cheat.Description.Replace("\"", ""), Properties.Resources._unchecked });
                    }
                }
            }
        }



        private void CheatTablesList_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void CheatTablesList_DragDrop(object sender, DragEventArgs e)
        {
            string[] path = e.Data.GetData(DataFormats.FileDrop)  as string[];
            FillDgv(path[0]);
        }

      


        private void browsCt(object o , EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.DefaultExt = "ct";
            opf.AddExtension = true;
            opf.Filter = "(*.ct)|*.ct";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                FillDgv(opf.FileName);
            }
        }


        private void resetAll(object s , EventArgs args)
        {
            if (dgv.Rows.Count > 0)
            {
                foreach (DataGridViewRow r in dgv.Rows)
                {
                    DataGridViewImageCell cell = r.Cells[2] as DataGridViewImageCell;

                    if (cell.Description == "true")
                    {
                        cell.Description = "false";
                        cell.Value = Properties.Resources._unchecked;
                    }
                }
            }
        }


        private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dgvr = dgv.Rows[e.RowIndex];
            DataGridViewImageCell cell = dgvr.Cells[2] as DataGridViewImageCell;

            if (cell.Description == "false")
            {
                cell.Description = "true";
                cell.Value = Properties.Resources.checkeed;
            }
            else
            {
                cell.Description = "false";
                cell.Value = Properties.Resources._unchecked;
            }
        }

        private void dgv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow r in dgv.SelectedRows)
                    {
                        DataGridViewImageCell cell = r.Cells[2] as DataGridViewImageCell;

                        if (cell.Description == "false")
                        {
                            cell.Description = "true";
                            cell.Value = Properties.Resources.checkeed;
                        }
                        else
                        {
                            cell.Description = "false";
                            cell.Value = Properties.Resources._unchecked;
                        }
                    }
                }
            }
        }
    }
}
