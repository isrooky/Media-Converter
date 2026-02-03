namespace MediaConverter
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            PanelDragDrop = new Panel();
            label1 = new Label();
            InputTB = new TextBox();
            InputLabel = new Label();
            OutputLabel = new Label();
            OutputTB = new TextBox();
            FormatCB = new ComboBox();
            label2 = new Label();
            ConvertBtn = new Button();
            label3 = new Label();
            ReduceCB = new ComboBox();
            ReduceBtn = new Button();
            label4 = new Label();
            PanelVideos = new Panel();
            ExtractFrameBtn = new Button();
            CutBtn = new Button();
            StartTrack = new TrackBar();
            EndTrack = new TrackBar();
            LBFin = new Label();
            LBInicio = new Label();
            label6 = new Label();
            AudioCB = new ComboBox();
            ExtractBtn = new Button();
            label5 = new Label();
            PanelImages = new Panel();
            CreatePNGBtn = new Button();
            LBSoftness = new Label();
            SoftnessTrack = new TrackBar();
            LBTolerance = new Label();
            ToleranceTrack = new TrackBar();
            ColorPreviewPanel = new Panel();
            PickColorBtn = new Button();
            label9 = new Label();
            ExtractChannelsCB = new ComboBox();
            label8 = new Label();
            ExtractChannelsBtn = new Button();
            FormatImageCB = new ComboBox();
            label7 = new Label();
            ConvertImageBtn = new Button();
            PanelDragDrop.SuspendLayout();
            PanelVideos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)StartTrack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EndTrack).BeginInit();
            PanelImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SoftnessTrack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ToleranceTrack).BeginInit();
            SuspendLayout();
            // 
            // PanelDragDrop
            // 
            PanelDragDrop.AllowDrop = true;
            PanelDragDrop.BorderStyle = BorderStyle.FixedSingle;
            PanelDragDrop.Controls.Add(label1);
            PanelDragDrop.Location = new Point(12, 12);
            PanelDragDrop.Name = "PanelDragDrop";
            PanelDragDrop.Size = new Size(398, 148);
            PanelDragDrop.TabIndex = 0;
            PanelDragDrop.DragDrop += PanelDragDrop_DragDrop;
            PanelDragDrop.DragEnter += PanelDragDrop_DragEnter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F);
            label1.Location = new Point(55, 50);
            label1.Name = "label1";
            label1.Size = new Size(275, 37);
            label1.TabIndex = 0;
            label1.Text = "Drag your media here";
            // 
            // InputTB
            // 
            InputTB.Location = new Point(68, 166);
            InputTB.Name = "InputTB";
            InputTB.Size = new Size(342, 23);
            InputTB.TabIndex = 1;
            // 
            // InputLabel
            // 
            InputLabel.AutoSize = true;
            InputLabel.Location = new Point(12, 169);
            InputLabel.Name = "InputLabel";
            InputLabel.Size = new Size(38, 15);
            InputLabel.TabIndex = 2;
            InputLabel.Text = "Input:";
            // 
            // OutputLabel
            // 
            OutputLabel.AutoSize = true;
            OutputLabel.Location = new Point(12, 198);
            OutputLabel.Name = "OutputLabel";
            OutputLabel.Size = new Size(48, 15);
            OutputLabel.TabIndex = 4;
            OutputLabel.Text = "Output:";
            // 
            // OutputTB
            // 
            OutputTB.Location = new Point(68, 195);
            OutputTB.Name = "OutputTB";
            OutputTB.Size = new Size(342, 23);
            OutputTB.TabIndex = 3;
            // 
            // FormatCB
            // 
            FormatCB.DropDownStyle = ComboBoxStyle.DropDownList;
            FormatCB.FormattingEnabled = true;
            FormatCB.Location = new Point(74, 47);
            FormatCB.Name = "FormatCB";
            FormatCB.Size = new Size(121, 23);
            FormatCB.TabIndex = 5;
            FormatCB.SelectedIndexChanged += FormatCB_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 20F);
            label2.Location = new Point(9, 7);
            label2.Name = "label2";
            label2.Size = new Size(197, 37);
            label2.TabIndex = 1;
            label2.Text = "Convert media:";
            // 
            // ConvertBtn
            // 
            ConvertBtn.Location = new Point(227, 39);
            ConvertBtn.Name = "ConvertBtn";
            ConvertBtn.Size = new Size(117, 36);
            ConvertBtn.TabIndex = 6;
            ConvertBtn.Text = "Convert";
            ConvertBtn.UseVisualStyleBackColor = true;
            ConvertBtn.Click += ConvertBtn_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 20F);
            label3.Location = new Point(9, 73);
            label3.Name = "label3";
            label3.Size = new Size(197, 37);
            label3.TabIndex = 7;
            label3.Text = "Reduce weight:";
            // 
            // ReduceCB
            // 
            ReduceCB.DropDownStyle = ComboBoxStyle.DropDownList;
            ReduceCB.FormattingEnabled = true;
            ReduceCB.Location = new Point(74, 128);
            ReduceCB.Name = "ReduceCB";
            ReduceCB.Size = new Size(121, 23);
            ReduceCB.TabIndex = 8;
            ReduceCB.SelectedIndexChanged += ReduceCB_SelectedIndexChanged;
            // 
            // ReduceBtn
            // 
            ReduceBtn.Location = new Point(227, 120);
            ReduceBtn.Name = "ReduceBtn";
            ReduceBtn.Size = new Size(117, 36);
            ReduceBtn.TabIndex = 9;
            ReduceBtn.Text = "Reduce";
            ReduceBtn.UseVisualStyleBackColor = true;
            ReduceBtn.Click += ReduceBtn_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F);
            label4.Location = new Point(17, 110);
            label4.Name = "label4";
            label4.Size = new Size(189, 15);
            label4.TabIndex = 10;
            label4.Text = "Does not change media extension.";
            // 
            // PanelVideos
            // 
            PanelVideos.Controls.Add(ExtractFrameBtn);
            PanelVideos.Controls.Add(CutBtn);
            PanelVideos.Controls.Add(StartTrack);
            PanelVideos.Controls.Add(EndTrack);
            PanelVideos.Controls.Add(LBFin);
            PanelVideos.Controls.Add(LBInicio);
            PanelVideos.Controls.Add(label6);
            PanelVideos.Controls.Add(AudioCB);
            PanelVideos.Controls.Add(ExtractBtn);
            PanelVideos.Controls.Add(label5);
            PanelVideos.Controls.Add(ReduceBtn);
            PanelVideos.Controls.Add(label4);
            PanelVideos.Controls.Add(FormatCB);
            PanelVideos.Controls.Add(label2);
            PanelVideos.Controls.Add(ReduceCB);
            PanelVideos.Controls.Add(ConvertBtn);
            PanelVideos.Controls.Add(label3);
            PanelVideos.Location = new Point(416, 12);
            PanelVideos.Name = "PanelVideos";
            PanelVideos.Size = new Size(372, 426);
            PanelVideos.TabIndex = 11;
            // 
            // ExtractFrameBtn
            // 
            ExtractFrameBtn.Location = new Point(229, 307);
            ExtractFrameBtn.Name = "ExtractFrameBtn";
            ExtractFrameBtn.Size = new Size(117, 36);
            ExtractFrameBtn.TabIndex = 21;
            ExtractFrameBtn.Text = "Extract Start Frame";
            ExtractFrameBtn.UseVisualStyleBackColor = true;
            ExtractFrameBtn.Click += ExtractFrameBtn_Click;
            // 
            // CutBtn
            // 
            CutBtn.Location = new Point(227, 372);
            CutBtn.Name = "CutBtn";
            CutBtn.Size = new Size(117, 36);
            CutBtn.TabIndex = 20;
            CutBtn.Text = "Cut Start-End";
            CutBtn.UseVisualStyleBackColor = true;
            CutBtn.Click += CutBtn_Click;
            // 
            // StartTrack
            // 
            StartTrack.Location = new Point(17, 307);
            StartTrack.Name = "StartTrack";
            StartTrack.Size = new Size(189, 45);
            StartTrack.TabIndex = 19;
            // 
            // EndTrack
            // 
            EndTrack.Location = new Point(15, 372);
            EndTrack.Name = "EndTrack";
            EndTrack.Size = new Size(189, 45);
            EndTrack.TabIndex = 18;
            // 
            // LBFin
            // 
            LBFin.AutoSize = true;
            LBFin.Font = new Font("Segoe UI", 9F);
            LBFin.Location = new Point(15, 354);
            LBFin.Name = "LBFin";
            LBFin.Size = new Size(75, 15);
            LBFin.TabIndex = 17;
            LBFin.Text = "End: 00:00:00";
            // 
            // LBInicio
            // 
            LBInicio.AutoSize = true;
            LBInicio.Font = new Font("Segoe UI", 9F);
            LBInicio.Location = new Point(11, 289);
            LBInicio.Name = "LBInicio";
            LBInicio.Size = new Size(79, 15);
            LBInicio.TabIndex = 15;
            LBInicio.Text = "Start: 00:00:00";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 20F);
            label6.Location = new Point(11, 252);
            label6.Name = "label6";
            label6.Size = new Size(141, 37);
            label6.TabIndex = 14;
            label6.Text = "Cut Video:";
            // 
            // AudioCB
            // 
            AudioCB.DropDownStyle = ComboBoxStyle.DropDownList;
            AudioCB.FormattingEnabled = true;
            AudioCB.Location = new Point(76, 213);
            AudioCB.Name = "AudioCB";
            AudioCB.Size = new Size(121, 23);
            AudioCB.TabIndex = 13;
            AudioCB.SelectedIndexChanged += AudioCB_SelectedIndexChanged;
            // 
            // ExtractBtn
            // 
            ExtractBtn.Location = new Point(229, 205);
            ExtractBtn.Name = "ExtractBtn";
            ExtractBtn.Size = new Size(117, 36);
            ExtractBtn.TabIndex = 12;
            ExtractBtn.Text = "Extract";
            ExtractBtn.UseVisualStyleBackColor = true;
            ExtractBtn.Click += ExtractBtn_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 20F);
            label5.Location = new Point(11, 164);
            label5.Name = "label5";
            label5.Size = new Size(180, 37);
            label5.TabIndex = 11;
            label5.Text = "Extract Audio:";
            // 
            // PanelImages
            // 
            PanelImages.Controls.Add(CreatePNGBtn);
            PanelImages.Controls.Add(LBSoftness);
            PanelImages.Controls.Add(SoftnessTrack);
            PanelImages.Controls.Add(LBTolerance);
            PanelImages.Controls.Add(ToleranceTrack);
            PanelImages.Controls.Add(ColorPreviewPanel);
            PanelImages.Controls.Add(PickColorBtn);
            PanelImages.Controls.Add(label9);
            PanelImages.Controls.Add(ExtractChannelsCB);
            PanelImages.Controls.Add(label8);
            PanelImages.Controls.Add(ExtractChannelsBtn);
            PanelImages.Controls.Add(FormatImageCB);
            PanelImages.Controls.Add(label7);
            PanelImages.Controls.Add(ConvertImageBtn);
            PanelImages.Location = new Point(416, 8);
            PanelImages.Name = "PanelImages";
            PanelImages.Size = new Size(372, 430);
            PanelImages.TabIndex = 12;
            // 
            // CreatePNGBtn
            // 
            CreatePNGBtn.Location = new Point(229, 278);
            CreatePNGBtn.Name = "CreatePNGBtn";
            CreatePNGBtn.Size = new Size(117, 36);
            CreatePNGBtn.TabIndex = 33;
            CreatePNGBtn.Text = "Create PNG";
            CreatePNGBtn.UseVisualStyleBackColor = true;
            CreatePNGBtn.Click += CreatePNGBtn_Click;
            // 
            // LBSoftness
            // 
            LBSoftness.AutoSize = true;
            LBSoftness.Location = new Point(17, 289);
            LBSoftness.Name = "LBSoftness";
            LBSoftness.Size = new Size(51, 15);
            LBSoftness.TabIndex = 31;
            LBSoftness.Text = "Softness";
            // 
            // SoftnessTrack
            // 
            SoftnessTrack.LargeChange = 10;
            SoftnessTrack.Location = new Point(17, 307);
            SoftnessTrack.Maximum = 100;
            SoftnessTrack.Name = "SoftnessTrack";
            SoftnessTrack.Size = new Size(183, 45);
            SoftnessTrack.TabIndex = 32;
            SoftnessTrack.TickFrequency = 5;
            SoftnessTrack.Value = 7;
            // 
            // LBTolerance
            // 
            LBTolerance.AutoSize = true;
            LBTolerance.Location = new Point(17, 241);
            LBTolerance.Name = "LBTolerance";
            LBTolerance.Size = new Size(60, 15);
            LBTolerance.TabIndex = 12;
            LBTolerance.Text = "Tolerance:";
            // 
            // ToleranceTrack
            // 
            ToleranceTrack.LargeChange = 10;
            ToleranceTrack.Location = new Point(17, 259);
            ToleranceTrack.Maximum = 100;
            ToleranceTrack.Name = "ToleranceTrack";
            ToleranceTrack.Size = new Size(183, 45);
            ToleranceTrack.TabIndex = 30;
            ToleranceTrack.TickFrequency = 5;
            ToleranceTrack.Value = 15;
            // 
            // ColorPreviewPanel
            // 
            ColorPreviewPanel.BackColor = Color.Red;
            ColorPreviewPanel.Location = new Point(158, 208);
            ColorPreviewPanel.Name = "ColorPreviewPanel";
            ColorPreviewPanel.Size = new Size(30, 30);
            ColorPreviewPanel.TabIndex = 29;
            // 
            // PickColorBtn
            // 
            PickColorBtn.Location = new Point(35, 213);
            PickColorBtn.Name = "PickColorBtn";
            PickColorBtn.Size = new Size(117, 23);
            PickColorBtn.TabIndex = 28;
            PickColorBtn.Text = "Pick Color";
            PickColorBtn.UseVisualStyleBackColor = true;
            PickColorBtn.Click += PickColorBtn_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 20F);
            label9.Location = new Point(3, 168);
            label9.Name = "label9";
            label9.Size = new Size(161, 37);
            label9.TabIndex = 27;
            label9.Text = "Create PNG:";
            // 
            // ExtractChannelsCB
            // 
            ExtractChannelsCB.DropDownStyle = ComboBoxStyle.DropDownList;
            ExtractChannelsCB.FormattingEnabled = true;
            ExtractChannelsCB.Location = new Point(70, 131);
            ExtractChannelsCB.Name = "ExtractChannelsCB";
            ExtractChannelsCB.Size = new Size(121, 23);
            ExtractChannelsCB.TabIndex = 24;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 20F);
            label8.Location = new Point(3, 87);
            label8.Name = "label8";
            label8.Size = new Size(212, 37);
            label8.TabIndex = 25;
            label8.Text = "Extract channels:";
            // 
            // ExtractChannelsBtn
            // 
            ExtractChannelsBtn.Location = new Point(223, 123);
            ExtractChannelsBtn.Name = "ExtractChannelsBtn";
            ExtractChannelsBtn.Size = new Size(117, 36);
            ExtractChannelsBtn.TabIndex = 26;
            ExtractChannelsBtn.Text = "Extract";
            ExtractChannelsBtn.UseVisualStyleBackColor = true;
            ExtractChannelsBtn.Click += ExtractChannelsBtn_Click;
            // 
            // FormatImageCB
            // 
            FormatImageCB.DropDownStyle = ComboBoxStyle.DropDownList;
            FormatImageCB.FormattingEnabled = true;
            FormatImageCB.Location = new Point(70, 54);
            FormatImageCB.Name = "FormatImageCB";
            FormatImageCB.Size = new Size(121, 23);
            FormatImageCB.TabIndex = 22;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 20F);
            label7.Location = new Point(3, 10);
            label7.Name = "label7";
            label7.Size = new Size(197, 37);
            label7.TabIndex = 22;
            label7.Text = "Convert media:";
            // 
            // ConvertImageBtn
            // 
            ConvertImageBtn.Location = new Point(223, 46);
            ConvertImageBtn.Name = "ConvertImageBtn";
            ConvertImageBtn.Size = new Size(117, 36);
            ConvertImageBtn.TabIndex = 23;
            ConvertImageBtn.Text = "Convert";
            ConvertImageBtn.UseVisualStyleBackColor = true;
            ConvertImageBtn.Click += ConvertImageBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(PanelImages);
            Controls.Add(PanelVideos);
            Controls.Add(OutputLabel);
            Controls.Add(OutputTB);
            Controls.Add(InputLabel);
            Controls.Add(InputTB);
            Controls.Add(PanelDragDrop);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Media Converter";
            PanelDragDrop.ResumeLayout(false);
            PanelDragDrop.PerformLayout();
            PanelVideos.ResumeLayout(false);
            PanelVideos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)StartTrack).EndInit();
            ((System.ComponentModel.ISupportInitialize)EndTrack).EndInit();
            PanelImages.ResumeLayout(false);
            PanelImages.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SoftnessTrack).EndInit();
            ((System.ComponentModel.ISupportInitialize)ToleranceTrack).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel PanelDragDrop;
        private Label label1;
        private TextBox InputTB;
        private Label InputLabel;
        private Label OutputLabel;
        private TextBox OutputTB;
        private ComboBox FormatCB;
        private Label label2;
        private Button ConvertBtn;
        private Label label3;
        private ComboBox ReduceCB;
        private Button ReduceBtn;
        private Label label4;
        private Panel PanelVideos;
        private Button ExtractBtn;
        private Label label5;
        private ComboBox AudioCB;
        private Label label6;
        private Button CutBtn;
        private TrackBar StartTrack;
        private TrackBar EndTrack;
        private Label LBFin;
        private Label LBInicio;
        private Button ExtractFrameBtn;
        private Panel PanelImages;
        private Label label7;
        private ComboBox FormatImageCB;
        private Button ConvertImageBtn;
        private ComboBox ExtractChannelsCB;
        private Label label8;
        private Button ExtractChannelsBtn;
        private Label label9;
        private Panel ColorPreviewPanel;
        private Button PickColorBtn;
        private Label LBSoftness;
        private TrackBar SoftnessTrack;
        private Label LBTolerance;
        private TrackBar ToleranceTrack;
        private Button CreatePNGBtn;
    }
}
