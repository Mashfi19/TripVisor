namespace TripVisor
{
    partial class AddHotel
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
            this.btnBrowse = new Guna.UI2.WinForms.Guna2Button();
            this.picHotel = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblAddEdit = new System.Windows.Forms.Label();
            this.txtName = new Guna.UI2.WinForms.Guna2TextBox();
            this.cbClass = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbRoom = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrice = new Guna.UI2.WinForms.Guna2TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picHotel)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Animated = true;
            this.btnBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowse.BorderColor = System.Drawing.Color.Transparent;
            this.btnBrowse.BorderRadius = 12;
            this.btnBrowse.BorderThickness = 2;
            this.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowse.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBrowse.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBrowse.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBrowse.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBrowse.FillColor = System.Drawing.Color.DarkSlateGray;
            this.btnBrowse.FocusedColor = System.Drawing.Color.Transparent;
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBrowse.ForeColor = System.Drawing.Color.White;
            this.btnBrowse.HoverState.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.btnBrowse.HoverState.FillColor = System.Drawing.Color.White;
            this.btnBrowse.HoverState.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnBrowse.Location = new System.Drawing.Point(415, 397);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.ShadowDecoration.BorderRadius = 15;
            this.btnBrowse.ShadowDecoration.Color = System.Drawing.Color.DarkSlateGray;
            this.btnBrowse.ShadowDecoration.Enabled = true;
            this.btnBrowse.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3);
            this.btnBrowse.Size = new System.Drawing.Size(327, 39);
            this.btnBrowse.TabIndex = 52;
            this.btnBrowse.Text = "Browse Photos";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // picHotel
            // 
            this.picHotel.BorderRadius = 12;
            this.picHotel.FillColor = System.Drawing.Color.Gray;
            this.picHotel.ImageRotate = 0F;
            this.picHotel.Location = new System.Drawing.Point(415, 138);
            this.picHotel.Name = "picHotel";
            this.picHotel.Size = new System.Drawing.Size(327, 239);
            this.picHotel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHotel.TabIndex = 51;
            this.picHotel.TabStop = false;
            // 
            // lblAddEdit
            // 
            this.lblAddEdit.AutoSize = true;
            this.lblAddEdit.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddEdit.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblAddEdit.Location = new System.Drawing.Point(108, 76);
            this.lblAddEdit.Name = "lblAddEdit";
            this.lblAddEdit.Size = new System.Drawing.Size(0, 37);
            this.lblAddEdit.TabIndex = 49;
            // 
            // txtName
            // 
            this.txtName.Animated = true;
            this.txtName.BackColor = System.Drawing.Color.Transparent;
            this.txtName.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.txtName.BorderRadius = 12;
            this.txtName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtName.DefaultText = "";
            this.txtName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtName.FocusedState.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.HoverState.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.txtName.HoverState.FillColor = System.Drawing.Color.White;
            this.txtName.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.txtName.IconLeftOffset = new System.Drawing.Point(5, 0);
            this.txtName.IconLeftSize = new System.Drawing.Size(17, 17);
            this.txtName.Location = new System.Drawing.Point(115, 129);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtName.Name = "txtName";
            this.txtName.PasswordChar = '\0';
            this.txtName.PlaceholderText = "Hotel Name";
            this.txtName.SelectedText = "";
            this.txtName.ShadowDecoration.BorderRadius = 15;
            this.txtName.ShadowDecoration.Color = System.Drawing.Color.DarkSlateGray;
            this.txtName.ShadowDecoration.Enabled = true;
            this.txtName.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3);
            this.txtName.Size = new System.Drawing.Size(263, 39);
            this.txtName.TabIndex = 48;
            this.txtName.TextOffset = new System.Drawing.Point(5, 0);
            // 
            // cbClass
            // 
            this.cbClass.BackColor = System.Drawing.Color.Transparent;
            this.cbClass.BorderColor = System.Drawing.Color.Gray;
            this.cbClass.BorderRadius = 12;
            this.cbClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClass.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbClass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbClass.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbClass.ItemHeight = 30;
            this.cbClass.Items.AddRange(new object[] {
            "High",
            "Standard",
            "Low"});
            this.cbClass.Location = new System.Drawing.Point(115, 205);
            this.cbClass.Name = "cbClass";
            this.cbClass.Size = new System.Drawing.Size(263, 36);
            this.cbClass.TabIndex = 55;
            // 
            // cbRoom
            // 
            this.cbRoom.BackColor = System.Drawing.Color.Transparent;
            this.cbRoom.BorderColor = System.Drawing.Color.Gray;
            this.cbRoom.BorderRadius = 12;
            this.cbRoom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRoom.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbRoom.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbRoom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbRoom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbRoom.ItemHeight = 30;
            this.cbRoom.Items.AddRange(new object[] {
            "Super Delux",
            "Delux",
            "Regular"});
            this.cbRoom.Location = new System.Drawing.Point(115, 279);
            this.cbRoom.Name = "cbRoom";
            this.cbRoom.Size = new System.Drawing.Size(263, 36);
            this.cbRoom.TabIndex = 56;
            // 
            // btnClose
            // 
            this.btnClose.Animated = true;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BorderColor = System.Drawing.Color.Transparent;
            this.btnClose.BorderRadius = 12;
            this.btnClose.BorderThickness = 2;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.DarkRed;
            this.btnClose.FocusedColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.HoverState.BorderColor = System.Drawing.Color.DarkRed;
            this.btnClose.HoverState.FillColor = System.Drawing.Color.White;
            this.btnClose.HoverState.ForeColor = System.Drawing.Color.DarkRed;
            this.btnClose.Location = new System.Drawing.Point(263, 397);
            this.btnClose.Name = "btnClose";
            this.btnClose.ShadowDecoration.BorderRadius = 15;
            this.btnClose.ShadowDecoration.Color = System.Drawing.Color.DarkSlateGray;
            this.btnClose.ShadowDecoration.Enabled = true;
            this.btnClose.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3);
            this.btnClose.Size = new System.Drawing.Size(115, 39);
            this.btnClose.TabIndex = 58;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Animated = true;
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.BorderColor = System.Drawing.Color.Transparent;
            this.btnAdd.BorderRadius = 12;
            this.btnAdd.BorderThickness = 2;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAdd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAdd.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAdd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAdd.FillColor = System.Drawing.Color.DarkSlateGray;
            this.btnAdd.FocusedColor = System.Drawing.Color.Transparent;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.HoverState.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.btnAdd.HoverState.FillColor = System.Drawing.Color.White;
            this.btnAdd.HoverState.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnAdd.Location = new System.Drawing.Point(115, 397);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.ShadowDecoration.BorderRadius = 15;
            this.btnAdd.ShadowDecoration.Color = System.Drawing.Color.DarkSlateGray;
            this.btnAdd.ShadowDecoration.Enabled = true;
            this.btnAdd.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3);
            this.btnAdd.Size = new System.Drawing.Size(115, 39);
            this.btnAdd.TabIndex = 57;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(122, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 59;
            this.label1.Text = "Select Class";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(122, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 60;
            this.label2.Text = "Select Room";
            // 
            // txtPrice
            // 
            this.txtPrice.Animated = true;
            this.txtPrice.BackColor = System.Drawing.Color.Transparent;
            this.txtPrice.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.txtPrice.BorderRadius = 12;
            this.txtPrice.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPrice.DefaultText = "";
            this.txtPrice.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtPrice.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtPrice.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPrice.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPrice.FocusedState.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.txtPrice.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtPrice.ForeColor = System.Drawing.Color.Black;
            this.txtPrice.HoverState.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.txtPrice.HoverState.FillColor = System.Drawing.Color.White;
            this.txtPrice.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPrice.IconLeftOffset = new System.Drawing.Point(5, 0);
            this.txtPrice.IconLeftSize = new System.Drawing.Size(17, 17);
            this.txtPrice.Location = new System.Drawing.Point(115, 342);
            this.txtPrice.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.PasswordChar = '\0';
            this.txtPrice.PlaceholderText = "Price/Day";
            this.txtPrice.SelectedText = "";
            this.txtPrice.ShadowDecoration.BorderRadius = 15;
            this.txtPrice.ShadowDecoration.Color = System.Drawing.Color.DarkSlateGray;
            this.txtPrice.ShadowDecoration.Enabled = true;
            this.txtPrice.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3);
            this.txtPrice.Size = new System.Drawing.Size(263, 39);
            this.txtPrice.TabIndex = 67;
            this.txtPrice.TextOffset = new System.Drawing.Point(5, 0);
            // 
            // AddHotel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(848, 525);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbRoom);
            this.Controls.Add(this.cbClass);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.picHotel);
            this.Controls.Add(this.lblAddEdit);
            this.Controls.Add(this.txtName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddHotel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add_Hotel";
            ((System.ComponentModel.ISupportInitialize)(this.picHotel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btnBrowse;
        private Guna.UI2.WinForms.Guna2PictureBox picHotel;
        private System.Windows.Forms.Label lblAddEdit;
        private Guna.UI2.WinForms.Guna2TextBox txtName;
        private Guna.UI2.WinForms.Guna2ComboBox cbClass;
        private Guna.UI2.WinForms.Guna2ComboBox cbRoom;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox txtPrice;
    }
}