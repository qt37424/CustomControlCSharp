namespace JsonToCSV
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
            this.txtboxJSONFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.convertBtn = new System.Windows.Forms.Button();
            this.treeView = new System.Windows.Forms.TreeView();
            this.btnConfigInputBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtboxJSONFile
            // 
            this.txtboxJSONFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtboxJSONFile.Location = new System.Drawing.Point(100, 29);
            this.txtboxJSONFile.Name = "txtboxJSONFile";
            this.txtboxJSONFile.Size = new System.Drawing.Size(619, 19);
            this.txtboxJSONFile.TabIndex = 0;
            this.txtboxJSONFile.TextChanged += new System.EventHandler(this.txtboxJSONFile_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Path file JSON";
            // 
            // convertBtn
            // 
            this.convertBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.convertBtn.Location = new System.Drawing.Point(678, 54);
            this.convertBtn.Name = "convertBtn";
            this.convertBtn.Size = new System.Drawing.Size(75, 23);
            this.convertBtn.TabIndex = 2;
            this.convertBtn.Text = "Convert";
            this.convertBtn.UseVisualStyleBackColor = true;
            this.convertBtn.Click += new System.EventHandler(this.convertBtn_Click);
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.CheckBoxes = true;
            this.treeView.Location = new System.Drawing.Point(0, 94);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(765, 332);
            this.treeView.TabIndex = 3;
            // 
            // btnConfigInputBrowse
            // 
            this.btnConfigInputBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfigInputBrowse.BackColor = System.Drawing.Color.White;
            this.btnConfigInputBrowse.Location = new System.Drawing.Point(724, 28);
            this.btnConfigInputBrowse.Margin = new System.Windows.Forms.Padding(0);
            this.btnConfigInputBrowse.Name = "btnConfigInputBrowse";
            this.btnConfigInputBrowse.Size = new System.Drawing.Size(32, 20);
            this.btnConfigInputBrowse.TabIndex = 4;
            this.btnConfigInputBrowse.Text = "...";
            this.btnConfigInputBrowse.UseVisualStyleBackColor = false;
            this.btnConfigInputBrowse.Click += new System.EventHandler(this.btnInputBrowse_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 426);
            this.Controls.Add(this.btnConfigInputBrowse);
            this.Controls.Add(this.convertBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtboxJSONFile);
            this.Controls.Add(this.treeView);
            this.Name = "Main";
            this.Text = "JsonToCSV";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtboxJSONFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button convertBtn;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Button btnConfigInputBrowse;
    }
}

