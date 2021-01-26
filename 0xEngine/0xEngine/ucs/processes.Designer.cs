namespace GenGine.ucs
{
    partial class processes
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nextBtn = new System.Windows.Forms.Panel();
            this.nextTxt = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.processes_list = new System.Windows.Forms.ListView();
            this.pid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nextBtn.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nextBtn
            // 
            this.nextBtn.BackColor = System.Drawing.Color.DarkSalmon;
            this.nextBtn.Controls.Add(this.nextTxt);
            this.nextBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nextBtn.Location = new System.Drawing.Point(99, 32);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(83, 37);
            this.nextBtn.TabIndex = 5;
            this.nextBtn.Tag = "next";
            this.nextBtn.Click += new System.EventHandler(this.selectedProcNext);
            // 
            // nextTxt
            // 
            this.nextTxt.AutoSize = true;
            this.nextTxt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nextTxt.Location = new System.Drawing.Point(23, 12);
            this.nextTxt.Name = "nextTxt";
            this.nextTxt.Size = new System.Drawing.Size(36, 13);
            this.nextTxt.TabIndex = 0;
            this.nextTxt.Tag = "next";
            this.nextTxt.Text = "n e x t";
            this.nextTxt.Click += new System.EventHandler(this.selectedProcNext);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Coral;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel1.Location = new System.Drawing.Point(10, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(83, 37);
            this.panel1.TabIndex = 4;
            this.panel1.Click += new System.EventHandler(this.refresh_processes_list);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Location = new System.Drawing.Point(12, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "r e f r e s h";
            this.label3.Click += new System.EventHandler(this.refresh_processes_list);
            // 
            // processes_list
            // 
            this.processes_list.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.processes_list.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.processes_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.pid,
            this.pname});
            this.processes_list.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processes_list.ForeColor = System.Drawing.Color.Gray;
            this.processes_list.FullRowSelect = true;
            this.processes_list.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.processes_list.Location = new System.Drawing.Point(10, 93);
            this.processes_list.MultiSelect = false;
            this.processes_list.Name = "processes_list";
            this.processes_list.Size = new System.Drawing.Size(652, 359);
            this.processes_list.TabIndex = 3;
            this.processes_list.TileSize = new System.Drawing.Size(548, 80);
            this.processes_list.UseCompatibleStateImageBehavior = false;
            this.processes_list.View = System.Windows.Forms.View.Tile;
            this.processes_list.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.processes_list_MouseDoubleClick);
            // 
            // pid
            // 
            this.pid.Text = "Process Id";
            this.pid.Width = 180;
            // 
            // pname
            // 
            this.pname.Text = "Process Name";
            this.pname.Width = 280;
            // 
            // processes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Controls.Add(this.nextBtn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.processes_list);
            this.Name = "processes";
            this.Size = new System.Drawing.Size(633, 485);
            this.Load += new System.EventHandler(this.processes_Load);
            this.nextBtn.ResumeLayout(false);
            this.nextBtn.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView processes_list;
        private System.Windows.Forms.ColumnHeader pid;
        private System.Windows.Forms.ColumnHeader pname;
        public System.Windows.Forms.Panel nextBtn;
        public System.Windows.Forms.Label nextTxt;
    }
}
