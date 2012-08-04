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
            this.cbEnableOnStartup = new System.Windows.Forms.CheckBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity)).BeginInit();
            this.SuspendLayout();
            // 
            // tbOpacity
            // 
            this.tbOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOpacity.Location = new System.Drawing.Point(61, 52);
            this.tbOpacity.Name = "tbOpacity";
            this.tbOpacity.Size = new System.Drawing.Size(219, 45);
            this.tbOpacity.TabIndex = 0;
            this.tbOpacity.Scroll += new System.EventHandler(this.tbOpacity_Scroll);
            // 
            // cbGlobalOptions
            // 
            this.cbGlobalOptions.AutoSize = true;
            this.cbGlobalOptions.Location = new System.Drawing.Point(13, 13);
            this.cbGlobalOptions.Name = "cbGlobalOptions";
            this.cbGlobalOptions.Size = new System.Drawing.Size(117, 17);
            this.cbGlobalOptions.TabIndex = 1;
            this.cbGlobalOptions.Text = "Use Global Options";
            this.cbGlobalOptions.UseVisualStyleBackColor = true;
            // 
            // lOpacity
            // 
            this.lOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lOpacity.AutoSize = true;
            this.lOpacity.Location = new System.Drawing.Point(12, 63);
            this.lOpacity.Name = "lOpacity";
            this.lOpacity.Size = new System.Drawing.Size(43, 13);
            this.lOpacity.TabIndex = 2;
            this.lOpacity.Text = "Opacity";
            // 
            // cbAlertSound
            // 
            this.cbAlertSound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAlertSound.AutoSize = true;
            this.cbAlertSound.Location = new System.Drawing.Point(15, 118);
            this.cbAlertSound.Name = "cbAlertSound";
            this.cbAlertSound.Size = new System.Drawing.Size(117, 17);
            this.cbAlertSound.TabIndex = 3;
            this.cbAlertSound.Text = "Enable Alert Sound";
            this.cbAlertSound.UseVisualStyleBackColor = true;
            // 
            // cbEnableOnStartup
            // 
            this.cbEnableOnStartup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.cbEnableOnStartup.AutoSize = true;
            this.cbEnableOnStartup.Location = new System.Drawing.Point(15, 95);
            this.cbEnableOnStartup.Name = "cbEnableOnStartup";
            this.cbEnableOnStartup.Size = new System.Drawing.Size(150, 17);
            this.cbEnableOnStartup.TabIndex = 4;
            this.cbEnableOnStartup.Text = "Enable Camera on Startup";
            this.cbEnableOnStartup.UseVisualStyleBackColor = true;
            // 
            // tbAudioFile
            // 
            this.tbAudioFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAudioFile.Location = new System.Drawing.Point(86, 144);
            this.tbAudioFile.Name = "tbAudioFile";
            this.tbAudioFile.Size = new System.Drawing.Size(152, 20);
            this.tbAudioFile.TabIndex = 5;
            // 
            // bBrowseAudioFile
            // 
            this.bBrowseAudioFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bBrowseAudioFile.Location = new System.Drawing.Point(244, 141);
            this.bBrowseAudioFile.Name = "bBrowseAudioFile";
            this.bBrowseAudioFile.Size = new System.Drawing.Size(75, 23);
            this.bBrowseAudioFile.TabIndex = 6;
            this.bBrowseAudioFile.Text = "Browse";
            this.bBrowseAudioFile.UseVisualStyleBackColor = true;
            this.bBrowseAudioFile.Click += new System.EventHandler(this.bBrowseAudioFile_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Sound File";
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bOK.Location = new System.Drawing.Point(61, 228);
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
            this.bCancel.Location = new System.Drawing.Point(189, 228);
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
            this.cbRecord.Location = new System.Drawing.Point(15, 174);
            this.cbRecord.Name = "cbRecord";
            this.cbRecord.Size = new System.Drawing.Size(190, 17);
            this.cbRecord.TabIndex = 11;
            this.cbRecord.Text = "Record Video on Motion Detection";
            this.cbRecord.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Save Folder";
            // 
            // bBrowseRecordFolder
            // 
            this.bBrowseRecordFolder.Location = new System.Drawing.Point(244, 193);
            this.bBrowseRecordFolder.Name = "bBrowseRecordFolder";
            this.bBrowseRecordFolder.Size = new System.Drawing.Size(75, 23);
            this.bBrowseRecordFolder.TabIndex = 13;
            this.bBrowseRecordFolder.Text = "Browse";
            this.bBrowseRecordFolder.UseVisualStyleBackColor = true;
            this.bBrowseRecordFolder.Click += new System.EventHandler(this.bBrowseRecordFolder_Click);
            // 
            // tbRecordFolder
            // 
            this.tbRecordFolder.Location = new System.Drawing.Point(86, 195);
            this.tbRecordFolder.Name = "tbRecordFolder";
            this.tbRecordFolder.Size = new System.Drawing.Size(152, 20);
            this.tbRecordFolder.TabIndex = 14;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 261);
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
            this.Controls.Add(this.cbEnableOnStartup);
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
        private System.Windows.Forms.CheckBox cbEnableOnStartup;
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
    }
}