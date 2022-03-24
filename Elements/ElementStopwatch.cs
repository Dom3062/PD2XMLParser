using XMLParser.Elements.Attributes;
namespace XMLParser.Elements
{
    class ElementStopwatch : ElementEnableUnit
    {
        List<element_item> timers = new();
        public ElementStopwatch(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values, "digital_gui_unit_ids")
        {
            XmlNode node = null;
            LoadNode(ref node, "timer");
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
                AddAttribute(FormatCustomElement("timer"), s);
            }
        }
    }

    class ElementStopwatchFilter : ElementBase
    {
        private List<element_item> stopwatch_value_ids = new();
        public ElementStopwatchFilter(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
            XmlNode node = null;
            LoadNode(ref node, "stopwatch_value_ids");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode excluded_unit in node.ChildNodes)
                {
                    this.stopwatch_value_ids.Add(new element_item(pos, Convert.ToInt32(excluded_unit.Attributes["value"].Value)));
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
            foreach (element_item stopwatch_value_id in this.stopwatch_value_ids)
            {
                if (stopwatch_value_id.GetElementID() == id)
                {
                    stopwatch_value_id.UpdateElementName(editor_name);
                    stopwatch_value_id.UpdateElementDisabled(disabled);
                }
            }
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (element_item stopwatch_value_id in this.stopwatch_value_ids)
            {
                s += stopwatch_value_id.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("stopwatch_value_ids"), s);
        }
    }
}