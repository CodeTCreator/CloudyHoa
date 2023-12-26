namespace WinformsClient_Admin_.AddingHoaWindows
{
    partial class AddingHoaMW
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
            this.textEditName = new DevExpress.XtraEditors.TextEdit();
            this.labelControlName = new DevExpress.XtraEditors.LabelControl();
            this.textEditPassword = new DevExpress.XtraEditors.TextEdit();
            this.labelControlPassword = new DevExpress.XtraEditors.LabelControl();
            this.labelControlNameWindow = new DevExpress.XtraEditors.LabelControl();
            this.universalButton = new DevExpress.XtraEditors.SimpleButton();
            this.backButton = new DevExpress.XtraEditors.SimpleButton();
            this.attentionLabelName = new DevExpress.XtraEditors.LabelControl();
            this.attentionLabelPass = new DevExpress.XtraEditors.LabelControl();
            this.textEditLogin = new DevExpress.XtraEditors.TextEdit();
            this.loginLabel = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLogin.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textEditName
            // 
            this.textEditName.Location = new System.Drawing.Point(259, 100);
            this.textEditName.Margin = new System.Windows.Forms.Padding(4);
            this.textEditName.Name = "textEditName";
            this.textEditName.Properties.Appearance.Font = new System.Drawing.Font("Sitka Banner", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textEditName.Properties.Appearance.Options.UseFont = true;
            this.textEditName.Size = new System.Drawing.Size(200, 30);
            this.textEditName.TabIndex = 5;
            this.textEditName.EditValueChanged += new System.EventHandler(this.textEditName_EditValueChanged);
            // 
            // labelControlName
            // 
            this.labelControlName.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControlName.Appearance.Options.UseFont = true;
            this.labelControlName.Location = new System.Drawing.Point(37, 103);
            this.labelControlName.Margin = new System.Windows.Forms.Padding(4);
            this.labelControlName.Name = "labelControlName";
            this.labelControlName.Size = new System.Drawing.Size(143, 28);
            this.labelControlName.TabIndex = 4;
            this.labelControlName.Text = "Название УК:";
            // 
            // textEditPassword
            // 
            this.textEditPassword.Location = new System.Drawing.Point(258, 238);
            this.textEditPassword.Margin = new System.Windows.Forms.Padding(4);
            this.textEditPassword.Name = "textEditPassword";
            this.textEditPassword.Properties.Appearance.Font = new System.Drawing.Font("Sitka Banner", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textEditPassword.Properties.Appearance.Options.UseFont = true;
            this.textEditPassword.Size = new System.Drawing.Size(200, 30);
            this.textEditPassword.TabIndex = 7;
            this.textEditPassword.EditValueChanged += new System.EventHandler(this.textEditPassword_EditValueChanged);
            // 
            // labelControlPassword
            // 
            this.labelControlPassword.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControlPassword.Appearance.Options.UseFont = true;
            this.labelControlPassword.Location = new System.Drawing.Point(46, 237);
            this.labelControlPassword.Margin = new System.Windows.Forms.Padding(4);
            this.labelControlPassword.Name = "labelControlPassword";
            this.labelControlPassword.Size = new System.Drawing.Size(86, 28);
            this.labelControlPassword.TabIndex = 6;
            this.labelControlPassword.Text = "Пароль:";
            // 
            // labelControlNameWindow
            // 
            this.labelControlNameWindow.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControlNameWindow.Appearance.Options.UseFont = true;
            this.labelControlNameWindow.Location = new System.Drawing.Point(92, 29);
            this.labelControlNameWindow.Name = "labelControlNameWindow";
            this.labelControlNameWindow.Size = new System.Drawing.Size(299, 28);
            this.labelControlNameWindow.TabIndex = 8;
            this.labelControlNameWindow.Text = "Добавление учетной записи";
            // 
            // universalButton
            // 
            this.universalButton.Location = new System.Drawing.Point(258, 308);
            this.universalButton.Name = "universalButton";
            this.universalButton.Size = new System.Drawing.Size(94, 29);
            this.universalButton.TabIndex = 9;
            this.universalButton.Text = "Добавить";
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(364, 308);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(94, 29);
            this.backButton.TabIndex = 10;
            this.backButton.Text = "Назад";
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // attentionLabelName
            // 
            this.attentionLabelName.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.attentionLabelName.Appearance.ForeColor = System.Drawing.Color.Red;
            this.attentionLabelName.Appearance.Options.UseFont = true;
            this.attentionLabelName.Appearance.Options.UseForeColor = true;
            this.attentionLabelName.Location = new System.Drawing.Point(262, 137);
            this.attentionLabelName.Name = "attentionLabelName";
            this.attentionLabelName.Size = new System.Drawing.Size(197, 16);
            this.attentionLabelName.TabIndex = 11;
            this.attentionLabelName.Text = "Такое название уже существует!";
            this.attentionLabelName.Visible = false;
            // 
            // attentionLabelPass
            // 
            this.attentionLabelPass.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.attentionLabelPass.Appearance.ForeColor = System.Drawing.Color.Red;
            this.attentionLabelPass.Appearance.Options.UseFont = true;
            this.attentionLabelPass.Appearance.Options.UseForeColor = true;
            this.attentionLabelPass.Location = new System.Drawing.Point(258, 275);
            this.attentionLabelPass.Name = "attentionLabelPass";
            this.attentionLabelPass.Size = new System.Drawing.Size(195, 16);
            this.attentionLabelPass.TabIndex = 12;
            this.attentionLabelPass.Text = "Необходимо больше 7 символов!";
            this.attentionLabelPass.Visible = false;
            // 
            // textEditLogin
            // 
            this.textEditLogin.Location = new System.Drawing.Point(258, 174);
            this.textEditLogin.Margin = new System.Windows.Forms.Padding(4);
            this.textEditLogin.Name = "textEditLogin";
            this.textEditLogin.Properties.Appearance.Font = new System.Drawing.Font("Sitka Banner", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textEditLogin.Properties.Appearance.Options.UseFont = true;
            this.textEditLogin.Size = new System.Drawing.Size(200, 30);
            this.textEditLogin.TabIndex = 14;
            // 
            // loginLabel
            // 
            this.loginLabel.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loginLabel.Appearance.Options.UseFont = true;
            this.loginLabel.Location = new System.Drawing.Point(60, 173);
            this.loginLabel.Margin = new System.Windows.Forms.Padding(4);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(72, 28);
            this.loginLabel.TabIndex = 13;
            this.loginLabel.Text = "Логин:";
            // 
            // AddingHoaMW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 342);
            this.Controls.Add(this.textEditLogin);
            this.Controls.Add(this.loginLabel);
            this.Controls.Add(this.attentionLabelPass);
            this.Controls.Add(this.attentionLabelName);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.universalButton);
            this.Controls.Add(this.labelControlNameWindow);
            this.Controls.Add(this.textEditPassword);
            this.Controls.Add(this.labelControlPassword);
            this.Controls.Add(this.textEditName);
            this.Controls.Add(this.labelControlName);
            this.Name = "AddingHoaMW";
            this.Text = "Учетная запись";
            this.Load += new System.EventHandler(this.AddingHoaMW_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLogin.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textEditName;
        private DevExpress.XtraEditors.LabelControl labelControlName;
        private DevExpress.XtraEditors.TextEdit textEditPassword;
        private DevExpress.XtraEditors.LabelControl labelControlPassword;
        private DevExpress.XtraEditors.LabelControl labelControlNameWindow;
        private DevExpress.XtraEditors.SimpleButton universalButton;
        private DevExpress.XtraEditors.SimpleButton backButton;
        private DevExpress.XtraEditors.LabelControl attentionLabelName;
        private DevExpress.XtraEditors.LabelControl attentionLabelPass;
        private DevExpress.XtraEditors.TextEdit textEditLogin;
        private DevExpress.XtraEditors.LabelControl loginLabel;
    }
}