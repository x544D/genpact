namespace GenPact.__.Sub_Panel_Features_Ucs
{
    partial class FlyHckUc
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
            this.FlyHack_Switcher = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.FlyHack_status = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FlyHack_Switcher)).BeginInit();
            this.SuspendLayout();
            // 
            // FlyHack_Switcher
            // 
            this.FlyHack_Switcher.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FlyHack_Switcher.Image = global::GenPact.Properties.Resources.off;
            this.FlyHack_Switcher.Location = new System.Drawing.Point(18, 26);
            this.FlyHack_Switcher.Name = "FlyHack_Switcher";
            this.FlyHack_Switcher.Size = new System.Drawing.Size(41, 38);
            this.FlyHack_Switcher.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.FlyHack_Switcher.TabIndex = 25;
            this.FlyHack_Switcher.TabStop = false;
            this.FlyHack_Switcher.Tag = "OFF";
            this.FlyHack_Switcher.Click += new System.EventHandler(this.FlyHack_Switcher_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FloralWhite;
            this.label3.Location = new System.Drawing.Point(65, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "WingMode - Fly H@ck";
            // 
            // FlyHack_status
            // 
            this.FlyHack_status.AutoSize = true;
            this.FlyHack_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.FlyHack_status.ForeColor = System.Drawing.Color.Red;
            this.FlyHack_status.Location = new System.Drawing.Point(65, 46);
            this.FlyHack_status.Name = "FlyHack_status";
            this.FlyHack_status.Size = new System.Drawing.Size(27, 13);
            this.FlyHack_status.TabIndex = 28;
            this.FlyHack_status.Text = "OFF";
            // 
            // FlyHckUc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.FlyHack_status);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FlyHack_Switcher);
            this.Name = "FlyHckUc";
            this.Size = new System.Drawing.Size(368, 190);
            this.Tag = "FlyH@ck";
            this.Load += new System.EventHandler(this.FlyHckUc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FlyHack_Switcher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox FlyHack_Switcher;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label FlyHack_status;
    }
}
