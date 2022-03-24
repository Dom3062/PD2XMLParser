namespace XMLParser.Elements.Attributes
{
    class rules
    {
        private List<rules> rule_names = new List<rules>();
        private List<element_item> elements = new List<element_item>();
        private string node_name;
        public rules(XmlNode node)
        {
            node_name = node.Name;
            if (node.HasChildNodes && node.FirstChild.Name != "value_node")
            {
                foreach (XmlNode rule in node.ChildNodes)
                {
                    rule_names.Add(new rules(rule));
                }
            }
            else
            {
                int i = 1;
                foreach (XmlNode element in node.ChildNodes)
                {
                    elements.Add(new element_item(i, element.Attributes["value"].Value));
                    i++;
                }
            }
        }

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string prefix = "")
        {
            string s = prefix + this.node_name + "\n";
            if (this.rule_names.Count > 0)
            {
                foreach (rules rule in this.rule_names)
                {
                    s += rule.ToString(prefix + "\t");
                }
            }
            else
            {
                foreach (element_item element in this.elements)
                {
                    s += element.ToString(prefix + "\t");
                }
            }
            return s;
        }
    }
}