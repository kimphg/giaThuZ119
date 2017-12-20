namespace Z119.ATK.Shell
{
    partial class fTEST
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.myImage1 = new Z119.ATK.Shell.MyControls.MyImage();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myImage1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.myImage1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1013, 626);
            this.panel1.TabIndex = 0;
            // 
            // myImage1
            // 
            this.myImage1.Image = global::Z119.ATK.Shell.Properties.Resources._3821_4;
            this.myImage1.Location = new System.Drawing.Point(3, 3);
            this.myImage1.Name = "myImage1";
            this.myImage1.Size = new System.Drawing.Size(1440, 900);
            this.myImage1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.myImage1.TabIndex = 0;
            this.myImage1.TabStop = false;
            // 
            // fTEST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 626);
            this.Controls.Add(this.panel1);
            this.Name = "fTEST";
            this.Text = "fTEST";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myImage1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MyControls.MyImage myImage1;
    }
}