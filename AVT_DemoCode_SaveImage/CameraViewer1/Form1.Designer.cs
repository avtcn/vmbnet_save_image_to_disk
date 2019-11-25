namespace CameraViewer1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_PictureBox = new System.Windows.Forms.PictureBox();
            this.tB_Log = new System.Windows.Forms.TextBox();
            this.panel_Left = new System.Windows.Forms.Panel();
            this.Log = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.p_SetCam1 = new System.Windows.Forms.Panel();
            this.lB_expTime = new System.Windows.Forms.Label();
            this.bT_Acqsingle = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.bT_Acqure = new System.Windows.Forms.Button();
            this.gBx_Cam1Tri = new System.Windows.Forms.GroupBox();
            this.tb_maxRate = new System.Windows.Forms.TextBox();
            this.panel_setRate = new System.Windows.Forms.Panel();
            this.bt_setRate = new System.Windows.Forms.Button();
            this.tB_setRate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rBt_FixTri = new System.Windows.Forms.RadioButton();
            this.rBt_SoftTri = new System.Windows.Forms.RadioButton();
            this.rBt_HardTri = new System.Windows.Forms.RadioButton();
            this.rBt_Freerun = new System.Windows.Forms.RadioButton();
            this.bT_OpenCamera = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel_Right = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_SavePath = new System.Windows.Forms.TextBox();
            this.bT_SelectSavePath = new System.Windows.Forms.Button();
            this.cB_SaveImage = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.List_Cam = new System.Windows.Forms.ListBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.Num_softTri = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBox)).BeginInit();
            this.panel_Left.SuspendLayout();
            this.Log.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.p_SetCam1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.gBx_Cam1Tri.SuspendLayout();
            this.panel_setRate.SuspendLayout();
            this.panel_Right.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_softTri)).BeginInit();
            this.SuspendLayout();
            // 
            // m_PictureBox
            // 
            this.m_PictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_PictureBox.BackColor = System.Drawing.Color.Navy;
            this.m_PictureBox.Location = new System.Drawing.Point(3, 9);
            this.m_PictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.m_PictureBox.Name = "m_PictureBox";
            this.m_PictureBox.Size = new System.Drawing.Size(582, 450);
            this.m_PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.m_PictureBox.TabIndex = 3;
            this.m_PictureBox.TabStop = false;
            // 
            // tB_Log
            // 
            this.tB_Log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_Log.Location = new System.Drawing.Point(5, 18);
            this.tB_Log.Margin = new System.Windows.Forms.Padding(2);
            this.tB_Log.Multiline = true;
            this.tB_Log.Name = "tB_Log";
            this.tB_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tB_Log.Size = new System.Drawing.Size(572, 155);
            this.tB_Log.TabIndex = 6;
            // 
            // panel_Left
            // 
            this.panel_Left.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Left.Controls.Add(this.m_PictureBox);
            this.panel_Left.Controls.Add(this.Log);
            this.panel_Left.Location = new System.Drawing.Point(-1, 0);
            this.panel_Left.Margin = new System.Windows.Forms.Padding(2);
            this.panel_Left.Name = "panel_Left";
            this.panel_Left.Size = new System.Drawing.Size(593, 643);
            this.panel_Left.TabIndex = 9;
            this.panel_Left.Resize += new System.EventHandler(this.panel_Left_Resize);
            // 
            // Log
            // 
            this.Log.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Log.Controls.Add(this.tB_Log);
            this.Log.Location = new System.Drawing.Point(3, 462);
            this.Log.Name = "Log";
            this.Log.Size = new System.Drawing.Size(582, 178);
            this.Log.TabIndex = 25;
            this.Log.TabStop = false;
            this.Log.Text = "Log";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.p_SetCam1);
            this.groupBox1.Controls.Add(this.bT_OpenCamera);
            this.groupBox1.Location = new System.Drawing.Point(3, 305);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(346, 335);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "相机操作";
            // 
            // p_SetCam1
            // 
            this.p_SetCam1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.p_SetCam1.Controls.Add(this.lB_expTime);
            this.p_SetCam1.Controls.Add(this.bT_Acqsingle);
            this.p_SetCam1.Controls.Add(this.label1);
            this.p_SetCam1.Controls.Add(this.trackBar1);
            this.p_SetCam1.Controls.Add(this.bT_Acqure);
            this.p_SetCam1.Controls.Add(this.gBx_Cam1Tri);
            this.p_SetCam1.Location = new System.Drawing.Point(4, 58);
            this.p_SetCam1.Name = "p_SetCam1";
            this.p_SetCam1.Size = new System.Drawing.Size(336, 271);
            this.p_SetCam1.TabIndex = 24;
            // 
            // lB_expTime
            // 
            this.lB_expTime.AutoSize = true;
            this.lB_expTime.Location = new System.Drawing.Point(252, 51);
            this.lB_expTime.Name = "lB_expTime";
            this.lB_expTime.Size = new System.Drawing.Size(45, 13);
            this.lB_expTime.TabIndex = 27;
            this.lB_expTime.Text = "1500 μs";
            // 
            // bT_Acqsingle
            // 
            this.bT_Acqsingle.Location = new System.Drawing.Point(191, 5);
            this.bT_Acqsingle.Name = "bT_Acqsingle";
            this.bT_Acqsingle.Size = new System.Drawing.Size(100, 23);
            this.bT_Acqsingle.TabIndex = 26;
            this.bT_Acqsingle.Text = "单张采图/拍照";
            this.bT_Acqsingle.UseVisualStyleBackColor = true;
            this.bT_Acqsingle.Click += new System.EventHandler(this.bT_Acqsingle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "曝光时间";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(67, 48);
            this.trackBar1.Maximum = 80000;
            this.trackBar1.Minimum = 71;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(179, 45);
            this.trackBar1.TabIndex = 25;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 100;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // bT_Acqure
            // 
            this.bT_Acqure.Location = new System.Drawing.Point(3, 5);
            this.bT_Acqure.Name = "bT_Acqure";
            this.bT_Acqure.Size = new System.Drawing.Size(119, 23);
            this.bT_Acqure.TabIndex = 22;
            this.bT_Acqure.Text = "开始采集/OnLine";
            this.bT_Acqure.UseVisualStyleBackColor = true;
            this.bT_Acqure.Click += new System.EventHandler(this.bT_Acqure_Click);
            // 
            // gBx_Cam1Tri
            // 
            this.gBx_Cam1Tri.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gBx_Cam1Tri.Controls.Add(this.label4);
            this.gBx_Cam1Tri.Controls.Add(this.Num_softTri);
            this.gBx_Cam1Tri.Controls.Add(this.panel_setRate);
            this.gBx_Cam1Tri.Controls.Add(this.rBt_FixTri);
            this.gBx_Cam1Tri.Controls.Add(this.rBt_SoftTri);
            this.gBx_Cam1Tri.Controls.Add(this.rBt_HardTri);
            this.gBx_Cam1Tri.Controls.Add(this.rBt_Freerun);
            this.gBx_Cam1Tri.Location = new System.Drawing.Point(3, 94);
            this.gBx_Cam1Tri.Name = "gBx_Cam1Tri";
            this.gBx_Cam1Tri.Size = new System.Drawing.Size(330, 177);
            this.gBx_Cam1Tri.TabIndex = 23;
            this.gBx_Cam1Tri.TabStop = false;
            this.gBx_Cam1Tri.Text = "触发设置";
            // 
            // tb_maxRate
            // 
            this.tb_maxRate.Location = new System.Drawing.Point(106, 30);
            this.tb_maxRate.Name = "tb_maxRate";
            this.tb_maxRate.ReadOnly = true;
            this.tb_maxRate.Size = new System.Drawing.Size(50, 20);
            this.tb_maxRate.TabIndex = 27;
            // 
            // panel_setRate
            // 
            this.panel_setRate.Controls.Add(this.bt_setRate);
            this.panel_setRate.Controls.Add(this.tB_setRate);
            this.panel_setRate.Controls.Add(this.tb_maxRate);
            this.panel_setRate.Controls.Add(this.label3);
            this.panel_setRate.Location = new System.Drawing.Point(161, 106);
            this.panel_setRate.Name = "panel_setRate";
            this.panel_setRate.Size = new System.Drawing.Size(163, 56);
            this.panel_setRate.TabIndex = 31;
            this.panel_setRate.Visible = false;
            // 
            // bt_setRate
            // 
            this.bt_setRate.Location = new System.Drawing.Point(12, 3);
            this.bt_setRate.Name = "bt_setRate";
            this.bt_setRate.Size = new System.Drawing.Size(75, 23);
            this.bt_setRate.TabIndex = 30;
            this.bt_setRate.Text = "设定帧率";
            this.bt_setRate.UseVisualStyleBackColor = true;
            this.bt_setRate.Click += new System.EventHandler(this.bt_setRate_Click);
            // 
            // tB_setRate
            // 
            this.tB_setRate.Location = new System.Drawing.Point(106, 3);
            this.tB_setRate.Name = "tB_setRate";
            this.tB_setRate.Size = new System.Drawing.Size(50, 20);
            this.tB_setRate.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "最大允许帧率：";
            // 
            // rBt_FixTri
            // 
            this.rBt_FixTri.AutoSize = true;
            this.rBt_FixTri.Location = new System.Drawing.Point(64, 108);
            this.rBt_FixTri.Name = "rBt_FixTri";
            this.rBt_FixTri.Size = new System.Drawing.Size(73, 17);
            this.rBt_FixTri.TabIndex = 26;
            this.rBt_FixTri.TabStop = true;
            this.rBt_FixTri.Text = "固定帧率";
            this.rBt_FixTri.UseVisualStyleBackColor = true;
            this.rBt_FixTri.CheckedChanged += new System.EventHandler(this.rBt_FixTri_CheckedChanged);
            // 
            // rBt_SoftTri
            // 
            this.rBt_SoftTri.AutoSize = true;
            this.rBt_SoftTri.Location = new System.Drawing.Point(64, 82);
            this.rBt_SoftTri.Name = "rBt_SoftTri";
            this.rBt_SoftTri.Size = new System.Drawing.Size(61, 17);
            this.rBt_SoftTri.TabIndex = 25;
            this.rBt_SoftTri.TabStop = true;
            this.rBt_SoftTri.Text = "软触发";
            this.rBt_SoftTri.UseVisualStyleBackColor = true;
            this.rBt_SoftTri.CheckedChanged += new System.EventHandler(this.rBt_SoftTri_CheckedChanged);
            // 
            // rBt_HardTri
            // 
            this.rBt_HardTri.AutoSize = true;
            this.rBt_HardTri.Location = new System.Drawing.Point(64, 56);
            this.rBt_HardTri.Name = "rBt_HardTri";
            this.rBt_HardTri.Size = new System.Drawing.Size(73, 17);
            this.rBt_HardTri.TabIndex = 24;
            this.rBt_HardTri.Text = "硬件触发";
            this.rBt_HardTri.UseVisualStyleBackColor = true;
            this.rBt_HardTri.CheckedChanged += new System.EventHandler(this.rBt_HardTri_CheckedChanged);
            // 
            // rBt_Freerun
            // 
            this.rBt_Freerun.AutoSize = true;
            this.rBt_Freerun.Checked = true;
            this.rBt_Freerun.Location = new System.Drawing.Point(64, 30);
            this.rBt_Freerun.Name = "rBt_Freerun";
            this.rBt_Freerun.Size = new System.Drawing.Size(73, 17);
            this.rBt_Freerun.TabIndex = 23;
            this.rBt_Freerun.TabStop = true;
            this.rBt_Freerun.Text = "自由运行";
            this.rBt_Freerun.UseVisualStyleBackColor = true;
            this.rBt_Freerun.CheckedChanged += new System.EventHandler(this.rBt_Freerun_CheckedChanged);
            // 
            // bT_OpenCamera
            // 
            this.bT_OpenCamera.Location = new System.Drawing.Point(6, 29);
            this.bT_OpenCamera.Name = "bT_OpenCamera";
            this.bT_OpenCamera.Size = new System.Drawing.Size(75, 23);
            this.bT_OpenCamera.TabIndex = 20;
            this.bT_OpenCamera.Text = "打开相机";
            this.bT_OpenCamera.UseVisualStyleBackColor = true;
            this.bT_OpenCamera.Click += new System.EventHandler(this.bT_OpenCamera_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            // 
            // panel_Right
            // 
            this.panel_Right.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Right.Controls.Add(this.label2);
            this.panel_Right.Controls.Add(this.tb_SavePath);
            this.panel_Right.Controls.Add(this.bT_SelectSavePath);
            this.panel_Right.Controls.Add(this.cB_SaveImage);
            this.panel_Right.Controls.Add(this.groupBox2);
            this.panel_Right.Controls.Add(this.groupBox1);
            this.panel_Right.Location = new System.Drawing.Point(597, 0);
            this.panel_Right.Name = "panel_Right";
            this.panel_Right.Size = new System.Drawing.Size(352, 643);
            this.panel_Right.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 264);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "存图路径:";
            // 
            // tb_SavePath
            // 
            this.tb_SavePath.Location = new System.Drawing.Point(121, 261);
            this.tb_SavePath.Name = "tb_SavePath";
            this.tb_SavePath.ReadOnly = true;
            this.tb_SavePath.Size = new System.Drawing.Size(168, 20);
            this.tb_SavePath.TabIndex = 28;
            // 
            // bT_SelectSavePath
            // 
            this.bT_SelectSavePath.Location = new System.Drawing.Point(295, 258);
            this.bT_SelectSavePath.Name = "bT_SelectSavePath";
            this.bT_SelectSavePath.Size = new System.Drawing.Size(39, 23);
            this.bT_SelectSavePath.TabIndex = 27;
            this.bT_SelectSavePath.Text = "浏览";
            this.bT_SelectSavePath.UseVisualStyleBackColor = true;
            this.bT_SelectSavePath.Click += new System.EventHandler(this.bT_SelectSavePath_Click);
            // 
            // cB_SaveImage
            // 
            this.cB_SaveImage.AutoSize = true;
            this.cB_SaveImage.Location = new System.Drawing.Point(230, 236);
            this.cB_SaveImage.Name = "cB_SaveImage";
            this.cB_SaveImage.Size = new System.Drawing.Size(74, 17);
            this.cB_SaveImage.TabIndex = 26;
            this.cB_SaveImage.Text = "保存图片";
            this.cB_SaveImage.UseVisualStyleBackColor = true;
            this.cB_SaveImage.CheckedChanged += new System.EventHandler(this.cB_SaveImage_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.List_Cam);
            this.groupBox2.Location = new System.Drawing.Point(2, 2);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(347, 184);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "相机列表";
            // 
            // List_Cam
            // 
            this.List_Cam.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.List_Cam.FormattingEnabled = true;
            this.List_Cam.Location = new System.Drawing.Point(6, 17);
            this.List_Cam.Margin = new System.Windows.Forms.Padding(2);
            this.List_Cam.Name = "List_Cam";
            this.List_Cam.Size = new System.Drawing.Size(339, 160);
            this.List_Cam.TabIndex = 7;
            this.List_Cam.SelectedIndexChanged += new System.EventHandler(this.List_Cam_SelectedIndexChanged);
            // 
            // Num_softTri
            // 
            this.Num_softTri.Location = new System.Drawing.Point(267, 82);
            this.Num_softTri.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.Num_softTri.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_softTri.Name = "Num_softTri";
            this.Num_softTri.Size = new System.Drawing.Size(50, 20);
            this.Num_softTri.TabIndex = 32;
            this.Num_softTri.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(158, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "软触发间隔（ms)：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 646);
            this.Controls.Add(this.panel_Right);
            this.Controls.Add(this.panel_Left);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "AVTCamera_Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBox)).EndInit();
            this.panel_Left.ResumeLayout(false);
            this.Log.ResumeLayout(false);
            this.Log.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.p_SetCam1.ResumeLayout(false);
            this.p_SetCam1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.gBx_Cam1Tri.ResumeLayout(false);
            this.gBx_Cam1Tri.PerformLayout();
            this.panel_setRate.ResumeLayout(false);
            this.panel_setRate.PerformLayout();
            this.panel_Right.ResumeLayout(false);
            this.panel_Right.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_softTri)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox m_PictureBox;
        private System.Windows.Forms.TextBox tB_Log;
        private System.Windows.Forms.Panel panel_Left;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button bT_OpenCamera;
        private System.Windows.Forms.Button bT_Acqure;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox Log;
        private System.Windows.Forms.Panel panel_Right;
        private System.Windows.Forms.GroupBox gBx_Cam1Tri;
        private System.Windows.Forms.RadioButton rBt_SoftTri;
        private System.Windows.Forms.RadioButton rBt_HardTri;
        private System.Windows.Forms.RadioButton rBt_Freerun;
        private System.Windows.Forms.Panel p_SetCam1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button bT_Acqsingle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox List_Cam;
        private System.Windows.Forms.Label lB_expTime;
        private System.Windows.Forms.CheckBox cB_SaveImage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_SavePath;
        private System.Windows.Forms.Button bT_SelectSavePath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox tb_maxRate;
        private System.Windows.Forms.RadioButton rBt_FixTri;
        private System.Windows.Forms.TextBox tB_setRate;
        private System.Windows.Forms.Button bt_setRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel_setRate;
        private System.Windows.Forms.NumericUpDown Num_softTri;
        private System.Windows.Forms.Label label4;
    }
}

