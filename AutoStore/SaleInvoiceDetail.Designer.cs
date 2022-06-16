
namespace AutoStore
{
    partial class SaleInvoiceDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaleInvoiceDetail));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.bunifuLabel2 = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuLabel1 = new Bunifu.UI.WinForms.BunifuLabel();
            this.datePick = new Bunifu.Framework.UI.BunifuDatepicker();
            this.viewBtn = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.selectSaleInvoice = new System.Windows.Forms.ComboBox();
            this.sale = new System.Windows.Forms.FlowLayoutPanel();
            this.saleGV = new Bunifu.UI.WinForms.BunifuDataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.grossLabel = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuLabel8 = new Bunifu.UI.WinForms.BunifuLabel();
            this.panel1.SuspendLayout();
            this.sale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saleGV)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSlateGray;
            this.panel1.Controls.Add(this.iconButton1);
            this.panel1.Controls.Add(this.bunifuLabel2);
            this.panel1.Controls.Add(this.bunifuLabel1);
            this.panel1.Controls.Add(this.datePick);
            this.panel1.Controls.Add(this.viewBtn);
            this.panel1.Controls.Add(this.selectSaleInvoice);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(218, 509);
            this.panel1.TabIndex = 0;
            // 
            // iconButton1
            // 
            this.iconButton1.BackgroundImage = global::AutoStore.Properties.Resources.icons8_back_64;
            this.iconButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconButton1.IconColor = System.Drawing.Color.Black;
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton1.Location = new System.Drawing.Point(0, 0);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Size = new System.Drawing.Size(66, 46);
            this.iconButton1.TabIndex = 7;
            this.iconButton1.UseVisualStyleBackColor = true;
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // bunifuLabel2
            // 
            this.bunifuLabel2.AutoEllipsis = false;
            this.bunifuLabel2.CursorType = null;
            this.bunifuLabel2.Font = new System.Drawing.Font("Segoe UI Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuLabel2.ForeColor = System.Drawing.Color.White;
            this.bunifuLabel2.Location = new System.Drawing.Point(17, 153);
            this.bunifuLabel2.Name = "bunifuLabel2";
            this.bunifuLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel2.Size = new System.Drawing.Size(180, 19);
            this.bunifuLabel2.TabIndex = 6;
            this.bunifuLabel2.Text = "SELECT PURCHASE INVOICE";
            this.bunifuLabel2.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel2.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuLabel1
            // 
            this.bunifuLabel1.AutoEllipsis = false;
            this.bunifuLabel1.CursorType = null;
            this.bunifuLabel1.Font = new System.Drawing.Font("Segoe UI Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuLabel1.ForeColor = System.Drawing.Color.White;
            this.bunifuLabel1.Location = new System.Drawing.Point(42, 65);
            this.bunifuLabel1.Name = "bunifuLabel1";
            this.bunifuLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel1.Size = new System.Drawing.Size(103, 19);
            this.bunifuLabel1.TabIndex = 4;
            this.bunifuLabel1.Text = "SELECT MONTH";
            this.bunifuLabel1.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel1.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // datePick
            // 
            this.datePick.BackColor = System.Drawing.Color.LightSlateGray;
            this.datePick.BorderRadius = 0;
            this.datePick.ForeColor = System.Drawing.Color.White;
            this.datePick.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePick.FormatCustom = "MMMM - yyyy ";
            this.datePick.Location = new System.Drawing.Point(12, 93);
            this.datePick.Name = "datePick";
            this.datePick.Size = new System.Drawing.Size(182, 36);
            this.datePick.TabIndex = 3;
            this.datePick.Value = new System.DateTime(2022, 3, 9, 8, 4, 9, 532);
            this.datePick.onValueChanged += new System.EventHandler(this.datePick_onValueChanged);
            // 
            // viewBtn
            // 
            this.viewBtn.AllowToggling = false;
            this.viewBtn.AnimationSpeed = 200;
            this.viewBtn.AutoGenerateColors = false;
            this.viewBtn.BackColor = System.Drawing.Color.Transparent;
            this.viewBtn.BackColor1 = System.Drawing.Color.Transparent;
            this.viewBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("viewBtn.BackgroundImage")));
            this.viewBtn.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.viewBtn.ButtonText = "VIEW";
            this.viewBtn.ButtonTextMarginLeft = 0;
            this.viewBtn.ColorContrastOnClick = 45;
            this.viewBtn.ColorContrastOnHover = 45;
            this.viewBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.viewBtn.CustomizableEdges = borderEdges1;
            this.viewBtn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.viewBtn.DisabledBorderColor = System.Drawing.Color.Empty;
            this.viewBtn.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.viewBtn.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.viewBtn.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.viewBtn.Font = new System.Drawing.Font("Bodoni Bd BT", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewBtn.ForeColor = System.Drawing.Color.White;
            this.viewBtn.IconLeftCursor = System.Windows.Forms.Cursors.Hand;
            this.viewBtn.IconMarginLeft = 11;
            this.viewBtn.IconPadding = 10;
            this.viewBtn.IconRightCursor = System.Windows.Forms.Cursors.Hand;
            this.viewBtn.IdleBorderColor = System.Drawing.Color.DodgerBlue;
            this.viewBtn.IdleBorderRadius = 2;
            this.viewBtn.IdleBorderThickness = 2;
            this.viewBtn.IdleFillColor = System.Drawing.Color.Transparent;
            this.viewBtn.IdleIconLeftImage = null;
            this.viewBtn.IdleIconRightImage = null;
            this.viewBtn.IndicateFocus = false;
            this.viewBtn.Location = new System.Drawing.Point(40, 250);
            this.viewBtn.Name = "viewBtn";
            stateProperties1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties1.BorderRadius = 2;
            stateProperties1.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            stateProperties1.BorderThickness = 2;
            stateProperties1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties1.ForeColor = System.Drawing.Color.White;
            stateProperties1.IconLeftImage = null;
            stateProperties1.IconRightImage = null;
            this.viewBtn.onHoverState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties2.BorderRadius = 2;
            stateProperties2.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            stateProperties2.BorderThickness = 2;
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties2.ForeColor = System.Drawing.Color.White;
            stateProperties2.IconLeftImage = null;
            stateProperties2.IconRightImage = null;
            this.viewBtn.OnPressedState = stateProperties2;
            this.viewBtn.Size = new System.Drawing.Size(131, 39);
            this.viewBtn.TabIndex = 2;
            this.viewBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.viewBtn.TextMarginLeft = 0;
            this.viewBtn.UseDefaultRadiusAndThickness = true;
            this.viewBtn.Click += new System.EventHandler(this.viewBtn_Click);
            // 
            // selectSaleInvoice
            // 
            this.selectSaleInvoice.BackColor = System.Drawing.Color.LightSlateGray;
            this.selectSaleInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.selectSaleInvoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectSaleInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectSaleInvoice.FormattingEnabled = true;
            this.selectSaleInvoice.Location = new System.Drawing.Point(12, 188);
            this.selectSaleInvoice.Name = "selectSaleInvoice";
            this.selectSaleInvoice.Size = new System.Drawing.Size(200, 21);
            this.selectSaleInvoice.TabIndex = 0;
            this.selectSaleInvoice.TextChanged += new System.EventHandler(this.selectSaleInvoice_TextChanged);
            // 
            // sale
            // 
            this.sale.Controls.Add(this.saleGV);
            this.sale.Controls.Add(this.panel4);
            this.sale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sale.Location = new System.Drawing.Point(218, 0);
            this.sale.Name = "sale";
            this.sale.Size = new System.Drawing.Size(646, 509);
            this.sale.TabIndex = 1;
            // 
            // saleGV
            // 
            this.saleGV.AllowCustomTheming = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.saleGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.saleGV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saleGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.saleGV.BackgroundColor = System.Drawing.Color.White;
            this.saleGV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.saleGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.saleGV.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.saleGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.saleGV.ColumnHeadersHeight = 40;
            this.saleGV.CurrentTheme.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.saleGV.CurrentTheme.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.saleGV.CurrentTheme.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
            this.saleGV.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.saleGV.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.saleGV.CurrentTheme.BackColor = System.Drawing.Color.White;
            this.saleGV.CurrentTheme.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.saleGV.CurrentTheme.HeaderStyle.BackColor = System.Drawing.Color.DodgerBlue;
            this.saleGV.CurrentTheme.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.saleGV.CurrentTheme.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.saleGV.CurrentTheme.Name = null;
            this.saleGV.CurrentTheme.RowsStyle.BackColor = System.Drawing.Color.White;
            this.saleGV.CurrentTheme.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.saleGV.CurrentTheme.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.saleGV.CurrentTheme.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.saleGV.CurrentTheme.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.saleGV.DefaultCellStyle = dataGridViewCellStyle3;
            this.saleGV.EnableHeadersVisualStyles = false;
            this.saleGV.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.saleGV.HeaderBackColor = System.Drawing.Color.DodgerBlue;
            this.saleGV.HeaderBgColor = System.Drawing.Color.Empty;
            this.saleGV.HeaderForeColor = System.Drawing.Color.White;
            this.saleGV.Location = new System.Drawing.Point(3, 3);
            this.saleGV.Name = "saleGV";
            this.saleGV.ReadOnly = true;
            this.saleGV.RowHeadersVisible = false;
            this.saleGV.RowTemplate.Height = 40;
            this.saleGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.saleGV.Size = new System.Drawing.Size(643, 432);
            this.saleGV.TabIndex = 0;
            this.saleGV.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.grossLabel);
            this.panel4.Controls.Add(this.bunifuLabel8);
            this.panel4.Location = new System.Drawing.Point(3, 441);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(643, 68);
            this.panel4.TabIndex = 61;
            // 
            // grossLabel
            // 
            this.grossLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grossLabel.AutoEllipsis = false;
            this.grossLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grossLabel.CursorType = null;
            this.grossLabel.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grossLabel.ForeColor = System.Drawing.Color.LightSlateGray;
            this.grossLabel.Location = new System.Drawing.Point(380, 20);
            this.grossLabel.Name = "grossLabel";
            this.grossLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grossLabel.Size = new System.Drawing.Size(58, 46);
            this.grossLabel.TabIndex = 73;
            this.grossLabel.Text = "000";
            this.grossLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.grossLabel.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuLabel8
            // 
            this.bunifuLabel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bunifuLabel8.AutoEllipsis = false;
            this.bunifuLabel8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.bunifuLabel8.CursorType = null;
            this.bunifuLabel8.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuLabel8.ForeColor = System.Drawing.Color.LightSlateGray;
            this.bunifuLabel8.Location = new System.Drawing.Point(147, 7);
            this.bunifuLabel8.Name = "bunifuLabel8";
            this.bunifuLabel8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel8.Size = new System.Drawing.Size(212, 46);
            this.bunifuLabel8.TabIndex = 70;
            this.bunifuLabel8.Text = "Gross Amount:";
            this.bunifuLabel8.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel8.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // SaleInvoiceDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 509);
            this.ControlBox = false;
            this.Controls.Add(this.sale);
            this.Controls.Add(this.panel1);
            this.Name = "SaleInvoiceDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.SaleInvoiceDetail_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.sale.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.saleGV)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox selectSaleInvoice;
        private System.Windows.Forms.FlowLayoutPanel sale;
        private Bunifu.UI.WinForms.BunifuDataGridView saleGV;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton viewBtn;
        private Bunifu.Framework.UI.BunifuDatepicker datePick;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel1;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel2;
        private System.Windows.Forms.Panel panel4;
        private Bunifu.UI.WinForms.BunifuLabel grossLabel;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel8;
        private FontAwesome.Sharp.IconButton iconButton1;
    }
}