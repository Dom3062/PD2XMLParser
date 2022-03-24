using System.IO;
namespace XMLParser.MissionFiles.Statics
{
    class instance
    {
        private Mission Mission;
        private Continent Continent;
        private readonly string instance_name;
        private readonly string folder;
        private readonly string folder_without_world;
        private readonly XmlNode values;
        private bool duplicate = false;
        private string instance_save_path;
        private bool all = false;
        private string instance_id;
        private string instance_folder;
        private string instance_type;
        public instance(XmlNode instance)
        {
            this.instance_name = instance.Attributes["name"].Value;
            this.folder = instance.Attributes["folder"].Value;
            this.folder_without_world = this.folder.Remove(this.folder.LastIndexOf("/world"));
            this.values = instance;
        }

        public void ParseInstanceData(string instance_path, bool all, string new_instance_save)
        {
            this.instance_save_path = instance_path;
            Continent = new(instance_path, "world", true);
            Mission = new(instance_path);
            ref List<unit> units = ref Continent.GetAllUnits();
            foreach (unit unit in units)
            {
                int unit_id = unit.GetUnitID();
                string unit_name = unit.GetFormattedName();
                string unit_pos = unit.GetUnitPos();
                Mission.UpdateUnitsInElements(unit_id, unit_name, unit_pos);
            }
            if (all)
            {
                this.all = true;
                var (level_id, folder_id) = SharedClass.GetLevelFolderID(instance_path, true);
                this.instance_id = level_id;
                this.instance_folder = folder_id;
                this.instance_type = instance_path.Contains("\\unique") ? "unique" : "shared";
                this.instance_save_path = new_instance_save;
            }
        }

        public void SetDuplicate()
        {
            this.duplicate = true;
        }

        public string GetInstanceName()
        {
            return this.instance_name;
        }

        public string GetInstanceFolder()
        {
            return this.folder;
        }

        public string GetInstanceFolderWithoutWorld()
        {
            return this.folder_without_world;
        }

        public string GetFormattedName()
        {
            string without_world = this.folder.Replace("/world", "");
            int last_underscore_in_name_id = this.instance_name.LastIndexOf("_");
            if (last_underscore_in_name_id == -1)
            {
                return without_world;
            }
            string name_without_number = this.instance_name.Substring(0, last_underscore_in_name_id);
            int last_slash_in_folder = without_world.LastIndexOf("/");
            string last_folder_name = without_world.Substring(last_slash_in_folder + 1);
            if (name_without_number == last_folder_name)
            {
                string number = this.instance_name.Substring(last_underscore_in_name_id + 1);
                return without_world + "/" + number;
            }
            return without_world + "/" + this.instance_name;
        }

        private string FormatAttribute(string attribute)
        {
            switch(attribute)
            {
                case "mission_placed":
                    {
                        string value = GetAttributeValue(attribute);
                        if (value == "true")
                        {
                            return "mission_placed True";
                        }
                        return "";
                    }
                case "position":
                case "rotation":
                    {
                        string value = GetAttributeValue(attribute);
                        value = value.Replace(",", ".").Replace(" ", ", ");
                        return attribute + " " + value;
                    }
                default:
                    {
                        return attribute + " " + GetAttributeValue(attribute);
                    }
            }
        }

        public override string ToString()
        {
            string s = "\t" + GetFormattedName() + "\n";
            foreach(string attribute in SharedClass.common_instance_attributes)
            {
                if (AttributeInInstanceExists(attribute))
                {
                    string value = FormatAttribute(attribute);
                    if (value != "")
                    {
                        s += "\t\t" + value + "\n";
                    }
                }
            }
            return s;
        }

        public void Save()
        {
            if (this.duplicate)
            {
                return;
            }
            string continent;
            string mission;
            if (this.all)
            {
                string common_path = this.instance_save_path + "\\instances\\" + this.instance_type + "\\" + this.instance_folder;
                SharedClass.CreateFolder(common_path);
                continent = common_path + this.instance_id + "_con.txt";
                mission = common_path + this.instance_id + "_mis";
            }
            else
            {
                continent = this.instance_save_path + "\\world\\world.continent.txt";
                mission = this.instance_save_path + "\\world\\world_mission";
            }
            using FileStream fsc = new(continent, FileMode.Create, FileAccess.Write);
            using StreamWriter swc = new(fsc);
            Continent.Save(swc);
            Mission.Save(mission);
        }

        private bool AttributeInInstanceExists(string attribute)
        {
            return this.values.Attributes[attribute] != null;
        }

        protected string GetAttributeValue(string attribute)
        {
            return this.values.Attributes[attribute].Value;
        }
    }
}