using XMLParser.Elements.Attributes;
namespace XMLParser.MissionFiles.Statics
{
    class editable_gui
    {
        private readonly XmlNode values;
        private readonly List<element_item> shapes;
        public editable_gui(XmlNode values, XmlNode shape)
        {
            this.values = values;
            if (this.shapes == null)
            {
                return;
            }
            this.shapes = new List<element_item>();
            int pos = 1;
            foreach (XmlNode _shape in shape.ChildNodes)
            {
                this.shapes.Add(new element_item(pos, _shape.Attributes["value"].Value));
            }
        }

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string prefix = "")
        {
            string s = "";
            foreach (XmlAttribute Attribute in this.values.Attributes)
            {
                s += prefix + Attribute.Name + " " + Attribute.Value + "\n";
            }
            if (this.shapes != null)
            {
                s += prefix + "shape\n";
                foreach (element_item shape in this.shapes)
                {
                    s += prefix + "\t" + shape.ToString() + "\n";
                }
            }
            return s;
        }
    }
}