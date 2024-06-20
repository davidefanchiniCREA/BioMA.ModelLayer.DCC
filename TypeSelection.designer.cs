namespace CRA.ModelLayer.DCC
{
    partial class TypeSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TypeSelection));
            this.textBoxType = new System.Windows.Forms.TextBox();
            this.lblSize = new System.Windows.Forms.Label();
            this.textBoxSize = new System.Windows.Forms.TextBox();
            this.listBoxTypes = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // textBoxType
            // 
            this.textBoxType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxType.Enabled = false;
            this.textBoxType.Location = new System.Drawing.Point(4, 4);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.Size = new System.Drawing.Size(216, 20);
            this.textBoxType.TabIndex = 0;
            // 
            // lblSize
            // 
            this.lblSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(226, 6);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(30, 13);
            this.lblSize.TabIndex = 1;
            this.lblSize.Text = "Size:";
            // 
            // textBoxSize
            // 
            this.textBoxSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSize.Enabled = false;
            this.textBoxSize.Location = new System.Drawing.Point(263, 4);
            this.textBoxSize.Name = "textBoxSize";
            this.textBoxSize.Size = new System.Drawing.Size(59, 20);
            this.textBoxSize.TabIndex = 2;
            // 
            // listBoxTypes
            // 
            this.listBoxTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxTypes.FormattingEnabled = true;
            this.listBoxTypes.Location = new System.Drawing.Point(4, 31);
            this.listBoxTypes.Name = "listBoxTypes";
            this.listBoxTypes.Size = new System.Drawing.Size(320, 225);
            this.listBoxTypes.TabIndex = 3;
            this.listBoxTypes.SelectedValueChanged += new System.EventHandler(this.listBoxTypes_SelectedValueChanged);
            // 
            // TypeSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 262);
            this.Controls.Add(this.listBoxTypes);
            this.Controls.Add(this.textBoxSize);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.textBoxType);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TypeSelection";
            this.Text = "Type Selection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TypeSelection_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TypeSelection_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxType;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.TextBox textBoxSize;
        private System.Windows.Forms.ListBox listBoxTypes;

    }
}