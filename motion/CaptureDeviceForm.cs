using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace motion
{
	/// <summary>
	/// Summary description for CaptureDeviceForm.
	/// </summary>
	public class CaptureDeviceForm : System.Windows.Forms.Form
	{
		FilterInfoCollection filters;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox deviceCombo;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private string device;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		// Device
		public string Device
		{
			get { return device; }
		}

		// Constructor
		public CaptureDeviceForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			try
			{
				filters = new FilterInfoCollection(FilterCategory.VideoInputDevice);

				if (filters.Count == 0)
					throw new ApplicationException();

				// add all devices to combo
				foreach (FilterInfo filter in filters)
				{
					deviceCombo.Items.Add(filter.Name);
				}
			}
			catch (ApplicationException)
			{
				deviceCombo.Items.Add("No local capture devices");
				deviceCombo.Enabled = false;
				okButton.Enabled = false;
			}

			deviceCombo.SelectedIndex = 0;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.deviceCombo = new System.Windows.Forms.ComboBox();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(156, 14);
			this.label1.TabIndex = 0;
			this.label1.Text = "Select capture device:";
			// 
			// deviceCombo
			// 
			this.deviceCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.deviceCombo.Location = new System.Drawing.Point(10, 30);
			this.deviceCombo.Name = "deviceCombo";
			this.deviceCombo.Size = new System.Drawing.Size(325, 21);
			this.deviceCombo.TabIndex = 6;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cancelButton.Location = new System.Drawing.Point(180, 78);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.TabIndex = 9;
			this.cancelButton.Text = "Cancel";
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.okButton.Location = new System.Drawing.Point(90, 78);
			this.okButton.Name = "okButton";
			this.okButton.TabIndex = 8;
			this.okButton.Text = "Ok";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// CaptureDeviceForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(344, 113);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.cancelButton,
																		  this.okButton,
																		  this.deviceCombo,
																		  this.label1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CaptureDeviceForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Open Local Capture Device";
			this.ResumeLayout(false);

		}
		#endregion

		// On "Ok" button
		private void okButton_Click(object sender, System.EventArgs e)
		{
			device = filters[deviceCombo.SelectedIndex].MonikerString;
		}
	}
}
