namespace WinformsClient_Admin_
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
            this.accountButton = new DevExpress.XtraEditors.SimpleButton();
            this.objectsButton = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // accountButton
            // 
            this.accountButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.accountButton.Location = new System.Drawing.Point(42, 111);
            this.accountButton.Name = "accountButton";
            this.accountButton.Size = new System.Drawing.Size(177, 71);
            this.accountButton.TabIndex = 0;
            this.accountButton.Text = "Аккаунты УК";
            this.accountButton.Click += new System.EventHandler(this.accountButton_Click);
            // 
            // objectsButton
            // 
            this.objectsButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.ImageOptions.Image")));
            this.objectsButton.Location = new System.Drawing.Point(258, 111);
            this.objectsButton.Name = "objectsButton";
            this.objectsButton.Size = new System.Drawing.Size(177, 71);
            this.objectsButton.TabIndex = 1;
            this.objectsButton.Text = "Объекты УК";
            this.objectsButton.Click += new System.EventHandler(this.objectsButton_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(155, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(158, 28);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Главный экран";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 320);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.objectsButton);
            this.Controls.Add(this.accountButton);
            this.Name = "Form1";
            this.Text = "Главный экран";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton accountButton;
        private DevExpress.XtraEditors.SimpleButton objectsButton;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}

