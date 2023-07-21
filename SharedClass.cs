using System.IO;
using System.Windows.Forms;
using DieselEngineFormats.ScriptData;
using unit = XMLParser.MissionFiles.Statics.unit;
using ElementBase = XMLParser.Elements.ElementBase;
using projection_light = XMLParser.MissionFiles.Statics.projection_light;
namespace XMLParser
{
    static class SharedClass
    {
        private static MainWindow MW;
        public static readonly string[] common_attributes = { "enabled", "execute_on_startup", "base_delay", "base_delay_rand", "trigger_times" };

        public static readonly string[] common_instance_attributes = { "continent", "index_size", "mission_placed", "position", "rotation", "script", "start_index" };

        public static readonly string[] unit_attributes_to_avoid = { "continent", "name", "name_id", "position", "rotation", "unit_id" };

        public static readonly string[] contractor_ids = { "armadillo", "bain", "butcher", "blaine", "classics", "continental", "dentist", "elephant", "escapes", "hector", "locke", "jiu", "mcshay", "pbr", "shayu", "skm", "vlad" };
        public static void SetMainWindow(MainWindow MW)
        {
            SharedClass.MW = MW;
        }
        public static string FormatElementName(string element_name)
        {
            return "´" + element_name + "´";
        }

        public static void LoadDieselScript(out XmlNode node, string diesel_file)
        {
            byte[] buffer = File.ReadAllBytes(diesel_file);
            using MemoryStream ms = new(buffer);
            ms.Position = 0;
            CustomXMLNode custom_node = new("table", (Dictionary<string, object>)new ScriptData(new BinaryReader(ms)).Root, "");
            string xml = custom_node.ToString(0, true); // Start from scratch and escape illegal XML characters
            CheckAndFixIllegalCharacters(ref xml);
            LoadXML(out node, ref xml, diesel_file);
        }

        private static void LoadXML(out XmlNode node, ref string xml, string diesel_file)
        {
            XmlDocument doc = new();
            try
            {
                doc.LoadXml(xml);
                node = doc.DocumentElement.SelectSingleNode("/table");
            }
            catch (XmlException ex)
            {
                MessageBox.Show("Neplatný soubor XML!\n" + ex.ToString(), "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (xml != null)
                {
                    if (MessageBox.Show("Přejete si Diesel Script (XML) uložit a podívat se na chybu ?", "Otázka", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        File.WriteAllText(diesel_file.Replace(".mission", "_broken.mission.txt"), xml);
                    }
                }
                node = null;
            }
        }

        private static void CheckAndFixIllegalCharacters(ref string xml)
        {
            if (xml.Contains('\u001f'))
            {
                xml = xml.Replace('\u001f', '_');
            }
        }

        private static string GetLevelID(string path)
        {
            return path[(path.LastIndexOf("\\") + 1)..];
        }

        private static string GetFolderID(string path)
        {
            int count = path.Count('\\');
            int pos = path.GetCharPos('\\', count - 2);
            string final = path[(pos + 1)..];
            string folder_id = final.Remove(final.LastIndexOf('\\'));
            return folder_id;
        }

        public static (string level_id, string folder_id) GetLevelFolderID(string path, bool instance = false)
        {
            string level_id = GetLevelID(path);
            string folder_id = GetFolderID(path);
            if (level_id.StartsWith("stage") && !instance)
            {
                if (folder_id == "rvd")
                {
                    folder_id = "bain";
                    level_id = level_id.Replace("stage", "rvd");
                }
                else if (folder_id == "thebomb")
                {
                    folder_id = "butcher";
                    level_id = level_id.Replace("stage", "crojob");
                }
                else if (folder_id == "hox")
                {
                    folder_id = "dentist";
                    level_id = level_id.Replace("stage", "hox");
                }
                else if (folder_id == "mia")
                {
                    folder_id = "dentist";
                    level_id = level_id.Replace("stage", "mia");
                }
                else if (folder_id == "peta")
                {
                    folder_id = "vlad";
                    if (level_id == "stage1")
                    {
                        level_id = "peta";
                    }
                    else
                    {
                        level_id = "peta2";
                    }
                }
                else if (folder_id.ContainsOr("e_election_day", "e_framing_frame", "e_welcome_to_the_jungle", "h_alex_must_die", "h_firestarter", "h_watchdogs", "short1", "short2"))
                {
                    level_id = folder_id + "_" + level_id;
                    folder_id = "";
                }
            }
            if (instance)
            {
                if (folder_id == "narratives" || folder_id == "instances" || folder_id == "unique" || folder_id == "shared")
                {
                    folder_id = "";
                }
            }
            else
            {
                if (folder_id == "chill" || folder_id == "narratives" || folder_id == "instances" || folder_id == "unique" || folder_id == "shared")
                {
                    folder_id = "";
                }
            }
            if (folder_id != "")
            {
                folder_id += "\\";
            }
            return (level_id, folder_id);
        }

        public static string GetWorldName(string level_id, string folder_id)
        {
            if (level_id == "mallcrasher")
            {
                return "mission_turf_war";
            }
            return "world";
        }

        public static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void UpdateProgressText(string status, string path)
        {
            MW.UpdateProgressText(status, path);
        }

        public static void SetProgressMax(int max)
        {
            MW.SetProgressMax(max);
        }

        public static void IncreaseProgress()
        {
            MW.IncreaseProgress();
        }

        public static void UpdateProgress(int progress)
        {
            MW.UpdateProgress(progress);
        }

        public static void AddError(ElementBase element)
        {
            string message = element.FormatHeader() + "; Value(s) not loaded: ";
            string[] values = element.GetChildsNotLoaded();
            foreach (string value in values)
            {
                message += value + "; ";
            }
            AddError(message);
        }

        public static void AddError(unit unit)
        {
            string message = unit.FormatHeader() + "; Value(s) not loaded: ";
            string[] values = unit.GetChildsNotLoaded();
            foreach (string value in values)
            {
                message += value + "; ";
            }
            AddError(message);
        }

        public static void AddError(unit unit, projection_light projection_light)
        {
            string message = unit.FormatHeader() + "; Value(s) not loaded: projection_lights -> ";
            string[] values = projection_light.GetChildsNotLoaded();
            foreach (string value in values)
            {
                message += value + "; ";
            }
            AddError(message);
        }

        public static void AddError(string error)
        {
            MW.AddError(error);
        }
    }
}