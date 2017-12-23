
namespace SharpVisaUI
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button buttonRefresh;
		private System.Windows.Forms.ListBox listBoxDevices;
		private System.Windows.Forms.TextBox textBoxCommand0;
		private System.Windows.Forms.Button buttonSend0;
		private System.Windows.Forms.RichTextBox richTextBoxLog;
		private System.Windows.Forms.Button buttonClear;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.listBoxDevices = new System.Windows.Forms.ListBox();
            this.textBoxCommand0 = new System.Windows.Forms.TextBox();
            this.buttonSend0 = new System.Windows.Forms.Button();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(14, 12);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(360, 23);
            this.buttonRefresh.TabIndex = 0;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.ButtonRefreshClick);
            // 
            // listBoxDevices
            // 
            this.listBoxDevices.FormattingEnabled = true;
            this.listBoxDevices.Location = new System.Drawing.Point(14, 48);
            this.listBoxDevices.Name = "listBoxDevices";
            this.listBoxDevices.Size = new System.Drawing.Size(360, 342);
            this.listBoxDevices.TabIndex = 1;
            this.listBoxDevices.SelectedIndexChanged += new System.EventHandler(this.ListBoxDevicesSelectedIndexChanged);
            // 
            // textBoxCommand0
            // 
            this.textBoxCommand0.Location = new System.Drawing.Point(397, 14);
            this.textBoxCommand0.Name = "textBoxCommand0";
            this.textBoxCommand0.Size = new System.Drawing.Size(475, 20);
            this.textBoxCommand0.TabIndex = 2;
            this.textBoxCommand0.Text = "*IDN?";
            // 
            // buttonSend0
            // 
            this.buttonSend0.Location = new System.Drawing.Point(878, 12);
            this.buttonSend0.Name = "buttonSend0";
            this.buttonSend0.Size = new System.Drawing.Size(116, 23);
            this.buttonSend0.TabIndex = 3;
            this.buttonSend0.Text = "Send";
            this.buttonSend0.UseVisualStyleBackColor = true;
            this.buttonSend0.Click += new System.EventHandler(this.ButtonSend0Click);
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.BackColor = System.Drawing.Color.Black;
            this.richTextBoxLog.Location = new System.Drawing.Point(397, 48);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.Size = new System.Drawing.Size(597, 313);
            this.richTextBoxLog.TabIndex = 4;
            this.richTextBoxLog.Text = "";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(397, 367);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(597, 23);
            this.buttonClear.TabIndex = 5;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.ButtonClearClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1075, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "DrawData";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 518);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.buttonSend0);
            this.Controls.Add(this.textBoxCommand0);
            this.Controls.Add(this.listBoxDevices);
            this.Controls.Add(this.buttonRefresh);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SharpVisaUI - https://github.com/samuelventura/SharpVisa";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private System.Windows.Forms.Button button1;
	}
}
