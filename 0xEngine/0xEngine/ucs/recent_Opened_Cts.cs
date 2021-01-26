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
    public partial class recent_Opened_Cts : UserControl
    {
        public event EventHandler onCloseBtnClick;
        public event EventHandler onFileClick;
        private string filePath = null;

        public bool BrowsedCtYet
        {
            get => Properties.Settings.Default.browsedCtYet;
        }

        public string GetSelectedRecentFile
        {
            get => filePath;
        }

        public recent_Opened_Cts()
        {
            InitializeComponent();
            dgv.BackgroundColor = Color.FromArgb(255, 40, 40, 40);

            dgv.RowTemplate.Height = 30;
            dgv.Columns[0].Width = 150;

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

            Properties.Settings.Default.browsedCtYet = false;
            pictureBox1.Cursor = Cursors.Hand;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.browsedCtYet = true;
            onCloseBtnClick?.Invoke(this, new EventArgs());

        }

        private void recent_Opened_Cts_Load(object sender, EventArgs e)
        {
            title.Text = this.Tag.ToString(); //  from call
            Dictionary<int, object[]> recentlyOpened = engine.get_recent_opened_cts();
            if (recentlyOpened.Count >= 1 )
            {
                MessageBox.Show(DateTime.Now.ToString());
                for (int i = 1; i <= recentlyOpened.Count; i++)
                {
                    dgv.Rows.Add(new object[] { recentlyOpened[i][0], recentlyOpened[i][1]});
                }
            }
        }

        private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            Properties.Settings.Default.browsedCtYet = true;
            DataGridViewRow dgvr = dgv.Rows[e.RowIndex];
            filePath = dgvr.Cells[1].Value.ToString() + dgvr.Cells[0].Value.ToString();
            onFileClick?.Invoke(this, new EventArgs());

  

            //try
            //{


            //}
            //catch (Exception)
            //{
            //    throw new Exception("~ Unknow error happend , Just close Recently Opened Window !");
            //}
        }
    }
}
