﻿namespace GHOST_28147_89
{
    partial class Encryption
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.path = new System.Windows.Forms.TextBox();
            this.openFileButton = new System.Windows.Forms.Button();
            this.openText = new System.Windows.Forms.TextBox();
            this.closeText = new System.Windows.Forms.TextBox();
            this.encryptionButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.key = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.initialVector = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.completeSaving = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // path
            // 
            this.path.Location = new System.Drawing.Point(12, 31);
            this.path.Name = "path";
            this.path.ReadOnly = true;
            this.path.Size = new System.Drawing.Size(352, 20);
            this.path.TabIndex = 0;
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(380, 31);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(116, 23);
            this.openFileButton.TabIndex = 1;
            this.openFileButton.Text = "Выбрать";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // openText
            // 
            this.openText.Location = new System.Drawing.Point(12, 164);
            this.openText.Multiline = true;
            this.openText.Name = "openText";
            this.openText.Size = new System.Drawing.Size(239, 187);
            this.openText.TabIndex = 2;
            this.openText.TextChanged += new System.EventHandler(this.openText_TextChanged);
            // 
            // closeText
            // 
            this.closeText.Enabled = false;
            this.closeText.Location = new System.Drawing.Point(257, 164);
            this.closeText.Multiline = true;
            this.closeText.Name = "closeText";
            this.closeText.Size = new System.Drawing.Size(239, 187);
            this.closeText.TabIndex = 3;
            this.closeText.TextChanged += new System.EventHandler(this.closeText_TextChanged);
            // 
            // encryptionButton
            // 
            this.encryptionButton.Enabled = false;
            this.encryptionButton.Location = new System.Drawing.Point(12, 369);
            this.encryptionButton.Name = "encryptionButton";
            this.encryptionButton.Size = new System.Drawing.Size(239, 23);
            this.encryptionButton.TabIndex = 4;
            this.encryptionButton.Text = "Зашифровать";
            this.encryptionButton.UseVisualStyleBackColor = true;
            this.encryptionButton.Click += new System.EventHandler(this.encryptButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(260, 369);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(239, 23);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Выберите файл";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Открытый текст";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(257, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Шифрованный текст";
            // 
            // key
            // 
            this.key.Location = new System.Drawing.Point(12, 90);
            this.key.Name = "key";
            this.key.Size = new System.Drawing.Size(352, 20);
            this.key.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Ключ";
            // 
            // initialVector
            // 
            this.initialVector.Location = new System.Drawing.Point(380, 90);
            this.initialVector.Name = "initialVector";
            this.initialVector.Size = new System.Drawing.Size(119, 20);
            this.initialVector.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(377, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Начальный вектор (int)";
            // 
            // completeSaving
            // 
            this.completeSaving.AutoSize = true;
            this.completeSaving.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.completeSaving.ForeColor = System.Drawing.SystemColors.Highlight;
            this.completeSaving.Location = new System.Drawing.Point(419, 145);
            this.completeSaving.Name = "completeSaving";
            this.completeSaving.Size = new System.Drawing.Size(77, 13);
            this.completeSaving.TabIndex = 13;
            this.completeSaving.Text = "COMPLETE!";
            this.completeSaving.Visible = false;
            // 
            // Encryption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 411);
            this.Controls.Add(this.completeSaving);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.initialVector);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.key);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.encryptionButton);
            this.Controls.Add(this.closeText);
            this.Controls.Add(this.openText);
            this.Controls.Add(this.openFileButton);
            this.Controls.Add(this.path);
            this.Name = "Encryption";
            this.Text = "Encryption";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox path;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.TextBox openText;
        private System.Windows.Forms.TextBox closeText;
        private System.Windows.Forms.Button encryptionButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        protected System.Windows.Forms.TextBox key;
        private System.Windows.Forms.Label label4;
        protected System.Windows.Forms.TextBox initialVector;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label completeSaving;
    }
}

