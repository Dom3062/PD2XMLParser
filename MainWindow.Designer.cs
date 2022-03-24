
namespace XMLParser
{
    partial class MainWindow
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.openfile_button = new System.Windows.Forms.Button();
            this.convert_button = new System.Windows.Forms.Button();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.errors_listbox = new System.Windows.Forms.ListBox();
            this.error_text = new System.Windows.Forms.Label();
            this.extract_all_radiobutton = new System.Windows.Forms.RadioButton();
            this.extract_heist_radiobutton = new System.Windows.Forms.RadioButton();
            this.extract_type_group = new System.Windows.Forms.GroupBox();
            this.reset_button = new System.Windows.Forms.Button();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.debug_button = new System.Windows.Forms.Button();
            this.save_errors_button = new System.Windows.Forms.Button();
            this.fbd_save = new System.Windows.Forms.FolderBrowserDialog();
            this.heist_group = new System.Windows.Forms.GroupBox();
            this.map_script_and_instances_radiobutton = new System.Windows.Forms.RadioButton();
            this.map_script_only_radiobutton = new System.Windows.Forms.RadioButton();
            this.progress_text = new System.Windows.Forms.Label();
            this.app_version_text = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.progress_total = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.extract_type_group.SuspendLayout();
            this.heist_group.SuspendLayout();
            this.SuspendLayout();
            // 
            // openfile_button
            // 
            this.openfile_button.Location = new System.Drawing.Point(12, 15);
            this.openfile_button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.openfile_button.Name = "openfile_button";
            this.openfile_button.Size = new System.Drawing.Size(75, 41);
            this.openfile_button.TabIndex = 0;
            this.openfile_button.Text = "Open";
            this.openfile_button.UseVisualStyleBackColor = true;
            this.openfile_button.Click += new System.EventHandler(this.openfile_button_Click);
            // 
            // convert_button
            // 
            this.convert_button.Enabled = false;
            this.convert_button.Location = new System.Drawing.Point(93, 15);
            this.convert_button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.convert_button.Name = "convert_button";
            this.convert_button.Size = new System.Drawing.Size(75, 41);
            this.convert_button.TabIndex = 1;
            this.convert_button.Text = "Convert";
            this.convert_button.UseVisualStyleBackColor = true;
            this.convert_button.Click += new System.EventHandler(this.convert_button_Click);
            // 
            // ofd
            // 
            this.ofd.Filter = "Soubory XML|*.xml|Diesel Format|*.mission;*.continent";
            this.ofd.Title = "Vyber XML soubor";
            // 
            // errors_listbox
            // 
            this.errors_listbox.FormattingEnabled = true;
            this.errors_listbox.ItemHeight = 20;
            this.errors_listbox.Location = new System.Drawing.Point(12, 255);
            this.errors_listbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.errors_listbox.Name = "errors_listbox";
            this.errors_listbox.Size = new System.Drawing.Size(995, 444);
            this.errors_listbox.TabIndex = 2;
            // 
            // error_text
            // 
            this.error_text.AutoSize = true;
            this.error_text.Location = new System.Drawing.Point(12, 235);
            this.error_text.Name = "error_text";
            this.error_text.Size = new System.Drawing.Size(50, 20);
            this.error_text.TabIndex = 3;
            this.error_text.Text = "Errors:";
            // 
            // extract_all_radiobutton
            // 
            this.extract_all_radiobutton.AutoSize = true;
            this.extract_all_radiobutton.Checked = true;
            this.extract_all_radiobutton.Location = new System.Drawing.Point(6, 26);
            this.extract_all_radiobutton.Name = "extract_all_radiobutton";
            this.extract_all_radiobutton.Size = new System.Drawing.Size(48, 24);
            this.extract_all_radiobutton.TabIndex = 5;
            this.extract_all_radiobutton.TabStop = true;
            this.extract_all_radiobutton.Text = "All";
            this.extract_all_radiobutton.UseVisualStyleBackColor = true;
            // 
            // extract_heist_radiobutton
            // 
            this.extract_heist_radiobutton.AutoSize = true;
            this.extract_heist_radiobutton.Location = new System.Drawing.Point(6, 56);
            this.extract_heist_radiobutton.Name = "extract_heist_radiobutton";
            this.extract_heist_radiobutton.Size = new System.Drawing.Size(64, 24);
            this.extract_heist_radiobutton.TabIndex = 6;
            this.extract_heist_radiobutton.Text = "Heist";
            this.extract_heist_radiobutton.UseVisualStyleBackColor = true;
            this.extract_heist_radiobutton.CheckedChanged += new System.EventHandler(this.extract_heist_radiobutton_CheckedChanged);
            // 
            // extract_type_group
            // 
            this.extract_type_group.Controls.Add(this.extract_all_radiobutton);
            this.extract_type_group.Controls.Add(this.extract_heist_radiobutton);
            this.extract_type_group.Location = new System.Drawing.Point(174, 12);
            this.extract_type_group.Name = "extract_type_group";
            this.extract_type_group.Size = new System.Drawing.Size(236, 117);
            this.extract_type_group.TabIndex = 7;
            this.extract_type_group.TabStop = false;
            this.extract_type_group.Text = "Extract:";
            // 
            // reset_button
            // 
            this.reset_button.Enabled = false;
            this.reset_button.Location = new System.Drawing.Point(12, 63);
            this.reset_button.Name = "reset_button";
            this.reset_button.Size = new System.Drawing.Size(94, 29);
            this.reset_button.TabIndex = 9;
            this.reset_button.Text = "Reset";
            this.reset_button.UseVisualStyleBackColor = true;
            this.reset_button.Click += new System.EventHandler(this.reset_button_Click);
            // 
            // debug_button
            // 
            this.debug_button.Location = new System.Drawing.Point(866, 12);
            this.debug_button.Name = "debug_button";
            this.debug_button.Size = new System.Drawing.Size(141, 29);
            this.debug_button.TabIndex = 10;
            this.debug_button.Text = "Debug Instance";
            this.debug_button.UseVisualStyleBackColor = true;
            this.debug_button.Click += new System.EventHandler(this.debug_button_Click);
            // 
            // save_errors_button
            // 
            this.save_errors_button.Location = new System.Drawing.Point(12, 98);
            this.save_errors_button.Name = "save_errors_button";
            this.save_errors_button.Size = new System.Drawing.Size(140, 29);
            this.save_errors_button.TabIndex = 11;
            this.save_errors_button.Text = "Save Errors to file";
            this.save_errors_button.UseVisualStyleBackColor = true;
            this.save_errors_button.Click += new System.EventHandler(this.save_errors_button_Click);
            // 
            // heist_group
            // 
            this.heist_group.Controls.Add(this.map_script_and_instances_radiobutton);
            this.heist_group.Controls.Add(this.map_script_only_radiobutton);
            this.heist_group.Location = new System.Drawing.Point(416, 12);
            this.heist_group.Name = "heist_group";
            this.heist_group.Size = new System.Drawing.Size(225, 83);
            this.heist_group.TabIndex = 12;
            this.heist_group.TabStop = false;
            this.heist_group.Text = "Heist:";
            // 
            // map_script_and_instances_radiobutton
            // 
            this.map_script_and_instances_radiobutton.AutoSize = true;
            this.map_script_and_instances_radiobutton.Enabled = false;
            this.map_script_and_instances_radiobutton.Location = new System.Drawing.Point(6, 56);
            this.map_script_and_instances_radiobutton.Name = "map_script_and_instances_radiobutton";
            this.map_script_and_instances_radiobutton.Size = new System.Drawing.Size(180, 24);
            this.map_script_and_instances_radiobutton.TabIndex = 1;
            this.map_script_and_instances_radiobutton.Text = "Map Script + Instances";
            this.map_script_and_instances_radiobutton.UseVisualStyleBackColor = true;
            // 
            // map_script_only_radiobutton
            // 
            this.map_script_only_radiobutton.AutoSize = true;
            this.map_script_only_radiobutton.Checked = true;
            this.map_script_only_radiobutton.Enabled = false;
            this.map_script_only_radiobutton.Location = new System.Drawing.Point(6, 26);
            this.map_script_only_radiobutton.Name = "map_script_only_radiobutton";
            this.map_script_only_radiobutton.Size = new System.Drawing.Size(136, 24);
            this.map_script_only_radiobutton.TabIndex = 0;
            this.map_script_only_radiobutton.TabStop = true;
            this.map_script_only_radiobutton.Text = "Map Script Only";
            this.map_script_only_radiobutton.UseVisualStyleBackColor = true;
            // 
            // progress_text
            // 
            this.progress_text.AutoSize = true;
            this.progress_text.Location = new System.Drawing.Point(12, 130);
            this.progress_text.Name = "progress_text";
            this.progress_text.Size = new System.Drawing.Size(90, 20);
            this.progress_text.TabIndex = 14;
            this.progress_text.Text = "No progress";
            // 
            // app_version_text
            // 
            this.app_version_text.AutoSize = true;
            this.app_version_text.Location = new System.Drawing.Point(877, 145);
            this.app_version_text.Name = "app_version_text";
            this.app_version_text.Size = new System.Drawing.Size(130, 20);
            this.app_version_text.TabIndex = 15;
            this.app_version_text.Text = "Update 219 (1.0.4)";
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(86, 168);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(921, 29);
            this.progress.TabIndex = 16;
            // 
            // progress_total
            // 
            this.progress_total.Location = new System.Drawing.Point(63, 203);
            this.progress_total.Name = "progress_total";
            this.progress_total.Size = new System.Drawing.Size(944, 29);
            this.progress_total.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "Progress:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 20);
            this.label2.TabIndex = 19;
            this.label2.Text = "Total:";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 712);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progress_total);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.app_version_text);
            this.Controls.Add(this.progress_text);
            this.Controls.Add(this.heist_group);
            this.Controls.Add(this.save_errors_button);
            this.Controls.Add(this.debug_button);
            this.Controls.Add(this.reset_button);
            this.Controls.Add(this.extract_type_group);
            this.Controls.Add(this.error_text);
            this.Controls.Add(this.errors_listbox);
            this.Controls.Add(this.convert_button);
            this.Controls.Add(this.openfile_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Window";
            this.extract_type_group.ResumeLayout(false);
            this.extract_type_group.PerformLayout();
            this.heist_group.ResumeLayout(false);
            this.heist_group.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openfile_button;
        private System.Windows.Forms.Button convert_button;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.ListBox errors_listbox;
        private System.Windows.Forms.Label error_text;
        private System.Windows.Forms.RadioButton extract_all_radiobutton;
        private System.Windows.Forms.RadioButton extract_heist_radiobutton;
        private System.Windows.Forms.GroupBox extract_type_group;
        private System.Windows.Forms.Button reset_button;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.Button debug_button;
        private System.Windows.Forms.Button save_errors_button;
        private System.Windows.Forms.FolderBrowserDialog fbd_save;
        private System.Windows.Forms.GroupBox heist_group;
        private System.Windows.Forms.RadioButton map_script_and_instances_radiobutton;
        private System.Windows.Forms.RadioButton map_script_only_radiobutton;
        private System.Windows.Forms.Label progress_text;
        private System.Windows.Forms.Label app_version_text;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.ProgressBar progress_total;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

