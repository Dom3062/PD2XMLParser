using XMLParser.Elements.Attributes;
namespace XMLParser.Elements
{
    class ElementClock : ElementBase
    {
        List<element_item> hour_elements = new();
        List<element_item> minute_elements = new();
        List<element_item> second_elements = new();
        public ElementClock(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
            XmlNode node = null;
            LoadNode(ref node, "hour_elements");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode hour_element in node.ChildNodes)
                {
                    this.hour_elements.Add(new element_item(pos, Convert.ToInt32(hour_element.Attributes["value"].Value)));
                    pos++;
                }
            }
            node = null;
            LoadNode(ref node, "minute_elements");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode minute_element in node.ChildNodes)
                {
                    this.minute_elements.Add(new element_item(pos, Convert.ToInt32(minute_element.Attributes["value"].Value)));
                    pos++;
                }
            }
            node = null;
            LoadNode(ref node, "second_elements");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode second_element in node.ChildNodes)
                {
                    this.second_elements.Add(new element_item(pos, Convert.ToInt32(second_element.Attributes["value"].Value)));
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
            foreach (element_item hour_element in this.hour_elements)
            {
                if (hour_element.GetElementID() == id)
                {
                    hour_element.UpdateElementName(editor_name);
                    hour_element.UpdateElementDisabled(disabled);
                }
            }
            foreach (element_item minute_element in this.minute_elements)
            {
                if (minute_element.GetElementID() == id)
                {
                    minute_element.UpdateElementName(editor_name);
                    minute_element.UpdateElementDisabled(disabled);
                }
            }
            foreach (element_item second_element in this.second_elements)
            {
                if (second_element.GetElementID() == id)
                {
                    second_element.UpdateElementName(editor_name);
                    second_element.UpdateElementDisabled(disabled);
                }
            }
        }
        protected override void PrepareAttributes()
        {
            string hour_elements = "";
            foreach (element_item hour_element in this.hour_elements)
            {
                hour_elements += hour_element.ToString("\t\t");
            }
            AddAttribute("\thour_elements\n", hour_elements);
            string minute_elements = "";
            foreach (element_item minute_element in this.minute_elements)
            {
                minute_elements += minute_element.ToString("\t\t");
            }
            AddAttribute("\tminute_elements\n", minute_elements);
            string second_elements = "";
            foreach (element_item second_element in this.second_elements)
            {
                second_elements += second_element.ToString("\t\t");
            }
            AddAttribute("\tsecond_elements\n", second_elements);
        }
    }
}