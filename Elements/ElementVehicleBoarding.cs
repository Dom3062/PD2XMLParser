using XMLParser.Elements.Attributes;
namespace XMLParser.Elements
{
    class ElementVehicleBoarding : ElementBase
    {
        List<element_item> seats = new();
        public ElementVehicleBoarding(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
            XmlNode node = null;
            LoadNode(ref node, "seats_order");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode seat in node.ChildNodes)
                {
                    seats.Add(new element_item(pos, seat.Attributes["value"].Value));
                    pos++;
                }
            }
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (element_item seat in this.seats)
            {
                s += seat.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("seats_order"), s);
        }
    }
}