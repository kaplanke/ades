namespace Test_OpenSURF_Winform
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbImages = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbImages = new System.Windows.Forms.ListBox();
            this.tbDirectory = new System.Windows.Forms.TextBox();
            this.bnLoad = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbUpright = new System.Windows.Forms.CheckBox();
            this.nudOctaves = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudIntervals = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudInit_Sample = new System.Windows.Forms.NumericUpDown();
            this.labelThreshold = new System.Windows.Forms.Label();
            this.nudThreshold = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudinterp_steps = new System.Windows.Forms.NumericUpDown();
            this.bnReflect = new System.Windows.Forms.Button();
            this.cbAutoReflect = new System.Windows.Forms.CheckBox();
            this.gbSURFImage = new System.Windows.Forms.GroupBox();
            this.pnSURFImage = new System.Windows.Forms.Panel();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.bnClearLog = new System.Windows.Forms.Button();
            this.gbImages.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOctaves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntervals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInit_Sample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudinterp_steps)).BeginInit();
            this.gbSURFImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbImages
            // 
            this.gbImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gbImages.Controls.Add(this.bnLoad);
            this.gbImages.Controls.Add(this.tbDirectory);
            this.gbImages.Controls.Add(this.lbImages);
            this.gbImages.Controls.Add(this.label1);
            this.gbImages.Location = new System.Drawing.Point(8, 4);
            this.gbImages.Name = "gbImages";
            this.gbImages.Size = new System.Drawing.Size(326, 786);
            this.gbImages.TabIndex = 0;
            this.gbImages.TabStop = false;
            this.gbImages.Text = "Images";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Directory:";
            // 
            // lbImages
            // 
            this.lbImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbImages.FormattingEnabled = true;
            this.lbImages.IntegralHeight = false;
            this.lbImages.Location = new System.Drawing.Point(6, 61);
            this.lbImages.Name = "lbImages";
            this.lbImages.Size = new System.Drawing.Size(309, 714);
            this.lbImages.TabIndex = 1;
            this.lbImages.SelectedIndexChanged += new System.EventHandler(this.lbImages_SelectedIndexChanged);
            // 
            // tbDirectory
            // 
            this.tbDirectory.Location = new System.Drawing.Point(6, 35);
            this.tbDirectory.Name = "tbDirectory";
            this.tbDirectory.Size = new System.Drawing.Size(274, 20);
            this.tbDirectory.TabIndex = 2;
            this.tbDirectory.Text = "D:\\Photosynth";
            // 
            // bnLoad
            // 
            this.bnLoad.Location = new System.Drawing.Point(286, 35);
            this.bnLoad.Name = "bnLoad";
            this.bnLoad.Size = new System.Drawing.Size(29, 20);
            this.bnLoad.TabIndex = 3;
            this.bnLoad.Text = "...";
            this.bnLoad.UseVisualStyleBackColor = true;
            this.bnLoad.Click += new System.EventHandler(this.bnLoad_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.bnClearLog);
            this.groupBox1.Controls.Add(this.lbLog);
            this.groupBox1.Controls.Add(this.cbAutoReflect);
            this.groupBox1.Controls.Add(this.bnReflect);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.nudinterp_steps);
            this.groupBox1.Controls.Add(this.labelThreshold);
            this.groupBox1.Controls.Add(this.nudThreshold);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.nudInit_Sample);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.nudIntervals);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nudOctaves);
            this.groupBox1.Controls.Add(this.cbUpright);
            this.groupBox1.Location = new System.Drawing.Point(340, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(889, 264);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "OpenSURF parameters";
            // 
            // cbUpright
            // 
            this.cbUpright.AutoSize = true;
            this.cbUpright.Location = new System.Drawing.Point(20, 26);
            this.cbUpright.Name = "cbUpright";
            this.cbUpright.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbUpright.Size = new System.Drawing.Size(60, 17);
            this.cbUpright.TabIndex = 0;
            this.cbUpright.Text = "Upright";
            this.cbUpright.UseVisualStyleBackColor = true;
            this.cbUpright.CheckedChanged += new System.EventHandler(this.cbUpright_CheckedChanged);
            // 
            // nudOctaves
            // 
            this.nudOctaves.Location = new System.Drawing.Point(153, 26);
            this.nudOctaves.Name = "nudOctaves";
            this.nudOctaves.Size = new System.Drawing.Size(54, 20);
            this.nudOctaves.TabIndex = 1;
            this.nudOctaves.ValueChanged += new System.EventHandler(this.nudOctaves_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(99, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Octaves:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(221, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Intervals:";
            // 
            // nudIntervals
            // 
            this.nudIntervals.Location = new System.Drawing.Point(275, 26);
            this.nudIntervals.Name = "nudIntervals";
            this.nudIntervals.Size = new System.Drawing.Size(54, 20);
            this.nudIntervals.TabIndex = 3;
            this.nudIntervals.ValueChanged += new System.EventHandler(this.nudIntervals_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(348, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Init Sample:";
            // 
            // nudInit_Sample
            // 
            this.nudInit_Sample.Location = new System.Drawing.Point(414, 26);
            this.nudInit_Sample.Name = "nudInit_Sample";
            this.nudInit_Sample.Size = new System.Drawing.Size(54, 20);
            this.nudInit_Sample.TabIndex = 5;
            this.nudInit_Sample.ValueChanged += new System.EventHandler(this.nudInit_Sample_ValueChanged);
            // 
            // labelThreshold
            // 
            this.labelThreshold.AutoSize = true;
            this.labelThreshold.Location = new System.Drawing.Point(482, 26);
            this.labelThreshold.Name = "labelThreshold";
            this.labelThreshold.Size = new System.Drawing.Size(79, 13);
            this.labelThreshold.TabIndex = 8;
            this.labelThreshold.Text = "Threshold(%%):";
            // 
            // nudThreshold
            // 
            this.nudThreshold.Location = new System.Drawing.Point(565, 26);
            this.nudThreshold.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudThreshold.Name = "nudThreshold";
            this.nudThreshold.Size = new System.Drawing.Size(54, 20);
            this.nudThreshold.TabIndex = 7;
            this.nudThreshold.ValueChanged += new System.EventHandler(this.nudThreshold_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(644, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Interp Steps:";
            // 
            // nudinterp_steps
            // 
            this.nudinterp_steps.Location = new System.Drawing.Point(715, 26);
            this.nudinterp_steps.Name = "nudinterp_steps";
            this.nudinterp_steps.Size = new System.Drawing.Size(54, 20);
            this.nudinterp_steps.TabIndex = 9;
            this.nudinterp_steps.ValueChanged += new System.EventHandler(this.nudinterp_steps_ValueChanged);
            // 
            // bnReflect
            // 
            this.bnReflect.Location = new System.Drawing.Point(20, 57);
            this.bnReflect.Name = "bnReflect";
            this.bnReflect.Size = new System.Drawing.Size(75, 23);
            this.bnReflect.TabIndex = 11;
            this.bnReflect.Text = "&Reflect";
            this.bnReflect.UseVisualStyleBackColor = true;
            this.bnReflect.Click += new System.EventHandler(this.bnReflect_Click);
            // 
            // cbAutoReflect
            // 
            this.cbAutoReflect.AutoSize = true;
            this.cbAutoReflect.Checked = true;
            this.cbAutoReflect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoReflect.Location = new System.Drawing.Point(114, 57);
            this.cbAutoReflect.Name = "cbAutoReflect";
            this.cbAutoReflect.Size = new System.Drawing.Size(85, 17);
            this.cbAutoReflect.TabIndex = 12;
            this.cbAutoReflect.Text = "Auto-Reflect";
            this.cbAutoReflect.UseVisualStyleBackColor = true;
            // 
            // gbSURFImage
            // 
            this.gbSURFImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSURFImage.Controls.Add(this.pnSURFImage);
            this.gbSURFImage.Location = new System.Drawing.Point(340, 274);
            this.gbSURFImage.Name = "gbSURFImage";
            this.gbSURFImage.Size = new System.Drawing.Size(889, 516);
            this.gbSURFImage.TabIndex = 2;
            this.gbSURFImage.TabStop = false;
            this.gbSURFImage.Text = "SURF Image";
            // 
            // pnSURFImage
            // 
            this.pnSURFImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnSURFImage.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnSURFImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnSURFImage.Location = new System.Drawing.Point(7, 19);
            this.pnSURFImage.Name = "pnSURFImage";
            this.pnSURFImage.Size = new System.Drawing.Size(867, 486);
            this.pnSURFImage.TabIndex = 0;
            // 
            // lbLog
            // 
            this.lbLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLog.FormattingEnabled = true;
            this.lbLog.Location = new System.Drawing.Point(20, 86);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(854, 160);
            this.lbLog.TabIndex = 13;
            // 
            // bnClearLog
            // 
            this.bnClearLog.Location = new System.Drawing.Point(821, 97);
            this.bnClearLog.Name = "bnClearLog";
            this.bnClearLog.Size = new System.Drawing.Size(43, 23);
            this.bnClearLog.TabIndex = 14;
            this.bnClearLog.Text = "Clear";
            this.bnClearLog.UseVisualStyleBackColor = true;
            this.bnClearLog.Click += new System.EventHandler(this.bnClearLog_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1241, 802);
            this.Controls.Add(this.gbSURFImage);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbImages);
            this.Name = "Form1";
            this.Text = "Test OpenSURF 1.000";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbImages.ResumeLayout(false);
            this.gbImages.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOctaves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntervals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInit_Sample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudinterp_steps)).EndInit();
            this.gbSURFImage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbImages;
        private System.Windows.Forms.Button bnLoad;
        private System.Windows.Forms.TextBox tbDirectory;
        private System.Windows.Forms.ListBox lbImages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbUpright;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudOctaves;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudIntervals;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudInit_Sample;
        private System.Windows.Forms.Label labelThreshold;
        private System.Windows.Forms.NumericUpDown nudThreshold;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudinterp_steps;
        private System.Windows.Forms.Button bnReflect;
        private System.Windows.Forms.CheckBox cbAutoReflect;
        private System.Windows.Forms.GroupBox gbSURFImage;
        private System.Windows.Forms.Panel pnSURFImage;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.Button bnClearLog;
    }
}

