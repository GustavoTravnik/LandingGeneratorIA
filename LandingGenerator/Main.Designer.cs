namespace LandingGenerator
{
    partial class Main
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
            this.btnStartSync = new System.Windows.Forms.Button();
            this.txtTheme = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listSections = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.AddSection = new System.Windows.Forms.Button();
            this.txtSectionName = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.RemoveSection = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartSync
            // 
            this.btnStartSync.Location = new System.Drawing.Point(302, 9);
            this.btnStartSync.Name = "btnStartSync";
            this.btnStartSync.Size = new System.Drawing.Size(75, 20);
            this.btnStartSync.TabIndex = 0;
            this.btnStartSync.Text = "Go";
            this.btnStartSync.UseVisualStyleBackColor = true;
            this.btnStartSync.Click += new System.EventHandler(this.btnStartSync_Click);
            // 
            // txtTheme
            // 
            this.txtTheme.Location = new System.Drawing.Point(67, 9);
            this.txtTheme.Name = "txtTheme";
            this.txtTheme.Size = new System.Drawing.Size(229, 20);
            this.txtTheme.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tema";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pictureBox1.Location = new System.Drawing.Point(-5, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(401, 10);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // listSections
            // 
            this.listSections.FormattingEnabled = true;
            this.listSections.Location = new System.Drawing.Point(15, 78);
            this.listSections.Name = "listSections";
            this.listSections.Size = new System.Drawing.Size(281, 108);
            this.listSections.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Secções";
            // 
            // AddSection
            // 
            this.AddSection.Location = new System.Drawing.Point(302, 52);
            this.AddSection.Name = "AddSection";
            this.AddSection.Size = new System.Drawing.Size(75, 20);
            this.AddSection.TabIndex = 6;
            this.AddSection.Text = "Add";
            this.AddSection.UseVisualStyleBackColor = true;
            this.AddSection.Click += new System.EventHandler(this.AddSection_Click);
            // 
            // txtSectionName
            // 
            this.txtSectionName.Location = new System.Drawing.Point(67, 52);
            this.txtSectionName.Name = "txtSectionName";
            this.txtSectionName.Size = new System.Drawing.Size(229, 20);
            this.txtSectionName.TabIndex = 7;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pictureBox2.Location = new System.Drawing.Point(-5, 192);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(401, 10);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // RemoveSection
            // 
            this.RemoveSection.Location = new System.Drawing.Point(302, 78);
            this.RemoveSection.Name = "RemoveSection";
            this.RemoveSection.Size = new System.Drawing.Size(75, 20);
            this.RemoveSection.TabIndex = 9;
            this.RemoveSection.Text = "Remove";
            this.RemoveSection.UseVisualStyleBackColor = true;
            this.RemoveSection.Click += new System.EventHandler(this.RemoveSection_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 476);
            this.Controls.Add(this.RemoveSection);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.txtSectionName);
            this.Controls.Add(this.AddSection);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listSections);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTheme);
            this.Controls.Add(this.btnStartSync);
            this.Name = "Main";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartSync;
        private System.Windows.Forms.TextBox txtTheme;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox listSections;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button AddSection;
        private System.Windows.Forms.TextBox txtSectionName;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button RemoveSection;
    }
}

