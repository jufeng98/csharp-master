namespace CsharpHardware
{
    partial class FormPrintPreview
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
            this.paperSizeLabel = new System.Windows.Forms.Label();
            this.printResolutionLabel = new System.Windows.Forms.Label();
            this.printAreaLabel = new System.Windows.Forms.Label();
            this.printerSettingsButton = new System.Windows.Forms.Button();
            this.pageSettingButton = new System.Windows.Forms.Button();
            this.pd = new System.Drawing.Printing.PrintDocument();
            this.SuspendLayout();
            // 
            // paperSizeLabel
            // 
            this.paperSizeLabel.AutoSize = true;
            this.paperSizeLabel.Location = new System.Drawing.Point(388, 3);
            this.paperSizeLabel.Name = "paperSizeLabel";
            this.paperSizeLabel.Size = new System.Drawing.Size(35, 17);
            this.paperSizeLabel.TabIndex = 2;
            this.paperSizeLabel.Text = "纸张:";
            // 
            // printResolutionLabel
            // 
            this.printResolutionLabel.AutoSize = true;
            this.printResolutionLabel.Location = new System.Drawing.Point(712, 3);
            this.printResolutionLabel.Name = "printResolutionLabel";
            this.printResolutionLabel.Size = new System.Drawing.Size(71, 17);
            this.printResolutionLabel.TabIndex = 3;
            this.printResolutionLabel.Text = "打印分辨率:";
            // 
            // printAreaLabel
            // 
            this.printAreaLabel.AutoSize = true;
            this.printAreaLabel.Location = new System.Drawing.Point(525, 3);
            this.printAreaLabel.Name = "printAreaLabel";
            this.printAreaLabel.Size = new System.Drawing.Size(71, 17);
            this.printAreaLabel.TabIndex = 4;
            this.printAreaLabel.Text = "可打印区域:";
            // 
            // printerSettingsButton
            // 
            this.printerSettingsButton.BackColor = System.Drawing.SystemColors.Control;
            this.printerSettingsButton.Location = new System.Drawing.Point(234, 0);
            this.printerSettingsButton.Name = "printerSettingsButton";
            this.printerSettingsButton.Size = new System.Drawing.Size(82, 22);
            this.printerSettingsButton.TabIndex = 5;
            this.printerSettingsButton.Text = "打印机设置";
            this.printerSettingsButton.UseVisualStyleBackColor = false;
            this.printerSettingsButton.Click += new System.EventHandler(this.PrinterSettingsButton_Click);
            // 
            // pageSettingButton
            // 
            this.pageSettingButton.BackColor = System.Drawing.SystemColors.Control;
            this.pageSettingButton.Location = new System.Drawing.Point(311, 0);
            this.pageSettingButton.Name = "pageSettingButton";
            this.pageSettingButton.Size = new System.Drawing.Size(73, 22);
            this.pageSettingButton.TabIndex = 6;
            this.pageSettingButton.Text = "页面设置";
            this.pageSettingButton.UseVisualStyleBackColor = false;
            this.pageSettingButton.Click += new System.EventHandler(this.PageSettingsButton_Click);
            // 
            // FormPrintPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(926, 681);
            this.Controls.Add(this.pageSettingButton);
            this.Controls.Add(this.printerSettingsButton);
            this.Controls.Add(this.printAreaLabel);
            this.Controls.Add(this.printResolutionLabel);
            this.Controls.Add(this.paperSizeLabel);
            this.Icon = global::CsharpHardware.Properties.Resources.favicon32;
            this.Name = "FormPrintPreview";
            this.ShowInTaskbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.UseAntiAlias = true;
            this.Controls.SetChildIndex(this.paperSizeLabel, 0);
            this.Controls.SetChildIndex(this.printResolutionLabel, 0);
            this.Controls.SetChildIndex(this.printAreaLabel, 0);
            this.Controls.SetChildIndex(this.printerSettingsButton, 0);
            this.Controls.SetChildIndex(this.pageSettingButton, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label paperSizeLabel;
        private System.Windows.Forms.Label printResolutionLabel;
        private System.Windows.Forms.Label printAreaLabel;
        private System.Windows.Forms.Button printerSettingsButton;
        private System.Windows.Forms.Button pageSettingButton;
        public System.Drawing.Printing.PrintDocument pd;
    }
}