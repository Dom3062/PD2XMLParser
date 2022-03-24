using XMLParser.Elements.Attributes;
namespace XMLParser.Elements
{
    class ElementPrePlanning : ElementBase
    {
        List<element_item> allowed_types = new List<element_item>();
        List<element_item> disables_types = new List<element_item>();
        public ElementPrePlanning(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
            XmlNode node = null;
            LoadNode(ref node, "allowed_types");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode allowed_type in node.ChildNodes)
                {
                    allowed_types.Add(new element_item(pos, allowed_type.Attributes["value"].Value));
                    pos++;
                }
            }
            node = null;
            LoadNode(ref node, "disables_types");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode disables_type in node.ChildNodes)
                {
                    disables_types.Add(new element_item(pos, disables_type.Attributes["value"].Value));
                    pos++;
                }
            }
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (element_item allowed_type in this.allowed_types)
            {
                s += allowed_type.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("allowed_types"), s);
            s = "";
            foreach (element_item disables_type in this.disables_types)
            {
                s += disables_type.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("disables_types"), s);
        }
    }

    class ElementPrePlanningExecuteGroup : ElementBase
    {
        List<element_item> location_groups = new();
        public ElementPrePlanningExecuteGroup(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
            XmlNode node = null;
            LoadNode(ref node, "location_groups");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode location_group in node.ChildNodes)
                {
                    location_groups.Add(new element_item(pos, location_group.Attributes["value"].Value));
                    pos++;
                }
            }
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (element_item allowed_type in this.location_groups)
            {
                s += allowed_type.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("location_groups"), s);
        }
    }
}