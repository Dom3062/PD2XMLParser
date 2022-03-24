using InstanceContinent = XMLParser.MissionFiles.InstanceContinent;
using MainContinent = XMLParser.MissionFiles.MainContinent;
using Continent = XMLParser.MissionFiles.Continent;
using Mission = XMLParser.MissionFiles.Mission;
using instance = XMLParser.MissionFiles.Statics.instance;
using unit = XMLParser.MissionFiles.Statics.unit;
using System.IO;
using System.Linq;
namespace XMLParser
{
    class ConvertDiesel
    {
        private const string instances = "\\instances";
        private const string instances_shared = "\\instances\\shared";
        private const string instances_unique = "\\instances\\unique";
        private const string narratives = "\\narratives";
        private struct MapDefinition
        {
            public Continent Continent;
            public Mission Mission;
        }
        private struct InstanceDefinition
        {
            public string Name;
            public string FilePath;
        }
        private struct Settings
        {
            public bool ExtractInstaces;
            public bool ExtractAll;
        }
        string[] files;
        string heist_path = null;
        string save_path = null;
        string level_id = null;
        string folder_id = null;
        int mission_index = -1;
        List<string> instance_already_converted = new();
        MainContinent MainContinent;
        MapDefinition[] MapDefinitions;
        Settings Options = new();
        public ConvertDiesel()
        {
        }

        public void SetFiles(string[] files)
        {
            this.files = files;
        }

        public void SetExtractDetails(int extract, int heist)
        {
            if (extract == 1)
            {
                this.Options.ExtractAll = true;
                this.Options.ExtractInstaces = true;
            }
            else
            {
                this.Options.ExtractAll = false;
                this.Options.ExtractInstaces = heist == 2;
            }
        }

        public void SetSavePath(string path)
        {
            this.save_path = path;
        }

        private void ClearVariables()
        {
            heist_path = null;
            MainContinent = null;
            MapDefinitions = null;
            GC.Collect();
        }

        public void UpdateHeistPath(string heist_path)
        {
            ClearVariables();
            if (!File.Exists(heist_path + "\\continents.continents"))
            {
                throw new DieselPathNotValid();
            }
            this.heist_path = heist_path;
            var (level_id, folder_id) = SharedClass.GetLevelFolderID(heist_path);
            this.level_id = level_id;
            this.folder_id = folder_id;
            MainContinent = new(heist_path);
            string[] continents = MainContinent.GetContinents();
            MapDefinitions = new MapDefinition[continents.Length];
            for (int i = 0; i < continents.Length; i++)
            {
                Continent c = new(heist_path, continents[i]);
                mission_index = c.IsWorldContinent() ? i : mission_index;
                MapDefinitions[i] = new MapDefinition()
                {
                    Continent = c,
                    Mission = c.IsWorldContinent() ? new Mission(heist_path) : null
                };
            }
            UpdateUnitsInElements();
            if (this.Options.ExtractInstaces)
            {
                LoadInstances();
                UpdateInstancesInElements();
            }
        }

        private void LoadInstances()
        {
            Continent MissionContinent = this.MapDefinitions[this.mission_index].Continent;
            string[] AllInstances = this.files.Where(file => file.Contains("\\levels\\instances")).ToArray();
            InstanceDefinition[] Instances = new InstanceDefinition[AllInstances.Length];
            for (int i = 0; i < AllInstances.Length; i++)
            {
                string worldless = AllInstances[i].Replace("\\world", "");
                Instances[i] = new InstanceDefinition()
                {
                    Name = worldless[AllInstances[i].LastIndexOf("levels\\instances\\")..],
                    FilePath = worldless
                };
            }
            InstanceContinent[] LoadedInstances = MissionContinent.GetLoadedInstances();
            foreach (InstanceDefinition instance in Instances)
            {
                string fixed_path = instance.Name.Replace("\\", "/");
                foreach (InstanceContinent continent in LoadedInstances)
                {
                    if (fixed_path == continent.name)
                    {
                        if (this.instance_already_converted.Contains(fixed_path))
                        {
                            MissionContinent.MarkInstanceAsAlreadySaved(continent.position);
                        }
                        else
                        {
                            MissionContinent.LoadInstanceData(continent.position, instance.FilePath, this.Options.ExtractAll, save_path);
                            this.instance_already_converted.Add(fixed_path);
                        }
                    }
                }
            }
        }

        private void UpdateInstancesInElements()
        {
            ref List<instance> Instances = ref this.MapDefinitions[this.mission_index].Continent.GetAllInstances();
            Mission Mission = this.MapDefinitions[this.mission_index].Mission;
            foreach (instance instance in Instances)
            {
                Mission.UpdateInstancesInElements(instance.GetInstanceName(), instance.GetFormattedName());
            }
        }

        private void UpdateUnitsInElements()
        {
            Mission Mission = this.MapDefinitions[this.mission_index].Mission;
            foreach (MapDefinition map in this.MapDefinitions)
            {
                ref List<unit> units = ref map.Continent.GetAllUnits();
                foreach (unit unit in units)
                {
                    int unit_id = unit.GetUnitID();
                    string unit_name = unit.GetFormattedName();
                    string unit_pos = unit.GetUnitPos();
                    Mission.UpdateUnitsInElements(unit_id, unit_name, unit_pos);
                }
            }
        }

        public void Save()
        {
            if (this.Options.ExtractAll)
            {
                SharedClass.CreateFolder(save_path + instances);
                SharedClass.CreateFolder(save_path + instances_shared);
                SharedClass.CreateFolder(save_path + instances_unique);
                SharedClass.CreateFolder(save_path + narratives);
                foreach (string contractor_id in SharedClass.contractor_ids)
                {
                    SharedClass.CreateFolder(save_path + narratives + "\\" + contractor_id);
                }
                string continent = this.save_path + narratives + "\\" + this.folder_id + this.level_id + "_con.txt";
                using FileStream fsc = new(continent, FileMode.Create, FileAccess.Write);
                using StreamWriter swc = new(fsc);
                MainContinent.Save(swc);
                this.MapDefinitions[this.mission_index].Continent.Save(swc);
                string mission = this.save_path + narratives + "\\" + this.folder_id + this.level_id + "_mis"; // nodes are added in Mission.Save();
                this.MapDefinitions[this.mission_index].Mission.Save(mission);
            }
            else
            {
                string continent = this.heist_path + "\\world\\world.continent.txt";
                using FileStream fsc = new(continent, FileMode.Create, FileAccess.Write);
                using StreamWriter swc = new(fsc);
                MainContinent.Save(swc);
                this.MapDefinitions[this.mission_index].Continent.Save(swc);
                string mission = this.heist_path + "\\world\\world_mission"; // nodes are added in Mission.Save();
                this.MapDefinitions[this.mission_index].Mission.Save(mission);
            }
        }
    }

    class DieselPathNotValid : Exception
    {
        public DieselPathNotValid()
        {
        }
    }
}