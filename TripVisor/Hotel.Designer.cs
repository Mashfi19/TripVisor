namespace TripVisor
{
    partial class Hotel
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
            this.pnlContainHotel = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Separator5 = new Guna.UI2.WinForms.Guna2Separator();
            this.label3 = new System.Windows.Forms.Label();
            this.flpHotel = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlContainHotel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainHotel
            // 
            this.pnlContainHotel.BackColor = System.Drawing.Color.White;
            this.pnlContainHotel.Controls.Add(this.guna2Separator5);
            this.pnlContainHotel.Controls.Add(this.label3);
            this.pnlContainHotel.Location = new System.Drawing.Point(0, 0);
            this.pnlContainHotel.Name = "pnlContainHotel";
            this.pnlContainHotel.Size = new System.Drawing.Size(901, 83);
            this.pnlContainHotel.TabIndex = 0;
            this.pnlContainHotel.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlContainHotel_Paint);
            // 
            // guna2Separator5
            // 
            this.guna2Separator5.FillThickness = 2;
            this.guna2Separator5.Location = new System.Drawing.Point(202, 49);
            this.guna2Separator5.Name = "guna2Separator5";
            this.guna2Separator5.Size = new System.Drawing.Size(480, 34);
            this.guna2Separator5.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(311, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(292, 50);
            this.label3.TabIndex = 11;
            this.label3.Text = "Find Your Hotel";
            // 
            // flpHotel
            // 
            this.flpHotel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpHotel.AutoScroll = true;
            this.flpHotel.BackColor = System.Drawing.Color.White;
            this.flpHotel.Location = new System.Drawing.Point(0, 89);
            this.flpHotel.Name = "flpHotel";
            this.flpHotel.Size = new System.Drawing.Size(901, 554);
            this.flpHotel.TabIndex = 13;
            // 
            // Hotel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(901, 643);
            this.ControlBox = false;
            this.Controls.Add(this.flpHotel);
            this.Controls.Add(this.pnlContainHotel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Hotel";
            this.Text = "Hotel";
            this.Load += new System.EventHandler(this.Hotel_Load);
            this.pnlContainHotel.ResumeLayout(false);
            this.pnlContainHotel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlContainHotel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flpHotel;
        private ucHotel ucHotel1;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator5;
    }
}