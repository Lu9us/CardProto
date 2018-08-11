namespace DataBuilder
{
    partial class Form1
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
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.btbSave = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btbLoadObject = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(12, 12);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(653, 343);
            this.propertyGrid1.TabIndex = 0;
            // 
            // btbSave
            // 
            this.btbSave.Location = new System.Drawing.Point(517, 468);
            this.btbSave.Name = "btbSave";
            this.btbSave.Size = new System.Drawing.Size(75, 23);
            this.btbSave.TabIndex = 1;
            this.btbSave.Text = "save";
            this.btbSave.UseVisualStyleBackColor = true;
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(429, 389);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(204, 20);
            this.txtFile.TabIndex = 3;
            // 
            // btbLoadObject
            // 
            this.btbLoadObject.Location = new System.Drawing.Point(517, 430);
            this.btbLoadObject.Name = "btbLoadObject";
            this.btbLoadObject.Size = new System.Drawing.Size(75, 23);
            this.btbLoadObject.TabIndex = 4;
            this.btbLoadObject.Text = "Load";
            this.btbLoadObject.UseVisualStyleBackColor = true;
            this.btbLoadObject.Click += new System.EventHandler(this.btbLoadObject_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(174, 362);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(459, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 561);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btbLoadObject);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.btbSave);
            this.Controls.Add(this.propertyGrid1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button btbSave;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btbLoadObject;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

