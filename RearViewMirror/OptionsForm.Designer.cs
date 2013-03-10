namespace RearViewMirror
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.tbOpacity = new System.Windows.Forms.TrackBar();
            this.cbGlobalOptions = new System.Windows.Forms.CheckBox();
            this.lOpacity = new System.Windows.Forms.Label();
            this.cbAlertSound = new System.Windows.Forms.CheckBox();
            this.tbAudioFile = new System.Windows.Forms.TextBox();
            this.bBrowseAudioFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.lCameraName = new System.Windows.Forms.Label();
            this.cbRecord = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bBrowseRecordFolder = new System.Windows.Forms.Button();
            this.tbRecordFolder = new System.Windows.Forms.TextBox();
            this.cbCodec = new System.Windows.Forms.ComboBox();
            this.lVideoCompression = new System.Windows.Forms.Label();
            this.cbAlwaysShow = new System.Windows.Forms.CheckBox();
            this.cbDetectorType = new System.Windows.Forms.ComboBox();
            this.cbEnableMotionAlarm = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity)).BeginInit();
            this.SuspendLayout();
            // 
            // tbOpacity
            // 
            this.tbOpacity.Location = new System.Drawing.Point(125, 52);
            this.tbOpacity.Name = "tbOpacity";
            this.tbOpacity.Size = new System.Drawing.Size(155, 45);
            this.tbOpacity.TabIndex = 0;
            this.tbOpacity.Scroll += new System.EventHandler(this.tbOpacity_Scroll);
            // 
            // cbGlobalOptions
            // 
            this.cbGlobalOptions.AutoSize = true;
            this.cbGlobalOptions.Location = new System.Drawing.Point(12, 13);
            this.cbGlobalOptions.Name = "cbGlobalOptions";
            this.cbGlobalOptions.Size = new System.Drawing.Size(117, 17);
            this.cbGlobalOptions.TabIndex = 1;
            this.cbGlobalOptions.Text = "Use Global Options";
            this.cbGlobalOptions.UseVisualStyleBackColor = true;
            this.cbGlobalOptions.CheckedChanged += new System.EventHandler(this.cbGlobalOptions_CheckedChanged);
            // 
            // lOpacity
            // 
            this.lOpacity.AutoSize = true;
            this.lOpacity.Location = new System.Drawing.Point(12, 61);
            this.lOpacity.Name = "lOpacity";
            this.lOpacity.Size = new System.Drawing.Size(107, 13);
            this.lOpacity.TabIndex = 2;
            this.lOpacity.Text = "Viewer Transparency";
            // 
            // cbAlertSound
            // 
            this.cbAlertSound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAlertSound.AutoSize = true;
            this.cbAlertSound.Location = new System.Drawing.Point(12, 199);
            this.cbAlertSound.Name = "cbAlertSound";
            this.cbAlertSound.Size = new System.Drawing.Size(117, 17);
            this.cbAlertSound.TabIndex = 3;
            this.cbAlertSound.Text = "Enable Alert Sound";
            this.cbAlertSound.UseVisualStyleBackColor = true;
            this.cbAlertSound.CheckedChanged += new System.EventHandler(this.cbAlertSound_CheckedChanged);
            // 
            // tbAudioFile
            // 
            this.tbAudioFile.Location = new System.Drawing.Point(83, 225);
            this.tbAudioFile.Name = "tbAudioFile";
            this.tbAudioFile.Size = new System.Drawing.Size(152, 20);
            this.tbAudioFile.TabIndex = 5;
            // 
            // bBrowseAudioFile
            // 
            this.bBrowseAudioFile.Location = new System.Drawing.Point(241, 222);
            this.bBrowseAudioFile.Name = "bBrowseAudioFile";
            this.bBrowseAudioFile.Size = new System.Drawing.Size(75, 23);
            this.bBrowseAudioFile.TabIndex = 6;
            this.bBrowseAudioFile.Text = "Browse";
            this.bBrowseAudioFile.UseVisualStyleBackColor = true;
            this.bBrowseAudioFile.Click += new System.EventHandler(this.bBrowseAudioFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 228);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Sound File";
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bOK.Location = new System.Drawing.Point(61, 345);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(91, 27);
            this.bOK.TabIndex = 8;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.Location = new System.Drawing.Point(189, 345);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(91, 27);
            this.bCancel.TabIndex = 9;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // lCameraName
            // 
            this.lCameraName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lCameraName.AutoSize = true;
            this.lCameraName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lCameraName.Location = new System.Drawing.Point(195, 13);
            this.lCameraName.Name = "lCameraName";
            this.lCameraName.Size = new System.Drawing.Size(122, 20);
            this.lCameraName.TabIndex = 10;
            this.lCameraName.Text = "Camera Name";
            this.lCameraName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbRecord
            // 
            this.cbRecord.AutoSize = true;
            this.cbRecord.Location = new System.Drawing.Point(12, 255);
            this.cbRecord.Name = "cbRecord";
            this.cbRecord.Size = new System.Drawing.Size(190, 17);
            this.cbRecord.TabIndex = 11;
            this.cbRecord.Text = "Record Video on Motion Detection";
            this.cbRecord.UseVisualStyleBackColor = true;
            this.cbRecord.CheckedChanged += new System.EventHandler(this.cbRecord_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 279);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Save Folder";
            // 
            // bBrowseRecordFolder
            // 
            this.bBrowseRecordFolder.Location = new System.Drawing.Point(241, 274);
            this.bBrowseRecordFolder.Name = "bBrowseRecordFolder";
            this.bBrowseRecordFolder.Size = new System.Drawing.Size(75, 23);
            this.bBrowseRecordFolder.TabIndex = 13;
            this.bBrowseRecordFolder.Text = "Browse";
            this.bBrowseRecordFolder.UseVisualStyleBackColor = true;
            this.bBrowseRecordFolder.Click += new System.EventHandler(this.bBrowseRecordFolder_Click);
            // 
            // tbRecordFolder
            // 
            this.tbRecordFolder.Location = new System.Drawing.Point(83, 276);
            this.tbRecordFolder.Name = "tbRecordFolder";
            this.tbRecordFolder.Size = new System.Drawing.Size(152, 20);
            this.tbRecordFolder.TabIndex = 14;
            // 
            // cbCodec
            // 
            this.cbCodec.FormattingEnabled = true;
            this.cbCodec.Location = new System.Drawing.Point(83, 302);
            this.cbCodec.Name = "cbCodec";
            this.cbCodec.Size = new System.Drawing.Size(152, 21);
            this.cbCodec.TabIndex = 15;
            // 
            // lVideoCompression
            // 
            this.lVideoCompression.AutoSize = true;
            this.lVideoCompression.Location = new System.Drawing.Point(12, 306);
            this.lVideoCompression.Name = "lVideoCompression";
            this.lVideoCompression.Size = new System.Drawing.Size(68, 13);
            this.lVideoCompression.TabIndex = 16;
            this.lVideoCompression.Text = "Video Codec";
            // 
            // cbAlwaysShow
            // 
            this.cbAlwaysShow.AutoSize = true;
            this.cbAlwaysShow.Location = new System.Drawing.Point(12, 103);
            this.cbAlwaysShow.Name = "cbAlwaysShow";
            this.cbAlwaysShow.Size = new System.Drawing.Size(124, 17);
            this.cbAlwaysShow.TabIndex = 17;
            this.cbAlwaysShow.Text = "Always Show Viewer";
            this.cbAlwaysShow.UseVisualStyleBackColor = true;
            // 
            // cbDetectorType
            // 
            this.cbDetectorType.FormattingEnabled = true;
            this.cbDetectorType.Location = new System.Drawing.Point(128, 161);
            this.cbDetectorType.Name = "cbDetectorType";
            this.cbDetectorType.Size = new System.Drawing.Size(152, 21);
            this.cbDetectorType.TabIndex = 18;
            // 
            // cbEnableMotionAlarm
            // 
            this.cbEnableMotionAlarm.AutoSize = true;
            this.cbEnableMotionAlarm.Location = new System.Drawing.Point(12, 138);
            this.cbEnableMotionAlarm.Name = "cbEnableMotionAlarm";
            this.cbEnableMotionAlarm.Size = new System.Drawing.Size(118, 17);
            this.cbEnableMotionAlarm.TabIndex = 19;
            this.cbEnableMotionAlarm.Text = "Enable Motion Alert";
            this.cbEnableMotionAlarm.UseVisualStyleBackColor = true;
            this.cbEnableMotionAlarm.CheckedChanged += new System.EventHandler(this.cbEnableMotionAlarm_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Motion Detector Type";
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 384);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbEnableMotionAlarm);
            this.Controls.Add(this.cbDetectorType);
            this.Controls.Add(this.cbAlwaysShow);
            this.Controls.Add(this.lVideoCompression);
            this.Controls.Add(this.cbCodec);
            this.Controls.Add(this.tbRecordFolder);
            this.Controls.Add(this.bBrowseRecordFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbRecord);
            this.Controls.Add(this.lCameraName);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bBrowseAudioFile);
            this.Controls.Add(this.tbAudioFile);
            this.Controls.Add(this.cbAlertSound);
            this.Controls.Add(this.lOpacity);
            this.Controls.Add(this.cbGlobalOptions);
            this.Controls.Add(this.tbOpacity);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionsForm";
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar tbOpacity;
        private System.Windows.Forms.CheckBox cbGlobalOptions;
        private System.Windows.Forms.Label lOpacity;
        private System.Windows.Forms.CheckBox cbAlertSound;
        private System.Windows.Forms.TextBox tbAudioFile;
        private System.Windows.Forms.Button bBrowseAudioFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Label lCameraName;
        private System.Windows.Forms.CheckBox cbRecord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bBrowseRecordFolder;
        private System.Windows.Forms.TextBox tbRecordFolder;
        private System.Windows.Forms.ComboBox cbCodec;
        private System.Windows.Forms.Label lVideoCompression;
        private System.Windows.Forms.CheckBox cbAlwaysShow;
        private System.Windows.Forms.ComboBox cbDetectorType;
        private System.Windows.Forms.CheckBox cbEnableMotionAlarm;
        private System.Windows.Forms.Label label3;
    }
}