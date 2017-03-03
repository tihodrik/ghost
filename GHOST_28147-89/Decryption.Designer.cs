namespace GHOST_28147_89
{
    partial class Decryption
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
            this.label5 = new System.Windows.Forms.Label();
            this.initialVector = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.key = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.decryptionButton = new System.Windows.Forms.Button();
            this.openText = new System.Windows.Forms.TextBox();
            this.closeText = new System.Windows.Forms.TextBox();
            this.openFileButton = new System.Windows.Forms.Button();
            this.path = new System.Windows.Forms.TextBox();
            this.copyButton = new System.Windows.Forms.Button();
            this.completeSaving = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(377, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Начальный вектор (int)";
            // 
            // initialVector
            // 
            this.initialVector.Location = new System.Drawing.Point(380, 93);
            this.initialVector.Name = "initialVector";
            this.initialVector.Size = new System.Drawing.Size(119, 20);
            this.initialVector.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Ключ";
            // 
            // key
            // 
            this.key.Location = new System.Drawing.Point(12, 93);
            this.key.Name = "key";
            this.key.Size = new System.Drawing.Size(352, 20);
            this.key.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(257, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Открытый текст";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Шифротекст";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Выберите файл";
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(260, 397);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(239, 23);
            this.saveButton.TabIndex = 18;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // decryptionButton
            // 
            this.decryptionButton.Enabled = false;
            this.decryptionButton.Location = new System.Drawing.Point(12, 397);
            this.decryptionButton.Name = "decryptionButton";
            this.decryptionButton.Size = new System.Drawing.Size(239, 23);
            this.decryptionButton.TabIndex = 17;
            this.decryptionButton.Text = "Расшифровать";
            this.decryptionButton.UseVisualStyleBackColor = true;
            this.decryptionButton.Click += new System.EventHandler(this.decryptionButton_Click);
            // 
            // openText
            // 
            this.openText.Enabled = false;
            this.openText.Location = new System.Drawing.Point(257, 192);
            this.openText.Multiline = true;
            this.openText.Name = "openText";
            this.openText.Size = new System.Drawing.Size(239, 187);
            this.openText.TabIndex = 16;
            this.openText.TextChanged += new System.EventHandler(this.openText_TextChanged);
            // 
            // closeText
            // 
            this.closeText.Location = new System.Drawing.Point(12, 192);
            this.closeText.Multiline = true;
            this.closeText.Name = "closeText";
            this.closeText.Size = new System.Drawing.Size(239, 187);
            this.closeText.TabIndex = 15;
            this.closeText.TextChanged += new System.EventHandler(this.closeText_TextChanged);
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(380, 32);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(116, 23);
            this.openFileButton.TabIndex = 14;
            this.openFileButton.Text = "Выбрать";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // path
            // 
            this.path.Location = new System.Drawing.Point(12, 34);
            this.path.Name = "path";
            this.path.ReadOnly = true;
            this.path.Size = new System.Drawing.Size(352, 20);
            this.path.TabIndex = 13;
            // 
            // copyButton
            // 
            this.copyButton.Location = new System.Drawing.Point(12, 133);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(484, 23);
            this.copyButton.TabIndex = 1;
            this.copyButton.Text = "Скопировать данные";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.copy_Click);
            // 
            // completeSaving
            // 
            this.completeSaving.AutoSize = true;
            this.completeSaving.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.completeSaving.ForeColor = System.Drawing.SystemColors.Highlight;
            this.completeSaving.Location = new System.Drawing.Point(419, 173);
            this.completeSaving.Name = "completeSaving";
            this.completeSaving.Size = new System.Drawing.Size(77, 13);
            this.completeSaving.TabIndex = 26;
            this.completeSaving.Text = "COMPLETE!";
            this.completeSaving.Visible = false;
            // 
            // Decryption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 434);
            this.Controls.Add(this.completeSaving);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.initialVector);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.key);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.decryptionButton);
            this.Controls.Add(this.openText);
            this.Controls.Add(this.closeText);
            this.Controls.Add(this.openFileButton);
            this.Controls.Add(this.path);
            this.Name = "Decryption";
            this.Text = "Decryption";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox initialVector;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox key;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button decryptionButton;
        private System.Windows.Forms.TextBox openText;
        private System.Windows.Forms.TextBox closeText;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.TextBox path;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.Label completeSaving;
    }
}