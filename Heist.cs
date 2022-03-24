using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace XMLParser
{
    public partial class Heist : Form
    {
        private string selected_heist = null;
        public string SelectedHeist
        {
            get
            { 
                return selected_heist;
            }
        }
        public Heist(string[] heists)
        {
            InitializeComponent();
            foreach (string heist in heists)
            {
                if (heist.Contains("\\world"))
                {
                    this.heist_listbox.Items.Add(heist);
                }
            }
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void confirm_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void heist_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.heist_listbox.SelectedIndex != -1)
            {
                selected_heist = this.heist_listbox.SelectedItem.ToString();
            }
            else
            {
                selected_heist = null;
            }
        }

        private void heist_listbox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.heist_listbox.SelectedIndex != -1)
            {
                selected_heist = this.heist_listbox.SelectedItem.ToString();
                confirm_button_Click(null, null);
            }
            else
            {
                selected_heist = null;
            }
        }
    }
}