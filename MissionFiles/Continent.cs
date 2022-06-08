using System.IO;
using XMLParser.MissionFiles.Statics;
namespace XMLParser.MissionFiles
{
    public struct InstanceContinent
    {
        public string name;
        public int position;
    }

    class Continent
    {
        readonly string continent_name;
        readonly bool main_world = false;
        readonly bool instance = false;
        List<unit> units = new();
        List<instance> instances;
        public Continent(string path, string continent_name, string level_id, string folder_id, bool instance = false)
        {
            this.instance = instance;
            string full_path = path + "\\" + continent_name + "\\" + continent_name + ".continent";
            SharedClass.UpdateProgressText("Reading continent", full_path);
            if (!File.Exists(full_path))
            {
                throw new MainContinentNotFoundException();
            }
            SharedClass.LoadDieselScript(out XmlNode table, full_path);
            if (table == null)
            {
                throw new MainContinentNotLoadedException();
            }
            SharedClass.UpdateProgressText("Parsing continent", full_path);
            this.continent_name = SharedClass.GetWorldName(level_id, folder_id);
            this.main_world = continent_name == this.continent_name;
            if (this.main_world && !instance)
            {
                SharedClass.UpdateProgressText("Parsing instances", full_path);
                instances = new();
                ParseInstances(table.SelectSingleNode("/table/instances"));
            }
            SharedClass.UpdateProgressText("Parsing units", full_path);
            ParseUnits(table.SelectSingleNode("/table/statics"));
        }

        public bool IsWorldContinent()
        {
            return this.main_world;
        }

        public string ContinentName()
        {
            return this.continent_name;
        }

        private void ParseInstances(XmlNode instances)
        {
            if (instances == null)
            {
                return;
            }
            foreach (XmlNode instance in instances.ChildNodes)
            {
                this.instances.Add(new instance(instance));
            }
        }

        public void LoadInstanceData(int instance_pos, string instance_path, bool all, string new_instance_save)
        {
            this.instances[instance_pos].ParseInstanceData(instance_path, all, new_instance_save);
        }

        public void MarkInstanceAsAlreadySaved(int instance_pos)
        {
            this.instances[instance_pos].SetDuplicate();
        }

        public InstanceContinent[] GetLoadedInstances()
        {
            int count = this.instances.Count;
            List<InstanceContinent> instances = new();
            List<string> temp = new();
            for (int i = 0; i < count; i++)
            {
                string folder = this.instances[i].GetInstanceFolderWithoutWorld();
                if (temp.Contains(folder))
                {
                    this.instances[i].SetDuplicate();
                }
                else
                {
                    temp.Add(folder);
                    instances.Add(new InstanceContinent()
                    {
                        name = folder,
                        position = i
                    });
                }
            }
            return instances.ToArray();
        }

        public ref List<instance> GetAllInstances()
        {
            return ref this.instances;
        }

        public ref List<unit> GetAllUnits()
        {
            return ref this.units;
        }

        private void ParseUnits(XmlNode units)
        {
            if (units == null)
            {
                return;
            }
            foreach (XmlNode unit in units.ChildNodes)
            {
                this.units.Add(new unit(unit));
            }
        }

        public void Save(StreamWriter sw)
        {
            SaveInstances();
            sw.WriteLine("");
            if (this.instances != null && this.instances.Count > 0)
            {
                sw.WriteLine("instances");
                foreach (instance instance in this.instances)
                {
                    sw.Write(instance.ToString());
                }
            }
            if (this.units.Count > 0)
            {
                CheckForUnitErrors();
                sw.WriteLine("statics");
                foreach (unit unit in this.units)
                {
                    sw.Write(unit.ToString());
                }
            }
        }

        private void SaveInstances()
        {
            if (this.instance)
            {
                return;
            }
            foreach (instance instance in this.instances)
            {
                instance.Save();
            }
        }

        private void CheckForUnitErrors()
        {
            foreach (unit unit in this.units)
            {
                if (!unit.AllChildsLoaded())
                {
                    SharedClass.AddError(unit);
                }
            }
            CheckForUnitProjectionLightErrors();
        }

        private void CheckForUnitProjectionLightErrors()
        {
            foreach (unit unit in this.units)
            {
                if (!unit.CheckForUnitProjectionLightErrors())
                {
                    SharedClass.AddError(unit, unit.GetUnitProjectionLight());
                }
            }
        }
    }

    class ContinentNotFoundException : Exception
    {
        public ContinentNotFoundException()
        {
        }
    }

    class ContinentNotLoadedException : Exception
    {
        public ContinentNotLoadedException()
        {
        }
    }
}