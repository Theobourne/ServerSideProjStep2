namespace _408ProjectStep1_server
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
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.button_listen = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_browse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(147, 98);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(295, 26);
            this.textBox_port.TabIndex = 0;
            // 
            // button_listen
            // 
            this.button_listen.Enabled = false;
            this.button_listen.Location = new System.Drawing.Point(459, 93);
            this.button_listen.Name = "button_listen";
            this.button_listen.Size = new System.Drawing.Size(104, 37);
            this.button_listen.TabIndex = 1;
            this.button_listen.Text = "Listen";
            this.button_listen.UseVisualStyleBackColor = true;
            this.button_listen.Click += new System.EventHandler(this.button_listen_Click);
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(68, 157);
            this.logs.Name = "logs";
            this.logs.Size = new System.Drawing.Size(495, 260);
            this.logs.TabIndex = 2;
            this.logs.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(98, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "Port";
            // 
            // textBox_path
            // 
            this.textBox_path.Enabled = false;
            this.textBox_path.Location = new System.Drawing.Point(147, 33);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.Size = new System.Drawing.Size(295, 26);
            this.textBox_path.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(26, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 22);
            this.label2.TabIndex = 6;
            this.label2.Text = "Storage Path";
            // 
            // button_browse
            // 
            this.button_browse.Location = new System.Drawing.Point(459, 28);
            this.button_browse.Name = "button_browse";
            this.button_browse.Size = new System.Drawing.Size(104, 37);
            this.button_browse.TabIndex = 7;
            this.button_browse.Text = "Browse";
            this.button_browse.UseVisualStyleBackColor = true;
            this.button_browse.Click += new System.EventHandler(this.button_browse_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 450);
            this.Controls.Add(this.button_browse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_path);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.button_listen);
            this.Controls.Add(this.textBox_port);
            this.Name = "Form1";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Button button_listen;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_path;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_browse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

