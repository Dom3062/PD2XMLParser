namespace XMLParser.MissionFiles.Statics
{
    class projection_light
    {
        private struct lo_omni
        {
            public int x, y;
            public string light;

            public string ToString(string prefix)
            {
                string s = prefix + light + "\n";
                s += prefix + "\tx " + x.ToString() + "\n";
                s += prefix + "\ty " + y.ToString() + "\n";
                return s;
            }
        }

        lo_omni? lo;
        private List<string> all_child_names = new();
        public projection_light(XmlNode values)
        {
            foreach (XmlNode node in values.ChildNodes)
            {
                this.all_child_names.Add(node.Name);
            }
            XmlNode light = null;
            LoadNode(in values, ref light, "lo_omni");
            if (light != null)
            {
                if (light.Attributes != null && light.Attributes.Count > 0)
                {
                    this.lo = new lo_omni()
                    {
                        x = Convert.ToInt32(light.Attributes["x"].Value),
                        y = Convert.ToInt32(light.Attributes["y"].Value),
                        light = "lo_omni"
                    };
                }
            }
            light = null;
            LoadNode(in values, ref light, "ls_spot");
            if (light != null)
            {
                if (light.Attributes != null && light.Attributes.Count > 0)
                {
                    this.lo = new lo_omni()
                    {
                        x = Convert.ToInt32(light.Attributes["x"].Value),
                        y = Convert.ToInt32(light.Attributes["y"].Value),
                        light = "ls_spot"
                    };
                }
            }
            light = null;
            LoadNode(in values, ref light, "lo_light_flicker");
            if (light != null)
            {
                if (light.Attributes != null && light.Attributes.Count > 0)
                {
                    this.lo = new lo_omni()
                    {
                        x = Convert.ToInt32(light.Attributes["x"].Value),
                        y = Convert.ToInt32(light.Attributes["y"].Value),
                        light = "lo_light_flicker"
                    };
                }
            }
            light = null;
            LoadNode(in values, ref light, "ls_light");
            if (light != null)
            {
                if (light.Attributes != null && light.Attributes.Count > 0)
                {
                    this.lo = new lo_omni()
                    {
                        x = Convert.ToInt32(light.Attributes["x"].Value),
                        y = Convert.ToInt32(light.Attributes["y"].Value),
                        light = "ls_light"
                    };
                }
            }
            light = null;
            LoadNode(in values, ref light, "lo_omni_01");
            if (light != null)
            {
                if (light.Attributes != null && light.Attributes.Count > 0)
                {
                    this.lo = new lo_omni()
                    {
                        x = Convert.ToInt32(light.Attributes["x"].Value),
                        y = Convert.ToInt32(light.Attributes["y"].Value),
                        light = "lo_omni_01"
                    };
                }
            }
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

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string prefix = "")
        {
            string s = this.lo?.ToString(prefix) ?? "";
            /*if (this.lo != null)
            {
                s = this.lo.ToString(prefix);
            }*/
            return s;
        }
    }
}