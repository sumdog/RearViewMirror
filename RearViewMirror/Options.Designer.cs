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
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity)).BeginInit();
            this.SuspendLayout();
            // 
            // tbOpacity
            // 
            this.tbOpacity.Location = new System.Drawing.Point(61, 47);
            this.tbOpacity.Name = "tbOpacity";
            this.tbOpacity.Size = new System.Drawing.Size(219, 45);
            this.tbOpacity.TabIndex = 0;
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
            this.lOpacity.AutoSize = true;
            this.lOpacity.Location = new System.Drawing.Point(12, 57);
            this.lOpacity.Name = "lOpacity";
            this.lOpacity.Size = new System.Drawing.Size(43, 13);
            this.lOpacity.TabIndex = 2;
            this.lOpacity.Text = "Opacity";
            // 
            // cbAlertSound
            // 
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
            this.tbAudioFile.Location = new System.Drawing.Point(76, 141);
            this.tbAudioFile.Name = "tbAudioFile";
            this.tbAudioFile.Size = new System.Drawing.Size(160, 20);
            this.tbAudioFile.TabIndex = 5;
            // 
            // bBrowseAudioFile
            // 
            this.bBrowseAudioFile.Location = new System.Drawing.Point(242, 138);
            this.bBrowseAudioFile.Name = "bBrowseAudioFile";
            this.bBrowseAudioFile.Size = new System.Drawing.Size(75, 23);
            this.bBrowseAudioFile.TabIndex = 6;
            this.bBrowseAudioFile.Text = "Browse";
            this.bBrowseAudioFile.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Sound File";
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(61, 175);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(91, 27);
            this.bOK.TabIndex = 8;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(189, 174);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(91, 28);
            this.bCancel.TabIndex = 9;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 214);
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
    }
}