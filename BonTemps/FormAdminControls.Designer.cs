namespace BonTemps
{
    partial class FormAdminControls
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
            this.cbDatabases = new System.Windows.Forms.ComboBox();
            this.bFlush = new System.Windows.Forms.Button();
            this.bChange = new System.Windows.Forms.Button();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.bDeleteSelectedRow = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // cbDatabases
            // 
            this.cbDatabases.FormattingEnabled = true;
            this.cbDatabases.Location = new System.Drawing.Point(12, 12);
            this.cbDatabases.Name = "cbDatabases";
            this.cbDatabases.Size = new System.Drawing.Size(335, 21);
            this.cbDatabases.TabIndex = 0;
            this.cbDatabases.SelectedIndexChanged += new System.EventHandler(this.cbDatabases_SelectedIndexChanged);
            // 
            // bFlush
            // 
            this.bFlush.Location = new System.Drawing.Point(12, 283);
            this.bFlush.Name = "bFlush";
            this.bFlush.Size = new System.Drawing.Size(75, 23);
            this.bFlush.TabIndex = 1;
            this.bFlush.Text = "Flush";
            this.bFlush.UseVisualStyleBackColor = true;
            this.bFlush.Click += new System.EventHandler(this.bFlush_Click);
            // 
            // bChange
            // 
            this.bChange.Location = new System.Drawing.Point(230, 312);
            this.bChange.Name = "bChange";
            this.bChange.Size = new System.Drawing.Size(117, 23);
            this.bChange.TabIndex = 2;
            this.bChange.Text = "Update Changes";
            this.bChange.UseVisualStyleBackColor = true;
            // 
            // dgvResult
            // 
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Location = new System.Drawing.Point(12, 39);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.Size = new System.Drawing.Size(335, 238);
            this.dgvResult.TabIndex = 3;
            // 
            // bDeleteSelectedRow
            // 
            this.bDeleteSelectedRow.Location = new System.Drawing.Point(230, 283);
            this.bDeleteSelectedRow.Name = "bDeleteSelectedRow";
            this.bDeleteSelectedRow.Size = new System.Drawing.Size(117, 23);
            this.bDeleteSelectedRow.TabIndex = 4;
            this.bDeleteSelectedRow.Text = "Delete Selected";
            this.bDeleteSelectedRow.UseVisualStyleBackColor = true;
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(12, 356);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(335, 23);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // FormAdminControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 391);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.bDeleteSelectedRow);
            this.Controls.Add(this.dgvResult);
            this.Controls.Add(this.bChange);
            this.Controls.Add(this.bFlush);
            this.Controls.Add(this.cbDatabases);
            this.Name = "FormAdminControls";
            this.Text = "AdminControls";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDatabases;
        private System.Windows.Forms.Button bFlush;
        private System.Windows.Forms.Button bChange;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Button bDeleteSelectedRow;
        private System.Windows.Forms.Button btnLogout;
    }
}