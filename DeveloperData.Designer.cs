namespace CRA.ModelLayer.DCC
{
    partial class DeveloperData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeveloperData));
            this.btnSaveDeveloperData = new System.Windows.Forms.Button();
            this.btnExitDeveloperData = new System.Windows.Forms.Button();
            this.txtNameLastname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInstitution = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSaveDeveloperData
            // 
            this.btnSaveDeveloperData.Location = new System.Drawing.Point(12, 154);
            this.btnSaveDeveloperData.Name = "btnSaveDeveloperData";
            this.btnSaveDeveloperData.Size = new System.Drawing.Size(75, 23);
            this.btnSaveDeveloperData.TabIndex = 0;
            this.btnSaveDeveloperData.Text = "save";
            this.btnSaveDeveloperData.UseVisualStyleBackColor = true;
            this.btnSaveDeveloperData.Click += new System.EventHandler(this.btnSaveDeveloperData_Click);
            // 
            // btnExitDeveloperData
            // 
            this.btnExitDeveloperData.Location = new System.Drawing.Point(197, 154);
            this.btnExitDeveloperData.Name = "btnExitDeveloperData";
            this.btnExitDeveloperData.Size = new System.Drawing.Size(75, 23);
            this.btnExitDeveloperData.TabIndex = 1;
            this.btnExitDeveloperData.Text = "exit";
            this.btnExitDeveloperData.UseVisualStyleBackColor = true;
            this.btnExitDeveloperData.Click += new System.EventHandler(this.btnExitDeveloperData_Click);
            // 
            // txtNameLastname
            // 
            this.txtNameLastname.Location = new System.Drawing.Point(12, 24);
            this.txtNameLastname.Name = "txtNameLastname";
            this.txtNameLastname.Size = new System.Drawing.Size(170, 20);
            this.txtNameLastname.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(188, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name Lastname";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(13, 50);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(169, 20);
            this.txtEmail.TabIndex = 4;
            this.txtEmail.Text = "@";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Email";
            // 
            // txtInstitution
            // 
            this.txtInstitution.Location = new System.Drawing.Point(13, 76);
            this.txtInstitution.Name = "txtInstitution";
            this.txtInstitution.Size = new System.Drawing.Size(169, 20);
            this.txtInstitution.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(188, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Institution";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(13, 103);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(169, 20);
            this.txtURL.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(188, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "URL";
            // 
            // DeveloperData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 193);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtInstitution);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNameLastname);
            this.Controls.Add(this.btnExitDeveloperData);
            this.Controls.Add(this.btnSaveDeveloperData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DeveloperData";
            this.Text = "Developer Data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveDeveloperData;
        private System.Windows.Forms.Button btnExitDeveloperData;
        private System.Windows.Forms.TextBox txtNameLastname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInstitution;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label4;
    }
}