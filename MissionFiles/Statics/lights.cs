namespace XMLParser.MissionFiles.Statics
{
    class lights
    {
        private readonly XmlNode values;
        private readonly int pos;
        public lights(int pos, XmlNode values)
        {
            this.pos = pos;
            this.values = values;
        }

        private string FormatAttribute(string attribute, string value)
        {
            switch(attribute)
            {
                case "clipping_values":
                case "color":
                    {
                        value = value.Replace(",", ".").Replace(" ", ", ");
                        return attribute + " " + value;
                    }
                default:
                    {
                        return attribute + " " + value;
                    }
            }
        }

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string prefix = "")
        {
            string s = prefix + this.pos.ToString() + "\n";
            foreach (XmlAttribute node in this.values.Attributes)
            {
                s += prefix + "\t" + FormatAttribute(node.Name, node.Value) + "\n";
            }
            return s;
        }
    }
}