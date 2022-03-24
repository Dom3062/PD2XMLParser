using XMLParser.Elements.Attributes;
namespace XMLParser.Elements
{
    class ElementCheckDLC : ElementBase
    {
        List<element_item> dlc_ids = new();
        public ElementCheckDLC(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
            XmlNode dlc_ids = null;
            LoadNode(ref dlc_ids, "dlc_ids");
            if (dlc_ids != null)
            {
                int pos = 1;
                foreach (XmlNode dlc_id in dlc_ids)
                {
                    this.dlc_ids.Add(new element_item(id, dlc_id.Attributes["value"].Value));
                    pos++;
                }
            }
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (element_item dlc_id in this.dlc_ids)
            {
                s += dlc_id.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("dlc_ids"), s);
        }
    }
}