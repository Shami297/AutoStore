namespace AutoStore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            this.userbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pwdbox = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.bunifuButton1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.circularButton2 = new AutoStore.CircularButton();
            this.circularButton1 = new AutoStore.CircularButton();
            this.SuspendLayout();
            // 
            // userbox
            // 
            this.userbox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.userbox.BackColor = System.Drawing.SystemColors.MenuText;
            this.userbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userbox.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userbox.ForeColor = System.Drawing.SystemColors.Window;
            this.userbox.Location = new System.Drawing.Point(460, 275);
            this.userbox.MaxLength = 6;
            this.userbox.Name = "userbox";
            this.userbox.Size = new System.Drawing.Size(197, 22);
            this.userbox.TabIndex = 0;
            this.userbox.Leave += new System.EventHandler(this.userbox_Leave);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Britannic Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Purple;
            this.label1.Location = new System.Drawing.Point(455, 251);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "User Name";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Britannic Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Purple;
            this.label2.Location = new System.Drawing.Point(456, 314);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // pwdbox
            // 
            this.pwdbox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pwdbox.BackColor = System.Drawing.SystemColors.MenuText;
            this.pwdbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pwdbox.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwdbox.ForeColor = System.Drawing.SystemColors.Window;
            this.pwdbox.Location = new System.Drawing.Point(461, 338);
            this.pwdbox.MaxLength = 20;
            this.pwdbox.Name = "pwdbox";
            this.pwdbox.Size = new System.Drawing.Size(197, 22);
            this.pwdbox.TabIndex = 1;
            this.pwdbox.UseSystemPasswordChar = true;
            this.pwdbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pwdbox_KeyPress);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(543, 361);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(122, 21);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Show Password";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // bunifuButton1
            // 
            this.bunifuButton1.AllowToggling = false;
            this.bunifuButton1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.bunifuButton1.AnimationSpeed = 200;
            this.bunifuButton1.AutoGenerateColors = false;
            this.bunifuButton1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuButton1.BackColor1 = System.Drawing.Color.White;
            this.bunifuButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuButton1.BackgroundImage")));
            this.bunifuButton1.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Dash;
            this.bunifuButton1.ButtonText = "Are You Forget Password ?";
            this.bunifuButton1.ButtonTextMarginLeft = 0;
            this.bunifuButton1.ColorContrastOnClick = 45;
            this.bunifuButton1.ColorContrastOnHover = 45;
            this.bunifuButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.bunifuButton1.CustomizableEdges = borderEdges1;
            this.bunifuButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.bunifuButton1.DisabledBorderColor = System.Drawing.Color.Empty;
            this.bunifuButton1.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.bunifuButton1.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.bunifuButton1.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.bunifuButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.bunifuButton1.ForeColor = System.Drawing.Color.Blue;
            this.bunifuButton1.IconLeftCursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuButton1.IconMarginLeft = 11;
            this.bunifuButton1.IconPadding = 10;
            this.bunifuButton1.IconRightCursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuButton1.IdleBorderColor = System.Drawing.Color.White;
            this.bunifuButton1.IdleBorderRadius = 3;
            this.bunifuButton1.IdleBorderThickness = 1;
            this.bunifuButton1.IdleFillColor = System.Drawing.Color.White;
            this.bunifuButton1.IdleIconLeftImage = null;
            this.bunifuButton1.IdleIconRightImage = null;
            this.bunifuButton1.IndicateFocus = false;
            this.bunifuButton1.Location = new System.Drawing.Point(459, 490);
            this.bunifuButton1.Name = "bunifuButton1";
            stateProperties1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties1.BorderRadius = 3;
            stateProperties1.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            stateProperties1.BorderThickness = 1;
            stateProperties1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties1.ForeColor = System.Drawing.Color.White;
            stateProperties1.IconLeftImage = null;
            stateProperties1.IconRightImage = null;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties2.BorderRadius = 3;
            stateProperties2.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            stateProperties2.BorderThickness = 1;
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties2.ForeColor = System.Drawing.Color.White;
            stateProperties2.IconLeftImage = null;
            stateProperties2.IconRightImage = null;
            this.bunifuButton1.Size = new System.Drawing.Size(210, 45);
            this.bunifuButton1.TabIndex = 5;
            this.bunifuButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuButton1.TextMarginLeft = 0;
            this.bunifuButton1.UseDefaultRadiusAndThickness = true;
            this.bunifuButton1.Click += new System.EventHandler(this.bunifuButton1_Click);
            // 
            // circularButton2
            // 
            this.circularButton2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.circularButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.circularButton2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.circularButton2.FlatAppearance.BorderSize = 0;
            this.circularButton2.Font = new System.Drawing.Font("Britannic Bold", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.circularButton2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.circularButton2.Location = new System.Drawing.Point(584, 386);
            this.circularButton2.Name = "circularButton2";
            this.circularButton2.Size = new System.Drawing.Size(73, 69);
            this.circularButton2.TabIndex = 4;
            this.circularButton2.Text = "Cancel";
            this.circularButton2.UseVisualStyleBackColor = false;
            this.circularButton2.Click += new System.EventHandler(this.circularButton2_Click);
            // 
            // circularButton1
            // 
            this.circularButton1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.circularButton1.BackColor = System.Drawing.Color.Green;
            this.circularButton1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.circularButton1.FlatAppearance.BorderSize = 0;
            this.circularButton1.Font = new System.Drawing.Font("Britannic Bold", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.circularButton1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.circularButton1.Location = new System.Drawing.Point(459, 386);
            this.circularButton1.Name = "circularButton1";
            this.circularButton1.Size = new System.Drawing.Size(73, 69);
            this.circularButton1.TabIndex = 3;
            this.circularButton1.Text = "SignIn";
            this.circularButton1.UseVisualStyleBackColor = false;
            this.circularButton1.Click += new System.EventHandler(this.CircularButton1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AutoStore.Properties.Resources.Login;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.ControlBox = false;
            this.Controls.Add(this.bunifuButton1);
            this.Controls.Add(this.circularButton2);
            this.Controls.Add(this.circularButton1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pwdbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.userbox);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox userbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pwdbox;
        private System.Windows.Forms.CheckBox checkBox1;
        private CircularButton circularButton1;
        private CircularButton circularButton2;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton bunifuButton1;
    }
}

