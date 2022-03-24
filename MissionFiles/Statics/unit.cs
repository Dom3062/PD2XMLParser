namespace XMLParser.MissionFiles.Statics
{
    class unit
    {
        private readonly int unit_id;
        private readonly string folder;
        private readonly string name_id;
        private readonly string position;
        private readonly XmlNode values = null;
        private List<string> all_child_names = new();

        // Unit Data
        private ladder ladder;
        private List<lights> lights;
        private projection_light projection_light;
        private unit_data zipline;
        private editable_gui editable_gui;
        private string projection_texture = null;
        private string projection_texture_name = null;
        public unit(XmlNode unit)
        {
            LoadNode(in unit, ref this.values, "unit_data");
            if (values == null)
            {
                throw new ArgumentException("Unit je null", "values");
            }
            this.unit_id = Convert.ToInt32(values.Attributes["unit_id"].Value);
            this.folder = values.Attributes["name"].Value;
            this.name_id = values.Attributes["name_id"].Value;
            this.position = "(" + values.Attributes["position"].Value.Replace(",", ".").Replace(" ", ", ") + ")";
            foreach (XmlNode node in this.values.ChildNodes)
            {
                this.all_child_names.Add(node.Name);
            }
            ParseUnitData();
        }

        private void LoadNode(in XmlNode unit, ref XmlNode xml_node, string node_name)
        {
            foreach (XmlNode node in unit.ChildNodes)
            {
                if (node.Name == node_name)
                {
                    xml_node = node;
                    RemoveLoadedChild(node_name);
                    break;
                }
            }
        }

        private void RemoveLoadedChild(string child_name)
        {
            this.all_child_names.Remove(child_name);
        }

        public bool AllChildsLoaded()
        {
            return this.all_child_names.Count == 0;
        }

        public string[] GetChildsNotLoaded()
        {
            return this.all_child_names.ToArray();
        }

        public string GetFormattedName()
        {
            int last_underscore_in_name_id = this.name_id.LastIndexOf("_");
            if (last_underscore_in_name_id != -1)
            {
                string name_without_number = this.name_id.Substring(0, last_underscore_in_name_id);
                int last_slash_in_folder = this.folder.LastIndexOf("/");
                string last_folder_name = this.folder.Substring(last_slash_in_folder + 1);
                if (name_without_number == last_folder_name)
                {
                    string number = this.name_id.Substring(last_underscore_in_name_id + 1);
                    return this.folder + "/" + number;
                }

            }
            return this.folder + "/" + this.name_id;
        }

        public string FormatHeader()
        {
            return this.unit_id + " " + GetFormattedName() + " " + this.position;
        }

        public int GetUnitID()
        {
            return this.unit_id;
        }

        public string GetUnitPos()
        {
            return this.position;
        }

        public bool CheckForUnitProjectionLightErrors()
        {
            if (this.projection_light == null)
            {
                return true;
            }
            return this.projection_light.AllChildsLoaded();
        }

        public projection_light GetUnitProjectionLight()
        {
            return this.projection_light;
        }

        private string FormatAttributes(string prefix)
        {
            string formatted_attributes = "";
            foreach (XmlAttribute attribute in this.values.Attributes)
            {
                if (AttributeInElementCannotBeAvoided(attribute.Name))
                {
                    string value = FormatAttribute(attribute.Name);
                    if (value != "")
                    {
                        formatted_attributes += prefix + value + "\n";
                    }
                }
            }
            return formatted_attributes;
        }

        private string FormatAttribute(string attribute)
        {
            switch (attribute)
            {
                default:
                    {
                        string value = GetAttributeValue(attribute);
                        if (value == "true")
                        {
                            return attribute + " True";
                        }
                        if (value == "false")
                        {
                            return attribute + " False";
                        }
                        return attribute + " " + GetAttributeValue(attribute);
                    }
            }
        }

        private string GetAttributeValue(string attribute)
        {
            return this.values.Attributes[attribute].Value;
        }

        private bool AttributeInElementCannotBeAvoided(string attribute)
        {
            return !SharedClass.unit_attributes_to_avoid.Contains(attribute);
        }

        private void ParseUnitData()
        {
            XmlNode ladder = null;
            LoadNode(in this.values, ref ladder, "ladder");
            if (ladder != null)
            {
                this.ladder = new ladder(ladder.Attributes["height"].Value, ladder.Attributes["width"].Value);
            }
            XmlNode lights = null;
            LoadNode(in this.values, ref lights, "lights");
            if (lights != null)
            {
                int pos = 1;
                this.lights = new List<lights>();
                foreach (XmlNode light in lights)
                {
                    this.lights.Add(new lights(pos, light));
                    pos++;
                }
            }
            XmlNode zipline = null;
            LoadNode(in this.values, ref zipline, "zipline");
            if (zipline != null)
            {
                this.zipline = new unit_data(zipline);
            }
            XmlNode editable_gui = null;
            LoadNode(in this.values, ref editable_gui, "editable_gui");
            if (editable_gui != null)
            {
                XmlNode shape = null;
                LoadNode(in this.values, ref shape, "shape");
                this.editable_gui = new editable_gui(editable_gui, shape);
            }
            XmlNode projection_texture = null;
            LoadNode(in this.values, ref projection_texture, "projection_textures");
            if (projection_texture != null)
            {
                this.projection_texture_name = projection_texture.Attributes[0].Name;
                this.projection_texture = projection_texture.Attributes[0].Value;
            }
            XmlNode projection_light = null;
            LoadNode(in this.values, ref projection_light, "projection_lights");
            if (projection_light != null)
            {
                this.projection_light = new projection_light(projection_light);
            }
        }

        public override string ToString()
        {
            string s = "\t" + FormatHeader() + "\n";
            s += FormatAttributes("\t\t");
            s += FormatUnitData("\t\t");
            return s;
        }

        private string FormatUnitData(string prefix = "")
        {
            string s = "";
            if (this.ladder != null)
            {
                s += this.ladder.ToString(prefix);
            }
            if (this.lights != null)
            {
                foreach (lights light in this.lights)
                {
                    s += light.ToString(prefix);
                }
            }
            if (this.zipline != null)
            {
                s += this.zipline.ToString(prefix);
            }
            if (this.editable_gui != null)
            {
                s += this.editable_gui.ToString(prefix);
            }
            if (this.projection_texture != null)
            {
                s += prefix + this.projection_texture_name + " " + this.projection_texture + "\n";
            }
            if (this.projection_light != null)
            {
                s += prefix + this.projection_light.ToString(prefix);
            }
            return s;
        }
    }
}