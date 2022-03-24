using XMLParser.Elements.Attributes;
namespace XMLParser.Elements
{
    class ElementEnableSoundEnvironment : ElementBase
    {
        private List<graph_ids> elements;
        public ElementEnableSoundEnvironment(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
            this.AddFakeElement();
        }

        protected override void ParseElements(XmlNode elements)
        {
            this.elements = new();
            if (elements == null)
            {
                return;
            }
            int pos = 1;
            foreach (XmlNode element in elements)
            {
                this.elements.Add(new graph_ids(pos, element.Attributes["value"].Value));
                pos++;
            }
        }

        protected override void PrepareAttributes()
        {
        }

        protected override string FormatElements(string prefix = "")
        {
            string s = "";
            if (this.elements != null)
            {
                foreach (graph_ids e in this.elements)
                {
                    s += e.ToString("\t\t");
                }
            }
            return s;
        }
    }
}