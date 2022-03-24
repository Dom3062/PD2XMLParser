using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace XMLParser
{
    public partial class MainWindow : Form
    {
        // Constants
        private const string instance_path = "\\instances";
        private const string instance_shared_path = instance_path + "\\shared";
        private const string narrative_path = "\\narratives";
        private const string continent_extension = ".continent";
        private const string continent_txt_extension = ".continent.txt";
        private const string continent_file = "\\world.continent";
        private const string mission_extension = ".mission";
        private const string mission_txt_extension = ".mission.txt";
        private const string mission_file = "\\world.mission";

        int n_of_errors = 0;
        string xml_file = null;
        string selected_heist = null;
        string selected_load_path = null;
        string selected_save_path = null;
        public MainWindow()
        {
            InitializeComponent();
            SharedClass.SetMainWindow(this);
        }

        public void SetEnabledRadiobuttons(bool enabled)
        {
            this.extract_all_radiobutton.Enabled = enabled;
            this.extract_heist_radiobutton.Enabled = enabled;
            this.convert_button.Enabled = !enabled;
            this.reset_button.Enabled = !enabled;
        }

        private void openfile_button_Click(object sender, EventArgs e)
        {
            if (this.extract_all_radiobutton.Checked)
            {
                if (this.fbd.ShowDialog() == DialogResult.OK)
                {
                    selected_load_path = this.fbd.SelectedPath;
                    if (this.fbd_save.ShowDialog() == DialogResult.OK)
                    {
                        selected_save_path = this.fbd_save.SelectedPath;
                        SetEnabledRadiobuttons(false);
                    }
                    else
                    {
                        selected_load_path = null;
                    }
                }
            }
            else if (this.extract_heist_radiobutton.Checked)
            {
                if (this.fbd.ShowDialog() == DialogResult.OK)
                {
                    string folder_path = this.fbd.SelectedPath;
                    if (Directory.Exists(folder_path + narrative_path) && Directory.Exists(folder_path + instance_path))
                    {
                        string[] heists = EnemurateDirectories(folder_path + narrative_path);
                        using Heist h = new(heists);
                        if (h.ShowDialog() == DialogResult.OK)
                        {
                            string selected_heist = h.SelectedHeist;
                            if (selected_heist != null)
                            {
                                this.selected_heist = selected_heist;
                                selected_load_path = folder_path;
                                SetEnabledRadiobuttons(false);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Folder 'narratives' and/or 'instances' do not exists", "Missing folder(s)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    xml_file = ofd.FileName;
                    SetEnabledRadiobuttons(false);
                }
            }
        }

        private void convert_button_Click(object sender, EventArgs e)
        {
            if (this.extract_all_radiobutton.Checked)
            {
                string[] directories = EnemurateDirectories(selected_load_path);
                string[] heists = directories.Where(heist => heist.Contains("\\levels\\narratives")).ToArray();
                SetProgressTotalMax(heists.Length);
                ConvertDiesel Convertor = new();
                Convertor.SetFiles(directories);
                Convertor.SetExtractDetails(1, 0);
                Convertor.SetSavePath(selected_save_path);
                foreach (string directory in heists)
                {
                    string worldless = directory.Replace("\\world", "");
                    Convertor.UpdateHeistPath(worldless);
                    Convertor.Save();
                    IncreaseProgressTotal();
                }
            }
            else if (this.extract_heist_radiobutton.Checked)
            {
                string[] directories = EnemurateDirectories(selected_load_path);
                ConvertDiesel Convertor = new();
                Convertor.SetFiles(directories);
                Convertor.SetExtractDetails(GetExtractType(), GetHeistType());
                string worldless = this.selected_heist.Replace("\\world", "");
                Convertor.UpdateHeistPath(worldless);
                Convertor.Save();
                /*if (MissionAndContinentFilesExist(selected_heist))
                { //Oba soubory existují. Prvně načteme continent
                    xml_file = selected_heist + continent_file;
                    UpdateProgressText("Reading continent", xml_file);
                    LoadDieselScript(out string continent, xml_file);
                    LoadXML(ref continent); // Continent se načte do paměti
                    UpdateProgressText("Parsing continent", xml_file);
                    SaveMissionWorld(xml_file.Replace(continent_extension, continent_txt_extension), ref instances, ref statics);
                    continent = null;
                    //save_enabled = false; // Zamezíme uložení elementů, protože jsme neaktualizovali instance
                    xml_file = selected_heist + mission_file;
                    UpdateProgressText("Reading mission script", xml_file);
                    LoadDieselScript(out string mission, xml_file);
                    UpdateProgressText("Parsing mission script", xml_file);
                    LoadXML(ref mission); // Elementy se načtou, ale neuloží!
                    UpdateProgressText("Updating instances in elements", xml_file);
                    UpdateInstancesInElements(); // Aktualizuj instance v elementech
                    UpdateProgressText("Updating units in elements", xml_file);
                    UpdateUnitsInElements(); // Aktualizuj units v elementech
                    SaveMissionScript(xml_file.Replace(mission_extension, mission_txt_extension));
                    mission = null;
                    if (this.map_script_and_instances_radiobutton.Checked)
                    {
                        ConvertMissionInstances();
                    }
                    GC.Collect();
                }
                else
                {
                    MessageBox.Show("Continent and mission files do not exist", "Missing files", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }*/
            }
            else
            {
                /*if (xml_file == null)
                {
                    return;
                }
                if (xml_file.Contains(".xml"))
                {
                    string nil = null;
                    LoadXML(ref nil);
                    xml_file = xml_file.Replace(".xml", ".txt");
                }
                else
                {
                    LoadDieselScript(out string script, xml_file);
                    LoadXML(ref script);
                    script = null;
                    xml_file = xml_file.Replace("mission", "mission.txt").Replace("continent", "continent.txt");
                    GC.Collect();
                }
                if (this.elements.Count > 0)
                {
                    SaveMissionScript(xml_file);
                }
                else
                {
                    SaveMissionWorld(xml_file, ref instances, ref statics);
                }*/
            }
            UpdateProgressText("", "Done");
            Done();
        }

        private void reset_button_Click(object sender, EventArgs e)
        {
            xml_file = null;
            selected_load_path = null;
            selected_save_path = null;
            SetEnabledRadiobuttons(true);
            this.error_text.Text = "Errors:";
            this.errors_listbox.Items.Clear();
            this.n_of_errors = 0;
        }

        private void Done()
        {
            xml_file = null;
            selected_load_path = null;
            selected_save_path = null;
            SetEnabledRadiobuttons(true);
            this.selected_heist = null;
        }

        private void debug_button_Click(object sender, EventArgs e)
        {
        }

        private void save_errors_button_Click(object sender, EventArgs e)
        {
            if (this.errors_listbox.Items.Count > 0)
            {
                File.WriteAllText("C:\\Users\\Dominik\\Desktop\\errors.txt", "");
                foreach (object item in this.errors_listbox.Items)
                {
                    File.AppendAllText("C:\\Users\\Dominik\\Desktop\\errors.txt", item.ToString() + "\n");
                }
            }
        }

        private void extract_heist_radiobutton_CheckedChanged(object sender, EventArgs e)
        {
            this.map_script_only_radiobutton.Enabled = this.extract_heist_radiobutton.Checked;
            this.map_script_and_instances_radiobutton.Enabled = this.extract_heist_radiobutton.Checked;
        }

        private string[] EnemurateDirectories(string path)
        {
            string[] dirs = Directory.GetDirectories(path, "*.*", SearchOption.AllDirectories);
            List<string> to_return = new();
            foreach (string dir in dirs)
            {
                if (dir.EndsWith("\\world"))
                {
                    to_return.Add(dir);
                }
            }
            return to_return.ToArray();
        }

        #region Shared Methods
        private int GetExtractType()
        {
            if (this.extract_all_radiobutton.Checked)
            {
                return 1;
            }
            else if (this.extract_heist_radiobutton.Checked)
            {
                return 2;
            }
            return 3;
        }

        private int GetHeistType()
        {
            return this.map_script_only_radiobutton.Checked ? 1 : 2;
        }

        public void UpdateProgressText(string status, string path)
        {
            this.progress_text.Text = "Progress: " + path + "\n" + status;
            this.progress_text.Invalidate();
            this.progress_text.Update();
            this.progress_text.Refresh();
        }

        public void SetProgressMax(int max)
        {
            this.progress.Maximum = max;
        }

        private void SetProgressTotalMax(int max)
        {
            this.progress_total.Maximum = max;
        }

        public void UpdateProgress(int progress)
        {
            this.progress.Value = progress;
            this.progress.Invalidate();
            this.progress.Update();
            this.progress.Refresh();
        }

        public void IncreaseProgress()
        {
            UpdateProgress(this.progress.Value + 1);
        }

        private void UpdateProgressTotal(int progress)
        {
            this.progress_total.Value = progress;
            this.progress_total.Invalidate();
            this.progress_total.Update();
            this.progress_total.Refresh();
        }

        private void IncreaseProgressTotal()
        {
            UpdateProgressTotal(this.progress_total.Value + 1);
        }

        public void AddError(string message)
        {
            this.n_of_errors++;
            this.error_text.Text = "Errors (" + this.n_of_errors.ToString() + "):";
            this.error_text.Invalidate();
            this.error_text.Update();
            this.error_text.Refresh();
            this.errors_listbox.Items.Add(message);
            this.errors_listbox.Invalidate();
            this.errors_listbox.Update();
            this.errors_listbox.Refresh();
        }
        #endregion
    }
}