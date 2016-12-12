namespace RetroTicker {
    partial class CredentialsForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.nickLabel = new System.Windows.Forms.Label();
            this.nickTextBox = new System.Windows.Forms.TextBox();
            this.oauthLabel = new System.Windows.Forms.Label();
            this.channelLabel = new System.Windows.Forms.Label();
            this.oauthTextBox = new System.Windows.Forms.TextBox();
            this.channelTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.serverTextBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.serverLabel = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // nickLabel
            // 
            this.nickLabel.AutoSize = true;
            this.nickLabel.Location = new System.Drawing.Point(12, 61);
            this.nickLabel.Name = "nickLabel";
            this.nickLabel.Size = new System.Drawing.Size(27, 13);
            this.nickLabel.TabIndex = 0;
            this.nickLabel.Text = "nick";
            // 
            // nickTextBox
            // 
            this.nickTextBox.Location = new System.Drawing.Point(53, 58);
            this.nickTextBox.Name = "nickTextBox";
            this.nickTextBox.Size = new System.Drawing.Size(218, 20);
            this.nickTextBox.TabIndex = 3;
            // 
            // oauthLabel
            // 
            this.oauthLabel.AutoSize = true;
            this.oauthLabel.Location = new System.Drawing.Point(12, 88);
            this.oauthLabel.Name = "oauthLabel";
            this.oauthLabel.Size = new System.Drawing.Size(34, 13);
            this.oauthLabel.TabIndex = 2;
            this.oauthLabel.Text = "oauth";
            // 
            // channelLabel
            // 
            this.channelLabel.AutoSize = true;
            this.channelLabel.Location = new System.Drawing.Point(12, 115);
            this.channelLabel.Name = "channelLabel";
            this.channelLabel.Size = new System.Drawing.Size(31, 13);
            this.channelLabel.TabIndex = 3;
            this.channelLabel.Text = "chan";
            // 
            // oauthTextBox
            // 
            this.oauthTextBox.Location = new System.Drawing.Point(52, 85);
            this.oauthTextBox.Name = "oauthTextBox";
            this.oauthTextBox.Size = new System.Drawing.Size(219, 20);
            this.oauthTextBox.TabIndex = 4;
            // 
            // channelTextBox
            // 
            this.channelTextBox.Location = new System.Drawing.Point(52, 112);
            this.channelTextBox.Name = "channelTextBox";
            this.channelTextBox.Size = new System.Drawing.Size(219, 20);
            this.channelTextBox.TabIndex = 5;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(15, 138);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(256, 36);
            this.saveButton.TabIndex = 99;
            this.saveButton.Text = "save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // serverTextBox
            // 
            this.serverTextBox.Location = new System.Drawing.Point(53, 6);
            this.serverTextBox.Name = "serverTextBox";
            this.serverTextBox.Size = new System.Drawing.Size(218, 20);
            this.serverTextBox.TabIndex = 1;
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(12, 35);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(25, 13);
            this.portLabel.TabIndex = 9;
            this.portLabel.Text = "port";
            // 
            // serverLabel
            // 
            this.serverLabel.AutoSize = true;
            this.serverLabel.Location = new System.Drawing.Point(12, 9);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(36, 13);
            this.serverLabel.TabIndex = 10;
            this.serverLabel.Text = "server";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(53, 32);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(218, 20);
            this.portTextBox.TabIndex = 2;
            // 
            // CredentialsForm
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 182);
            this.Controls.Add(this.serverLabel);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.serverTextBox);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.channelTextBox);
            this.Controls.Add(this.oauthTextBox);
            this.Controls.Add(this.channelLabel);
            this.Controls.Add(this.oauthLabel);
            this.Controls.Add(this.nickTextBox);
            this.Controls.Add(this.nickLabel);
            this.Name = "CredentialsForm";
            this.Text = "set bot credentials";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nickLabel;
        private System.Windows.Forms.TextBox nickTextBox;
        private System.Windows.Forms.Label oauthLabel;
        private System.Windows.Forms.Label channelLabel;
        private System.Windows.Forms.TextBox oauthTextBox;
        private System.Windows.Forms.TextBox channelTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox serverTextBox;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label serverLabel;
        private System.Windows.Forms.TextBox portTextBox;
    }
}