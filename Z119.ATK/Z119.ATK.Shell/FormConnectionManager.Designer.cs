namespace Z119.ATK.Shell
{
    partial class FormConnectionManager
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.comboBox_LoadControl = new System.Windows.Forms.ComboBox();
            this.comboBox_switchControl = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(248, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(181, 108);
            this.listBox1.TabIndex = 0;
            // 
            // comboBox_LoadControl
            // 
            this.comboBox_LoadControl.FormattingEnabled = true;
            this.comboBox_LoadControl.Location = new System.Drawing.Point(152, 251);
            this.comboBox_LoadControl.Name = "comboBox_LoadControl";
            this.comboBox_LoadControl.Size = new System.Drawing.Size(121, 21);
            this.comboBox_LoadControl.TabIndex = 2;
            // 
            // comboBox_switchControl
            // 
            this.comboBox_switchControl.FormattingEnabled = true;
            this.comboBox_switchControl.Location = new System.Drawing.Point(152, 300);
            this.comboBox_switchControl.Name = "comboBox_switchControl";
            this.comboBox_switchControl.Size = new System.Drawing.Size(121, 21);
            this.comboBox_switchControl.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(298, 337);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 25);
            this.button1.TabIndex = 4;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormConnectionManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 396);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox_switchControl);
            this.Controls.Add(this.comboBox_LoadControl);
            this.Controls.Add(this.listBox1);
            this.Name = "FormConnectionManager";
            this.Text = "FormConnectionManager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ComboBox comboBox_LoadControl;
        private System.Windows.Forms.ComboBox comboBox_switchControl;
        private System.Windows.Forms.Button button1;
    }
}