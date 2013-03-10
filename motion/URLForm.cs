using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace motion
{
	/// <summary>
	/// Summary description for URLForm.
	/// </summary>
	public class URLForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox urlCombo;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;

		private string url;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		// URL property
		public string URL
		{
			get { return url; }
		}

		// Description property
		public string Description
		{
			set
			{
				label1.Text = value;
			}
		}

		// URLs property
		public string[] URLs
		{
			set
			{
				urlCombo.Items.AddRange(value);
			}
		}


		// Constructor
		public URLForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.urlCombo = new System.Windows.Forms.ComboBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(332, 19);
			this.label1.TabIndex = 0;
			// 
			// urlCombo
			// 
			this.urlCombo.Location = new System.Drawing.Point(10, 30);
			this.urlCombo.Name = "urlCombo";
			this.urlCombo.Size = new System.Drawing.Size(325, 21);
			this.urlCombo.TabIndex = 1;
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.okButton.Location = new System.Drawing.Point(90, 78);
			this.okButton.Name = "okButton";
			this.okButton.TabIndex = 2;
			this.okButton.Text = "Ok";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cancelButton.Location = new System.Drawing.Point(180, 78);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "Cancel";
			// 
			// URLForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(344, 113);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.cancelButton,
																		  this.okButton,
																		  this.urlCombo,
																		  this.label1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "URLForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Open URL";
			this.ResumeLayout(false);

		}
		#endregion

		// On "Ok" button
		private void okButton_Click(object sender, System.EventArgs e)
		{
			url = urlCombo.Text;
		}
	}
}
