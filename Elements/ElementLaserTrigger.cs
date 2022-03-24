namespace XMLParser.Elements
{
    class connections
    {
        private readonly int pos;
        private readonly string from;
        private readonly string to;
        public connections(int pos, string from, string to)
        {
            this.pos = pos;
            this.from = from;
            this.to = to;
        }

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string prefix = "")
        {
            string double_prefix = prefix + "\t";
            string s = prefix + this.pos.ToString() + "\n";
            s += double_prefix + "from " + this.from + "\n";
            s += double_prefix + "to " + this.to + "\n";
            return s;
        }
    }

    class points
    {
        private readonly int id;
        private readonly string pos;
        private readonly string rot;
        public points(int id, string pos, string rot)
        {
            this.id = id;
            this.pos = pos.Replace(",", ".").Replace(" ", ", ");
            this.rot = rot.Replace(",", ".").Replace(" ", ", ");
        }

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string prefix = "")
        {
            string double_prefix = prefix + "\t";
            string s = prefix + this.id.ToString() + "\n";
            s += double_prefix + "pos " + this.pos + "\n";
            s += double_prefix + "rot " + this.rot + "\n";
            return s;
        }
    }

    class ElementLaserTrigger : ElementBase
    {
        List<connections> connections = new List<connections>();
        List<points> points = new List<points>();
        public ElementLaserTrigger(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
            XmlNode node = null;
            LoadNode(ref node, "connections");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode connection in node.ChildNodes)
                {
                    connections.Add(new connections(pos, connection.Attributes["from"].Value, connection.Attributes["to"].Value));
                    pos++;
                }
            }
            node = null;
            LoadNode(ref node, "points");
            if (node != null)
            {
                int pos = 1;
                foreach (XmlNode point in node.ChildNodes)
                {
                    points.Add(new points(pos, point.Attributes["pos"].Value, point.Attributes["rot"].Value));
                    pos++;
                }
            }
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (connections connection in this.connections)
            {
                s += connection.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("connections"), s);
            s = "";
            foreach (points point in this.points)
            {
                s += point.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("points"), s);
        }
    }
}