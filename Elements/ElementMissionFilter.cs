using XMLParser.Elements.Attributes;
namespace XMLParser.Elements
{
    class ElementMissionFilter : ElementBase
    {
        List<filter> filters;
        public ElementMissionFilter(string c, string editor_name, int id, string node_name, XmlNodeList values) : base(c, editor_name, id, node_name, values)
        {
            int n = GetNumberOfUnloadedChilds();
            if (n == 0)
            {
                return;
            }
            filters = new List<filter>(n);
            List<filter> filter_temp = new List<filter>();
            XmlNode node = null;
            for (int i = n - 1; i >= 0; i--)
            {
                LoadNodeAtPosition(ref node, i);
                filter_temp.Add(new filter(i + 1, node.Attributes["value"].Value));
            }
            for (int i = n - 1; i >= 0; i--)
            {
                filters.Add(filter_temp[i]);
            }
            filter_temp.Clear();
        }

        protected override void PrepareAttributes()
        {
            string s = "";
            foreach (filter filter in this.filters)
            {
                s += filter.ToString("\t");
            }
            AddAttribute("", s);
        }
    }
}