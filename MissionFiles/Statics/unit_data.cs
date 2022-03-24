namespace XMLParser.MissionFiles.Statics
{
    class unit_data
    {
        private readonly XmlNode values;
        public unit_data(XmlNode values)
        {
            this.values = values;
        }

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string prefix = "")
        {
            string s = "";
            foreach (XmlAttribute attribute in values.Attributes)
            {
                s += prefix + attribute.Name + " " + attribute.Value + "\n";
            }
            return s;
        }
    }
}