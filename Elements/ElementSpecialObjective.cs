using XMLParser.Elements.Attributes;
namespace XMLParser.Elements
{
    class ElementSpecialObjective : ElementBase
    {
        List<element_item> followup_elements = new();
        List<element_item> spawn_instigator_ids = new();
        List<instance_params> instance_var_names;
        private string node;
        public ElementSpecialObjective(string c, string editor_name, int id, string node_name, XmlNodeList values, string save_node = "followup_elements") : base(c, editor_name, id, node_name, values)
        {
            XmlNode node = null;
            this.node = save_node;
            LoadNode(ref node, save_node);
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode graph_id in node.ChildNodes)
                {
                    followup_elements.Add(new element_item(pos, Convert.ToInt32(graph_id.Attributes["value"].Value)));
                    pos++;
                }
            }
            node = null;
            LoadNode(ref node, "spawn_instigator_ids");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode spawn_instigator_id in node.ChildNodes)
                {
                    spawn_instigator_ids.Add(new element_item(pos, Convert.ToInt32(spawn_instigator_id.Attributes["value"].Value)));
                    pos++;
                }
            }
            node = null;
            LoadNode(ref node, "instance_var_names"); //ElementSpawnEnemyDummy
            if (node != null)
            {
                instance_var_names = new();
                foreach (XmlAttribute var_name in node.Attributes)
                {
                    string var = var_name.Name;
                    string value = var_name.Value;
                    instance_var_names.Add(new instance_params(var, value));
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
            foreach (element_item followup_element in this.followup_elements)
            {
                if (followup_element.GetElementID() == id)
                {
                    followup_element.UpdateElementName(editor_name);
                    followup_element.UpdateElementDisabled(disabled);
                }
            }
            foreach (element_item spawn_instigator_id in this.spawn_instigator_ids)
            {
                if (spawn_instigator_id.GetElementID() == id)
                {
                    spawn_instigator_id.UpdateElementName(editor_name);
                    spawn_instigator_id.UpdateElementDisabled(disabled);
                }
            }
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (element_item followup_element in this.followup_elements)
            {
                s += followup_element.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement(this.node), s);
            s = "";
            if (this.instance_var_names != null)
            {
                foreach (instance_params param in this.instance_var_names)
                {
                    s += param.ToString("\t\t");
                }
                AddAttribute(FormatCustomElement("instance_var_names"), s);
            }
            s = "";
            if (this.spawn_instigator_ids.Count > 0)
            {
                foreach (element_item spawn_instigator_id in this.spawn_instigator_ids)
                {
                    s += spawn_instigator_id.ToString("\t\t");
                }
                AddAttribute(FormatCustomElement("spawn_instigator_ids"), s);
            }
        }
    }
}