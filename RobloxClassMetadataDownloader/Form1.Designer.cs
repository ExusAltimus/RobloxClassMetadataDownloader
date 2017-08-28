namespace RobloxClassMetadataDownloader
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
            this.btnDownloadMetadata = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.chkFormatTable = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnDownloadMetadata
            // 
            this.btnDownloadMetadata.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownloadMetadata.Location = new System.Drawing.Point(12, 518);
            this.btnDownloadMetadata.Name = "btnDownloadMetadata";
            this.btnDownloadMetadata.Size = new System.Drawing.Size(407, 74);
            this.btnDownloadMetadata.TabIndex = 0;
            this.btnDownloadMetadata.Text = "Download Metadata";
            this.btnDownloadMetadata.UseVisualStyleBackColor = true;
            this.btnDownloadMetadata.Click += new System.EventHandler(this.btnDownloadMetadata_Click);
            // 
            // txtResult
            // 
            this.txtResult.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Location = new System.Drawing.Point(12, 12);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(407, 459);
            this.txtResult.TabIndex = 1;
            this.txtResult.Text = "";
            this.txtResult.WordWrap = false;
            // 
            // chkFormatTable
            // 
            this.chkFormatTable.AutoSize = true;
            this.chkFormatTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFormatTable.Location = new System.Drawing.Point(110, 477);
            this.chkFormatTable.Name = "chkFormatTable";
            this.chkFormatTable.Size = new System.Drawing.Size(194, 35);
            this.chkFormatTable.TabIndex = 2;
            this.chkFormatTable.Text = "Format Table";
            this.chkFormatTable.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 604);
            this.Controls.Add(this.chkFormatTable);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnDownloadMetadata);
            this.Name = "Form1";
            this.Text = "Roblox Metadata Downloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDownloadMetadata;
        private System.Windows.Forms.RichTextBox txtResult;
        private System.Windows.Forms.CheckBox chkFormatTable;
    }
}

