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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 40);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(148, 121);
            this.listBox1.TabIndex = 0;
            // 
            // comboBox_LoadControl
            // 
            this.comboBox_LoadControl.FormattingEnabled = true;
            this.comboBox_LoadControl.Location = new System.Drawing.Point(302, 58);
            this.comboBox_LoadControl.Name = "comboBox_LoadControl";
            this.comboBox_LoadControl.Size = new System.Drawing.Size(104, 21);
            this.comboBox_LoadControl.TabIndex = 2;
            // 
            // comboBox_switchControl
            // 
            this.comboBox_switchControl.FormattingEnabled = true;
            this.comboBox_switchControl.Location = new System.Drawing.Point(301, 107);
            this.comboBox_switchControl.Name = "comboBox_switchControl";
            this.comboBox_switchControl.Size = new System.Drawing.Size(105, 21);
            this.comboBox_switchControl.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(293, 286);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 25);
            this.button1.TabIndex = 4;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Điều khiển tải";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Điều khiển chuyển mạch";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Các cổng COM khả dụng";
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(17, 216);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(270, 95);
            this.listBox2.TabIndex = 8;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Các ô xi lô  khả dụng";
            // 
            // FormConnectionManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 352);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox_switchControl);
            this.Controls.Add(this.comboBox_LoadControl);
            this.Controls.Add(this.listBox1);
            this.Name = "FormConnectionManager";
            this.Text = "FormConnectionManager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ComboBox comboBox_LoadControl;
        private System.Windows.Forms.ComboBox comboBox_switchControl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label4;
    }
}