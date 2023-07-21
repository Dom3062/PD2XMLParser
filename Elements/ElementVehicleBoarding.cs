using XMLParser.Elements.Attributes;
namespace XMLParser.Elements
{
    class ElementVehicleBoarding : ElementBase
    {
        List<element_item> seats = new();
        List<element_item> teleport_points = new();
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
            node = null;
            LoadNode(ref node, "teleport_points");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode teleport_point in node.ChildNodes)
                {
                    teleport_points.Add(new element_item(pos, teleport_point.Attributes["value"].Value));
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
            foreach (element_item teleport_point in this.teleport_points)
            {
                if (teleport_point.GetElementID() == id)
                {
                    teleport_point.UpdateElementName(editor_name);
                    teleport_point.UpdateElementDisabled(disabled);
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
            s = "";
            foreach (element_item teleport_point in this.teleport_points)
            {
                s += teleport_point.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("teleport_points"), s);
        }
    }
}