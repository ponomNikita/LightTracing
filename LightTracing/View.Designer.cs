namespace LightTracing
{
    partial class View
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
            this.RenderBtn = new System.Windows.Forms.Button();
            this.Image = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.Image)).BeginInit();
            this.SuspendLayout();
            // 
            // RenderBtn
            // 
            this.RenderBtn.Location = new System.Drawing.Point(468, 12);
            this.RenderBtn.Name = "RenderBtn";
            this.RenderBtn.Size = new System.Drawing.Size(116, 23);
            this.RenderBtn.TabIndex = 0;
            this.RenderBtn.Text = "Build scene";
            this.RenderBtn.UseVisualStyleBackColor = true;
            this.RenderBtn.Click += new System.EventHandler(this.RenderBtn_Click);
            // 
            // Image
            // 
            this.Image.BackColor = System.Drawing.SystemColors.HighlightText;
            this.Image.Location = new System.Drawing.Point(12, 12);
            this.Image.Name = "Image";
            this.Image.Size = new System.Drawing.Size(450, 450);
            this.Image.TabIndex = 1;
            this.Image.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 483);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(450, 23);
            this.progressBar.TabIndex = 2;
            this.progressBar.Visible = false;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 518);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.Image);
            this.Controls.Add(this.RenderBtn);
            this.Name = "View";
            this.Text = "View";
            ((System.ComponentModel.ISupportInitialize)(this.Image)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RenderBtn;
        private System.Windows.Forms.PictureBox Image;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

