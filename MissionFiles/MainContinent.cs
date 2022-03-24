using System.IO;
using System.Linq;
namespace XMLParser.MissionFiles
{
    /* Loads continent file */
    class MainContinent
    {
        private struct ContinentDefinition
        {
            public int base_id;
            public string name;

            public string ToString(string prefix = "")
            {
                return prefix + base_id.ToString() + ": " + name + "\n";
            }
        }

        private readonly ContinentDefinition[] continent_definitions;
        private const string continents_file = "\\continents.continents";
        public MainContinent(string heist_path)
        {
            string full_path = heist_path + continents_file;
            if (!File.Exists(full_path))
            {
                throw new MainContinentNotFoundException();
            }
            SharedClass.LoadDieselScript(out XmlNode table, full_path);
            if (table == null)
            {
                throw new MainContinentNotLoadedException();
            }
            List<ContinentDefinition> defs = new();
            foreach (XmlNode continent in table.ChildNodes)
            {
                XmlAttributeCollection attributes = continent.Attributes;
                defs.Add(new ContinentDefinition()
                {
                    base_id = Convert.ToInt32(attributes["base_id"].Value),
                    name = attributes["name"].Value
                });
            }
            continent_definitions = defs.OrderBy(con => con.base_id).ToArray();
        }

        public string[] GetContinents()
        {
            string[] s = new string[continent_definitions.Length];
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = continent_definitions[i].name;
            }
            return s;
        }

        public void Save(StreamWriter sw)
        {
            sw.Write("ID range vs continent name:\n");
            foreach (ContinentDefinition continent in continent_definitions)
            {
                sw.Write(continent.ToString("\t"));
            }
        }
    }

    class MainContinentNotFoundException : Exception
    {
        public MainContinentNotFoundException()
        {
        }
    }

    class MainContinentNotLoadedException : Exception
    {
        public MainContinentNotLoadedException()
        {
        }
    }
}