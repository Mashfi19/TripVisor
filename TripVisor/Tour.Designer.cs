namespace TripVisor
{
    partial class Tour
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tour));
            this.pnlContainTour = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Separator5 = new Guna.UI2.WinForms.Guna2Separator();
            this.lblHeading = new System.Windows.Forms.Label();
            this.flpPack = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlContainTour.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainTour
            // 
            this.pnlContainTour.AutoScrollMargin = new System.Drawing.Size(0, 50);
            this.pnlContainTour.BackColor = System.Drawing.Color.White;
            this.pnlContainTour.Controls.Add(this.guna2Separator5);
            this.pnlContainTour.Controls.Add(this.lblHeading);
            this.pnlContainTour.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlContainTour.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlContainTour.Location = new System.Drawing.Point(0, 0);
            this.pnlContainTour.Name = "pnlContainTour";
            this.pnlContainTour.Size = new System.Drawing.Size(901, 83);
            this.pnlContainTour.TabIndex = 1;
            // 
            // guna2Separator5
            // 
            this.guna2Separator5.FillThickness = 2;
            this.guna2Separator5.Location = new System.Drawing.Point(210, 57);
            this.guna2Separator5.Name = "guna2Separator5";
            this.guna2Separator5.Size = new System.Drawing.Size(480, 34);
            this.guna2Separator5.TabIndex = 11;
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblHeading.Location = new System.Drawing.Point(212, 8);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(476, 50);
            this.lblHeading.TabIndex = 2;
            this.lblHeading.Text = "Special Vacation Packages";
            // 
            // flpPack
            // 
            this.flpPack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpPack.AutoScroll = true;
            this.flpPack.BackColor = System.Drawing.Color.White;
            this.flpPack.Location = new System.Drawing.Point(0, 83);
            this.flpPack.Name = "flpPack";
            this.flpPack.Size = new System.Drawing.Size(901, 560);
            this.flpPack.TabIndex = 2;
            this.flpPack.Paint += new System.Windows.Forms.PaintEventHandler(this.flpPack_Paint);
            // 
            // Tour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 643);
            this.ControlBox = false;
            this.Controls.Add(this.flpPack);
            this.Controls.Add(this.pnlContainTour);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Tour";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tour";
            this.Load += new System.EventHandler(this.Tour_Load);
            this.pnlContainTour.ResumeLayout(false);
            this.pnlContainTour.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel pnlContainTour;
        private System.Windows.Forms.Label lblHeading;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator5;
        private System.Windows.Forms.FlowLayoutPanel flpPack;
        private ucPackage ucPackage1;
    }
}