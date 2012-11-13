namespace BonTemps
{
    partial class formMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formMain));
            this.lbxOverview = new System.Windows.Forms.ListBox();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportClientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alterMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.checkStockedSuppliesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlOrder = new System.Windows.Forms.Panel();
            this.lbl1_pnlOrder = new System.Windows.Forms.Label();
            this.lblClientName_pnlOrder = new System.Windows.Forms.Label();
            this.lblClientID_pnlOrder = new System.Windows.Forms.Label();
            this.lblTableID_pnlOrder = new System.Windows.Forms.Label();
            this.btnCancelNewOrder = new System.Windows.Forms.Button();
            this.btnCreateNewOrder = new System.Windows.Forms.Button();
            this.tbx1_pnlOrder = new System.Windows.Forms.TextBox();
            this.tbxClientName_pnlOrder = new System.Windows.Forms.TextBox();
            this.tbxClientID_pnlOrder = new System.Windows.Forms.TextBox();
            this.tbxTableID_pnlOrder = new System.Windows.Forms.TextBox();
            this.pnlOverview = new System.Windows.Forms.Panel();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportClientsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitbuttonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlOrder.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbxOverview
            // 
            this.lbxOverview.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbxOverview.FormattingEnabled = true;
            this.lbxOverview.Location = new System.Drawing.Point(0, 24);
            this.lbxOverview.Name = "lbxOverview";
            this.lbxOverview.Size = new System.Drawing.Size(161, 336);
            this.lbxOverview.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exportClientsToolStripMenuItem
            // 
            this.exportClientsToolStripMenuItem.Name = "exportClientsToolStripMenuItem";
            this.exportClientsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exportClientsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.exportClientsToolStripMenuItem.Text = "Export Clients";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(183, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.menuToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // alterMenuToolStripMenuItem
            // 
            this.alterMenuToolStripMenuItem.Name = "alterMenuToolStripMenuItem";
            this.alterMenuToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.alterMenuToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.alterMenuToolStripMenuItem.Text = "Alter Menu";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(196, 6);
            // 
            // checkStockedSuppliesToolStripMenuItem
            // 
            this.checkStockedSuppliesToolStripMenuItem.Name = "checkStockedSuppliesToolStripMenuItem";
            this.checkStockedSuppliesToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.checkStockedSuppliesToolStripMenuItem.Text = "Check Stocked Supplies";
            // 
            // pnlOrder
            // 
            this.pnlOrder.AutoScroll = true;
            this.pnlOrder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlOrder.BackgroundImage")));
            this.pnlOrder.Controls.Add(this.lbl1_pnlOrder);
            this.pnlOrder.Controls.Add(this.lblClientName_pnlOrder);
            this.pnlOrder.Controls.Add(this.lblClientID_pnlOrder);
            this.pnlOrder.Controls.Add(this.lblTableID_pnlOrder);
            this.pnlOrder.Controls.Add(this.btnCancelNewOrder);
            this.pnlOrder.Controls.Add(this.btnCreateNewOrder);
            this.pnlOrder.Controls.Add(this.tbx1_pnlOrder);
            this.pnlOrder.Controls.Add(this.tbxClientName_pnlOrder);
            this.pnlOrder.Controls.Add(this.tbxClientID_pnlOrder);
            this.pnlOrder.Controls.Add(this.tbxTableID_pnlOrder);
            this.pnlOrder.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlOrder.Location = new System.Drawing.Point(585, 24);
            this.pnlOrder.Name = "pnlOrder";
            this.pnlOrder.Size = new System.Drawing.Size(200, 336);
            this.pnlOrder.TabIndex = 0;
            // 
            // lbl1_pnlOrder
            // 
            this.lbl1_pnlOrder.AutoSize = true;
            this.lbl1_pnlOrder.Location = new System.Drawing.Point(3, 132);
            this.lbl1_pnlOrder.Name = "lbl1_pnlOrder";
            this.lbl1_pnlOrder.Size = new System.Drawing.Size(36, 13);
            this.lbl1_pnlOrder.TabIndex = 2;
            this.lbl1_pnlOrder.Text = "Order:";
            // 
            // lblClientName_pnlOrder
            // 
            this.lblClientName_pnlOrder.AutoSize = true;
            this.lblClientName_pnlOrder.Location = new System.Drawing.Point(3, 93);
            this.lblClientName_pnlOrder.Name = "lblClientName_pnlOrder";
            this.lblClientName_pnlOrder.Size = new System.Drawing.Size(67, 13);
            this.lblClientName_pnlOrder.TabIndex = 2;
            this.lblClientName_pnlOrder.Text = "Client Name:";
            // 
            // lblClientID_pnlOrder
            // 
            this.lblClientID_pnlOrder.AutoSize = true;
            this.lblClientID_pnlOrder.Location = new System.Drawing.Point(3, 54);
            this.lblClientID_pnlOrder.Name = "lblClientID_pnlOrder";
            this.lblClientID_pnlOrder.Size = new System.Drawing.Size(50, 13);
            this.lblClientID_pnlOrder.TabIndex = 2;
            this.lblClientID_pnlOrder.Text = "Client ID:";
            // 
            // lblTableID_pnlOrder
            // 
            this.lblTableID_pnlOrder.AutoSize = true;
            this.lblTableID_pnlOrder.Location = new System.Drawing.Point(3, 15);
            this.lblTableID_pnlOrder.Name = "lblTableID_pnlOrder";
            this.lblTableID_pnlOrder.Size = new System.Drawing.Size(37, 13);
            this.lblTableID_pnlOrder.TabIndex = 2;
            this.lblTableID_pnlOrder.Text = "Table:";
            // 
            // btnCancelNewOrder
            // 
            this.btnCancelNewOrder.Location = new System.Drawing.Point(3, 203);
            this.btnCancelNewOrder.Name = "btnCancelNewOrder";
            this.btnCancelNewOrder.Size = new System.Drawing.Size(163, 23);
            this.btnCancelNewOrder.TabIndex = 1;
            this.btnCancelNewOrder.Text = "Cancel";
            this.btnCancelNewOrder.UseVisualStyleBackColor = true;
            // 
            // btnCreateNewOrder
            // 
            this.btnCreateNewOrder.Location = new System.Drawing.Point(3, 174);
            this.btnCreateNewOrder.Name = "btnCreateNewOrder";
            this.btnCreateNewOrder.Size = new System.Drawing.Size(163, 23);
            this.btnCreateNewOrder.TabIndex = 1;
            this.btnCreateNewOrder.Text = "New Order";
            this.btnCreateNewOrder.UseVisualStyleBackColor = true;
            // 
            // tbx1_pnlOrder
            // 
            this.tbx1_pnlOrder.Location = new System.Drawing.Point(3, 148);
            this.tbx1_pnlOrder.Name = "tbx1_pnlOrder";
            this.tbx1_pnlOrder.Size = new System.Drawing.Size(163, 20);
            this.tbx1_pnlOrder.TabIndex = 0;
            // 
            // tbxClientName_pnlOrder
            // 
            this.tbxClientName_pnlOrder.Location = new System.Drawing.Point(3, 109);
            this.tbxClientName_pnlOrder.Name = "tbxClientName_pnlOrder";
            this.tbxClientName_pnlOrder.Size = new System.Drawing.Size(163, 20);
            this.tbxClientName_pnlOrder.TabIndex = 0;
            // 
            // tbxClientID_pnlOrder
            // 
            this.tbxClientID_pnlOrder.Location = new System.Drawing.Point(3, 70);
            this.tbxClientID_pnlOrder.Name = "tbxClientID_pnlOrder";
            this.tbxClientID_pnlOrder.Size = new System.Drawing.Size(163, 20);
            this.tbxClientID_pnlOrder.TabIndex = 0;
            // 
            // tbxTableID_pnlOrder
            // 
            this.tbxTableID_pnlOrder.Location = new System.Drawing.Point(3, 31);
            this.tbxTableID_pnlOrder.Name = "tbxTableID_pnlOrder";
            this.tbxTableID_pnlOrder.Size = new System.Drawing.Size(163, 20);
            this.tbxTableID_pnlOrder.TabIndex = 0;
            // 
            // pnlOverview
            // 
            this.pnlOverview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlOverview.BackgroundImage")));
            this.pnlOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOverview.Location = new System.Drawing.Point(161, 24);
            this.pnlOverview.Name = "pnlOverview";
            this.pnlOverview.Size = new System.Drawing.Size(624, 336);
            this.pnlOverview.TabIndex = 2;
            // 
            // menuMain
            // 
            this.menuMain.BackgroundImage = global::BonTemps.Properties.Resources.menustrip2;
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.menuToolStripMenuItem1,
            this.exitbuttonToolStripMenuItem,
            this.minimizeToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuMain.Size = new System.Drawing.Size(785, 24);
            this.menuMain.TabIndex = 1;
            this.menuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportClientsToolStripMenuItem1,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "File";
            this.fileToolStripMenuItem1.DropDownClosed += new System.EventHandler(this.fileToolStripMenuItem1_DropDownClosed);
            this.fileToolStripMenuItem1.DropDownOpened += new System.EventHandler(this.fileToolStripMenuItem1_DropDownOpened);
            // 
            // exportClientsToolStripMenuItem1
            // 
            this.exportClientsToolStripMenuItem1.Name = "exportClientsToolStripMenuItem1";
            this.exportClientsToolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.exportClientsToolStripMenuItem1.Text = "Export Clients";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(143, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // menuToolStripMenuItem1
            // 
            this.menuToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.menuToolStripMenuItem1.Name = "menuToolStripMenuItem1";
            this.menuToolStripMenuItem1.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem1.Text = "Menu";
            this.menuToolStripMenuItem1.DropDownClosed += new System.EventHandler(this.menuToolStripMenuItem1_DropDownClosed);
            this.menuToolStripMenuItem1.DropDownOpened += new System.EventHandler(this.menuToolStripMenuItem1_DropDownOpened);
            // 
            // exitbuttonToolStripMenuItem
            // 
            this.exitbuttonToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.exitbuttonToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.exitbuttonToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.exitbuttonToolStripMenuItem.Image = global::BonTemps.Properties.Resources.closebutton;
            this.exitbuttonToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.exitbuttonToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.exitbuttonToolStripMenuItem.Name = "exitbuttonToolStripMenuItem";
            this.exitbuttonToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.exitbuttonToolStripMenuItem.Size = new System.Drawing.Size(28, 20);
            this.exitbuttonToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.exitbuttonToolStripMenuItem.Click += new System.EventHandler(this.exitbuttonToolStripMenuItem_Click);
            // 
            // minimizeToolStripMenuItem
            // 
            this.minimizeToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.minimizeToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.minimizeToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.minimizeToolStripMenuItem.Image = global::BonTemps.Properties.Resources.minimizebutton;
            this.minimizeToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.minimizeToolStripMenuItem.Name = "minimizeToolStripMenuItem";
            this.minimizeToolStripMenuItem.Size = new System.Drawing.Size(28, 20);
            // 
            // closeMenuToolStripMenuItem
            // 
            this.closeMenuToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.closeMenuToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.closeMenuToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.closeMenuToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("closeMenuToolStripMenuItem.Image")));
            this.closeMenuToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.closeMenuToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.closeMenuToolStripMenuItem.Name = "closeMenuToolStripMenuItem";
            this.closeMenuToolStripMenuItem.Size = new System.Drawing.Size(28, 20);
            this.closeMenuToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // minimizeMenuToolStripMenuItem
            // 
            this.minimizeMenuToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.minimizeMenuToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.minimizeMenuToolStripMenuItem.Image = global::BonTemps.Properties.Resources.minimizebutton;
            this.minimizeMenuToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.minimizeMenuToolStripMenuItem.Name = "minimizeMenuToolStripMenuItem";
            this.minimizeMenuToolStripMenuItem.Size = new System.Drawing.Size(28, 20);
            this.minimizeMenuToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.minimizeMenuToolStripMenuItem.Click += new System.EventHandler(this.minimizeMenuToolStripMenuItem_Click);
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 360);
            this.Controls.Add(this.pnlOrder);
            this.Controls.Add(this.pnlOverview);
            this.Controls.Add(this.lbxOverview);
            this.Controls.Add(this.menuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuMain;
            this.Name = "formMain";
            this.Text = "Form1";
            this.pnlOrder.ResumeLayout(false);
            this.pnlOrder.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbxOverview;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.Panel pnlOverview;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportClientsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem alterMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem checkStockedSuppliesToolStripMenuItem;
        private System.Windows.Forms.Panel pnlOrder;
        private System.Windows.Forms.Label lbl1_pnlOrder;
        private System.Windows.Forms.Label lblClientName_pnlOrder;
        private System.Windows.Forms.Label lblClientID_pnlOrder;
        private System.Windows.Forms.Label lblTableID_pnlOrder;
        private System.Windows.Forms.Button btnCreateNewOrder;
        private System.Windows.Forms.TextBox tbx1_pnlOrder;
        private System.Windows.Forms.TextBox tbxClientName_pnlOrder;
        private System.Windows.Forms.TextBox tbxClientID_pnlOrder;
        private System.Windows.Forms.TextBox tbxTableID_pnlOrder;
        private System.Windows.Forms.ToolStripMenuItem closeMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimizeMenuToolStripMenuItem;
        private System.Windows.Forms.Button btnCancelNewOrder;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exportClientsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitbuttonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimizeToolStripMenuItem;
    }
}

