using XMLParser.Elements.Attributes;
namespace XMLParser.Elements
{
    class ElementTimer : ElementCounter
    {
        List<element_item> timers = new List<element_item>();
        private string time_node;
        public ElementTimer(string c, string editor_name, int id, string node_name, XmlNodeList values, string time_node = "timer") : base(c, editor_name, id, node_name, values)
        {
            XmlNode node = null;
            this.time_node = time_node;
            LoadNode(ref node, time_node);
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode timer in node.ChildNodes)
                {
                    timers.Add(new element_item(pos, timer.Attributes["value"].Value));
                    pos++;
                }
            }
        }

        protected override void PrepareAttributes()
        {
            base.PrepareAttributes();
            if (this.timers.Count > 0)
            {
                string s = "";
                foreach (element_item timer in this.timers)
                {
                    s += timer.ToString("\t\t");
                }
                AddAttribute(FormatCustomElement(this.time_node), s);
            }
        }
    }

    class ElementTimerOperator : ElementTimer
    {
        public ElementTimerOperator(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values, "time")
        {
        }
    }
}