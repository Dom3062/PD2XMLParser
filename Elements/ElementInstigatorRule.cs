using XMLParser.Elements.Attributes;
namespace XMLParser.Elements
{
    class ElementInstigatorRule : ElementBase
    {
        List<rules> rules = new();
        public ElementInstigatorRule(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
            XmlNode node = null;
            LoadNode(ref node, "rules");
            foreach (XmlNode rule in node.ChildNodes)
            {
                rules.Add(new rules(rule));
            }
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (rules rule in this.rules)
            {
                s += rule.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("rules"), s);
        }
    }
}