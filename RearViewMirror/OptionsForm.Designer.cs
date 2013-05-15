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
            resources.ApplyResources(this.tbOpacity, "tbOpacity");
            this.tbOpacity.Name = "tbOpacity";
            this.tbOpacity.Scroll += new System.EventHandler(this.tbOpacity_Scroll);
            // 
            // cbGlobalOptions
            // 
            resources.ApplyResources(this.cbGlobalOptions, "cbGlobalOptions");
            this.cbGlobalOptions.Name = "cbGlobalOptions";
            this.cbGlobalOptions.UseVisualStyleBackColor = true;
            this.cbGlobalOptions.CheckedChanged += new System.EventHandler(this.cbGlobalOptions_CheckedChanged);
            // 
            // lOpacity
            // 
            resources.ApplyResources(this.lOpacity, "lOpacity");
            this.lOpacity.Name = "lOpacity";
            // 
            // cbAlertSound
            // 
            resources.ApplyResources(this.cbAlertSound, "cbAlertSound");
            this.cbAlertSound.Name = "cbAlertSound";
            this.cbAlertSound.UseVisualStyleBackColor = true;
            this.cbAlertSound.CheckedChanged += new System.EventHandler(this.cbAlertSound_CheckedChanged);
            // 
            // tbAudioFile
            // 
            resources.ApplyResources(this.tbAudioFile, "tbAudioFile");
            this.tbAudioFile.Name = "tbAudioFile";
            // 
            // bBrowseAudioFile
            // 
            resources.ApplyResources(this.bBrowseAudioFile, "bBrowseAudioFile");
            this.bBrowseAudioFile.Name = "bBrowseAudioFile";
            this.bBrowseAudioFile.UseVisualStyleBackColor = true;
            this.bBrowseAudioFile.Click += new System.EventHandler(this.bBrowseAudioFile_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // bOK
            // 
            resources.ApplyResources(this.bOK, "bOK");
            this.bOK.Name = "bOK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            resources.ApplyResources(this.bCancel, "bCancel");
            this.bCancel.Name = "bCancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // lCameraName
            // 
            resources.ApplyResources(this.lCameraName, "lCameraName");
            this.lCameraName.Name = "lCameraName";
            // 
            // cbRecord
            // 
            resources.ApplyResources(this.cbRecord, "cbRecord");
            this.cbRecord.Name = "cbRecord";
            this.cbRecord.UseVisualStyleBackColor = true;
            this.cbRecord.CheckedChanged += new System.EventHandler(this.cbRecord_CheckedChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // bBrowseRecordFolder
            // 
            resources.ApplyResources(this.bBrowseRecordFolder, "bBrowseRecordFolder");
            this.bBrowseRecordFolder.Name = "bBrowseRecordFolder";
            this.bBrowseRecordFolder.UseVisualStyleBackColor = true;
            this.bBrowseRecordFolder.Click += new System.EventHandler(this.bBrowseRecordFolder_Click);
            // 
            // tbRecordFolder
            // 
            resources.ApplyResources(this.tbRecordFolder, "tbRecordFolder");
            this.tbRecordFolder.Name = "tbRecordFolder";
            // 
            // cbCodec
            // 
            resources.ApplyResources(this.cbCodec, "cbCodec");
            this.cbCodec.FormattingEnabled = true;
            this.cbCodec.Name = "cbCodec";
            // 
            // lVideoCompression
            // 
            resources.ApplyResources(this.lVideoCompression, "lVideoCompression");
            this.lVideoCompression.Name = "lVideoCompression";
            // 
            // cbAlwaysShow
            // 
            resources.ApplyResources(this.cbAlwaysShow, "cbAlwaysShow");
            this.cbAlwaysShow.Name = "cbAlwaysShow";
            this.cbAlwaysShow.UseVisualStyleBackColor = true;
            // 
            // cbDetectorType
            // 
            resources.ApplyResources(this.cbDetectorType, "cbDetectorType");
            this.cbDetectorType.FormattingEnabled = true;
            this.cbDetectorType.Name = "cbDetectorType";
            // 
            // cbEnableMotionAlarm
            // 
            resources.ApplyResources(this.cbEnableMotionAlarm, "cbEnableMotionAlarm");
            this.cbEnableMotionAlarm.Name = "cbEnableMotionAlarm";
            this.cbEnableMotionAlarm.UseVisualStyleBackColor = true;
            this.cbEnableMotionAlarm.CheckedChanged += new System.EventHandler(this.cbEnableMotionAlarm_CheckedChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // OptionsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.Name = "OptionsForm";
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