using XMLParser.Elements.Attributes;
namespace XMLParser.Elements
{
    class ElementAIArea : ElementBase
    {
        List<graph_ids> nav_segs = new();
        private readonly string save_node;
        public ElementAIArea(string c, string editor_name, int id, string node_name, XmlNodeList values, string save_node = "nav_segs") : base(c, editor_name, id, node_name, values)
        {
            XmlNode node = null;
            this.save_node = save_node;
            LoadNode(ref node, save_node);
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode nav_seg in node.ChildNodes)
                {
                    this.nav_segs.Add(new graph_ids(pos, nav_seg.Attributes["value"].Value));
                    pos++;
                }
            }
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (graph_ids nav_seg in this.nav_segs)
            {
                s += nav_seg.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement(this.save_node), s);
        }
    }

    class ElementAIGraph : ElementBase
    {
        List<element_item> graph_ids = new();
        public ElementAIGraph(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
            XmlNode node = null;
            LoadNode(ref node, "graph_ids");
            if (node == null)
            {
                return;
            }
            int pos = 1;
            foreach (XmlNode graph_id in node.ChildNodes)
            {
                graph_ids.Add(new element_item(pos, graph_id.Attributes["value"].Value));
                pos++;
            }
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (element_item graph_id in this.graph_ids)
            {
                s += graph_id.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("graph_ids"), s);
        }
    }

    class ElementAIForceAttention : ElementBase
    {
        //List<string> affected_units = new();
        List<element_item> excluded_units = new();
        List<element_item> included_units = new();
        public ElementAIForceAttention(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
            XmlNode node = null;
            LoadNode(ref node, "affected_units");
            if (node != null)
            {
                // Currently unused
                if (node.ChildNodes.Count > 0)
                {
                    throw new ArgumentException("affected_units není prázdný!");
                }
                /*int pos = 1;
                foreach (XmlNode nav_seg in node.ChildNodes)
                {
                    this.affected_units.Add("");
                    pos++;
                }*/
            }
            node = null;
            LoadNode(ref node, "excluded_units");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode excluded_unit in node.ChildNodes)
                {
                    this.excluded_units.Add(new element_item(pos, Convert.ToInt32(excluded_unit.Attributes["value"].Value)));
                    pos++;
                }
            }
            node = null;
            LoadNode(ref node, "included_units");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode included_unit in node.ChildNodes)
                {
                    this.included_units.Add(new element_item(pos, Convert.ToInt32(included_unit.Attributes["value"].Value)));
                    pos++;
                }
            }
        }

        public override void UpdateElements(int id, string editor_name, bool disabled)
        {
            if (CheckIfElementIDIsTheSame(id))
            {
                return;
            }
            base.UpdateElements(id, editor_name, disabled);
            /*foreach (element_item affected_unit in this.affected_units)
            {
                if (affected_unit.GetElementID() == id)
                {
                    affected_unit.UpdateElementName(editor_name);
                    affected_unit.UpdateElementDisabled(disabled);
                }
            }*/
            foreach (element_item excluded_unit in this.excluded_units)
            {
                if (excluded_unit.GetElementID() == id)
                {
                    excluded_unit.UpdateElementName(editor_name);
                    excluded_unit.UpdateElementDisabled(disabled);
                }
            }
            foreach (element_item included_unit in this.included_units)
            {
                if (included_unit.GetElementID() == id)
                {
                    included_unit.UpdateElementName(editor_name);
                    included_unit.UpdateElementDisabled(disabled);
                }
            }
        }

        protected override void PrepareAttributes()
        {
            AddAttribute(FormatCustomElement("affected_units"), "");
            string s = "";
            foreach (element_item excluded_unit in this.excluded_units)
            {
                s += excluded_unit.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("excluded_units"), s);
            s = "";
            foreach (element_item included_unit in this.included_units)
            {
                s += included_unit.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("included_units"), s);
        }
    }
}