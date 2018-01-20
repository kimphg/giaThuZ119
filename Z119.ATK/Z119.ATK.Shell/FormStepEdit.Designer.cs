namespace Z119.ATK.Shell
{
    partial class FormStepEdit
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
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboListTrue = new System.Windows.Forms.ComboBox();
            this.comboBoxSchemePoints = new System.Windows.Forms.ComboBox();
            this.comboListFalse = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(12, 62);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(291, 39);
            this.textBoxDescription.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(189, 193);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(189, 164);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Hủy";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Nếu không đạt nhảy đến bước:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nếu hoàn thành nhảy đến bước:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Mô tả:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // comboListTrue
            // 
            this.comboListTrue.FormattingEnabled = true;
            this.comboListTrue.Location = new System.Drawing.Point(12, 129);
            this.comboListTrue.Name = "comboListTrue";
            this.comboListTrue.Size = new System.Drawing.Size(121, 21);
            this.comboListTrue.TabIndex = 10;
            // 
            // comboBoxSchemePoints
            // 
            this.comboBoxSchemePoints.FormattingEnabled = true;
            this.comboBoxSchemePoints.Location = new System.Drawing.Point(187, 129);
            this.comboBoxSchemePoints.Name = "comboBoxSchemePoints";
            this.comboBoxSchemePoints.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSchemePoints.TabIndex = 11;
            // 
            // comboListFalse
            // 
            this.comboListFalse.FormattingEnabled = true;
            this.comboListFalse.Location = new System.Drawing.Point(12, 195);
            this.comboListFalse.Name = "comboListFalse";
            this.comboListFalse.Size = new System.Drawing.Size(121, 21);
            this.comboListFalse.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(186, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Điểm đo tương ứng:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Tên:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(12, 19);
            this.textBoxName.Multiline = true;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(128, 23);
            this.textBoxName.TabIndex = 15;
            // 
            // FormStepEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 250);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboListFalse);
            this.Controls.Add(this.comboBoxSchemePoints);
            this.Controls.Add(this.comboListTrue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxDescription);
            this.Name = "FormStepEdit";
            this.Text = "FormTextInput";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;

        public System.Windows.Forms.ComboBox ComboListFalse
        {
            get { return comboListFalse; }
            set { comboListFalse = value; }
        }
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboListTrue;
        private System.Windows.Forms.ComboBox comboBoxSchemePoints;
        private System.Windows.Forms.ComboBox comboListFalse;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxName;


        public System.Windows.Forms.Label Label2
        {
            get { return label2; }
            set { label2 = value; }
        }

        
    }
}