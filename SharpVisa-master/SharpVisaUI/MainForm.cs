using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
//using System.Web.UI.DataVisualization.Charting;

//using System.Windows.Forms.DataVisualization.Charting;
namespace SharpVisaUI
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}
		
		private void RefreshDevices()
		{
			listBoxDevices.Items.Clear();
			SharpVisaCLI.Program.List((inst)=>{ listBoxDevices.Items.Add(inst); });
			buttonSend0.Enabled = listBoxDevices.SelectedIndex >= 0;
		}
		
		private void Log(string prefix, Color color, string text) {
			var len = richTextBoxLog.TextLength;
			if (len > 0) {
				richTextBoxLog.SelectionStart = len - 1;
				richTextBoxLog.SelectionLength = 1;
				if (richTextBoxLog.SelectedText != "\n") {
					richTextBoxLog.AppendText("\n");
				}
			}		
			richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
			richTextBoxLog.SelectionColor = color;
			richTextBoxLog.AppendText(string.Format("{0} {1} {2}", DateTime.Now.ToString("HH:mm:ss.fff"), prefix, text));
			richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
			richTextBoxLog.ScrollToCaret();
		}
		
		private void Send(string inst, string req) {
			Log(">", Color.White, req);
			try {
				SharpVisaCLI.Program.Send(inst, req, (res)=>{
					Log("<", Color.Lime, res);
				});
			} catch(Exception ex) {
				Log("<", Color.Tomato, ex.ToString());
			}
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			RefreshDevices();
		}
		
		void ButtonRefreshClick(object sender, EventArgs e)
		{
			RefreshDevices();
		}
		
		void ButtonSend0Click(object sender, EventArgs e)
		{
			var req = textBoxCommand0.Text;
			var inst = (string)listBoxDevices.SelectedItem;
			Send(inst, req);
		}

		void ListBoxDevicesSelectedIndexChanged(object sender, EventArgs e)
		{
			buttonSend0.Enabled = listBoxDevices.SelectedIndex >= 0;
		}
		
		void ButtonClearClick(object sender, EventArgs e)
		{
			richTextBoxLog.Clear();
		}

        private void button1_Click(object sender, EventArgs e)
        {
            var inst = (string)listBoxDevices.SelectedItem;
            var req = ":waveform:data?";
            SharpVisaCLI.Program.Send(inst, req, (res) =>
            {
                DrawData(res);
            });
        }

        private void DrawData(string res)
        {
            
        }
	}
}
