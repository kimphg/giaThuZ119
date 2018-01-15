namespace Z119.ATK.Shell
{
    partial class FormTextInput
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboListFalse = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxListTrue = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(4, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(386, 58);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(203, 125);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(294, 125);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Hủy";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboListFalse
            // 
            this.comboListFalse.FormattingEnabled = true;
            this.comboListFalse.Location = new System.Drawing.Point(12, 154);
            this.comboListFalse.Name = "comboListFalse";
            this.comboListFalse.Size = new System.Drawing.Size(121, 21);
            this.comboListFalse.TabIndex = 4;
            this.comboListFalse.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Nếu sai nhảy đến bước:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nếu hoàn thành nhảy đến bước:";
            // 
            // comboBoxListTrue
            // 
            this.comboBoxListTrue.FormattingEnabled = true;
            this.comboBoxListTrue.Location = new System.Drawing.Point(12, 101);
            this.comboBoxListTrue.Name = "comboBoxListTrue";
            this.comboBoxListTrue.Size = new System.Drawing.Size(121, 21);
            this.comboBoxListTrue.TabIndex = 8;
            // 
            // FormTextInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 228);
            this.Controls.Add(this.comboBoxListTrue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboListFalse);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "FormTextInput";
            this.Text = "FormTextInput";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;

        
        private System.Windows.Forms.ComboBox comboListFalse;

        public System.Windows.Forms.ComboBox ComboListFalse
        {
            get { return comboListFalse; }
            set { comboListFalse = value; }
        }
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxListTrue;

        public System.Windows.Forms.ComboBox ComboBoxListTrue
        {
            get { return comboBoxListTrue; }
            set { comboBoxListTrue = value; }
        }

        public System.Windows.Forms.Label Label2
        {
            get { return label2; }
            set { label2 = value; }
        }

        
    }
}