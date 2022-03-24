using XMLParser.Elements.Attributes;
namespace XMLParser.Elements
{
    class ElementNavObstacle : ElementBase
    {
        protected List<obstacle_list> obstacle_list;
        public ElementNavObstacle(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
            XmlNode unit_ids = null;
            LoadNode(ref unit_ids, "obstacle_list");
            ParseUnitIDs(unit_ids);
        }

        public override void UpdateUnitElements(string ElementName, int id, string unit_name, string unit_pos)
        {
            foreach (obstacle_list obstacle in this.obstacle_list)
            {
                if (obstacle.GetUnitID() == id)
                {
                    obstacle.UpdateUnitName(unit_name, unit_pos);
                }
            }
        }

        public override bool HasUnits()
        {
            return this.obstacle_list.Count > 0;
        }

        private void ParseUnitIDs(XmlNode obstacle_list)
        {
            this.obstacle_list = new List<obstacle_list>();
            if (obstacle_list == null)
            {
                return;
            }
            int pos = 1;
            foreach (XmlNode obstacle in obstacle_list)
            {
                int? guis_id = null;
                if (obstacle.Attributes["guis_id"] != null)
                {
                    guis_id = Convert.ToInt32(obstacle.Attributes["guis_id"].Value);
                }
                string obj_name = obstacle.Attributes["obj_name"].Value;
                int unit_id = Convert.ToInt32(obstacle.Attributes["unit_id"].Value);
                this.obstacle_list.Add(new obstacle_list(pos, guis_id, obj_name, unit_id));
                pos++;
            }
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (obstacle_list obstacle in this.obstacle_list)
            {
                s += obstacle.ToString("\t\t");
            }
            AddAttribute(FormatCustomElement("obstacle_list"), s);
        }
    }
}