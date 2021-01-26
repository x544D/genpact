namespace GenPact.__
{
    partial class SpeedUc
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SpeedBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.CurrentSpeed = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.UltiCD_SwitcherValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SkillCD_SwitcherValue = new System.Windows.Forms.TextBox();
            this.SkillCD_Switcher = new System.Windows.Forms.PictureBox();
            this.UltiCD_Switcher = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SkillCD_Switcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltiCD_Switcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.OrangeRed;
            this.checkBox1.Location = new System.Drawing.Point(42, 37);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(78, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Go Crazy ?";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // SpeedBar
            // 
            this.SpeedBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.SpeedBar.Cursor = System.Windows.Forms.Cursors.Default;
            this.SpeedBar.LargeChange = 1;
            this.SpeedBar.Location = new System.Drawing.Point(32, 69);
            this.SpeedBar.Maximum = 5;
            this.SpeedBar.Minimum = 1;
            this.SpeedBar.Name = "SpeedBar";
            this.SpeedBar.Size = new System.Drawing.Size(305, 45);
            this.SpeedBar.TabIndex = 1;
            this.SpeedBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.SpeedBar.Value = 1;
            this.SpeedBar.Scroll += new System.EventHandler(this.SpeedBar_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightSalmon;
            this.label1.Location = new System.Drawing.Point(208, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Current Speed :";
            // 
            // CurrentSpeed
            // 
            this.CurrentSpeed.AutoSize = true;
            this.CurrentSpeed.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.CurrentSpeed.ForeColor = System.Drawing.Color.Bisque;
            this.CurrentSpeed.Location = new System.Drawing.Point(295, 38);
            this.CurrentSpeed.Name = "CurrentSpeed";
            this.CurrentSpeed.Size = new System.Drawing.Size(15, 14);
            this.CurrentSpeed.TabIndex = 3;
            this.CurrentSpeed.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FloralWhite;
            this.label2.Location = new System.Drawing.Point(211, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Ultimate Cooldown";
            // 
            // UltiCD_SwitcherValue
            // 
            this.UltiCD_SwitcherValue.BackColor = System.Drawing.Color.Black;
            this.UltiCD_SwitcherValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UltiCD_SwitcherValue.Font = new System.Drawing.Font("Tahoma", 27F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.UltiCD_SwitcherValue.ForeColor = System.Drawing.Color.LawnGreen;
            this.UltiCD_SwitcherValue.Location = new System.Drawing.Point(252, 151);
            this.UltiCD_SwitcherValue.MaxLength = 2;
            this.UltiCD_SwitcherValue.Name = "UltiCD_SwitcherValue";
            this.UltiCD_SwitcherValue.Size = new System.Drawing.Size(58, 40);
            this.UltiCD_SwitcherValue.TabIndex = 21;
            this.UltiCD_SwitcherValue.Text = "5";
            this.UltiCD_SwitcherValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FloralWhite;
            this.label3.Location = new System.Drawing.Point(47, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Skills Cooldown";
            // 
            // SkillCD_SwitcherValue
            // 
            this.SkillCD_SwitcherValue.BackColor = System.Drawing.Color.Black;
            this.SkillCD_SwitcherValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SkillCD_SwitcherValue.Font = new System.Drawing.Font("Tahoma", 27F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.SkillCD_SwitcherValue.ForeColor = System.Drawing.Color.LawnGreen;
            this.SkillCD_SwitcherValue.Location = new System.Drawing.Point(88, 151);
            this.SkillCD_SwitcherValue.MaxLength = 2;
            this.SkillCD_SwitcherValue.Name = "SkillCD_SwitcherValue";
            this.SkillCD_SwitcherValue.Size = new System.Drawing.Size(58, 40);
            this.SkillCD_SwitcherValue.TabIndex = 25;
            this.SkillCD_SwitcherValue.Text = "5";
            this.SkillCD_SwitcherValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SkillCD_Switcher
            // 
            this.SkillCD_Switcher.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SkillCD_Switcher.Image = global::GenPact.Properties.Resources.toggleoff;
            this.SkillCD_Switcher.Location = new System.Drawing.Point(39, 151);
            this.SkillCD_Switcher.Name = "SkillCD_Switcher";
            this.SkillCD_Switcher.Size = new System.Drawing.Size(43, 40);
            this.SkillCD_Switcher.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SkillCD_Switcher.TabIndex = 24;
            this.SkillCD_Switcher.TabStop = false;
            this.SkillCD_Switcher.Tag = "off";
            this.SkillCD_Switcher.Click += new System.EventHandler(this.off_on_Switcher);
            // 
            // UltiCD_Switcher
            // 
            this.UltiCD_Switcher.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UltiCD_Switcher.Image = global::GenPact.Properties.Resources.toggleoff;
            this.UltiCD_Switcher.Location = new System.Drawing.Point(203, 151);
            this.UltiCD_Switcher.Name = "UltiCD_Switcher";
            this.UltiCD_Switcher.Size = new System.Drawing.Size(43, 40);
            this.UltiCD_Switcher.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.UltiCD_Switcher.TabIndex = 23;
            this.UltiCD_Switcher.TabStop = false;
            this.UltiCD_Switcher.Tag = "off";
            this.UltiCD_Switcher.Click += new System.EventHandler(this.off_on_Switcher);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox3.Image = global::GenPact.Properties.Resources.howto;
            this.pictureBox3.Location = new System.Drawing.Point(342, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(23, 21);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 20;
            this.pictureBox3.TabStop = false;
            // 
            // SpeedUc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SkillCD_SwitcherValue);
            this.Controls.Add(this.SkillCD_Switcher);
            this.Controls.Add(this.UltiCD_Switcher);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UltiCD_SwitcherValue);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.CurrentSpeed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SpeedBar);
            this.Controls.Add(this.checkBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "SpeedUc";
            this.Size = new System.Drawing.Size(368, 222);
            this.Tag = "Speed & Cooldown";
            this.Load += new System.EventHandler(this.SpeedUc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SpeedBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SkillCD_Switcher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltiCD_Switcher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TrackBar SpeedBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label CurrentSpeed;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox UltiCD_Switcher;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox UltiCD_SwitcherValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SkillCD_SwitcherValue;
        private System.Windows.Forms.PictureBox SkillCD_Switcher;
    }
}
