
namespace XMLParser
{
    partial class Heist
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
            this.confirm_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.heist_listbox = new System.Windows.Forms.ListBox();
            this.select_heist_text = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // confirm_button
            // 
            this.confirm_button.Location = new System.Drawing.Point(157, 409);
            this.confirm_button.Name = "confirm_button";
            this.confirm_button.Size = new System.Drawing.Size(94, 29);
            this.confirm_button.TabIndex = 0;
            this.confirm_button.Text = "Confirm";
            this.confirm_button.UseVisualStyleBackColor = true;
            this.confirm_button.Click += new System.EventHandler(this.confirm_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(494, 409);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(94, 29);
            this.cancel_button.TabIndex = 1;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // heist_listbox
            // 
            this.heist_listbox.FormattingEnabled = true;
            this.heist_listbox.ItemHeight = 20;
            this.heist_listbox.Location = new System.Drawing.Point(12, 99);
            this.heist_listbox.Name = "heist_listbox";
            this.heist_listbox.Size = new System.Drawing.Size(776, 304);
            this.heist_listbox.TabIndex = 2;
            this.heist_listbox.SelectedIndexChanged += new System.EventHandler(this.heist_listbox_SelectedIndexChanged);
            this.heist_listbox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.heist_listbox_MouseDoubleClick);
            // 
            // select_heist_text
            // 
            this.select_heist_text.AutoSize = true;
            this.select_heist_text.Location = new System.Drawing.Point(12, 9);
            this.select_heist_text.Name = "select_heist_text";
            this.select_heist_text.Size = new System.Drawing.Size(632, 20);
            this.select_heist_text.TabIndex = 3;
            this.select_heist_text.Text = "Select heist in the dropdown menu and press \"Confirm\" or double click the mission" +
    " to confirm.";
            // 
            // Heist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.select_heist_text);
            this.Controls.Add(this.heist_listbox);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.confirm_button);
            this.MaximizeBox = false;
            this.Name = "Heist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Heist";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button confirm_button;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.ListBox heist_listbox;
        private System.Windows.Forms.Label select_heist_text;
    }
}