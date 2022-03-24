using XMLParser.Elements.Attributes;
namespace XMLParser.Elements
{
    class ElementAreaTrigger : ElementBase
    {
        List<element_item> rules_element_ids = new();
        List<element_item> spawn_unit_elements = new();
        List<element_item> use_shape_element_ids = new();
        List<unit_ids> unit_ids = new();
        public ElementAreaTrigger(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
            XmlNode node = null;
            LoadNode(ref node, "rules_element_ids");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode rules_element_id in node.ChildNodes)
                {
                    rules_element_ids.Add(new element_item(pos, Convert.ToInt32(rules_element_id.Attributes["value"].Value)));
                    pos++;
                }
            }
            node = null;
            LoadNode(ref node, "spawn_unit_elements");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode spawn_unit_element in node.ChildNodes)
                {
                    spawn_unit_elements.Add(new element_item(pos, Convert.ToInt32(spawn_unit_element.Attributes["value"].Value)));
                    pos++;
                }
            }
            node = null;
            LoadNode(ref node, "use_shape_element_ids");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode use_shape_element_id in node.ChildNodes)
                {
                    spawn_unit_elements.Add(new element_item(pos, Convert.ToInt32(use_shape_element_id.Attributes["value"].Value)));
                    pos++;
                }
            }
            node = null;
            LoadNode(ref node, "unit_ids");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode unit_id in node.ChildNodes)
                {
                    int u_id = Convert.ToInt32(unit_id.Attributes["value"].Value);
                    unit_ids.Add(new unit_ids(u_id, pos));
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
            foreach (element_item rules_element_id in this.rules_element_ids)
            {
                if (rules_element_id.GetElementID() == id)
                {
                    rules_element_id.UpdateElementName(editor_name);
                    rules_element_id.UpdateElementDisabled(disabled);
                }
            }
            foreach (element_item spawn_unit_element in this.spawn_unit_elements)
            {
                if (spawn_unit_element.GetElementID() == id)
                {
                    spawn_unit_element.UpdateElementName(editor_name);
                    spawn_unit_element.UpdateElementDisabled(disabled);
                }
            }
            foreach (element_item use_shape_element_id in this.use_shape_element_ids)
            {
                if (use_shape_element_id.GetElementID() == id)
                {
                    use_shape_element_id.UpdateElementName(editor_name);
                    use_shape_element_id.UpdateElementDisabled(disabled);
                }
            }
        }

        public override void UpdateUnitElements(string ElementName, int id, string unit_name, string unit_pos)
        {
            foreach (unit_ids unit_id in this.unit_ids)
            {
                if (unit_id.GetElementID() == id)
                {
                    unit_id.UpdateUnitName(unit_name, unit_pos);
                }
            }
        }

        public override bool HasUnits()
        {
            return this.unit_ids.Count > 0;
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (element_item rules_element_id in this.rules_element_ids)
            {
                s += rules_element_id.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("rules_element_ids"), s);
            s = "";
            foreach (element_item spawn_unit_element in this.spawn_unit_elements)
            {
                s += spawn_unit_element.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("spawn_unit_elements"), s);
            s = "";
            foreach (element_item use_shape_element_id in this.use_shape_element_ids)
            {
                s += use_shape_element_id.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("use_shape_element_ids"), s);
            s = "";
            foreach (unit_ids unit_id in this.unit_ids)
            {
                s += unit_id.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("unit_ids"), s);
        }
    }

    class ElementAreaReportTrigger : ElementBase
    {
        List<element_item> rules_element_ids = new();
        List<element_item> spawn_unit_elements = new();
        List<element_item> use_shape_element_ids = new();
        List<unit_ids> unit_ids = new();
        public ElementAreaReportTrigger(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
            XmlNode node = null;
            LoadNode(ref node, "rules_element_ids");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode rules_element_id in node.ChildNodes)
                {
                    rules_element_ids.Add(new element_item(pos, Convert.ToInt32(rules_element_id.Attributes["value"].Value)));
                    pos++;
                }
            }
            node = null;
            LoadNode(ref node, "spawn_unit_elements");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode spawn_unit_element in node.ChildNodes)
                {
                    spawn_unit_elements.Add(new element_item(pos, Convert.ToInt32(spawn_unit_element.Attributes["value"].Value)));
                    pos++;
                }
            }
            node = null;
            LoadNode(ref node, "use_shape_element_ids");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode use_shape_element_id in node.ChildNodes)
                {
                    spawn_unit_elements.Add(new element_item(pos, Convert.ToInt32(use_shape_element_id.Attributes["value"].Value)));
                    pos++;
                }
            }
            LoadNode(ref node, "unit_ids");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode unit_id in node.ChildNodes)
                {
                    int u_id = Convert.ToInt32(unit_id.Attributes["value"].Value);
                    unit_ids.Add(new unit_ids(u_id, pos));
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
            foreach (element_item rules_element_id in this.rules_element_ids)
            {
                if (rules_element_id.GetElementID() == id)
                {
                    rules_element_id.UpdateElementName(editor_name);
                    rules_element_id.UpdateElementDisabled(disabled);
                }
            }
            foreach (element_item spawn_unit_element in this.spawn_unit_elements)
            {
                if (spawn_unit_element.GetElementID() == id)
                {
                    spawn_unit_element.UpdateElementName(editor_name);
                    spawn_unit_element.UpdateElementDisabled(disabled);
                }
            }
            foreach (element_item use_shape_element_id in this.use_shape_element_ids)
            {
                if (use_shape_element_id.GetElementID() == id)
                {
                    use_shape_element_id.UpdateElementName(editor_name);
                    use_shape_element_id.UpdateElementDisabled(disabled);
                }
            }
        }

        public override void UpdateUnitElements(string ElementName, int id, string unit_name, string unit_pos)
        {
            foreach (unit_ids unit_id in this.unit_ids)
            {
                if (unit_id.GetElementID() == id)
                {
                    unit_id.UpdateUnitName(unit_name, unit_pos);
                }
            }
        }

        public override bool HasUnits()
        {
            return this.unit_ids.Count > 0;
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (element_item rules_element_id in this.rules_element_ids)
            {
                s += rules_element_id.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("rules_element_ids"), s);
            s = "";
            foreach (element_item spawn_unit_element in this.spawn_unit_elements)
            {
                s += spawn_unit_element.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("spawn_unit_elements"), s);
            s = "";
            foreach (element_item use_shape_element_id in this.use_shape_element_ids)
            {
                s += use_shape_element_id.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("use_shape_element_ids"), s);
            s = "";
            foreach (unit_ids unit_id in this.unit_ids)
            {
                s += unit_id.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("unit_ids"), s);
        }
    }
}