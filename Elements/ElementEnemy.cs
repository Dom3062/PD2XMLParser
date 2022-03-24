using XMLParser.Elements.Attributes;
namespace XMLParser.Elements
{
    class ElementSpawnEnemyGroup : ElementBase
    {
        List<element_item> spawn_groups = new List<element_item>();
        List<graph_ids> amount;
        private string node;
        public ElementSpawnEnemyGroup(string c, string editor_name, int id, string node_name, XmlNodeList values, string save_node = "preferred_spawn_groups") : base(c, editor_name, id, node_name, values)
        {
            XmlNode node = null;
            this.node = save_node;
            LoadNode(ref node, save_node);
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode graph_id in node.ChildNodes)
                {
                    spawn_groups.Add(new element_item(pos, graph_id.Attributes["value"].Value));
                    pos++;
                }
            }
            node = null;
            LoadNode(ref node, "amount");
            if (node != null)
            {
                amount = new();
                int pos = 1;
                foreach (XmlNode amount in node.ChildNodes)
                {
                    this.amount.Add(new graph_ids(pos, amount.Attributes["value"].Value));
                    pos++;
                }
            }
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (element_item spawn_group in this.spawn_groups)
            {
                s += spawn_group.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement(this.node), s);
            s = "";
            if (this.amount != null)
            {
                foreach (graph_ids amount in this.amount)
                {
                    s += amount.ToString("\t\t");
                }
                AddAttribute(FormatCustomElement("amount"), s);
            }
        }
    }

    class ElementEnemyPreferedAdd : ElementSpecialObjective
    {
        List<element_item> spawn_points;
        public ElementEnemyPreferedAdd(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values, "spawn_groups")
        {
            XmlNode node = null;
            LoadNode(ref node, "spawn_points");
            if (node != null)
            {
                int pos = 1;
                spawn_points = new();
                foreach (XmlNode graph_id in node.ChildNodes)
                {
                    spawn_points.Add(new element_item(pos, Convert.ToInt32(graph_id.Attributes["value"].Value)));
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
            if (this.spawn_points != null)
            {
                foreach (element_item followup_element in this.spawn_points)
                {
                    if (followup_element.GetElementID() == id)
                    {
                        followup_element.UpdateElementName(editor_name);
                        followup_element.UpdateElementDisabled(disabled);
                    }
                }
            }
        }

        protected override void PrepareAttributes()
        {
            base.PrepareAttributes();
            string s = "";
            if (this.spawn_points != null)
            {
                foreach (element_item point in this.spawn_points)
                {
                    s += point.ToString("\t\t");
                }
                AddAttribute(FormatCustomElement("spawn_points"), s);
            }
        }
    }
}